using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase
{
    public interface INetDevice:IDevice
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        OperateResult Connect(string ipAddress, int port);
    }
}
