using Com.RePower.Ocv.Model.Extensions;
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
        private OperateResult GetTrayCode()
        {
            string address = SettingManager.PlcValueCacheSetting?[$"托盘条码"]?.Address ?? string.Empty;
            if (string.IsNullOrEmpty(address))
                return OperateResult.CreateFailedResult($"无法获取\"托盘条码\"地址");
            int length =  SettingManager.PlcValueCacheSetting?[$"托盘条码"]?.Length ?? 0;

            var readResult = DevicesController.Plc?.ReadString(address, (ushort)length)
                ?? OperateResult.CreateFailedResult<string>("Plc实例为null");
            if (readResult.IsFailed)
                return readResult;
            string content = readResult.Content??string.Empty;
            string trayCode = content.Match(@"[0-9\.a-zA-Z_-]+")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(trayCode))
                return OperateResult.CreateFailedResult("托盘条码不合规");
            this.Tray.TrayCode = trayCode;
            return OperateResult.CreateSuccessResult();
        }
    }
}
