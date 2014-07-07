using System;
using System.Threading.Tasks;
using System.Windows;
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
            await LoadeAndCollageAsync();
        }

        private async Task LoadeAndCollageAsync()
        {
            if (DataContext is PhotoViewModel)
            {
                var viewModel = DataContext as PhotoViewModel;
                await viewModel.RefreshPopular();
                viewModel.MakeCollage(ref ImageCollage);
            }
        }

        private async void PhotoPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadeAndCollageAsync();
        }
    }
}