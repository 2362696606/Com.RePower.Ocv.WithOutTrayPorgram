using CLDC.Framework.Log;
using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class SerialPortDeviceBase : ISerialPortDeviceBase
    {
        private bool disposedValue;

        private SerialPortHelper _serialPort = new SerialPortHelper();

        public SerialPortHelper SerialPort
        {
            get { return _serialPort; }
            protected set { _serialPort = value; }
        }

        public RecoveryModel RecoveryModel
        {
            get { return _serialPort.RecoveryModel; }
            set { _serialPort.RecoveryModel = value; }
        }
        public int ReadDelay
        {
            get { return _serialPort.ReadDelay; }
            set { _serialPort.ReadDelay = value; }
        }

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
                SerialPort = SerialPort ?? new SerialPortHelper();
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
                SerialPort = SerialPort ?? new SerialPortHelper();
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
            SerialPort.Close();
            if (IsConnected)
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    SerialPort.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
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

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return SerialPort.SendAndRecovery(cmd, timeout);
                }
                else
                {
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
            return await Task.Run(() =>
            {
                return SendCmd(cmd, timeout, isNeedRecovery);
            });
        }
    }
}
