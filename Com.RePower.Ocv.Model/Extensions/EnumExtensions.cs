using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举值描述
        /// </summary>
        /// <param name="this">待获取枚举</param>
        /// <returns></returns>
        public static string GetDescriptionOriginal(this Enum @this)
        {
            var name = @this.ToString();
            var field = @this.GetType().GetField(name);
            if (field == null) return name;
            var att = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute), false);
            return att == null ? field.Name : ((DescriptionAttribute)att).Description;
        }
    }
}
