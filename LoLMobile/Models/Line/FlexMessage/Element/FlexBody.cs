using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class FlexBody
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("layout")]
        public string Layout { get; set; } = string.Empty;

        [JsonPropertyName("contents")]
        public List<Content> Contents { get; set; } = new List<Content>();
    }
}
