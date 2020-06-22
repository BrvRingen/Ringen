using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System.Collections.Generic;
using System.Timers;

namespace Ringen.Core.CS
{
    public class Bout : ExtendedNotifyPropertyChanged, IExplorerItem
    {
        #region properties

        private readonly JObject Data;
        public IExplorerItem ExplorerParent { get; }

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
                return (Competition)ExplorerParent;
            }
        }
        public Table Table
        {
            get
            {
                return (Table)ExplorerParent.ExplorerParent;
            }
        }

        private BoutSettings settings;

        public BoutSettings Settings
        {
            get
            {
                if (settings == null) settings = new BoutSettings(this);
                return settings;
            }
            set { settings = value; }
        }

        public int Order { get { return Get<int>(Data["order"]); } }
        public string WeightClass { get { return Get<string>(Data["weightClass"]); } }
        public BoutSettings.WrestleStyles WrestleStyle { get { return Get<BoutSettings.WrestleStyles>(Data["style"]); } }
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
        public BoutSettings.Results Result { get { return Get<BoutSettings.Results>(Data["result"]); } }
        public string Round1 { get { return Get<string>(Data["round1"]); } }
        public string Round2 { get { return Get<string>(Data["round2"]); } }
        public string Round3 { get { return Get<string>(Data["round3"]); } }
        public string Round4 { get { return Get<string>(Data["round4"]); } }
        public string Round5 { get { return Get<string>(Data["round5"]); } }
        public string HomeWrestlerStatus { get { return Get<string>(Data["homeWrestlerStatus"]); } }
        public string OpponentWrestlerStatus { get { return Get<string>(Data["opponentWrestlerStatus"]); } }



        private List<BoutPoint> points;

        public List<BoutPoint> Points
        {
            get
            {
                if (points == null) points = new List<BoutPoint>();
                return points;
            }
            set { points = value; }
        }

        public List<bool> Children { get; set; }

        #endregion

        #region constructors
        public Bout(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.ExplorerParent = Parent;
        }

        #endregion

        #region classes




        #endregion
    }

    public class BoutPoint
    {
        public Wrestler HomeOrOpponent { get; set; }
        public string Value { get; set; }
        public int Time { get; set; }

        public enum Wrestler { Home, Opponent };

        public BoutPoint(string Point, Wrestler HomeOrOpponent, int Time)
        {
            this.HomeOrOpponent = HomeOrOpponent;
            this.Value = Point;
            this.Time = Time;
        }
    }

    public class BoutSettings
    {
        public enum WrestleStyles { LL, GR }
        public enum Results { TÜ, SS, PS, KL }

        private List<string> posPoints;

        public List<string> PosPoints
        {
            get { return posPoints; }
            set { posPoints = value; }
        }

        public Dictionary<string, BoutTime> times;
        public Dictionary<string, BoutTime> Times
        {
            get
            {
                if (times == null)
                {
                    times = new Dictionary<string, BoutTime>
                    {
                        { "Bout", new BoutTime(360, new List<int>() { 180 }) },
                        { "Break", new BoutTime(30) },
                        { "HomeInjury", new BoutTime(120) },
                        { "OpponentInjury", new BoutTime(120) }//,
                        //{ "HomeActivity", new BoutTime(30) },
                        //{ "OpponentActivity", new BoutTime(30) },
                        //{ "HomeP", new BoutTime() },
                        //{ "OpponentP", new BoutTime() }
                    };
                }
                return times;
            }
        }

        public BoutSettings(Bout Bout)
        {
            //Aktuelle Regeln nach 2017
            if (Bout.WrestleStyle == WrestleStyles.LL)
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ", "A" };
            else
                posPoints = new List<string>() { "1", "2", "4", "5", "P", "0", "VZ" };


        }
    }

    public class BoutTime : ExtendedNotifyPropertyChanged
    {
        public enum Modes { Running, Paused, Finished }

        private Modes m_Mode;

        public Modes Mode
        {
            get { return Get(m_Mode); }
            set { Set(ref m_Mode, value); }
        }


        public int Max { get; set; }
        public List<int> Pauses { get; set; }

        public BoutTime(int Max, List<int> Pauses = null)
        {
            Mode = Modes.Paused;
            this.Max = Max;
            this.Pauses = Pauses;
        }

        public void Start()
        {
            Timer.Start();
            Mode = Modes.Running;
        }
        public void Stop()
        {
            Timer.Stop();
            Mode = Modes.Paused;
        }

        private Timer timer;
        private Timer Timer
        {
            get
            {
                if (timer == null)
                {
                    Mode = Modes.Paused;
                    timer = new Timer
                    {
                        Interval = 1000
                    };
                    timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                    {
                        Time++;
                    };
                }
                return timer;
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

                if (Pauses != null && Pauses.Contains(Time))
                {
                    Timer.Stop();
                    Mode = Modes.Paused;
                }
                if (Time == Max)
                {
                    Timer.Stop();
                    Mode = Modes.Finished;
                }

                base.OnPropertyChanged();
            }
        }
    }
}
