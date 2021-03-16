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
using System.Linq;
using System.Text.RegularExpressions;
using Ringen.Core.ViewModels;

namespace Ringen.Core.CS
{
    public class Bout : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {
        private JObject Data;
        public IExplorerItemViewModel ExplorerParent { get; }

        public int KampfNr
        {
            get
            {
                return Array.IndexOf(MannschaftskampfViewModel.GetKampffolge(), Order) + 1;
            }
        }

        public string Value
        {
            get
            {
                return $"{Data["homeWrestlerName"].ToString()} - {Data["opponentWrestlerName"].ToString()}"; ;
            }
        }

        public MannschaftskampfViewModel MannschaftskampfViewModel
        {
            get
            {
                return (MannschaftskampfViewModel)ExplorerParent;
            }
        }
        public LigaViewModel LigaViewModel
        {
            get
            {
                return null;
                //return (Table)ExplorerParent.ExplorerParent;
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
                    var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?startausweisNr={value.ToString()}&saisonId={MannschaftskampfViewModel.SaisonId}&competitionId={MannschaftskampfViewModel.CompetitionId}");

                    if (AssetResponse.IsSuccessStatusCode)
                    {
                        var Wrestler = (JObject)JsonConvert.DeserializeObject(AssetResponse.Content.ReadAsStringAsync().Result);
                        if (Wrestler != null)
                        {
                            HomeWrestlerName = Wrestler["name"].ToString();
                            HomeWrestlerGivenname = Wrestler["givenname"].ToString();
                            HomeWrestlerStatus = Wrestler["status"].ToString();
                            //birthday
                        }
                    }

                });
            }
        }

        //TODO Ist-Gewicht ersetzen sobald in API Verfügbar
        public double HomeWrestlerWeight
        {
            get
            {
                if (IsNoHomeWrestler())
                {
                    return 0;
                }

                string cleanWeightClass = Regex.Replace(WeightClass, "[A-Za-z ]", "");

                double klasse = Convert.ToDouble(cleanWeightClass);
                var random = new Random();
                double zufallAbzug = (random.NextDouble() * random.Next(1, 5));

                return klasse - zufallAbzug;
            }
        }

        //TODO Ist-Gewicht ersetzen sobald in API Verfügbar
        public double OpponentWrestlerWeight
        {
            get
            {
                if (IsNoOpponentWrestler())
                {
                    return 0;
                }

                string cleanWeightClass = Regex.Replace(WeightClass, "[A-Za-z ]", "");

                double klasse = Convert.ToDouble(cleanWeightClass);
                var random = new Random();
                double zufallAbzug = (random.NextDouble() * random.Next(1, 5));

                return klasse - zufallAbzug;
            }
        }

        public bool IsNoOpponentWrestler()
        {
            return string.IsNullOrEmpty(OpponentWrestlerFullname.Trim());
        }

        public bool IsNoHomeWrestler()
        {
            return string.IsNullOrEmpty(HomeWrestlerFullname.Trim());
        }

        public int HomeWrestlerLicId { get { return Get<int>(Data["homeWrestlerLicId"]); } }
        public string HomeWrestlerName
        {
            get { return Get<string>(Data["homeWrestlerName"]); }
            set { Set(ref Data, "homeWrestlerName", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("HomeWrestlerFullname"); }
        }
        public string HomeWrestlerGivenname
        {
            get { return Get<string>(Data["homeWrestlerGivenname"]); }
            set { Set(ref Data, "homeWrestlerGivenname", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("HomeWrestlerFullname"); }
        }
        public string HomeWrestlerStatus
        {
            get { return Get<string>(Data["homeWrestlerStatus"]); }
            set { Set(ref Data, "homeWrestlerStatus", value); }
        }
        public string HomeWrestlerFullname { get { return $"{Get<string>(Data["homeWrestlerGivenname"])} {Get<string>(Data["homeWrestlerName"])}"; } }
        public int HomeWrestlerPoints { get { return Get<int>(Data["homeWrestlerPoints"]); } }
        public int HomeWrestlerFlags { get { return Get<int>(Data["homeWrestlerFlags"]); } }

        public int OpponentWrestlerId
        {
            get
            {
                return Get<int>(Data["opponentWrestlerId"]);
            }
            set
            {
                Set(ref Data, "opponentWrestlerId", value);

                Async.RunSync(async () =>
                {
                    var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?startausweisNr={value.ToString()}&saisonId={MannschaftskampfViewModel.SaisonId}&competitionId={MannschaftskampfViewModel.CompetitionId}");

                    if (AssetResponse.IsSuccessStatusCode)
                    {
                        var Wrestler = (JObject)JsonConvert.DeserializeObject(AssetResponse.Content.ReadAsStringAsync().Result);
                        if (Wrestler != null)
                        {
                            OpponentWrestlerName = Wrestler["name"].ToString();
                            OpponentWrestlerGivenname = Wrestler["givenname"].ToString();
                            OpponentWrestlerStatus = Wrestler["status"].ToString();
                            //birthday
                        }
                    }

                });
            }
        }
        public int OpponentWrestlerLicId { get { return Get<int>(Data["opponentWrestlerLicId"]); } }
        public string OpponentWrestlerName
        {
            get { return Get<string>(Data["opponentWrestlerName"]); }
            set { Set(ref Data, "opponentWrestlerName", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("OpponentWrestlerFullname"); }
        }
        public string OpponentWrestlerGivenname
        {
            get { return Get<string>(Data["opponentWrestlerGivenname"]); }
            set { Set(ref Data, "opponentWrestlerGivenname", value); base.OnPropertyChanged("Value"); base.OnPropertyChanged("OpponentWrestlerFullname"); }
        }
        public string OpponentWrestlerStatus
        {
            get { return Get<string>(Data["opponentWrestlerStatus"]); }
            set { Set(ref Data, "opponentWrestlerStatus", value); }
        }
        public string OpponentWrestlerFullname { get { return $"{Get<string>(Data["opponentWrestlerGivenname"])} {Get<string>(Data["opponentWrestlerName"])}"; } }
        public int OpponentWrestlerPoints { get { return Get<int>(Data["opponentWrestlerPoints"]); } }
        public int OpponentWrestlerFlags { get { return Get<int>(Data["opponentWrestlerFlags"]); } }
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
                if (points == null) 
                {
                    UpdateDetails();
                    points.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => { base.OnPropertyChanged("Points");  };
                }
                return points;
            }
            set { points = value; }
        }

        public List<bool> Children { get; set; }

        public Bout(JObject Data, IExplorerItemViewModel Parent)
        {
            this.Data = Data;
            this.ExplorerParent = Parent;
        }

        public Bout(int Order, string WeightClass, string Style, IExplorerItemViewModel Parent)
        {
            this.Data = (JObject)DefaultBout.DeepClone();
            Data["order"] = Order;
            Data["weightClass"] = WeightClass;
            Data["style"] = Style;
            this.ExplorerParent = Parent;
        }

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

        private void UpdateDetails()
        {
            points = new ObservableCollection<BoutPoint>();

            Async.RunSync(async () =>
            {
                var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?saisonId={MannschaftskampfViewModel.SaisonId}&competitionId={MannschaftskampfViewModel.CompetitionId}&order={Order}");

                if (AssetResponse.IsSuccessStatusCode)
                {
                    var result = AssetResponse.Content.ReadAsStringAsync().Result;
                    foreach (var BoutAnnotation in (JArray)JsonConvert.DeserializeObject(result))
                    {
                        if (BoutAnnotation["type"].ToString() == "points")
                        {
                            foreach (var Point in BoutAnnotation["value"].ToString().Split(','))
                            {
                                var tmpPoint = new Regex(@"(?<value>.*)(?<Wrestler>[R|B])(?<Time>\d*)").Match(Point.ToUpper());
                                points.Add(new BoutPoint(tmpPoint.Groups["value"].Value, this, tmpPoint.Groups["Wrestler"].Value == "r" ? BoutPoint.Wrestler.Home : BoutPoint.Wrestler.Opponent, int.Parse(tmpPoint.Groups["Time"].Value)));
                            }
                        }
                        else if (BoutAnnotation["type"].ToString() == "duration")
                        {
                            Settings.Times[BoutTime.Types.Bout.ToString()].Time = int.Parse(BoutAnnotation["value"].ToString());
                        }
                    }
                }
            });
        }
    }
}
