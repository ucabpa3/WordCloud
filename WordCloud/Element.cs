﻿using System;
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
        public Element(string text, int pX, int pY, int fSize)
        {
            content = text;
            fontSize = fSize;
            X = pX;
            Y = pY;
        }
        #endregion

        #region Members
        string content;
        int fontSize;
        int X;
        int Y;
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

        #endregion
    }
}
