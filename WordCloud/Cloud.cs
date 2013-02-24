using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WordCloud
{
    class Cloud
    {
        #region Construction
        public Cloud()
        {          
        }
        #endregion

        #region Members
        List<Element> holder;
        //FontFamily fontFamily;
        #endregion

        #region Methods
        public void CreateCloud(Dummy d) {
            //fontFamily = new FontFamily("Comic Sans");
            Random rad = new Random();
            int factor = rad.Next(5, 10);
            holder = new List<Element>();
            for (int i = 0; i < d.dummy.Count; i++)
            {
                int x = rad.Next(50, 650);
                int y = rad.Next(50, 400);
                int fSize = d.dummy[i].Occurences * factor;
                Element el = new Element(d.dummy[i].Text, x, y, fSize);
                holder.Add(el);
            }
        }
        #endregion

        #region Properties
        //<summary>
        // Full path to project
        //</summary>

        public List<Element> Holder
        {
            get { return  holder; }
            set { holder = value; }
        }
        #endregion
    }
}
