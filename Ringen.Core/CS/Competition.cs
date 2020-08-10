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

        public string Value
        {
            get
            {
                return $"{Data["homeTeamName"].ToString()} - {Data["opponentTeamName"].ToString()}";
            }
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

        public string SaisonId
        {
            get
            {
                return Data["saisonId"].ToString();
            }
        }

        public string CompetitionId
        {
            get
            {
                return Data["competitionId"].ToString();
            }
        }

        public string HomeTeamName
        {
            get
            {
                return Get<string>(Data["homeTeamName"]);
            }
        }

        public string OpponentTeamName
        {
            get
            {
                return Get<string>(Data["opponentTeamName"]);
            }
        }

        public string HomePoints
        {
            get
            {
                return Data["homePoints"].ToString();
            }
        }

        public string OpponentPoints
        {
            get
            {
                return Data["opponentPoints"].ToString();
            }
        }

        public string BoutDate
        {
            get
            {
                return Data["boutDate"].ToString();
            }
        }

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

        public string Location
        {
            get
            {
                return Data["location"].ToString();
            }
        }

        public string EditorName
        {
            get
            {
                return Data["editorName"].ToString();
            }
        }

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
