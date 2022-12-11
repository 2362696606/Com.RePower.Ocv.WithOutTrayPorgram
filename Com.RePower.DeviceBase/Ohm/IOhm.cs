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
    public interface IOhm:IDevice
    {
        /// <summary>
        /// 读取内阻
        /// </summary>
        /// <returns>读取结果</returns>
        OperateResult<double> ReadRes();
        /// <summary>
        /// 直接发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns>指令返回结果</returns>
        OperateResult<byte[]> SendCmd(byte[] cmd);
    }
}
