using System.Windows.Controls;
using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class InstagramImage
    {
        [JsonProperty("standard_resolution")]
        public ImageDescription LowResolution { get; set; }
    }
}