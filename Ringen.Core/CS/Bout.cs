using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System.Collections.Generic;
using System.Timers;
using Ringen.Core.Messaging;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections;
using System;
using System.ComponentModel;
using System.IO;

namespace Ringen.Core.CS
{
    public class Bout : ExtendedNotifyPropertyChanged, IExplorerItem
    {
        #region properties

        private JObject Data;
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
        public int HomeWrestlerId
        {
            get
            {
                return Get<int>(Data["homeWrestlerId"]);
            }
            set
            {
                Set(ref Data, "homeWrestlerId", value);

                Async.RunSync(async () =>
                {
                    var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?startausweisNr={value.ToString()}&saisonId={Competition.SaisonId}&competitionId={Competition.CompetitionId}");

                    if (AssetResponse.IsSuccessStatusCode)
                    {
                        var Wrestler = (JObject)JsonConvert.DeserializeObject(AssetResponse.Content.ReadAsStringAsync().Result);
                        HomeWrestlerName = Wrestler["name"].ToString();
                        HomeWrestlerGivenname = Wrestler["givenname"].ToString();
                        HomeWrestlerStatus = Wrestler["status"].ToString();
                        //birthday
                    }

                });
            }
        }
        public int HomeWrestlerLicId { get { return Get<int>(Data["homeWrestlerLicId"]); } }
        public string HomeWrestlerName {
            get { return Get<string>(Data["homeWrestlerName"]); }
            set { Set(ref Data, "homeWrestlerName", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("HomeWrestlerFullnname"); }
        }
        public string HomeWrestlerGivenname
        {
            get { return Get<string>(Data["homeWrestlerGivenname"]); }
            set { Set(ref Data, "homeWrestlerGivenname", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("HomeWrestlerFullnname"); }
        }
        public string HomeWrestlerStatus
        {
            get { return Get<string>(Data["homeWrestlerStatus"]); }
            set { Set(ref Data, "homeWrestlerStatus", value); }
        }
        public string HomeWrestlerFullnname { get { return $"{Get<string>(Data["homeWrestlerGivenname"])} {Get<string>(Data["homeWrestlerName"])}"; } }
        public int HomeWrestlerPoints { get { return Get<int>(Data["homeWrestlerPoints"]); } }
        public int HomeWrestlerFlags { get { return Get<int>(Data["homeWrestlerFlags"]); } }

        public int OpponentWrestlerId { get { return Get<int>(Data["opponentWrestlerId"]); } }
        public int OpponentWrestlerLicId { get { return Get<int>(Data["opponentWrestlerLicId"]); } }
        public string OpponentWrestlerName { get { return Get<string>(Data["opponentWrestlerName"]); } }
        public string OpponentWrestlerGivenname { get { return Get<string>(Data["opponentWrestlerGivenname"]); } }
        public string OpponentWrestlerFullnname { get { return $"{Get<string>(Data["opponentWrestlerGivenname"])} {Get<string>(Data["opponentWrestlerName"])}"; } }
        public int OpponentWrestlerPoints { get { return Get<int>(Data["opponentWrestlerPoints"]); } }
        public int OpponentWrestlerFlags { get { return Get<int>(Data["opponentWrestlerFlags"]); } }
        public string OpponentWrestlerStatus { get { return Get<string>(Data["opponentWrestlerStatus"]); } }
        public BoutSettings.Results Result { get { return Get<BoutSettings.Results>(Data["result"]); } }
        public string Round1 { get { return Get<string>(Data["round1"]); } }
        public string Round2 { get { return Get<string>(Data["round2"]); } }
        public string Round3 { get { return Get<string>(Data["round3"]); } }
        public string Round4 { get { return Get<string>(Data["round4"]); } }
        public string Round5 { get { return Get<string>(Data["round5"]); } }



        private ObservableCollection<BoutPoint> points;

        public ObservableCollection<BoutPoint> Points
        {
            get
            {
                if (points == null) points = new ObservableCollection<BoutPoint>();
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

        public Bout(int Order, string WeightClass, string Style, IExplorerItem Parent)
        {
            this.Data = (JObject)DefaultBout.DeepClone();
            Data["order"] = Order;
            Data["weightClass"] = WeightClass;
            Data["style"] = Style;
            this.ExplorerParent = Parent;
        }

        #endregion

        #region internal functionas

        private JObject defaultBout;
        private JObject DefaultBout
        {
            get
            {
                if (defaultBout == null)
                {
                    var myAssembly = Assembly.GetExecutingAssembly();
                    System.IO.Stream tempStream = myAssembly.GetManifestResourceStream(myAssembly.GetName().Name + ".g.resources");
                    System.Resources.ResourceReader tempReader = new System.Resources.ResourceReader(tempStream);

                    foreach (DictionaryEntry resource in tempReader)
                    {
                        if (resource.Key.ToString().Contains(@"cs/defaultbout.json"))
                        {
                            var tempPath = new Uri("/" + myAssembly.GetName().Name + ";component/" + resource.Key.ToString(), UriKind.Relative);

                            using (var sr = new StreamReader(System.Windows.Application.GetResourceStream(tempPath).Stream))
                            {
                                defaultBout = JObject.Parse(sr.ReadToEnd());
                            }
                            break;
                        }
                    }
                }
                return defaultBout;
            }
        }


        #endregion

        #region classes




        #endregion
    }

    public class BoutPoint
    {
        public Bout Bout { get; set; }
        public Wrestler? HomeOrOpponent { get; set; }
        public string Value { get; set; }
        public int? Time { get; set; }

        public enum Wrestler { Home, Opponent };

        public BoutPoint(string Value, Bout Bout, Wrestler? HomeOrOpponent = null)
        {
            this.Bout = Bout;
            this.HomeOrOpponent = HomeOrOpponent;
            this.Value = Value;
            Time = Bout.Settings.Times[BoutTime.Types.Bout.ToString()].Time;
        }
    }

    public class BoutSettings
    {
        public Bout Bout { get; set; }

        public enum WrestleStyles
        {
            [Description("Freistil")]
            LL, 

            [Description("Gr.-röm.")]
            GR
        }
        public enum Results { TÜ, SS, PS, KL }

        private List<string> posPoints;

        public List<string> PosPoints
        {
            get { return posPoints; }
            set { posPoints = value; }
        }
        public List<BoutPoint> PosPointsHome
        {
            get
            {
                var tmp = new List<BoutPoint>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new BoutPoint(posPoint, Bout, BoutPoint.Wrestler.Home));
                }

                return tmp;
            }
        }
        public List<BoutPoint> PosPointsOpponent
        {
            get
            {
                var tmp = new List<BoutPoint>();
                foreach (var posPoint in posPoints)
                {
                    tmp.Add(new BoutPoint(posPoint, Bout, BoutPoint.Wrestler.Opponent));
                }

                return tmp;
            }
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
                        { BoutTime.Types.Bout.ToString(), new BoutTime(this, BoutTime.Types.Bout, 360, new List<int>() { 180 }) },
                        { BoutTime.Types.Break.ToString(), new BoutTime(this, BoutTime.Types.Break, 30) },
                        { BoutTime.Types.HomeInjury.ToString(), new BoutTime(this, BoutTime.Types.HomeInjury, 120) },
                        { BoutTime.Types.OpponentInjury.ToString(), new BoutTime(this, BoutTime.Types.OpponentInjury, 120) }//,
                        //{ BoutTime.Types.HomeActivity.ToString(), new BoutTime(30) },
                        //{ BoutTime.Types.OpponentActivity.ToString(), new BoutTime(30) },
                        //{ BoutTime.Types.HomeP.ToString(), new BoutTime() },
                        //{ BoutTime.Types.OpponentP.ToString(), new BoutTime() }
                    };
                }
                return times;
            }
        }

