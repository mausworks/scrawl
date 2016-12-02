using ScrawlCore;
using System.Text;

namespace ScrawlUI
{
    public class StringBuilderScrawler : Scrawler
    {
        public const int DefaultBuilderCapacity = 128;

        private readonly StringBuilder _stringBuilder;

        public StringBuilderScrawler()
            : this(DefaultBuilderCapacity)
        {

        }

        public StringBuilderScrawler(int capacity)
        {
            _stringBuilder = new StringBuilder(capacity);
        }

        public void Write(string value)
            => _stringBuilder.Append(value);
    }
}
