using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("flex")]
        public string Flex { get; set; }

        [JsonProperty("spacing")]
        public string Spacing { get; set; }

        [JsonProperty("margin")]
        public string Margin { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("align")]
        public string Align { get; set; }

        [JsonProperty("wrap")]
        public bool? Wrap { get; set; }

        [JsonProperty("action")]
        public FlexAction Action { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }
    }
}
