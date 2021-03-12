using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class BoutApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("competitionId")]
        public string CompetitionId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("weightClass")]
        public string WeightClass { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("homeWrestlerName")]
        public string HomeWrestlerName { get; set; }

        [JsonProperty("homeWrestlerGivenname")]
        public string HomeWrestlerGivenname { get; set; }

        [JsonProperty("opponentWrestlerName")]
        public string OpponentWrestlerName { get; set; }

        [JsonProperty("opponentWrestlerGivenname")]
        public string OpponentWrestlerGivenname { get; set; }

        [JsonProperty("homeWrestlerPoints")]
        public string HomeWrestlerPoints { get; set; }

        [JsonProperty("homeWrestlerFlags")]
        public string HomeWrestlerFlags { get; set; }

        [JsonProperty("opponentWrestlerPoints")]
        public string OpponentWrestlerPoints { get; set; }

        [JsonProperty("opponentWrestlerFlags")]
        public string OpponentWrestlerFlags { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("round1")]
        public string Round1 { get; set; }

        [JsonProperty("round2")]
        public string Round2 { get; set; }

        [JsonProperty("round3")]
        public string Round3 { get; set; }

        [JsonProperty("round4")]
        public string Round4 { get; set; }

        [JsonProperty("round5")]
        public string Round5 { get; set; }

        [JsonProperty("homeWrestlerStatus")]
        public string HomeWrestlerStatus { get; set; }

        [JsonProperty("opponentWrestlerStatus")]
        public string OpponentWrestlerStatus { get; set; }
        
        [JsonProperty("homeWrestlerPassCode")]
        public string HomeWrestlerPassCode { get; set; }

        [JsonProperty("homeWrestlerSaisonLicenceId")]
        public string HomeWrestlerSaisonLicenceId { get; set; }

        [JsonProperty("opponentWrestlerPassCode")]
        public string OpponentWrestlerPassCode { get; set; }

        [JsonProperty("opponentWrestlerSaisonLicenceId")]
        public string OpponentWrestlerSaisonLicenceId { get; set; }
        
        [JsonIgnore]
        public List<AnnotationApiModel> Annotations { get; set; }
    }
}
