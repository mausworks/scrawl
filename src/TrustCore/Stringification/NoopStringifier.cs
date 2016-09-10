namespace TrustCore.Stringification
{
    /// <summary>
    /// Stringifies objects by returning an empty string. This stringifier can stringify any type.
    /// </summary>
    public sealed class NoopStringifier : AnyTypeStringifier
    {
        /// <summary>
        /// Returns <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="value">[ignored]</param>
        /// <returns>An empty string</returns>
        public override string Stringify(object value)
            => string.Empty;
    }
}
