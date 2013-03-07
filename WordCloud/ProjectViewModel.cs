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

namespace WordCloud {

    class ProjectViewModel : ObservableObject {

        #region Declarations

        private ObservableCollection<ObservableObject> tabs;
        private int selectedTabNumber;

        #endregion

        #region Construction

        ///<summary>
        ///Constructs default instance of ProjectViewModel
        ///</summary>
        public ProjectViewModel() {

        }

        #endregion

        #region Properties

        public ObservableCollection<ObservableObject> Tabs {
            get {
                if (this.tabs == null) {
                    this.tabs = new ObservableCollection<ObservableObject>();
                    this.tabs.Add(new WelcomeTabViewModel(this));
                    this.tabs.Add(new CloudViewModel(this));
                    this.tabs.Add(new GraphViewModel(this));
                }

                return this.tabs;
            }
        }

        public CloudViewModel CloudTab {
            get {
                return Tabs[1] as CloudViewModel;
            }
        }

        public GraphViewModel GraphTab {
            get {
                return Tabs[2] as GraphViewModel;
            }
        }

        public int SelectedTabNumber {
            get {
                return selectedTabNumber;
            }
            set {
                if (selectedTabNumber == value)
                    return;
                selectedTabNumber = value;

                RaisePropertyChanged("SelectedTabNumber");
            }
        }

        #endregion

    }
}
