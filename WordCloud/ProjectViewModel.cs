using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using MicroMvvm;
using Levenshtein;
using MahApps.Metro.Controls;

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
        EditDistance ed = new EditDistance();
        public List<Element> elements = new List<Element>();
        private TextBlock cached_item = new TextBlock();
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

        private int selectedTabNumber;
        public int SelectedTabNumber
        {
            get
            {
                return selectedTabNumber;
            }
            set
            {
                if (selectedTabNumber == value) return;
                selectedTabNumber = value;

                RaisePropertyChanged("SelectedTabNumber");
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
            SelectedTabNumber = 1;
            Window win = Application.Current.MainWindow;
            Grid CanvasContainer = win.FindName("CanvasContainer") as Grid;


            // ProgressRing pr = win.FindName("WordCloudLoading") as ProgressRing;
            //  pr.IsActive = true;


            Dummy t = new Dummy();
            Cloud c = new Cloud(Convert.ToInt32(CanvasContainer.ActualHeight), Convert.ToInt32(CanvasContainer.ActualWidth));
            c.CreateCloud(t);
            Elements = c.Holder;

            //  pr.IsActive = false;
        }
        bool CanStartWordCloudExecute()
        {
            return true;
        }

        void TextBlockClickExecute(object parameter)
        {
            string clickedItem = parameter.ToString();

            /* Switch tab to Graph */
            SelectedTabNumber = 2;
            //Window win = Application.Current.MainWindow;
            //TabItem tab = win.FindName("GraphTab") as TabItem;
            //tab.IsSelected = true;


        }

        bool CanTextBlockClickExecute()
        {

            return true;
        }

        public ICommand GetPath { get { return new RelayCommand(GetPathExecute, () => true); } }
        public ICommand TextBlockClick { get { return new RelayCommand<object>((param) => this.TextBlockClickExecute(param)); } }
        public ICommand StartWordCloud { get { return new RelayCommand(StartWordCloudExecute, CanStartWordCloudExecute); } }
        #endregion
    }
}
