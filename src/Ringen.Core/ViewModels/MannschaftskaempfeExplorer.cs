using System;
using System.Collections.Generic;

namespace Ringen.Core.ViewModels
{
    public static class MannschaftskaempfeExplorer
    {
        private static IExplorerItemViewModel selectedItem;
        public static IExplorerItemViewModel SelectedItem
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

        private static List<IExplorerItemViewModel> data;

        public static List<IExplorerItemViewModel> Data
        {
            get
            {
                if (data == null)
                    data = new List<IExplorerItemViewModel>() { new MannschaftskaempfeViewModel() };


                return data;
            }
            set { data = value; }
        }
        public class SelectedItemChangedEventArgs : EventArgs
        {
            public IExplorerItemViewModel SelectedItem { get; }

            public SelectedItemChangedEventArgs(IExplorerItemViewModel SelectedItem)
            {
                this.SelectedItem = SelectedItem;
            }
        }
    }
}
