using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class TcpNetDeviceBase : INetDeviceBase
    {
        private TcpClient _tcpClient = new TcpClient();

        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            protected set { _tcpClient = value; }
        }


        private string _deviceName = "UnnamedDevice";

        private string _ipAddress = "127.0.0.1";

        private int _port = 502;
        private bool disposedValue;

        public bool IsConnected
        {
            get
            {
                return TcpClient.Connected;
            }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public OperateResult Connect()
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                TcpClient.Connect(IpAddress, Port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public OperateResult Connect(string ipAddress, int port)
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                this.IpAddress = ipAddress;
                this.Port = port;
                TcpClient.Connect(ipAddress, port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync()
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                await TcpClient.ConnectAsync(IpAddress, Port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            try
            {
                TcpClient = TcpClient ?? new TcpClient();
                this.IpAddress = ipAddress;
                this.Port = port;
                await TcpClient.ConnectAsync(ipAddress, port);
                if (IsConnected)
                {
                    return OperateResult.CreateSuccessResult();
                }
                else
                {
                    return OperateResult.CreateFailedResult("连接失败");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public OperateResult DisConnect()
        {
            try
            {
                this.TcpClient.Close();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            return await Task.Run(() => DisConnect());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    this.TcpClient.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~TcpNetDeviceBase()
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
            throw new NotImplementedException();
        }

        public Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            throw new NotImplementedException();
        }
    }
}
