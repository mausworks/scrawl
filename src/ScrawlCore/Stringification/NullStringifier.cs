namespace ScrawlCore.Stringification
{
    /// <summary>
    /// Stringifies objects by returning a null reference. 
    /// This stringifier can stringify any type and is considered no-op.
    /// </summary>
    public sealed class NullStringifier : AnyTypeStringifier
    {
        /// <summary>
        /// Returns a null reference.
        /// </summary>
        /// <param name="value">[ignored]</param>
        /// <returns>a null reference./returns>
        public override string Stringify(object value)
            => null;
    }
}
