using Com.RePower.DeviceBase.Helper;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.WpfBase;
using System.IO.Ports;
using System.Threading.Channels;

namespace Com.RePower.Device.SwitchBoard
{
    public abstract class SwitchBoardAbstract : ISwitchBoardSerialPort
    {
        private SerialPortHelper serialPort = new SerialPortHelper();

        private string _deviceName = "UnnamedDevice";
        public string PortName 
        {
            get 
            {
               return serialPort.PortName;
            }
            set 
            {
                serialPort.PortName = value;
            } 
        }

        public int BaudRate
        {
            get { return serialPort.BaudRate; }
            set { serialPort.BaudRate = value; }
        }

        public bool IsConnected
        {
            get { return serialPort.IsOpen; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }


        public abstract OperateResult CloseAllChannels(int boardIndex);

        public virtual async Task<OperateResult> CloseAllChannelsAsync(int boardIndex)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseAllChannels(boardIndex);
            });
        }

        public abstract OperateResult CloseChannel(int boardIndex, int channel);

        public virtual async Task<OperateResult> CloseChannelAsync(int boardIndex, int channel)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseChannel(boardIndex, channel);
            });
        }

        public abstract OperateResult CloseChannels(int boardIndex, int[] channels);

        public virtual async Task<OperateResult> CloseChannelsAsync(int boardIndex, int[] channels)
        {
            return await Task.Run<OperateResult>(() =>
            {
                return CloseChannels(boardIndex, channels);
            });
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            try
            {
                serialPort.BaudRate = baudRate;
                serialPort.PortName = portName;
                serialPort.Open();
                if(!IsConnected)
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
                return OperateResult.CreateSuccessResult();
            }
            catch(Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message,err.HResult);
            }
        }

        public OperateResult Connect()
        {
            try
            {
                serialPort.Open();
                if (!IsConnected)
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync(string portName, int baudRate)
        {
            var result = await Task.Run<OperateResult>(() =>
            {
                return Connect(portName, baudRate);
            });
            return result;
        }

        public async Task<OperateResult> ConnectAsync()
        {
            var result = await Task.Run<OperateResult>(() =>
            {
                return Connect();
            });
            return result;
        }

        public OperateResult DisConnect()
        {
            serialPort.Close();
            if(IsConnected)
            {
                return OperateResult.CreateFailedResult("断开连接失败");
            }
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run<OperateResult>(() =>
            {
                return DisConnect();
            });
        }

        public void Dispose()
        {
            serialPort.Dispose();
        }

        public abstract OperateResult OpenChannel(int boardIndex, int channel);

        public virtual async Task<OperateResult> OpenChannelAsync(int boardIndex, int channel)
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

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {

            try
            {
                if (isNeedRecovery)
                {
                    return serialPort.SendAndRecovery(cmd, timeout);
                }
                else
                {
                    serialPort.Write(cmd, 0, cmd.Length);
                    return OperateResult.CreateSuccessResult<byte[]>(null);
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return await Task.Run(() =>
            {
                return SendCmd(cmd, timeout, isNeedRecovery);
            });
        }
    }
}