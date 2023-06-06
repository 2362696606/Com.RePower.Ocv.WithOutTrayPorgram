﻿using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork
    {
        private OperateResult TestBatteries()
        {
            for (int i = 1; i <= (SettingManager.CurrentTestOrder?.Count ?? 0); i++) 
            {
                DoPauseOrStop();
                var closeAllResult = DevicesController.SwitchBoard?.CloseAllChannels(1) ?? OperateResult.CreateFailedResult("未找到切换板");
                if (closeAllResult.IsFailed)
                    return closeAllResult;
                var testResult = TestOneGroupBatteries(i);
                if (testResult.IsFailed)
                    return testResult;
            }
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult TestOneGroupBatteries(int groupNum)
        {
            var waitResult = WaitGroupReady(groupNum);
            if(waitResult.IsFailed)
                return waitResult;
            List<int> currentGroupBatteriesIndex = SettingManager.CurrentTestOrder?[groupNum - 1] ?? new List<int>();
            var currentGroupNginfos = Tray.NgInfos.Where(x => currentGroupBatteriesIndex.Exists(t => t == x.Battery.Position));
            var doTestOneGroupResult = DoTestOneGroup(currentGroupBatteriesIndex);
            if(doTestOneGroupResult.IsFailed)
                return doTestOneGroupResult;
            int currentRetestTimes = 0;
            while ((currentRetestTimes < SettingManager.CurrentTestOption?.RetestTimes)
                && currentGroupNginfos.Any(x => x.IsNg)) 
            {
                //发送复测信号
                var writeResult = SendRetestSignal(groupNum);
                if (writeResult.IsFailed)
                    return writeResult;
                //等待组就绪
                var waitResult1 = WaitGroupReady(groupNum);
                if (waitResult1.IsFailed)
                    return waitResult1;
                //复测组
                var doTestOneGroupResult1 = DoTestOneGroup(currentGroupBatteriesIndex,true);
                if (doTestOneGroupResult1.IsFailed)
                    return doTestOneGroupResult1;
                currentRetestTimes++;
            }
            var writeResult1 = SendGroupTestComplate(groupNum);
            if (writeResult1.IsFailed)
                return writeResult1;
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult DoTestOneGroup(List<int> groupIndexList,bool isRetest = false)
        {
            List<double>? tempList = null;
            for (int i = 0; i < groupIndexList.Count; i++)
            {
                DoPauseOrStop();
                NgInfo? ngInfo = Tray.NgInfos.FirstOrDefault(x => x.Battery.Position == groupIndexList[i]);
                if (ngInfo is { })
                {
                    if(isRetest && !ngInfo.IsNg)
                    {
                        continue;
                    }
                    else
                    {
                        var openResult = DevicesController.SwitchBoard?.OpenChannel(1, i + 1) ?? OperateResult.CreateFailedResult("切换板实例为null");
                        if (openResult.IsFailed)
                            return openResult;
                        var testOneBatteryResult = TestOneBattery(ngInfo);
                        if (testOneBatteryResult.IsFailed)
                            return testOneBatteryResult;
                        var closeResult = DevicesController.SwitchBoard?.CloseChannel(1, i + 1) ?? OperateResult.CreateFailedResult("切换板实例为null");
                        if (closeResult.IsFailed) return closeResult;
                        if (SettingManager.CurrentTestOption?.IsTestNVol ?? false)
                        {
                            DoPauseOrStop();
                            int startChannel = SettingManager.CurrentOtherSetting?.NVolStartChannel ?? 5;
                            var openResult1 = DevicesController.SwitchBoard?.OpenChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                            if (openResult1.IsFailed)
                                return openResult;
                            var testNVolResult = TestNVol(ngInfo);
                            if (testNVolResult.IsFailed) return testNVolResult;
                            var closeResult1 = DevicesController.SwitchBoard?.CloseChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                            if (closeResult1.IsFailed) return closeResult1;
                        }
                        if (SettingManager.CurrentTestOption?.IsTestPVol ?? false)
                        {
                            DoPauseOrStop();
                            int startChannel = SettingManager.CurrentOtherSetting?.PVolStartChannel ?? 9;
                            var openResult1 = DevicesController.SwitchBoard?.OpenChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                            if (openResult1.IsFailed)
                                return openResult;
                            var testPVolResult = TestPVol(ngInfo);
                            if (testPVolResult.IsFailed) return testPVolResult;
                            var closeResult1 = DevicesController.SwitchBoard?.CloseChannel(1, i + startChannel) ?? OperateResult.CreateFailedResult("切换板实例为null");
                            if (closeResult1.IsFailed) return closeResult1;
                        }
                        if (SettingManager.CurrentTestOption?.IsTestTemp ?? false)
                        {
                            DoPauseOrStop();
                            if (tempList == null)
                            {
                                var readResult = DevicesController.TemperatureSensor?.ReadTemp() ?? OperateResult.CreateFailedResult<double[]>("温度传感器实例为null");
                                if (readResult.IsFailed)
                                    return readResult;
                                if (readResult.Content is { })
                                    tempList = readResult.Content.ToList();
                                else
                                    return OperateResult.CreateFailedResult("读取温度时返回Content为null");
                            }
                            ngInfo.Battery.Temp = tempList[i];
                        }
                        ngInfo.Battery.IsTested = true;
                        ngInfo.Battery.TestTime = DateTime.Now;
                        var cleanResult = WaitStatusClean();
                        if (cleanResult.IsFailed)
                            return cleanResult;
                        var localNgInfoResult = ValidateNgInfoWithLocal(ngInfo);
                        if (localNgInfoResult.IsFailed)
                            return localNgInfoResult; 
                        if(isRetest)
                        {
                            if (ngInfo.Battery.ReserveInt1 == null)
                                ngInfo.Battery.ReserveInt1 = 0;
                            ngInfo.Battery.ReserveInt1 += 1;
                        }
                    }
                }
            }
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult TestNVol(NgInfo ngInfo)
        {
            if(SettingManager.CurrentTestOption?.IsTestNVol??false)
            {
                LogHelper.UiLog.Info("读取负极壳体电压");
                var readResult = DevicesController.Dmm?.ReadDc() ?? OperateResult.CreateFailedResult<double>("万用表实例为null");
                if (readResult.IsFailed)
                    return readResult;
                ngInfo.Battery.NVolValue = readResult.Content;
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult TestPVol(NgInfo ngInfo)
        {
            if (SettingManager.CurrentTestOption?.IsTestPVol ?? false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info("读取正极壳体电压");
                var readResult = DevicesController.Dmm?.ReadDc() ?? OperateResult.CreateFailedResult<double>("万用表实例为null");
                if (readResult.IsFailed)
                    return readResult;
                ngInfo.Battery.PVolValue = readResult.Content;
            }
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult TestOneBattery(NgInfo ngInfo)
        {
            DoPauseOrStop();
            LogHelper.UiLog.Info($"开始测试电池{ngInfo.Battery.Position}");
            if (SettingManager.CurrentTestOption?.IsTestVol ?? false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info("读取电压");
                var readResult = DevicesController.Dmm?.ReadDc() ?? OperateResult.CreateFailedResult<double>("万用表实例为null");
                if (readResult.IsFailed)
                    return readResult;
                ngInfo.Battery.VolValue = readResult.Content;
            }
            if (SettingManager.CurrentTestOption?.IsTestRes ?? false)
            {
                DoPauseOrStop();
                LogHelper.UiLog.Info("读取内阻");
                var readResult = DevicesController.Ohm?.ReadRes() ?? OperateResult.CreateFailedResult<double>("内阻仪实例为null");
                if (readResult.IsFailed)
                    return readResult;
                var resValue = readResult.Content;
                if (resValue > 100)
                {
                    ManualResetEvent flag = new ManualResetEvent(false);
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                        { { "warringInfo", "测试通道内阻超过100mΩ" }, { "flag", flag } };
                    DoMethodMessage message = new DoMethodMessage()
                    {
                        MethodName = "DoWarring",
                        Parameters = parameters
                    };
                    WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message, "DoMainViewMethod");
                    flag.WaitOne();
                }


                ngInfo.Battery.Res = readResult.Content;
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}
