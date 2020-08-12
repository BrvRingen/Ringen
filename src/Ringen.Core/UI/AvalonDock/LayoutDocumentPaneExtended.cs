using AvalonDock.Layout;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;

namespace Ringen.Core.UI.AvalonDock
{
    public class LayoutDocumentPaneExtended : LayoutDocumentPane
    {
        public static DependencyProperty CollectionChangedCommandProperty = DependencyProperty.Register("CollectionChangedCommand", typeof(ICommand), typeof(LayoutDocumentPaneExtended));

        public ICommand CollectionChangedCommand
        {
            get
            {
                return (ICommand)GetValue(CollectionChangedCommandProperty);
            }

            set
            {
                SetValue(CollectionChangedCommandProperty, value);
            }
        }

        public LayoutDocumentPaneExtended() : base()
        {
            Children.CollectionChanged += Children_CollectionChanged;
        }

        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChangedCommand?.Execute(e);
        }
    }

}
