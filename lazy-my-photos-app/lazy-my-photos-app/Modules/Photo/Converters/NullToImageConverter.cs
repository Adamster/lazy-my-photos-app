using Microsoft.Maui.Controls;
using System;

namespace Lazy.MyPhotos.App.Modules.Photo.Converters;

public class NullToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        // If the value (ImageSource) is null, return a placeholder image
        if (value == null)
        {
            return ImageSource.FromFile("sloth.png"); // Replace with your actual placeholder image
        }

        // Otherwise, return the actual ImageSource
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}