using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace InstaPhone
{
    public class Collage : ICollage
    {
        public void MakeCollage(ref Image collage, int collageWidth, int collageHeight,
            IEnumerable<BitmapImage> imagesParam)
        {
            IList<BitmapImage> sourceImages = imagesParam as IList<BitmapImage> ?? imagesParam.ToList();
            var number = (int) Math.Ceiling(Math.Sqrt(sourceImages.Count));
            int width = collageWidth/number;
            int height = collageHeight/number;

            var wbFinal = new WriteableBitmap(collageWidth, collageHeight);

            using (var memoryStream = new MemoryStream())
            {
                for (int column = 0; column < number; column++)
                {
                    for (int row = 0; row < number; row++)
                    {
                        var itemNumber = column*2 + row;
                        if (itemNumber >= sourceImages.Count) continue;
                        var wb = new WriteableBitmap(sourceImages[itemNumber]);
                        wb = wb.Resize(width, height, WriteableBitmapExtensions.Interpolation.Bilinear);
                        var image = new Image {Source = wb};
                        var transform = new TranslateTransform {X = row*width, Y = column*height};
                        wbFinal.Render(image, transform);
                    }
                }
                wbFinal.Invalidate();
                wbFinal.SaveJpeg(memoryStream, collageWidth, collageHeight, 0, 100);
                memoryStream.Seek(0, SeekOrigin.Begin); 
                collage.Source = wbFinal;
            }
        }
    }
}

