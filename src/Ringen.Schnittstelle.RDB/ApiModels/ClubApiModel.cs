using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class ClubApiModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("clubCode")]
        public string ClubCode { get; set; }

        [JsonProperty("uAuthClubCode")]
        public string UAuthClubCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("nationCode")]
        public string NationCode { get; set; }

        [JsonProperty("fedCode")]
        public string FedCode { get; set; }

        [JsonProperty("organisation")]
        public string Organisation { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("founded")]
        public string Founded { get; set; }

        [JsonProperty("registered")]
        public string Registered { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("flags")]
        public string Flags { get; set; }

        [JsonProperty("departmentList")]
        public string DepartmentList { get; set; }

        [JsonProperty("noSport")]
        public string NoSport { get; set; }

        //[JsonProperty("_gymMap")]
        //public object[] _gymMap { get; set; }

        //[JsonProperty("_locationMap")]
        //public object[] _locationMap { get; set; }
    }

}
