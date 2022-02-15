using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class FlexBody
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("contents")]
        public List<Content> Contents { get; set; }
    }
}
