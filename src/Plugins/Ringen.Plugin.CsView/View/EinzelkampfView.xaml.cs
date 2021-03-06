﻿using Ringen.Core;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ringen.Core.DependencyInjection;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsView
{
    /// <summary>
    /// Interaktionslogik für Bout.xaml
    /// </summary>
    public partial class EinzelkampfView : ExtendedNotifyPropertyChangedUserControl
    {
        private EinzelkampfViewModel _einzelkampfViewModel;
        
        public EinzelkampfViewModel EinzelkampfViewModel
        {
            get { return _einzelkampfViewModel; }
            set
            {
                _einzelkampfViewModel = value;
                OnPropertyChanged(nameof(EinzelkampfViewModel));
            }
        }

        public EinzelkampfView()
        {
            InitializeComponent();
            UpdateUi();
            MannschaftskaempfeExplorer.SelectedItemChanged += ((object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) => { UpdateUi(); });
        }

        public void UpdateUi()
        {
            EinzelkampfViewModel = MannschaftskaempfeExplorer.SelectedItem as EinzelkampfViewModel;
        }
    }
}
