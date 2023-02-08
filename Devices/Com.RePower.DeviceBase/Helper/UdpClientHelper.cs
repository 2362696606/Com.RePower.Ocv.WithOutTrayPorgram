using Com.RePower.WpfBase.Extensions;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CLDC.Framework.Log;

namespace Com.RePower.DeviceBase.Helper
{
    public static class UdpClientHelper
    {
        public static OperateResult<byte[]> SendAndRecovery(this UdpClient udpClient, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            try
            {
                string ip = ((IPEndPoint?)udpClient.Client.RemoteEndPoint)?.Address.ToString() ?? "未知IP";
                string port = ((IPEndPoint?)udpClient.Client.RemoteEndPoint)?.Port.ToString() ?? "未知端口";
                Log.getMessageFile("udp通讯日志").Info($"IP:{ip},端口:{port},发送:{bytes.ToHexString(',')}");
                //udpClient.GetStream().Write(bytes, 0, bytes.Length);
                udpClient.Send(bytes,bytes.Length);
                byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                udpClient.Client.ReceiveTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                Thread.Sleep(readDelay);
                var ipEndPoint = (IPEndPoint?)udpClient.Client.RemoteEndPoint;
                if (ipEndPoint is { })
                {
                    byte[] buffer = udpClient.Receive(ref ipEndPoint);
                    Log.getMessageFile("tcp通讯日志").Info($"IP:{ip},端口:{port},收到回复:{buffer.ToHexString(',')}");
                    return OperateResult.CreateSuccessResult(buffer);
                }
                else
                {
                    return OperateResult.CreateFailedResult<byte[]>("当前UdpClient的\"IpEndPoint\"为null");
                }
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }

        public static async Task<OperateResult<byte[]>> SendAndRecoveryAsync(this UdpClient udpClient, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            return await Task.Run(() => udpClient.SendAndRecovery(bytes, timeOut, readDelay));
        }
    }
}
