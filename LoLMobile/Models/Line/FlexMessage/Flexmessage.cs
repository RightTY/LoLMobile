using LoLMobile.Models.Line.FlexMessage.Element;
using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage
{
    public class Flexmessage
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("altText")]
        public string AltText { get; set; }
        [JsonProperty("contents")]
        public Carousel Contents { get; set; }
    }
}
