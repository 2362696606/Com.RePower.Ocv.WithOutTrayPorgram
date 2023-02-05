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
        private OperateResult WaitPlcRequestRetest()
        {
            _retestCount++;
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)3);
            if(writeResult.IsFailed)
                return writeResult;
            var waitResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["请求测试"], (short)3);
            if(waitResult.IsFailed)
                return waitResult;
            var writeResult1 = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["请求测试"], (short)0);
            if(writeResult1.IsFailed)
                return writeResult1;
            return OperateResult.CreateSuccessResult();
        }
    }
}
