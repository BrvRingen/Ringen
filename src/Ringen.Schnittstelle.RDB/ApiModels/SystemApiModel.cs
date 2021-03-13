using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class SystemApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("getWrestlerBy")]
        public string GetWrestlerBy { get; set; }
    }

}
