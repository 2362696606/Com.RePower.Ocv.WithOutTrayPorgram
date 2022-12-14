using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Impl
{
    public class Siemens_S1500Impl:PlcNetAbstract
    {
        public Siemens_S1500Impl()
        {
            this.netWorkDeviceBase = new HslCommunication.Profinet.Siemens.SiemensS7Net(HslCommunication.Profinet.Siemens.SiemensPLCS.S1500);
            base.OnDeviceCreated();
        }
    }
}
