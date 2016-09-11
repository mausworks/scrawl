using System;

namespace ScrawlCore
{
    /// <summary>
    /// This class provides a context for writing. It combines scrawling and stringification into a nice hot soup.
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
        /// This context's primary object stringifier.
        /// </summary>
        public Stringifier Stringifier { get; }

        /// <summary>
        /// This context's scrawler, tasked with writing.
        /// </summary>
        public Scrawler Scrawler { get; }

        /// <summary>
        /// Initializes a new <see cref="WriteContext"/> using the provided primary and surrogate stringifiers.
        /// </summary>
        /// <param name="scrawler">The scrawler to use for writing. This parameter may not be null.</param>
        /// <param name="primaryStringifier">The stringifier to use for object stringification. To use multiple stringifiers, use a <see cref="CompositeStringifier"/>. This parameter may not be null.</param>
        public WriteContext(Scrawler scrawler, Stringifier primaryStringifier)
        {
            if (scrawler == null)
            {
                throw new ArgumentNullException(nameof(scrawler));
            }
            if (primaryStringifier == null)
            {
                throw new ArgumentNullException(nameof(primaryStringifier));
            }

            Scrawler = scrawler;
            Stringifier = primaryStringifier;
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

        /// <summary>
        /// Writes the provided object using <see cref="Write(string)"/> by first stringifying the provided object using this context's primary or surrogate stringifier.
        /// If null is passed; this context's null symbol will be written instead 
        /// </summary>
        /// <param name="obj">The object to stringify and write, may be null.</param>
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
