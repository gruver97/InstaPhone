using System;
using GalaSoft.MvvmLight;

namespace InstaPhone.ViewModel
{
    public class AuthViewModel : ViewModelBase
    {
        public AuthViewModel()
        {
            SourceUri = InstagramHttpClient.AuthUri;
        }

        public Uri SourceUri { get; set; }
    }
}