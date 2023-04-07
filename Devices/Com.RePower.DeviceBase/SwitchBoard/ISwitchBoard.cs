using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase.SwitchBoard
{
    /// <summary>
    /// 切换板
    /// </summary>
    [DeviceInfo(Models.DeviceType.SwitchBoard)]
    public interface ISwitchBoard : IDevice, ISendCmd
    {
        /// <summary>
        /// 打开单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>开启结果</returns>
        OperateResult OpenChannel(int boardIndex, int channel);

        /// <summary>
        /// 异步打开单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>开启结果</returns>
        Task<OperateResult> OpenChannelAsync(int boardIndex, int channel);

        /// <summary>
        /// 打开多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>开启结果</returns>
        OperateResult OpenChannels(int boardIndex, int[] channels);

        /// <summary>
        /// 异步打开多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>开启结果</returns>
        Task<OperateResult> OpenChannelsAsync(int boardIndex, int[] channels);

        /// <summary>
        /// 关闭单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>关闭结果</returns>
        OperateResult CloseChannel(int boardIndex, int channel);

        /// <summary>
        /// 异步关闭单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>关闭结果</returns>
        Task<OperateResult> CloseChannelAsync(int boardIndex, int channel);

        /// <summary>
        /// 关闭多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>关闭结果</returns>
        OperateResult CloseChannels(int boardIndex, int[] channels);

        /// <summary>
        /// 异步关闭多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>关闭结果</returns>
        Task<OperateResult> CloseChannelsAsync(int boardIndex, int[] channels);

        /// <summary>
        /// 关闭所有通道
        /// </summary>
        /// <returns>关闭结果</returns>
        OperateResult CloseAllChannels(int boardIndex);

        /// <summary>
        /// 异步关闭所有通道
        /// </summary>
        /// <returns>关闭结果</returns>
        Task<OperateResult> CloseAllChannelsAsync(int boardIndex);
    }
}