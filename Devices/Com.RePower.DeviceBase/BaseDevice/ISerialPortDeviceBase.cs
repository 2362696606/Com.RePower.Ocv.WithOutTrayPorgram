using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public interface ISerialPortDeviceBase: ISerialPortDevice, ISendCmd
    {
        public int ReadDelay { get; set; }
    }
}
