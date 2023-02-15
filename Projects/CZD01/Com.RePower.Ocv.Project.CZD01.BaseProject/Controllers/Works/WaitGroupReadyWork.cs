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
        private OperateResult WaitGroupReady(int groupNum)
        {
            LogHelper.UiLog.Info($"等待组{groupNum}就绪");
            string address = SettingManager.PlcValueCacheSetting?[$"测试状态"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult($"无法获取\"测试状态\"地址");
            var waitResult = DevicesController.Plc?.Wait(address, (short)groupNum, cancellation: this.FlowController.CancelToken)
                ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
