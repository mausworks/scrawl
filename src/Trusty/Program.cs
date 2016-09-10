using System;
using TrustCore;
using TrustUI.Stringification;
using TrustUI.Components;
using System.Linq;
using System.Collections.Generic;

namespace Trustyy
{
    public class Program
    {
        public static WriteContext ConsoleContext = new ConsoleContext(new CompositeStringifier(
                new BooleanStringifier("yes", "no"),
                new StructStringifier(),
                new EnumerableConcatenator()));

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
