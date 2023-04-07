using Com.RePower.WpfBase;

namespace Com.RePower.DeviceBase
{
    public interface ISendCmd
    {
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="isNeedRecovery">是否需要回复</param>
        /// <returns>指令返回结果</returns>
        OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        /// <summary>
        /// 异步发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <param name="isNeedRecovery">是否需要回复</param>
        /// <returns>指令结果</returns>
        Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);
    }
}