using System;
using System.Linq;
using System.Reflection;
using ScrawlCore;

namespace ScrawlUI.Components
{
    public class DefinitionList : UIComponent
    {
        public TypeInfo ModelType { get; }

        public object Model { get; }

        public DefinitionList(object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            Model = model;
            ModelType = model.GetType().GetTypeInfo();
        }

        public void Write(ObjectWriteContext context)
        {
            var properties = ModelType.GetProperties();
            var longestPropertyName = properties.Max(p => p.Name.Length);
            
            foreach (var property in properties)
            {
                context.Write($"{property.Name.PadRight(longestPropertyName)} : ");
                context.WriteObject(property.GetValue(Model));
                context.NewLine();
            }
        }
    }
}
