using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Ringen.Core.Messaging;
using Ringen.Core.UI;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Point.xaml
    /// </summary>
    public partial class GriffbewertungspunktButton : ExtendedNotifyPropertyChangedUserControl
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

        public static DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Schnittstellen.Contracts.Models.Griffbewertungspunkt), typeof(GriffbewertungspunktButton), new PropertyMetadata());

        public Schnittstellen.Contracts.Models.Griffbewertungspunkt Data
        {
            get { return (Schnittstellen.Contracts.Models.Griffbewertungspunkt)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public GriffbewertungspunktButton()
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

        private RelayCommand m_AddToPoints;
        public RelayCommand AddToPoints => m_AddToPoints ?? (m_AddToPoints = new RelayCommand(() =>
        {
            EinzelkampfViewModel.ExplorerStates.Einzelkampf.Wertungspunkte.Add(Data);
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, "Point added to Points"));
        }
        ));

    }
}
