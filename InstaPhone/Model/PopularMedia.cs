using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class PopularMedia
    {
        [JsonProperty("images")]
        public InstagramImage InstagramImages { get; set; }

        [JsonProperty("likes")]
        public Likes Likes { get; set; }

        [JsonProperty("type")]
        public string MediaType { get; set; } 
    }
}