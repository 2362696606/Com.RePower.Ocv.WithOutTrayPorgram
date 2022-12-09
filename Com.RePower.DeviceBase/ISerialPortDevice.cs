using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase
{
    public interface ISerialPortDevice:IDevice
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        OperateResult Connect(string portName, int baudRate);
    }
}
