using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WordCloud {
    internal class SizeHelper : DependencyObject {

        public static DependencyProperty SizeHelperEnabledProperty = DependencyProperty.RegisterAttached("SizeHelperEnabled", typeof(bool), typeof(SizeHelper), new UIPropertyMetadata(false, new PropertyChangedCallback(SizeHelperEnabledChanged)));

        public static bool GetSizeHelperEnabled(DependencyObject s) {
            return Convert.ToBoolean(s.GetValue(SizeHelperEnabledProperty));
        }

        public static void SetSizeHelperEnabled(DependencyObject s, bool value) {
            s.SetValue(SizeHelperEnabledProperty, value);
        }

        private static void SizeHelperEnabledChanged(DependencyObject s, DependencyPropertyChangedEventArgs e) {
            new SizeHelper(s as ItemsControl);
        }

        private ItemsControl control;

        public SizeHelper(ItemsControl control) {
            this.control = control;
            this.control.Loaded += control_Loaded;
        }

        void control_Loaded(object sender, RoutedEventArgs e) {
            //ActualHeight = control.ActualHeight;
        }

        public static DependencyProperty ActualHeightProperty = DependencyProperty.RegisterAttached("AcutalHeight", typeof(double), typeof(SizeHelper));

        public static double GetActualHeight(DependencyObject s) {
            return Convert.ToDouble(s.GetValue(ActualHeightProperty));   
        }

        public void SetActualHeight(DependencyObject s, double v) {
            s.SetValue(ActualHeightProperty, v);
        }

    }
}
