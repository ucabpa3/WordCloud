using MicroMvvm;
using MicrosoftJava.Shared;
using System;
using System.Collections.Generic;
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

        public void StartWordCloud() {
<<<<<<< HEAD
            var words = API.PublicAPI.ExtractTokens(@"C:\Projects\SortTest.java").ToList();
=======
            var words = API.PublicAPI.ExtractTokens(@"C:\Users\MConstantinides\Desktop\Quine.java").ToList();
>>>>>>> 466453e3e563f1b4f9aaac33f3f77749714c19fb

            var t = from w in words.Where(w => w != null)
                    group w by new { w.Type, w.Name } into g
                    select new Word(g.Key.Type, g.Key.Name, g.Count());
            wordsList = t.ToList();

            Cloud c = new Cloud(Convert.ToInt32(CanvasHeight), Convert.ToInt32(CanvasWidth));
            c.CreateCloud(wordsList);
            Elements = c.Holder;
        }

        #endregion

        #region Commands

        public ICommand TextBlockClick { get { return new RelayCommand<object>((param) => this.TextBlockClickExecute(param)); } }

        void TextBlockClickExecute(object parameter) {
            string clickedItem = parameter.ToString();

            /* Switch tab to Graph */
            parent.SelectedTabNumber = 1;
            parent.GraphTab.RunGraph(clickedItem, wordsList);
        }

        #endregion

    }
}
