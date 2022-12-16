using Com.RePower.WpfBase;
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
        /// 发送并接受消息
        /// </summary>
        /// <param name="bytes">指令</param>
        /// <param name="timeOut">超时事件，-1为无限等待</param>
        /// <returns></returns>
        public OperateResult<byte[]> SendAndRecovery(byte[] bytes, int timeOut)
        {
            DataReceived += new SerialDataReceivedEventHandler(SetFlag);
            Write(bytes, 0, bytes.Length);
            var waitResult = this.m_event.WaitOne(timeOut);
            this.m_event.Reset();
            if (!waitResult)
            {
                return OperateResult.CreateFailedResult<byte[]>("等待回复超时");
            }
            int length = BytesToRead;
            byte[] result = new byte[length];
            Read(result, 0, length);
            DataReceived -= new SerialDataReceivedEventHandler(SetFlag);
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
