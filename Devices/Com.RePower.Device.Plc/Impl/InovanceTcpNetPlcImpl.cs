using HslCommunication.LogNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Impl
{
    public class InovanceTcpNetPlcImpl:PlcNetAbstract
    {
        public InovanceTcpNetPlcImpl()
        {
            var device = new HslCommunication.Profinet.Inovance.InovanceTcpNet();
            device.DataFormat = HslCommunication.Core.DataFormat.DCBA;
            netWorkDeviceBase = device;
            OnDeviceCreated();
        }
    }
}
