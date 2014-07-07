using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InstaPhone
{
    public interface ICollage
    {
        void MakeCollage(ref Image collage, int width, int height, IEnumerable<BitmapImage> imagesParam);
    }
}