using System.Collections.Specialized;
using System.Windows;

namespace Ringen.Core.DataGrid
{
        public class ExtDataGrid : System.Windows.Controls.DataGrid
    {
        public static readonly DependencyProperty ScrollToLastProperty = DependencyProperty.RegisterAttached("ScrollToLast", typeof(bool), typeof(ExtDataGrid), new PropertyMetadata());


        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (GetScrollToLast(this) && Items.Count > 2)
                this.ScrollIntoView(Items[Items.Count - 1]);
        }

        public static bool GetScrollToLast(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollToLastProperty);
        }

        public static void SetScrollToLast(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollToLastProperty, value);
        }
    }
}
