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
            this.netWorkDeviceBase = new HslCommunication.Profinet.Inovance.InovanceTcpNet();
            this.netWorkDeviceBase.LogNet = new LogNetDateTime(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/PlcLogs"), GenerateMode.ByEveryDay, 30);
            this.netWorkDeviceBase.SetPersistentConnection();
        }
    }
}
