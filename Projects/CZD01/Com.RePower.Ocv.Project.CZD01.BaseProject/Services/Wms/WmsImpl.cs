using AutoMapper;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms
{
    public class WmsImpl:IWmsService
    {
        public WmsImpl(IHttpClientFactory? httpClientFactory, Tray tray, IMapper mapper)
        {
            HttpClientFactory = httpClientFactory;
            Tray = tray;
            Mapper = mapper;
        }
        protected SettingManager SettingManager { get => SettingManager.Instance; }
        protected IHttpClientFactory? HttpClientFactory { get; }
        protected Tray Tray { get; }
        protected IMapper Mapper { get; }
        public WmsSetting? WmsSetting { get => SettingManager.CurrentWmsSetting; }
        protected HttpClient HttpClient
        {
            get
            {
                var temp = HttpClientFactory?.CreateClient("WmsHttpClient_" + (SettingManager.CurrentOcvType.ToString()));
                if (temp is { })
                {
                    temp.BaseAddress = new Uri(WmsSetting?.BaseAddress ?? "http://172.17.2.200:44311/api/services/app/ForeignInterfaceService/");
                    return temp;
                }
                else
                {
                    return new HttpClient();
                }
            }
        }
        protected virtual OperateResult<string> Post(Object obj, string url, string optionName)
        {
            string jStr = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "utf-8";

            var requestUri = new Uri(url, UriKind.RelativeOrAbsolute);
            var baseUri = HttpClient.BaseAddress;
            Uri? fullUri = null;
            if(baseUri!=null)
            {
                fullUri = new Uri(baseUri, requestUri);
            }
            else
            {
                fullUri = requestUri;
            }

            var result = HttpClient.PostAsync(url, content).Result;
            if (result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.WmsServiceLog.Info($"{optionName}:请求地址:{fullUri?.AbsoluteUri};请求内容:{jStr};返回{strResult}");
                if (string.IsNullOrEmpty(strResult))
                {
                    return OperateResult.CreateFailedResult<string>($"{optionName}失败，请求地址:{fullUri?.AbsoluteUri};返回为空或null");
                }
                else
                {
                    return OperateResult.CreateSuccessResult<string>(strResult);
                }
            }
            else
            {
                LogHelper.WmsServiceLog.Error($"{optionName}失败，请求地址:{fullUri?.AbsoluteUri};内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"{optionName}失败，请求地址:{fullUri?.AbsoluteUri};内容{jStr}无法成功与调度通讯");
            }
        }

        public OperateResult<string> GetBatteriesInfo()
        {
            var requestDto = new WmsGetBatteriesInfoRequestDto
            {
                RequestLocation = WmsSetting?.RequestLocation ?? string.Empty,
                TrayCode = Tray.TrayCode,
            };
            return Post(requestDto, WmsSetting?.GetBatteryInfoUrl ?? "OcvGetTrayInfo", "请求电芯条码");
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run<OperateResult<string>>(GetBatteriesInfo);
        }

        public OperateResult<string> RequestAllLocateCellToWms()
        {
            WmsRequestAllRequestDto dto = new WmsRequestAllRequestDto
            {
                WhCode = WmsSetting?.WhCode ?? string.Empty,
                Location = WmsSetting?.Location ?? string.Empty,
                TrayBarcode = Tray.TrayCode,
                ProjectCode = WmsSetting?.ProjectCode ?? string.Empty,
            };
            return Post(dto, WmsSetting?.RequestAllocateCellToWmsUrl ?? "RequestAllocateCell", "上传测试总完成至调度");
        }

        public async Task<OperateResult<string>> RequestAllLocateCellToWmsAsync()
        {
            return await Task.Run<OperateResult<string>>(RequestAllLocateCellToWms);
        }

        public OperateResult<string> UploadTestResult()
        {
            var requestContent = new WmsUploadResultRequestDto()
            {
                WhCode = WmsSetting?.WhCode ?? string.Empty,
                TrayBarcode = Tray.TrayCode,
                DeviceName = SettingManager.CurrentOcvType.ToString(),
                Procedure = SettingManager.CurrentOcvType.ToString(),
                ProjectCode = SettingManager.CurrentWmsSetting?.ProjectCode ?? "CP06",
            };
            WmsResultDetails details = new WmsResultDetails();
            details.BatteryStandard = Mapper.Map<Settings.Dtos.BatteryStandardSettingDto>(SettingManager.CurrentBatteryStandard);
            details.NgInfos = new List<NgInfoDto>();
            foreach (var item in Tray.NgInfos)
            {
                var batteryResultContent = new WmsBatteryResultContent
                {
                    Barcode = item.Battery.BarCode,
                    TestResult = item.IsNg ? "Ng" : "Ok",
                    Index = item.Battery.Position
                };
                requestContent.BatteryResultContent.Add(batteryResultContent);
                NgInfoDto dto = Mapper.Map<NgInfoDto>(item);
                details.NgInfos.Add(dto);
            }
            requestContent.ResultDetails = details;
            return Post(requestContent, WmsSetting?.UploadTestResultUrl ?? "UploadOCVTestResult", "上传测试结果至调度");
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run<OperateResult<string>>(UploadTestResult);
        }
    }
}
