using System;

namespace TrustCore
{
    /// <summary>
    /// An interface which defines the functionality of object stringification.
    /// </summary>
    public interface Stringifier
    {
        /// <summary>
        /// Determines whether the provided <paramref name="type"/> can be stringified by this stringifier.
        /// </summary>
        /// <param name="type">The type to evaluate, may never be null.</param>
        /// <returns><c>true</c> if the provided type can be stringified using this stringifier, otherwise <c>false</c>.</returns>
        bool CanStringify(Type type);

        /// <summary>
        /// Stringifies the provided <paramref name="value"/>, may throw exceptions.
        /// </summary>
        /// <param name="value">The value to stringify, may never be null.</param>
        /// <returns>A stringified version of the provided <paramref name="value"/>, if successful.</returns>
        string Stringify(object value);
    }
}
