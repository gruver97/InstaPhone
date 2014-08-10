using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InstaPhone.Model;

namespace InstaPhone.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private bool _isLoading;
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
            IsLoading = true;
            try
            {
                var instagramClient = new InstagramHttpClient();
                IEnumerable<PopularMedia> popularMedia = await instagramClient.GetPopularPhotosAsync(PhotoCount);
                if (popularMedia != null)
                {
                    PopularMedia.Clear();
                    foreach (PopularMedia media in popularMedia)
                    {
                        Stream imageStream = await DownloadImage(media.InstagramImages.LowResolution.Uri);
                        media.InstagramImages.LowResolution.SetImage(imageStream);
                        PopularMedia.Add(media);
                    }
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void MakeCollage(ref Image collage)
        {
            var collageWorker = new Collage();
            collageWorker.MakeCollage(ref collage, 420, 420,
                PopularMedia.Select(media => media.InstagramImages.LowResolution.Image));
        }

        public async Task RefreshPopular()
        {
            await GetMostPopularPhotos();
        }

        private async void ViewLoadedCommandExecute(RoutedEventArgs eventArgs)
        {
            await GetMostPopularPhotos();
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (Equals(_isLoading, value)) return;
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
    }
}