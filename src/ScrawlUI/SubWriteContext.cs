using ScrawlCore;

namespace ScrawlUI
{
    public class SubWriteContext : WriteContext
    {
        /// <summary>
        /// The context which this sub context was derived from.
        /// </summary>
        public WriteContext ParentContext { get; }

        /// <summary>
        /// Creates a new sub-context for writing, using a new scrawler.
        /// </summary>
        /// <param name="scrawler">The new scrawler to use for writing.</param>
        /// <param name="parentContext">The parent context, to birth this context from.</param>
        public SubWriteContext(Scrawler scrawler, WriteContext parentContext) 
            : base(scrawler, parentContext.Stringifier)
        {
            ParentContext = parentContext;

            NullSymbol = parentContext.NullSymbol;
            LineTerminator = parentContext.LineTerminator;
        }
    }
}
