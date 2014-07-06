using System;
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
    }
}