using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
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
        public OperateResult ScheduledCalibration()
        {
            var connectResult = ConnectDevices();
            if(connectResult.IsFailed) return connectResult;
            LogHelper.UiLog.Info("预约校准");
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
            LogHelper.UiLog.Info("等待请求校表");
            var getAddressResult = DevicesController.PlcAddressCache.TryGetValue("校表请求信号", out string? address);
            if (!getAddressResult)
                return OperateResult.CreateFailedResult("未找到\"校表请求信号\"对应地址");
            var waitResult = DevicesController.Plc.Wait(address??string.Empty, (short)1);
            if(waitResult.IsFailed) return waitResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 下发开始校准
        /// </summary>
        /// <returns></returns>
        private OperateResult SendStartCalibration()
        {
            LogHelper.UiLog.Info("下发开始校准");
            var getAddressResult = DevicesController.PlcAddressCache.TryGetValue("校表标志位", out string? address);
            if (!getAddressResult) return OperateResult.CreateFailedResult("未找到\"校表标志位\"对应地址");
            var writeResult = DevicesController.Plc.Write(address??string.Empty, (short)1);
            if(writeResult.IsFailed) return writeResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 等待请求计量
        /// </summary>
        /// <returns></returns>
        private OperateResult WaitRequestMeasure()
        {
            LogHelper.UiLog.Info("等待请求计量");
            var getAddressResult = DevicesController.PlcAddressCache.TryGetValue("请求信号", out string? address);
            if (!getAddressResult)
                return OperateResult.CreateFailedResult("未找到\"请求信号\"对应地址");
            var waitResult = DevicesController.Plc.Wait(address ?? string.Empty, (short)2);
            if (waitResult.IsFailed) return waitResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 执行校准
        /// </summary>
        /// <returns></returns>
        private OperateResult DoCalibration()
        {
            LogHelper.UiLog.Info("执行校准");
            var closeAllResult = DevicesController.SwitchBoard?.CloseAllChannels(1) ?? OperateResult.CreateFailedResult("切换板实例为null");
            if (closeAllResult.IsFailed) return closeAllResult;
            foreach (var item in (SettingManager.CurrentCalibrationSetting?.CalibrationValues ?? new List<Model.Entity.CalibrationValue>())) 
            {
                var openResult = DevicesController.SwitchBoard?.OpenChannel(1, item.Channel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                if (openResult.IsFailed)
                    return openResult;
                var readResult = DevicesController.Ohm?.ReadRes() ?? OperateResult.CreateFailedResult<double>("内阻仪实例为null");
                if (readResult.IsFailed) return readResult;
                item.GaugeValue = readResult.Content;
                item.AutoValue = -(item.DeviationValue);
                var closeResult = DevicesController.SwitchBoard?.CloseChannel(1, item.Channel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                if (closeResult.IsFailed) return closeResult;
            }
            SettingManager.CurrentCalibrationSetting?.SaveChanged();
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 下发计量完成
        /// </summary>
        /// <returns></returns>
        private OperateResult SendMeasureComplate()
        {
            LogHelper.UiLog.Info("校准完成");
            var getAddressResult = DevicesController.PlcAddressCache.TryGetValue("校表标志位", out string? address);
            if (!getAddressResult) return OperateResult.CreateFailedResult("未找到\"校表标志位\"对应地址");
            var writeResult = DevicesController.Plc.Write(address ?? string.Empty, (short)3);
            if (writeResult.IsFailed) return writeResult;
            IsScheduledCalibration = false;
            return OperateResult.CreateSuccessResult();
        }
    }
}
