using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase.DMM
{
    /// <summary>
    /// 万用表
    /// </summary>
    [DeviceInfo(Models.DeviceType.Dmm)]
    public interface IDmm : IDevice, ISendCmd
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