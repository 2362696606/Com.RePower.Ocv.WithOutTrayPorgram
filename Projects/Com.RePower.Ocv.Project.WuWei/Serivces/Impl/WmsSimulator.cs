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
    public class WmsSimulator : IWmsService
    {
        public OperateResult<string> GetBatteriesInfo()
        {
            WmsBatteriesInfoDto dto = new WmsBatteriesInfoDto()
            {
                Result = 1,
                Message = "成功"
            };
            dto.PileContent = new PileContent()
            {
                PalletBarcode = "trayCode0001",
                FileName = "Ocv0",
                BatteryType = "114",
            };
            List<OneBattery> batteries = new List<OneBattery>();
            for (int i = 1; i <= 38; i++) 
            {
                var oneBattery = new OneBattery()
                {
                    BatteryBarcode = $"batteryCode000{i.ToString()}",
                    PalletIndex = i,
                    IsRealBattery = 1,
                    IsNg = 0
                };
                batteries.Add(oneBattery);
            }
            dto.PileContent.Batterys = batteries;
            string jStr = JsonConvert.SerializeObject(dto);
            return OperateResult.CreateSuccessResult<string>(jStr);
        }

        public async Task<OperateResult<string>> GetBatteriesInfoAsync()
        {
            return await Task.Run<OperateResult<string>>(() =>
            {
                return GetBatteriesInfo();
            });
        }

        public OperateResult<string> UploadTestResult()
        {
            return OperateResult.CreateSuccessResult<string>("成功");
        }

        public async Task<OperateResult<string>> UploadTestResultAsync()
        {
            return await Task.Run<OperateResult<string>>(() =>
            {
                return UploadTestResult();
            });
        }
    }
}
