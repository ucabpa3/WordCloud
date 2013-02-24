using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloud
{
    class DummyWords
    {
        #region Construction
        public DummyWords(string t, int o)
        {
            text = t;
            occurences = o;
        }
        #endregion

        #region Members
        string text;
        int occurences;
        #endregion

        #region Properties
        #endregion
    }
}
