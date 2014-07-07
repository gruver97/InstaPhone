using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
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
            //if (DataContext is PhotoViewModel)
            //{
            //    var viewModel = DataContext as PhotoViewModel;
            //    viewModel.CreateCollage();
            //}
            CreateCollage();
        }

        public void CreateCollage()
        {
            try
            {
                int width = 0; // Final width.
                int height = 0; // Final height.
                var imageStreams = new List<Stream>(); 
                var viewModel  = new PhotoViewModel();
                if (DataContext is PhotoViewModel)
                {
                    viewModel = DataContext as PhotoViewModel;
                    imageStreams.AddRange(viewModel._imageStreams);
                }

                int tempWidth = 0; // Parameter for Translate.X 
                int tempHeight = 0; // Parameter for Translate.Y 
                var images = new List<BitmapImage>();

                foreach (var media in viewModel.PopularMedia)
                {
                    var image = media.InstagramImages.LowResolution.Image;

                    WriteableBitmap wb = new WriteableBitmap(image);

                    // Update the size of the final bitmap
                    width = wb.PixelWidth > width ? wb.PixelWidth : width;
                    height = wb.PixelHeight > height ? wb.PixelHeight : height;

                    images.Add(image);
                }

                var finalImage = new BitmapImage();
                StreamResourceInfo sri = System.Windows.Application.GetResourceStream(new Uri(@"Assets\white.jpg",
                    UriKind.Relative));
                finalImage.SetSource(sri.Stream);

                var wbFinal = new WriteableBitmap(finalImage);
                using (var mem = new MemoryStream())
                {
                    foreach (var bitmapImage in images)
                    {
                        var image = new Image();
                        image.Height = bitmapImage.PixelHeight;
                        image.Width = bitmapImage.PixelWidth;
                        image.Source = bitmapImage;

                        var transform = new TranslateTransform();
                        transform.X = tempWidth;
                        transform.Y = tempHeight;

                        wbFinal.Render(image, transform);
                        tempWidth += bitmapImage.PixelWidth;
                    }
                    wbFinal.Invalidate();
                    wbFinal.SaveJpeg(mem, width, height, 0, 100);
                    mem.Seek(0, SeekOrigin.Begin);

                    ImageCollage.Source = wbFinal;
                    
                }
            }
            catch (Exception)
            {

            }
        }
    }
}