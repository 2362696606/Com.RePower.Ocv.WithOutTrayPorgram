using log4net.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Com.RePower.Ocv.Ui.WuWei.ValueConverters
{
    public class LogLevelToBrushConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Level level = (Level)value;
            if (level.Name == "WARN")
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if (level.Name == "ERROR")
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
