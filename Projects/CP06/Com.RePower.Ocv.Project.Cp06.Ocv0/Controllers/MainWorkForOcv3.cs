using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Dto;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Enums;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Cp06.Ocv0.DbContext;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Enums;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Dto;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Mes;
using Com.RePower.Ocv.Project.Cp06.Ocv0.Services.Wms;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public class MainWorkForOcv3 : MainWorkAbstract
    {
        public MainWorkForOcv3(DevicesController devicesController
            ,IWmsService wmsService
            ,IMesService mesService
            ,SettingManager settingManager
            ,Tray tray
            ,IMapper mapper)
        {
            DevicesController = devicesController;
            WmsService = wmsService;
            MesService = mesService;
            SettingManager = settingManager;
            Tray = tray;
            Mapper = mapper;
        }

        public DevicesController DevicesController { get; }
        public IWmsService WmsService { get; }
        public IMesService MesService { get; }
        public SettingManager SettingManager { get; }
        public Tray Tray { get; }
        public IMapper Mapper { get; }

        protected override OperateResult DoWork()
        {
            while(true)
            {
                var testPreparationResult = TestPreparation();
                if (testPreparationResult.IsFailed)
                    return testPreparationResult;
                DoPauseOrStop();
                var getBatteryCodeResult = GetBatteryCode();
                if(getBatteryCodeResult.IsFailed)
                    return getBatteryCodeResult;
                DoPauseOrStop();
                var testBatteryResult = TestBattery();
                if(testBatteryResult.IsFailed)
                    return testBatteryResult;
                DoPauseOrStop();
                var retestResult = RetestBattery();
                if (retestResult.IsFailed)
                    return retestResult;
                DoPauseOrStop();
                var verfyKValueResult = VerifyKValue();
                if (verfyKValueResult.IsFailed)
                    return verfyKValueResult;
                DoPauseOrStop();
                var saveToLocationResult = SaveToLocation();
                if (saveToLocationResult.IsFailed)
                    return saveToLocationResult;
                DoPauseOrStop();
                //var saveToSceneDbResult = SaveToSceneDb();
                //if (saveToSceneDbResult.IsFailed)
                //    return saveToSceneDbResult;
                //DoPauseOrStop();
                var uploadToMesResult = UploadToMes();
                if(uploadToMesResult.IsFailed)
                    return uploadToMesResult;
                var unBindingBatteryResult = UnBindingBattery();
                if(unBindingBatteryResult.IsFailed)
                    return unBindingBatteryResult;
                //var uploadToWmsResult = UploadTestResultToWms();
                //if (uploadToWmsResult.IsFailed)
                //    return uploadToWmsResult;
            }
        }
        /// <summary>
        /// 测试准备
        /// </summary>
        /// <returns></returns>
        private OperateResult TestPreparation()
        {
            if (!DevicesController.Plc.IsConnected)
            {
                LogHelper.UiLog.Info("连接plc");
                var result = DevicesController.Plc.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接plc");
                }
                else
                {
                    return result;
                }
            }
            if (!(DevicesController.DMM?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接万用表");
                var result = DevicesController.DMM.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接万用表");
                }
                else
                {
                    return result;
                }
            }
            if (!(DevicesController.SwitchBoard?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接切换板");
                }
                else
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("正在等待PLC请求测试");
            LogHelper.UiLog.Info("测试请求信号为1");
            var waitResult = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["测试状态(Ocv3)"], (short)1, cancellation: FlowController.CancelToken);
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            LogHelper.UiLog.Info("测试请求信号为1");
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取电芯条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetBatteryCode()
        {
            this.Tray.TrayCode = string.Empty;
            List<NgInfo> tempNgInfo = new List<NgInfo>();
            LogHelper.UiLog.Info("读取电芯条码");
            var readBatteryCodeResult = DevicesController.Plc.ReadString(DevicesController.PlcAddressCache["电芯条码"], 20);
            if (readBatteryCodeResult.IsFailed)
            {
                return readBatteryCodeResult;
            }
            string getCode = readBatteryCodeResult?.Content ?? string.Empty;
            string batteryCode = getCode.Match(@"[0-9\.a-zA-Z_-]+").Value;
            if (string.IsNullOrEmpty(batteryCode))
            {
                return OperateResult.CreateFailedResult("托盘条码不合规");
            }
            NgInfo ngInfo = new NgInfo();
            ngInfo.Battery = new Battery()
            {
                BarCode = batteryCode,
                Position = 1,
                OcvType = OcvTypeEnmu.OCV3.ToString(),
            };
            tempNgInfo.Add(ngInfo);
            this.Tray.NgInfos = tempNgInfo;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 测试电池
        /// </summary>
        /// <returns></returns>
        private OperateResult TestBattery()
        {
            NgInfo ngInfo = this.Tray.NgInfos[0];
            int vChannel = SettingManager.CurrentTestOption?.VolChannelForOcv3 ?? 1;
            int nvChannel = SettingManager.CurrentTestOption?.NVolChannelForOcv3 ?? 7;
            LogHelper.UiLog.Info($"打开通道{vChannel}");
            var openResult = DevicesController.SwitchBoard?.OpenChannel(1, vChannel) ?? OperateResult.CreateFailedResult("未找到切换板");
            if (openResult.IsFailed)
                return openResult;
            LogHelper.UiLog.Info($"读取电压");
            var readVolResult = DevicesController.DMM?.ReadDc() ?? OperateResult.CreateFailedResult<double>("未找到万用表");
            if (readVolResult.IsFailed)
                return readVolResult;
            ngInfo.Battery.VolValue = readVolResult.Content;
            ngInfo.Battery.IsTested = true;
            LogHelper.UiLog.Info($"打开通道{nvChannel}");
            openResult = DevicesController.SwitchBoard?.OpenChannel(1,nvChannel)??OperateResult.CreateFailedResult("未找到切换板");
            if(openResult.IsFailed)
                return openResult;
            LogHelper.UiLog.Info("读取负极壳体电压");
            var readNVolResult = DevicesController.DMM?.ReadDc() ?? OperateResult.CreateFailedResult<double>("未找到万用表");
            if (readNVolResult.IsFailed)
                return readNVolResult;
            ngInfo.Battery.NVolValue = readNVolResult.Content;
            ngInfo.Battery.IsTested = true;
            ngInfo.Battery.TestTime = DateTime.Now;
            var validateResult = ValidateOneBatteryLocalNgStatus(this.Tray.NgInfos[0]);
            if(validateResult.IsFailed)
                return validateResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 验证单个电池ng情况
        /// </summary>
        /// <param name="ngInfo">电池</param>
        /// <returns></returns>
        private OperateResult ValidateOneBatteryLocalNgStatus(NgInfo ngInfo)
        {
            if (ngInfo.Battery.IsTested)
            {
                double maxVol = SettingManager.CurrentBatteryStandard?.MaxVol ?? 0;
                double minVol = SettingManager.CurrentBatteryStandard?.MinVol ?? 0;
                double maxRes = SettingManager.CurrentBatteryStandard?.MaxRes ?? 0;
                double minRes = SettingManager.CurrentBatteryStandard?.MinRes ?? 0;
                double maxNVol = SettingManager.CurrentBatteryStandard?.MaxNVol ?? 0;
                double minNVol = SettingManager.CurrentBatteryStandard?.MinNVol ?? 0;

                //bool volNg = false;
                //bool resNg = false;
                //bool nVolNg = false;
                if (SettingManager.CurrentTestOption?.IsTestVol ?? false)
                {

                    if (ngInfo.Battery.VolValue > maxVol)
                    {
                        //volNg = true;
                        //ngInfo.IsNg = true;
                        ngInfo.AddNgType(NgTypeEnum.电压过高);
                        ngInfo.RemoveNgType(NgTypeEnum.电压过低);
                    }
                    else if (ngInfo.Battery.VolValue < minVol)
                    {
                        //volNg = true;
                        //ngInfo.IsNg = true;
                        ngInfo.AddNgType(NgTypeEnum.电压过低);
                        ngInfo.RemoveNgType(NgTypeEnum.电压过高);
                    }
                    else
                    {
                        //volNg = false;
                        ngInfo.RemoveNgType(NgTypeEnum.电压过高);
                        ngInfo.RemoveNgType(NgTypeEnum.电压过低);
                    }
                }
                if (SettingManager.CurrentTestOption?.IsTestRes ?? false)
                {
                    if (ngInfo.Battery.Res > maxRes)
                    {
                        //resNg = true;
                        //ngInfo.IsNg = true;
                        ngInfo.AddNgType(NgTypeEnum.内阻过高);
                        ngInfo.RemoveNgType(NgTypeEnum.内阻过低);
                    }
                    else if (ngInfo.Battery.Res < minRes)
                    {
                        //resNg = true;
                        //ngInfo.IsNg = true;
                        ngInfo.AddNgType(NgTypeEnum.内阻过低);
                        ngInfo.RemoveNgType(NgTypeEnum.内阻过高);
                    }
                    else
                    {
                        //resNg = false;
                        ngInfo.RemoveNgType(NgTypeEnum.内阻过高);
                        ngInfo.RemoveNgType(NgTypeEnum.内阻过低);
                    }
                }
                if (SettingManager.CurrentTestOption?.IsTestNVol ?? false)
                {
                    if (ngInfo.Battery.NVolValue > maxNVol)
                    {
                        //nVolNg = true;
                        ngInfo.AddNgType(NgTypeEnum.负极壳体电压过高);
                        ngInfo.RemoveNgType(NgTypeEnum.负极壳体电压过低);
                    }
                    else if (ngInfo.Battery.NVolValue < minNVol)
                    {
                        //nVolNg = true;
                        ngInfo.AddNgType(NgTypeEnum.负极壳体电压过低);
                        ngInfo.RemoveNgType(NgTypeEnum.负极壳体电压过高);
                    }
                    else
                    {
                        //nVolNg = false;
                        ngInfo.RemoveNgType(NgTypeEnum.负极壳体电压过低);
                        ngInfo.RemoveNgType(NgTypeEnum.负极壳体电压过高);
                    }
                }
                ngInfo.SetIsNg();
                ngInfo.SetNgDescritpion();
            }
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 验证K值
        /// </summary>
        /// <returns></returns>
        private OperateResult VerifyKValue()
        {
            if (SettingManager.CurrentTestOption?.VerifyKValue ?? false)
            {
                List<BatteryDto> batteryList = new List<BatteryDto>();
                using (var resultContext = new OcvTestResultDbContext())
                {
                    switch (SettingManager.CurrentOcvType)
                    {
                        case OcvTypeEnmu.OCV1:
                            batteryList = resultContext.Batterys.Where(x => (Tray.NgInfos.Any(y => y.Battery.BarCode == x.BarCode)) && x.OcvType == "OCV0").ToList();
                            break;
                        case OcvTypeEnmu.OCV2:
                            batteryList = resultContext.Batterys.Where(x => (Tray.NgInfos.Any(y => y.Battery.BarCode == x.BarCode)) && x.OcvType == "OCV1").ToList();
                            break;
                        case OcvTypeEnmu.OCV3:
                            batteryList = resultContext.Batterys.Where(x => (Tray.NgInfos.Any(y => y.Battery.BarCode == x.BarCode)) && x.OcvType == "OCV2").ToList();
                            break;
                    }
                }
                if (batteryList.Count <= 0)
                {
                    return OperateResult.CreateFailedResult("无法找到当前托盘上一OCV工站的数据");
                }
                if (batteryList.Count < Tray.NgInfos.Count)
                {
                    var codeList1 = batteryList.Select(x => x.BarCode).ToList();
                    var codeList2 = Tray.NgInfos.Select(x => x.Battery.BarCode).ToList();
                    var codeList3 = codeList2.Where(x => !codeList1.Contains(x)).ToList();
                    var result = String.Join(';', codeList3);
                    return OperateResult.CreateFailedResult($"无法找到电芯{result}在上一OCV工站的数据");
                }
                foreach (var item in Tray.NgInfos)
                {
                    var battery = item.Battery;
                    var batteryDto = batteryList.OrderByDescending(o => o.TestTime).FirstOrDefault(x => x.BarCode == item.Battery.BarCode);
                    if (batteryDto != null)
                    {
                        TimeSpan hoursSpan = item.Battery.TestTime - batteryDto.TestTime;
                        double hours = hoursSpan.TotalHours;
                        var v = batteryDto.VolValue - item.Battery.VolValue;
                        var kValue = Math.Round((v ?? 0) / hours, 3);
                        double maxK = SettingManager.CurrentBatteryStandard?.MaxKValue ?? 0;
                        double minK = SettingManager.CurrentBatteryStandard?.MinKValue ?? 0;
                        switch (SettingManager.CurrentOcvType)
                        {
                            case OcvTypeEnmu.OCV1:
                                {
                                    item.Battery.KValue1 = kValue;
                                    if (kValue > maxK)
                                    {
                                        item.AddNgType(NgTypeEnum.K1过高);
                                        item.RemoveNgType(NgTypeEnum.K1过低);
                                    }
                                    else if (kValue < minK)
                                    {
                                        item.AddNgType(NgTypeEnum.K1过低);
                                        item.RemoveNgType(NgTypeEnum.K1过高);
                                    }
                                    else
                                    {
                                        item.RemoveNgType(NgTypeEnum.K1过高);
                                        item.RemoveNgType(NgTypeEnum.K1过低);
                                    }
                                    break;
                                }
                            case OcvTypeEnmu.OCV2:
                                {
                                    item.Battery.KValue2 = kValue;
                                    if (kValue > maxK)
                                    {
                                        item.AddNgType(NgTypeEnum.K2过高);
                                        item.RemoveNgType(NgTypeEnum.K2过低);
                                    }
                                    else if (kValue < minK)
                                    {
                                        item.AddNgType(NgTypeEnum.K2过低);
                                        item.RemoveNgType(NgTypeEnum.K2过高);
                                    }
                                    else
                                    {
                                        item.RemoveNgType(NgTypeEnum.K2过高);
                                        item.RemoveNgType(NgTypeEnum.K2过低);
                                    }
                                    break;
                                }
                            case OcvTypeEnmu.OCV3:
                                {
                                    item.Battery.KValue3 = kValue;
                                    if (kValue > maxK)
                                    {
                                        item.AddNgType(NgTypeEnum.K3过高);
                                        item.RemoveNgType(NgTypeEnum.K3过低);
                                    }
                                    else if (kValue < minK)
                                    {
                                        item.AddNgType(NgTypeEnum.K3过低);
                                        item.RemoveNgType(NgTypeEnum.K3过高);
                                    }
                                    else
                                    {
                                        item.RemoveNgType(NgTypeEnum.K3过高);
                                        item.RemoveNgType(NgTypeEnum.K3过低);
                                    }
                                    break;
                                }
                        }
                        item.SetIsNg();
                        item.SetNgDescritpion();
                    }
                }
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 保存到本地数据库
        /// </summary>
        /// <returns></returns>
        private OperateResult SaveToLocation()
        {
            using (var localContext = new LocalTestResultDbContext())
            {
                var dto = Mapper.Map<NgInfoDto>(Tray.NgInfos[0]);
                localContext.NgInfos.Add(dto);
                localContext.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult SaveToSceneDb()
        {
            using (var sceneContext = new OcvTestResultDbContext())
            {
                var dto = Mapper.Map<NgInfoDto>(Tray.NgInfos[0]);
                sceneContext.NgInfos.Add(dto);
                sceneContext.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 重测
        /// </summary>
        /// <returns></returns>
        private OperateResult RetestBattery()
        {
            if (SettingManager.CurrentTestOption?.IsDoRetest ?? false && SettingManager.CurrentTestOption.RetestTimes > 0) 
            {
                for (int i = 0;i < SettingManager.CurrentTestOption.RetestTimes;i++)
                {
                    if (Tray.NgInfos[0].IsNg)
                    {
                        LogHelper.UiLog.Info("执行复测");
                        LogHelper.UiLog.Info("写入复测信号");
                        var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试状态(Ocv3)"], (short)5);
                        if (writeResult.IsFailed)
                            return writeResult;

                        LogHelper.UiLog.Info("等待复测信号");
                        var waitResult1 = DevicesController.Plc.Wait("OCV3PLC.测试状态", (short)5, cancellation: FlowController.CancelToken);
                        if (waitResult1.IsFailed)
                            return waitResult1;

                        LogHelper.UiLog.Info("等待请求测试信号");
                        var waitResult2 = DevicesController.Plc.Wait("OCV3PLC.测试状态", (short)1, cancellation: FlowController.CancelToken);
                        if (waitResult2.IsFailed)
                            return waitResult2;
                        var testResult = TestBattery();
                        if (testResult.IsFailed)
                            return testResult;
                    }
                    else
                        break;
                }
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 上传测试结果到mes
        /// </summary>
        /// <returns></returns>
        private OperateResult UploadToMes()
        {
            LogHelper.UiLog.Info("上传测试结果到Mes");
            var uploadResult = MesService.UploadResult();
            if (uploadResult.IsFailed)
                return uploadResult;
            string returnContentStr = uploadResult.Content ?? string.Empty;
            var contentObj = JsonConvert.DeserializeObject<MesBatteryResultReturnDto>(returnContentStr) ?? new MesBatteryResultReturnDto();
            if (!contentObj.Status)
            {
                return OperateResult.CreateFailedResult($"上传mes失败:{contentObj.Message}");
            }
            if (contentObj.ErrorCode == "warn")
            {
                LogHelper.UiLog.Warn(contentObj.Message);
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 向plc下发测试结果
        /// </summary>
        /// <returns></returns>
        private OperateResult UnBindingBattery()
        {
            var ngInfo = Tray.NgInfos[0];
            var sentValue = ngInfo.IsNg ? 4 : 3;
            LogHelper.UiLog.Info("想Plc写入测试结果");
            var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试状态"], (short)sentValue);
            if(writeResult.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 上传测试结果到Wms
        /// </summary>
        /// <returns></returns>
        private OperateResult UploadTestResultToWms()
        {
            LogHelper.UiLog.Info("上传OCV结果到WMS");
            var uploadResult = WmsService.UploadTestResult();
            if (uploadResult.IsFailed)
                return uploadResult;
            string recovertContentStr = uploadResult.Content ?? string.Empty;
            var obj = JsonConvert.DeserializeObject<WmsNormalReturnDto>(recovertContentStr) ?? new WmsNormalReturnDto();
            if (obj.Result != 1)
                return OperateResult.CreateFailedResult($"上传调度失败:{obj.Message}");
            return OperateResult.CreateSuccessResult();
        }
    }
}
