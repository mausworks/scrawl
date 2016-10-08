namespace ScrawlCore
{
    /// <summary>
    /// An interface which defines the functionality of scrawling.
    /// <para>Scrawl [verb]: write (something) in a hurried, (careless) way.</para>
    /// </summary>
    public interface Scrawler
    {
        /// <summary>
        /// Writes the provided <paramref name="value"/>. 
        /// </summary>
        /// <param name="value">The value to write.</param>
        void Write(string value);
    }
}
