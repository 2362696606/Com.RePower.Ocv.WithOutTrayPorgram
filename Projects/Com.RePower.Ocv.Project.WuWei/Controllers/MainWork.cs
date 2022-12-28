using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.WuWei.Model;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    /*
    public partial class MainWork : ObservableObject,IProjectMainWork
    {
        private int _workStatus;

        public MainWork(DevicesController devicesController
            ,FlowController flowController
            ,NgInfo ngInfo
            ,BatteryNgCriteria batteryNgCriteria
            ,TestOption testOption)
        {
            DevicesController = devicesController;
            FlowController = flowController;
            NgInfo = ngInfo;
            BatteryNgCriteria = batteryNgCriteria;
            TestOption = testOption;
        }

        public int WorkStatus
        {
            get { return _workStatus; }
            set { SetProperty(ref _workStatus,value); }
        }

        public DevicesController DevicesController { get; }
        public FlowController FlowController { get; }
        public NgInfo NgInfo { get; }
        public BatteryNgCriteria BatteryNgCriteria { get; }
        public TestOption TestOption { get; }
        public bool IsDoUploadToMes
        {
            get { return TestOption.IsDoUploadToMes; }
        }

        public Battery Battery 
        {
            get { return NgInfo.Battery; } 
        }

        / <summary>
        / 是否复测
        / </summary>
        public bool IsDoRetest
        {
            get { return TestOption.IsDoRetest; }
        }
        / <summary>
        / 复测次数
        / </summary>
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
            get { return FlowController.CancelToken;} 
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
                            if(result.IsFailed)
                            {
                                LogHelper.UiLog.Warn(result.Message);
                            }
                        }
                        catch (Exception e)
                        {
                            if(e.GetType() == typeof(OperationCanceledException))
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
            else if(WorkStatus == 2)
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
            while(true)
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
                int reReadTimes = 0;
                string batteryCode = string.Empty;
                do
                {
                    reReadTimes++;
                    LogHelper.UiLog.Info("等待物流Plc[握手类型] = 1");
                    var wait2 = DevicesController.LogisticsPlc.Wait(DevicesController.LogisticsPlcAddressCache["握手类型"], 1);
                    if (wait2.IsFailed)
                    {
                        return OperateResult.CreateFailedResult(wait2.Message ?? "等待物流Plc[握手类型] = 1失败", wait2.ErrorCode);
                    }
                    LogHelper.UiLog.Info("读取物流Plc[托盘条码]");
                    var read1 = DevicesController.LogisticsPlc.ReadString(DevicesController.LogisticsPlcAddressCache["托盘条码"], 20);
                    if (read1.IsFailed)
                    {
                        return OperateResult.CreateFailedResult(read1.Message ?? "读取物流Plc[托盘条码]失败", read1.ErrorCode);
                    }
                    batteryCode = read1.Content ?? string.Empty;
                    Battery.BarCode = batteryCode;
                    validateResult = ValidateBatteryCode(batteryCode);
                    if (!validateResult)
                    {
                        LogHelper.UiLog.Info("写入物流Plc[握手确认] = 2");
                        var write1 = DevicesController.LogisticsPlc.Write(DevicesController.LogisticsPlcAddressCache["握手确认"], 2);
                        if (write1.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write1.Message ?? "写入物流Plc[握手确认] = 2失败", wait2.ErrorCode);
                        }
                    }
                }
                while (reReadTimes < 5 && !validateResult);
                if (!validateResult)
                {
                    return OperateResult.CreateFailedResult($"读取托盘条码失败,{batteryCode}不合规");
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
                    测试电池
                    var test1 = TestBattery();
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
                    验证ng结果
                    ValidateNgResult();
                    if (NgInfo.IsNg && IsDoRetest && reReadTimes < RetestTimes)
                    {
                        LogHelper.UiLog.Info("写入本地Plc[Send_2] = 2");
                        var write4 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)2);
                        if (write4.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write4.Message ?? "写入本地Plc[Send_2] = 2失败", write4.ErrorCode);
                        }
                    }
                    else if (NgInfo.IsNg)
                    {
                        LogHelper.UiLog.Info("写入本地Plc[Send_3] = 2");
                        var write4 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)2);
                        if (write4.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write4.Message ?? "写入本地Plc[Send_3] = 2失败", write4.ErrorCode);
                        }
                        return OperateResult.CreateFailedResult("电芯异常，已停止测试");
                    }
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
        private OperateResult TestBattery()
        {
            if (DevicesController.DMM.IsConnected == false)
            {
                var result = DevicesController.DMM.Connect();
                if(result.IsFailed)
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("读取电压");
            var read1 = DevicesController.DMM.ReadDc();
            if(read1.IsFailed)
            {
                return OperateResult.CreateFailedResult(read1.Message ?? "读取电压失败", read1.ErrorCode);
            }
            Battery.PVolValue = read1.Content;
            Battery.TestTime = DateTime.Now;
            return OperateResult.CreateSuccessResult();
        }
        private void ValidateNgResult()
        {
            if(Battery.PVolValue>BatteryNgCriteria.MaxPVol)
            {
                NgInfo.NgDescription = "电压过高";
                NgInfo.IsNg = true;
            }
            else if(Battery.PVolValue<BatteryNgCriteria.MinPVol)
            {
                NgInfo.NgDescription = "电压过低";
                NgInfo.IsNg = true;
            }
            else
            {
                NgInfo.NgDescription = string.Empty;
                NgInfo.IsNg= false;
            }
        }
        private OperateResult InitWork()
        {
            NgInfo.Battery = new Battery();
            NgInfo.NgDescription = string.Empty;
            NgInfo.IsNg = false;
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
    }*/
}
