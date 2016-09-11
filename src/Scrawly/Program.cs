using System;
using ScrawlCore;
using ScrawlUI.Stringification;
using ScrawlUI.Components;
using System.Linq;
using System.Collections.Generic;
using ScrawlCore.Stringification;

namespace Scrawly
{
    public class Program
    {
        public static WriteContext ConsoleContext = new WriteContext(new ConsoleScrawler(), new CompositeStringifier(
                new BooleanStringifier("yes", "no"),
                new StructStringifier(),
                new EnumerableConcatenator(),

                // Add a AnyTypeStringifer at the end to handle unknown types.
                new TypeNameStringifier()));

        public static void Main(string[] args)
        {
            var now = DateTime.Now;

            ConsoleContext.WriteDefinitionList(new
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
            });

            Console.ReadLine();
        }

        private static IEnumerable<int> Primes(int n) => Enumerable.Range(2, int.MaxValue - 1)
            .Where(candidate => Enumerable.Range(2, (int)Math.Sqrt(candidate))
            .All(divisor => candidate % divisor != 0))
            .Take(n);
    }
}
