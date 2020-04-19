using System.Windows;
using System.Windows.Controls;

namespace Ringen
{
    public class HintTextBox : TextBox
    {
        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(HintTextBox), new PropertyMetadata("Hint"));

        static HintTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HintTextBox), new FrameworkPropertyMetadata(typeof(HintTextBox)));
        }

        public HintTextBox()
        {

        }

        public string Hint
        {
            get
            {
                return (string)GetValue(HintProperty);
            }
            set
            {
                SetValue(HintProperty, value);
            }
        }
    }
}
