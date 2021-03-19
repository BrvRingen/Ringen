using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class CompetitionPostApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("competitionId")]
        public string CompetitionId { get; set; }

        
        [JsonProperty("homePoints")]
        public string HomePoints { get; set; }

        [JsonProperty("opponentPoints")]
        public string OpponentPoints { get; set; }
        
        [JsonProperty("audience")]
        public string Audience { get; set; }
        
        /// <summary>
        /// Bemerkung Protokoll
        /// </summary>
        [JsonProperty("editorComment")]
        public string EditorComment { get; set; }

        [JsonProperty("refereeName")]
        public string RefereeName { get; set; }

        [JsonProperty("refereeGivenname")]
        public string RefereeGivenname { get; set; }
        
        /// <summary>
        /// Format HH:MM:SS
        /// </summary>
        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        /// <summary>
        /// Format HH:MM:SS
        /// </summary>
        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("_boutList")]
        public List<BoutPostApiModel> BoutList { get; set; }
    }
}
