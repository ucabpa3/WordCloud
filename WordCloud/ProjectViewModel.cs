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
using MahApps.Metro.Controls;
using MicrosoftJava.Shared;

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
        private ObservableCollection<ObservableObject> tabs;
        
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

        public ObservableCollection<ObservableObject> Tabs {
            get {
                if (this.tabs == null) {
                    this.tabs = new ObservableCollection<ObservableObject>();
                    this.tabs.Add(new CloudViewModel(this));
                    this.tabs.Add(new GraphViewModel(this));
                }

                return this.tabs;
            }
        }

        public CloudViewModel CloudTab {
            get {
                return Tabs[0] as CloudViewModel;
            }
        }

        public GraphViewModel GraphTab {
            get {
                return Tabs[1] as GraphViewModel;
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
            SelectedTabNumber = 0;
            (Tabs[0] as CloudViewModel).StartWordCloud();

            //  pr.IsActive = false;
        }
        
        bool CanStartWordCloudExecute()
        {
            return true;
        }

        public ICommand GetPath { get { return new RelayCommand(GetPathExecute, () => true); } }
        public ICommand StartWordCloud { get { return new RelayCommand(StartWordCloudExecute, CanStartWordCloudExecute); } }

        #endregion
    }
}
