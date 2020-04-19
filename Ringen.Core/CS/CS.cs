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
    public class CS : ExtendedNotifyPropertyChangedUserControl, IExplorerItem
    {
        public CS()
        {
            Value = "Mannschaftskämpfe";
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
                    Helpers.Async.RunSync(async () =>
                    {
                        var AssetResponse = await REST.Client().GetAsync($"/BrvApi/v1/cs/");

                        if (AssetResponse.IsSuccessStatusCode)
                        {
                            var result = AssetResponse.Content.ReadAsStringAsync().Result;
                            foreach (var SeasonData in (JArray)JsonConvert.DeserializeObject(result))
                            {
                                seasons.Add(new Season((JObject)SeasonData));
                            }
                        }
                    });
                }
                    
                return seasons; }
            set { seasons = value; }
        }

    }
}
