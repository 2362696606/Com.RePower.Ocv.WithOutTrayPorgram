using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Controllers;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WmsWebService;

namespace Com.RePower.Ocv.Project.Byd.CB15.Services.Wms
{
    public class WmsImpl : IWmsService
    {
        public WmsImpl(Tray tray, IMapper mapper)
        {
            Tray = tray;
            Mapper = mapper;
            switch (SettingManager.CurrentOcvType)
            {
                case Enums.OcvTypeEnmu.OCV1:
                case Enums.OcvTypeEnmu.OCV2:
                    WebServiceClient = new OCVWebServiceSoapClient(OCVWebServiceSoapClient.EndpointConfiguration.OCVWebServiceSoap12);
                    break;
                case Enums.OcvTypeEnmu.OCV3:
                case Enums.OcvTypeEnmu.OCV4:
                    WebServiceClient = new OCVWebServiceSoapClient(OCVWebServiceSoapClient.EndpointConfiguration.OCVWebServiceSoap);
                    break;
                default:
                    WebServiceClient = new OCVWebServiceSoapClient(OCVWebServiceSoapClient.EndpointConfiguration.OCVWebServiceSoap);
                    break;
            }
        }

        public Tray Tray { get; }
        public SettingManager SettingManager => SettingManager.Instance;
        public IMapper Mapper { get; }
        public OCVWebServiceSoapClient WebServiceClient { get; set; }

        public OperateResult<TrayInfoOCV> GetTechnologyInfoByBarCode()
        {
            try
            {
                var address = WebServiceClient.Endpoint.Address;
                var name = WebServiceClient.Endpoint.Name;
                var result = WebServiceClient.getTechnologyInfoByBarCodeAsync(SettingManager.CurrentWmeSetting?.EquipNum ?? string.Empty, Tray.TrayCode).Result;
                LogHelper.WmsServiceLog.Info($"获取电芯条码:\r\nEndpointName:{name};\r\nAddress:{address};\r\n参数:{SettingManager.CurrentWmeSetting?.EquipNum ?? ""},{Tray.TrayCode}\r\nResult:{JsonConvert.SerializeObject(result)}");
                return OperateResult.CreateSuccessResult(result.Body.getTechnologyInfoByBarCodeResult);
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult<TrayInfoOCV>(e.Message);
            }
        }

        public OperateResult<Result> GetTestResultByTrayCode()
        {
            try
            {
                string equipNum = SettingManager.CurrentWmeSetting?.EquipNum??string.Empty;
                string barCode = Tray.TrayCode??string.Empty;
                List<BatteryInfo> batteryInfos = new List<BatteryInfo>();
                foreach(var item in Tray.NgInfos)
                {
                    var temp = new BatteryInfo
                    {
                        Position = item.Battery.Position,
                        BatteryCode = item.Battery.BarCode,
                        Result = item.IsNg ? 1 : 0
                    };
                    batteryInfos.Add(temp);
                }
                var result = WebServiceClient.getOCVTestResultByBarCodeAsync(equipNum, barCode, batteryInfos.ToArray()).Result;
                
                return OperateResult.CreateSuccessResult(result.Body.getOCVTestResultByBarCodeResult);
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult<Result>(e.Message);
            }
        }

        public OperateResult<TrayTypeOCV> GetTrayType()
        {
            try
            {
                var result = WebServiceClient.getTrayTpyeAsync(SettingManager.CurrentWmeSetting?.EquipNum ?? string.Empty, Tray.TrayCode).Result;
                
                return OperateResult.CreateSuccessResult(result.Body.getTrayTpyeResult);
            }
            catch (Exception e)
            {
                return OperateResult.CreateFailedResult<TrayTypeOCV>(e.Message);
            }
        }

        public OperateResult<Status> UpdateOCVStatus()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var result = WebServiceClient.UpdateOCVStatusAsync(SettingManager.CurrentWmeSetting?.EquipNum ?? string.Empty, 2, 1, string.Empty).Result;
                
            //    return OperateResult.CreateSuccessResult(result.Body.UpdateOCVStatusResult);
            //}
            //catch (Exception e)
            //{
            //    return OperateResult.CreateFailedResult<Status>(e.Message);
            //}
        }
    }
}
