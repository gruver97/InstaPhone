using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstaPhone.Model
{
    public class InstagramImage
    {
        [JsonProperty("low_resolution")]
        public ImageDescription LowResolution { get; set; }
    }
}
