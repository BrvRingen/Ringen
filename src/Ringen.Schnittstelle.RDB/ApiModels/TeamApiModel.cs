using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class TeamApiModel
    {
        [JsonProperty("oid")]
        public string Oid { get; set; }
        
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("teamShortName")]
        public string TeamShortName { get; set; }

        [JsonProperty("clubCode")]
        public string ClubCode { get; set; }

        [JsonProperty("clubName")]
        public string ClubName { get; set; }
    }
}