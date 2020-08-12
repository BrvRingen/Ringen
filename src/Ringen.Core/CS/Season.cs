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
    public class Season : ExtendedNotifyPropertyChanged, IExplorerItem
    {
        private JObject Data { get; set; }
        public IExplorerItem ExplorerParent { get; }

        public Season(JObject Data, IExplorerItem Parent)
        {
            this.Data = Data;
            this.ExplorerParent = Parent;
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
                    Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/?saisonId={SaisonId}");

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
