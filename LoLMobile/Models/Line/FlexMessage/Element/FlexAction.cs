using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class FlexAction
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("uri")]
        public Uri? Uri { get; set; }


        [JsonPropertyName("data")]
        public string Data { get; set; } = string.Empty;
    }
}
