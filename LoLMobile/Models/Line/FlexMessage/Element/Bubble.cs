using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Bubble
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("body")]
        public FlexBody Body { get; set; }

        [JsonProperty("footer")]
        public FlexFooter Footer { get; set; }
    }
}
