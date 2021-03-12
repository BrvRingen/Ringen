using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class LigaApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("ligaId")]
        public string LigaId { get; set; }

        [JsonProperty("fid")]
        public string Fid { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("tableId")]
        public string TableId { get; set; }

        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("indicator")]
        public string Indicator { get; set; }

        [JsonProperty("extId")]
        public string ExtId { get; set; }

        [JsonProperty("boutSchemeId")]
        public string BoutSchemeId { get; set; }

        [JsonProperty("wrestlerMissed")]
        public string WrestlerMissed { get; set; }

        [JsonProperty("noOfBoutDays")]
        public string NoOfBoutDays { get; set; }

        [JsonProperty("sumOfLigaBoutDays")]
        public string SumOfLigaBoutDays { get; set; }

        [JsonProperty("lastUpdate")]
        public string LastUpdate { get; set; }

        [JsonProperty("moveUpRank")]
        public string MoveUpRank { get; set; }

        [JsonProperty("dropDownRank")]
        public string DropDownRank { get; set; }

        [JsonProperty("cntl")]
        public string Cntl { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}