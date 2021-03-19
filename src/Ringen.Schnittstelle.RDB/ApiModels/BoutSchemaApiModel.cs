using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class BoutSchemaApiModel
    {
        [JsonProperty("boutSchemeId")]
        public string BoutSchemeId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonProperty("styleA")]
        public string StyleA { get; set; }

        [JsonProperty("styleB")]
        public string StyleB { get; set; }

        [JsonProperty("weightClass")]
        public string WeightClass { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }
    }

}
