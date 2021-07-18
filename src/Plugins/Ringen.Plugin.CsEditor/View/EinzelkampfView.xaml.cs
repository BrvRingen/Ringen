using GalaSoft.MvvmLight.Command;
using Ringen.Core.DependencyInjection;
using Ringen.Core.UI;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor
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
            MannschaftskaempfeExplorer.SelectedItemChanged += (object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) =>
            {
                UpdateUi();
            };
        }

        public void UpdateUi()
        {
            EinzelkampfViewModel = MannschaftskaempfeExplorer.SelectedItem as EinzelkampfViewModel;
        }


        private RelayCommand<string> m_Start;
        public RelayCommand<string> Start => m_Start ?? (m_Start = new RelayCommand<string>((string TimeType) => {
            EinzelkampfViewModel.Settings.Times[TimeType].Start();
            if(TimeType.Contains("Injury"))
                EinzelkampfViewModel.Settings.Times["Bout"].Stop();

        }));

        private RelayCommand<string> m_Stop;
        public RelayCommand<string> Stop => m_Stop ?? (m_Stop = new RelayCommand<string>((string TimeType) => { EinzelkampfViewModel.Settings.Times[TimeType].Stop(); }));
    }
}
