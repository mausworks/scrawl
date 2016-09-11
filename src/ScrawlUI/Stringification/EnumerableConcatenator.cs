using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ScrawlUI.Stringification
{
    /// <summary>
    /// Provides methods for stringifying <see cref="IEnumerable"/>'s with simple types, such as value-type arrays.
    /// </summary>
    public class EnumerableConcatenator : EnumerableStringifier
    {
        /// <summary>
        /// The options for this concatenator.
        /// </summary>
        public ConcatenatorOptions Options { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableConcatenator"/> class with default options.
        /// </summary>
        public EnumerableConcatenator()
            : this(new ConcatenatorOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableConcatenator"/> class with provided <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The options to use for this concatenator.</param>
        public EnumerableConcatenator(ConcatenatorOptions options) 
            : base(options.ElementStringifier)
        {
            Options = options;
        }

        /// <summary>
        /// Stringifies the provided value by concatenating the elements of the enumerable.
        /// </summary>
        /// <param name="value">The value to stringify, must be derived from <see cref="IEnumerable"/>.</param>
        /// <returns>A string concatenated according to the provided <see cref="ConcatenatorOptions"/>.</returns>
        public override string Stringify(object value)
        {
            var stringValues = StringifyAll((IEnumerable)value).ToList();

            return string.Format(Options.Format, 
                string.Join(Options.Delimiter, stringValues), stringValues.Count);
        }
    }
}
