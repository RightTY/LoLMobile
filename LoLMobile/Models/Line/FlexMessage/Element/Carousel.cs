using Newtonsoft.Json;

namespace LoLMobile.Models.Line.FlexMessage.Element
{
    public class Carousel
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("contents")]
        public List<Bubble> Contents { get; set; }
    }
}
