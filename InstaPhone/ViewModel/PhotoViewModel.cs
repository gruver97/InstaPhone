using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Windows.Storage.Streams;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InstaPhone.Model;

namespace InstaPhone.ViewModel
{
    public class PhotoViewModel : ViewModelBase
    {
        private Image _imageCollage;
        private const int PhotoCount = 4;
        public ICollection<Stream> _imageStreams;
        public PhotoViewModel()
        {
            ViewLoadedCommand = new RelayCommand<RoutedEventArgs>(ViewLoadedCommandExecute);
            PopularMedia = new ObservableCollection<PopularMedia>();
            _imageCollage = new Image();
            _imageStreams = new List<Stream>();
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
                    _imageStreams.Clear();
                    foreach (PopularMedia media in popularMedia)
                    {
                        var imageStream = await DownloadImage(media.InstagramImages.LowResolution.Uri);
                        _imageStreams.Add(imageStream);
                        media.InstagramImages.LowResolution.SetImage(imageStream);
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

        //async Task CreateCollage(IList<Stream> files)
        //{
        //    var sampleDataGroups = files;
        //    if (sampleDataGroups.Count() == 0) return;

        //    try
        //    {
        //        // Do a square-root of the number of images to get the
        //        // number of images on x and y axis
        //        int number = (int)Math.Ceiling(Math.Sqrt((double)files.Count));
        //        // Calculate the width of each small image in the collage
        //        int numberX = (int)(ImageCollage.ActualWidth / number);
        //        int numberY = (int)(ImageCollage.ActualHeight / number);
        //        // Initialize an empty WriteableBitmap.
        //        WriteableBitmap destination = new WriteableBitmap(numberX * number, numberY * number);
        //        int col = 0; // Current Column Position
        //        int row = 0; // Current Row Position
        //        destination.Clear(Colors.White); // Set the background color of the image to white
        //        WriteableBitmap bitmap; // Temporary bitmap into which the source
        //        // will be copied
        //        foreach (var file in files)
        //        {
        //            // Create RandomAccessStream reference from the current selected image
        //            RandomAccessStreamReference streamRef = RandomAccessStreamReference.CreateFromStream(file);
        //            int wid = 0;
        //            int hgt = 0;
        //            byte[] srcPixels;
        //            // Read the image file into a RandomAccessStream
        //            using (IRandomAccessStreamWithContentType fileStream = await streamRef.OpenReadAsync())
        //            {
        //                // Now that you have the raw bytes, create a Image Decoder
        //                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(fileStream);
        //                // Get the first frame from the decoder because we are picking an image
        //                BitmapFrame frame = await decoder.GetFrameAsync(0);
        //                // Convert the frame into pixels
        //                PixelDataProvider pixelProvider = await frame.GetPixelDataAsync();
        //                // Convert pixels into byte array
        //                srcPixels = pixelProvider.DetachPixelData();
        //                wid = (int)frame.PixelWidth;
        //                hgt = (int)frame.PixelHeight;
        //                // Create an in memory WriteableBitmap of the same size
        //                bitmap = new WriteableBitmap(wid, hgt);
        //                Stream pixelStream = bitmap.PixelBuffer.AsStream();
        //                pixelStream.Seek(0, SeekOrigin.Begin);
        //                // Push the pixels from the original file into the in-memory bitmap
        //                pixelStream.Write(srcPixels, 0, (int)srcPixels.Length);
        //                bitmap.Invalidate();

        //                if (row < number)
        //                {
        //                    // Resize the in-memory bitmap and Blit (paste) it at the correct tile
        //                    // position (row, col)
        //                    destination.Blit(new Rect(col * numberX, row * numberY, numberX, numberY),
        //                        bitmap.Resize(numberX, numberY, WriteableBitmapExtensions.Interpolation.Bilinear),
        //                        new Rect(0, 0, numberX, numberY));
        //                    col++;
        //                    if (col >= number)
        //                    {
        //                        row++;
        //                        col = 0;
        //                    }
        //                }
        //            }
        //        }

        //        ImageCollage.Source = destination;
        //        ((WriteableBitmap)ImageCollage.Source).Invalidate();
        //        //progressIndicator.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        //    }
        //    catch (Exception ex)
        //    {
        //        // TODO: Log Error, unable to render image
        //        throw;
        //    }
        //}

        public void CreateCollage()
        {
            try
            {
                int tempWidth = 0;   // Parameter for Translate.X 
                int tempHeight = 0;  // Parameter for Translate.Y 
                var images = new List<BitmapImage>();
                foreach (var imageStream in _imageStreams)
                {
                    var image = new BitmapImage();
                    image.SetSource(imageStream);
                    images.Add(image);
                }

                var finalImage = new BitmapImage();
                StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(@"Assets\white.jpg",
    UriKind.Relative));
                finalImage.SetSource(sri.Stream);

                var wbFinal = new WriteableBitmap(finalImage);
                //using (var mem = new MemoryStream())
                //{
                //    foreach (var bitmapImage in images)
                //    {
                //        var image = new Image();
                //        image.Height = bitmapImage.PixelHeight;
                //        image.Width = bitmapImage.PixelWidth;
                //        image.Source = bitmapImage;

                //        var transform = new TranslateTransform();
                //        transform.X = tempWidth;
                //        transform.Y = tempHeight;

                //        wbFinal.Render(image, transform);
                //        tempHeight += bitmapImage.PixelHeight;
                //    }
                var mem = new MemoryStream();
                    wbFinal.Invalidate();
                    wbFinal.SaveJpeg(mem, 2000, 306, 0, 100);
                    mem.Seek(0, SeekOrigin.Begin);

                    ImageCollage.Source = finalImage;
                    RaisePropertyChanged(() => ImageCollage);
                //}
            }
            catch (Exception)
            {
                
            }
        }

        public Image ImageCollage
        {
            get { return _imageCollage; }
            set
            {
                //if (Equals(_imageCollage,value)) return;
                _imageCollage = value;
                RaisePropertyChanged(() => ImageCollage);
            }
        }
    }
}