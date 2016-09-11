using System;
using ScrawlCore;

namespace ScrawlUI.Stringification
{
    /// <summary>
    /// Provides methods for stringifying boolean values.
    /// </summary>
    public sealed class BooleanStringifier : Stringifier
    {
        /// <summary>
        /// The symbol used when printing "true".
        /// </summary>
        public string TrueSymbol { get; }

        /// <summary>
        /// The symbol used when printing "false".
        /// </summary>
        public string FalseSymbol { get; }

        /// <summary>
        /// Creates a new boolean stringifier.
        /// </summary>
        /// <param name="trueSymbol">The symbol to use when stringifying <c>true</c>.</param>
        /// <param name="falseSymbol">The symbol to use when stringifying <c>false</c>.</param>
        public BooleanStringifier(string trueSymbol, string falseSymbol)
        {
            TrueSymbol = trueSymbol;
            FalseSymbol = falseSymbol;
        }
        
        /// <summary>
        /// Determines whether this type is boolean.
        /// </summary>
        /// <param name="type">The type to check.</param>
        public bool CanStringify(Type type)
            => type == typeof(bool);

        /// <summary>
        /// Stringifies the provided value using either the symbol for true or false.
        /// </summary>
        /// <param name="value">The boolean value to stringify.</param>
        /// <returns>A stringified version of the boolean value.</returns>
        public string Stringify(object value)
            => (bool)value ? TrueSymbol : FalseSymbol;
    }
}
