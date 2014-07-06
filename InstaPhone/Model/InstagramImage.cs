using System.Windows.Controls;
using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class InstagramImage
    {
        [JsonProperty("low_resolution")]
        public ImageDescription LowResolution { get; set; }
    }
}