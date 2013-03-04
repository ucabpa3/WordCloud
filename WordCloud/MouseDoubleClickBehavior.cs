using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using MicroMvvm;

namespace WordCloud
{
    internal class MouseDoubleClickBehavior : Behavior<TextBlock>
    {
        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(RelayCommand), typeof(MouseDoubleClickBehavior));

        public RelayCommand Command
        {
            get
            {
                return this.GetValue(CommandProperty) as RelayCommand;
            }
            set
            {
                this.SetValue(CommandProperty, value as RelayCommand);
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            TextBlock t = this.AssociatedObject;

            t.MouseDown += t_MouseDown;
        }

        void t_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Command.Execute(null);
        }
    }
}
