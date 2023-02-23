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
        private OperateResult SendPlcNgInfo()
        {
            LogHelper.UiLog.Info("向Plc下发测试结果");
            if (Tray.NgInfos.Any(x => x.IsNg)) 
            {
                var sendResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试结果"], (short)3);
                if (sendResult.IsFailed)
                    return sendResult; 
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
