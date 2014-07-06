using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InstaPhone.Model;

namespace InstaPhone.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private const int PhotoCount = 4;

        public PhotoViewModel()
        {
            ViewLoadedCommand = new RelayCommand<RoutedEventArgs>(ViewLoadedCommandExecute);
            PopularMedia = new ObservableCollection<PopularMedia>();
        }

        public ObservableCollection<PopularMedia> PopularMedia { get; set; }

        public RelayCommand<RoutedEventArgs> ViewLoadedCommand { get; set; }

        private async Task<Stream> DownloadImage(Uri imageUri)
        {
            var client = new InstagramHttpClient();
            return await client.DownloadImage(imageUri);
        }

        private async Task GetMostPopularPhotos()
        {
            try
            {
                var instagramClient = new InstagramHttpClient();
                IEnumerable<PopularMedia> popularMedia = await instagramClient.GetPopularPhotosAsync(PhotoCount);
                if (popularMedia != null)
                {
                    PopularMedia.Clear();
                    foreach (PopularMedia media in popularMedia)
                    {
                        media.InstagramImages.LowResolution.SetImage(
                            await DownloadImage(media.InstagramImages.LowResolution.Uri));
                        PopularMedia.Add(media);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public async Task RefreshPopular()
        {
            await GetMostPopularPhotos();
        }

        private async void ViewLoadedCommandExecute(RoutedEventArgs eventArgs)
        {
            await GetMostPopularPhotos();
        }
    }
}