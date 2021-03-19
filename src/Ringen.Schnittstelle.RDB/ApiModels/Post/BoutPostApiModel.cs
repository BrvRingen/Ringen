using System;
using Newtonsoft.Json;
using Ringen.Schnittstelle.RDB.ApiModels.Post;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class BoutPostApiModel
    {
        [JsonProperty("weightClass")]
        public string WeightClass { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }



        [JsonProperty("homeWrestlerName")]
        public string HomeWrestlerName { get; set; }

        [JsonProperty("homeWrestlerGivenname")]
        public string HomeWrestlerGivenname { get; set; }

        [JsonProperty("homeWrestlerRating")]
        public string HomeWrestlerRating { get; set; }

        [JsonProperty("homeWrestlerPassCode")]
        public string HomeWrestlerPassCode { get; set; }

        [JsonProperty("homeWrestlerPoints")]
        public string HomeWrestlerPoints { get; set; }


        
        [JsonProperty("opponentWrestlerName")]
        public string OpponentWrestlerName { get; set; }

        [JsonProperty("opponentWrestlerGivenname")]
        public string OpponentWrestlerGivenname { get; set; }
        
        [JsonProperty("opponentWrestlerRating")]
        public string OpponentWrestlerRating { get; set; }

        [JsonProperty("opponentWrestlerPassCode")]
        public string OpponentWrestlerPassCode { get; set; }

        [JsonProperty("opponentWrestlerPoints")]
        public string OpponentWrestlerPoints { get; set; }


        
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("round1")]
        public string Round1 { get; set; }

        [JsonProperty("round2")]
        [Obsolete("Stand 2021: Wird nicht mehr gerungen, war damalige Regel, dass man 3 von 5 Runden gewinnen muss")]
        public string Round2 { get; set; }

        [JsonProperty("round3")]
        [Obsolete("Stand 2021: Wird nicht mehr gerungen, war damalige Regel, dass man 3 von 5 Runden gewinnen muss")]
        public string Round3 { get; set; }

        [JsonProperty("round4")]
        [Obsolete("Stand 2021: Wird nicht mehr gerungen, war damalige Regel, dass man 3 von 5 Runden gewinnen muss")]
        public string Round4 { get; set; }

        [JsonProperty("round5")]
        [Obsolete("Stand 2021: Wird nicht mehr gerungen, war damalige Regel, dass man 3 von 5 Runden gewinnen muss")]
        public string Round5 { get; set; }
        
        [JsonProperty("annotation")]
        public AnnotationsPostApiModel Annotations { get; set; }
    }
}
