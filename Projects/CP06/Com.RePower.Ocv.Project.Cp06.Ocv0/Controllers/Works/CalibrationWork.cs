using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers.Works
{
    public partial class MainWork
    {
        /// <summary>
        /// 预约校准
        /// </summary>
        /// <returns></returns>
        private OperateResult ScheduledCalibration()
        {
            if (DevicesController.PlcAddressCache.TryGetValue("清空/计量选择", out string? address))
            {
                var writeResult = DevicesController.Plc.Write(address, (short)2);
                if (writeResult.IsFailed)
                    return writeResult;
                IsScheduledCalibration = true;
                return OperateResult.CreateSuccessResult();
            }
            else
                return OperateResult.CreateFailedResult("未找到\"清空/计量选择\"对应地址");
        }
        /// <summary>
        /// 等待请求校表
        /// </summary>
        /// <returns></returns>
        private OperateResult WaitRequestCalibration()
        {
            var getAddressResult = DevicesController.PlcAddressCache.TryGetValue("校表请求信号", out string? address);
            if (!getAddressResult)
                return OperateResult.CreateFailedResult("未找到\"校表请求信号\"对应地址");
            var waitResult = DevicesController.Plc.Wait(address!, (short)1);
            if(waitResult.IsFailed) return waitResult;
            return OperateResult.CreateSuccessResult();
        }
        //private OperateResult DoCalibration()
        //{
            
        //    var CalibrationValues = SettingManager.CurrentCalibrationSetting?.CalibrationValues;
        //    if (CalibrationValues is { })
        //    {
        //        var closeAllResult = DevicesController.SwitchBoard?.CloseAllChannels(1) ?? OperateResult.CreateFailedResult("切换板实例为null");
        //        if(closeAllResult.IsFailed) return closeAllResult;
        //        foreach (var item in CalibrationValues)
        //        {
        //            var openResult = DevicesController.SwitchBoard?.OpenChannel(1, item.Channel) ?? OperateResult.CreateFailedResult("切换板实例为null");
        //            if (openResult.IsFailed) return openResult;
        //            var resResult = DevicesController.Ohm?.ReadRes() ?? OperateResult.CreateFailedResult<double>("内阻仪实例为null");
        //            if(resResult.IsFailed) return resResult;
        //            var res = resResult.Content;
        //        }
        //    }
        //    else return OperateResult.CreateFailedResult("无法获取校表配置");
        //}
    }
}
