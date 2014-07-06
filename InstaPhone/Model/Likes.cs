using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class Likes
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}