using Azure;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.YiWei.DataBaseContext;
using Com.RePower.Ocv.Project.YiWei.Model;
using Com.RePower.Ocv.Project.YiWei.Models;
using Com.RePower.Ocv.Project.YiWei.Serivces;
using Com.RePower.Ocv.Project.YiWei.Serivces.Dto;
using Com.RePower.Ocv.Project.YiWei.Serivces.Module;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Com.RePower.Ocv.Project.YiWei.Controllers
{
    public partial class MainWorkFixed : ObservableObject, IProjectMainWork
    {
        private int _workStatus;

        public MainWorkFixed(DevicesController devicesController
            , FlowController flowController
            , Tray tray
            , BatteryNgCriteria batteryNgCriteria
            , TestOption testOption)
        {
            DevicesController = devicesController;
            FlowController = flowController;
            Tray = tray;
            BatteryNgCriteria = batteryNgCriteria;
            TestOption = testOption;
            //WmsService = wmsService;
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
        //public IWmsService WmsService { get; }

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
        /// <summary>
        /// 暂停测试
        /// </summary>
        public async void PauseWorkAsync()
        {
            if (WorkStatus != 2)
            {
                await Task.Run(() =>
                {
                    ResetEvent.Reset();
                    LogHelper.UiLog.Warn($"Pause");
                });
            }
            WorkStatus = 2;
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        public async void StartWorkAsync()
        {
            if (WorkStatus != 1)
            {
                if (WorkStatus == 0)
                {
                    WorkStatus = 1;
                    await Task.Run(() =>
                    {
                        try
                        {
                            ResetEvent.Set();
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
                    ResetEvent.Set();
                }
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public async void StopWorkAsync()
        {
            if (WorkStatus != 0)
            {
                await Task.Run(() =>
                {
                    CancelTokenSource.Cancel();
                    if (WorkStatus == 2)
                    {
                        ResetEvent.Set();
                    }
                    LogHelper.UiLog.Warn($"Stop");
                });
            }
            WorkStatus = 0;
        }

        private OperateResult DoWork()
        {
            while (true)
            {
                #region 初始化Plc
                //var init1 = InitWork();
                //if (init1.IsFailed)
                //{
                //    return init1;
                //}
                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion
                #endregion
                #region 等待测试准备信号

                //LogHelper.UiLog.Info("连接PLC");
                #region 链接设备
                if (!DevicesController.LocalPlc.IsConnected)
                {
                    var result = DevicesController.LocalPlc.Connect();
                    if (result.IsFailed)
                    {
                        return result;
                    }
                }
                if (DevicesController.SwitchBoard.IsConnected == false)
                {
                    var result = DevicesController.SwitchBoard.Connect();//切换板
                    if (result.IsFailed)
                    {
                        return result;
                    }
                }
                if (DevicesController.DMM.IsConnected == false)
                {
                    var result = DevicesController.DMM.Connect();//万用表
                    if (result.IsFailed)
                    {
                        return result;
                    }
                }
                if (DevicesController.Ohm.IsConnected == false)
                {
                    var result = DevicesController.Ohm.Connect();//万用表
                    if (result.IsFailed)
                    {
                        return result;
                    }
                }
                #endregion

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion
                LogHelper.UiLog.Info("等待Plc[上位机交互] = 0");
                var wait0 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["上位机交互"], 0);
                if (wait0.IsFailed)
                {
                    return OperateResult.CreateFailedResult(wait0.Message ?? "等待本地Plc[上位机交互] = 5失败", wait0.ErrorCode);
                }

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion
                LogHelper.UiLog.Info("等待Plc[上位机交互] = 5");
                var wait1 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["上位机交互"], 5);
                if (wait1.IsFailed)
                {
                    return OperateResult.CreateFailedResult(wait1.Message ?? "等待本地Plc[上位机交互] = 5失败", wait1.ErrorCode);
                }
                #endregion
                var tempNgInfos = new List<NgInfo>();

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion

                #region 读取电池条码1
                string batteryCode = string.Empty;
                LogHelper.UiLog.Info("读取PLC[电池条码1]");
                var read1 = DevicesController.LocalPlc.ReadString(DevicesController.LocalPlcAddressCache["电池条码1"], 50);
                if (read1.IsFailed)
                {
                    return OperateResult.CreateFailedResult(read1.Message ?? "读取Plc[电池条码1]失败", read1.ErrorCode);
                }
                batteryCode = read1.Content ?? string.Empty;
                batteryCode = Regex.Match(read1.Content ?? string.Empty, @"[0-9\.a-zA-Z_-]+").Value;
                if(string.IsNullOrEmpty(batteryCode))
                {
                    return OperateResult.CreateFailedResult("电池条码1为空");
                }
                var ngInfo = new NgInfo();
                ngInfo.Battery.BarCode = batteryCode;
                ngInfo.Battery.Position = 1;
                tempNgInfos.Add(ngInfo);
                #endregion

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion

                #region 读取电池条码2
                batteryCode = string.Empty;
                LogHelper.UiLog.Info("读取PLC[电池条码2]");
                var read2 = DevicesController.LocalPlc.ReadString(DevicesController.LocalPlcAddressCache["电池条码2"], 50);
                if (read2.IsFailed)
                {
                    return OperateResult.CreateFailedResult(read2.Message ?? "读取Plc[电池条码2]失败", read2.ErrorCode);
                }
                batteryCode = read2.Content ?? string.Empty;
                batteryCode = Regex.Match(batteryCode ?? string.Empty, @"[0-9\.a-zA-Z_-]+").Value;
                if(string.IsNullOrEmpty(batteryCode))
                {
                    return OperateResult.CreateFailedResult("电池条码2为空");
                }
                ngInfo = new NgInfo();
                ngInfo.Battery.BarCode = batteryCode;
                ngInfo.Battery.Position = 2;
                tempNgInfos.Add(ngInfo);
                Tray.TrayCode = "null";
                Tray.NgInfos = tempNgInfos;
                #endregion

                int reTestTimes = 0;
                do
                {
                    #region 暂停或停止
                    ResetEvent.WaitOne();
                    CancelToken.ThrowIfCancellationRequested();
                    #endregion

                    reTestTimes++;
                    //测试电池
                    LogHelper.UiLog.Info("开始测试电池");
                    var test1 = TestBatteries();
                    if (test1.IsFailed)
                    {
                        return test1;
                    }
                    //验证ng结果
                    ValidateNgResult();
                    break;
                    
                } while (IsDoRetest && reTestTimes < RetestTimes);

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion
                LogHelper.UiLog.Info("写入Plc[上位机交互] = 10");
                var write6 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["上位机交互"], 10);
                if (write6.IsFailed)
                {
                    return OperateResult.CreateFailedResult(write6.Message ?? "写入Plc[上位机交互] = 10失败", write6.ErrorCode);
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
                var result = DevicesController.SwitchBoard.Connect();//切换板
                if (result.IsFailed)
                {
                    return result;
                }
            }
            if (DevicesController.DMM.IsConnected == false)
            {
                var result = DevicesController.DMM.Connect();//万用表
                if (result.IsFailed)
                {
                    return result;
                }
            }
            if(DevicesController.Ohm.IsConnected == false)
            {
                var result = DevicesController.Ohm.Connect();//万用表
                if (result.IsFailed)
                {
                    return result;
                }
            }


            //关闭所有通道
            var closeResult = DevicesController.SwitchBoard.CloseAllChannels(2);
            if (closeResult.IsFailed)
            {
                return closeResult;
            }

            foreach (var item in Tray.NgInfos)
            {

                #region 暂停或停止
                ResetEvent.WaitOne();
                CancelToken.ThrowIfCancellationRequested();
                #endregion

                var result = TestOneBattery(item);
                if (result.IsFailed)
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
            CancelToken.ThrowIfCancellationRequested();
            ResetEvent.WaitOne();
            var battery = ngInfo.Battery;
            LogHelper.UiLog.Info($"开始测试电池{battery.Position}");
            if (battery.IsTested && !ngInfo.IsNg)
            {
                return OperateResult.CreateSuccessResult();
            }
            #region

            //打开对应通道
            var openResult = SwitchChannel(battery.Position);
            if (openResult.IsFailed)
            {
                return openResult;
            }
            Thread.Sleep(1000);
            #endregion
            LogHelper.UiLog.Info("读取电压");
            var read1 = DevicesController.DMM.ReadDc();
            if (read1.IsFailed)
            {
                return OperateResult.CreateFailedResult($"读取电压失败{read1.Message ?? "未知原因"}", read1.ErrorCode);
            }
            battery.VolValue = read1.Content;
            var read2 = DevicesController.Ohm.ReadRes();
            if(read2.IsFailed)
            {
                return OperateResult.CreateFailedResult($"读取内阻失败{read2.Message ?? "未知原因"}", read2.ErrorCode);
            }
            battery.Res = read2.Content;
            battery.IsTested = true;
            battery.TestTime = DateTime.Now;
            //打开对应通道
            var closeResult = SwitchChannel(battery.Position,false);
            if (closeResult.IsFailed)
            {
                return closeResult;
            }
            return OperateResult.CreateSuccessResult();
        }
        private OperateResult SwitchChannel(int channel, bool open = true)
        {
            OperateResult openResult;
            if (channel <= 19)
            {
                if (open)
                {
                    openResult = DevicesController.SwitchBoard.OpenChannel(2, channel);
                }
                else
                {
                    openResult = DevicesController.SwitchBoard.CloseChannel(2, channel);
                }
            }
            else
            {
                if (open)
                {
                    openResult = DevicesController.SwitchBoard.OpenChannel(2, channel - 19);
                }
                else
                {
                    openResult = DevicesController.SwitchBoard.CloseChannel(2, channel - 19);
                }
            }
            return openResult;
        }
        private void ValidateNgResult()
        {
            foreach (var ngInfo in Tray.NgInfos)
            {
                ngInfo.NgDescription = string.Empty;
                ngInfo.IsNg = false;
                if (ngInfo.Battery.VolValue > BatteryNgCriteria.MaxVol)
                {
                    ngInfo.NgDescription = "电压过高";
                    ngInfo.IsNg = true;
                }
                else if (ngInfo.Battery.VolValue < BatteryNgCriteria.MinVol)
                {
                    ngInfo.NgDescription = "电压过低";
                    ngInfo.IsNg = true;
                }
                if(ngInfo.Battery.Res>BatteryNgCriteria.MaxRes)
                {
                    ngInfo.NgDescription = String.IsNullOrEmpty(ngInfo.NgDescription) ? "内阻过高" : "|内阻过高";
                    ngInfo.IsNg = true;
                }
                else if (ngInfo.Battery.Res > BatteryNgCriteria.MaxRes)
                {
                    ngInfo.NgDescription = String.IsNullOrEmpty(ngInfo.NgDescription) ? "内阻过低" : "|内阻过低";
                    ngInfo.IsNg = true;
                }
            }
        }
        private OperateResult InitWork()
        {
            Tray.NgInfos = new List<NgInfo>();
            LogHelper.UiLog.Info("写入本地Plc[上位机交互] = 0");
            var write1 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["上位机交互"], 0);
            if (write1.IsFailed)
            {
                return OperateResult.CreateFailedResult(write1.Message ?? "写入本地Plc[上位机交互] = 0失败", write1.ErrorCode);
            }
            return OperateResult.CreateSuccessResult();
        }

        private OperateResult UpLoadMesResult()
        {
            OperateResult result = OperateResult.CreateFailedResult();
            try
            {

                List<RnDbOcv> listrnDbs = new List<RnDbOcv>();
                foreach (var item in Tray.NgInfos)
                {
                    RnDbOcv rnDbOcv = new RnDbOcv();
                    rnDbOcv.EqpId = "rn_" + TestOption.OcvType + "_6"; //设备编码   //EquipmentCode;
                    rnDbOcv.PcId = TestOption.OcvType + "_6";//设备号+线
                    rnDbOcv.Operation = TestOption.OcvType;//设备号
                    if (string.IsNullOrWhiteSpace(Tray.TrayCode))
                    {
                        LogHelper.UiLog.Error("托盘条码为空！");
                        result.Message = "托盘条码为空!";
                        return result;
                    }
                    rnDbOcv.TrayId = Tray.TrayCode;//托盘号
                    rnDbOcv.IsTrans = 1;//是否跨线
                    rnDbOcv.ModelNo = TestOption.OcvType + "_6";//设备号+线
                    if (Tray.NgInfos.FirstOrDefault(s => s.IsNg == true) != null)
                    {

                        rnDbOcv.TotalNgState = "NG";
                    }
                    rnDbOcv.CellId = item.Battery.BarCode ?? "KONG";
                    rnDbOcv.OcvVoltage = Convert.ToDecimal(item.Battery.VolValue);//电池电压
                    rnDbOcv.TestResult = item.IsNg == true ? "NG" : "OK";
                    rnDbOcv.TestResultDesc = item.NgDescription;
                    rnDbOcv.BatteryPos = item.Battery.Position;
                    rnDbOcv.EndDateTime = item.Battery.TestTime;
                    rnDbOcv.InsertTime = DateTime.Now;
                    rnDbOcv.TestMode = "自动";
                    rnDbOcv.PostiveShellVoltage = Convert.ToDecimal(item.Battery.PVolValue);
                    rnDbOcv.PostiveSvResult = item.IsNg == true ? "NG" : "OK";
                    listrnDbs.Add(rnDbOcv);
                }

                //listrnDbs.Add(rnDbOcv);
                using (var context = new OcvDataDbContext())
                {

                    foreach (var item in listrnDbs)
                    {
                        context.Set<RnDbOcv>().Add(item);
                    }
                    context.SaveChanges();

                }
                result.IsSuccess = true;
                return result;

            }
            catch (Exception ex)
            {

                LogHelper.UiLog.Error(ex.Message);
                result.Message = ex.Message;
                return result;
            }
        }
        //public NgInfo tempNgInfo(string Cellode,int Position) {
        
        //   NgInfo result = new NgInfo();
        //   result.Battery.BarCode=Cellode;
        //    result.Battery.Position = Position;
        //    return result;
        //}

        #region 保存测试结果到本地Excel文件中

        //public OperateResult SaveTestResultToExcel()
        //{
        //    return SaveTestData(Tray.TrayCode);
        //}

        ///// <summary>
        ///// 获取保存路径字符串
        ///// </summary>
        ///// <returns></returns>
        //private (string savePath, string title) GetSaveExcelFilePath(string trayCode)
        //{
        //    var saveDir = @"D:\OCV\OCV测试数据"; //OcvConfigFromJson.OcvResultSaveExcelPath;
        //    string dateTimeString = DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss");
        //    saveDir = Path.Combine(saveDir, DateTime.Now.ToString("yyyy-MM-dd"));
        //    if (!Directory.Exists(saveDir))
        //    {
        //        Directory.CreateDirectory(saveDir);
        //    }

        //    string savePath = Path.Combine(saveDir, $@"{TestOption.OcvType}_{trayCode}_{dateTimeString}.xlsx");
        //    string title = $"{TestOption.OcvType}_测试结果_{dateTimeString}";
        //    return (savePath, title);
        //}

        /////// <summary>
        /////// 保存新DataGridView结果到Excel文件
        /////// </summary>
        /////// <typeparam name="T"></typeparam>
        /////// <param name="testResult"></param>
        /////// <returns></returns>
        ////private (DataTable, Dictionary<string, string>) GetDataTableAndDicFromTestResult<T>(List<T> testResult) where T : class
        ////{
        ////    DataTable dt = new DataTable();
        ////    Type type = typeof(T);
        ////    Dictionary<string, string> dic = new Dictionary<string, string>();
        ////    List<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        ////    foreach (PropertyInfo propertyInfo in propertyInfos)
        ////    {
        ////        var attr = propertyInfo.GetCustomAttribute(typeof(TableColumnNameAttribute))?.As<TableColumnNameAttribute>();
        ////        if (attr != null && attr.Enable)
        ////        {
        ////            if (propertyInfo.GetCustomAttribute(typeof(IgnoreWhenOutputExcelAttribute)) == null)
        ////            {
        ////                dic.Add(propertyInfo.Name, attr.ColumnName);
        ////                dt.Columns.Add(propertyInfo.Name, typeof(string));
        ////            }
        ////        }
        ////    }
        ////    foreach (var item in testResult)
        ////    {
        ////        DataRow row = dt.NewRow();
        ////        foreach (PropertyInfo propertyInfo in propertyInfos)
        ////        {
        ////            var attr = propertyInfo.GetCustomAttribute(typeof(TableColumnNameAttribute))?.As<TableColumnNameAttribute>();
        ////            if (attr != null && attr.Enable)
        ////            {
        ////                if (propertyInfo.GetCustomAttribute(typeof(IgnoreWhenOutputExcelAttribute)) == null)
        ////                {
        ////                    PropertyInfo findProperty = item.GetPropertyObj(propertyInfo.Name);
        ////                    if (findProperty != null)
        ////                    {
        ////                        var obj = item.GetPropertyByName<object>(propertyInfo.Name);
        ////                        row[propertyInfo.Name] = obj + "";
        ////                    }
        ////                }
        ////            }
        ////        }

        ////        dt.Rows.Add(row);
        ////    }
        ////    return (dt, dic);
        ////}

        ///// <summary>
        ///// 保存测试结果
        ///// </summary>
        //private OperateResult SaveTestData(string trayCode)
        //{
        //    var pathTuple = GetSaveExcelFilePath(trayCode);
        //    try
        //    {
        //       // (DataTable, Dictionary<string, string>) formatData = GetDataTableAndDicFromTestResult(_work.TrayModel.BatteryCells);
        //        //NpoiHelperFromCommon.ExportDTtoExcel(formatData.Item1, pathTuple.title, pathTuple.savePath, formatData.Item2, false, 5000);
        //        return OperateResult.CreateSuccessResult();
        //    }
        //    catch (Exception ex)
        //    {
        //        return OperateResult.CreateFailedResult($"SaveTestData异常：{ex.Message}\r\n{ex.StackTrace}");
        //    }
        //}

        #endregion 保存测试结果到本地Excel文件中
    }
}