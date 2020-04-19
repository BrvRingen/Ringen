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
    public class Competition : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
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

        public Competition(JObject Data)
        {
            this.Data = Data;
        }

        public string Value
        {
            get
            {
                return $"{Data["homeTeamName"].ToString()} - {Data["opponentTeamName"].ToString()}";
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
                    Helpers.Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/BrvApi/v1/cs/?saisonId={SaisonId}&competitionId={CompetitionId}");

                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var BoutData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                bouts.Add(new Bout((JObject)BoutData));
                            }
                        }
                    });
                }

                return bouts;
            }
            set { bouts = value; }
        }

        public async Task SendAsync()
        {
            var sUpdate = JsonConvert.SerializeObject(Data);

            var UpdateResponse = await REST.Client().PutAsync($"/BrvApi/v1/cs/?saisonId={SaisonId}&competitionId={CompetitionId}", new StringContent(sUpdate, Encoding.UTF8, "application/json"));

            if (UpdateResponse.IsSuccessStatusCode)
            {
                var result = UpdateResponse.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
