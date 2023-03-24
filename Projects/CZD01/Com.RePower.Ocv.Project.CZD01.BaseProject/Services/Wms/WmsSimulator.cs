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
                        WmsGetBatteriesInfoResultDto dto = new WmsGetBatteriesInfoResultDto()
                        {
                            Result = 1,
                            Message = "请求成功"
                        };
                        //switch (SettingManager.CurrentOcvType)
                        //{
                        //    case Model.Enums.OcvTypeEnum.OCV0:
                        //    case Model.Enums.OcvTypeEnum.OCV3:
                        //        dto.HandleResult.Procedure = SettingManager.CurrentOcvType.ToString();
                        //        break;
                        //    case Model.Enums.OcvTypeEnum.OCV1:
                        //    case Model.Enums.OcvTypeEnum.OCV2:
                        //        dto.HandleResult.Procedure = new Random().Next(2) == 1 ? Model.Enums.OcvTypeEnum.OCV1.ToString() : Model.Enums.OcvTypeEnum.OCV2.ToString();
                        //        break;
                        //}
                        dto.HandleResult.Procedure = SettingManager.CurrentOcvType.ToString();
                        //dto.HandleResult.Procedure = settingManager.CurrentOcvType.ToString();
                        //dto.HandleResult.Skip = new Random().Next(2) == 1 ? true : false;
                        //string randomNumStr = string.Format("{0:D5}", new Random().Next(10000));
                        dto.HandleResult.TrayCode = Tray.TrayCode;
                        int batteryConut = 0;
                        foreach (var item in SettingManager.CurrentTestOrder ?? new List<List<int>>())
                        {
                            batteryConut += item.Count();
                        }
                        for (int i = 0; i < batteryConut; i++)
                        {
                            var battery = new WmsBatteryInfo { Index = i + 1 };
                            string batteryRandom = string.Format("{0:D5}", new Random().Next(10000));
                            battery.BarCode = $"BatteryCode_{batteryRandom}";
                            dto.HandleResult.BatteriesInfoList.Add(battery);
                        }
                        returnStr = JsonConvert.SerializeObject(dto);
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
