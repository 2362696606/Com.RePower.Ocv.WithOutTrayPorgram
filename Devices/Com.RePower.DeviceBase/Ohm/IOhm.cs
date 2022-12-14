using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.Ohm
{
    /// <summary>
    /// 内阻仪
    /// </summary>
    [DeviceInfo(Models.DeviceType.Ohm)]
    public interface IOhm:IDevice,ISendCmd
    {
        /// <summary>
        /// 读取内阻
        /// </summary>
        /// <returns>读取结果</returns>
        OperateResult<double> ReadRes();
        /// <summary>
        /// 异步读取内阻
        /// </summary>
        /// <returns>读取结果</returns>
        Task<OperateResult<double>> ReadResAsync();
    }
}
