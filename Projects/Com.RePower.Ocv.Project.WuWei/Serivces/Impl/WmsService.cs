using Azure;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.WuWei.Serivces.Dto;
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
        public WmsService(IHttpClientFactory httpClientFactory,Tray tray)
        {
            HttpClient = httpClientFactory.CreateClient("WmsHttpClient");
            HttpClient.BaseAddress = new Uri(BaseAddress);
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
                LogHelper.WmsServiceLog.Info($"请求电芯条码，请求内容{jStr},返回{strResult}");
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
            throw new NotImplementedException();
        }

        public Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OperateResult<string>> UploadTestResultAsync()
        {
            throw new NotImplementedException();
        }

        public string GetBatteryInfoUrl { get; set; } = "RequetEquipmentStationBingAsset";
        public string UploadTestResultUrl { get; set; } = "UploadOCVTestResult";
        public string BaseAddress { get; set; } = "https://da49d2a1-05ff-4fcd-b537-2f74d2290138.mock.pstmn.io";
        public string EquipmentCode { get; set; } = string.Empty;
        public HttpClient HttpClient { get; }
        public Tray Tray { get; }
    }
}
