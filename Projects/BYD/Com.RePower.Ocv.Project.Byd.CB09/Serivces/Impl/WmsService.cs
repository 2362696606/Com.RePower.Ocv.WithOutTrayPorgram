using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces.Dto;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Project.Byd.CB09.Serivces.Impl
{
    public class WmsService : IWmsService
    {
        public WmsService(IHttpClientFactory httpClientFactory,Tray tray)
        {
            _wmsSetting = new WmsSetting();
            HttpClientFactory = httpClientFactory;
            HttpClient = httpClientFactory.CreateClient("WmsHttpClient");
            HttpClient.BaseAddress = new Uri(_wmsSetting.BaseAddress);
            Tray = tray;
        }

        public OperateResult<string> GetBatteriesInfo()
        {
            var obj = new { PalletCode = Tray.TrayCode, EquipmentCode };
            return Post(obj, GetBatteryInfoUrl ,"请求电芯条码");
            //string jStr = JsonConvert.SerializeObject(obj);
            //HttpContent content = new StringContent(jStr);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            //{
            //    CharSet = "utf-8"
            //};

            //var result = HttpClient.PostAsync(GetBatteryInfoUrl, content).Result;
            //if (result.IsSuccessStatusCode)
            //{
            //    string strResult = result.Content.ReadAsStringAsync().Result;
            //    LogHelper.WmsServiceLog.Info($"请求电芯条码，请求内容{jStr},返回{strResult}");
            //    if(string.IsNullOrEmpty(strResult))
            //    {
            //        return OperateResult.CreateFailedResult<string>("请求电芯条码失败，返回为空或null");
            //    }
            //    else
            //    {
            //        return OperateResult.CreateSuccessResult<string>(strResult);
            //    }
            //}
            //else
            //{
            //    LogHelper.WmsServiceLog.Error($"请求电芯条码失败，内容{jStr},无法成功与调度通讯");
            //    return OperateResult.CreateFailedResult<string>($"请求电芯条码失败，内容{jStr}无法成功与调度通讯");
            //}
        }

        public OperateResult<string> UploadTestResult()
        {
            UploadOcvTestResultDto dto = new UploadOcvTestResultDto
            {
                EquipmentCode = EquipmentCode,
                PalletBarcode = Tray.TrayCode,
                FileName = _wmsSetting.FileName,
                BatteryTestFlag = Tray.NgInfos.Any(x => x.IsNg) ? 0 : 1,
                BatteryType = Tray.NgInfos.First().Battery.BatteryType == 0 ? "104" : "102",
                BatteryTestResults = new List<OneBatteryTestResult>()
            };
            foreach(var item in Tray.NgInfos)
            {
                OneBatteryTestResult tempResult = new OneBatteryTestResult
                {
                    BatteryBarcode = item.Battery.BarCode,
                    BatteryNgCode = item.IsNg?"Ng":"Ok",
                    Result = item.IsNg ? 0 : 1,
                    BatteryIndex = item.Battery.Position
                };
                dto.BatteryTestResults.Add(tempResult);
            }

            return Post(dto, UploadTestResultUrl, "上传测试结果");
            //string jStr = JsonConvert.SerializeObject(dto);
            //HttpContent content = new StringContent(jStr);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            //{
            //    CharSet = "utf-8"
            //};
            //var result = HttpClient.PostAsync(UploadTestResultUrl, content).Result;
            //if (result.IsSuccessStatusCode)
            //{
            //    string strResult = result.Content.ReadAsStringAsync().Result;
            //    LogHelper.WmsServiceLog.Info($"上传测试结果，请求内容{jStr},返回{strResult}");
            //    if (string.IsNullOrEmpty(strResult))
            //    {
            //        return OperateResult.CreateFailedResult<string>("上传测试结果到WMS失败，返回为空或null");
            //    }
            //    else if (!strResult.Contains("成功")) {

            //        return OperateResult.CreateFailedResult<string>("上传测试结果到WMS失败，"+ strResult);

            //    }
            //    else
            //    {
            //        return OperateResult.CreateSuccessResult<string>(strResult);
            //    }
            //}
            //else
            //{
            //    LogHelper.WmsServiceLog.Error($"上传测试结果到WMS失败，内容{jStr},无法成功与调度通讯");
            //    return OperateResult.CreateFailedResult<string>("请上传测试结果到WMS失败，无法成功与调度通讯");
            //}
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run(GetBatteriesInfo);
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run(UploadTestResult);
        }
        public HttpClient HttpClient { get; set; }
        private string GetBatteryInfoUrl => _wmsSetting.GetBatteryInfoUrl;
        private string UploadTestResultUrl => _wmsSetting.UploadTestResultUrl;
        private string EquipmentCode => _wmsSetting.EquipmentCode;

        public IHttpClientFactory HttpClientFactory { get; }
        public Tray Tray { get; }
        private readonly WmsSetting _wmsSetting;


        protected virtual OperateResult<string> Post(Object obj, string url, string optionName)
        {
            string jStr = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            {
                CharSet = "utf-8"
            };

            var requestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            var baseUri = HttpClient.BaseAddress;
            var fullUri = baseUri != null ? new Uri(baseUri, requestUri) : requestUri;

            var result = HttpClient.PostAsync(url, content).Result;
            if (result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.WmsServiceLog.Info($"{optionName}:请求地址:{fullUri.AbsoluteUri};请求内容:{jStr};返回{strResult}");
                if (string.IsNullOrEmpty(strResult))
                {
                    return OperateResult.CreateFailedResult<string>($"{optionName}失败，请求地址:{fullUri.AbsoluteUri};返回为空或null");
                }
                else
                {
                    return OperateResult.CreateSuccessResult<string>(strResult);
                }
            }
            else
            {
                LogHelper.WmsServiceLog.Error($"{optionName}失败，请求地址:{fullUri.AbsoluteUri};内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"{optionName}失败，请求地址:{fullUri.AbsoluteUri};内容{jStr}无法成功与调度通讯");
            }
        }
    }
}
