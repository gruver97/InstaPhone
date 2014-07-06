using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InstaPhone.Model;
using InstaPhone.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InstaPhone
{
    public class InstagramHttpClient : HttpClient, IInstagramClient
    {
        private static readonly string ClientId = AppResources.ClientId;

        private static readonly string ResponseUri = AppResources.RedirectUrl;

        private static string _accessToken;

        public static Uri AuthUri
        {
            get { return new Uri(string.Format(AppResources.AuthUrl, ClientId, ResponseUri)); }
        }

        public bool ParseAuthResult(Uri authResult)
        {
            if (authResult == null) return false;
            if (authResult.Fragment.Contains("access_token"))
            {
                _accessToken = authResult.Fragment.Remove(0, "#access_token=".Length);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<PopularMedia>> GetPopularPhotosAsync(int count)
        {
            if (count < 1) throw new ArgumentException("Count must be positive.", "count");
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.CancelPendingRequests();
            HttpResponseMessage response =
                await GetAsync(new Uri(string.Format(AppResources.PopularMediaUrl, _accessToken)));
            if (response.IsSuccessStatusCode)
            {
                string mediaJsonString = await response.Content.ReadAsStringAsync();
                JToken jobject = JObject.Parse(mediaJsonString)["data"];
                var popularMedia = JsonConvert.DeserializeObject<List<PopularMedia>>(jobject.ToString());
                return
                    popularMedia.Where(media => media.MediaType == "image")
                        .OrderByDescending(media => media.Likes.Count)
                        .Take(count).ToList();
            }
            return null;
        }

        public async Task<byte[]> DownloadImage(Uri imageUri)
        {
            if (imageUri == null) throw new ArgumentNullException("imageUri");
            CancelPendingRequests();
            MaxResponseContentBufferSize = 256000;
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
            HttpResponseMessage response =
                await GetAsync(imageUri);
            if (response.IsSuccessStatusCode)
            {
                var mediaJsonString = await response.Content.ReadAsStreamAsync();
                return null;
            }
            return null;
        }
    }
}