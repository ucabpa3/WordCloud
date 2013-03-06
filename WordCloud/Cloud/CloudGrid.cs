using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WordCloud {
    public class CloudGrid : Grid {

        #region Declarations

        public static readonly DependencyProperty CanvasHeightProperty = DependencyProperty.Register("CanvasHeight", typeof(double), typeof(CloudGrid));
        public static readonly DependencyProperty CanvasWidthProperty = DependencyProperty.Register("CanvasWidth", typeof(double), typeof(CloudGrid));

        #endregion

        #region Initialization

        public CloudGrid() {
            this.SizeChanged += CloudGrid_SizeChanged;
        }

        #endregion

        #region Properties

        public double CanvasHeight {
            get {
                return Convert.ToDouble(this.GetValue(CanvasHeightProperty));
            }
            set {
                this.SetValue(CanvasHeightProperty, value);
            }
        }

        public double CanvasWidth {
            get {
                return Convert.ToDouble(this.GetValue(CanvasWidthProperty));
            }
            set {
                this.SetValue(CanvasWidthProperty, value);
            }
        }

        #endregion

        #region EventHandlers

        void CloudGrid_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (!(sender is Grid)) return;
            CanvasHeight = (sender as Grid).ActualHeight;
            CanvasWidth = (sender as Grid).ActualWidth;
        }

        #endregion

    }
}
