using System;
using System.IO;
using System.Windows.Media.Imaging;
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

        public BitmapImage Image { get; set; }

        public void SetImage(byte[] bytes)
        {
            var memoryStream = new MemoryStream(bytes);
            var bitmapImage = new BitmapImage();
            bitmapImage.SetSource(memoryStream);
        }
    }
}