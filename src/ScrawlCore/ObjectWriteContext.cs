using System;

namespace ScrawlCore
{
    /// <summary>
    /// This class provides methods for writing objects and strings to a <see cref="Scrawler"/>.
    /// </summary>
    public class ObjectWriteContext : WriteContext
    {
        /// <summary>
        /// This context's object stringifier.
        /// </summary>
        public Stringifier Stringifier { get; }

        /// <summary>
        /// Creates a new write context which contains helper methods for writing to a <see cref="Scrawler"/>.
        /// </summary>
        /// <param name="scrawler">The scrawler to use for writing.</param>
        /// <param name="stringifier">The stringifier to use for object stringification.</param>
        public ObjectWriteContext(Scrawler scrawler, Stringifier stringifier)
            : base(scrawler)
        {
            if (stringifier == null)
            {
                throw new ArgumentNullException(nameof(stringifier));
            }

            Stringifier = stringifier;
        }

        /// <summary>
        /// Writes the provided object by stringifying it.
        /// If null is passed; this context's null symbol will be written instead 
        /// </summary>
        /// <param name="obj">The object to stringify and write.</param>
        public virtual void WriteObject(object obj)
        {
            if (obj == null)
            {
                WriteNull();

                return;
            }
            
            var str = obj as string;

            if (str != null)
            {
                // Strings are directly written to the context.
                // No need for stringification.
                Write(str);

                return;
            }

            var objectType = obj.GetType();

            if (Stringifier.CanStringify(objectType))
            {
                Write(Stringifier.Stringify(obj));

                return;
            }

            throw new NotSupportedException($"The provided object of the type '{objectType.Name}' could not be stringified. A stringifier has not been provided for the type.");
        }
    }
}
