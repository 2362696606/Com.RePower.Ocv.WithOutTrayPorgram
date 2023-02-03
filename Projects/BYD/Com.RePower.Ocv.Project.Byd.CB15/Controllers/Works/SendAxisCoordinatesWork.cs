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
        /// 下发X1,X2轴坐标
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult SendAxisCoordinates()
        {
            LogHelper.UiLog.Info("下发轴坐标");
            float x1 = SettingManager.CurrentOtherSetting?.X1AxisCoordinates ?? 0;
            float x2 = SettingManager.CurrentOtherSetting?.X2AxisCoordinates ?? 0;
            LogHelper.UiLog.Info($"X1轴坐标写入{x1}");
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["X1轴坐标"], x1);
            if (writeResult.IsFailed)
                return writeResult;
            LogHelper.UiLog.Info($"X2轴坐标写入{x2}");
            var writeResult1 = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["X2轴坐标"], x2);
            if (writeResult1.IsFailed)
                return writeResult;
            LogHelper.UiLog.Info($"测试标志写1");
            var writeResult2 = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)1);
            if (writeResult2.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
    }
}
