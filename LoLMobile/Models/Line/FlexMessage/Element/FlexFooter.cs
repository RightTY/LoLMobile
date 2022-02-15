using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class FlexFooter
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("spacing")]
        public string Spacing { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }

        [JsonProperty("flex")]
        public long Flex { get; set; }
    }
}
