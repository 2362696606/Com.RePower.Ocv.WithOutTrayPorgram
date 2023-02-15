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
        private OperateResult SendGroupTestComplate(int groupNum)
        {
            LogHelper.UiLog.Info($"下发组{groupNum}测试完成信号");
            string address = SettingManager.PlcValueCacheSetting?["测试标志位"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"测试标志位\"地址");
            var writeResult = DevicesController.Plc?.Write(address, (short)(groupNum + 10)) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
    }
}
