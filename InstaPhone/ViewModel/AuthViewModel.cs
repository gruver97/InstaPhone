using System;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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