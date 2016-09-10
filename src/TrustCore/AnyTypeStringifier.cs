using System;

namespace TrustCore
{
    /// <summary>
    /// An abstract class that denotes that a stringifier can stringify any type.
    /// </summary>
    public abstract class AnyTypeStringifier : Stringifier
    {
        /// <summary>
        /// Returns <c>true</c>.
        /// </summary>
        /// <param name="type">[ignored]</param>
        /// <returns><c>true</c></returns>
        public bool CanStringify(Type type)
            => true;

        /// <summary>
        /// When overridden in a derived class - stringifies the provided <paramref cref="value"/>.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <returns>The stringified value.</returns>
        public abstract string Stringify(object value);
    }
}
