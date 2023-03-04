using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
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
            if (connectResult.IsFailed) return connectResult;
            LogHelper.UiLog.Info("预约校准");
            string address = SettingManager.PlcValueCacheSetting?["清空/计量选择"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"清空/计量选择\"地址");
            var writeResult = DevicesController.Plc?.Wait(address ?? string.Empty, (short)2) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            IsScheduledCalibration = true;
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 等待请求校表
        /// </summary>
        /// <returns></returns>
        private OperateResult WaitRequestCalibration()
        {
            LogHelper.UiLog.Info("等待请求校表");
            string address = SettingManager.PlcValueCacheSetting?["校表请求信号"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"校表请求信号\"地址");
            var writeResult = DevicesController.Plc?.Wait(address ?? string.Empty, (short)1) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 下发开始校准
        /// </summary>
        /// <returns></returns>
        private OperateResult SendStartCalibration()
        {
            LogHelper.UiLog.Info("下发开始校准");
            string address = SettingManager.PlcValueCacheSetting?["校表标志位"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"校表标志位\"地址");
            var writeResult = DevicesController.Plc?.Wait(address ?? string.Empty, (short)1) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 等待请求计量
        /// </summary>
        /// <returns></returns>
        private OperateResult WaitRequestMeasure()
        {
            LogHelper.UiLog.Info("等待请求计量");
            string address = SettingManager.PlcValueCacheSetting?["请求信号"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"请求信号\"地址");
            var writeResult = DevicesController.Plc?.Wait(address ?? string.Empty, (short)2) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
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
            string address = SettingManager.PlcValueCacheSetting?["校表标志位"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"校表标志位\"地址");
            var writeResult = DevicesController.Plc?.Wait(address ?? string.Empty, (short)3) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            IsScheduledCalibration = false;
            return OperateResult.CreateSuccessResult();
        }
    }
}
