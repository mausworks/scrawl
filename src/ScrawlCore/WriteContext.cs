using System;

namespace ScrawlCore
{
    /// <summary>
    /// Provides helper methods for writing to a <see cref="ScrawlCore.Scrawler"/>.
    /// </summary>
    public class WriteContext
    {
        /// <summary>
        /// The symbol to write instead of empty values for null references. Set to "&lt;null&gt;" by default, but may be overridden.
        /// </summary>
        public virtual string NullSymbol { get; set; } = "<null>";

        /// <summary>
        /// The symbol used for terminating lines. Set to <see cref="Environment.NewLine"/> by default, but may be overridden.
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
        /// When overridden in a derived class, writes the provided <paramref name="value"/> to a source.
        /// </summary>
        /// <param name="value"></param>
        public virtual void Write(string value)
            => Scrawler.Write(value);
        
        /// <summary>
        /// Writes the provided value followed by this context's line terminator using <see cref="Write(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        public virtual void WriteLine(string value)
        {
            Write(value);
            Write(LineTerminator);
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
