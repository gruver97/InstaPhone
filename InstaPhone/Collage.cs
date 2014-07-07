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
        public void MakeCollage(ref Image collage, int collageWidth, int collageHeight, IEnumerable<BitmapImage> imagesParam)
        {
            try
            {
                var bitmapImages = imagesParam as IList<BitmapImage> ?? imagesParam.ToList();
                var number = (int) Math.Ceiling(Math.Sqrt(bitmapImages.Count));
                var width = (int) (collageWidth/number);
                var height = (int)(collageHeight / number);
                
                int tempWidth = 0; // Parameter for Translate.X 
                int tempHeight = 0; // Parameter for Translate.Y

                var wbFinal = new WriteableBitmap(collageWidth, collageHeight);

                using (var mem = new MemoryStream())
                {
                    foreach (BitmapImage bitmapImage in bitmapImages.Take(2))
                    {
                        var wb = new WriteableBitmap(bitmapImage);
                        wb = wb.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
                        var image = new Image {Source = wb};

                        var transform = new TranslateTransform {X = tempWidth, Y = tempHeight};
                        wbFinal.Render(image, transform);
                        tempWidth += wb.PixelWidth;
                    }
                    tempWidth = 0;
                    tempHeight = height;
                    foreach (BitmapImage bitmapImage in bitmapImages.Skip(2))
                    {
                        var wb = new WriteableBitmap(bitmapImage);
                        wb = wb.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
                        var image = new Image {Source = wb};

                        var transform = new TranslateTransform {X = tempWidth, Y = tempHeight};
                        wbFinal.Render(image, transform);
                        tempWidth += wb.PixelWidth;
                    }
                    wbFinal.Invalidate();
                    wbFinal.SaveJpeg(mem, collageWidth, collageHeight, 0, 100);
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