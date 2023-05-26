using Com.RePower.DeviceBase.BaseDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Ohm.Impl.Hioki_BT3562
{
    public class Hioki_BT3562ManualImpl:HiokiBt3562Impl
    {
        public Hioki_BT3562ManualImpl():base()
        {
            this.DeviceBase = new SwitchBoardDeviceBase();
        }
    }
}
