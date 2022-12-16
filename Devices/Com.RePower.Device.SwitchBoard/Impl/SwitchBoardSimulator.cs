using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.SwitchBoard.Impl
{
    public class SwitchBoardSimulator : ISwitchBoardSerialPort
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 5002;

        public bool IsConnected { get; set; }

        public string DeviceName { get; set; } = "UnnamedDevice";

        public OperateResult CloseAllChannels(int boardIndex)
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> CloseAllChannelsAsync(int boardIndex)
        {
            return await Task.Run(() => CloseAllChannels(boardIndex));
        }

        public OperateResult CloseChannel(int boardIndex, int channel)
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> CloseChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run(() => CloseChannel(boardIndex, channel));
        }

        public OperateResult CloseChannels(int boardIndex, int[] channels)
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> CloseChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run(() => CloseChannels(boardIndex, channels));
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            this.PortName = portName;
            this.BaudRate = baudRate;
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Connect()
        {
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            return await Task.Run(() => Connect(portName, baudRate));
        }

        public async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public OperateResult DisConnect()
        {
            this.IsConnected = false;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        public void Dispose()
        {
            this.IsConnected = false;
        }

        public OperateResult OpenChannel(int boardIndex, int channel)
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> OpenChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run(() => OpenChannel(boardIndex, channel));
        }

        public OperateResult OpenChannels(int boardIndex, int[] channels)
        {
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> OpenChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run(() => OpenChannels(boardIndex, channels));
        }

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            if (isNeedRecovery)
            {
                return OperateResult.CreateSuccessResult(cmd);
            }
            else
            {
                return OperateResult.CreateSuccessResult<byte[]>(null);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }
    }
}
