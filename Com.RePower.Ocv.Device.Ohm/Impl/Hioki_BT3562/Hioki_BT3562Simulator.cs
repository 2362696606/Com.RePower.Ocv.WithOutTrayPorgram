using Com.RePower.DeviceBase.BaseDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Device.Ohm.Impl.Hioki_BT3562
{
    public class Hioki_BT3562Simulator:Hioki_BT3562Impl
    {
        public Hioki_BT3562Simulator()
        {
            this.deviceBase = new SerialPortDeviceBaseSimulator();
        }
    }
}
