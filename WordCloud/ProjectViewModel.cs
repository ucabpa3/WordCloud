using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MicroMvvm;

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
            get { return elements; }
            set
            {
                elements = value;
                RaisePropertyChanged("Elements");
            }
        }
        #endregion

        #region Commands
        void GetPathExecute()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.ShowDialog(); ;
            FullPath = dialog.SelectedPath;
        }
        bool CanGetPathExecute() 
        {
            return true;
        }

        void StartWordCloudExecute()
        {
            Dummy t = new Dummy();
            Cloud c = new Cloud();
            c.CreateCloud(t);
            Elements = c.Holder;
            //System.Windows.Forms.MessageBox.Show("My message here");
        }
        bool CanStartWordCloudExecute()
        {
            return true;
        }

        public ICommand GetPath { get { return new RelayCommand(GetPathExecute, CanGetPathExecute); } }
        public ICommand StartWordCloud { get { return new RelayCommand(StartWordCloudExecute, CanStartWordCloudExecute); } }
        #endregion
    }
}
