﻿using Com.RePower.DeviceBase.Attribute;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.DeviceBase.DMM
{
    /// <summary>
    /// 万用表
    /// </summary>
    [DeviceInfo(Models.DeviceType.DMM)]
    public interface IDMM:IDevice
    {
        /// <summary>
        /// 读直流电压
        /// </summary>
        /// <returns></returns>
        OperateResult<double> ReadDc();
        /// <summary>
        /// 读交流电压
        /// </summary>
        /// <returns></returns>
        OperateResult<double> ReadAc();
        /// <summary>
        /// 直接发送指令
        /// </summary>
        /// <param name="cmd">指令</param>
        /// <returns>指令返回结果</returns>
        OperateResult<byte[]> SendCmd(byte[] cmd);
    }
}
