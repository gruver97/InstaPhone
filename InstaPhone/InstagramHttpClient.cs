using System;
using System.Collections.Generic;
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

        public InstagramHttpClient()
        {
            //_accessToken = string.Empty;
        }

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

        public async Task GetPopularPhotosAsync()
        {
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response =
                await GetAsync(new Uri(string.Format(AppResources.PopularMediaUrl, _accessToken)));
            if (response.IsSuccessStatusCode)
            {
                string mediaJsonString = await response.Content.ReadAsStringAsync();
                var jobject = JObject.Parse(mediaJsonString)["data"];
                var popularMedias = JsonConvert.DeserializeObject<List<PopularMedia>>(jobject.ToString());
                //return siteList;
            }
            ;
        }
    }
}