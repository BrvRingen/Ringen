﻿using System.Windows;
using System.Windows.Controls;
using Ringen.Core.DependencyInjection;
using Ringen.Core.ViewModels;

namespace Ringen.View
{
    /// <summary>
    /// Interaktionslogik für HomeWindow.xaml
    /// </summary>
    public partial class ExplorerWindow : UserControl
    {
        private ExplorerStates _explorerStates;
        public ExplorerWindow()
        {
            _explorerStates = DependencyInjectionContainer.GetService<ExplorerStates>();

            InitializeComponent();
            MannschaftskaempfeExplorer.SelectedItemChanged += ((object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) => { });
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Update_ExplorerState(e.NewValue as IExplorerItemViewModel);
        }

        private void TreeViewItem_OnExpanded(object sender, RoutedEventArgs e)
        {
            Update_ExplorerState(((System.Windows.FrameworkElement)e.OriginalSource).DataContext as IExplorerItemViewModel);
        }

        private void Update_ExplorerState(IExplorerItemViewModel viewModel)
        {

            if (viewModel.GetType() == typeof(SaisonViewModel))
            {
                _explorerStates.Saison = viewModel as SaisonViewModel;
                _explorerStates.Liga = null;
                _explorerStates.Mannschaftskampf = null;
                _explorerStates.Einzelkampf = null;
            }
            else if (viewModel.GetType() == typeof(LigaViewModel))
            {
                _explorerStates.Liga = viewModel as LigaViewModel;
                _explorerStates.Mannschaftskampf = null;
                _explorerStates.Einzelkampf = null;
            }
            else if (viewModel.GetType() == typeof(MannschaftskampfViewModel))
            {
                _explorerStates.Mannschaftskampf = viewModel as MannschaftskampfViewModel;
                _explorerStates.Einzelkampf = null;
            }
            else if (viewModel.GetType() == typeof(EinzelkampfViewModel))
            {
                _explorerStates.Einzelkampf = viewModel as EinzelkampfViewModel;
            }

        }
    }
}
