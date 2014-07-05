using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InstaPhone
{
    public class InstagramHttpClient:HttpClient, IInstagramClient
    {
        private readonly string _clientId;
        private readonly Uri _responseUri;

        public InstagramHttpClient(string clientId, Uri responseUri)
        {
            _clientId = clientId;
            _responseUri = responseUri;
        }

        public async Task<bool> Login()
        {
            return false;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
