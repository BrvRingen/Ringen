using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class SaisonApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        /// <summary>
        /// Ligenleiter
        /// </summary>
        [JsonProperty("controllerName")]
        public string ControllerName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("noOfBoutDays")]
        public string NoOfBoutDays { get; set; }

        [JsonProperty("teamIdSize")]
        public string TeamIdSize { get; set; }

        [JsonProperty("noOfSystems")]
        public string NoOfSystems { get; set; }

        [JsonProperty("ruleType")]
        public string RuleType { get; set; }
    }
}
