using System;
using ScrawlCore;
using ScrawlUI.Stringification;
using ScrawlUI.Components;
using System.Linq;
using System.Collections.Generic;
using ScrawlCore.Stringification;
using ScrawlUI;

namespace Scrawly
{
    public class Program
    {
        public static ObjectWriteContext ConsoleContext = new ObjectWriteContext(new ConsoleScrawler(), new CompositeStringifier(
                new BooleanStringifier("yes", "no"),
                new StructStringifier(),
                new EnumerableConcatenator(),

                // Add a AnyTypeStringifer at the end to handle unknown types.
                new TypeNameStringifier()));

        public static void Main(string[] args)
        {
            var now = DateTime.Now;

            var foo = new
            {
                Title = "Welcome!",
                Description = "This is a definition list test!",
                CurrentDateAndTime = now,
                TimeSinceYesterday = now - now.AddDays(-1),
                Colors = new[] { "Purple", "Green", "Magenta", "Brown", "Turquoise" },
                TrueToken = true,
                FalseToken = false,
                Primes = Primes(10),
                UnknownObject = ConsoleContext
            };

            var dl = CreateDefinitionList(foo);

            var container = new Container(dl)
            {
                Bordered = true,
                Padding = new Offset(1, 2),
                Margin = new Offset(0, 2)
            };

            var secondContainer = new Container(container)
            {
                Bordered = true,
                Margin = new Offset(1, 4)
            };

            var thirdContainer = new Container(secondContainer)
            {
                Bordered = true,
                Margin = new Offset(1, 4)
            };

            thirdContainer.Write(ConsoleContext);
            
            Console.ReadLine();
        }

        private static DefinitionList CreateDefinitionList(object model)
            => new DefinitionList(model);

        private static IEnumerable<int> Primes(int n) => Enumerable.Range(2, int.MaxValue - 1)
            .Where(candidate => Enumerable.Range(2, (int)Math.Sqrt(candidate))
            .All(divisor => candidate % divisor != 0))
            .Take(n);
    }
}
