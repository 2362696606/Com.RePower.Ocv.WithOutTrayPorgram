using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase
{
    /// <summary>
    /// 网口通讯类型设备
    /// </summary>
    public interface INetDevice : IDevice
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="ipAddress">Ip地址</param>
        /// <param name="port">端口</param>
        /// <returns>连接结果</returns>
        OperateResult Connect(string ipAddress, int port);

        /// <summary>
        /// 异步连接设备
        /// </summary>
        /// <param name="ipAddress">Ip地址</param>
        /// <param name="port">端口</param>
        /// <returns>连接结果</returns>
        Task<OperateResult> ConnectAsync(string ipAddress, int port);
    }
}