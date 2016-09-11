using Moq;
using ScrawlCore.Stringification;
using System;
using Xunit;

using static Xunit.Assert;

namespace ScrawlCore.Test
{
    public class WriteContextTests
    {
        [Fact]
        public void CreateWithNullScrawler_ThrowsArgumentNullException()
            => Throws<ArgumentNullException>(() => new WriteContext(null, new NoopStringifier()));
        
        [Fact]
        public void CreateWithNullStringifier_ThrowsArgumentNullException()
            => Throws<ArgumentNullException>(() => new WriteContext(new Mock<Scrawler>().Object, null));

        [Fact]
        public void Write_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);

            const string testWord = "anything";

            scrawlerMock.Setup(s => s.Write(testWord))
                .Verifiable();
            
            context.Write(testWord);

            scrawlerMock.Verify();
        }

        [Fact]
        public void WriteLine_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);

            var testWord = "anything";
            
            var result = string.Empty;

            scrawlerMock.Setup(s => s.Write(It.IsAny<string>()))
                .Callback<string>(s => result += s)
                .Verifiable();

            context.WriteLine(testWord);

            scrawlerMock.Verify();

            Equal($"{testWord}{context.LineTerminator}", result);
        }

        [Fact]
        public void WriteObject_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);

            // We could check s.Write(string.Empty), 
            // but that's not the point of this test.
            scrawlerMock.Setup(s => s.Write(It.IsAny<string>()))
                .Verifiable();
                
            var tester = new
            {
                Name = "Earl",
                Occupation = "Tester",
                Age = (DateTime.UtcNow - new DateTime(1970, 1, 1, 10, 0, 0, DateTimeKind.Utc)).Days / 365,
                Assignment = "Getting written"
            };

            context.WriteObject(tester);

            scrawlerMock.Verify();
        }

        [Fact]
        public void WriteNull_WritesDefaultNullSymbolToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);
            
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
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);

            scrawlerMock.Setup(s => s.Write(context.LineTerminator))
                .Verifiable();

            context.NewLine();

            NotNull(context.LineTerminator);
            scrawlerMock.Verify();
        }

        [Theory]
        [InlineData("<null>")]
        [InlineData("NIL")]
        public void WriteNull_WritesNullSymbolToScrawler(string nullSymbol)
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);
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
            var stringifier = new NoopStringifier();

            var context = new WriteContext(scrawlerMock.Object, stringifier);
            context.LineTerminator = lineTerminator;

            scrawlerMock.Setup(s => s.Write(lineTerminator))
                .Verifiable();

            context.NewLine();

            Equal(lineTerminator, context.LineTerminator);
            scrawlerMock.Verify();
        }
    }
}
