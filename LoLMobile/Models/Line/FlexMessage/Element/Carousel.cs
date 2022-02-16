using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Carousel
    {
        [JsonPropertyName("type")] 
        public string Type { get; set; } = "carousel";

        [JsonPropertyName("contents")]
        public List<Bubble> Contents { get; set; } = new List<Bubble>();
    }
}
