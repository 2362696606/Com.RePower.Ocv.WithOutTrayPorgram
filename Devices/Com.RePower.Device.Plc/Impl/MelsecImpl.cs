using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.Plc.Impl
{
    public class MelsecImpl : PlcNetAbstract
    {
        public MelsecImpl()
        {
            this.NetWorkDeviceBase = new HslCommunication.Profinet.Melsec.MelsecMcNet();
            base.OnDeviceCreated();
        }
    }
}
