using Ringen.Core.UI;
using Ringen.Core;
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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using Ringen.Core.Messaging;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ringen.Plugin.CsEditor
{
    /// <summary>
    /// Interaktionslogik für CsEditor.xaml
    /// </summary>
    public partial class View : RingenTabItem
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

        private Core.CS.Bout bout;

        public Core.CS.Bout Bout
        {
            get { return bout; }
            set
            {
                bout = value;
                OnPropertyChanged("Bout");
            }
        }

        public View()
        {
            InitializeComponent();
            SetData();

            Explorer.SelectedItemChanged += ((object sender, Explorer.SelectedItemChangedEventArgs e) => { SetData(); });
        }

        private void SetData()
        {
            Competition = Explorer.SelectedItem as Core.CS.Competition;
            Bout = Explorer.SelectedItem as Core.CS.Bout;
        }

        private RelayCommand m_ZeitRunde1Start;
        public RelayCommand ZeitRunde1Start => m_ZeitRunde1Start ?? (m_ZeitRunde1Start = new RelayCommand(OnZeitRunde1Start));

        private void OnZeitRunde1Start()
        {
            Bout.MyTimer.Start();
        }

        private RelayCommand m_ZeitRunde1Stop;
        public RelayCommand ZeitRunde1Stop => m_ZeitRunde1Stop ?? (m_ZeitRunde1Stop = new RelayCommand(() => { Bout.MyTimer.Stop(); }));



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
    }
}
