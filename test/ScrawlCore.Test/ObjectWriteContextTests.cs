using Moq;
using ScrawlCore.Stringification;
using System;
using Xunit;

using static Xunit.Assert;

namespace ScrawlCore.Test
{
    public class ObjectWriteContextTests
    {
        [Fact]
        public void CreateWithNullStringifier_ThrowsArgumentNullException()
            => Throws<ArgumentNullException>(() => new ObjectWriteContext(new Mock<Scrawler>().Object, null));
        
        [Fact]
        public void WriteObject_WritesToScrawler()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NullStringifier();

            var context = new ObjectWriteContext(scrawlerMock.Object, stringifier);

            // We could check s.Write(string.Empty), 
            // but that's not the point of this test.
            scrawlerMock.Setup(s => s.Write(It.IsAny<string>()))
                .Verifiable();

            var tester = new
            {
                Name = "Earl",
                Occupation = "Tester",
                Age = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Days / 365,
                Assignment = "Getting written"
            };

            context.WriteObject(tester);

            scrawlerMock.Verify();
        }

        [Fact]
        public void WriteObjectWithNullRef_WritesNullSymbol()
        {
            var scrawlerMock = new Mock<Scrawler>();
            var stringifier = new NullStringifier();

            var context = new ObjectWriteContext(scrawlerMock.Object, stringifier);

            scrawlerMock.Setup(s => s.Write(context.NullSymbol))
                .Verifiable();

            context.WriteObject(null);
            
            scrawlerMock.Verify();
        }
    }
}
