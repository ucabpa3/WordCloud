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
            int font_groups = maxFontSize - minFontSize;
            int step = d.dummy.Count / font_groups;
            int top_words = d.dummy.Count - step * font_groups;
            int op_groups = step / 3;
            double op_step;
            int op_groups_top = step - op_groups * 3;
            int word_counter = step - op_groups_top;
            int fSize = maxFontSize;
            double opacity = 1.0;
            int tw_op_groups = 0;
            int top_tw_op = 0;
            int inner_step = word_counter;
            string color = setColor(rad);
            int font_step = 1;

            if (step <= 0)
            {
                font_groups = d.dummy.Count;
                font_step = (maxFontSize - minFontSize) / font_groups;
                op_step = 0;
            }
            else
            {
                if (op_groups < 10)
                {
                    string temp = "0." + (10 / word_counter).ToString();
                    op_step = Convert.ToDouble(temp);
                }
                else
                {
                    op_step = 0.1;
                }

                if (top_words > 2)
                {
                    tw_op_groups = top_words / 2;
                    top_tw_op = top_words - tw_op_groups * 2;
                }
            }
            for (int i = 0; i < d.dummy.Count; i++)
            {

                if (top_words > 0)
                {

                    if (tw_op_groups != 0)
                    {
                        if (top_tw_op > 0)
                        {
                            top_tw_op--;
                        }
                        else if ((top_words % 2 != 0 && top_words > 0) || tw_op_groups == 0)
                        {

                            opacity -= op_step;
                        }
                    }
                    else
                    {
                        opacity -= op_step;
                    }

                    top_words--;
                    fSize -= font_step;

                }
                else if ((i - (d.dummy.Count - step * font_groups)) % step == 0)
                {
                    color = setColor(rad);
                    fSize -= font_step;
                    if (word_counter > 0)
                    {
                        word_counter = step - op_groups_top;
                        opacity = 1.0;
                    }
                    opacity = 1.0;
                }
                else
                {
                    if (op_groups_top != 0)
                    {
                        op_groups_top--;
                    }
                    else if (word_counter > 0)
                    {
                        word_counter--;
                        if (word_counter == 0)
                        {
                            opacity -= op_step;
                        }
                    }
                }
                if (step <= 0) { color = setColor(rad); }

                double lineHeight = Math.Ceiling(fSize * fontFamily.LineSpacing + fontFamily.LineSpacing);
                FormattedText dum = new FormattedText(d.dummy[i].Text,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight,
                                                new Typeface("Verdana"), fSize, Brushes.Black);

                double wordWidth = dum.Width + 10;
                int x = rad.Next(0, CanvasWidth - Convert.ToInt32(wordWidth));
                int y = Convert.ToInt32(CanvasHeight / 2 -  lineHeight) + rad.Next(-50,50);

                ResolveCollisions(ref x, ref y, ref lineHeight, ref wordWidth);
                
                Element el = new Element(d.dummy[i].Text, x, y, fSize, lineHeight, wordWidth, opacity);

                el.Color = color;
                holder.Add(el);


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
            double t = 0.1;
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
                t = t + 0.1;
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
