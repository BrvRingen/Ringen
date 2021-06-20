using GalaSoft.MvvmLight.Command;
using Ringen.Core;
using Ringen.Core.UI;
using System.Threading.Tasks;
using System.Windows;
using Ringen.Core.DependencyInjection;
using Ringen.Core.ViewModels;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Competition.xaml
    /// </summary>
    public partial class MannschaftskampfView : ExtendedNotifyPropertyChangedUserControl
    {
        private MannschaftskampfViewModel _mannschaftskampfViewModel;

        public MannschaftskampfViewModel MannschaftskampfViewModel
        {
            get { return _mannschaftskampfViewModel; }
            set
            {
                _mannschaftskampfViewModel = value;
                OnPropertyChanged(nameof(MannschaftskampfViewModel));
            }
        }

        private CompetitionInfosViewModel _competitionInfos;

        public CompetitionInfosViewModel CompetitionInfos
        {
            get { return _competitionInfos; }
            set
            {
                _competitionInfos = value;
                OnPropertyChanged(nameof(MannschaftskampfView.CompetitionInfos));
            }
        }

        public MannschaftskampfView()
        {
            InitializeComponent();
            UpdateUi();
            MannschaftskaempfeExplorer.SelectedItemChanged += ((object sender, MannschaftskaempfeExplorer.SelectedItemChangedEventArgs e) => { UpdateUi(); });
        }

        public void UpdateUi()
        {
            MannschaftskampfViewModel = MannschaftskaempfeExplorer.SelectedItem as MannschaftskampfViewModel;

            CompetitionInfos = new CompetitionInfosViewModel();
            CompetitionInfos.Ordner.Add("Test Ordner 1");
            CompetitionInfos.Ordner.Add("Test Ordner 2");
        }

        private RelayCommand m_SendCompetitionToBrv;
        public RelayCommand SendCompetitionToBrv => m_SendCompetitionToBrv ?? (m_SendCompetitionToBrv = new RelayCommand(async () => { await OnSendCompetitionToBrvAsync(); }));

        private async Task OnSendCompetitionToBrvAsync()
        {
            await MannschaftskampfViewModel.Sende_Ergebnis_Async();

            return;
        }


        private RelayCommand m_GetAudience;
        public RelayCommand GetAudience => m_GetAudience ?? (m_GetAudience = new RelayCommand(async () => {
            var PrivateInfos = await Privat.GetPrivateInfos();
            MannschaftskampfViewModel.AnzahlZuschauer = PrivateInfos.audience;
        }));

        private RelayCommand m_CreateProtocol;
        public RelayCommand CreateProtocol => m_CreateProtocol ?? (m_CreateProtocol = new RelayCommand(async () => {
            await Protocol.OnCreateProtocolAsync(this.MannschaftskampfViewModel);}));

        private RelayCommand m_CreateAllList;
        public RelayCommand CreateAllList => m_CreateAllList ?? (m_CreateAllList = new RelayCommand(async () => {
            await Protocol.OnCreateCreateAllListAsync(this.MannschaftskampfViewModel);
        }));

        private RelayCommand m_CreateBoutResultList;
        public RelayCommand CreateBoutResultList => m_CreateBoutResultList ?? (m_CreateBoutResultList = new RelayCommand(async () => {
            await Protocol.OnCreateBoutResultListAsync(this.MannschaftskampfViewModel);
        }));

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CompetitionInfos.Ordner.Add(txtNeuerOdner.Text);
            OrdnerListBox.Items.Refresh();
            txtNeuerOdner.Text = "";
        }
    }
}
