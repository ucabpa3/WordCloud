﻿using System;
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
            fontFamily = new FontFamily("Verdana");
            maxFontSize = 62;
            minFontSize = 12;
            CanvasHeight = 800;
            CanvasWidth = 1300;
        }
        public Cloud(int CanvasH, int CanvasW)
        {
            fontFamily = new FontFamily("Verdana");
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
            int top_words = d.Count % font_groups;
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
                    if (d[i].Count != prev_occ && opacity > 0.1) { opacity -= 0.1; if (opacity < 0.1) { opacity = 0.1; } }
                }
                else 
                {
                    if (d[i].Count != prev_occ && fontSize > minFontSize)
                    {
                        color = setColor(rad);
                        fontSize -= font_step;
                    }
                }
               
                double lineHeight = Math.Ceiling(fontSize * fontFamily.LineSpacing + fontFamily.LineSpacing);
                FormattedText dum = new FormattedText(d[i].Name,
                                                System.Globalization.CultureInfo.GetCultureInfo("en-us"),
                                                FlowDirection.LeftToRight,
                                                new Typeface("Verdana"), fontSize, Brushes.Black);

                double wordWidth = dum.Width - 4;
                int x = CanvasWidth / 2;
                if (d.Count > 200) { x = rad.Next(CanvasWidth / 8, CanvasWidth  - CanvasWidth / 8); }
                //else  x = rad.Next(CanvasWidth/4, CanvasWidth - CanvasWidth/4);
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
            double step = 1.557;
            double t = step;
            bool alt = false;
            Random rad = new Random();
            Random d = new Random();
            List<int> prev_x = new List<int>();

            prev_x.Add(x);

            while (DetectCollisions(ref x, ref y, fontHeight, wordWidth)) //|| x<0 || y<0 || x+Convert.ToInt32(wordWidth)>CanvasWidth || y+Convert.ToInt32(fontHeight) > CanvasHeight)
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

               /* if (x < 0 || y < 0 || x + Convert.ToInt32(wordWidth) > CanvasWidth || y + Convert.ToInt32(fontHeight) > CanvasHeight)
                {

                    while (!prev_x.Contains(x))
                    {
                        x = rad.Next(0, CanvasWidth - Convert.ToInt32(wordWidth));
                        y = Convert.ToInt32(CanvasHeight / 2 - fontHeight);
                    }
                }*/
                t += step;
            }

        }
        private bool DetectCollisions(ref int x, ref int y, double fontHeight, double wordWidth)
        {
            for (int i = 0; i < holder.Count; i++)
            {
                if (!( x + wordWidth < holder[i].PosX || y + fontHeight < holder[i].PosY || x > holder[i].PosX + holder[i].WordWidth || y > holder[i].PosY + holder[i].LineHeight ))
                    return true;
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
