using TrustCore;

namespace TrustUI.Stringification
{
    /// <summary>
    /// Defines the options for a concatenator.
    /// </summary>
    public class ConcatenatorOptions
    {
        /// <summary>
        /// The delimiter to use when concatenating elements.
        /// </summary>
        public string Delimiter { get; set; } = ", ";

        /// <summary>
        /// The return format of the concatinator.
        /// <para>{0} = the concatenated items. {1} = the number of items.</para>
        /// </summary>
        public string Format { get; set; } = "[{1}] {0}";

        /// <summary>
        /// Gets or sets the stringifier to use for all elements within a sequence.
        /// </summary>
        public Stringifier ElementStringifier { get; set; } 
            = new StructStringifier();
    }
}
