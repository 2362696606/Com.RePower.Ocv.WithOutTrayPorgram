using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl.Keysight_34461A_
{
    public abstract class Keysight_34461AAbstract : DMMBase
    {
        public override OperateResult<double> ReadAc()
        {
            throw new NotImplementedException();
        }

        public override OperateResult<double> ReadDc()
        {
            throw new NotImplementedException();
        }

        public override OperateResult<double> ReadRes()
        {
            throw new NotImplementedException();
        }
    }
}
