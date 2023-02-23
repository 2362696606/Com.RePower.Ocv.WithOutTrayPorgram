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
        private OperateResult WaitStatusClean()
        {
            LogHelper.UiLog.Info($"下发测试完成");
            string address = SettingManager.PlcValueCacheSetting?["测试标志位"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"测试标志位\"地址");
            var writeResult = DevicesController.Plc?.Write(address, (short)8) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            LogHelper.UiLog.Info($"等待测试状态清零");
            address = SettingManager.PlcValueCacheSetting?["测试状态"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"测试状态\"地址");
            var waitResult = DevicesController.Plc?.Wait(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if(waitResult.IsFailed)
                return waitResult;
            return OperateResult.CreateSuccessResult();
        }
    }
}
