using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.DbContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Setting;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes
{
    public class MesImpl : IMesService
    {
        public MesImpl(IHttpClientFactory httpClientFactory,SettingManager settingManager,Tray tray)
        {
            HttpClientFactory = httpClientFactory;
            SettingManager = settingManager;
            Tray = tray;
        }
        public MesSetting? MesSetting
        {
            get => SettingManager.CurrentMesSetting;
        }
        public HttpClient HttpClient
        {
            get
            {
                var temp = HttpClientFactory.CreateClient("MesHttpClient_" + (SettingManager.CurrentOcvType.ToString()));
                temp.BaseAddress = new Uri(MesSetting?.BaseAddress ?? "http://10.10.1.240:8578/mes/third/");
                return temp;
            }
        }

        public IHttpClientFactory HttpClientFactory { get; }
        public SettingManager SettingManager { get; }
        public Tray Tray { get; }

        public OperateResult<string> BatteryDismantlingDiskDataUploadToMes()
        {
            if (string.IsNullOrEmpty(Tray.TrayCode))
            {
                return OperateResult.CreateFailedResult<string>("Mes获取到本地托盘条码为空");
            }
            MesDismantlingDiskDto mesUploadDto = new MesDismantlingDiskDto()
            {
                SITE = MesSetting?.SITE??string.Empty,
                RESOURCE_NO = MesSetting?.RESOURCE_NO??string.Empty,
                DC_USER = MesSetting?.DC_USER??string.Empty,
                TRAY_NO = Tray.TrayCode,
                SHOP_ORDER_NO = string.Empty,
                IS_FIRST_INSPECTION = true.ToString()
            };
            return Post(mesUploadDto, MesSetting?.SfcTrayOnceUnbindUrl?? "thirdPartyAPI!sfc_tray_once_unbind.action", "Mes拆盘数据上传");
        }

        public OperateResult<string> CapacitySortingNG()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> FormingNG()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> UploadHistoricalResult(List<NgInfoDto> ngInfos)
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> UploadingDeviceStatus(int status,bool isShutdown,string message = "")
        {
            MesDeviceStatusDto dto = new MesDeviceStatusDto()
            {
                Site = SettingManager.CurrentMesSetting?.SITE ?? "25",
                MachineNo = SettingManager.CurrentMesSetting?.RESOURCE_NO ?? string.Empty,
                Status = status.ToString().PadLeft(2, '0'),
                IsShutdown = isShutdown ? "Y" : "N",
                Message = message,
                Operator = SettingManager.CurrentMesSetting?.DC_USER ?? "MACHINE_JCD",
            };
            return Post(dto, SettingManager.CurrentMesSetting?.UploadMachineStatusUrl ?? "http://10.10.1.240:8578/mes/third/thirdPartyAPI!doUploadMachineStatus_Change.action", "上传设备状态至mes");
        }

        public OperateResult<string> UploadResult()
        {
            switch(SettingManager.CurrentOcvType)
            {
                case Enums.OcvTypeEnmu.OCV0:
                    return UploadResultForOcv0();
                case Enums.OcvTypeEnmu.OCV1:
                    return UploadResultForOcv1();
                case Enums.OcvTypeEnmu.OCV2:
                    return UploadResultForOcv2();
                case Enums.OcvTypeEnmu.OCV3:
                    return UploadResultForOcv3();
                default:
                    return OperateResult.CreateFailedResult<string>("未实现当前ocv工站mes上传业务");
            }
        }

        public OperateResult<string> UserAuthentication(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> VerifyDataOCV1OCV2TestCabinet()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Ocv0上传结果
        /// </summary>
        /// <returns></returns>
        protected OperateResult<string> UploadResultForOcv0()
        {
            MesUploadDto mesUploadDto = new MesUploadDto()
            {
                SITE = MesSetting?.SITE??string.Empty,
                ITEM_NO = string.Empty,
                SHOP_ORDER_NO = string.Empty,
                OPERATION_NO = string.Empty,
                TRAY_NO = Tray.TrayCode,
                RESOURCE_NO = MesSetting?.RESOURCE_NO??string.Empty,
                SHIFTS = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班",
                IS_FIRST_INSPECTION = "Y",
                DC_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DC_USER = MesSetting?.DC_USER ?? string.Empty,
                OCV_TIMES = SettingManager.CurrentOcvType.ToString(),
            };
            //var batteries = this.context.BatteryTray.Batteries;
            var ngInfos = Tray.NgInfos;
            List<MesBatteryResultDto> batteryResults = new List<MesBatteryResultDto>();
            foreach (var item in ngInfos)
            {
                MesBatteryResultForOcv0Dto batteryResultDtoForOcv0 = new MesBatteryResultForOcv0Dto
                {
                    LOCATION_NO = item.Battery.Position.ToString(),
                    SFC_NO = item.Battery.BarCode,
                    DC_RESULT = item.IsNg ? "NG" : "OK",
                    NG_REASON = item.NgDescription??string.Empty+item.ExtraNgDescription,
                    OCV0 = item.Battery.VolValue.ToString()??string.Empty,
                    OCV0_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinVol.ToString()??string.Empty,
                    OCV0_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                    OCR0 = item.Battery.Res.ToString()??string.Empty,
                    OCR0_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                    OCR0_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,
                    CCCR = string.Empty,
                    CCCR_MIN_VALUE = string.Empty,
                    CCCR_MAX_VALUE = string.Empty,
                    SIDE_VOLTAGE = item.Battery.NVolValue.ToString()??string.Empty,
                    SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxNVol.ToString() ?? string.Empty,
                    LEVEL_NAME = string.Empty,
                    OCV0_DATE = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SFC_LIST = batteryResults;
            return Post(mesUploadDto, MesSetting?.SaveDataAutoUrl ?? "thirdPartyAPI!saveData_autoOCV0ForE.action", "Mes上传测试结果");
        }
        /// <summary>
        /// Ocv1上传结果
        /// </summary>
        /// <returns></returns>
        private OperateResult<string> UploadResultForOcv1()
        {
            MesUploadDto mesUploadDto = new MesUploadDto()
            {
                SITE = MesSetting?.SITE ?? string.Empty,
                ITEM_NO = string.Empty,
                SHOP_ORDER_NO = string.Empty,
                OPERATION_NO = string.Empty,
                TRAY_NO = Tray.TrayCode,
                RESOURCE_NO = MesSetting?.RESOURCE_NO ?? string.Empty,
                SHIFTS = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班",
                IS_FIRST_INSPECTION = "Y",
                DC_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DC_USER = MesSetting?.DC_USER ?? string.Empty,
                OCV_TIMES = SettingManager.CurrentOcvType.ToString(),
            };
            var ngInfos = Tray.NgInfos;
            List<MesBatteryResultDto> batteryResults = new List<MesBatteryResultDto>();
            foreach (var item in ngInfos)
            {
                MesBatteryResultForOcv1Dto batteryResultDtoForOcv0 = new MesBatteryResultForOcv1Dto
                {
                    LOCATION_NO = item.Battery.Position.ToString(),
                    SFC_NO = item.Battery.BarCode,
                    DC_RESULT = item.IsNg ? "NG" : "OK",
                    NG_REASON = item.NgDescription ?? string.Empty + item.ExtraNgDescription,

                    OCV0 = string.Empty,
                    OCV0_MAX_VALUE = string.Empty,
                    OCV0_MIN_VALUE = string.Empty,
                    OCR0 = string.Empty,
                    OCR0_MAX_VALUE = string.Empty,
                    OCR0_MIN_VALUE = string.Empty,

                    OCV1 = item.Battery.VolValue.ToString()??string.Empty,
                    OCV1_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                    OCV1_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                    OCR1 = item.Battery.Res.ToString()??string.Empty,
                    OCR1_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                    OCR1_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,
                    CCCR = string.Empty,
                    CCCR_MIN_VALUE = string.Empty,
                    CCCR_MAX_VALUE = string.Empty,
                    SIDE_VOLTAGE = item.Battery.NVolValue.ToString()??string.Empty,
                    SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxNVol.ToString() ?? string.Empty,
                    LEVEL_NAME = string.Empty,
                    OCV1_DATE = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SFC_LIST = batteryResults;
            return Post(mesUploadDto, MesSetting?.SaveDataAutoUrl ?? "thirdPartyAPI!saveData_autoOCV1ForE.action", "Mes上传测试结果");
        }
        /// <summary>
        /// Ocv2上传结果
        /// </summary>
        /// <returns></returns>
        private OperateResult<string> UploadResultForOcv2()
        {
            MesUploadDto mesUploadDto = new MesUploadDto()
            {
                SITE = MesSetting?.SITE ?? string.Empty,
                ITEM_NO = string.Empty,
                SHOP_ORDER_NO = string.Empty,
                OPERATION_NO = string.Empty,
                TRAY_NO = Tray.TrayCode,
                RESOURCE_NO = MesSetting?.RESOURCE_NO ?? string.Empty,
                SHIFTS = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班",
                IS_FIRST_INSPECTION = "Y",
                DC_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DC_USER = MesSetting?.DC_USER ?? string.Empty,
                OCV_TIMES = SettingManager.CurrentOcvType.ToString(),
            };
            var ngInfos = Tray.NgInfos;
            List<MesBatteryResultDto> batteryResults = new List<MesBatteryResultDto>();

            //List<BatteryDto> batteryDtos = dbContext.BatteryDtos.Where(x => x.TrayCode == context.BatteryTray.TrayCode && x.OcvType == "OCV1").ToList();
            foreach (var item in ngInfos)
            {
                MesBatteryResultForOcv2Dto batteryResultDtoForOcv0 = new MesBatteryResultForOcv2Dto
                {
                    LOCATION_NO = item.Battery.Position.ToString(),
                    SFC_NO = item.Battery.BarCode,
                    DC_RESULT = item.IsNg ? "NG" : "OK",
                    NG_REASON = item.NgDescription ?? string.Empty + item.ExtraNgDescription,

                    OCV0 = string.Empty,
                    OCV0_MAX_VALUE = string.Empty,
                    OCV0_MIN_VALUE = string.Empty,
                    OCR0 = string.Empty,
                    OCR0_MAX_VALUE = string.Empty,
                    OCR0_MIN_VALUE = string.Empty,

                    OCV1 = string.Empty,
                    OCV1_MIN_VALUE = string.Empty,
                    OCV1_MAX_VALUE = string.Empty,
                    OCR1 = string.Empty,
                    OCR1_MIN_VALUE = string.Empty,
                    OCR1_MAX_VALUE = string.Empty,

                    OCV2 = item.Battery.VolValue.ToString() ?? string.Empty,
                    OCV2_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                    OCV2_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                    OCR2 = item.Battery.Res?.ToString() ?? string.Empty,
                    OCR2_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                    OCR2_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,
                    CCCR = string.Empty,
                    CCCR_MIN_VALUE = string.Empty,
                    CCCR_MAX_VALUE = string.Empty,
                    SIDE_VOLTAGE = item.Battery.NVolValue?.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxNVol.ToString() ?? string.Empty,
                    LEVEL_NAME = string.Empty,
                    OCV1_DATE = string.Empty,
                    OCV2_DATE = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    K12 = item.Battery.KValue2?.ToString() ?? string.Empty,
                    K12_MAX_VALUE = string.Empty,
                    K12_MIN_VALUE = string.Empty,
                    OCV1_OCV2_INTERNAL = string.Empty,
                    OCV1_OCV2_INTERNAL_MAX_VALUE = string.Empty,
                    OCV1_OCV2_INTERNAL_MIN_VALUE = string.Empty
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SFC_LIST = batteryResults;
            return Post(mesUploadDto, MesSetting?.SaveDataAutoUrl ?? "thirdPartyAPI!saveData_autoOCV2ForE.action", "Mes上传测试结果");
        }
        /// <summary>
        /// Ocv3上传结果
        /// </summary>
        /// <returns></returns>
        private OperateResult<string> UploadResultForOcv3()
        {
            var ngInfo = Tray.NgInfos[0];
            MesUploadDtoForOcv3Dto dto = new MesUploadDtoForOcv3Dto
            {
                SITE = MesSetting?.SITE ?? string.Empty,
                ITEM_NO = string.Empty,
                SHOP_ORDER_NO = string.Empty,
                OPERATION_NO = string.Empty,
                TRAY_NO = Tray.TrayCode,
                RESOURCE_NO = MesSetting?.RESOURCE_NO ?? string.Empty,
                SHIFTS = ngInfo.Battery.TestTime.Hour >= 8 && ngInfo.Battery.TestTime.Hour < 20 ? "白班" : "夜班",
                IS_FIRST_INSPECTION = "Y",
                DC_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DC_USER = MesSetting?.DC_USER ?? string.Empty,
                OCV_TIMES = SettingManager.CurrentOcvType.ToString(),

                LOCATION_NO = ngInfo.Battery.Position.ToString(),
                SFC_NO = ngInfo.Battery.BarCode,
                DC_RESULT = ngInfo.IsNg ? "NG" : "OK",
                NG_REASON = ngInfo.NgDescription ?? string.Empty,
                OCV3_DATE = ngInfo.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),

                OCV3 = ngInfo.Battery.VolValue?.ToString() ?? string.Empty,
                OCV3_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                OCV3_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                OCR3 = ngInfo.Battery.Res?.ToString() ?? string.Empty,
                OCR3_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                OCR3_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,

                CCCR_MIN_VALUE = string.Empty,
                CCCR_MAX_VALUE = string.Empty,
                SIDE_VOLTAGE = ngInfo.Battery.NVolValue?.ToString() ?? string.Empty,
                SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                LEVEL_NAME = string.Empty,

                K23 = ngInfo.Battery.KValue3.ToString() ?? string.Empty,
                K23_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxKValue.ToString() ?? string.Empty,
                K23_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinKValue.ToString() ?? string.Empty,
            };
            return Post(dto, MesSetting?.SaveDataAutoUrl ?? "thirdPartyAPI!saveData_autoOCV2ForE.action", "Mes上传测试结果");
        }
        protected virtual OperateResult<string> Post(Object obj, string url, string optionName)
        {
            string jStr = JsonConvert.SerializeObject(obj);
            HttpContent content = new StringContent(jStr);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentType.CharSet = "utf-8";

            var result = HttpClient.PostAsync(url, content).Result;
            if (result.IsSuccessStatusCode)
            {
                string strResult = result.Content.ReadAsStringAsync().Result;
                LogHelper.MesServiceLog.Info($"{optionName}，请求内容{jStr},返回{strResult}");
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
                LogHelper.MesServiceLog.Error($"{optionName}失败，内容{jStr},无法成功与调度通讯");
                return OperateResult.CreateFailedResult<string>($"{optionName}失败，内容{jStr}无法成功与调度通讯");
            }
        }
    }
}
