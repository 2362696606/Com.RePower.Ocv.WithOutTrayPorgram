using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult GetBatteriesInfo()
        {
            LogHelper.UiLog.Info("获取电芯条码");
            var result = WmsService?.GetBatteriesInfo() ?? OperateResult.CreateFailedResult<string>("WmsService实例为null");
            if (result.IsFailed)
                return OperateResult.CreateFailedResult($"请求电芯条码失败:{result.Message ?? "未知原因"}");
            string contentStr = result.Content ?? string.Empty;
            if (string.IsNullOrEmpty(contentStr))
            {
                return OperateResult.CreateFailedResult("请求电芯条码返回为空");
            }
            var resultDto = JsonConvert.DeserializeObject<WmsGetBatteriesInfoResultDto>(contentStr);
            if (resultDto is { })
            {
                if (resultDto.Result != 1)
                    return OperateResult.CreateFailedResult($"请求电芯条码失败:{resultDto.Message}");
                if (resultDto.HandleResult.TrayCode != Tray.TrayCode)
                    return OperateResult.CreateFailedResult($"请求电芯条码失败:WMS下发电芯条码为{resultDto.HandleResult.TrayCode},但本地电芯条码为{Tray.TrayCode}");
                if (resultDto.HandleResult.BatteriesInfoList.Count <= 0)
                    return OperateResult.CreateFailedResult("获取电芯条码失败:电芯条码数组为空");
                //切换当前工站
                SettingManager.CurrentOcvType = Enum.Parse<OcvTypeEnum>(resultDto.HandleResult.Procedure);
                List<NgInfo> ngInfos = new List<NgInfo>();
                Tray.TaskCode = resultDto.HandleResult.TaskCode;
                foreach (var item in resultDto.HandleResult.BatteriesInfoList)
                {
                    var ngInfo = new NgInfo();
                    ngInfo.Battery.TrayCode = Tray.TrayCode;
                    //ngInfo.Battery = new Battery();
                    ngInfo.Battery.Position = item.Index;
                    ngInfo.Battery.BarCode = item.BarCode;
                    ngInfo.Battery.OcvType = resultDto.HandleResult.Procedure;
                    ngInfo.Battery.TaskCode = resultDto.HandleResult.TaskCode;
                    ngInfos.Add(ngInfo);
                }
                ngInfos.OrderBy(x => x.Battery.Position);
                Tray.NgInfos = ngInfos;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
