using CLDC.Framework.Log;
using Com.RePower.WpfBase;
using Com.RePower.WpfBase.Extensions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.Helper
{
    public class SerialPortHelper : SerialPort
    {
        /// <summary>
        /// 等待收到消息
        /// </summary>
        private ManualResetEvent m_event = new ManualResetEvent(false);
        /// <summary>
        /// 获取回复方式
        /// </summary>
        public RecoveryModel RecoveryModel { get; set; } = RecoveryModel.Auto;
        /// <summary>
        /// 读取延时
        /// </summary>
        public int ReadDelay { get; set; } = 500;
        /// <summary>
        /// 发送并接受消息
        /// </summary>
        /// <param name="bytes">指令</param>
        /// <param name="timeOut">超时时间，-1为无限等待，只有自动模式才有效</param>
        /// <param name="readDelay">读取延迟，-1为默认延迟，只有手动模式才有效</param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndRecovery(byte[] bytes, int timeOut = 1000,int readDelay = -1)
        {
            Log.getMessageFile("串口日志").Info($"串口:{PortName}发送{bytes.ToHexString(',')}");
            if (RecoveryModel == RecoveryModel.Auto)
            {
                DataReceived += new SerialDataReceivedEventHandler(SetFlag);
                Write(bytes, 0, bytes.Length);
                var waitResult = this.m_event.WaitOne(timeOut);
                this.m_event.Reset();
                if (!waitResult)
                {
                    Log.getMessageFile("串口日志").Error($"串口:{PortName},等待回复超时");
                    return OperateResult.CreateFailedResult<byte[]>("等待回复超时");
                }
                DataReceived -= new SerialDataReceivedEventHandler(SetFlag);
            }
            else
            {
                Write(bytes, 0, bytes.Length);
                if (readDelay == -1)
                {
                    if(ReadDelay<0)
                    {
                        return OperateResult.CreateFailedResult<byte[]>("读取延时为复数");
                    }
                    Thread.Sleep(ReadDelay);
                }
                else if(readDelay<0)
                {
                    return OperateResult.CreateFailedResult<byte[]>("读取延时为复数");
                }
                else
                {
                    Thread.Sleep(readDelay);
                }
            }
            int length = BytesToRead;
            byte[] result = new byte[length];
            Read(result, 0, length);
            Log.getMessageFile("串口日志").Info($"串口:{PortName}收到回复{result.ToHexString()}");
            return OperateResult.CreateSuccessResult<byte[]>(result);
        }

        private void SetFlag(object sender, SerialDataReceivedEventArgs e)
        {
            this.m_event.Set();
        }

        public async Task<OperateResult<byte[]>> SendAndRecoveryAsync(byte[] bytes, int timeOut)
        {
            return await Task.Run<OperateResult<byte[]>>(() =>
            {
                return SendAndRecovery(bytes, timeOut);
            });
        }
    }
}
