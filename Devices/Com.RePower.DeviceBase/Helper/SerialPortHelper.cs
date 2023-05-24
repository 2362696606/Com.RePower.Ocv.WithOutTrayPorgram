using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System.IO.Ports;
using NPOI.SS.Formula.Functions;
using Log = CLDC.Framework.Log.Log;

namespace Com.RePower.DeviceBase.Helper
{
    public static class SerialPortHelper
    {
        /// <summary>
        /// 发送并接受消息
        /// </summary>
        /// <param name="serialPort"></param>
        /// <param name="bytes">指令</param>
        /// <param name="timeOut">超时时间，-1为无限等待，只有自动模式才有效</param>
        /// <param name="readDelay">读取延迟，-1为默认延迟，只有手动模式才有效</param>
        /// <returns></returns>
        public static OperateResult<byte[]> SendAndRecovery(this SerialPort serialPort, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            Log.getMessageFile("串口日志").Info($"串口:{serialPort.PortName}发送{bytes.ToHexString(' ')}");

            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            void SetFlag(object sender, SerialDataReceivedEventArgs e)
            {
                manualResetEvent.Set();
            }

            //serialPort.DataReceived += setFlag;
            serialPort.Write(bytes, 0, bytes.Length);

            #region 等待延迟

            var t1 = Task.Delay(readDelay < 0 ? 0 : readDelay);
            bool waitResult = false;
            Task t2 = Task.Run(() =>
            {
                waitResult = manualResetEvent.WaitOne(timeOut);
            });
            Task.WaitAll(t1, t2);
            serialPort.DataReceived += SetFlag;
            if (!waitResult && readDelay < timeOut)
            {
                Log.getMessageFile("串口日志").Error($"串口:{serialPort.PortName},等待回复超时");
                return OperateResult.CreateFailedResult<byte[]>("等待回复超时");
            }

            #endregion 等待延迟

            int length = serialPort.BytesToRead;
            byte[] result = new byte[length];
            serialPort.Read(result, 0, length);
            Log.getMessageFile("串口日志").Info($"串口:{serialPort.PortName}收到回复{result.ToHexString(' ')}");
            return OperateResult.CreateSuccessResult(result);
        }

        public static OperateResult<byte[]> SendAndRecoveryManual(this SerialPort serialPort, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            try
            {
                Log.getMessageFile("串口日志").Info($"串口:{serialPort.PortName}发送{bytes.ToHexString(',')}");
                serialPort.Write(bytes, 0, bytes.Length);
                serialPort.ReadTimeout = timeOut;
                if (readDelay < 0)
                    readDelay = 0;
                Thread.Sleep(readDelay);
                int length = serialPort.BytesToRead;
                byte[] result = new byte[length];
                serialPort.Read(result, 0, length);
                Log.getMessageFile("串口日志").Info($"串口:{serialPort.PortName}收到回复{result.ToHexString()}");
                return OperateResult.CreateSuccessResult(result);
            }
            catch (Exception err)
            {
                return OperateResult.CreateFailedResult<byte[]>(err.ToString(), err.HResult);
            }
        }

        public static async Task<OperateResult<byte[]>> SendAndRecoveryAsync(this SerialPort serialPort, byte[] bytes, int timeOut = 10000, int readDelay = -1)
        {
            return await Task.Run(() => serialPort.SendAndRecovery(bytes, timeOut, readDelay));
        }
    }
}