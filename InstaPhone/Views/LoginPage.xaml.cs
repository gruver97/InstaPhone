using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace InstaPhone.Views
{
    public partial class LoginPage : PhoneApplicationPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void WebBrowser_OnNavigating(object sender, NavigatingEventArgs e)
        {
            var instagramClient = new InstagramHttpClient();
            if (!instagramClient.ParseAuthResult(e.Uri)) return;
            e.Cancel = true;
            NavigationService.Navigate(new Uri("/Views/PhotoPage.xaml", UriKind.Relative));
        }

        private void WebBrowser_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            MessageBox.Show("Повторите попытку позднее");
            e.Handled = true;
        }
    }
}