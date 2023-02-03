using Com.RePower.Ocv.Model.Entity;
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
        /// 获取电芯条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetBatteriesInfo()
        {
            var getTrayInfoResult = WmsService.GetTechnologyInfoByBarCode();
            if (getTrayInfoResult.IsFailed)
                return getTrayInfoResult;
            var resultEntity = getTrayInfoResult.Content;
            if ((resultEntity?.Status.Code ?? 1) == 1)
            {
                return OperateResult.CreateFailedResult(resultEntity?.Status.Message ?? "未知异常");
            }
            if ((resultEntity?.trayCode ?? string.Empty) != Tray.TrayCode)
            {
                return OperateResult.CreateFailedResult("Wms下发托盘条码与当前托盘条码不一致");
            }
            List<NgInfo> ngInfos = new List<NgInfo>();
            foreach (var item in resultEntity?.TrayInfoLstResult ?? new List<WmsWebService.TrayInfoResult>().ToArray())
            {
                NgInfo tempNgInfo = new NgInfo();
                tempNgInfo.Battery = new Battery();
                tempNgInfo.Battery.TrayCode = Tray.TrayCode;
                tempNgInfo.Battery.BarCode = item.BatteryCode;
                tempNgInfo.Battery.Position = item.Position;
                ngInfos.Add(tempNgInfo);
            }
            Tray.NgInfos = ngInfos;
            return OperateResult.CreateSuccessResult();
        }
    }
}
