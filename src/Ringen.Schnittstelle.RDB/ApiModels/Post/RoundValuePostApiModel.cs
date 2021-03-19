using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels.Post
{
    internal class RoundValuePostApiModel
    {
        [JsonProperty("1")]
        public string Round1Value { get; set; }

        public RoundValuePostApiModel(string round1Value)
        {
            Round1Value = round1Value;
        }
    }
}