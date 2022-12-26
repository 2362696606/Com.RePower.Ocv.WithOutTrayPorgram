using Azure;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.WuWei.Serivces.Dto;
using Com.RePower.Ocv.Project.WuWei.Serivces.Module;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Serivces.Impl
{
    public class WmsService : IWmsService
    {
        public WmsService(IHttpClientFactory httpClientFactory,Tray tray,WmsSetting wmsSetting)
        {
            WmsSetting = wmsSetting;
            HttpClientFactory = httpClientFactory;
            HttpClient = httpClientFactory.CreateClient("WmsHttpClient");
            HttpClient.BaseAddress = new Uri(wmsSetting.BaseAddress);
            Tray = tray;
        }

        public OperateResult<string> GetBatteriesInfo()
        {
            var obj = new { PalletCode = Tray.TrayCode, EquipmentCode = EquipmentCode };
            string jStr = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "utf-8";
            var result = HttpClient.PostAsync(GetBatteryInfoUrl, content).Result;
            if(result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.WmsServiceLog.Info($"请求电芯条码，请求地址{result.RequestMessage?.RequestUri}，请求内容{jStr},返回{strResult}");
                if(string.IsNullOrEmpty(strResult))
                {
                    return OperateResult.CreateFailedResult<string>("请求电芯条码失败，返回为空或null");
                }
                else
                {
                    return OperateResult.CreateSuccessResult<string>(strResult);
                }
            }
            else
            {
                LogHelper.WmsServiceLog.Error($"请求电芯条码失败，内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"请求电芯条码失败，无法成功与调度通讯");
            }
        }

        public OperateResult<string> UploadTestResult()
        {
            UploadOCVTestResultDto dto = new UploadOCVTestResultDto();
            dto.EquipmentCode = EquipmentCode;
            dto.PalletBarcode = Tray.TrayCode;
            dto.FileName = WmsSetting.FileName;
            dto.BatteryTestFlag = Tray.NgInfos.Any(x => x.IsNg) ? 0 : 1;
            dto.BatteryType = Tray.NgInfos.First().Battery.BatteryType == 0 ? "104" : "102";
            dto.BatteryTestResults = new List<OneBatteryTestResult>();
            foreach(var item in Tray.NgInfos)
            {
                OneBatteryTestResult tempResult = new OneBatteryTestResult();
                tempResult.BatteryBarcode = item.Battery.BarCode;
                tempResult.BatteryNGCode = string.Empty;
                tempResult.Rseult = item.IsNg ? 0 : 1;
                tempResult.BatteryIndex = item.Battery.Position;
                dto.BatteryTestResults.Add(tempResult);
            }
            string jStr = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "utf-8";
            var result = HttpClient.PostAsync(GetBatteryInfoUrl, content).Result;
            if (result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.WmsServiceLog.Info($"上传测试结果，请求内容{jStr},返回{strResult}");
                if (string.IsNullOrEmpty(strResult))
                {
                    return OperateResult.CreateFailedResult<string>("上传测试结果到WMS失败，返回为空或null");
                }
                else
                {
                    return OperateResult.CreateSuccessResult<string>(strResult);
                }
            }
            else
            {
                LogHelper.WmsServiceLog.Error($"上传测试结果到WMS失败，内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"请上传测试结果到WMS失败，无法成功与调度通讯");
            }
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run<OperateResult<string>>(() => { return GetBatteriesInfo(); });
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run<OperateResult<string>>(() => { return UploadTestResult(); });
        }
        public HttpClient HttpClient { get; set; }
        public string GetBatteryInfoUrl { get { return WmsSetting.GetBatteryInfoUrl; } }
        public string UploadTestResultUrl { get { return WmsSetting.UploadTestResultUrl; } }
        public string BaseAddress { get { return WmsSetting.BaseAddress; } }
        public string EquipmentCode { get { return WmsSetting.EquipmentCode; } }

        public IHttpClientFactory HttpClientFactory { get; }
        public Tray Tray { get; }
        public WmsSetting WmsSetting { get; }
    }
}
