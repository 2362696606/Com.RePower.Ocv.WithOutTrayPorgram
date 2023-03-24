using Com.RePower.Ocv.Ui.YiWei.Views.UserControls;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Com.RePower.Ocv.Ui.YiWei.ValueConverters
{
    public class PlcValueToAlarmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType != typeof(AlarmType) || value == null)
            {
                return DependencyProperty.UnsetValue;
            }
            else
            {
                string? valueString = value as string;
                if (!string.IsNullOrEmpty(valueString))
                {
                    //bool valueBool = bool.Parse(valueString);
                    bool valueBool;
                    var convertResult = bool.TryParse(valueString, out valueBool);
                    if (!convertResult)
                    {
                        return DependencyProperty.UnsetValue;
                    }
                    if (valueBool)
                    {
                        return AlarmType.Error;
                    }
                    else
                    {
                        return AlarmType.Start;
                    }
                }
                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
