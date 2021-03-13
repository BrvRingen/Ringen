using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class BoutdayApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("orgBoutday")]
        public string OrgBoutday { get; set; }

        [JsonProperty("boutDate")]
        public string BoutDate { get; set; }

        [JsonProperty("any")]
        public string Any { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }
    }

}
