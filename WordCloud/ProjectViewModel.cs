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
        #endregion

        #region Commands
        void GetPathExecute()
        {
           // OpenFileDialog dialog = new OpenFileDialog();
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.ShowDialog();
            FullPath = dialog.SelectedPath;
        }
        bool CanGetPathExecute() 
        {
            return true;
        }
        public ICommand GetPath { get { return new RelayCommand(GetPathExecute, CanGetPathExecute); } }
        #endregion
    }
}
