using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ScrawlUI
{
    public class LineList : IReadOnlyList<string>
    {
        public int LongestLineLength { get; private set; }

        private List<string> _innerList;

        public LineList(int capacity)
        {
            _innerList = new List<string>(capacity);
        }

        /// <summary>
        /// Adds the provided <paramref name="line"/> to this line list.
        /// </summary>
        /// <param name="line">The line to add.</param>
        public void AddLine(string line)
        {
            LongestLineLength = Math.Max(line.Length, LongestLineLength);

            _innerList.Add(line);
        }

        /// <summary>
        /// Pads all lines in this line list with the number of provided <paramref name="padding"/> characters.
        /// </summary>
        /// <param name="padding">The number of padding characters to add.</param>
        public void LeftPad(int padding)
        {
            _innerList = _innerList.Select(s => s.PadLeft(padding))
                .ToList();   
        }

        /// <summary>
        /// Pads all lines in this line list with the number of provided <paramref name="padding"/> characters.
        /// </summary>
        /// <param name="padding">The number of padding characters to add.</param>
        public void RightPad(int padding)
        {
            _innerList = _innerList.Select(s => s.PadRight(padding))
                .ToList();
        }

        public string this[int index] 
            => _innerList[index];

        public int Count
            => _innerList.Count;

        public IEnumerator<string> GetEnumerator()
            => _innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
