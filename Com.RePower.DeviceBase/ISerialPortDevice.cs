using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase
{
    /// <summary>
    /// 串口通讯类型设备
    /// </summary>
    public interface ISerialPortDevice:IDevice
    {
        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="portName">串口名</param>
        /// <param name="baudRate">波特率</param>
        /// <returns></returns>
        OperateResult Connect(string portName, int baudRate);
        /// <summary>
        /// 异步连接设备
        /// </summary>
        /// <param name="portName">串口名</param>
        /// <param name="baudRate">波特率</param>
        /// <returns></returns>
        Task<OperateResult> ConnectAsync(string portName, int baudRate);
    }
}
