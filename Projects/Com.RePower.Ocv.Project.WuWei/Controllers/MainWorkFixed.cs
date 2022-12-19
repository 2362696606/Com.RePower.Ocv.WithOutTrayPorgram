using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.WuWei.Model;
using Com.RePower.Ocv.Project.WuWei.Serivces;
using Com.RePower.Ocv.Project.WuWei.Serivces.Dto;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public partial class MainWorkFixed : ObservableObject, IProjectMainWork
    {
        private int _workStatus;

        public MainWorkFixed(DevicesController devicesController
            , FlowController flowController
            , Tray tray
            , BatteryNgCriteria batteryNgCriteria
            , TestOption testOption
            , IWmsService wmsService)
        {
            DevicesController = devicesController;
            FlowController = flowController;
            Tray = tray;
            BatteryNgCriteria = batteryNgCriteria;
            TestOption = testOption;
            WmsService = wmsService;
        }

        public int WorkStatus
        {
            get { return _workStatus; }
            set { SetProperty(ref _workStatus, value); }
        }

        public DevicesController DevicesController { get; }
        public FlowController FlowController { get; }
        public Tray Tray { get; }
        public BatteryNgCriteria BatteryNgCriteria { get; }
        public TestOption TestOption { get; }
        public IWmsService WmsService { get; }

        public bool IsDoUploadToMes
        {
            get { return TestOption.IsDoUploadToMes; }
        }

        /// <summary>
        /// 是否复测
        /// </summary>
        public bool IsDoRetest
        {
            get { return TestOption.IsDoRetest; }
        }
        /// <summary>
        /// 复测次数
        /// </summary>
        public int RetestTimes
        {
            get { return TestOption.RetestTimes; }
        }

        public ManualResetEvent ResetEvent
        {
            get { return FlowController.ResetEvent; }
        }
        public CancellationTokenSource CancelTokenSource
        {
            get { return FlowController.CancelTokenSource; }
        }
        public CancellationToken CancelToken
        {
            get { return FlowController.CancelToken; }
        }

        public async void PauseWorkAsync()
        {
            WorkStatus = 2;
            await Task.Run(() =>
            {
                ResetEvent.Set();
            });
        }

        public async void StartWorkAsync()
        {
            if (WorkStatus == 0)
            {
                WorkStatus = 1;
                await Task.Run(() =>
                {
                    try
                    {
                        var result = DoWork();
                        if (result.IsFailed)
                        {
                            LogHelper.UiLog.Warn(result.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.GetType() == typeof(OperationCanceledException))
                        {
                            FlowController.CancelTokenSource = new CancellationTokenSource();
                        }
                        else
                        {
                            LogHelper.UiLog.Error(e.Message);
                        }
                    }
                    finally
                    {
                        WorkStatus = 0;
                    }
                });
            }
            else if (WorkStatus == 2)
            {
                WorkStatus = 1;
                ResetEvent.Reset();
            }
        }

        public async void StopWorkAsync()
        {
            WorkStatus = 0;
            await Task.Run(() =>
            {
                CancelTokenSource.Cancel();
            });
        }

        private OperateResult DoWork()
        {
            while (true)
            {
                #region 初始化Plc
                var init1 = InitWork();
                if (init1.IsFailed)
                {
                    return init1;
                }
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 等待测试准备信号
                LogHelper.UiLog.Info("等待本地Plc[Read_1] = 1");
                var wait1 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["Read_1"], (short)1);
                if (wait1.IsFailed)
                {
                    return OperateResult.CreateFailedResult(wait1.Message ?? "等待本地Plc[Read_1] = 1失败", wait1.ErrorCode);
                }
                #endregion
                #region 读取托盘条码
                bool validateResult = false;
                string trayCode = string.Empty;
                LogHelper.UiLog.Info("读取物流Plc[托盘条码]");
                var read1 = DevicesController.LocalPlc.ReadString(DevicesController.LocalPlcAddressCache["Read_2"], 20);
                if (read1.IsFailed)
                {
                    return OperateResult.CreateFailedResult(read1.Message ?? "读取物流Plc[托盘条码]失败", read1.ErrorCode);
                }
                trayCode = read1.Content ?? string.Empty;
                Tray.TrayCode = trayCode;
                validateResult = ValidateBatteryCode(trayCode);
                if (!validateResult)
                {
                    return OperateResult.CreateFailedResult($"读取托盘条码失败,{trayCode}不合规");
                }
                #endregion
                #region 请求电芯条码
                LogHelper.UiLog.Info("请求电芯条码");
                var getBatteriesInfoResult = WmsService.GetBatteriesInfo();
                if (getBatteriesInfoResult.IsFailed)
                {
                    return getBatteriesInfoResult;
                }
                string content = getBatteriesInfoResult?.Content ?? string.Empty;
                var resultObj = JsonConvert.DeserializeObject<WmsBatteriesInfoDto>(content);
                if (resultObj == null)
                {
                    return OperateResult.CreateFailedResult("请求电芯条码返回值为null");
                }
                if (resultObj.Result == 0)
                {
                    return OperateResult.CreateSuccessResult($"请求电芯条码失败，原因是{resultObj.Message}");
                }
                if (resultObj.PileContent == null)
                {
                    return OperateResult.CreateFailedResult($"请求电芯条码失败，主体为null");
                }
                if (resultObj.PileContent.PilletBarcode != Tray.TrayCode)
                {
                    return OperateResult.CreateFailedResult($"请求电芯条码失败，WMS返回托盘条码{resultObj.PileContent.PilletBarcode}与本地托盘条码{Tray.TrayCode}不一致");
                }
                foreach (var item in resultObj.PileContent.Batterys)
                {
                    NgInfo ngInfo = new NgInfo();
                    ngInfo.Battery.BarCode = item.BatteryBarcode;
                    ngInfo.Battery.Position = item.PalletIndex;
                    Tray.NgInfos.Add(ngInfo);
                } 
                #endregion
                LogHelper.UiLog.Info("写入本地Plc[Send_1] = 1");
                var write2 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_1"], (short)1);
                if (write2.IsFailed)
                {
                    return OperateResult.CreateFailedResult(write2.Message ?? "写入本地Plc[Send_1] = 1失败", write2.ErrorCode);
                }
                int reTestTimes = 0;
                do
                {
                    reTestTimes++;
                    LogHelper.UiLog.Info("等待本地Plc[Read_1] = 2");
                    var wait3 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["Read_1"], (short)2);
                    if (wait3.IsFailed)
                    {
                        return OperateResult.CreateFailedResult(wait3.Message ?? "等待本地Plc[Read_1] = 2失败", wait3.ErrorCode);
                    }
                    //测试电池
                    LogHelper.UiLog.Info("开始测试电池");
                    var test1 = TestBatteries();
                    if (test1.IsFailed)
                    {
                        return test1;
                    }
                    LogHelper.UiLog.Info("写入本地Plc[Send_1] = 2");
                    var write3 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_1"], (short)2);
                    if (write3.IsFailed)
                    {
                        return OperateResult.CreateFailedResult(write3.Message ?? "写入本地Plc[Send_1] = 2失败", write3.ErrorCode);
                    }
                    //验证ng结果
                    ValidateNgResult();
                    if (Tray.NgInfos.Any(x=>x.IsNg) && IsDoRetest && reTestTimes < RetestTimes)
                    {
                        LogHelper.UiLog.Info("写入本地Plc[Send_2] = 2");
                        var write4 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)2);
                        if (write4.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write4.Message ?? "写入本地Plc[Send_2] = 2失败", write4.ErrorCode);
                        }
                    }
                    //else if (Tray.NgInfos.Any(x => x.IsNg))
                    //{
                    //    LogHelper.UiLog.Info("写入本地Plc[Send_3] = 2");
                    //    var write4 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)2);
                    //    if (write4.IsFailed)
                    //    {
                    //        return OperateResult.CreateFailedResult(write4.Message ?? "写入本地Plc[Send_3] = 2失败", write4.ErrorCode);
                    //    }
                    //    return OperateResult.CreateFailedResult("电芯异常，已停止测试");
                    //}
                    else
                    {
                        LogHelper.UiLog.Info("写入本地Plc[Send_3] = 1");
                        var write5 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)1);
                        if (write5.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write5.Message ?? "写入本地Plc[Send_3] = 1失败", write5.ErrorCode);
                        }
                        break;
                    }
                } while (IsDoRetest && reTestTimes < RetestTimes);
                LogHelper.UiLog.Info("写入本地Plc[Send_2] = 1");
                var write6 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)1);
                if (write6.IsFailed)
                {
                    return OperateResult.CreateFailedResult(write6.Message ?? "写入本地Plc[Send_1] = 1失败", write6.ErrorCode);
                }
            }
        }
        private bool ValidateBatteryCode(string batteryCode)
        {
            return true;
        }
        private OperateResult TestBatteries()
        {
            if (DevicesController.SwitchBoard.IsConnected == false)
            {
                var result = DevicesController.SwitchBoard.Connect();
                if (result.IsFailed)
                {
                    return result;
                }
            }
            if (DevicesController.DMM.IsConnected == false)
            {
                var result = DevicesController.DMM.Connect();
                if (result.IsFailed)
                {
                    return result;
                }
            }
            foreach(var item in Tray.NgInfos)
            {
                var result = TestOneBattery(item);
                if(result.IsFailed)
                {
                    return result;
                }
            }
            return OperateResult.CreateSuccessResult();
            //LogHelper.UiLog.Info("读取电压");
            //var read1 = DevicesController.DMM.ReadDc();
            //if (read1.IsFailed)
            //{
            //    return OperateResult.CreateFailedResult(read1.Message ?? "读取电压失败", read1.ErrorCode);
            //}
            //Battery.PVolValue = read1.Content;
            //Battery.TestTime = DateTime.Now;
            //return OperateResult.CreateSuccessResult();
        }
        private OperateResult TestOneBattery(NgInfo ngInfo)
        {
            var battery = ngInfo.Battery;
            LogHelper.UiLog.Info($"开始测试电池{battery.Position}");
            if (battery.IsTested && !ngInfo.IsNg) 
            {
                return OperateResult.CreateSuccessResult();
            }
            if(battery.Position<=20)
            {
                //ToDo:确定通道是否正确;
                int[] channels = new int[] { battery.Position, 22, 24 };
                DevicesController.SwitchBoard.OpenChannels(1, channels);
            }
            else
            {
                int[] channels = new int[] { battery.Position - 20, 22, 24 };
                DevicesController.SwitchBoard.OpenChannels(2, channels);
            }
            LogHelper.UiLog.Info("读取电压");
            var read1 = DevicesController.DMM.ReadDc();
            if (read1.IsFailed)
            {
                return OperateResult.CreateFailedResult(read1.Message ?? "读取电压失败", read1.ErrorCode);
            }
            battery.PVolValue = read1.Content;
            battery.IsTested = true;
            battery.TestTime = DateTime.Now;
            return OperateResult.CreateSuccessResult();
        }
        private void ValidateNgResult()
        {
            foreach (var ngInfo in Tray.NgInfos)
            {
                if (ngInfo.Battery.PVolValue > BatteryNgCriteria.MaxPVol)
                {
                    ngInfo.NgDescription = "电压过高";
                    ngInfo.IsNg = true;
                }
                else if (ngInfo.Battery.PVolValue < BatteryNgCriteria.MinPVol)
                {
                    ngInfo.NgDescription = "电压过低";
                    ngInfo.IsNg = true;
                }
                else
                {
                    ngInfo.NgDescription = string.Empty;
                    ngInfo.IsNg = false;
                }
            }
        }
        private OperateResult InitWork()
        {
            Tray.NgInfos = new System.Collections.ObjectModel.ObservableCollection<NgInfo>();
            LogHelper.UiLog.Info("写入本地Plc[Send_1] = 0");
            var write1 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_1"], (short)0);
            if (write1.IsFailed)
            {
                return OperateResult.CreateFailedResult(write1.Message ?? "写入本地Plc[Send_1] = 0失败", write1.ErrorCode);
            }
            LogHelper.UiLog.Info("写入本地Plc[Send_2] = 0");
            var write2 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)0);
            if (write2.IsFailed)
            {
                return OperateResult.CreateFailedResult(write2.Message ?? "写入本地Plc[Send_2] = 0失败", write2.ErrorCode);
            }
            LogHelper.UiLog.Info("写入本地Plc[Send_3] = 0");
            var write3 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)0);
            if (write3.IsFailed)
            {
                return OperateResult.CreateFailedResult(write3.Message ?? "写入本地Plc[Send_3] = 0失败", write3.ErrorCode);
            }
            return OperateResult.CreateSuccessResult();
        }
    }
}