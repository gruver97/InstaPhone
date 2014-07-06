using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InstaPhone.Model;

namespace InstaPhone.ViewModel
{
    public class PhotoViewModel:ViewModelBase
    {
        private IInstagramClient _instagramClient;
        private const int PhotoCount = 4;
        public PhotoViewModel()
        {
            _instagramClient = new InstagramHttpClient();
            ViewLoadedCommand = new RelayCommand<RoutedEventArgs>(GetMostPopularPhotos);
            PopularMedia = new ObservableCollection<PopularMedia>();
        }

        public ObservableCollection<PopularMedia> PopularMedia { get; set; }
        private async void GetMostPopularPhotos(RoutedEventArgs e)
        {
            try
            {
                var popularMedia = await _instagramClient.GetPopularPhotosAsync(PhotoCount);
                if (popularMedia != null)
                {
                    PopularMedia.Clear();
                    foreach (var media in popularMedia)
                    {
                        await DownloadImage(media.InstagramImages.LowResolution.Uri);
                        PopularMedia.Add(media);
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        private async Task DownloadImage(Uri imageUri)
        {
            await _instagramClient.DownloadImage(imageUri);
        }

        public RelayCommand<RoutedEventArgs> ViewLoadedCommand { get; set; }
    }
}
