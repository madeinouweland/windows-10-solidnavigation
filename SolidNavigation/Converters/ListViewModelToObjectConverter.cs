using SolidNavigation.Lists;
using System;
using Windows.UI.Xaml.Data;

namespace SolidNavigation.Converters
{
    public class ListViewModelToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (ListViewModel)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
