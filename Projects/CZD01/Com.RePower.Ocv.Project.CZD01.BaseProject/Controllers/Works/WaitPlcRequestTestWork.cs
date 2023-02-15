using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult WaitPlcRequestTest()
        {
            LogHelper.UiLog.Info("等待测试请求");
            string address = SettingManager.PlcValueCacheSetting?[$"测试请求信号"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult($"无法获取\"测试请求信号\"地址");
            var waitResult = DevicesController.Plc?.Wait(address, (short)1, cancellation: this.FlowController.CancelToken)
                ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
