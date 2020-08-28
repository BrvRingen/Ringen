using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ringen.Core.CS
{
    public class Competition : ExtendedNotifyPropertyChanged, IExplorerItem
    {
        /*
        	$saisonId,
	        $competitionId,
	        $ligaId,
	        $tableId,
	        $status,
	        $scheme,
	        $manualInput,
	        $inTable,
	        $inStatistics,
	        $invalidated,
	        $planned,
	        $boutday,
	        $homeTeamName,
	        $opponentTeamName,
	        $homePoints,
	        $opponentPoints,
	        $boutDate,
	        $scaleTime,
	        $audience,
	        $location,
	        $editorName,
	        $editorComment,
	        $refereeName,
	        $refereeGivenname,
	        $lastModified,
	        $editedAt,
	        $editedBy,
	        $editedIpAddr,
	        $controlledAt,
	        $controlledBy,
	        $controllerComment,
	        $validatedHomePoints,
	        $validatedOpponentPoints,
	        $decision;
        */
        private JObject Data;
        public IExplorerItem ExplorerParent { get; }

        public Competition(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.ExplorerParent = Parent;
        }

        public int[] GetKampffolge()
        {
            List<int> temp = new List<int>();
            foreach (var bout in Children)
            {
                temp.Add(bout.Order);
            }

            List<int> result = new List<int>();
            bool first = true;
            while (temp.Any())
            {
                if (first)
                {
                    var elem = temp.First();
                    result.Add(elem);
                    temp.Remove(elem);
                    first = false;
                }
                else
                {
                    var elem = temp.Last();
                    result.Add(elem);
                    temp.Remove(elem);
                    first = true;
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// z. B. RCA Bayreuth - ASV Hof II
        /// </summary>
        public string Value
        {
            get
            {
                return $"{Data["homeTeamName"].ToString()} - {Data["opponentTeamName"].ToString()}";
            }
        }

        public bool IstDoppelRunde()
        {
            var ersteGewichtsklasse = Children.First()?.WeightClass;
            var anzahlSelbeGewichtsklassen = Children.Count(li => li.WeightClass.Equals(ersteGewichtsklasse, StringComparison.OrdinalIgnoreCase));

            if (anzahlSelbeGewichtsklassen > 1)
            {
                return true;
            }

            return false;
        }

        public string LigaId
        {
            get
            {
                return Data["ligaId"].ToString();
            }
        }

        public string TableId
        {
            get
            {
                return Data["tableId"].ToString();
            }
        }

        public Table Table
        {
            get
            {
                return (Table)ExplorerParent;
            }
        }

        /// <summary>
        /// z. B. 19:00:00
        /// </summary>
        public TimeSpan ScaleTime
        {
            get
            {
                return TimeSpan.Parse(Data["scaleTime"].ToString());
            }
        }

        /// <summary>
        /// z. B. 2019
        /// </summary>
        public string SaisonId
        {
            get
            {
                return Data["saisonId"].ToString();
            }
        }

        /// <summary>
        /// z. B. 092100g
        /// </summary>
        public string CompetitionId
        {
            get
            {
                return Data["competitionId"].ToString();
            }
        }

        /// <summary>
        /// z. B. RCA Bayreuth
        /// </summary>
        public string HomeTeamName
        {
            get
            {
                return Get<string>(Data["homeTeamName"]);
            }
        }

        /// <summary>
        /// z. B. ASV Hof II
        /// </summary>
        public string OpponentTeamName
        {
            get
            {
                return Get<string>(Data["opponentTeamName"]);
            }
        }

        /// <summary>
        /// z. B. 12
        /// </summary>
        public string HomePoints
        {
            get
            {
                return Data["homePoints"].ToString();
            }
        }

        /// <summary>
        /// z. B. 44
        /// </summary>
        public string OpponentPoints
        {
            get
            {
                return Data["opponentPoints"].ToString();
            }
        }

        /// <summary>
        /// Kampfdatum
        /// z. B. 2019-12-07
        /// </summary>
        public string BoutDate
        {
            get
            {
                return Data["boutDate"].ToString();
            }
        }

        public DateTime BoutDateDateTime
        {
            get { return DateTime.Parse(BoutDate); }
        }

        /// <summary>
        /// Zuschaueranzahl
        /// z. B. 16
        /// </summary>
        public int Audience
        {
            get
            {
                return Get<int>(Data["audience"]);
            }
            set
            {
                Set(ref Data, "audience", value);
            }
        }

        /// <summary>
        /// Kampfstätte
        /// z. B. Altstadtschule Bayreuth, Fantasierstr. 11, 95445 Bayreuth
        /// </summary>
        public string Location
        {
            get
            {
                return Data["location"].ToString();
            }
        }

        /// <summary>
        /// Benutzername von BRV-Webseite, welcher das Protokoll eingereicht hat.
        /// bayreuth
        /// </summary>
        public string EditorName
        {
            get
            {
                return Data["editorName"].ToString();
            }
        }

        /// <summary>
        /// Protokoll-Kommentar (von Schiedsrichter)
        /// z. B. Bayreuth 57 u. 130 g+f unbesetzt<br>Hof 66 g+f unbesetzt<br>keine Pause<br>
        /// </summary>
        public string EditorComment
        {
            get
            {
                return Data["editorComment"].ToString().Replace("<br>", Environment.NewLine);
            }
            set
            {
                Set(ref Data, "editorComment", value.Replace(Environment.NewLine, "<br>"));
            }
        }

        /// <summary>
        /// Schiedsrichter
        /// z. B. Jürgen Fischer
        /// </summary>
        public string Referee
        {
            get
            {
                return $"{Data["refereeGivenname"].ToString()} {Data["refereeName"].ToString()}";
            }
        }

        private List<Bout> bouts;

        public List<Bout> Children
        {
            get
            {
                if (bouts == null)
                {
                    bouts = new List<Bout>();
                    Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?saisonId={SaisonId}&competitionId={CompetitionId}");
                        
                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var BoutData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                bouts.Add(new Bout((JObject)BoutData, this));
                            }
                        }
                    });
                    if (bouts.Count() == 0)
                    {
                        var BoutsForCompetition = new List<(string WeightClass, string Style)>();
                        //TODO: Aktuell werden die Kämpfe noch nicht übergeben. Übergabe durch BRV notwendig.
                        if (Table.Value.Contains("(S)"))
                        {
                            BoutsForCompetition.Add(("29", "LL"));
                            BoutsForCompetition.Add(("33", "GR"));
                            BoutsForCompetition.Add(("36", "LL"));
                            BoutsForCompetition.Add(("41", "GR"));
                            BoutsForCompetition.Add(("46", "LL"));
                            BoutsForCompetition.Add(("50", "GR"));
                            BoutsForCompetition.Add(("60", "LL"));
                            BoutsForCompetition.Add(("76", "GR"));

                            BoutsForCompetition.Add(("29", "GR"));
                            BoutsForCompetition.Add(("33", "LL"));
                            BoutsForCompetition.Add(("36", "GR"));
                            BoutsForCompetition.Add(("41", "LL"));
                            BoutsForCompetition.Add(("46", "GR"));
                            BoutsForCompetition.Add(("50", "LL"));
                            BoutsForCompetition.Add(("60", "GR"));
                            BoutsForCompetition.Add(("76", "LL"));
                        }
                        else if (Table.Value.Contains("Landesliga"))
                        {
                            BoutsForCompetition.Add(("57", "LL"));
                            BoutsForCompetition.Add(("61", "GR"));
                            BoutsForCompetition.Add(("66", "LL"));
                            BoutsForCompetition.Add(("75", "GR"));
                            BoutsForCompetition.Add(("86", "LL"));
                            BoutsForCompetition.Add(("98", "GR"));
                            BoutsForCompetition.Add(("130", "LL"));

                            BoutsForCompetition.Add(("57", "GR"));
                            BoutsForCompetition.Add(("61", "LL"));
                            BoutsForCompetition.Add(("66", "GR"));
                            BoutsForCompetition.Add(("75", "LL"));
                            BoutsForCompetition.Add(("86", "GR"));
                            BoutsForCompetition.Add(("98", "LL"));
                            BoutsForCompetition.Add(("130", "GR"));
                        }
                        else
                        {
                            BoutsForCompetition.Add(("57", "LL"));
                            BoutsForCompetition.Add(("61", "GR"));
                            BoutsForCompetition.Add(("66", "LL"));
                            BoutsForCompetition.Add(("71", "GR"));
                            BoutsForCompetition.Add(("75 A", "LL"));
                            BoutsForCompetition.Add(("75 B", "GR"));
                            BoutsForCompetition.Add(("80", "LL"));
                            BoutsForCompetition.Add(("86", "GR"));
                            BoutsForCompetition.Add(("98", "LL"));
                            BoutsForCompetition.Add(("130", "GR"));
                        }

                        var i = 1;
                        foreach (var BoutForCompetition in BoutsForCompetition)
                        {
                            bouts.Add(new Bout(i++, BoutForCompetition.WeightClass, BoutForCompetition.Style, this));
                        }
                    }

                }

                return bouts;
            }
            set { bouts = value; }
        }

        public async Task SendAsync()
        {
            var sUpdate = JsonConvert.SerializeObject(Data);

            var UpdateResponse = await REST.Client().PutAsync($"/Api/v1/cs/?saisonId={SaisonId}&competitionId={CompetitionId}", new StringContent(sUpdate, Encoding.UTF8, "application/json"));

            if (UpdateResponse.IsSuccessStatusCode)
            {
                var result = UpdateResponse.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
