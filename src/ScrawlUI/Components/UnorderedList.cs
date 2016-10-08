using ScrawlCore;
using System.Collections.Generic;

namespace ScrawlUI.Components
{
    public class UnorderedList : UIComponent
    {
        public IEnumerable<string> Items { get; }

        public UnorderedList(IEnumerable<string> items)
        {
            Items = items;
        }

        public void Write(ObjectWriteContext context)
        {
            foreach (var item in Items)
            {
                context.Write("- ");
                context.Write(item);
                context.NewLine();
            }
        }
    }
}
