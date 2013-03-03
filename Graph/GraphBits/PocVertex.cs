using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Graph
{
    /// <summary>
    /// A simple identifiable vertex.
    /// </summary>
    [DebuggerDisplay("{word}-{distance}")]
    public class PocVertex
    {
        public string word { get; private set; }
        public int distance { get; private set; }

        public PocVertex(string word, int distance)
        {
            this.word = word;
            this.distance = distance;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", word, distance);
        }
    }
}
