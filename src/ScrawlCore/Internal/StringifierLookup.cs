using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrawlCore.Internal
{
    /// <summary>
    /// A class that looks up stringifiers from types.
    /// </summary>
    public class StringifierLookup
    {
        /// <summary>
        /// The original list of stringifiers.
        /// </summary>
        public IList<Stringifier> _stringifiers;

        /// <summary>
        /// A dictionary mapping types to an instance of a stringifier.
        /// There is a one-to-many relationship between types and stringifiers.
        /// </summary>
        public IDictionary<Type, Stringifier> _knownStringifiers;

        /// <summary>
        /// A hash set containing types that cannot be stringified by any provided stringifier.
        /// Used to improve performance in <see cref="CanGetStringifier(Type)"/>.
        /// </summary>
        private HashSet<Type> _unstringifiableTypes;

        /// <summary>
        /// Creates a new stringifier lookup from the provided stringifiers.
        /// </summary>
        /// <param name="stringifiers">Enumerable stringifiers to use within this lookup.</param>
        /// <param name="knownStringifiedTypes">
        ///     Enumerable types that will be seeded to a dictionary of "known types", this speeds the lookup process up.
        ///     The more known types provided, the better performance, in theory.
        /// </param>
        public StringifierLookup(IEnumerable<Stringifier> stringifiers, IEnumerable<Type> knownStringifiedTypes)
        {
            _stringifiers = stringifiers as List<Stringifier> ?? stringifiers.ToList();
            
            SeedKnownStringifiers(knownStringifiedTypes as ICollection<Type> ?? knownStringifiedTypes.ToList());

            _unstringifiableTypes = new HashSet<Type>();
        }

        /// <summary>
        /// Looks up a known stringifier from a type, throws if not found.
        /// <para>A stringifier is only known for the provided type if <see cref="CanGetStringifier(Type)"/> has been called for that particular type.</para>
        /// </summary>
        /// <param name="type">The type to look up the stringifier from, may not be null.</param>
        /// <returns>A known stringifier, if successful.</returns>
        public Stringifier Get(Type type)
            => _knownStringifiers[type];

        /// <summary>
        /// Determines whether this lookup has a stringifier for the provided <paramref name="type"/>.
        /// <para>Calling this method makes the provided type "known", and a stringifier can be fetched via Get</para>
        /// </summary>
        /// <param name="type">The type to lookup, may not be null.</param>
        /// <returns><c>true</c> if this lookup has a stringifier for the provided type.</returns>
        public bool CanGetStringifier(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type), "Cannot lookup stringifier of <null> type.");
            }
            
            if (IsKnownUnstringifiableType(type))
            {
                return false;
            }

            if (!_knownStringifiers.ContainsKey(type))
            {
                var stringifier = _stringifiers.FirstOrDefault(sf => sf.CanStringify(type));

                if (stringifier != null)
                {
                    AddToKnownStringifiersOrThrow(type, stringifier);

                    return true;
                }
                
                _unstringifiableTypes.Add(type);

                return false;
            }

            return true;
        }

        private bool IsKnownUnstringifiableType(Type type)
            => _unstringifiableTypes.Contains(type);

        /// <summary>
        /// Pretty much does what it says on the tin.
        /// </summary>
        /// <param name="type">The type (key) to add to the known stringifiers.</param>
        /// <param name="stringifier">The stringifier (value) to add to the known stringifiers.</param>
        private void AddToKnownStringifiersOrThrow(Type type, Stringifier stringifier)
        {
            try
            {
                _knownStringifiers.Add(type, stringifier);
            }
            catch (ArgumentException ex)
            {
                throw new InvalidOperationException($"A stringifier for the type '{type}' has already been added.", ex);
            }
        }

        /// <summary>
        /// Pretty much does what it says on the tin.
        /// </summary>
        /// <param name="type">The type to lookup.</param>
        private Stringifier FindStringifierForTypeOrThrow(Type type)
        {
            try
            {
                return _stringifiers.First(s => s.CanStringify(type));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(
                    $"A stringifier for the type '{type.FullName}' has not been provided.", ex);
            }
        }

        /// <summary>
        /// Seeds the "known stringifiers" dictionary from the provided types.
        /// </summary>
        /// <param name="seedTypes">The types to seed the dictionary with.</param>
        private void SeedKnownStringifiers(ICollection<Type> seedTypes)
        {
            var initialCapacity = GetInitialKnownStringifiersCapacity(seedTypes.Count);

            _knownStringifiers = new Dictionary<Type, Stringifier>(initialCapacity);

            foreach (var type in seedTypes)
            {
                var stringifier = FindStringifierForTypeOrThrow(type);
                
                if (stringifier.CanStringify(type))
                {
                    AddToKnownStringifiersOrThrow(type, stringifier);
                }
            }
        }
        
        /// <summary>
        /// Gets a vague (some would say rough) estimate of how many items should be in the <see cref="_knownStringifiers"/> dictionary.
        /// <para>Roughly calculated from the provided <paramref name="seedTypesCount"/> or the _stringifiers count.</para>
        /// </summary>
        /// <param name="seedTypesCount">The number of types to (possibly) seed the dictionary with.</param>
        private int GetInitialKnownStringifiersCapacity(int seedTypesCount)
        {
            // Even if there is no direct one-to-one relationship between types and stringifiers,
            // we can instantiate the "known identifiers"-dictionary size with a high estimate.
            // This estimate is grasped from thin air though and is probably off by a factor.

            // The estimate is first based on the largest collection.
            if (seedTypesCount > _stringifiers.Count)
            {
                // When passing in the known seed types, the implementer probably
                // has a rough idea of how many types that will realistically be stringified during runtime.
                // - But nobody is perfect, so we multiply it by a magic number.
                return (int)Math.Round(seedTypesCount * 1.2f);
            }

            // The _knownStringifiers will contain multiple values that are references to the same stringifier.
            // 
            // For example:
            //  
            // The SimpleTypesStringifier can convert all value types. 
            // - The following examples all evaluate to true:
            //
            //  SimpleTypesStringifier.CanStringify(long);
            //                        .CanStringify(TimeSpan);
            //                        .CanStringify(int);
            //                        .CanStringify(DateTime);
            //                                      + More
            //
            // The known stringifier will look something like this:
            //
            //  { typeof(long), <SimpleTypesStringifier> } 
            //  { typeof(TimeSpan), <SimpleTypesStringifier> } 
            //  { typeof(int), <SimpleTypesStringifier> } 
            //  { typeof(DateTime), <SimpleTypesStringifier> } 
            //
            // Let's imagine that's a relationship of 1:20 - however.;
            // What also needs to be taken into consideration is how many types that will 
            // realistically be stringified during runtime.
            // Say for instance long, DateTime and TimeSpan gets stringified, 
            // this is far less than *all value types*, of which there are theoretically infinite.
            //
            // So, to take this into consideration, we just add X to the formula.
            // I let X be 8 for now.
            return (int)Math.Round((_stringifiers.Count * 1.2f) + 8);
        }
    }
}
