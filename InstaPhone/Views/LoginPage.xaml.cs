using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

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
            if (instagramClient.ParseAuthResult(e.Uri))
            {
                e.Cancel = true;
                this.NavigationService.Navigate(new Uri("/Views/PhotoPage.xaml", UriKind.Relative));
            }
        }

        private void WebBrowser_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            MessageBox.Show("Повторите попытку позднее");
            e.Handled = true;
        }
    }
}