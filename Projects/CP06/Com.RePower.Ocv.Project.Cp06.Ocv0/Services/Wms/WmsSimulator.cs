using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Wms
{
    public class WmsSimulator : IWmsService
    {
        private SettingManager settingManager => SettingManager.Instance;

        public WmsSimulator(Tray tray)
        {
            Tray = tray;
        }

        public Tray Tray { get; }

        public OperateResult<string> GetBatteriesInfo()
        {
            WmsGetBatteriesInfoResultDto dto = new WmsGetBatteriesInfoResultDto()
            {
                Result = 1,
                Message = "请求成功"
            };
            switch(settingManager.CurrentOcvType)
            {
                case OcvTypeEnmu.OCV0:
                case OcvTypeEnmu.OCV3:
                    dto.HandleResult.Procedure = settingManager.CurrentOcvType.ToString();
                    break;
                case OcvTypeEnmu.OCV1:
                case OcvTypeEnmu.OCV2:
                    dto.HandleResult.Procedure = new Random().Next(2) == 1? OcvTypeEnmu.OCV1.ToString():OcvTypeEnmu.OCV2.ToString();
                    break;
            }

            //dto.HandleResult.Procedure = settingManager.CurrentOcvType.ToString();
            //dto.HandleResult.Skip = new Random().Next(2) == 1 ? true : false;
            //string randomNumStr = string.Format("{0:D5}", new Random().Next(10000));
            dto.HandleResult.TrayCode = Tray.TrayCode;
            int batteryConut = 0;
            foreach(var item in settingManager.CurrentTestOrder)
            {
                batteryConut += item.Count();
            }
            for(int i=0;i<batteryConut;i++)
            {
                var battery = new WmsBatteryInfo { Index = i + 1 };
                string batteryRandom = string.Format("{0:D5}", new Random().Next(10000));
                battery.BarCode = $"BatteryCode_{batteryRandom}";
                dto.HandleResult.BatteriesInfoList.Add(battery);
            }
            string jsonStr = JsonConvert.SerializeObject(dto);
            return OperateResult.CreateSuccessResult<string>(jsonStr);
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run<OperateResult<string>>(GetBatteriesInfo);
        }

        public OperateResult<string> RequestAllLocateCellToWms()
        {
            WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
            string jStr = JsonConvert.SerializeObject(dto);
            return OperateResult.CreateSuccessResult<string>(jStr);
        }

        public async Task<OperateResult<string>> RequestAllLocateCellToWmsAsync()
        {
            return await Task.Run<OperateResult<string>>(RequestAllLocateCellToWms);
        }

        public OperateResult<string> UploadTestResult()
        {
            //return OperateResult.CreateSuccessResult<string>("上传成功");
            WmsNormalReturnDto dto = new WmsNormalReturnDto { Result = 1, Message = "成功" };
            string jStr = JsonConvert.SerializeObject(dto);
            return OperateResult.CreateSuccessResult<string>(jStr);
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run<OperateResult<string>>(UploadTestResult);
        }
    }
}
