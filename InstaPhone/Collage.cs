using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace InstaPhone
{
    public class Collage : ICollage
    {
        public void MakeCollage(ref Image collage, int width, int height, IEnumerable<BitmapImage> imagesParam)
        {
            try
            {
                var imageStreams = new List<Stream>();

                int tempWidth = 0; // Parameter for Translate.X 
                int tempHeight = 0; // Parameter for Translate.Y 
                var images = new List<BitmapImage>();

                var finalImage = new BitmapImage();
                StreamResourceInfo sri = Application.GetResourceStream(new Uri(@"Assets\white.jpg", UriKind.Relative));
                finalImage.SetSource(sri.Stream);

                var wbFinal = new WriteableBitmap(finalImage);
                using (var mem = new MemoryStream())
                {
                    var bitmapImages = imagesParam as IList<BitmapImage> ?? imagesParam.ToList();
                    foreach (BitmapImage bitmapImage in bitmapImages.Take(2))
                    {
                        var wb = new WriteableBitmap(bitmapImage);
                        wb = wb.Resize(50, 50, WriteableBitmapExtensions.Interpolation.Bilinear);
                        var image = new Image();
                        //image.Height = bitmapImage.PixelHeight;
                        //image.Width = bitmapImage.PixelWidth;
                        image.Source = wb;

                        var transform = new TranslateTransform();
                        transform.X = tempWidth;
                        transform.Y = tempHeight;
                        wbFinal.Render(image, transform);
                        tempWidth += wb.PixelWidth;
                    }
                    tempWidth = 0;
                    tempHeight = 306;
                    foreach (BitmapImage bitmapImage in bitmapImages.Skip(2))
                    {
                        var wb = new WriteableBitmap(bitmapImage);
                        wb = wb.Resize(50, 50, WriteableBitmapExtensions.Interpolation.Bilinear);
                        var image = new Image();
                        //image.Height = bitmapImage.PixelHeight;
                        //image.Width = bitmapImage.PixelWidth;
                        image.Source = wb;

                        var transform = new TranslateTransform();
                        transform.X = tempWidth;
                        transform.Y = tempHeight;
                        wbFinal.Render(image, transform);
                        tempHeight += wb.PixelHeight;
                    }
                    wbFinal.Invalidate();
                    wbFinal.SaveJpeg(mem, width, height, 0, 100);
                    mem.Seek(0, SeekOrigin.Begin);

                    collage.Source = wbFinal;

                };
            }

            catch (Exception)
            {
            }


        }
    }
}