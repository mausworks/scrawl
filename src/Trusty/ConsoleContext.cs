using System;
using TrustCore;
using TrustCore.Stringification;

namespace Trustyy
{
    public class ConsoleContext : WriteContext
    {
        public ConsoleContext(Stringifier stringifier)
            : base(stringifier, new TypeNameStringifier())
        {

        }

        public override void Write(string value)
            => Console.Write(value);
    }
}
