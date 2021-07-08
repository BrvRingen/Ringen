using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Ringen.Core.Messaging;
using Ringen.Core.UI;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Griffbewertungspunkt.xaml
    /// </summary>
    public partial class Griffbewertungspunkt : ExtendedNotifyPropertyChangedUserControl
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

        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Schnittstellen.Contracts.Models.Griffbewertungspunkt), typeof(Griffbewertungspunkt), new PropertyMetadata());

        public Schnittstellen.Contracts.Models.Griffbewertungspunkt Data
        {
            get { return (Schnittstellen.Contracts.Models.Griffbewertungspunkt)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public Griffbewertungspunkt()
        {
            InitializeComponent();
            UpdateUi();
            MannschaftskaempfeExplorer.SelectedItemChanged += (object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) =>
            {
                UpdateUi();
            };
        }

        public void UpdateUi()
        {
            EinzelkampfViewModel = MannschaftskaempfeExplorer.SelectedItem as EinzelkampfViewModel;
        }

        private RelayCommand m_Delete;
        public RelayCommand Delete => m_Delete ?? (m_Delete = new RelayCommand(() =>
        {
            EinzelkampfViewModel.ExplorerStates.Einzelkampf.Wertungspunkte.Remove(Data);
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, "Point deleted to Points"));
        }
        ));

    }
}
