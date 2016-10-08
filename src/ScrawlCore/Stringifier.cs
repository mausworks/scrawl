using System;

namespace ScrawlCore
{
    /// <summary>
    /// An interface which defines the functionality of object stringification.
    /// </summary>
    public interface Stringifier
    {
        /// <summary>
        /// Determines whether the provided <paramref name="type"/> can be stringified.
        /// </summary>
        /// <param name="type">The type to evaluate.</param>
        /// <returns><c>true</c> if the provided type can be stringified, otherwise <c>false</c>.</returns>
        bool CanStringify(Type type);

        /// <summary>
        /// Stringifies the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <returns>The stringified version of the provided <paramref name="value"/>.</returns>
        string Stringify(object value);
    }
}
