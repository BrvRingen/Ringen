using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class AnnotationApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("competitionId")]
        public string CompetitionId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

    }
}