using SolidNavigation.Tasks;
using System;
using Windows.UI.Xaml.Data;

namespace SolidNavigation.Converters
{
    public class TaskViewModelToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (TaskViewModel)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
