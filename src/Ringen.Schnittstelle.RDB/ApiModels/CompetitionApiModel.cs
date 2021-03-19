using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class CompetitionApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("competitionId")]
        public string CompetitionId { get; set; }

        [JsonProperty("ligaId")]
        public string LigaId { get; set; }

        [JsonProperty("tableId")]
        public string TableId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("scheme")]
        public string Scheme { get; set; }

        [JsonProperty("manualInput")]
        public string ManualInput { get; set; }

        [JsonProperty("inTable")]
        public string InTable { get; set; }

        [JsonProperty("inStatistics")]
        public string InStatistics { get; set; }

        [JsonProperty("invalidated")]
        public string Invalidated { get; set; }

        [JsonProperty("planned")]
        public string Planned { get; set; }

        [JsonProperty("boutday")]
        public string Boutday { get; set; }

        [JsonProperty("homeTeamName")]
        public string HomeTeamName { get; set; }

        [JsonProperty("opponentTeamName")]
        public string OpponentTeamName { get; set; }

        [JsonProperty("homePoints")]
        public string HomePoints { get; set; }

        [JsonProperty("opponentPoints")]
        public string OpponentPoints { get; set; }

        [JsonProperty("boutDate")]
        public string BoutDate { get; set; }

        [JsonProperty("scaleTime")]
        public string ScaleTime { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("editorName")]
        public string EditorName { get; set; }

        [JsonProperty("editorComment")]
        public string EditorComment { get; set; }

        [JsonProperty("refereeName")]
        public string RefereeName { get; set; }

        [JsonProperty("refereeGivenname")]
        public string RefereeGivenname { get; set; }

        [JsonProperty("lastModified")]
        public string LastModified { get; set; }

        [JsonProperty("editedAt")]
        public string EditedAt { get; set; }

        [JsonProperty("editedBy")]
        public string EditedBy { get; set; }

        [JsonProperty("editedIpAddr")]
        public string EditedIpAddr { get; set; }

        [JsonProperty("controlledAt")]
        public string ControlledAt { get; set; }

        [JsonProperty("controlledBy")]
        public string ControlledBy { get; set; }

        [JsonProperty("controllerComment")]
        public string ControllerComment { get; set; }

        [JsonProperty("validatedHomePoints")]
        public string ValidatedHomePoints { get; set; }

        [JsonProperty("validatedOpponentPoints")]
        public string ValidatedOpponentPoints { get; set; }

        [JsonProperty("decision")]
        public string Decision { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("uploadedAt")]
        public object UploadedAt { get; set; }
    }

}
