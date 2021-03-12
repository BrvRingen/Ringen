using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels
{
    internal class PlaceApiModel
    {
        [JsonProperty("saisonId")]
        public string SaisonId { get; set; }

        [JsonProperty("ligaId")]
        public string LigaId { get; set; }

        [JsonProperty("tableId")]
        public string TableId { get; set; }

        [JsonProperty("teamId")]
        public string TeamId { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("segment")]
        public string Segment { get; set; }

        [JsonProperty("segmentOrder")]
        public string SegmentOrder { get; set; }

        [JsonProperty("wonMatch")]
        public string WonMatch { get; set; }

        [JsonProperty("lostMatch")]
        public string LostMatch { get; set; }

        [JsonProperty("tieMatch")]
        public string TieMatch { get; set; }

        [JsonProperty("wonBPoints")]
        public string WonBPoints { get; set; }

        [JsonProperty("lostBPoints")]
        public string LostBPoints { get; set; }

        [JsonProperty("wonTPoints")]
        public string WonTPoints { get; set; }

        [JsonProperty("lostTPoints")]
        public string LostTPoints { get; set; }

        [JsonProperty("wonMatchOffset")]
        public string WonMatchOffset { get; set; }

        [JsonProperty("lostMatchOffset")]
        public string LostMatchOffset { get; set; }

        [JsonProperty("tieMatchOffset")]
        public string TieMatchOffset { get; set; }

        [JsonProperty("wonBPointsOffset")]
        public string WonBPointsOffset { get; set; }

        [JsonProperty("lostBPointsOffset")]
        public string LostBPointsOffset { get; set; }

        [JsonProperty("wonTPointsOffset")]
        public string WonTPointsOffset { get; set; }

        [JsonProperty("lostTPointsOffset")]
        public string LostTPointsOffset { get; set; }

        [JsonProperty("flags")]
        public string Flags { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}