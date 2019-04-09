using Newtonsoft.Json;

namespace Bot.Services.YouTube
{
    public class StreamFormat
    {
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "acodec")]
        public string Codec { get; set; }
    }
}
