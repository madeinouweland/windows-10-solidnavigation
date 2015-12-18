using SolidNavigation.Details;
using System;
using Windows.UI.Xaml.Data;

namespace SolidNavigation.Converters
{
    public class CommentViewModelToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (CommentViewModel)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
