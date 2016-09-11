using ScrawlCore;

namespace ScrawlUI
{
    public static class ScrawlContextExtensions
    {
        /// <summary>
        /// Creates a new context with the same properties as the current context, but with the provided sub-scrawler.
        /// </summary>
        /// <param name="context">The context to create a sub context from.</param>
        /// <param name="subscrawler">The scrawler to use in the sub context.</param>
        /// <returns></returns>
        public static SubWriteContext CreateSubContext(this WriteContext context, Scrawler subscrawler)
            => new SubWriteContext(subscrawler, context);
    }
}
