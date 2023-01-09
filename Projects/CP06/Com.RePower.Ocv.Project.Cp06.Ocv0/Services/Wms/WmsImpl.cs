using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Wms
{
    public class WmsImpl : IWmsService
    {
        private readonly SettingManager settingManager;

        public WmsImpl(IHttpClientFactory httpClientFactory, Tray tray, SettingManager settingManager)
        {
            this.settingManager = settingManager;
            HttpClientFactory = httpClientFactory;
            HttpClient = httpClientFactory.CreateClient("WmsHttpClient");
            HttpClient.BaseAddress = new Uri(WmsSetting.BaseAddress);
            Tray = tray;
        }


        public WmsSetting WmsSetting { get => settingManager.CurrentWmsSetting; }
        public IHttpClientFactory HttpClientFactory { get; private set; }
        public HttpClient HttpClient { get; private set; }
        public Tray Tray { get; private set; }

        public OperateResult<string> GetBatteriesInfo()
        {
            var requestDto = new WmsGetBatteriesInfoRequestDto
            {
                RequestLocation = WmsSetting.RequestLocation,
                TrayCode = Tray.TrayCode
            };
            return Post(requestDto, WmsSetting.GetBatteryInfoUrl, "请求电芯条码");
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run<OperateResult<string>>(GetBatteriesInfo);
        }

        public OperateResult<string> UploadTestResult()
        {
            var requestContent = new WmsUploadResultRequestDto()
            {
                WhCode = WmsSetting.WhCode,
                TrayBarcode = Tray.TrayCode,
            };
            foreach(var item in Tray.NgInfos)
            {
                var batteryResultContent = new WmsBatteryResultContent();
            }
            return OperateResult.CreateSuccessResult<string>(string.Empty);
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run<OperateResult<string>>(UploadTestResult);
        }

        private OperateResult<string> Post(Object obj, string url,string optionName)
        {
            string jStr = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "utf-8";

            var result = HttpClient.PostAsync(url, content).Result;
            if (result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.WmsServiceLog.Info($"{optionName}，请求内容{jStr},返回{strResult}");
                if (string.IsNullOrEmpty(strResult))
                {
                    return OperateResult.CreateFailedResult<string>($"{optionName}失败，返回为空或null");
                }
                else
                {
                    return OperateResult.CreateSuccessResult<string>(strResult);
                }
            }
            else
            {
                LogHelper.WmsServiceLog.Error($"{optionName}失败，内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"{optionName}失败，内容{jStr}无法成功与调度通讯");
            }
        }
    }
}
