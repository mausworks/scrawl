using System;

namespace TrustCore
{
    /// <summary>
    /// Provides a context for writing. 
    /// </summary>
    public abstract class WriteContext
    {
        /// <summary>
        /// The symbol used instead of empty values for null references. Set to "&lt;null&gt;" by default.
        /// </summary>
        public virtual string NullSymbol { get; set; } = "<null>";

        /// <summary>
        /// The symbol used for terminating lines. Set to <see cref="Environment.NewLine"/> by default.
        /// </summary>
        public virtual string LineTerminator { get; set; } = Environment.NewLine;

        /// <summary>
        /// This context's primary object stringifier.
        /// </summary>
        public Stringifier Stringifier { get; }

        /// <summary>
        /// Initializes a new <see cref="WriteContext"/> using the provided primary and surrogate stringifiers.
        /// </summary>
        /// <param name="primaryStringifier">The stringifier to use for object stringification. To use multiple stringifiers, use a <see cref="CompositeStringifier"/>. This parameter may not be null.</param>
        /// <param name="surrogateStringifier">The stringifier to use when the <paramref name="primaryStringifier"/> cannot stringify an object. This parameter may not be null.</param>
        protected WriteContext(Stringifier primaryStringifier)
        {
            if (primaryStringifier == null)
            {
                throw new ArgumentNullException(nameof(primaryStringifier));
            }

            Stringifier = primaryStringifier;
        }

        /// <summary>
        /// When overridden in a derived class, writes the provided <paramref name="value"/> to a source.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Write(string value);

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
