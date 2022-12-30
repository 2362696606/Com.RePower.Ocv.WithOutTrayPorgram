using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.SwitchBoard
{
    public abstract class SwitcbBoardBase : ISwitchBoard
    {

        public abstract bool IsConnected { get; }

        public abstract string DeviceName { get; set; }

        public abstract OperateResult CloseAllChannels(int boardIndex);

        public virtual async Task<OperateResult> CloseAllChannelsAsync(int boardIndex)
        {
            return await Task.Run(() => CloseAllChannels(boardIndex));
        }

        public abstract OperateResult CloseChannel(int boardIndex, int channel);

        public virtual async Task<OperateResult> CloseChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run(() => CloseChannel(boardIndex, channel));
        }

        public abstract OperateResult CloseChannels(int boardIndex, int[] channels);

        public virtual async Task<OperateResult> CloseChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run(() => CloseChannels(boardIndex, channels));
        }

        public abstract OperateResult Connect();

        public virtual async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public abstract OperateResult DisConnect();

        public virtual async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        public abstract void Dispose();

        public abstract OperateResult OpenChannel(int boardIndex, int channel);

        public virtual async Task<OperateResult> OpenChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run(() => OpenChannel(boardIndex, channel));
        }

        public abstract OperateResult OpenChannels(int boardIndex, int[] channels);

        public virtual async Task<OperateResult> OpenChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run(() => OpenChannels(boardIndex, channels));
        }

        public abstract OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        public virtual async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }
    }
}
