using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using MicrosoftJava.Shared;

namespace WordCloud
{
    class Cloud
    {
        #region Construction
        public Cloud()
        {
            fontFamily = new FontFamily("Square721 BT");
            maxFontSize = 120;
            minFontSize = 8;
            CanvasHeight = 460;
            CanvasWidth = 380;
        }
        public Cloud(int CanvasH, int CanvasW)
        {
            fontFamily = new FontFamily("Square721 BT");
            maxFontSize = 62;
            minFontSize = 12;
            CanvasHeight = CanvasH;
            CanvasWidth = CanvasW;
        }
        public Cloud(string FFamily, int maxFont, int minFont)
        {
            maxFontSize = maxFont;
            minFontSize = minFont;
        }
        #endregion

        #region Members
        
        List<Element> holder;
        FontFamily fontFamily;
        int maxFontSize;
        int minFontSize;
        int CanvasHeight;
        int CanvasWidth;
        
        #endregion

        #region Methods
        public void CreateCloud(List<Word> d)
        {
            holder = new List<Element>();
            Random rad = new Random();
            bool grouped_by_font = true;
            double opacity = 1.0;
            int font_step = 1;
            int prev_occ = 0;
            int fontSize = maxFontSize;          
            int font_groups = maxFontSize - minFontSize;
            int n_words_in_groups = d.Count / font_groups;
            int top_words = d.Count - n_words_in_groups * font_groups;
            string color = setColor(rad);
            

            if (n_words_in_groups < 2)
            {
                top_words = 0;
                font_step = 5;
                grouped_by_font = false;
            }

            
            for (int i = 0; i < d.Count; i++)
            {
                if (grouped_by_font)
                {
                    if (top_words > 0)
                    {
                        top_words--;                  
                        if (top_words == 0) { opacity = 1.0; }
                    }
                    else if ((i % n_words_in_groups) == 0 && top_words <= 0)
                    {
                        color = setColor(rad);
                        fontSize -= font_step;
                        opacity = 1.0;
                    }
                    if (d[i].Count != prev_occ && opacity > 0.1) { opacity -= 0.1; }
                }
                else 
                {
                    if (d[i].Count != prev_occ && fontSize > minFontSize)
                    {
                        color = setColor(rad);
                        fontSize -= font_step;
                    }
                }
               
                double lineHeight = Math.Ceiling(fontSize * fontFamily.LineSpacing + fontFamily.LineSpacing) - 7;
                FormattedText dum = new FormattedText(d[i].Name,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight,
                                                new Typeface("Verdana"), fontSize, Brushes.Black);

                double wordWidth = dum.Width - 7;
                int x = rad.Next(0, CanvasWidth - Convert.ToInt32(wordWidth));
                int y = Convert.ToInt32(CanvasHeight / 2 -  lineHeight);

                ResolveCollisions(ref x, ref y, ref lineHeight, ref wordWidth);
                
                Element el = new Element(d[i].Name, x, y, fontSize, lineHeight, wordWidth, opacity);

                el.Color = color;
                holder.Add(el);
                prev_occ = d[i].Count;

            }
        }

        private string setColor(Random rad) 
        {
            int scR = rad.Next(0, 9);
            int scG = rad.Next(0, 9);
            int scB = rad.Next(0, 9);

            return "sc# 0." + scR.ToString() + ",0." + scG.ToString() + ",0." + scB.ToString();
        }

