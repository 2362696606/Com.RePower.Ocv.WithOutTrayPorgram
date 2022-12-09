using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.DMM
{
    public interface IDMM
    {
        /// <summary>
        /// 读直流电压
        /// </summary>
        /// <returns></returns>
        OperateResult<double> ReadDc();
        /// <summary>
        /// 读交流电压
        /// </summary>
        /// <returns></returns>
        OperateResult<double> ReadAc();
        
    }
}
