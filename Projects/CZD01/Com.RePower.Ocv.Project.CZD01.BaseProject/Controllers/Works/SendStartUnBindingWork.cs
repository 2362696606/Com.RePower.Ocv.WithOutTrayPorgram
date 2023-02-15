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
        private OperateResult SendStartUnBinding()
        {
            LogHelper.UiLog.Info("下发开始拆盘");
            string address = SettingManager.PlcValueCacheSetting?["拆盘标志位"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult("无法获取\"拆盘标志位\"地址");
            var writeResult = DevicesController.Plc?.Write(address, (short)1) ?? OperateResult.CreateFailedResult("Plc实例为null");
            if (writeResult.IsFailed)
            {
                return writeResult;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
