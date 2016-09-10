namespace TrustCore.Stringification
{
    /// <summary>
    /// Stringifies objects by returning their type names. This stringifier can stringify any type.
    /// </summary>
    public sealed class TypeNameStringifier : AnyTypeStringifier
    {
        /// <summary>
        /// Stringifies the provided value by printing its type name.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <returns>The provided <paramref name="value"/>'s type name.</returns>
        public override string Stringify(object value)
            => value.GetType().Name;
    }
}
