﻿using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.TemperatureSensor
{
    /// <summary>
    /// 温度传感器
    /// </summary>
    [DeviceInfo( Models.DeviceType.TemperatureSensor)]
    public interface ITemperatureSensor:IDevice
    {
        /// <summary>
        /// 读取温度
        /// </summary>
        /// <returns>温度读取结果</returns>
        OperateResult<double[]> ReadTemp();
        /// <summary>
        /// 直接发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns>指令返回结果</returns>
        OperateResult<byte[]> SendCmd(byte[] cmd);
    }
}
