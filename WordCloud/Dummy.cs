using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloud
{
    class Dummy
    {
        #region Construction
        public Dummy() {
            dummy = new List<DummyWords>();
            dummy.Add(new DummyWords("lal", 12));
            dummy.Add(new DummyWords("ert", 11));
            dummy.Add(new DummyWords("dfg", 10));
            dummy.Add(new DummyWords("vbn", 9));
            dummy.Add(new DummyWords("jkk", 8));
            dummy.Add(new DummyWords("pli", 7));
            dummy.Add(new DummyWords("oyo", 6));
            dummy.Add(new DummyWords("bmn", 5));           
        }
        #endregion

        #region Members
        List<DummyWords> dummy;
        #endregion

        #region Properties

        #endregion
    }
}
