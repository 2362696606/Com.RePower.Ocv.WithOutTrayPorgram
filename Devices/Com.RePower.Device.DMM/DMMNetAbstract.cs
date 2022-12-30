using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Com.RePower.Device.DMM
{
    public abstract class DMMNetAbstract : IDMMNet
    {
        protected TcpClient tcpClient = new TcpClient();

        private string _deviceName = "UnnamedDevice";

        private string _ipAddress = "127.0.0.1";

        private int _port = 502;

        public bool IsConnected { 
            get 
            {
                return tcpClient.Connected;
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

        public virtual OperateResult Connect()
        {
            try
            {
                tcpClient.Connect(IpAddress, Port);
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

        public virtual OperateResult Connect(string ipAddress, int port)
        {
            try
            {

                this.IpAddress = ipAddress;
                this.Port = port;
                tcpClient.Connect(ipAddress, port);
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

        public  virtual async Task<OperateResult> ConnectAsync()
        {
            try
            {
                await tcpClient.ConnectAsync(IpAddress, Port);
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

        public virtual async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            try
            {
                this.IpAddress = ipAddress;
                this.Port = port;
                await tcpClient.ConnectAsync(ipAddress, port);
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

                this.tcpClient.Close();
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    tcpClient.Close();
                });
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult(err.Message, err.HResult);
            }
        }

        public void Dispose()
        {
            tcpClient.Dispose();
        }

        /// <summary>
        /// 获取读取交流电压指令
        /// </summary>
        /// <returns>指令对应byte[]</returns>
        protected abstract byte[] GetReadAcCmd();

        public OperateResult<double> ReadAc()
        {
            return ReadValue(GetReadAcCmd);
        }

        public async Task<OperateResult<double>> ReadAcAsync()
        {
            return await ReadValueAsync(GetReadAcCmd);
        }

        /// <summary>
        /// 获取读取直流电压指令
        /// </summary>
        /// <returns>指令对应byte[]</returns>
        protected abstract byte[] GetReadDcCmd();

        public OperateResult<double> ReadDc()
        {
            return ReadValue(GetReadDcCmd);
        }

        public async Task<OperateResult<double>> ReadDcAsync()
        {
            return await ReadValueAsync(GetReadDcCmd);
        }

        /// <summary>
        /// 获取读取电阻指令
        /// </summary>
        /// <returns>指令对应byte[]</returns>
        protected abstract byte[] GetReadResCmd();

        public OperateResult<double> ReadRes()
        {
            return ReadValue(GetReadResCmd);
        }

        public async Task<OperateResult<double>> ReadResAsync()
        {
            return await ReadValueAsync(GetReadResCmd);
        }

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                tcpClient.GetStream().Write(cmd, 0, cmd.Length);
                if (isNeedRecovery)
                {
                    byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                    tcpClient.ReceiveTimeout = timeout;

                    Thread.Sleep(1000);

                    int leg = tcpClient.GetStream().Read(rBuffer, 0, rBuffer.Length);
                    byte[] buffer = new byte[leg];//实际接收数据大小
                    Array.Copy(rBuffer, 0, buffer, 0, leg);
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateSuccessResult(new byte[] { });
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd,int timeout = 10000, bool isNeedRecovery = true)
        {
            try
            {
                await tcpClient.GetStream().WriteAsync(cmd, 0, cmd.Length);
                if (isNeedRecovery)
                {
                    byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                    tcpClient.ReceiveTimeout = timeout;
                    int leg = await tcpClient.GetStream().ReadAsync(rBuffer, 0, rBuffer.Length);
                    byte[] buffer = new byte[leg];//实际接收数据大小
                    Array.Copy(rBuffer, 0, buffer, 0, leg);
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateSuccessResult(new byte[] { });
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        protected abstract double TranslateToDouble(byte[] bytes);

        protected virtual OperateResult<double> ReadValue(Func<byte[]> getCmdAction)
        {
            var cmd = getCmdAction();
            var result = SendCmd(cmd);
            if(result.IsSuccess)
            {
                var byteValue = result.Content;
                if (byteValue == null || byteValue.Length <= 0) 
                {
                    return OperateResult.CreateFailedResult<double>("设备返回byte[]为空");
                }
                else
                {
                    var value = TranslateToDouble(byteValue);
                    return OperateResult.CreateSuccessResult(value);
                }
            }
            else
            {
                return OperateResult.CreateFailedResult<double>(result.Message ?? "读取失败", result.ErrorCode);
            }
        }
        protected virtual async Task<OperateResult<double>> ReadValueAsync(Func<byte[]> getCmdAction)
        {
            var cmd  = getCmdAction();
            var result = await SendCmdAsync(cmd);
            if (result.IsSuccess)
            {
                var byteValue = result.Content;
                if (byteValue == null || byteValue.Length <= 0)
                {
                    return OperateResult.CreateFailedResult<double>("设备返回byte[]为空");
                }
                else
                {
                    var value = TranslateToDouble(byteValue);
                    return OperateResult.CreateSuccessResult(value);
                }
            }
            else
            {
                return OperateResult.CreateFailedResult<double>(result.Message ?? "读取失败", result.ErrorCode);
            }
        }
    }
}