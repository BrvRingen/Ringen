using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor
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
                OnPropertyChanged(nameof(ViewCompetition.MannschaftskampfViewModel));
            }
        }

        private CompetitionInfos _competitionInfos;

        public CompetitionInfos CompetitionInfos
        {
            get { return _competitionInfos; }
            set
            {
                _competitionInfos = value;
                OnPropertyChanged(nameof(ViewCompetition.CompetitionInfos));
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
            CompetitionInfos = new CompetitionInfos();
            CompetitionInfos.Ordner.Add("Test Ordner 1");
            CompetitionInfos.Ordner.Add("Test Ordner 2");
        }

        private RelayCommand m_SendCompetitionToBrv;
        public RelayCommand SendCompetitionToBrv => m_SendCompetitionToBrv ?? (m_SendCompetitionToBrv = new RelayCommand(async () => { await OnSendCompetitionToBrvAsync(); }));

        private async Task OnSendCompetitionToBrvAsync()
        {
            await MannschaftskampfViewModel.SendAsync();

            return;
        }


        private RelayCommand m_GetAudience;
        public RelayCommand GetAudience => m_GetAudience ?? (m_GetAudience = new RelayCommand(async () => {

            var Response = await PrivateREST.Client().GetAsync($"/Api/v1/cs/");

            if (Response.IsSuccessStatusCode)
            {
                var result = (JObject)JsonConvert.DeserializeObject(Response.Content.ReadAsStringAsync().Result);
                MannschaftskampfViewModel.Audience = int.Parse(result["audience"].ToString());
            }

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
