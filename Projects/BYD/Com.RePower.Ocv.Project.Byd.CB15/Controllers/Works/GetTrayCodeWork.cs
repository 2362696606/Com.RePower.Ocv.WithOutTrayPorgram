using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
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
        /// 获取托盘条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetTrayCode()
        {
            LogHelper.UiLog.Info("读取托盘条码");
            var readTrayCodeResult = DevicesController.Plc.ReadString(SettingManager.CurrentPlcAddressCache["上托盘条码信息"], 32);
            if (readTrayCodeResult.IsFailed)
            {
                return readTrayCodeResult;
            }
            string getCode = readTrayCodeResult?.Content ?? string.Empty;
            string trayCode = getCode.Match(@"[0-9\.a-zA-Z_-]+").Value;
            if (string.IsNullOrEmpty(trayCode))
            {
                return OperateResult.CreateFailedResult("托盘条码不合规");
            }
            Tray.TrayCode = trayCode;
            return OperateResult.CreateSuccessResult();
        }
    }
}