        private void ResolveCollisions(ref int x, ref int y, ref  double fontHeight, ref double wordWidth)
        {
            double t = 0.01;
            bool alt = false;
            Random rad = new Random();
            Random d = new Random();
            List<int> prev_x = new List<int>();

            prev_x.Add(x);

            while (DetectCollisions(ref x, ref y, fontHeight, wordWidth) || x<0 || y<0 || x+Convert.ToInt32(wordWidth)>CanvasWidth || y+Convert.ToInt32(fontHeight) > CanvasHeight)
            {

                if (alt)
                {
                    x = x + Convert.ToInt32(Math.Ceiling(t * Math.Cos(t)));
                    y = y + Convert.ToInt32(Math.Ceiling(t * Math.Sin(t)));
                    alt = false;
                }
                else
                {
                    x = x - Convert.ToInt32(Math.Ceiling(t * Math.Cos(t)));
                    y = y - Convert.ToInt32(Math.Ceiling(t * Math.Sin(t)));
                    alt = true;
                }


                prev_x.Add(x);

                if (x < 0 || y < 0 || x + Convert.ToInt32(wordWidth) > CanvasWidth || y + Convert.ToInt32(fontHeight) > CanvasHeight)
                {

                    while (!prev_x.Contains(x))
                    {
                        x = rad.Next(0, CanvasWidth - Convert.ToInt32(wordWidth));
                        y = Convert.ToInt32(CanvasHeight / 2 - fontHeight);
                    }
                }
                t = t + 0.01;
            }

        }
        private bool DetectCollisions(ref int x, ref int y, double fontHeight, double wordWidth)
        {

            for (int i = 0; i < holder.Count; i++)
            {
                 
                if (((x >= holder[i].PosX) && (x <= (holder[i].PosX + holder[i].WordWidth))) && ((y >= holder[i].PosY) && (y <= (holder[i].LineHeight + holder[i].PosY))))
                {

                    return true;
                }
                //(x+ww,y)
                if ((((x + wordWidth) >= holder[i].PosX) && ((x + wordWidth) <= (holder[i].PosX + holder[i].WordWidth))) && ((y >= holder[i].PosY) && (y <= (holder[i].LineHeight + holder[i].PosY))))
                {

                    return true;
                }
                //(x,y+lh)
                if (((x >= holder[i].PosX) && (x <= (holder[i].PosX + holder[i].WordWidth))) && (((y + fontHeight) >= holder[i].PosY) && ((y + fontHeight) <= (holder[i].LineHeight + holder[i].PosY))))
                {

                    return true;
                }
                //(x+wW, y+lH)
                if ((((x + wordWidth) >= holder[i].PosX) && ((x + wordWidth) <= (holder[i].PosX + holder[i].WordWidth))) && (((y + fontHeight) >= holder[i].PosY) && ((y + fontHeight) <= (holder[i].LineHeight + holder[i].PosY))))
                {

                    return true;
                }
                //center
                if ((((x + wordWidth / 2) >= holder[i].PosX) && ((x + wordWidth / 2) <= (holder[i].PosX + holder[i].WordWidth))) && (((y + fontHeight / 2) >= holder[i].PosY) && ((y + fontHeight / 2) <= (holder[i].LineHeight + holder[i].PosY))))
                {

                    return true;
                }
                //Case: current word inserting bigger than the others
                if (((x <= holder[i].PosX) && (x + wordWidth >= holder[i].PosX)) && ((y <= holder[i].PosY) && ((y + fontHeight) >= holder[i].PosY)))
                {
                    return true;
                }
                //(x+ww,y)
                if (((x <= holder[i].PosX + holder[i].WordWidth) && ((x + wordWidth) >= (holder[i].PosX + holder[i].WordWidth))) && ((y <= holder[i].PosY) && (y + fontHeight >= holder[i].PosY)))
                {

                    return true;
                }
                //(x,y+lh)
                if (((x <= holder[i].PosX) && (x + wordWidth >= holder[i].PosX)) && ((y <= holder[i].PosY + holder[i].LineHeight) && ((y + fontHeight) >= (holder[i].LineHeight + holder[i].PosY))))
                {
                    return true;
                }
                //(x+wW, y+lH)
                if (((x <= holder[i].PosX + holder[i].WordWidth) && ((x + wordWidth) >= (holder[i].PosX + holder[i].WordWidth))) && ((y <= holder[i].PosY + holder[i].LineHeight) && ((y + fontHeight) >= (holder[i].LineHeight + holder[i].PosY))))
                {
                    return true;
                }
                if (((x <= holder[i].PosX + holder[i].WordWidth / 2) && (x + wordWidth >= (holder[i].PosX + holder[i].WordWidth / 2))) && ((y <= holder[i].PosY + holder[i].LineHeight / 2) && ((y + fontHeight) >= (holder[i].LineHeight / 2 + holder[i].PosY))))
                {

                    return true;
                }

            }

            return false;
        }

        #endregion

        #region Properties
        //<summary>
        // Set of Elements
        //</summary>

        public List<Element> Holder
        {
            get { return holder; }
            set { holder = value; }
        }

        #endregion
    }
}
