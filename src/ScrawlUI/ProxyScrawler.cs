using ScrawlCore;
using System;

namespace ScrawlUI
{
    /// <summary>
    /// A class that provides a proxy action which is invoked on <see cref="Write(string)"/>, useful for capturing output when writing to a context.
    /// </summary>
    public class ProxyScrawler : Scrawler
    {
        /// <summary>
        /// The action which is invoked on <see cref="Write(string)"/>.
        /// </summary>
        private Action<string> _onWrite;

        /// <summary>
        /// The action which is invoked on <see cref="Write(string)"/>
        /// </summary>
        private Action<string> _onWriteLine;

        /// <summary>
        /// Sets an action that is invoked by <see cref="Write(string)"/>.
        /// </summary>
        /// <param name="proxyAction">An action that is invoked by <see cref="Write(string)"/>.</param>
        public void OnWrite(Action<string> proxyAction)
            => _onWrite = proxyAction;


        /// <summary>
        /// Invokes the underlaying proxy action with the provided <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to pass to the proxy action.</param>
        public void Write(string value)
           => _onWrite(value);
    }
}
