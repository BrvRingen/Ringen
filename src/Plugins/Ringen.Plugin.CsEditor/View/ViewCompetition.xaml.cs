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
using Ringen.Shared.Models;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Competition.xaml
    /// </summary>
    public partial class ViewCompetition : ExtendedNotifyPropertyChangedUserControl
    {
        private Core.CS.Competition _competition;

        public Core.CS.Competition Competition
        {
            get { return _competition; }
            set
            {
                _competition = value;
                OnPropertyChanged(nameof(ViewCompetition.Competition));
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
            Explorer.SelectedItemChanged += ((object sender, Explorer.SelectedItemChangedEventArgs e) => { UpdateUi(); });
        }

        public void UpdateUi()
        {
            Competition = Explorer.SelectedItem as Core.CS.Competition;
            CompetitionInfos = new CompetitionInfos();
            CompetitionInfos.Ordner.Add("Test Ordner 1");
            CompetitionInfos.Ordner.Add("Test Ordner 2");
        }

        private RelayCommand m_SendCompetitionToBrv;
        public RelayCommand SendCompetitionToBrv => m_SendCompetitionToBrv ?? (m_SendCompetitionToBrv = new RelayCommand(async () => { await OnSendCompetitionToBrvAsync(); }));

        private async Task OnSendCompetitionToBrvAsync()
        {
            await Competition.SendAsync();

            return;
        }


        private RelayCommand m_GetAudience;
        public RelayCommand GetAudience => m_GetAudience ?? (m_GetAudience = new RelayCommand(async () => {

            var Response = await PrivateREST.Client().GetAsync($"/Api/v1/cs/");

            if (Response.IsSuccessStatusCode)
            {
                var result = (JObject)JsonConvert.DeserializeObject(Response.Content.ReadAsStringAsync().Result);
                Competition.Audience = int.Parse(result["audience"].ToString());
            }

        }));

        private RelayCommand m_CreateProtocol;
        public RelayCommand CreateProtocol => m_CreateProtocol ?? (m_CreateProtocol = new RelayCommand(async () => {
            await Protocol.OnCreateProtocolAsync(this.Competition);}));

        private RelayCommand m_CreateAllList;
        public RelayCommand CreateAllList => m_CreateAllList ?? (m_CreateAllList = new RelayCommand(async () => {
            await Protocol.OnCreateCreateAllListAsync(this.Competition);
        }));

        private RelayCommand m_CreateBoutResultList;
        public RelayCommand CreateBoutResultList => m_CreateBoutResultList ?? (m_CreateBoutResultList = new RelayCommand(async () => {
            await Protocol.OnCreateBoutResultListAsync(this.Competition);
        }));

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CompetitionInfos.Ordner.Add(txtNeuerOdner.Text);
            OrdnerListBox.Items.Refresh();
            txtNeuerOdner.Text = "";
        }
    }
}
