﻿using log4net.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Com.RePower.Ocv.Ui.Byd.CB15.Views.Converters
{
    public class NgInfoToColorMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#14FFFFFF"));
            if (values[0] is bool a && values[1] is bool b)
            {
                if (!b) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#14FFFFFF"));
                else if (b && a) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCDD2"));
                else return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69F0AE"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#14FFFFFF"));
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
