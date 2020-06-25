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

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für Competition.xaml
    /// </summary>
    public partial class ViewCompetition : ExtendedNotifyPropertyChangedUserControl
    {
        private Core.CS.Competition competition;

        public Core.CS.Competition Competition
        {
            get { return competition; }
            set
            {
                competition = value;
                OnPropertyChanged("Competition");
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

    }
}
