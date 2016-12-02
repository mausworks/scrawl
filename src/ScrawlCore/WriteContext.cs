using System;

namespace ScrawlCore
{
    /// <summary>
    /// Provides helper methods for writing to a <see cref="ScrawlCore.Scrawler"/>.
    /// </summary>
    public class WriteContext
    {
        /// <summary>
        /// The symbol written using <see cref="WriteNull()"/>. Defaults to &lt;null&gt;.
        /// </summary>
        public virtual string NullSymbol { get; set; } = "<null>";

        /// <summary>
        /// The symbol written using <see cref="NewLine()"/>. Defaults to <see cref="Environment.NewLine"/>.
        /// </summary>
        public virtual string LineTerminator { get; set; } = Environment.NewLine;

        /// <summary>
        /// This context's scrawler, tasked with writing.
        /// </summary>
        public Scrawler Scrawler { get; }

        /// <summary>
        /// Creates a new write context which contains helper methods for writing to a <see cref="Scrawler"/>.
        /// </summary>
        /// <param name="scrawler">The scrawler to write to.</param>
        public WriteContext(Scrawler scrawler)
        {
            if (scrawler == null)
            {
                throw new ArgumentNullException(nameof(scrawler));
            }
            
            Scrawler = scrawler;
        }

        /// <summary>
        /// Writes the provided <paramref name="value"/> to this context's scrawler.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public virtual void Write(string value)
            => Scrawler.Write(value);
        
        /// <summary>
        /// Writes the provided value followed by this context's line terminator using <see cref="Write(string)"/>.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public virtual void WriteLine(string value)
        {
            Write(value);
            NewLine();
        }

        /// <summary>
        /// Writes this context's line terminator using <see cref="Write(string)"/>.
        /// </summary>
        public virtual void NewLine()
            => Write(LineTerminator);

        /// <summary>
        /// Writes this context's null symbol using <see cref="Write(string)"/>.
        /// </summary>
        public virtual void WriteNull()
            => Write(NullSymbol);
    }
}
