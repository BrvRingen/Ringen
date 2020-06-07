using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System.Collections.Generic;
using System.Timers;

namespace Ringen.Core.CS
{
    public class Bout : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
    {
        private JObject Data;
        public IExplorerItem Parent { get; }

        public Bout(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.Parent = Parent;
        }

        public string Value
        {
            get
            {
                return $"{Data["homeWrestlerName"].ToString()} - {Data["opponentWrestlerName"].ToString()}"; ;
            }
        }

        public Competition Competition
        {
            get
            {
                return (Competition)Parent;
            }
        }
        public Table Table
        {
            get
            {
                return (Table)Parent.Parent;
            }
        }

        public int Order { get { return Get<int>(Data["order"]); } }
        public string WeightClass { get { return Get<string>(Data["weightClass"]); } }
        public string WrestleStyle { get { return Get<string>(Data["style"]); } }
        public int HomeWrestlerId { get { return Get<int>(Data["homeWrestlerId"]); } }
        public int HomeWrestlerLicId { get { return Get<int>(Data["homeWrestlerLicId"]); } }
        public string HomeWrestlerName { get { return Get<string>(Data["homeWrestlerName"]); } }
        public string HomeWrestlerGivenname { get { return Get<string>(Data["homeWrestlerGivenname"]); } }
        public int OpponentWrestlerId { get { return Get<int>(Data["opponenteWrestlerId"]); } }
        public int OpponentWrestlerLicId { get { return Get<int>(Data["opponentWrestlerLicId"]); } }
        public string OpponentWrestlerName { get { return Get<string>(Data["opponentWrestlerName"]); } }
        public string OpponentWrestlerGivenname { get { return Get<string>(Data["opponentWrestlerGivenname"]); } }
        public int HomeWrestlerPoints { get { return Get<int>(Data["homeWrestlerPoints"]); } }
        public int HomeWrestlerFlags { get { return Get<int>(Data["homeWrestlerFlags"]); } }
        public int OpponentWrestlerPoints { get { return Get<int>(Data["opponentWrestlerPoints"]); } }
        public int OpponentWrestlerFlags { get { return Get<int>(Data["opponentWrestlerFlags"]); } }
        public Results Result { get { return Get<Results>(Data["result"]); } }
        public string Round1 { get { return Get<string>(Data["round1"]); } }
        public string Round2 { get { return Get<string>(Data["round2"]); } }
        public string Round3 { get { return Get<string>(Data["round3"]); } }
        public string Round4 { get { return Get<string>(Data["round4"]); } }
        public string Round5 { get { return Get<string>(Data["round5"]); } }
        public string HomeWrestlerStatus { get { return Get<string>(Data["homeWrestlerStatus"]); } }
        public string OpponentWrestlerStatus { get { return Get<string>(Data["opponentWrestlerStatus"]); } }




        public enum Results { TÜ, SS, PS }



        private Timer myTimer;
        public Timer MyTimer
        {
            get
            {
                if(myTimer == null)
                {
                    myTimer = new Timer();
                    myTimer.Interval = 1000;
                    myTimer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        ZeitRunde1++;
                        Time++;
                    };
                }
                return myTimer;
            }
        }

        private int zeitRunde1 = 0;
        public int ZeitRunde1
        {
            get
            {
                return zeitRunde1;
            }
            set
            {
                zeitRunde1 = value;
                base.OnPropertyChanged();
            }
        }

        private int time = 0;
        public int Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                base.OnPropertyChanged();
            }
        }



        public List<bool> Children { get; set; }
    }
}
