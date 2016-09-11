using System;
using System.Linq;
using System.Reflection;
using ScrawlCore;

namespace ScrawlUI.Components
{
    public class DefinitionList<TModel> : UIComponent
    {
        public TypeInfo ModelType { get; }

        public TModel Model { get; }

        public DefinitionList(TModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            Model = model;
            ModelType = typeof(TModel).GetTypeInfo();
        }

        public void Write(WriteContext context)
        {
            var properties = ModelType.GetProperties();
            var longestPropertyName = properties.Max(p => p.Name.Length);
            var padWidth = longestPropertyName;

            foreach (var property in properties)
            {
                var dt = property.Name.PadRight(padWidth);

                context.Write($"{dt} : ");
                context.WriteObject(property.GetValue(Model));
                context.NewLine();
            }
        }
    }
}
