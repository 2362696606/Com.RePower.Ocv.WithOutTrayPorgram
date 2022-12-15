using Com.RePower.DeviceBase.DMM;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Device.DMM.Impl
{
    public class DmmSimulator : IDMMNet
    {
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; }

        public bool IsConnected { get; set; } = false;

        public string DeviceName { get; set; } = "UnnamedDevice";

        public OperateResult Connect(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult Connect()
        {
            this.IsConnected = true;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.Port = port;
            this.IsConnected = true;
            await Task.Delay(1000);
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> ConnectAsync()
        {
            this.IsConnected = true;
            await Task.Delay(1000);
            return OperateResult.CreateSuccessResult();
        }

        public OperateResult DisConnect()
        {
            this.IsConnected = false;
            return OperateResult.CreateSuccessResult();
        }

        public async Task<OperateResult> DisConnectAsync()
        {
            this.IsConnected= false;
            await Task.Delay(1000);
            return OperateResult.CreateSuccessResult();
        }

        public void Dispose()
        {

        }

        public OperateResult<double> ReadAc()
        {
            var random = new Random();
            var randNum = random.NextDouble() * (500.0 - 300.0) + 300.0;
            var value = Convert.ToDouble(randNum.ToString("f" + 6));
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public async Task<OperateResult<double>> ReadAcAsync()
        {
            double value = 0;
            await Task.Run(() =>
            {
                var random = new Random();
                var randNum = random.NextDouble() * (500.0 - 300.0) + 300.0;
                value = Convert.ToDouble(randNum.ToString("f" + 6));
            });
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public OperateResult<double> ReadDc()
        {
            var random = new Random();
            var randNum = random.NextDouble() * (500.0 - 300.0) + 300.0;
            var value = Convert.ToDouble(randNum.ToString("f" + 6));
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public async Task<OperateResult<double>> ReadDcAsync()
        {
            double value = 0;
            await Task.Run(() =>
            {
                var random = new Random();
                var randNum = random.NextDouble() * (500.0 - 300.0) + 300.0;
                value = Convert.ToDouble(randNum.ToString("f" + 6));
            });
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public OperateResult<double> ReadRes()
        {
            var random = new Random();
            var randNum = random.NextDouble() * (3.0 - 0.5) + 300.0;
            var value = Convert.ToDouble(randNum.ToString("f" + 6));
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public async Task<OperateResult<double>> ReadResAsync()
        {
            double value = 0;
            await Task.Run(() =>
            {
                var random = new Random();
                var randNum = random.NextDouble() * (3.0 - 0.5) + 300.0;
                value = Convert.ToDouble(randNum.ToString("f" + 6));
            });
            return OperateResult.CreateSuccessResult<double>(value);
        }

        public OperateResult<byte[]> SendCmd(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            return OperateResult.CreateSuccessResult(new byte[0]);
        }

        public async Task<OperateResult<byte[]>> SendCmdAsync(byte[] cmd, int timeout = 10000, bool isNeedRecovery = true)
        {
            await Task.Delay(1000);
            return OperateResult.CreateSuccessResult(new byte[0]);
        }
    }
}
