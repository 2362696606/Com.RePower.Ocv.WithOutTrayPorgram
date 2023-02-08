using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult TestTemp()
        {
            if(SettingManager.CurrentTestOption?.IsTestPTemp??false)
            {
                LogHelper.UiLog.Info("开始测试温度");
                var readResult = DevicesController.PTemperatureSensor?.ReadTemp() ?? OperateResult.CreateFailedResult<double[]>("正极温度MTVS设备为null");
                if (readResult.IsFailed)
                    return readResult;
                var content = readResult.Content;
                if(content is { })
                {
                    int minPosition = Tray.NgInfos.Min(x => x.Battery.Position);
                    int maxPosition = Tray.NgInfos.Max(x => x.Battery.Position);
                    for (int i = minPosition; i <= maxPosition; i++) 
                    {
                        var temp = Tray.NgInfos.FirstOrDefault(x => x.Battery.Position == i);
                        if(temp is { })
                        {
                            if(!(temp.Battery.IsTested && !temp.IsNg && !_isMsaTest && !_lastTimesIsMsa))
                                temp.Battery.PTemp = content[i - 1];
                        }
                    }
                    return OperateResult.CreateSuccessResult();
                }
                else
                    return OperateResult.CreateFailedResult();
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