        public BoutSettings(Bout Bout)
        {
            this.Bout = Bout;
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

        public enum Types { Bout, Break, HomeInjury, OpponentInjury, HomeActivity, OpponentActivity, HomeP, OpponentP }

        private Types m_Type;

        public Types Type
        {
            get { return Get(m_Type); }
            set { Set(ref m_Type, value); }
        }

        private BoutSettings BoutSettings;

        public int Max { get; set; }
        public List<int> Pauses { get; set; }

        public BoutTime(BoutSettings BoutSettings, Types Type, int Max, List<int> Pauses = null)
        {
            this.BoutSettings = BoutSettings;
            this.Type = Type;
            Mode = Modes.Paused;
            this.Max = Max;
            this.Pauses = Pauses;
        }

        public void Start()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{Type.ToString()}' wurde gestartet."));
            Timer.Start();
            Mode = Modes.Running;

            if (Type == Types.HomeInjury || Type == Types.OpponentInjury) BoutSettings.Times[Types.Bout.ToString()].Stop();
        }
        public void Stop()
        {
            LoggerMessage.Send(new LogEntry(LogEntryType.Message, $"Timer '{Type.ToString()}' wurde gestoppt."));
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

                    if (Type == Types.Bout) BoutSettings.Times[Types.Break.ToString()].Start();
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
