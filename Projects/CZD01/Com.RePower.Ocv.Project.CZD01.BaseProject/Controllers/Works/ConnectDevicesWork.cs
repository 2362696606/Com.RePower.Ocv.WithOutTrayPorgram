using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
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
        private OperateResult ConnectDevices()
        {
            if (DevicesController.Plc is { })
            {
                if (!DevicesController.Plc.IsConnected)
                {
                    LogHelper.UiLog.Info("连接plc");
                    var result = DevicesController.Plc.Connect();
                    if (result.IsFailed)
                        return result;
                    LogHelper.UiLog.Info("成功连接plc");
                }
            }
            else
            {
                return OperateResult.CreateFailedResult("Plc实例为null");
            }
            if ((SettingManager.Instance.CurrentTestOption?.IsTestVol ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestNTemp ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestPVol ?? false))
            {
                if (DevicesController.Dmm is { })
                {
                    if (!DevicesController.Dmm.IsConnected)
                    {
                        LogHelper.UiLog.Info("连接万用表");
                        var result = DevicesController.Dmm.Connect();
                        if (result.IsFailed)
                            return result;
                        LogHelper.UiLog.Info("成功连接万用表");
                    }
                }
                else
                {
                    return OperateResult.CreateFailedResult("测试项中包含电压测试项但万用表实例为null");
                }
            }
            if (SettingManager.Instance.CurrentTestOption?.IsTestRes ?? false)
            {
                if (DevicesController.Ohm is { })
                {
                    if (!DevicesController.Ohm.IsConnected)
                    {
                        LogHelper.UiLog.Info("连接内阻仪");
                        var result = DevicesController.Ohm.Connect();
                        if (result.IsFailed)
                            return result;
                        LogHelper.UiLog.Info("成功连接内阻仪");
                        if(DevicesController.Ohm is Hioki_BT3562Impl tempOhm)
                        {
                            LogHelper.UiLog.Info("开始初始化内阻仪");
                            var setResult = tempOhm.SetInitiateContinuous();
                            if (setResult.IsFailed) return setResult;
                            setResult = tempOhm.SetRang();
                            if (setResult.IsFailed) return setResult;
                            LogHelper.UiLog.Info("成功初始化内阻仪");
                        }
                        
                    }
                }
                else
                {
                    return OperateResult.CreateFailedResult("测试项中包含内阻测试项但内阻仪实例为null");
                }
            }
            if ((SettingManager.Instance.CurrentTestOption?.IsTestTemp ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestPTemp ?? false)
                || (SettingManager.Instance.CurrentTestOption?.IsTestNTemp ?? false))
            {
                if (DevicesController.TemperatureSensor is { })
                {
                    if (!DevicesController.TemperatureSensor.IsConnected)
                    {
                        LogHelper.UiLog.Info("温度传感器");
                        var result = DevicesController.TemperatureSensor.Connect();
                        if (result.IsFailed)
                            return result;
                        LogHelper.UiLog.Info("成功连接温度传感器");
                    }
                }
                else
                {
                    return OperateResult.CreateFailedResult("测试项中包含温度测试项但温度传感器实例为null");
                }
            }
            if (DevicesController.SwitchBoard is { })
            {
                if (!DevicesController.SwitchBoard.IsConnected)
                {
                    LogHelper.UiLog.Info("连接切换板");
                    var result = DevicesController.SwitchBoard.Connect();
                    if (result.IsFailed)
                        return result;
                    LogHelper.UiLog.Info("成功连接切换板");
                }
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
