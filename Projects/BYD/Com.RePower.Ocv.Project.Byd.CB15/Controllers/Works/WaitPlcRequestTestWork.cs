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
        /// <summary>
        /// 等待Plc请求测试
        /// </summary>
        /// <returns></returns>
        private OperateResult WaitPlcRequestTest()
        {
            LogHelper.UiLog.Info("等待请求测试");
            var waitResult = DevicesController.Plc.Wait(SettingManager.CurrentPlcAddressCache["请求测试"], (short)2);
            if (waitResult.IsFailed)
                return waitResult;
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["请求测试"], (short)0);
            if (writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
    }
}
