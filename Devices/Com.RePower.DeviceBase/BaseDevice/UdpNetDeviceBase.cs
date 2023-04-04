using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Com.RePower.DeviceBase.Helper;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.BaseDevice
{
    public class UdpNetDeviceBase : INetDeviceBase
    {
        private UdpClient _udpClient = new UdpClient();

        public int ReadDelay { get; set; } = 1000;
        private string _ipAddress = "127.0.0.1";
        private int _port = 502;
        private string _deviceName = "UnnamedDevice";
        private bool _isConnected;
        private bool _disposedValue;

        public UdpClient UdpClient
        {
            get { return _udpClient; }
            protected set { _udpClient = value; }
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

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; }
        }


        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        public OperateResult Connect(string ipAddress, int port)
        {
            try
            {
                this.IpAddress = ipAddress;
                this.Port = port;
                UdpClient.Connect(ipAddress, port);
                this.IsConnected = true;
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                this.IsConnected = false;
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public OperateResult Connect()
        {
            try
            {
                UdpClient.Connect(IpAddress, Port);
                this.IsConnected = true;
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                this.IsConnected = false;
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            return await Task.Run(() => Connect(ipAddress, port));
        }

        public async Task<OperateResult> ConnectAsync()
        {
            return await Task.Run(() => Connect());
        }

        public OperateResult DisConnect()
        {
            try
            {
                UdpClient.Close();
                this.IsConnected = false;
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


        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return UdpClient.SendAndRecovery(cmd, timeout, ReadDelay);
                }
                else
                {
                    UdpClient.Send(cmd, cmd.Length);
                    return OperateResult.CreateSuccessResult<byte[]>(null);
                }
            }
            catch (Exception err)
            {
                this.IsConnected = false;
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                if (isNeedRecovery)
                {
                    return await UdpClient.SendAndRecoveryAsync(cmd, timeout, ReadDelay);
                }
                else
                {
                    await UdpClient.SendAsync(cmd, cmd.Length);
                    return OperateResult.CreateSuccessResult<byte[]>(null);
                }
            }
            catch (Exception err)
            {
                this.IsConnected = false;
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    this.UdpClient.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                _disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~UdpNetDeviceBase()
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
    }
}
