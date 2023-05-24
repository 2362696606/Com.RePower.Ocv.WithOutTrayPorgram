using CLDC.Framework.Log;
using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System.IO.Ports;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class SerialPortDeviceBase : ISerialPortDeviceBase
    {
        private bool _disposedValue;

        private SerialPort _serialPort = new SerialPort();

        public SerialPort SerialPort
        {
            get { return _serialPort; }
            protected set { _serialPort = value; }
        }

        /// <summary>
        /// 读取延迟
        /// </summary>
        public int ReadDelay { get; set; }

        private string _deviceName = "UnnamedDevice";

        public string PortName
        {
            get
            {
                return SerialPort.PortName;
            }
            set
            {
                SerialPort.PortName = value;
            }
        }

        public int BaudRate
        {
            get { return SerialPort.BaudRate; }
            set { SerialPort.BaudRate = value; }
        }

        public bool IsConnected
        {
            get { return SerialPort.IsOpen; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public OperateResult Connect(string portName, int baudRate)
        {
            try
            {
                SerialPort = SerialPort ?? new SerialPort();
                SerialPort.BaudRate = baudRate;
                SerialPort.PortName = portName;
                SerialPort.Open();
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

        public OperateResult Connect()
        {
            try
            {
                SerialPort = SerialPort ?? new SerialPort();
                SerialPort.Open();
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
            return await Task.Run(() => Connect(portName, baudRate));
        }

        public async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public OperateResult DisConnect()
        {
            SerialPort.Close();
            if (IsConnected)
            {
                return OperateResult.CreateFailedResult("断开连接失败");
            }
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    SerialPort.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                _disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~SerialPortDeviceBase()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return SerialPort.SendAndRecovery(cmd, timeout, ReadDelay);
                }
                else
                {
                    Log.getMessageFile("串口日志").Info($"串口:{SerialPort.PortName}发送{cmd.ToHexString(' ')}");
                    SerialPort.Write(cmd, 0, cmd.Length);
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
            return await Task.Run(() => SendCmd(cmd, timeout, isNeedRecovery));
        }
    }
}