using AutoMapper;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public partial class MainWork : MainWorkAbstract
    {
        public MainWork(DevicesController devicesController
            ,Tray tray
            ,IWmsService wmsService
            ,IMesService mesService
            ,SettingManager settingManager
            ,IMapper mapper)
        {
            DevicesController = devicesController;
            Tray = tray;
            WmsService = wmsService;
            MesService = mesService;
            SettingManager = settingManager;
            Mapper = mapper;
            using (var settingContext = new OcvSettingDbContext())
            {
                var item = settingContext.SettingItems.First(x => x.SettingName == "TestOrder");
                string jStr = item.JsonValue;
                this.TestOrder = JsonConvert.DeserializeObject<List<List<int>>>(jStr)??new List<List<int>>();
            }
        }

        public DevicesController DevicesController { get; }
        public Tray Tray { get; }
        public IWmsService WmsService { get; }
        public IMesService MesService { get; }
        public SettingManager SettingManager { get; }
        public IMapper Mapper { get; }
        public List<List<int>> TestOrder { get; set; }

        protected override OperateResult DoWork()
        {
            while(true)
            {
                DoPauseOrStop();
                var testPreParationResult = TestPreparation();
                if(testPreParationResult.IsFailed)
                    return testPreParationResult;
                DoPauseOrStop();
                var getTrayCodeResult = GetTrayCode();
                if(getTrayCodeResult.IsFailed)
                    return getTrayCodeResult;
                DoPauseOrStop();
                var getBatteriesInfoResult = GetBatteriesInfo();
                if(getBatteriesInfoResult.IsFailed)
                    return getBatteriesInfoResult;
                DoPauseOrStop();
                var testBatteriesResult = TestBatteries();
                if(testBatteriesResult.IsFailed)
                    return testBatteriesResult;
                var verfyKValueResult = VerifyKValue();
                if(verfyKValueResult.IsFailed)
                    return verfyKValueResult;
                DoPauseOrStop();
                var saveToLocationResult = SaveToLocation();
                if(saveToLocationResult.IsFailed)
                    return saveToLocationResult;
                DoPauseOrStop();
                //var saveToSceneDbResult = SaveToSceneDb();
                //if (saveToSceneDbResult.IsFailed)
                //    return saveToSceneDbResult;
                //DoPauseOrStop();
                var uploadToMesResult = UploadTestResultToMes();
                if(uploadToMesResult.IsFailed)
                    return uploadToMesResult;
                DoPauseOrStop();
                var unBindingResult = UnBindingTray();
                if(unBindingResult.IsFailed)
                    return unBindingResult;
                DoPauseOrStop();
                var uploadToWmsResult = UploadTestResultToWms();
                if (uploadToWmsResult.IsFailed)
                    return uploadToWmsResult;
                DoPauseOrStop();
                var requestAllLocateCellResult = RequestAllLocateCellToWms();
                if(requestAllLocateCellResult.IsFailed)
                    return requestAllLocateCellResult;
                DoPauseOrStop();
                var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)2);
                if(writeResult.IsFailed)
                    return writeResult;
                //return OperateResult.CreateSuccessResult();

            }
        }


        /// <summary>
        /// 测试准备
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult TestPreparation()
        {
            if(!DevicesController.Plc.IsConnected)
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
            if(!(DevicesController.DMM?.IsConnected ?? true))
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
            if(!(DevicesController.Ohm?.IsConnected??true))
            {
                LogHelper.UiLog.Info("连接内阻仪");
                var result = DevicesController.Ohm.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接内阻仪");
                }
                else
                {
                    return result;
                }
            }
            if(!(DevicesController.SwitchBoard?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if(result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接切换板");
                }
                else
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("测试标志位写入0");
            var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], 0);
            if (writeResult.IsFailed)
            {
                return writeResult;
            }
            LogHelper.UiLog.Info("正在等待PLC请求测试");
            var waitResult = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["测试请求信号"], (short)1, cancellation: this.FlowController.CancelToken);
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            LogHelper.UiLog.Info("完成准备流程");
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取托盘条码
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult GetTrayCode()
        {
            LogHelper.UiLog.Info("读取托盘条码");
            var readTrayCodeResult = DevicesController.Plc.ReadString(DevicesController.PlcAddressCache["托盘条码"], 20);
            if (readTrayCodeResult.IsFailed)
            {
                return readTrayCodeResult;
            }
            string getCode = readTrayCodeResult?.Content ?? string.Empty;
            string trayCode = getCode.Match(@"[0-9\.a-zA-Z_-]+").Value;
            if (string.IsNullOrEmpty(trayCode))
            {
                return OperateResult.CreateFailedResult("托盘条码不合规");
            }
            Tray.TrayCode = trayCode;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取电芯条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetBatteriesInfo()
        {
            LogHelper.UiLog.Info("获取电芯条码");
            var result = WmsService.GetBatteriesInfo();
            if (result.IsFailed)
                return OperateResult.CreateFailedResult($"请求电芯条码失败:{result.Message ?? "未知原因"}");
            string contentStr = result.Content??string.Empty;
            if (string.IsNullOrEmpty(contentStr))
            {
                return OperateResult.CreateFailedResult("请求电芯条码返回为空");
            }
            var resultDto = JsonConvert.DeserializeObject<WmsGetBatteriesInfoResultDto>(contentStr);
            if(resultDto is { })
            {
                if (resultDto.Result != 1)
                    return OperateResult.CreateFailedResult($"请求电芯条码失败:{resultDto.Message}");
                if (resultDto.HandleResult.TrayCode != Tray.TrayCode)
                    return OperateResult.CreateFailedResult($"请求电芯条码失败:WMS下发电芯条码为{resultDto.HandleResult.TrayCode},但本地电芯条码为{Tray.TrayCode}");
                if (resultDto.HandleResult.BatteriesInfoList.Count <= 0)
                    return OperateResult.CreateFailedResult("获取电芯条码失败:电芯条码数组为空");
                //切换当前工站
                SettingManager.CurrentOcvType = Enum.Parse<OcvTypeEnmu>(resultDto.HandleResult.Procedure);
                List <NgInfo> ngInfos = new List<NgInfo>();
                foreach(var item in resultDto.HandleResult.BatteriesInfoList)
                {
                    var ngInfo = new NgInfo();
                    ngInfo.Battery.TrayCode = Tray.TrayCode;
                    ngInfo.Battery = new Battery();
                    ngInfo.Battery.Position = item.Index;
                    ngInfo.Battery.BarCode = item.BarCode;
                    ngInfo.Battery.OcvType = resultDto.HandleResult.Procedure;
                    ngInfos.Add(ngInfo);
                }
                ngInfos.OrderBy(x => x.Battery.Position);
                this.Tray.NgInfos = ngInfos;
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 测试电池
        /// </summary>
        /// <returns></returns>
        private OperateResult TestBatteries()
        {
            LogHelper.UiLog.Info("写入\"测试标志位\"=1");
            var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)1);
            if (writeResult.IsFailed)
                return writeResult;
            DoPauseOrStop();
            for(int i = 0;i<TestOrder.Count;i++)
            {
                DoPauseOrStop();
                var closeAllResult = DevicesController.SwitchBoard?.CloseAllChannels(1) ?? OperateResult.CreateFailedResult("未找到切换板");
                if (closeAllResult.IsFailed)
                    return closeAllResult;
                var testResult = TestOneGroupBatteries(i);
                if(testResult.IsFailed)
                    return testResult;
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 测试整组电池
        /// </summary>
        /// <param name="groupIndex">组序号</param>
        /// <returns></returns>
        private OperateResult TestOneGroupBatteries(int groupIndex)
        {
            LogHelper.UiLog.Info($"等待组{groupIndex + 1}就绪");
            var waitResult = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["测试状态"], (short)(groupIndex + 1), cancellation: FlowController.CancelToken);
            if (waitResult.IsFailed)
                return waitResult;
            List<int> currentGroup = TestOrder[groupIndex];
            var doTestResult = DoTestOneGroup(currentGroup);
            if (doTestResult.IsFailed)
                return doTestResult;
            if (SettingManager.CurrentTestOption?.IsDoRetest ?? false && SettingManager.CurrentTestOption?.RetestTimes > 0) 
            {
                List<NgInfo> ngInfos = Tray.NgInfos.Where(x => currentGroup.Contains(x.Battery.Position)).ToList();

                for (int i = 0; i < SettingManager.CurrentTestOption.RetestTimes; i++)
                {
                    if (ngInfos.Any(x => x.IsNg))
                    {
                        DoPauseOrStop();
                        LogHelper.UiLog.Info($"组{groupIndex + 1}执行复测");
                        var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)(groupIndex + 111));
                        if (writeResult.IsFailed)
                            return writeResult;
                        var waitResult1 = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["测试状态"], (short)(groupIndex + 1), cancellation: FlowController.CancelToken);
                        if (waitResult1.IsFailed)
                            return waitResult1;
                        var doTestResult1 = DoTestOneGroup(currentGroup);
                        if (doTestResult1.IsFailed)
                            return doTestResult1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            LogHelper.UiLog.Info($"组{groupIndex + 1}测试完成");
            var writeComplateResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)(groupIndex + 11));
            if (writeComplateResult.IsFailed)
                return writeComplateResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 执行整组测试
        /// </summary>
        /// <param name="currentGroup"></param>
        /// <returns></returns>
        private OperateResult DoTestOneGroup(List<int> currentGroup)
        {
            for (int i = 0; i < currentGroup.Count; i++)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info($"开始测试电池{currentGroup[i]}电压与内阻");
                var openResult = DevicesController.SwitchBoard?.OpenChannel(1, i + 1) ?? OperateResult.CreateFailedResult("未找到切换板");
                if (openResult.IsFailed)
                    return openResult;
                var ngInfo = Tray.NgInfos.First(x => x.Battery.Position == currentGroup[i]);
                var testResult = TestOneBattery(ngInfo);
                if (testResult.IsFailed)
                    return testResult;
                var closeResult = DevicesController.SwitchBoard?.CloseChannel(1, i + 1) ?? OperateResult.CreateFailedResult("未找到切换板");
                if (closeResult.IsFailed)
                    return closeResult;
                if (SettingManager.CurrentTestOption?.IsTestNVol??false)
                {
                    int startChannel = SettingManager.CurrentTestOption.NVolStartChannel;
                    LogHelper.UiLog.Info($"开始测试电池{currentGroup[i]}负极壳体电压");
                    var openNVolChannelResult = DevicesController.SwitchBoard?.OpenChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("未找到切换板");
                    if (openNVolChannelResult.IsFailed)
                        return openResult;
                    var testNVolResult = TestBatteryNVol(ngInfo);
                    if (testNVolResult.IsFailed)
                        return testNVolResult;
                    var closeNVolChannelResult = DevicesController.SwitchBoard?.CloseChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("未找到切换板");
                    if (closeNVolChannelResult.IsFailed)
                        return openResult;
                }
                var validateResult = ValidateOneBatteryLocalNgStatus(ngInfo);
            }
            var writeResult2 = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], (short)8);
            if (writeResult2.IsFailed)
                return writeResult2;
            LogHelper.UiLog.Info($"等待测试状态清零");
            var waitResult2 = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["测试状态"], (short)0, cancellation: FlowController.CancelToken);
            if (waitResult2.IsFailed)
                return waitResult2;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 测试单个电池
        /// </summary>
        /// <param name="ngInfo">电池</param>
        /// <returns></returns>
        private OperateResult TestOneBattery(NgInfo ngInfo)
        {
            ngInfo.Battery.TestTime = DateTime.Now;
            //未测试已ng
            if(!ngInfo.Battery.IsTested && ngInfo.IsNg)
            {
                LogHelper.UiLog.Info($"电芯{ngInfo.Battery.Position}外部ng，不进行测试");
                return OperateResult.CreateSuccessResult();
            }
            //已测试未ng
            if(!ngInfo.IsNg && ngInfo.Battery.IsTested)
            {
                return OperateResult.CreateSuccessResult();
            }
            if (SettingManager.CurrentTestOption?.IsTestVol??false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info($"开始测试电芯{ngInfo.Battery.Position}电压");
                var dmmReadValue = DevicesController.DMM?.ReadDc() ?? OperateResult.CreateFailedResult<double>("未找到万用表");
                if (dmmReadValue.IsFailed)
                    return dmmReadValue;
                ngInfo.Battery.IsTested = true;
                ngInfo.Battery.VolValue = dmmReadValue.Content;
            }
            if (SettingManager.CurrentTestOption?.IsTestRes??false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info($"开始测试电芯{ngInfo.Battery.Position}内阻");
                var ohmReadValue = DevicesController.Ohm?.ReadRes() ?? OperateResult.CreateFailedResult<double>("未找到内阻仪");
                if (ohmReadValue.IsFailed)
                    return ohmReadValue;
                ngInfo.Battery.IsTested = true;
                ngInfo.Battery.Res = ohmReadValue.Content;
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 测试单个电池负极壳体电压
        /// </summary>
        /// <param name="ngInfo"></param>
        /// <returns></returns>
        private OperateResult TestBatteryNVol(NgInfo ngInfo)
        {
            if (ngInfo.Battery.IsTested && ngInfo.IsNg)
            {
                LogHelper.UiLog.Info($"电芯{ngInfo.Battery.Position}外部ng，不进行测试");
                return OperateResult.CreateSuccessResult();
            }
            if (SettingManager.CurrentTestOption?.IsTestVol ?? false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info($"开始测试电芯{ngInfo.Battery.Position}负极壳体电压");
                var dmmReadValue = DevicesController.DMM?.ReadDc() ?? OperateResult.CreateFailedResult<double>("未找到万用表");
                if (dmmReadValue.IsFailed)
                    return dmmReadValue;
                ngInfo.Battery.IsTested = true;
                ngInfo.Battery.NVolValue = dmmReadValue.Content;
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 判断本地ng情况
        /// </summary>
        /// <returns></returns>
        private OperateResult ValidateBatteriesLocalNgStatus()
        {
            foreach (var item in Tray.NgInfos)
            {
                ValidateOneBatteryLocalNgStatus(item);
            }
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
                    }
                }
                if(batteryList.Count<=0)
                {
                    return OperateResult.CreateFailedResult("无法找到当前托盘上一OCV工站的数据");
                }
                if(batteryList.Count<Tray.NgInfos.Count)
                {
                    var codeList1 = batteryList.Select(x => x.BarCode).ToList();
                    var codeList2 = Tray.NgInfos.Select(x => x.Battery.BarCode).ToList();
                    var codeList3 = codeList2.Where(x => !codeList1.Contains(x)).ToList();
                    var result = String.Join(';', codeList3);
                    return OperateResult.CreateFailedResult($"无法找到电芯{result}在上一OCV工站的数据");
                }
                foreach(var item in Tray.NgInfos)
                {
                    var battery = item.Battery;
                    var batteryDto = batteryList.OrderByDescending(o => o.TestTime).FirstOrDefault(x => x.BarCode == item.Battery.BarCode);
                    if (batteryDto != null)
                    {
                        TimeSpan hoursSpan = item.Battery.TestTime - batteryDto.TestTime;
                        double hours = hoursSpan.TotalHours;
                        var v = batteryDto.VolValue - item.Battery.VolValue;
                        var kValue = Math.Round((v ?? 0) / hours, 3);
                        double maxK = SettingManager.CurrentBatteryStandard?.MaxKValue??0;
                        double minK = SettingManager.CurrentBatteryStandard?.MinKValue??0;
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
                //var dto = Mapper.Map<TrayDto>(Tray);
                //localContext.Trays.Add(dto);
                //localContext.SaveChanges();
                var dto = Mapper.Map<TrayDto>(Tray);

                var trays = localContext.Trays.Where(x => x.TrayCode == Tray.TrayCode).ToList();
                if (trays == null || trays.Count() <= 0)
                {
                    localContext.Trays.Add(dto);
                }
                else if (trays.Count() == 1)
                {
                    var temp = trays.First();
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    localContext.Update(temp);
                }
                else
                {
                    var temp = trays.First();
                    int count = trays.Count();
                    for (int i = 1; i < count; i++)
                    {
                        foreach (var item in trays[i].NgInfos)
                        {
                            temp.NgInfos.Add(item);
                        }
                        localContext.Remove(trays[i]);
                    }
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    localContext.Update(temp);
                }   
                localContext.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 保存到现场服务器数据库
        /// </summary>
        /// <returns></returns>
        private OperateResult SaveToSceneDb()
        {
            using (var sceneContext = new OcvTestResultDbContext())
            {
                var dto = Mapper.Map<TrayDto>(Tray);

                var trays = sceneContext.Trays.Where(x => x.TrayCode == Tray.TrayCode).ToList();
                if(trays == null || trays.Count()<=0)
                {
                    sceneContext.Trays.Add(dto);
                }
                else if(trays.Count() == 1)
                {
                    var temp = trays.First();
                    foreach(var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    sceneContext.Update(temp);
                }
                else
                {
                    var temp = trays.First();
                    int count = trays.Count();
                    for(int i = 1;i<count;i++)
                    {
                        foreach(var item in trays[i].NgInfos)
                        {
                            temp.NgInfos.Add(item);
                        }
                        sceneContext.Remove(trays[i]);
                    }
                    foreach (var item in dto.NgInfos)
                    {
                        temp.NgInfos.Add(item);
                    }
                    sceneContext.Update(temp);
                }
                sceneContext.SaveChanges();
            }
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 保存到本地数据库
        /// </summary>
        /// <returns></returns>
        private OperateResult UploadTestResultToMes()
        {
            LogHelper.UiLog.Info("上传测试结果到Mes");
            var uploadResult = MesService.UploadResult();
            if (uploadResult.IsFailed)
                return uploadResult;
            string returnContentStr = uploadResult.Content ?? string.Empty;
            var contentObj = JsonConvert.DeserializeObject<MesBatteryResultReturnDto>(returnContentStr) ?? new MesBatteryResultReturnDto();
            if(!contentObj.Status)
            {
                return OperateResult.CreateFailedResult($"上传mes失败:{contentObj.Message}");
            }
            if(contentObj.ErrorCode == "warn")
            {
                LogHelper.UiLog.Warn(contentObj.Message);
            }
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 拆盘
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult UnBindingTray()
        {
            foreach(var item in Tray.NgInfos)
            {
                LogHelper.UiLog.Info($"写电池{item.Battery.Position}结果到plc");
                var getNgValueResult = GetNgValue(item);
                if(getNgValueResult.IsFailed)
                    return getNgValueResult;
                var sendValue = getNgValueResult.Content;
                var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache[$"位置{item.Battery.Position}托盘电池状态"], (short)sendValue);
                if (writeResult.IsFailed)
                    return writeResult;
            }
            LogHelper.UiLog.Info("等待拆盘请求");
            //等待拆盘请求
            var waitResult = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["拆盘请求信号"], (short)1, cancellation: FlowController.CancelToken);
            if(waitResult.IsFailed)
                return waitResult;
            LogHelper.UiLog.Info("写入开始拆盘");
            //写入开始拆盘
            var writeResult1 = DevicesController.Plc.Write(DevicesController.PlcAddressCache[$"拆盘标志位"], (short)1);
            if (writeResult1.IsFailed)
                return writeResult1;
            LogHelper.UiLog.Info("等待拆盘完成");
            //等待拆盘完成
            var waitResult1 = DevicesController.Plc.Wait(DevicesController.PlcAddressCache["托盘状态"], (short)2, cancellation: FlowController.CancelToken);
            if (waitResult1.IsFailed)
                return waitResult1;
            LogHelper.UiLog.Info("写入收到拆盘完成确认");
            //写入收到拆盘完成确认
            var writeResult2 = DevicesController.Plc.Write(DevicesController.PlcAddressCache[$"拆盘标志位"], (short)2);
            if (writeResult2.IsFailed)
                return writeResult2;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取ng通道
        /// </summary>
        /// <param name="ngInfo"></param>
        /// <returns></returns>
        private OperateResult<int> GetNgValue(NgInfo ngInfo)
        {
            int sendValue = 1;
            if(SettingManager.CurrentOcvType == OcvTypeEnmu.OCV0 || SettingManager.CurrentOcvType == OcvTypeEnmu.OCV3)
            {
                sendValue = ngInfo.IsNg ? 2 : 1;
            }
            else
            {
                if (ngInfo.HasNgType(NgTypeEnum.电压过低 | NgTypeEnum.电压过高))
                {
                    sendValue = SettingManager.CurrentTestOption?.VolNgChannel ?? 2;
                }
                else if (ngInfo.HasNgType(NgTypeEnum.内阻过低 | NgTypeEnum.内阻过高))
                {
                    sendValue = SettingManager.CurrentTestOption?.ResNgChannel ?? 2;
                }
                else if (ngInfo.HasNgType(NgTypeEnum.负极壳体电压过低 | NgTypeEnum.负极壳体电压过高))
                {
                    sendValue = SettingManager.CurrentTestOption?.NVolNgChannel ?? 2;
                }
                else if (ngInfo.HasNgType(NgTypeEnum.K1过低 | NgTypeEnum.K1过高 | NgTypeEnum.K2过低 | NgTypeEnum.K2过高 | NgTypeEnum.K3过低 | NgTypeEnum.K3过高))
                {
                    sendValue = SettingManager.CurrentTestOption?.KValueNgChannel ?? 2;
                }
            }
            return OperateResult.CreateSuccessResult(sendValue);
        }
        /// <summary>
        /// 上传结果到WMS
        /// </summary>
        /// <returns></returns>
        private OperateResult UploadTestResultToWms()
        {
            LogHelper.UiLog.Info("上传OCV结果到WMS");
            var uploadResult = WmsService.UploadTestResult();
            if(uploadResult.IsFailed)
                return uploadResult;
            string recovertContentStr = uploadResult.Content ?? string.Empty;
            var obj = JsonConvert.DeserializeObject<WmsNormalReturnDto>(recovertContentStr)??new WmsNormalReturnDto();
            if (obj.Result != 1)
                return OperateResult.CreateFailedResult($"上传调度失败:{obj.Message}");
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// OCV0上传出库信号给wms
        /// </summary>
        /// <returns></returns>
        private OperateResult RequestAllLocateCellToWms()
        {
            if (SettingManager.CurrentOcvType == OcvTypeEnmu.OCV0)
            {
                LogHelper.UiLog.Info("Ocv0调用Wms出库信号接口");
                var result = WmsService.RequestAllLocateCellToWms();
                if (result.IsFailed)
                    return result;
                string str = result.Content ?? string.Empty;
                var obj = JsonConvert.DeserializeObject<WmsNormalReturnDto>(str) ?? new WmsNormalReturnDto();
                if (obj.Result != 1)
                    return OperateResult.CreateFailedResult($"OCV0出库接口调用失败:{obj.Message}");
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
