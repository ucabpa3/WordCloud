using MicroMvvm;
using MicrosoftJava.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordCloud {
    internal class WelcomeTabViewModel : ObservableObject {
        #region Declarations

        private string sourcePath; // = @"C:\Users\vasil.vasilev\Dropbox\UCL\Advanced Analysis and Design A\Project\klee-build-env\klee-uclibc-0.02-x64\test\regex";
        private TokenType selectedWordType;
        private TLanguageType selectedLanguageType; // = TLanguageType.C;

        private ProjectViewModel parent;
        private RelayCommand browseSourcePathCommand;
        private RelayCommand generateCloudCommand;

        #endregion

        #region Initialization

        public WelcomeTabViewModel(ProjectViewModel parent) {
            this.parent = parent;
        }

        #endregion

        #region Properties

        public string Title {
            get {
                return "Welcome";
            }
        }

        public string SourcePath {
            get {
                return sourcePath;
            }
            set {
                if (sourcePath != value) {
                    sourcePath = value;
                    RaisePropertyChanged("SourcePath");
                }
            }
        }

        public int SelectedWordType {
            get {
                return (int)this.selectedWordType;
            }
            set {
                if (this.selectedWordType == (TokenType)value)
                    return;
                this.selectedWordType = (TokenType)value;

                RaisePropertyChanged("SelectedWordType");
            }
        }

        public int SelectedLanguageType {
            get {
                return (int)selectedLanguageType;
            }
            set {
                if (this.selectedLanguageType == (TLanguageType)value)
                    return;
                selectedLanguageType = (TLanguageType)value;
                RaisePropertyChanged("SelectedLanguageType");
            }
        }


        #endregion

        #region Commands

        public RelayCommand BrowseSourcePathCommand {
            get {
                if (this.browseSourcePathCommand == null)
                    this.browseSourcePathCommand = new RelayCommand(this.BrowseSourcePath);
                return this.browseSourcePathCommand;
            }
        }

        private void BrowseSourcePath() {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;
            dialog.ShowDialog();
            SourcePath = dialog.SelectedPath;
        }

        public RelayCommand GenerateCloudCommand {
            get {
                if (this.generateCloudCommand == null)
                    this.generateCloudCommand = new RelayCommand(this.GenerateCloud);
                return this.generateCloudCommand;
            }
        }

        private void GenerateCloud() {
            parent.SelectedTabNumber = 1;
            Mouse.OverrideCursor = Cursors.Wait;
            parent.CloudTab.StartWordCloud(SourcePath, selectedWordType, selectedLanguageType);
        }

        #endregion
    }
}
