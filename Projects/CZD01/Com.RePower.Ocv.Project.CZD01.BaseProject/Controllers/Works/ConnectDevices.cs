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
            if (DevicesController.Dmm is { } && !DevicesController.Dmm.IsConnected)
            {
                LogHelper.UiLog.Info("连接万用表");
                var result = DevicesController.Dmm.Connect();
                if (result.IsFailed)
                    return result;
                LogHelper.UiLog.Info("成功连接万用表");
            }
            if (DevicesController.Ohm is { } && !DevicesController.Ohm.IsConnected)
            {
                LogHelper.UiLog.Info("连接内阻仪");
                var result = DevicesController.Ohm.Connect();
                if (result.IsFailed)
                    return result;
                LogHelper.UiLog.Info("成功连接内阻仪");
            }
            if (DevicesController.SwitchBoard is { } && !DevicesController.SwitchBoard.IsConnected)
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if (result.IsFailed)
                    return result;
                LogHelper.UiLog.Info("成功连接切换板");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
