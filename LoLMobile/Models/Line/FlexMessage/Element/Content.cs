using System.Text.Json.Serialization;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Content
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("layout")]
        public string? Layout { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("flex")]
        public int? Flex { get; set; }

        [JsonPropertyName("spacing")]
        public string? Spacing { get; set; }

        [JsonPropertyName("margin")]
        public string? Margin { get; set; }

        [JsonPropertyName("weight")]
        public string? Weight { get; set; }

        [JsonPropertyName("height")]
        public string? Height { get; set; }

        [JsonPropertyName("size")]
        public string? Size { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }

        [JsonPropertyName("style")]
        public string? Style { get; set; }

        [JsonPropertyName("align")]
        public string? Align { get; set; }

        [JsonPropertyName("wrap")]
        public bool? Wrap { get; set; }

        [JsonPropertyName("action")]
        public FlexAction? Action { get; set; }

        [JsonPropertyName("contents")]
        public List<Content>? Contents { get; set; }
    }
}
