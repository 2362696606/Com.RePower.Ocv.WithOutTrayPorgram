using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.SwitchBoard
{
    /// <summary>
    /// 切换板
    /// </summary>
    [DeviceInfo(Models.DeviceType.SwitchBoard)]
    public interface ISwitchBoard:IDevice
    {
        /// <summary>
        /// 打开单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>开启结果</returns>
        OperateResult OpenChannel(int channel);
        /// <summary>
        /// 打开多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>开启结果</returns>
        OperateResult OpenChannels(int[] channels);
        /// <summary>
        /// 关闭单个通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <returns>关闭结果</returns>
        OperateResult CloseChannel(int channel);
        /// <summary>
        /// 关闭多个通道
        /// </summary>
        /// <param name="channels">通道号数组</param>
        /// <returns>关闭结果</returns>
        OperateResult CloseChannels(int[] channels);
        /// <summary>
        /// 关闭所有通道
        /// </summary>
        /// <returns>关闭结果</returns>
        OperateResult CloseAllChannels();
        /// <summary>
        /// 直接发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns>指令返回结果</returns>
        OperateResult<byte[]> SendCmd(byte[] cmd);
    }
}
