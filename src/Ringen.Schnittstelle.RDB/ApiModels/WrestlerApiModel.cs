using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class WrestlerApiModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("givenname")]
        public string Givenname { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("licenceCode")]
        public object LicenceCode { get; set; }

        [JsonProperty("authRating")]
        public string AuthRating { get; set; }

        [JsonProperty("clubCode")]
        public string ClubCode { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("passCode")]
        public string PassCode { get; set; }

        [JsonProperty("nationCode")]
        public string NationCode { get; set; }

        [JsonProperty("authCode")]
        public string AuthCode { get; set; }
    }

}
