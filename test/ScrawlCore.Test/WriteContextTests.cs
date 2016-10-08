using Moq;
using System;
using Xunit;

using static Xunit.Assert;

namespace ScrawlCore.Test
{
    public class WriteContextTests
    {
        public const string TestWord = "anything";

        [Fact]
        public void CreateWithNullScrawler_ThrowsArgumentNullException()
            => Throws<ArgumentNullException>(() => new WriteContext(null));
        
        [Fact]
        public void CreateWithNullStringifier_ThrowsArgumentNullException()
            => Throws<ArgumentNullException>(() => new ObjectWriteContext(new Mock<Scrawler>().Object, null));

        [Fact]
        public void Write_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            scrawlerMock.Setup(s => s.Write(TestWord))
                .Verifiable();
            
            context.Write(TestWord);

            scrawlerMock.Verify();
        }

        [Fact]
        public void WriteWithNullRef_WritesNullRefToScrawler()
        {
            // Writing a null reference to the context should
            // not write the context's null symbol, but rather a null reference.
            // This might seem a bit "offensive" at first,
            // but isn't really. Implementations must be specific for when to write the null
            // symbol and when to write null references, using either Write(null) or WriteNull()
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            scrawlerMock.Setup(s => s.Write(null))
                .Verifiable();

            context.Write(null);

            scrawlerMock.Verify();
        }

        [Fact]
        public void WriteLine_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            var result = string.Empty;

            scrawlerMock.Setup(s => s.Write(It.IsAny<string>()))
                .Callback<string>(s => result += s)
                .Verifiable();

            context.WriteLine(TestWord);

            scrawlerMock.Verify();

            Equal($"{TestWord}{context.LineTerminator}", result);
        }

        [Fact]
        public void WriteNull_WritesDefaultNullSymbolToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);
            
            scrawlerMock.Setup(s => s.Write(context.NullSymbol))
                .Verifiable();

            context.WriteNull();

            NotNull(context.NullSymbol);
            scrawlerMock.Verify();
        }

        [Fact]
        public void NewLine_WritesDefaultLineTerminatorToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            scrawlerMock.Setup(s => s.Write(context.LineTerminator))
                .Verifiable();

            context.NewLine();

            NotNull(context.LineTerminator);
            scrawlerMock.Verify();
        }

        [Theory]
        [InlineData("<null>")]
        [InlineData("NIL")]

        // Having null as null symbol should be perfectly valid.
        [InlineData(null)] 
        public void WriteNull_WritesNullSymbolToScrawler(string nullSymbol)
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            context.NullSymbol = nullSymbol;

            scrawlerMock.Setup(s => s.Write(nullSymbol))
                .Verifiable();

            context.WriteNull();

            Equal(nullSymbol, context.NullSymbol);
            scrawlerMock.Verify();
        }
        
        [Theory]
        [InlineData("\r\n")]
        [InlineData("\n")]
        public void NewLine_WritesToScrawler(string lineTerminator)
        {
            var scrawlerMock = new Mock<Scrawler>();
            var context = new WriteContext(scrawlerMock.Object);

            context.LineTerminator = lineTerminator;

            scrawlerMock.Setup(s => s.Write(lineTerminator))
                .Verifiable();

            context.NewLine();

            Equal(lineTerminator, context.LineTerminator);
            scrawlerMock.Verify();
        }
    }
}
