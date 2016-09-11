namespace ScrawlCore.Stringification
{
    /// <summary>
    /// Stringifies objects by invoking <see cref="object.ToString()"/> on it. This stringifier can stringify any type.
    /// </summary>
    public sealed class ToStringStringifier : AnyTypeStringifier
    {
        /// <summary>
        /// Stringifies the provided <see cref="value"/> by calling <see cref="object.ToString()"/>.
        /// </summary>
        /// <param name="value">The value to stringify, may not be null.</param>
        /// <returns>A string that represents the value.</returns>
        public override string Stringify(object value)
            => value.ToString();
    }
}
