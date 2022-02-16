using LoLMobile.Models.Line.FlexMessage.Element;
using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage
{
    public class Flexmessage
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("altText")]
        public string AltText { get; set; } = string.Empty;
        [JsonPropertyName("contents")]
        public Carousel Contents { get; set; } = new Carousel();
    }
}
