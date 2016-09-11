using System;
using System.Reflection;
using ScrawlCore;

namespace ScrawlUI.Stringification
{
    /// <summary>
    /// Provides methods for stringifying simple value types.
    /// </summary>
    public class StructStringifier : Stringifier
    {
        /// <summary>
        /// Determines whether the provided type is a value type.
        /// </summary>
        /// <param name="type">The type to check whether it can be stringified.</param>
        /// <returns><c>true</c> if the type can be stringified.</returns>
        public bool CanStringify(Type type)
            => type.GetTypeInfo().IsValueType;

        /// <summary>
        /// Stringifies the provided object by calling <see cref="object.ToString()"/> on it.
        /// </summary>
        /// <param name="value">The object to stringify.</param>
        /// <returns>A string representation of the value, if successful.</returns>
        public string Stringify(object value)
            => value.ToString();
    }
}
