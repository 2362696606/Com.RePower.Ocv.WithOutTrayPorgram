using Com.RePower.DeviceBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.Attribute
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface,AllowMultiple = true,Inherited = true)]
    public class DeviceInfoAttribute : System.Attribute
    {
        public DeviceInfoAttribute(DeviceType deviceType)
        {
            this.DeviceType = deviceType;
        }
        public DeviceType DeviceType { get; set; }
    }
}
