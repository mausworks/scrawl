using System.Linq;
using ScrawlCore;

namespace ScrawlUI.Components
{
	/// <summary>
    /// Contains extensions of the write context for the definition list component.
    /// </summary>
    public static partial class DefinitionListWriteContextExtensions
    {
        /// <summary>
        /// Writes a definition list from the provided model to the context.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="writeContext">The context to write the list to.</param>
        /// <param name="model">The model to create a list from.</param>
        public static void WriteDefinitionList<TModel>(this WriteContext writeContext, TModel model)
        {
            var definitionList = new DefinitionList<TModel>(model);
            definitionList.Write(writeContext);
        }
    }
}
