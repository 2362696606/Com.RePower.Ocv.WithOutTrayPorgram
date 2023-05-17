using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms.Dtos;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Services.Wms
{
    public class WmsSimulator : WmsImpl
    {

        public WmsSimulator(Tray tray, IMapper mapper) : base(null, tray, mapper)
        {
        }

        protected override OperateResult<string> Post(object obj, string url, string optionName)
        {
            string serializeStr = JsonConvert.SerializeObject(obj);
            string returnStr = string.Empty;
            switch(url)
            {
                case "OcvGetTrayInfo":
                    {
                        //WmsGetBatteriesInfoResultDto dto = new WmsGetBatteriesInfoResultDto()
                        //{
                        //    Result = 1,
                        //    Message = "请求成功"
                        //};
                        //dto.HandleResult.Procedure = SettingManager.CurrentOcvType.ToString();
                        //dto.HandleResult.TrayCode = Tray.TrayCode;
                        //int batteryConut = 0;
                        //foreach (var item in SettingManager.CurrentTestOrder ?? new List<List<int>>())
                        //{
                        //    batteryConut += item.Count();
                        //}
                        //for (int i = 0; i < batteryConut; i++)
                        //{
                        //    var battery = new WmsBatteryInfo { Index = i + 1 };
                        //    string batteryRandom = string.Format("{0:D5}", new Random().Next(10000));
                        //    battery.BarCode = $"BatteryCode_{batteryRandom}";
                        //    dto.HandleResult.BatteriesInfoList.Add(battery);
                        //}
                        //returnStr = JsonConvert.SerializeObject(dto);
                        returnStr =
                            "{\"handleResult\":{\"requestLocation\":\"FrontOcvStation:1_1_2_L01\",\"trayCode\":\"ZS01020204\",\"procedure\":\"OCV3\",\"batteriesInfoList\":[{\"barcode\":\"TLBCBDA12D3011D301210056RC\",\"index\":1,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBDA12D3011D301210015RC\",\"index\":2,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBDA12D3011D301210060RC\",\"index\":3,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBAA22D4011D451120121\",\"index\":4,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBAA22D4011D451120131\",\"index\":5,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBDA12D4011D411210065RC\",\"index\":6,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD2011D2H12X0014RC\",\"index\":7,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3J12X0160RC\",\"index\":8,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3E12X0152RC\",\"index\":9,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0036RC\",\"index\":10,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0118RC\",\"index\":11,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0107RC\",\"index\":12,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3212X0090RC\",\"index\":13,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0049RC\",\"index\":14,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3H12X0039RC\",\"index\":15,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3E12X0006RC\",\"index\":16,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3J12X0161RC\",\"index\":17,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"123456\",\"index\":18,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0113RC\",\"index\":19,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3H12X0027RC\",\"index\":20,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3F12X0127RC\",\"index\":21,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3H12X0025RC\",\"index\":22,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3212X0033RC\",\"index\":23,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD2011D2J12X0009RC\",\"index\":24,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD2011D2K12X0026RC\",\"index\":25,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3A12X0004RC\",\"index\":26,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3312X0138RC\",\"index\":27,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3912X0152RC\",\"index\":28,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3A12X0173RC\",\"index\":29,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3212X0062RC\",\"index\":30,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD2011D2L12X0009RC\",\"index\":31,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3A12X0150RC\",\"index\":32,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3312X0140RC\",\"index\":33,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCB27DAD3011D3212X0048RC\",\"index\":34,\"isExist\":false,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBDA12D3011D301210066RC\",\"index\":35,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}},{\"barcode\":\"TLBCBDA12D4011D411210147RC\",\"index\":36,\"isExist\":true,\"attachedNgInfo\":{\"isOk\":true,\"previousStation\":\"OCV3\",\"message\":\"\"}}],\"taskCode\":638172433216086305},\"result\":1,\"message\":\"请求成功\",\"errorCode\":0}";
                        break;
                    }
                case "RequestAllocateCell":
                    {
                        WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
                        returnStr = JsonConvert.SerializeObject(dto);
                        break;
                    }
                case "UploadOCVTestResult":
                default:
                    {
                        //return OperateResult.CreateSuccessResult<string>("上传成功");
                        WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
                        returnStr = JsonConvert.SerializeObject(dto);
                        break;
                    }
            }
            LogHelper.WmsServiceLog.Info($"{optionName}，请求内容{serializeStr},返回{returnStr}");
            return OperateResult.CreateSuccessResult<string>(returnStr);
        }

        //public OperateResult<string> GetBatteriesInfo()
        //{
        //    WmsGetBatteriesInfoResultDto dto = new WmsGetBatteriesInfoResultDto()
        //    {
        //        Result = 1,
        //        Message = "请求成功"
        //    };
        //    //switch (SettingManager.CurrentOcvType)
        //    //{
        //    //    case Model.Enums.OcvTypeEnum.OCV0:
        //    //    case Model.Enums.OcvTypeEnum.OCV3:
        //    //        dto.HandleResult.Procedure = SettingManager.CurrentOcvType.ToString();
        //    //        break;
        //    //    case Model.Enums.OcvTypeEnum.OCV1:
        //    //    case Model.Enums.OcvTypeEnum.OCV2:
        //    //        dto.HandleResult.Procedure = new Random().Next(2) == 1 ? Model.Enums.OcvTypeEnum.OCV1.ToString() : Model.Enums.OcvTypeEnum.OCV2.ToString();
        //    //        break;
        //    //}
        //    dto.HandleResult.Procedure = SettingManager.CurrentOcvType.ToString();
        //    //dto.HandleResult.Procedure = settingManager.CurrentOcvType.ToString();
        //    //dto.HandleResult.Skip = new Random().Next(2) == 1 ? true : false;
        //    //string randomNumStr = string.Format("{0:D5}", new Random().Next(10000));
        //    dto.HandleResult.TrayCode = Tray.TrayCode;
        //    int batteryConut = 0;
        //    foreach (var item in SettingManager.CurrentTestOrder ?? new List<List<int>>()) 
        //    {
        //        batteryConut += item.Count();
        //    }
        //    for (int i = 0; i < batteryConut; i++)
        //    {
        //        var battery = new WmsBatteryInfo { Index = i + 1 };
        //        string batteryRandom = string.Format("{0:D5}", new Random().Next(10000));
        //        battery.BarCode = $"BatteryCode_{batteryRandom}";
        //        dto.HandleResult.BatteriesInfoList.Add(battery);
        //    }
        //    string jsonStr = JsonConvert.SerializeObject(dto);
        //    return OperateResult.CreateSuccessResult<string>(jsonStr);
        //}


        //public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        //{
        //    return await Task.Run<OperateResult<string>>(GetBatteriesInfo);
        //}

        //public OperateResult<string> RequestAllLocateCellToWms()
        //{
        //    WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
        //    string jStr = JsonConvert.SerializeObject(dto);
        //    return OperateResult.CreateSuccessResult<string>(jStr);
        //}

        //public async Task<OperateResult<string>> RequestAllLocateCellToWmsAsync()
        //{
        //    return await Task.Run<OperateResult<string>>(RequestAllLocateCellToWms);
        //}

        //public OperateResult<string> UploadTestResult()
        //{
        //    //return OperateResult.CreateSuccessResult<string>("上传成功");
        //    WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
        //    string jStr = JsonConvert.SerializeObject(dto);
        //    return OperateResult.CreateSuccessResult<string>(jStr);
        //}

        //public async Task<OperateResult<string>> UploadTestResultAsync()
        //{
        //    return await Task.Run<OperateResult<string>>(UploadTestResult);
        //}
    }
}
