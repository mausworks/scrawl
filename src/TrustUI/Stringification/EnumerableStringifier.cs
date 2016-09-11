using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using ScrawlCore;

namespace ScrawlUI.Stringification
{
    /// <summary>
    /// Provides base methods for stringifying <see cref="IEnumerable"/>'s.
    /// </summary>
    public abstract class EnumerableStringifier : Stringifier
    {
        /// <summary>
        /// Gets the stringifier tasked with stringifying each element of the enumerable.
        /// </summary>
        public Stringifier ElementStringifier { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableStringifier"/> 
        /// with the provided elements stringifier.
        /// </summary>
        /// <param name="elementsStringifier">The stringifier to use for every element of the provided enumerable.</param>
        public EnumerableStringifier(Stringifier elementsStringifier)
        {
            ElementStringifier = elementsStringifier;
        }

        /// <summary>
        /// Determines whether the provided <paramref name="type"/> can be stringified by this stringifier.
        /// </summary>
        /// <param name="type">The type to check whether it can be stringified.</param>
        /// <returns></returns>
        public bool CanStringify(Type type)
            => IsEnumerable(type.GetTypeInfo());

        /// <summary>
        /// Stringifies the value.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <returns></returns>
        public abstract string Stringify(object value);
        
        /// <summary>
        /// The type info of <see cref="IEnumerable"/>.
        /// </summary>
        private readonly TypeInfo _enumerableTypeInfo 
            = typeof(IEnumerable).GetTypeInfo();

        /// <summary>
        /// Determines whether the provided type info is assignable from <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="typeInfo">The type info to check whether it is enumerable.</param>
        protected bool IsEnumerable(TypeInfo typeInfo)
            => _enumerableTypeInfo.IsAssignableFrom(typeInfo);

        /// <summary>
        /// Stringifies all elements and returns them in a sequence.
        /// </summary>
        /// <param name="enumerable">The enumerable to stringify all elements from.</param>
        /// <returns></returns>
        protected IEnumerable<string> StringifyAll(IEnumerable enumerable)
        {
            foreach (var element in enumerable)
            {
                var str = element as string;

                if (str != null)
                {
                    yield return str;
                }

                if (ElementStringifier.CanStringify(element.GetType()))
                {
                    yield return ElementStringifier.Stringify(element);
                }
            }
        }
    }
}
