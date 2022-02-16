using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class FlexFooter
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("layout")]
        public string? Layout { get; set; }

        [JsonPropertyName("spacing")]
        public string? Spacing { get; set; }

        [JsonPropertyName("flex")]
        public string? Flex { get; set; }

        [JsonPropertyName("contents")]
        public List<Content>? Contents { get; set; }


    }
}
