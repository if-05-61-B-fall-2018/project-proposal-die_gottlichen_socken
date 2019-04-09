using System;
using System.IO;
using Newtonsoft.Json;

namespace Bot.Services.YouTube
{
    public class VideoData : IPlayable
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        [JsonProperty(PropertyName = "webpage_url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "display_id")]
        public string DisplayID { get; set; }

        [JsonProperty(PropertyName = "_filename")]
        public string FileName { get; set; }

        public string Requester { get; set; }

        public string DurationString => TimeSpan.FromSeconds(Duration).ToString();

        public string Uri => FileName;

        public int Speed { get; set; } = 48;

        public void OnPostPlay()
        {
            File.Delete(FileName);
        }
    }
}
