using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using MicroMvvm;
using Levenshtein;

namespace WordCloud
{
    class ProjectViewModel : ObservableObject
    {
        #region Construction
        ///<summary>
        ///Constructs default instance of ProjectViewModel
        ///</summary>
        public ProjectViewModel()
        {

        }
        #endregion

        #region Members
        Project _project = new Project();
        ToolTip graphToolTip = null;
        EditDistance ed = new EditDistance();
        public List<Element> elements = new List<Element>();
        #endregion

        #region Properties
        public Project Path
        {
            get { return _project; }
            set { _project = value; }
        }

        public string FullPath
        {
            get { return Path.FullPath; }
            set
            {
                if (Path.FullPath != value)
                {
                    Path.FullPath = value;
                    RaisePropertyChanged("FullPath");
                }
            }
        }

        public List<Element> Elements
        {
            get
            {
                return elements;
            }
            set
            {
                elements = value;
                RaisePropertyChanged("Elements");
                System.Diagnostics.Debug.WriteLine(" ");
                for (int i = 0; i < elements.Count; i++)
                {
                    System.Diagnostics.Debug.WriteLine(elements[i].Content + " " + elements[i].PosX + " " + elements[i].PosY + " " + elements[i].LineHeight + " " + elements[i].WordWidth);
                }

            }
        }

        #endregion

        #region Commands
        void GetPathExecute()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.ShowDialog();
            FullPath = dialog.SelectedPath;
        }
        bool CanGetPathExecute()
        {
            return true;
        }

        void StartWordCloudExecute()
        {

            Window win = Application.Current.MainWindow;
            Grid CanvasContainer = win.FindName("CanvasContainer") as Grid;
            Dummy t = new Dummy();
            Cloud c = new Cloud(Convert.ToInt32(CanvasContainer.ActualHeight), Convert.ToInt32(CanvasContainer.ActualWidth));
            c.CreateCloud(t);
            Elements = c.Holder;
        }
        bool CanStartWordCloudExecute()
        {
            return true;
        }

        void TextBlockClickExecute(object parameter)
        {
            TextBlock clickedItem = parameter as TextBlock;
            UIElement container = Application.Current.MainWindow.FindName("CanvasContainer") as UIElement;
            Point gt = clickedItem.TranslatePoint(new Point(0, 0), container);
            double Y = gt.Y;
            double X = gt.X;


            Dummy dummies = new Dummy();
            List<string> words = new List<string>();

            foreach (DummyWords dw in dummies.dummy)
            {
                words.Add(dw.Text);
            }


            //Dictionary<string, int> distances = ed.GetShortestLevenshtein(clickedItem.Text, words);

            StartGraphExecute(clickedItem, ed.GetShortestLevenshtein(clickedItem.Text, words));

            // initToolTip(clickedItem, content);


            //System.Windows.Forms.MessageBox.Show(clickedItem.FontSize + " " + clickedItem.Text + " " + X.ToString() + "-" + (clickedItem.ActualWidth + X).ToString() + " , " + Y.ToString() + "-" + (Y + clickedItem.ActualWidth).ToString());
            //System.Windows.Forms.MessageBox.Show(clickedItem.Opacity.ToString());
            //System.Windows.Forms.MessageBox.Show(clickedItem.FontSize.ToString());
        }



        void txt_MouseClick(object sender, MouseButtonEventArgs e)
        {

                MessageBox.Show("click");

        }

        void StartGraphExecute(object clickedItem, Dictionary<string, int> distances)
        {
            TextBlock txtClicked = clickedItem as TextBlock;
            Grid graph = new Grid();
            graph.Name = txtClicked.Text + "Graph";

            foreach (KeyValuePair<string, int> item in distances)
            {
                TextBlock txt = new TextBlock();
               // txt.MouseDown += new MouseButtonEventHandler(txt_MouseClick);
                txt.Text = item.Key + " " + item.Value;
                graph.Children.Add(txt);
            }



            graphToolTip = new ToolTip();

            graphToolTip.Content = graph;
            graphToolTip.IsOpen = true;
            graphToolTip.Width = 300;
            graphToolTip.Height = 300;
            txtClicked.ToolTip = graphToolTip;


        }

        void initToolTip(object clickedItem, string content)
        {

            TextBlock txtBlock = clickedItem as TextBlock;

            graphToolTip = new ToolTip();

            graphToolTip.Content = content;
            graphToolTip.IsOpen = true;
            graphToolTip.Width = 300;
            graphToolTip.Height = 300;
            txtBlock.ToolTip = graphToolTip;

        }

        bool CanTextBlockClickExecute()
        {
            return true;
        }

        public ICommand GetPath { get { return new RelayCommand(GetPathExecute, CanGetPathExecute); } }
        public ICommand TextBlockClick { get { return new RelayCommand<object>((param) => this.TextBlockClickExecute(param)); } }
        public ICommand StartWordCloud { get { return new RelayCommand(StartWordCloudExecute, CanStartWordCloudExecute); } }
        #endregion
    }
}
