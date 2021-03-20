using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.CS;
using Ringen.Core.UI;

namespace Ringen.Core.ViewModels
{
    public class EinzelkampfViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {

        public int KampfNr { get; internal set; }

        public string Value
        {
            get
            {
                return $"{HomeWrestlerFullname} - {OpponentWrestlerFullname}"; ;
            }
        }

        public MannschaftskampfViewModel MannschaftskampfViewModel //TODO: muss raus
        {
            get
            {
                return null;
            }
        }

        public LigaViewModel LigaViewModel //TODO muss raus
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

        public int Order { get; internal set; }
        public string WeightClass { get; internal set; }
        public BoutSettings.WrestleStyles WrestleStyle { get; internal set; }
        
        public int HomeWrestlerId { get; internal set; }

        public double HomeWrestlerWeight { get; internal set; }

        public double OpponentWrestlerWeight { get; internal set; }

        public bool IsNoOpponentWrestler()
        {
            return string.IsNullOrEmpty(OpponentWrestlerFullname.Trim());
        }

        public bool IsNoHomeWrestler()
        {
            return string.IsNullOrEmpty(HomeWrestlerFullname.Trim());
        }

        public int HomeWrestlerLicId { get; internal set; }
        public string HomeWrestlerName { get; internal set; }
        public string HomeWrestlerGivenname { get; internal set; }
        public string HomeWrestlerStatus { get; internal set; }

        public string HomeWrestlerFullname
        {
            get
            {
                return $"{HomeWrestlerName}{(!string.IsNullOrEmpty(HomeWrestlerGivenname) ? $", {HomeWrestlerGivenname}" : string.Empty)}";
            }
        }

        public int HomeWrestlerPoints { get; internal set; }
        public int HomeWrestlerFlags { get; internal set; }

        public int OpponentWrestlerId { get; internal set; }
        public int OpponentWrestlerLicId { get; internal set; }
        public string OpponentWrestlerName { get; internal set; }
        public string OpponentWrestlerGivenname { get; internal set; }
        public string OpponentWrestlerStatus { get; internal set; }

        public string OpponentWrestlerFullname
        {
            get
            {
                return $"{OpponentWrestlerName}{(!string.IsNullOrEmpty(OpponentWrestlerGivenname) ? $", {OpponentWrestlerGivenname}" : string.Empty)}";
            }
        }

        public int OpponentWrestlerPoints { get; internal set; }
        public int OpponentWrestlerFlags { get; internal set; }
        public BoutSettings.Results Result { get; internal set; }
        public string Round1 { get; internal set; }


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
                var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?saisonId={MannschaftskampfViewModel.SaisonId}&competitionId={MannschaftskampfViewModel.WettkampfId}&order={Order}");

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
