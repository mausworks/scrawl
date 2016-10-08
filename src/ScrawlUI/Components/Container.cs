using System;
using ScrawlCore;

namespace ScrawlUI.Components
{
    public class Container : UIComponent
    {
        public bool Bordered { get; set; }

        public Offset Padding { get; set; } = Offset.None;

        public Offset Margin { get; set; } = Offset.None;

        public UIComponent InnerComponent { get; }

        public Container(UIComponent innerComponent)
        {
            if (innerComponent == null)
            {
                throw new ArgumentNullException(nameof(innerComponent));
            }

            InnerComponent = innerComponent;
        }
        
        private void WriteTopCap(ObjectWriteContext context, int longestLineLength)
        {
            for (var i = 0; i < Margin.Top; i++)
            {
                context.NewLine();
            }

            WriteHorizontalBorder(context, longestLineLength);
            
            for (var i = 0; i < Padding.Top; i++)
            {
                context.NewLine();
                WriteVerticalBorder(context, Margin.Left, Padding.Left);
                context.Write(new string(' ', longestLineLength));
                WriteVerticalBorder(context, Padding.Right, Margin.Right);
            }
        }

        private void WriteBottomCap(ObjectWriteContext context, int contentLength)
        {
            for (var i = 0; i < Padding.Bottom; i++)
            {
                WriteVerticalBorder(context, Margin.Left, Padding.Left);
                context.Write(new string(' ', contentLength));
                WriteVerticalBorder(context, Padding.Right, Margin.Right);

                context.NewLine();
            }

            WriteHorizontalBorder(context, contentLength);

            for (var i = 0; i < Margin.Bottom; i++)
            {
                context.NewLine();
            }
        }

        private void WriteHorizontalBorder(ObjectWriteContext context, int contentLength)
        {
            if (Bordered)
            {
                if (Margin.Left > 0)
                {
                    context.Write(new string(' ', Margin.Left));
                }

                context.Write("+");
                context.Write(new string('-', contentLength + Padding.Left + Padding.Right));
                context.Write("+");

                if (Margin.Right > 0)
                {
                    context.Write(new string(' ', Margin.Right));
                }
            }
        }
        
        public void WriteVerticalBorder(ObjectWriteContext context, int paddingLeft, int paddingRight)
        {
            if (paddingLeft > 0)
            {
                context.Write(new string(' ', paddingLeft));
            }

            if (Bordered)
            {
                context.Write("|");
            }

            if (paddingRight > 0)
            {
                context.Write(new string(' ', paddingRight));
            }
        }

        public void Write(ObjectWriteContext context)
        {
            var proxy = new ProxyScrawler();
            var proxiedContext = context.CreateSubContext(proxy);
            
            var isTerminated = false;
            var isLineTerminator = false;

            var terminatorChars = proxiedContext.LineTerminator.ToCharArray();

            var lineList = new LineList(32);
            string currentLine = string.Empty;

            // Setup on write method for the proxy.
            proxy.OnWrite(s =>
            {
                var len = s.Length;

                if (len == 0)
                {
                    return;
                }

                isLineTerminator = s.Equals(proxiedContext.LineTerminator, StringComparison.Ordinal);
                
                if (isLineTerminator)
                {
                    lineList.AddLine(currentLine);
                    currentLine = string.Empty;

                    return;
                }

                isTerminated = isLineTerminator || s.EndsWith(proxiedContext.LineTerminator);

                if (isTerminated)
                {
                    lineList.AddLine(currentLine.TrimEnd(terminatorChars));
                    currentLine = string.Empty;

                    return;
                }

                currentLine += s;
            });

            // Write to the proxied context.
            InnerComponent.Write(proxiedContext);

            // Make all lines into a "block".
            // | abc abc abc |
            // | abc abc     |
            // | abc ab abc  |
            lineList.RightPad(lineList.LongestLineLength);

            // Write top border
            WriteTopCap(context, lineList.LongestLineLength);

            context.NewLine();

            foreach (var line in lineList)
            {
                WriteVerticalBorder(context, Margin.Left, Padding.Left);

                context.Write(line);

                WriteVerticalBorder(context, Padding.Right, Margin.Right);

                context.NewLine();
            }

            WriteBottomCap(context, lineList.LongestLineLength);

            context.NewLine();
        }
    }
}
