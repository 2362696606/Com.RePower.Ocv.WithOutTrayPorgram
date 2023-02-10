using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Attributes
{

    [System.AttributeUsage(AttributeTargets.Field|AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class IgnorSettingAttribute : Attribute
    {
    }
}
