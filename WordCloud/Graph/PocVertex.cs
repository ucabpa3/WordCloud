using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace WordCloud
{
    /// <summary>
    /// A simple identifiable vertex.
    /// </summary>
    [DebuggerDisplay("{word}-{distance}")]
    public class PocVertex
    {
        public int ID { get; private set; }
        public string word { get; private set; }
        public int distance { get; private set; }

        public PocVertex(int ID, string word, int distance)
        {
            this.ID = ID;
            this.word = word;
            this.distance = distance;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", ID, word, distance);
        }
    }
}
