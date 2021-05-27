using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ringen.Core.CS;
using Ringen.Core.UI;
using Ringen.Core.ViewModels.Enums;
using Ringen.Schnittstellen.Contracts.Models;

namespace Ringen.Core.ViewModels
{
    public class EinzelkampfViewModel : ExtendedNotifyPropertyChanged, IExplorerItemViewModel
    {

        public int KampfNr { get; internal set; }

        public string Value
        {
            get
            {
                return $"{HeimRinger.Vorname} {HeimRinger.Nachname} - {GastRinger.Vorname} {GastRinger.Nachname}"; ;
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
                if (settings == null) settings = new BoutSettings(StilartViewModel);
                return settings;
            }
            set { settings = value; }
        }

        public int Order { get; internal set; }
        public string Gewichtsklasse { get; internal set; }
        public StilartViewModel StilartViewModel { get; internal set; }
        
        public Ringer HeimRinger { get; internal set; }
        public double HeimRingerGewicht { get; internal set; }

        public int HeimMannschaftswertung { get; internal set; }


        public bool IsNoOpponentWrestler()
        {
            return GastRinger == null;
        }

        public bool IsNoHomeWrestler()
        {
            return HeimRinger == null;
        }


        public Ringer GastRinger { get; internal set; }
        public double GastRingerGewicht { get; internal set; }

        public int GastMannschaftswertung { get; internal set; }


        public SiegartViewModelEnum SiegartViewModel { get; internal set; }
        public string Round1 { get; internal set; }


        private ObservableCollection<BoutPoint> points;

        public ObservableCollection<BoutPoint> Points
        {
            get
            {
                if (points == null) 
                {
                    UpdateDetails();
                    points.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
                    { 
                        base.OnPropertyChanged();
                    };
                }
                return points;
            }
            set { points = value; }
        }

        public List<bool> Children { get; set; }


        private void UpdateDetails()
        {
            points = new ObservableCollection<BoutPoint>();

            //Async.RunSync(async () =>
            //{
            //    var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?saisonId={MannschaftskampfViewModel.SaisonId}&competitionId={MannschaftskampfViewModel.WettkampfId}&order={Order}");

            //    if (AssetResponse.IsSuccessStatusCode)
            //    {
            //        var result = AssetResponse.Content.ReadAsStringAsync().Result;
            //        foreach (var BoutAnnotation in (JArray)JsonConvert.DeserializeObject(result))
            //        {
            //            if (BoutAnnotation["type"].ToString() == "points")
            //            {
            //                foreach (var Point in BoutAnnotation["value"].ToString().Split(','))
            //                {
            //                    var tmpPoint = new Regex(@"(?<value>.*)(?<Wrestler>[R|B])(?<Time>\d*)").Match(Point.ToUpper());
            //                    points.Add(new BoutPoint(tmpPoint.Groups["value"].Value, tmpPoint.Groups["Wrestler"].Value == "r" ? Wrestler.Home : Wrestler.Opponent, int.Parse(tmpPoint.Groups["Time"].Value)));
            //                }
            //            }
            //            else if (BoutAnnotation["type"].ToString() == "duration")
            //            {
            //                Settings.Times[Types.Bout.ToString()].Time = int.Parse(BoutAnnotation["value"].ToString());
            //            }
            //        }
            //    }
            //});
        }
    }
}
