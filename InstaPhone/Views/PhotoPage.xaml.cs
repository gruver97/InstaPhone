using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using InstaPhone.Model;
using InstaPhone.ViewModel;
using Microsoft.Phone.Controls;

namespace InstaPhone.Views
{
    public partial class PhotoPage : PhoneApplicationPage
    {
        public PhotoPage()
        {
            InitializeComponent();
        }

        private async void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            if (DataContext is PhotoViewModel)
            {
                var viewModel = DataContext as PhotoViewModel;
                await viewModel.RefreshPopular();
            }
        }

        private void CollageButton_OnClick(object sender, EventArgs e)
        {
            if (DataContext is PhotoViewModel)
            {
                var viewModel = DataContext as PhotoViewModel;
                viewModel.MakeCollage(ref ImageCollage);
            }
        }
    }
}