using System;
using System.Collections.Generic;
using System.Linq;
using ScrawlCore.Internal;

namespace ScrawlCore.Stringification
{
    /// <summary>
    /// Allows for passing multiple stringifiers as a single stringifier.
    /// </summary>
    public class CompositeStringifier : Stringifier
    {
        public StringifierLookup StringifierLookup { get; }
     
        /// <summary>
        /// Creates a new composite stringifier using the provided <paramref name="stringifiers"/>.
        /// </summary>
        /// <param name="stringifiers">The stringifier to use in this composite.</param>
        public CompositeStringifier(params Stringifier[] stringifiers)
            : this((IEnumerable<Stringifier>)stringifiers)
        {  
        }

        /// <summary>
        /// Creates a new composite stringifier using the provided <paramref name="stringifiers"/>.
        /// </summary>
        /// <param name="stringifiers">The stringifiers to use in this composite.</param>
        public CompositeStringifier(IEnumerable<Stringifier> stringifiers)
            : this(stringifiers, Enumerable.Empty<Type>())
        {
        }

        /// <summary>
        /// Creates a new composite stringifier using the provided <paramref name="stringifiers"/> 
        /// and then seeds the underlaying lookup with the provided <paramref name="knownStringifiedTypes"/> for faster lookup.
        /// </summary>
        /// <param name="stringifiers">The stringifiers to use in this composite, may not be null.</param>
        /// <param name="knownStringifiedTypes">The types which are known to be stringified, if any of the provided types cannot be stringified a <see cref="InvalidOperationException"/> will be thrown.</param>
        public CompositeStringifier(IEnumerable<Stringifier> stringifiers, IEnumerable<Type> knownStringifiedTypes)
        {
            if (stringifiers == null)
            {
                throw new ArgumentNullException(nameof(stringifiers));
            }
            if (knownStringifiedTypes == null)
            {
                throw new ArgumentNullException(nameof(knownStringifiedTypes));
            }

            var stringifierList = stringifiers as IList<Stringifier> 
                ?? stringifiers.ToList();

            if (stringifierList.Count == 0)
            {
                throw new ArgumentException("Must provide one ore more stringifiers.", nameof(stringifiers));
            }

            // Leaky...
            StringifierLookup = new StringifierLookup(stringifierList, knownStringifiedTypes);
        }


        /// <summary>
        /// Determines whether the provided <paramref name="type"/> can be stringified by any of the stringifiers contained in this composite.
        /// </summary>
        /// <param name="type">The type to lookup whether it can be stringified.</param>
        /// <returns><c>true</c> if the provided type can be stringified, otherwise <c>false</c>.</returns>
        public bool CanStringify(Type type)
            => StringifierLookup.CanGetStringifier(type);

        /// <summary>
        /// Stringifies the provided <paramref name="value"/> using the first known stringifier for the type.
        /// Throws if the provided <paramref name="value"/> cannot be stringified.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <returns>A stringified version of the provided <see cref="value"/>, if successful.</returns>
        public string Stringify(object value)
        {
            var stringifier = StringifierLookup.Get(value.GetType());

            return stringifier.Stringify(value);
        }
    }
}
