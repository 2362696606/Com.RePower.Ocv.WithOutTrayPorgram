using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.WpfBase;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmsWebService;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms
{
    public class WmsSimulator : IWmsService
    {
        public WmsSimulator(Tray tray, IMapper mapper)
        {
            Tray = tray;
            Mapper = mapper;
        }

        public Tray Tray { get; }
        public SettingManager SettingManager => SettingManager.Instance;
        public IMapper Mapper { get; }

        public OperateResult<TrayInfoOCV> GetTechnologyInfoByBarCode()
        {
            TrayInfoOCV trayInfoOCV = new TrayInfoOCV();
            trayInfoOCV.EquipNum = SettingManager.CurrentWmeSetting?.EquipNum;
            trayInfoOCV.trayCode = Tray.TrayCode;
            int ocvType;
            switch(SettingManager.CurrentOcvType)
            {
                case Enums.OcvTypeEnmu.OCV1:
                    ocvType = 1;
                    break;
                case Enums.OcvTypeEnmu.OCV2:
                    ocvType = 2;
                    break;
                case Enums.OcvTypeEnmu.OCV3:
                    ocvType = 3;
                    break;
                case Enums.OcvTypeEnmu.OCV4:
                    ocvType = 4;
                    break;
                default: ocvType = 0; break;
            }
            trayInfoOCV.oprationType = ocvType;
            trayInfoOCV.oprationVersion = string.Empty;
            List<TrayInfoResult> trayInfoResults = new List<TrayInfoResult>();
            for(int i = 1;i<=SettingManager.CurrentTestOption?.BatteryCount;i++)
            {
                TrayInfoResult trayInfoResult = new TrayInfoResult();
                string batteryRandom = string.Format("{0:D5}", new Random().Next(10000));
                trayInfoResult.BatteryCode = $"BatteryCode_{batteryRandom}";
                trayInfoResult.Position = i;
                trayInfoResults.Add(trayInfoResult);
            }

            var r = new Random();
            for (var i = trayInfoResults.Count() - 1; i > 0; --i)
            {
                int randomIndex = r.Next(i + 1);

                var temp = trayInfoResults[i];
                trayInfoResults[i] = trayInfoResults[randomIndex];
                trayInfoResults[randomIndex] = temp;
            }

            trayInfoOCV.TrayInfoLstResult = trayInfoResults.ToArray();
            Status status = new Status();
            status.Code = 0;
            status.Message = string.Empty;
            trayInfoOCV.Status = status;
            return OperateResult.CreateSuccessResult(trayInfoOCV);
        }

        public OperateResult<Result> GetTestResultByTrayCode()
        {
            Result result = new Result();
            result.Code = 0;
            result.Msg = string.Empty;
            return OperateResult.CreateSuccessResult(result);
        }

        public OperateResult<TrayTypeOCV> GetTrayType()
        {
            TrayTypeOCV result = new TrayTypeOCV();
            int value = new Random().Next(1, 2);
            result.TrayType = value.ToString();
            return OperateResult.CreateSuccessResult(result);
        }

        public OperateResult<Status> UpdateOCVStatus()
        {
            throw new NotImplementedException();
        }
    }
}
