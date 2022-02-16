using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Bubble
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = "bubble";

        [JsonPropertyName("body")]
        public FlexBody? Body { get; set; }

        [JsonPropertyName("footer")]
        public FlexFooter? Footer { get; set; }
    }
}
