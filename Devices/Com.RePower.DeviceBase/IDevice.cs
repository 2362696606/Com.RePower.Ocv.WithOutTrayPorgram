using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase
{
    /// <summary>
    /// 设备
    /// </summary>
    public interface IDevice:IDisposable
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected { get; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns>连接结果</returns>
        OperateResult Connect();
        /// <summary>
        /// 异步连接
        /// </summary>
        /// <returns>连接结果</returns>
        Task<OperateResult> ConnectAsync();
        /// <summary>
        /// 断开设备连接
        /// </summary>
        /// <returns>断开结果</returns>
        OperateResult DisConnect();
        /// <summary>
        /// 异步断开连接
        /// </summary>
        /// <returns>断开结果</returns>
        Task<OperateResult> DisConnectAsync();
    }
}