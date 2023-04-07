using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase.Ohm
{
    /// <summary>
    /// 内阻仪
    /// </summary>
    [DeviceInfo(Models.DeviceType.Ohm)]
    public interface IOhm : IDevice, ISendCmd
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