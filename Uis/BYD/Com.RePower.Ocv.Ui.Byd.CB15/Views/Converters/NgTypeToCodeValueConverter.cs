using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Com.RePower.Ocv.Ui.Byd.CB15.Views.Converters
{
    public class NgTypeToCodeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int intValue)
            {
                var ngType = (NgTypeEnum)intValue;
                if ((ngType & NgTypeEnum.电压过高) != 0)
                {
                    return string.Format("{0:D2}", 1);
                }
                else if ((ngType & NgTypeEnum.电压过低) != 0)
                {
                    return string.Format("{0:D2}", 2);
                }
                else if ((ngType & NgTypeEnum.正极壳体电压过低) != 0
                    || (ngType & NgTypeEnum.正极壳体电压过高) != 0
                    || (ngType & NgTypeEnum.负极壳体电压过低) != 0
                    || (ngType & NgTypeEnum.负极壳体电压过高) != 0) 
                {
                    return string.Format("{0:D2}", 3);
                }
                else if((ngType & NgTypeEnum.温度过高)!=0
                    ||(ngType & NgTypeEnum.温度过低)!=0)
                {
                    return string.Format("{0:D2}", 5);
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
