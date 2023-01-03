using CLDC.Framework.Log;
using Com.RePower.WpfBase.Extensions;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net.Http;

namespace Com.RePower.DeviceBase.Helper
{
    public static class TcpClientHelper
    {
        public static OperateResult<byte[]> SendAndRecovery(this TcpClient tcpClient, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            try
            {
                tcpClient.GetStream().Write(bytes, 0, bytes.Length);
                byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                tcpClient.ReceiveTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                Thread.Sleep(readDelay);
                int leg = tcpClient.GetStream().Read(rBuffer, 0, rBuffer.Length);
                byte[] buffer = new byte[leg];//实际接收数据大小
                Array.Copy(rBuffer, 0, buffer, 0, leg);
                return OperateResult.CreateSuccessResult(buffer);
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public static async Task<OperateResult<byte[]>> SendAndRecoveryAsync(this TcpClient tcpClient, byte[] bytes, int timeOut = 10000,int readDelay = -1)
        {
            try
            {
                await tcpClient.GetStream().WriteAsync(bytes, 0, bytes.Length);
                byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                tcpClient.ReceiveTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                await Task.Delay(readDelay);
                int leg = await tcpClient.GetStream().ReadAsync(rBuffer, 0, rBuffer.Length);
                byte[] buffer = new byte[leg];//实际接收数据大小
                Array.Copy(rBuffer, 0, buffer, 0, leg);
                return OperateResult.CreateSuccessResult(buffer);
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }
    }
}
