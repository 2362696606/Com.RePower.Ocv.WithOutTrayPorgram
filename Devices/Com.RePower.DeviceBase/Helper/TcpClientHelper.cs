using CLDC.Framework.Log;
using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System.Net;
using System.Net.Sockets;

namespace Com.RePower.DeviceBase.Helper
{
    public static class TcpClientHelper
    {
        public static OperateResult<byte[]> SendAndRecovery(this TcpClient tcpClient, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            try
            {
                string ip = ((IPEndPoint?)tcpClient.Client.RemoteEndPoint)?.Address.ToString() ?? "未知IP";
                string port = ((IPEndPoint?)tcpClient.Client.RemoteEndPoint)?.Port.ToString() ?? "未知端口";
                Log.getMessageFile("tcp通讯日志").Info($"IP:{ip},端口:{port},发送:{bytes.ToHexString(',')}");
                tcpClient.GetStream().Write(bytes, 0, bytes.Length);
                byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                tcpClient.ReceiveTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                Thread.Sleep(readDelay);
                int leg = tcpClient.GetStream().Read(rBuffer, 0, rBuffer.Length);
                byte[] buffer = new byte[leg];//实际接收数据大小
                Array.Copy(rBuffer, 0, buffer, 0, leg);
                Log.getMessageFile("tcp通讯日志").Info($"IP:{ip},端口:{port},收到回复:{buffer.ToHexString(',')}");
                return OperateResult.CreateSuccessResult(buffer);
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.ToString(), err.HResult);
            }
        }

        public static async Task<OperateResult<byte[]>> SendAndRecoveryAsync(this TcpClient tcpClient, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            try
            {
                string ip = ((IPEndPoint?)tcpClient.Client.RemoteEndPoint)?.Address.ToString() ?? "未知IP";
                string port = ((IPEndPoint?)tcpClient.Client.RemoteEndPoint)?.Port.ToString() ?? "未知端口";
                Log.getMessageFile("tcp通讯日志").Info($"IP:{ip},端口:{port},发送:{bytes.ToHexString(',')}");
                await tcpClient.GetStream().WriteAsync(bytes, 0, bytes.Length);
                byte[] rBuffer = new byte[1024 * 64];//接收临时缓存数组
                tcpClient.ReceiveTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                await Task.Delay(readDelay);
                int leg = await tcpClient.GetStream().ReadAsync(rBuffer, 0, rBuffer.Length);
                byte[] buffer = new byte[leg];//实际接收数据大小
                Array.Copy(rBuffer, 0, buffer, 0, leg);
                Log.getMessageFile("tcp通讯日志").Info($"IP:{ip},端口:{port},收到回复:{buffer.ToHexString(',')}");
                return OperateResult.CreateSuccessResult(buffer);
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.Message, err.HResult);
            }
        }
    }
}