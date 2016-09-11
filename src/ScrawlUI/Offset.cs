namespace ScrawlUI
{
    public struct Offset
    {
        /// <summary>
        /// A zero offset.
        /// </summary>
        public static Offset None { get; } = new Offset(0);

        /// <summary>
        /// The number of lines from the top to offset with.
        /// </summary>
        public int Top;

        /// <summary>
        /// The number of characters to the right to offset with.
        /// </summary>
        public int Right;

        /// <summary>
        /// The number of lines from the bottom to offset with.
        /// </summary>
        public int Bottom;

        /// <summary>
        /// The number of characters to the left to offset with.
        /// </summary>
        public int Left;

        public Offset(int all)
            : this(all, all)
        {

        }

        public Offset(int y, int x)
            : this(y, x, y, x)
        {
        }

        public Offset(int top, int right, int bottom, int left)
            : this()
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
}
