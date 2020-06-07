using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ringen.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ringen.Core.CS
{
    public class Season : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
    {
        private JObject Data { get; set; }
        public IExplorerItem Parent { get; }

        public Season(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.Parent = Parent;
        }

        public string Value
        {
            get
            {
                return SaisonId;
            }
        }

        public string SaisonId
        {
            get
            {
                return Data["saisonId"].ToString();
            }
        }


        private List<Table> tables;

        public List<Table> Children
        {
            get
            {
                if (tables == null)
                {
                    tables = new List<Table>();
                    Helpers.Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/BrvApi/v1/cs/?saisonId={SaisonId}");

                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var TableData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                tables.Add(new Table((JObject)TableData, this));
                            }
                        }
                    });
                }

                return tables;
            }
            set { tables = value; }
        }
    }
}
