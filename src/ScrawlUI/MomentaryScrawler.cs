using ScrawlCore;

namespace ScrawlUI
{
    /// <summary>
    /// A scrawler that saves the last written value to the <see cref="Written"/> property.
    /// </summary>
    public class MomentaryScrawler : Scrawler
    {
        /// <summary>
        /// The last written value of the scrawler.
        /// </summary>
        public string Written => _written;

        private string _written;

        /// <summary>
        /// Sets <see cref="Written"/> to the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to set the current value to.</param>
        public void Write(string value)
            => _written = value;
    }
}
