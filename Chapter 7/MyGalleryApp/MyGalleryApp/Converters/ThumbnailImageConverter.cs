using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Media;

namespace MyGalleryApp.Converters
{
    public class ThumbnailImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Picture)
            {
                Picture pic = value as Picture;
                BitmapImage image = new BitmapImage();

                if (parameter != null && parameter.ToString() == "thumbnail")
                {
                    image.SetSource(pic.GetThumbnail());
                }
                else
                {
                    image.SetSource(pic.GetImage());
                }

                return image;
            }
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
