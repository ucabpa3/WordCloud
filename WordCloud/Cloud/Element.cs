using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WordCloud
{
    class Element
    {
        #region Construction
        public Element(string text, int pX, int pY, int fSize, double line_height, double word_width)
        {
            content = text;
            fontSize = fSize;
            X = pX;
            Y = pY;
            lineHeight = line_height;
            wordWidth = word_width;
            color = "Black";
        }
        #endregion

        #region Members
        string content;
        int fontSize;
        int X;
        int Y;
        double lineHeight;
        double wordWidth;
        string color;
        #endregion

        #region Properties
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public int PosX
        {
            get { return X; }
            set { X = value; }
        }

        public int PosY
        {
            get { return Y; }
            set { Y = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public double LineHeight
        {
            get { return lineHeight; }
            set { lineHeight = value; }
        }

        public double WordWidth
        {
            get { return wordWidth; }
            set { wordWidth = value; }
        }
        #endregion
    }
}
