using MicroMvvm;
using MicrosoftJava.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WordCloud {
    internal class CloudViewModel : ObservableObject {
        #region Declarations

        private List<Element> elements = new List<Element>();
        private TextBlock cached_item = new TextBlock();
        private ProjectViewModel parent;
        private List<Word> wordsList;

        private Dictionary<TLanguageType, List<string>> extensions = new Dictionary<TLanguageType, List<string>>() {
            { TLanguageType.Java, new List<String>() { ".java" } },
            { TLanguageType.CSharp, new List<String>() { ".cs" } },
            { TLanguageType.C, new List<String>() { ".c", ".h" } },
            { TLanguageType.CPlusPlus, new List<String>() { ".cpp", ".h" } },
        };
        
        #endregion
        
        #region Initialization

        public CloudViewModel(ProjectViewModel parent) {
            this.parent = parent;
        }

        #endregion

        #region Properties

        public string Title {
            get {
                return "Cloud";
            }
        }

        public List<Element> Elements {
            get {
                return elements;
            }
            set {
                elements = value;
                RaisePropertyChanged("Elements");
                System.Diagnostics.Debug.WriteLine(" ");
                for (int i = 0; i < elements.Count; i++) {
                    System.Diagnostics.Debug.WriteLine(elements[i].Content + " " + elements[i].PosX + " " + elements[i].PosY + " " + elements[i].LineHeight + " " + elements[i].WordWidth);
                }

            }
        }

        public double CanvasHeight { get; set; }
        public double CanvasWidth { get; set; }

        #endregion

        #region Public Methods

        public void StartWordCloud(string directoryPath, TokenType wordType, TLanguageType lang) {
            
            List<Word> words = new List<Word>();
            try {
                foreach (string fileName in GetFiles(directoryPath, extensions[lang]))
                    words.AddRange(API.PublicAPI.ExtractTokens(new System.IO.StreamReader(fileName), lang));
            } catch {}

            var wl = from w in words.Where(w => w != null)
                           group w by new { w.Type, w.Name } into g
                           select new Word(g.Key.Type, g.Key.Name, g.Count());

            wordsList = wl.Where(w => w.Type == wordType).OrderByDescending(w => w.Count).ToList();

            if (wordsList.Count == 0) return;

            Cloud c = new Cloud();
            //Cloud c = new Cloud(Convert.ToInt32(CanvasHeight), Convert.ToInt32(CanvasWidth));
            c.CreateCloud(wordsList);
            Elements = c.Holder;
        }

        private List<string> GetFiles(string directoryPath, List<string> ext) {
            DirectoryInfo d = new DirectoryInfo(directoryPath);
            List<string> retVal = new List<string>();
            foreach(DirectoryInfo subD in d.GetDirectories())
                retVal.AddRange(GetFiles(subD.FullName, ext));
            foreach(FileInfo file in d.GetFiles()) {
                if (ext.Contains(file.Extension))
                    retVal.Add(file.FullName);
            }
            return retVal;
        }

        #endregion

        #region Commands

        public ICommand TextBlockClick { get { return new RelayCommand<object>((param) => this.TextBlockClickExecute(param)); } }

        void TextBlockClickExecute(object parameter) {
            string clickedItem = parameter.ToString();

            /* Switch tab to Graph */
            parent.SelectedTabNumber = 2;
            parent.GraphTab.RunGraph(clickedItem, wordsList);
        }

        #endregion

    }
}
