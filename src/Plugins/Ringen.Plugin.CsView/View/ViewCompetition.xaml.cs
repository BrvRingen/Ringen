using Ringen.Core;
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
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsView
{
    /// <summary>
    /// Interaktionslogik für Competition.xaml
    /// </summary>
    public partial class ViewCompetition : ExtendedNotifyPropertyChangedUserControl
    {
        private MannschaftskampfViewModel _mannschaftskampfViewModel;

        public MannschaftskampfViewModel MannschaftskampfViewModel
        {
            get { return _mannschaftskampfViewModel; }
            set
            {
                _mannschaftskampfViewModel = value;
                OnPropertyChanged("Competition");
            }
        }

        public ViewCompetition()
        {
            InitializeComponent();
            UpdateUi();
            MannschaftskaempfeExplorer.SelectedItemChanged += ((object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) => { UpdateUi(); });
        }

        public void UpdateUi()
        {
            MannschaftskampfViewModel = MannschaftskaempfeExplorer.SelectedItem as MannschaftskampfViewModel;
        }
    }
}
