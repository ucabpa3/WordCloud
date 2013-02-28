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
            dummy.Add(new DummyWords("asfg", 26));
            dummy.Add(new DummyWords("dyg", 25));
            dummy.Add(new DummyWords("ljg", 24));
            dummy.Add(new DummyWords("kfg", 23));
            dummy.Add(new DummyWords("mfg", 22));
            dummy.Add(new DummyWords("bfg", 21));
            dummy.Add(new DummyWords("qfg", 20));
            dummy.Add(new DummyWords("dwg", 19));
            dummy.Add(new DummyWords("dig", 18));
            dummy.Add(new DummyWords("djg", 17));
            dummy.Add(new DummyWords("dgg", 16));
            dummy.Add(new DummyWords("ddg", 15));
            dummy.Add(new DummyWords("dfgwwe", 14));
            dummy.Add(new DummyWords("vbn", 13));
            dummy.Add(new DummyWords("lal", 12));
            dummy.Add(new DummyWords("jkk", 11));
            dummy.Add(new DummyWords("ert", 10));
            dummy.Add(new DummyWords("pli", 9));
            dummy.Add(new DummyWords("pqweli", 8));
            dummy.Add(new DummyWords("oyo", 7));
            dummy.Add(new DummyWords("bmn", 6));
            dummy.Add(new DummyWords("bm2", 5));       
        }
        #endregion

        #region Members
        public List<DummyWords> dummy;
        #endregion

        #region Properties
        #endregion
    }
}
