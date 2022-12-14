using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.DMM
{
    /// <summary>
    /// 万用表
    /// </summary>
    [DeviceInfo(Models.DeviceType.DMM)]
    public interface IDMM:IDevice,ISendCmd
    {
        /// <summary>
        /// 读直流电压
        /// </summary>
        /// <returns>读取结果</returns>
        OperateResult<double> ReadDc();
        /// <summary>
        /// 异步读取直流电压
        /// </summary>
        /// <returns>读取结果</returns>
        Task<OperateResult<double>> ReadDcAsync();
        /// <summary>
        /// 读交流电压
        /// </summary>
        /// <returns>读取结果</returns>
        OperateResult<double> ReadAc();
        /// <summary>
        /// 异步读取交流电压
        /// </summary>
        /// <returns>读取结果</returns>
        Task<OperateResult<double>> ReadAcAsync();
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
