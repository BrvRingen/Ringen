using Nancy.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Ringen.Core.CS
{
    public class Table : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
    {
        private JObject Data { get; set; }
        public IExplorerItem Parent { get; }

        public Table(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.Parent = Parent;
        }

        public string Value
        {
            get
            {
                return $"{Data["ligaId"].ToString()} {Data["tableId"].ToString()}".Trim();
            }
        }

        public string SaisonId
        {
            get
            {
                return Data["saisonId"].ToString();
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

        private List<Competition> competitions;

        public List<Competition> Children
        {
            get
            {
                if (competitions == null)
                {
                    competitions = new List<Competition>();
                    Helpers.Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/BrvApi/v1/cs/?saisonId={SaisonId}&ligaId={HttpUtility.UrlEncode(LigaId, Encoding.GetEncoding("iso-8859-1"))}&tableId={HttpUtility.UrlEncode(TableId, Encoding.GetEncoding("iso-8859-1"))}");

                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var CompetitionData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                competitions.Add(new Competition((JObject)CompetitionData, this));
                            }
                        }
                    });
                }

                return competitions;
            }
            set { competitions = value; }
        }

    }
}
