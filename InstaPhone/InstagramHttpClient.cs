using System;
using System.Net.Http;
using InstaPhone.Resources;

namespace InstaPhone
{
    public class InstagramHttpClient : HttpClient, IInstagramClient
    {
        private const string UriPattern =
            "https://instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=token";

        private static string _clientId;
        private static Uri _responseUri;

        public InstagramHttpClient()
        {
            _clientId = AppResources.ClientId;
            _responseUri = new Uri(AppResources.RedirectUrl);
        }

        public static Uri AuthUri
        {
            get { return new Uri(string.Format(UriPattern, _clientId, _responseUri)); }
        }

        public bool ParseAuthResult(Uri authResult)
        {
            if (authResult == null) return false;
            return authResult.Fragment.Contains("access_token");
        }
    }
}