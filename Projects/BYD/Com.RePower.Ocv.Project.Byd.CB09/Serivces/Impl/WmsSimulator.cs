using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dto;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl
{
    public class WmsSimulator : WmsService
    {
        //public OperateResult<string> GetBatteriesInfo()
        //{
        //    WmsBatteriesInfoDto dto = new WmsBatteriesInfoDto
        //    {
        //        Result = 1,
        //        Message = "成功",
        //        PileContent = new PileContent()
        //        {
        //            PalletBarcode = "trayCode0001",
        //            FileName = "OCV0",
        //            BatteryType = "114",
        //        }
        //    };
        //    List<OneBattery> batteries = new List<OneBattery>();
        //    for (int i = 1; i <= 38; i++) 
        //    {
        //        var oneBattery = new OneBattery()
        //        {
        //            BatteryBarcode = $"batteryCode000{i.ToString()}",
        //            PalletIndex = i,
        //            IsRealBattery = 1,
        //            IsNg = 0
        //        };
        //        batteries.Add(oneBattery);
        //    }
        //    dto.PileContent.Batterys = batteries;
        //    string jStr = JsonConvert.SerializeObject(dto);
        //    return OperateResult.CreateSuccessResult<string>(jStr);
        //}

        //public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        //{
        //    return await Task.Run(GetBatteriesInfo);
        //}

        //public OperateResult<string> UploadTestResult()
        //{
        //    return OperateResult.CreateSuccessResult<string>("成功");
        //}

        //public async Task<OperateResult<string>> UploadTestResultAsync()
        //{
        //    return await Task.Run(UploadTestResult);
        //}
        public WmsSimulator(IHttpClientFactory httpClientFactory, Tray tray) : base(httpClientFactory, tray)
        {
        }

        protected override OperateResult<string> Post(object obj, string url, string optionName)
        {
            var requestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            var baseUri = HttpClient.BaseAddress;
            var fullUri = baseUri != null ? new Uri(baseUri, requestUri) : requestUri;
            string requestJStr = JsonConvert.SerializeObject(obj);
            LogHelper.WmsServiceLog.Info($"模拟\"{optionName}\"，请求地址:{fullUri.AbsoluteUri};内容{requestJStr}");

            var wmsSetting = new WmsSetting();
            var systemSetting = new SystemSetting();
            if (url == wmsSetting.GetBatteryInfoUrl)
            {
                WmsBatteriesInfoDto dto = new WmsBatteriesInfoDto
                {
                    Result = 1,
                    Message = "成功",
                    PileContent = new PileContent()
                    {
                        PalletBarcode = Tray.TrayCode,
                        FileName = systemSetting.DefaultOcvType.ToString(),
                        BatteryType = "114",
                    }
                };
                List<OneBattery> batteries = new List<OneBattery>();
                for (int i = 1; i <= 38; i++)
                {
                    var oneBattery = new OneBattery()
                    {
                        BatteryBarcode = $"batteryCode000{i.ToString()}",
                        PalletIndex = i,
                        IsRealBattery = 1,
                        IsNg = 0
                    };
                    batteries.Add(oneBattery);
                }
                dto.PileContent.Batterys = batteries;
                var recoverJStr = JsonConvert.SerializeObject(dto);
                LogHelper.WmsServiceLog.Info($"模拟\"{optionName}\"，请求地址:{fullUri.AbsoluteUri};内容:{requestJStr};返回:{recoverJStr}");
                return OperateResult.CreateSuccessResult<string>(recoverJStr);
            }
            else if (url == wmsSetting.UploadTestResultUrl)
            {
                LogHelper.WmsServiceLog.Info($"模拟\"{optionName}\"，请求地址:{fullUri.AbsoluteUri};内容:{requestJStr};返回:成功");
                return OperateResult.CreateSuccessResult<string>("成功");
            }
            else
            {
                LogHelper.WmsServiceLog.Info($"模拟\"{optionName}\"，请求地址:{fullUri.AbsoluteUri};内容:{requestJStr};返回:未找到接口{url}");
                return OperateResult.CreateFailedResult<string>($"未找到接口{url}");
            }
        }
    }
}
