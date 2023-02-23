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
        private OperateResult SendPlcTestComplete()
        {
            LogHelper.UiLog.Info("向Plc下发测试完成");
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)2);
            if(writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
    }
}
