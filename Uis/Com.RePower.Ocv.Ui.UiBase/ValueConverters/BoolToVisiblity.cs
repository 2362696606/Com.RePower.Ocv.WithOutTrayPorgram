using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Com.RePower.Ocv.Ui.UiBase.ValueConverters
{
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            if (targetType == typeof(Visibility))
            {
                Visibility visibility = boolValue ? Visibility.Visible : Visibility.Collapsed;
                return visibility;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
