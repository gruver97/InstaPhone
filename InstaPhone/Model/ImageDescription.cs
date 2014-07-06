using System;
using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class ImageDescription
    {
        [JsonProperty("url")]
        public Uri Uri { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}