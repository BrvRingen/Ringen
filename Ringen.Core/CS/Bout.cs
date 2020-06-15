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
        public WrestleStyles WrestleStyle { get { return Get<WrestleStyles>(Data["style"]); } }
        public int HomeWrestlerId { get { return Get<int>(Data["homeWrestlerId"]); } }
        public int HomeWrestlerLicId { get { return Get<int>(Data["homeWrestlerLicId"]); } }
        public string HomeWrestlerName { get { return Get<string>(Data["homeWrestlerName"]); } }
        public string HomeWrestlerGivenname { get { return Get<string>(Data["homeWrestlerGivenname"]); } }
        public string HomeWrestlerFullnname { get { return $"{Get<string>(Data["homeWrestlerGivenname"])} {Get<string>(Data["homeWrestlerName"])}"; } }
        public int OpponentWrestlerId { get { return Get<int>(Data["opponenteWrestlerId"]); } }
        public int OpponentWrestlerLicId { get { return Get<int>(Data["opponentWrestlerLicId"]); } }
        public string OpponentWrestlerName { get { return Get<string>(Data["opponentWrestlerName"]); } }
        public string OpponentWrestlerGivenname { get { return Get<string>(Data["opponentWrestlerGivenname"]); } }
        public string OpponentWrestlerFullnname { get { return $"{Get<string>(Data["opponentWrestlerGivenname"])} {Get<string>(Data["opponentWrestlerName"])}"; } }
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



        public enum WrestleStyles { LL, GR }
        public enum Results { TÜ, SS, PS, KL }

        private List<string> posPoints;

        public List<string> PosPoints
        {
            get
            {
                if(posPoints == null)
                {
                    //Aktuelle Regeln nach 2017
                    if (WrestleStyle == WrestleStyles.LL)
                        posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ", "A" };
                    else
                        posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ"};
                }

                return posPoints;
            }
            set { posPoints = value; }
        }


        //Timers: Kampf, P Rot, P Blau, Pause, Verletzung Rot, Verletzung Blau, Aktivitätszeit Rot, Aktivitätszeit Blau


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

        private List<Point> points;

        public List<Point> Points
        {
            get
            {
                if (points == null) points = new List<Point>();
                return points;
            }
            set { points = value; }
        }

        public List<bool> Children { get; set; }


        public class Point
        {
            public Wrestler HomeOrOpponent { get; set; }
            public string Value { get; set; }
            public int Time { get; set; }

            public enum Wrestler { Home, Opponent };

            public Point(string Point, Wrestler HomeOrOpponent, int Time)
            {
                this.HomeOrOpponent = HomeOrOpponent;
                this.Value = Point;
                this.Time = Time;
            }


        }
    }
}
