using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.DbContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Settings;
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
        public MesImpl(IHttpClientFactory httpClientFactory,Tray tray)
        {
            HttpClientFactory = httpClientFactory;
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
        public SettingManager SettingManager => SettingManager.Instance;
        public Tray Tray { get; }

        public OperateResult<string> BatteryDismantlingDiskDataUploadToMes()
        {
            if (string.IsNullOrEmpty(Tray.TrayCode))
            {
                return OperateResult.CreateFailedResult<string>("Mes获取到本地托盘条码为空");
            }
            MesDismantlingDiskDto mesUploadDto = new MesDismantlingDiskDto()
            {
                SITE = MesSetting?.Site??string.Empty,
                RESOURCE_NO = MesSetting?.ResourceNo??string.Empty,
                DC_USER = MesSetting?.DcUser??string.Empty,
                TRAY_NO = Tray.TrayCode,
                SHOP_ORDER_NO = SettingManager.Order ?? (SettingManager.OrderList?.First().Value ?? string.Empty),
                IS_FIRST_INSPECTION = true.ToString()
            };
            return Post(mesUploadDto, MesSetting?.SfcTrayOnceUnbindUrl?? "thirdPartyAPI!sfc_tray_once_unbind.action", "Mes拆盘数据上传");
        }

        public OperateResult<string> CapacitySortingNg()
        {
            throw new NotImplementedException();
        }

        public OperateResult<string> FormingNg()
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
                site = SettingManager.CurrentMesSetting?.Site ?? "25",
                machineNo = SettingManager.CurrentMesSetting?.ResourceNo ?? string.Empty,
                status = status.ToString().PadLeft(2, '0'),
                isShutdown = isShutdown ? "Y" : "N",
                message = message,
                Operator = SettingManager.CurrentMesSetting?.DcUser ?? "MACHINE_JCD",
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

        public OperateResult<string> VerifyDataOcv1Ocv2TestCabinet()
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
                Site = MesSetting?.Site??string.Empty,
                ItemNo = string.Empty,
                ShopOrderNo = SettingManager.Order ?? (SettingManager.OrderList?.First().Value ?? string.Empty),
                OperationNo = string.Empty,
                TrayNo = Tray.TrayCode,
                ResourceNo = MesSetting?.ResourceNo??string.Empty,
                Shifts = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班",
                IsFirstInspection = "Y",
                DcDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DcUser = MesSetting?.DcUser ?? string.Empty,
                OcvTimes = SettingManager.CurrentOcvType.ToString(),
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
                    //NG_REASON = item.NgDescription??string.Empty+item.ExtraNgDescription,
                    NG_REASON = item.NgDescription??string.Empty,
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
                    Ocv0Date = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SfcList = batteryResults;
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
                Site = MesSetting?.Site ?? string.Empty,
                ItemNo = string.Empty,
                ShopOrderNo = SettingManager.Order ?? (SettingManager.OrderList?.First().Value ?? string.Empty),
                OperationNo = string.Empty,
                TrayNo = Tray.TrayCode,
                ResourceNo = MesSetting?.ResourceNo ?? string.Empty,
                Shifts = DateTime.Now.Hour is >= 8 and < 20 ? "白班" : "夜班",
                IsFirstInspection = "Y",
                DcDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DcUser = MesSetting?.DcUser ?? string.Empty,
                OcvTimes = SettingManager.CurrentOcvType.ToString(),
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
                    //NG_REASON = item.NgDescription ?? string.Empty + item.ExtraNgDescription,
                    NG_REASON = item.NgDescription ?? string.Empty ,

                    OCV0 = string.Empty,
                    OCV0_MAX_VALUE = string.Empty,
                    OCV0_MIN_VALUE = string.Empty,
                    OCR0 = string.Empty,
                    OCR0_MAX_VALUE = string.Empty,
                    OCR0_MIN_VALUE = string.Empty,

                    Ocv1 = item.Battery.VolValue.ToString()??string.Empty,
                    Ocv1MinValue = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                    Ocv1MaxValue = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                    Ocr1 = item.Battery.Res.ToString()??string.Empty,
                    Ocr1MinValue = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                    Ocr1MaxValue = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,
                    CCCR = string.Empty,
                    CCCR_MIN_VALUE = string.Empty,
                    CCCR_MAX_VALUE = string.Empty,
                    SIDE_VOLTAGE = item.Battery.PVolValue.ToString()??string.Empty,
                    SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinPVol.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxPVol.ToString() ?? string.Empty,
                    LEVEL_NAME = string.Empty,
                    Ocv1Date = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SfcList = batteryResults;
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
                Site = MesSetting?.Site ?? string.Empty,
                ItemNo = string.Empty,
                ShopOrderNo = SettingManager.Order ?? (SettingManager.OrderList?.First().Value ?? string.Empty),
                OperationNo = string.Empty,
                TrayNo = Tray.TrayCode,
                ResourceNo = MesSetting?.ResourceNo ?? string.Empty,
                Shifts = DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 20 ? "白班" : "夜班",
                IsFirstInspection = "Y",
                DcDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DcUser = MesSetting?.DcUser ?? string.Empty,
                OcvTimes = SettingManager.CurrentOcvType.ToString(),
            };
            var ngInfos = Tray.NgInfos;
            List<MesBatteryResultDto> batteryResults = new List<MesBatteryResultDto>();

            //List<BatteryDto> batteryDtos = dbContext.BatteryDtos.Where(x => x.TrayCode == context.BatteryTray.TrayCode && x.OcvType == "OCV1").ToList();
            foreach (var item in ngInfos)
            {
                if (!item.Battery.IsExsit)
                {
                    continue;
                }
                MesBatteryResultForOcv2Dto batteryResultDtoForOcv0 = new MesBatteryResultForOcv2Dto
                {
                    LOCATION_NO = item.Battery.Position.ToString(),
                    SFC_NO = item.Battery.BarCode,
                    DC_RESULT = item.IsNg ? "NG" : "OK",
                    //NG_REASON = item.NgDescription ?? string.Empty + item.ExtraNgDescription,
                    NG_REASON = item.NgDescription ?? string.Empty,

                    OCV0 = string.Empty,
                    OCV0_MAX_VALUE = string.Empty,
                    OCV0_MIN_VALUE = string.Empty,
                    OCR0 = string.Empty,
                    OCR0_MAX_VALUE = string.Empty,
                    OCR0_MIN_VALUE = string.Empty,

                    Ocv1 = string.Empty,
                    Ocv1MinValue = string.Empty,
                    Ocv1MaxValue = string.Empty,
                    Ocr1 = string.Empty,
                    Ocr1MinValue = string.Empty,
                    Ocr1MaxValue = string.Empty,

                    Ocv2 = item.Battery.VolValue.ToString() ?? string.Empty,
                    Ocv2MinValue = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                    Ocv2MaxValue = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                    Ocr2 = item.Battery.Res?.ToString() ?? string.Empty,
                    Ocr2MinValue = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                    Ocr2MaxValue = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,
                    CCCR = string.Empty,
                    CCCR_MIN_VALUE = string.Empty,
                    CCCR_MAX_VALUE = string.Empty,
                    SIDE_VOLTAGE = item.Battery.PVolValue?.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MIN_VALUE = SettingManager.CurrentBatteryStandard?.MinPVol.ToString() ?? string.Empty,
                    SIDE_VOLTAGE_MAX_VALUE = SettingManager.CurrentBatteryStandard?.MaxPVol.ToString() ?? string.Empty,
                    LEVEL_NAME = string.Empty,
                    Ocv1Date = string.Empty,
                    Ocv2Date = item.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    K12 = item.Battery.KValue2?.ToString() ?? string.Empty,
                    K12MaxValue = string.Empty,
                    K12MinValue = string.Empty,
                    Ocv1Ocv2Internal = string.Empty,
                    Ocv1Ocv2InternalMaxValue = string.Empty,
                    Ocv1Ocv2InternalMinValue = string.Empty
                };
                batteryResults.Add(batteryResultDtoForOcv0);
            }
            mesUploadDto.SfcList = batteryResults;
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
                Site = MesSetting?.Site ?? string.Empty,
                ItemNo = string.Empty,
                ShopOrderNo = SettingManager.Order ?? (SettingManager.OrderList?.First().Value ?? string.Empty),
                OperationNo = string.Empty,
                TrayNo = Tray.TrayCode,
                ResourceNo = MesSetting?.ResourceNo ?? string.Empty,
                Shifts = ngInfo.Battery.TestTime.Hour >= 8 && ngInfo.Battery.TestTime.Hour < 20 ? "白班" : "夜班",
                IsFirstInspection = "Y",
                DcDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DcUser = MesSetting?.DcUser ?? string.Empty,
                OcvTimes = SettingManager.CurrentOcvType.ToString(),

                LocationNo = ngInfo.Battery.Position.ToString(),
                SfcNo = ngInfo.Battery.BarCode,
                DcResult = ngInfo.IsNg ? "NG" : "OK",
                NgReason = ngInfo.NgDescription ?? string.Empty,
                Ocv3Date = ngInfo.Battery.TestTime.ToString("yyyy-MM-dd HH:mm:ss"),

                Ocv3 = ngInfo.Battery.VolValue?.ToString() ?? string.Empty,
                Ocv3MinValue = SettingManager.CurrentBatteryStandard?.MinVol.ToString() ?? string.Empty,
                Ocv3MaxValue = SettingManager.CurrentBatteryStandard?.MaxVol.ToString() ?? string.Empty,
                Ocr3 = ngInfo.Battery.Res?.ToString() ?? string.Empty,
                Ocr3MinValue = SettingManager.CurrentBatteryStandard?.MinRes.ToString() ?? string.Empty,
                Ocr3MaxValue = SettingManager.CurrentBatteryStandard?.MaxRes.ToString() ?? string.Empty,

                CccrMinValue = string.Empty,
                CccrMaxValue = string.Empty,
                SideVoltage = ngInfo.Battery.NVolValue?.ToString() ?? string.Empty,
                SideVoltageMinValue = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                SideVoltageMaxValue = SettingManager.CurrentBatteryStandard?.MinNVol.ToString() ?? string.Empty,
                LevelName = string.Empty,

                K23 = ngInfo.Battery.KValue3.ToString() ?? string.Empty,
                K23MaxValue = SettingManager.CurrentBatteryStandard?.MaxKValue.ToString() ?? string.Empty,
                K23MinValue = SettingManager.CurrentBatteryStandard?.MinKValue.ToString() ?? string.Empty,
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

        public OperateResult<string> GetShopOrderList()
        {
            var obj = new MesGetShopOrderListRequestDto()
            {
                site = SettingManager.CurrentMesSetting?.Site ?? "25",
                resourceNo = MesSetting?.ResourceNo ?? string.Empty,
            };
            return Post(obj, MesSetting?.LoadShopOrderListUrl ?? "http://10.10.1.240:8578/mes/third/thirdPartyAPI!loadShopOrderList.action"
                , "获取工单列表");
        }
    }
}
