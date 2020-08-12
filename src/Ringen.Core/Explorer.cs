using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core
{
    public static class Explorer
    {
        private static IExplorerItem selectedItem;
        public static IExplorerItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                SelectedItemChanged?.Invoke(null, new SelectedItemChangedEventArgs(selectedItem));
            }
        }
        public static event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        private static List<IExplorerItem> data;

        public static List<IExplorerItem> Data
        {
            get
            {
                if (data == null)
                    data = new List<IExplorerItem>() { new Core.CS.CS() };


                return data;
            }
            set { data = value; }
        }
        public class SelectedItemChangedEventArgs : EventArgs
        {
            public IExplorerItem SelectedItem { get; }

            public SelectedItemChangedEventArgs(IExplorerItem SelectedItem)
            {
                this.SelectedItem = SelectedItem;
            }
        }
    }
}
