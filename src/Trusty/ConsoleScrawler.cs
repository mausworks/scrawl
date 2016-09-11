using System;
using TrustCore;

namespace Trustyy
{
    /// <summary>
    /// A scrawler that implements <see cref="Console.Write(string)"/>.
    /// </summary>
    public class ConsoleScrawler : Scrawler
    {
        /// <summary>
        /// Writes to the <see cref="Console"/>.
        /// </summary>
        /// <param name="value">The value to write to the console.</param>
        public void Write(string value)
            => Console.Write(value);
    }
}
