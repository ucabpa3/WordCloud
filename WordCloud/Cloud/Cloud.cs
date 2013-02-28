using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

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
            maxFontSize = 61;
            minFontSize = 55;
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
        public void CreateCloud(Dummy d)
        {
            holder = new List<Element>();
            Random rad = new Random();
            int first = d.dummy[0].Occurences;
            int last = d.dummy[d.dummy.Count - 1].Occurences;
            int diff = first - last;
            int groups = maxFontSize - minFontSize;
            int step = d.dummy.Count / groups;
            int top_words = d.dummy.Count - step * groups;

            int fSize = maxFontSize;
            for (int i = 0; i < d.dummy.Count; i++)
            {
                
                if (i % step == 0 && i!=0)
                {
                    fSize = fSize - 1;
                }
                double lineHeight = Math.Ceiling(fSize * fontFamily.LineSpacing + fontFamily.LineSpacing);
                FormattedText dum = new FormattedText(d.dummy[i].Text,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight,
                                                new Typeface("Square721 BT"), fSize, Brushes.Black);
                double wordWidth = dum.Width + 10;
                int x = rad.Next(0, CanvasWidth - Convert.ToInt32(wordWidth));
                int y = Convert.ToInt32(CanvasHeight / 2 -  lineHeight);

                ResolveCollisions(ref x, ref y, ref lineHeight, ref wordWidth);
                Element el = new Element(d.dummy[i].Text, x, y, fSize, lineHeight, wordWidth);
                int scR = rad.Next(0, 9);
                int scG = rad.Next(0, 9);
                int scB = rad.Next(0, 9);
                el.Color = "sc# 0." + scR.ToString() + ",0." + scG.ToString() + ",0." + scB.ToString();
                holder.Add(el);


            }
        }


        private void ResolveCollisions(ref int x, ref int y, ref  double fontHeight, ref double wordWidth)
        {
            double t = 0.1;
            bool alt = false;
            Random rad = new Random();
            Random d = new Random();
            List<int> prev_x = new List<int>();
            prev_x.Add(x);
            int step = 0;

            while (DetectCollisions(ref x, ref y, fontHeight, wordWidth) || x<0 || y<0 || x+Convert.ToInt32(wordWidth)>CanvasWidth || y+Convert.ToInt32(fontHeight) > CanvasHeight)
            {
                step++;
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
                t = t + 0.1;
            }

        }
        private bool DetectCollisions(ref int x, ref int y, double fontHeight, double wordWidth)
        {

            for (int i = 0; i < holder.Count; i++)
            {
                /*    //Case: current word inserting smaller than the others
                    //left,right
                    for (int x1 = x; x1 <= Math.Ceiling(x + wordWidth); x1 += Convert.ToInt32(Math.Ceiling(wordWidth)))
                    {
                        for (int y1 = y; y1 <= Math.Ceiling(y + fontHeight); y1++)
                        {
                            if (((x1 >= holder[i].PosX) && (x1 <= (holder[i].PosX + holder[i].WordWidth))) && ((y1 >= holder[i].PosY) && (y1 <= (holder[i].LineHeight + holder[i].PosY))))
                            {

                                return true;
                            }
                        }
                    }
                    //upper, bottom
                    for (int x1 = x; x1 <= Math.Ceiling(x + wordWidth); x1++)
                    {
                        for (int y1 = y; y1 <= Math.Ceiling(y + fontHeight); y1 += Convert.ToInt32(Math.Ceiling(fontHeight)))
                        {
                            if (((x1 >= holder[i].PosX) && (x1 <= (holder[i].PosX + holder[i].WordWidth))) && ((y1 >= holder[i].PosY) && (y1 <= (holder[i].LineHeight + holder[i].PosY))))
                            {

                                return true;
                            }
                        }
                    }
                    //Case word inserting smaller than in the holder
                    //Left, right
                    for (int x1 = holder[i].PosX; x1 <= Math.Ceiling(holder[i].PosX + holder[i].WordWidth); x1 += Convert.ToInt32(Math.Ceiling(holder[i].WordWidth)))
                    {
                        for (int y1 = holder[i].PosY; y1 <= Math.Ceiling(holder[i].PosY + holder[i].LineHeight); y1++)
                        {
                            if (((x1 >= x) && (x1 <= (x + wordWidth))) && ((y1 >= y) && (y1 <= (y + fontHeight))))
                            {

                                return true;
                            }
                        }
                    }
                    //Upper,bottom
                    for (int x1 = holder[i].PosX; x1 <= Math.Ceiling(holder[i].PosX + holder[i].WordWidth); x1++)
                    {
                        for (int y1 = holder[i].PosY; y1 <= Math.Ceiling(holder[i].PosY + holder[i].LineHeight); y1 += Convert.ToInt32(Math.Ceiling(holder[i].LineHeight)))
                        {
                            if (((x1 >= x) && (x1 <= (x + wordWidth))) && ((y1 >= y) && (y1 <= (y + fontHeight))))
                            {

                                return true;
                            }
                        }
                    }

                    }*/
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
