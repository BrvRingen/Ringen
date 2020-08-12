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
    public class CS : ExtendedNotifyPropertyChanged, IExplorerItem
    {
        public IExplorerItem ExplorerParent { get; }

        public CS()
        {
            Value = "Mannschaftskämpfe";
            ExplorerParent = null;
        }

        private string value;

        public string Value
        {
            get
            {
                return value;
            }
            set { this.value = value; }
        }

        private List<Season> seasons;

        public List<Season> Children
        {
            get {
                if (seasons == null)
                {
                    seasons = new List<Season>();
                    Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/Api/v1/cs/");

                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var SeasonData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                seasons.Add(new Season((JObject)SeasonData, this));
                            }
                        }
                    });
                }
                    
                return seasons; }
            set { seasons = value; }
        }

    }
}
