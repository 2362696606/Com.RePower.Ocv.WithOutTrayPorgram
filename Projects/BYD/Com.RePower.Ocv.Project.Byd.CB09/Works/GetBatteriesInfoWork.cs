using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dtos;
using Com.RePower.WpfBase;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 从调度获取托盘条码
    /// </summary>
    /// <returns>获取结果</returns>
    protected virtual OperateResult GetBatteriesInfo()
    {

        var getBatteryInfoResult = WmsService.GetBatteriesInfo();
        if (getBatteryInfoResult.IsFailed)
            return getBatteryInfoResult;
        if (string.IsNullOrEmpty(getBatteryInfoResult.Content))
            return OperateResult.CreateFailedResult("从WMS获取到的托盘电芯信息为空");
        string content = getBatteryInfoResult.Content;
        var resultObj = JsonConvert.DeserializeObject<WmsBatteriesInfoDto>(content);
        if (resultObj == null)
        {
            return OperateResult.CreateFailedResult("请求电芯条码无法完成序列化");
        }
        if (resultObj.Result == 0)
        {
            return OperateResult.CreateFailedResult($"请求电芯条码失败:{resultObj.Message??"未知原因"}");
        }
        if (resultObj.PileContent == null)
        {
            return OperateResult.CreateFailedResult("请求电芯条码失败，主体为null");
        }
        if (resultObj.PileContent.PalletBarcode != Tray.TrayCode)
        {
            return OperateResult.CreateFailedResult($"请求电芯条码失败，WMS返回的托盘条码{resultObj.PileContent.PalletBarcode}与当前托盘条码{Tray.TrayCode}不一致");
        }
        var tempNgInfos = new List<NgInfo>();
        foreach (var battery in resultObj.PileContent.Batterys)
        {
            NgInfo ngInfo = new();
            ngInfo.Battery.BarCode = battery.BatteryBarcode;
            ngInfo.Battery.Position = battery.PalletIndex;
            ngInfo.Battery.BatteryType = resultObj.PileContent.BatteryType == "102" ? 1 : 0;
            ngInfo.Battery.TrayCode = Tray.TrayCode;
            tempNgInfos.Add(ngInfo);
        }

        Tray.NgInfos = tempNgInfos.OrderBy(x => x.Battery.Position).ToList();
        return OperateResult.CreateSuccessResult();
    }
}