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

        public async Task<OperateResult> CloseAllChannelsAsync(int boardIndex)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseAllChannels(boardIndex);
            });
        }

        public abstract OperateResult CloseChannel(int boardIndex, int channel);

        public async Task<OperateResult> CloseChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseChannel(boardIndex, channel);
            });
        }

        public abstract OperateResult CloseChannels(int boardIndex, int[] channels);

        public async Task<OperateResult> CloseChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseChannels(boardIndex, channels);
            });
        }

        public abstract OperateResult Connect();

        public async Task<OperateResult> ConnectAsync()
        {
            var result = await Task.Run<OperateResult>(() =>
            {
                return Connect();
            });
            return result;
        }

        public abstract OperateResult DisConnect();

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run<OperateResult>(() =>
            {
                return DisConnect();
            });
        }

        public abstract void Dispose();

        public abstract OperateResult OpenChannel(int boardIndex, int channel);

        public async Task<OperateResult> OpenChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return OpenChannel(boardIndex, channel);
            });
        }

        public abstract OperateResult OpenChannels(int boardIndex, int[] channels);

        public async Task<OperateResult> OpenChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return OpenChannels(boardIndex, channels);
            });
        }

        public abstract OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true);

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() =>
            {
                return SendCmd(cmd, timeout, isNeedRecovery);
            });
        }

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposedValue)
        //    {
        //        if (disposing)
        //        {
        //            // TODO: 释放托管状态(托管对象)
        //        }

        //        // TODO: 释放未托管的资源(未托管的对象)并重写终结器
        //        // TODO: 将大型字段设置为 null
        //        disposedValue = true;
        //    }
        //}

        //// // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        //// ~SwitcbBoardBaseAbstract()
        //// {
        ////     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        ////     Dispose(disposing: false);
        //// }

        //public void Dispose()
        //{
        //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
