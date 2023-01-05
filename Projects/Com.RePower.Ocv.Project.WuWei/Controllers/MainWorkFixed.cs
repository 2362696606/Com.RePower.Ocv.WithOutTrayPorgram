using Azure;
using Com.RePower.Ocv.Model;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.WuWei.DataBaseContext;
using Com.RePower.Ocv.Project.WuWei.Model;
using Com.RePower.Ocv.Project.WuWei.Models;
using Com.RePower.Ocv.Project.WuWei.Serivces;
using Com.RePower.Ocv.Project.WuWei.Serivces.Dto;
using Com.RePower.Ocv.Project.WuWei.Serivces.Module;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Com.RePower.Ocv.Project.WuWei.Controllers
{
    public partial class MainWorkFixed : ObservableObject, IProjectMainWork
    {
        private int _workStatus;

        /// <summary>
        /// 执行MSA
        /// </summary>
        private bool _doMsa;

        /// <summary>
        /// MSA标志
        /// </summary>
        [ObservableProperty]
        private bool _msaFlag;

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

        /// <summary>
        /// 工作状态
        /// </summary>
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

        


        /// <summary>
        /// 是否上传到Mes
        /// </summary>
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

        /// <summary>
        /// 暂停标志
        /// </summary>
        public ManualResetEvent ResetEvent
        {
            get { return FlowController.ResetEvent; }
        }

        /// <summary>
        /// 停止标志源
        /// </summary>
        public CancellationTokenSource CancelTokenSource
        {
            get { return FlowController.CancelTokenSource; }
        }

        /// <summary>
        /// 停止标志
        /// </summary>
        public CancellationToken CancelToken
        {
            get { return FlowController.CancelToken; }
        }

        /// <summary>
        /// 暂停测试
        /// </summary>
        public async void PauseWorkAsync()
        {
            WorkStatus = 2;
            await Task.Run(() =>
            {
                ResetEvent.Reset();
            });
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        public async void StartWorkAsync()
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

        /// <summary>
        /// 停止测试
        /// </summary>
        public async void StopWorkAsync()
        {
            WorkStatus = 0;
            await Task.Run(() =>
            {
                CancelTokenSource.Cancel();
            });
        }

        /// <summary>
        /// 测试工作流
        /// </summary>
        /// <returns>测试结果</returns>
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
                #endregion
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 等待测试准备信号
                LogHelper.UiLog.Info("等待Plc[Read_1] = 1");
                var wait1 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["Read_1"], (short)1, cancellation: this.CancelToken);
                if (wait1.IsFailed)
                {
                    return OperateResult.CreateFailedResult(wait1.Message ?? "等待Plc[Read_1] = 1失败", wait1.ErrorCode);
                }
                #endregion
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 读取托盘条码
                var readTrayCodeResult = ReadTrayCode();
                if (readTrayCodeResult.IsFailed)
                    return readTrayCodeResult; 
                #endregion
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 获取电芯条码
                var getBatterysCodeResult = GetBatterysCode();
                if (getBatterysCodeResult.IsFailed)
                    return getBatterysCodeResult;
                #endregion
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 下发可以开始测试
                LogHelper.UiLog.Info("写入Plc[Send_1] = 1");
                var write2 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_1"], (short)1);
                if (write2.IsFailed)
                {
                    return OperateResult.CreateFailedResult($"写入Plc[Send_1] = 1失败{write2.Message ?? "未知原因"}", write2.ErrorCode);
                }
                #endregion
                int reTestTimes = 0;
                do
                {
                    reTestTimes++;
                    #region 停止或暂停
                    CancelToken.ThrowIfCancellationRequested();
                    ResetEvent.WaitOne();
                    #endregion
                    #region 等待气缸伸出到位
                    LogHelper.UiLog.Info("等待本地Plc[Read_1] = 2");
                    var wait3 = DevicesController.LocalPlc.Wait(DevicesController.LocalPlcAddressCache["Read_1"], (short)2, cancellation: this.CancelToken);
                    if (wait3.IsFailed)
                    {
                        return OperateResult.CreateFailedResult($"等待Plc[Read_1] = 2失败:{wait3.Message ?? "未知原因"}", wait3.ErrorCode);
                    }
                    #endregion
                    #region 停止或暂停
                    CancelToken.ThrowIfCancellationRequested();
                    ResetEvent.WaitOne();
                    #endregion
                    #region 测试电池
                    LogHelper.UiLog.Info("开始测试电池");
                    var test1 = TestBatteries();
                    if (test1.IsFailed)
                    {
                        return test1;
                    }
                    #endregion
                    #region 停止或暂停
                    CancelToken.ThrowIfCancellationRequested();
                    ResetEvent.WaitOne();
                    #endregion
                    #region 下发测试完成
                    LogHelper.UiLog.Info("写入本地Plc[Send_1] = 2");
                    var write3 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_1"], (short)2);
                    if (write3.IsFailed)
                    {
                        return OperateResult.CreateFailedResult(write3.Message ?? "写入Plc[Send_1] = 2失败", write3.ErrorCode);
                    }
                    #endregion
                    #region 停止或暂停
                    CancelToken.ThrowIfCancellationRequested();
                    ResetEvent.WaitOne();
                    #endregion
                    //验证ng结果
                    ValidateNgResult();
                    #region 复测或下发结果
                    #region 复测
                    if (Tray.NgInfos.Any(x => x.IsNg) && IsDoRetest && reTestTimes < RetestTimes)
                    {
                        LogHelper.UiLog.Info("写入Plc[Send_2] = 2");
                        var write4 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)2);
                        if (write4.IsFailed)
                        {
                            return OperateResult.CreateFailedResult(write4.Message ?? "写入Plc[Send_2] = 2失败", write4.ErrorCode);
                        }
                    }
                    #endregion
                    #region 下发测试结果
                    else
                    {
                        if (Tray.NgInfos.Any(x => x.IsNg))
                        {
                            LogHelper.UiLog.Info("写入本地Plc[Send_3] = 2");
                            var write5 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)2);
                            if (write5.IsFailed)
                            {
                                return OperateResult.CreateFailedResult($"写入Plc[Send_3] = 2失败:{write5.Message ?? "未知原因"}", write5.ErrorCode);
                            }
                        }
                        else
                        {
                            LogHelper.UiLog.Info("写入本地Plc[Send_3] = 1");
                            var write5 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)1);
                            if (write5.IsFailed)
                            {
                                return OperateResult.CreateFailedResult($"写入Plc[Send_3] = 1失败:{write5.Message ?? "未知原因"}", write5.ErrorCode);
                            }
                        }
                        break;
                    }
                    #endregion 
                    #endregion
                    #region 停止或暂停
                    CancelToken.ThrowIfCancellationRequested();
                    ResetEvent.WaitOne();
                    #endregion
                } while (IsDoRetest && reTestTimes <= RetestTimes);
                #region 上传结果
                if (IsDoUploadToMes)
                {
                    var mesResult = UpLoadMesResult();
                    if (!mesResult.IsSuccess)
                    {
                        SetAlarm();
                        return OperateResult.CreateFailedResult("上传到MES数据库失败," + mesResult.Message);
                    }
                    LogHelper.UiLog.Info("上传到MES数据库成功！");
                }
                var getupResult = WmsService.UploadTestResult();
                if (getupResult.IsFailed)
                {
                    SetAlarm();
                    return OperateResult.CreateFailedResult("上传调度OCV数据失败," + getupResult.Message);
                }
                LogHelper.UiLog.Info("上传调度OCV数据成功！");
                #endregion
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                #region 下发放行电池
                LogHelper.UiLog.Info("写入Plc[Send_2] = 1");
                var write6 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)1);
                if (write6.IsFailed)
                {
                    return OperateResult.CreateFailedResult(write6.Message ?? "写入Plc[Send_1] = 1失败", write6.ErrorCode);
                }
                write6 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_2"], (short)1);
                if (write6.IsFailed)
                {
                    return OperateResult.CreateFailedResult(write6.Message ?? "写入Plc[Send_1] = 1失败", write6.ErrorCode);
                }
                #endregion

                this._doMsa = MsaFlag;
            }
        }

        /// <summary>
        /// 获取托盘条码
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult ReadTrayCode()
        {
            #region 读取托盘条码
            bool validateResult = false;
            string trayCode = string.Empty;
            LogHelper.UiLog.Info("读取Plc[Read_2]");
            var read1 = DevicesController.LocalPlc.ReadString(DevicesController.LocalPlcAddressCache["Read_2"], 20);
            if (read1.IsFailed)
            {
                return OperateResult.CreateFailedResult($"读取物流Plc[托盘条码]失败{read1.Message ?? "未知原因"}", read1.ErrorCode);
            }
            trayCode = read1.Content ?? string.Empty;
            trayCode = Regex.Match(read1.Content ?? string.Empty, @"[0-9\.a-zA-Z_-]+").Value;
            Tray.TrayCode = trayCode;
            validateResult = ValidateTrayCode(trayCode);
            if (!validateResult)
            {
                return OperateResult.CreateFailedResult($"读取托盘条码失败,{trayCode}不合规");
            }
            return OperateResult.CreateSuccessResult();
            #endregion
        }

        /// <summary>
        /// 请求电芯条码
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult GetBatterysCode()
        {

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
                SetAlarm();
                return OperateResult.CreateFailedResult("请求电芯条码返回值为null");
            }
            if (resultObj.Result == 0)
            {
                SetAlarm();
                return OperateResult.CreateSuccessResult($"请求电芯条码失败:{resultObj.Message??"未知原因"}");
            }
            if (resultObj.PileContent == null)
            {
                SetAlarm();
                return OperateResult.CreateFailedResult($"请求电芯条码失败，主体为null");
            }
            if (resultObj.PileContent.PalletBarcode != Tray.TrayCode)
            {
                SetAlarm();
                return OperateResult.CreateFailedResult($"请求电芯条码失败，WMS返回托盘条码{resultObj.PileContent.PalletBarcode}与本地托盘条码{Tray.TrayCode}不一致");
            }
            var tempNgInfos = new System.Collections.ObjectModel.ObservableCollection<NgInfo>();
            foreach (var item in resultObj.PileContent.Batterys)
            {
                NgInfo ngInfo = new NgInfo();
                ngInfo.Battery.BarCode = item.BatteryBarcode;
                ngInfo.Battery.Position = item.PalletIndex;
                ngInfo.Battery.BatteryType = resultObj.PileContent.BatteryType == "102" ? 1 : 0;
                tempNgInfos.Add(ngInfo);
            }
            tempNgInfos.OrderBy(x => x.Battery.Position);
            Tray.NgInfos = tempNgInfos;
            return OperateResult.CreateSuccessResult();
            #endregion
        }

        /// <summary>
        /// 校验电芯条码是否合规
        /// </summary>
        /// <param name="batteryCode">电芯条码</param>
        /// <returns>验证结果</returns>
        private bool ValidateTrayCode(string batteryCode)
        {
            return true;
        }

        /// <summary>
        /// 测试所有电池
        /// </summary>
        /// <returns>操作结果</returns>
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
            //关闭所有通道
            var closeResult = DevicesController.SwitchBoard.CloseAllChannels(1);
            if(closeResult.IsFailed)
            {
                return closeResult;
            }
            closeResult = DevicesController.SwitchBoard.CloseAllChannels(2);
            if(closeResult.IsFailed)
            {
                return closeResult;
            }

            foreach (var item in Tray.NgInfos)
            {
                #region 停止或暂停
                CancelToken.ThrowIfCancellationRequested();
                ResetEvent.WaitOne();
                #endregion
                var result = TestOneBattery(item);
                if (result.IsFailed)
                {
                    return result;
                }
            }
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 测试单个电池
        /// </summary>
        /// <param name="ngInfo">单个电池ngInfo</param>
        /// <returns>操作结果</returns>
        private OperateResult TestOneBattery(NgInfo ngInfo)
        {
            var battery = ngInfo.Battery;
            LogHelper.UiLog.Info($"开始测试电池{battery.Position}");
            if (battery.IsTested && !ngInfo.IsNg)
            {
                return OperateResult.CreateSuccessResult();
            }
            //打开对应通道
            var openResult = SwitchChannel(battery.Position);
            if (openResult.IsFailed)
            {
                return openResult;
            }
            LogHelper.UiLog.Info("读取电压");
            Thread.Sleep(TestOption.DMMReadDelayWhenSwitch);
            var read1 = DevicesController.DMM.ReadDc();
            if (read1.IsFailed)
            {
                return OperateResult.CreateFailedResult(read1.Message ?? "读取电压失败", read1.ErrorCode);
            }
            battery.PVolValue = read1.Content;
            battery.IsTested = true;
            battery.TestTime = DateTime.Now;
            //关闭对应通道
            var closeResult = SwitchChannel(battery.Position,false);
            if (closeResult.IsFailed)
            {
                return closeResult;
            }
            Thread.Sleep(TestOption.SwitchDelay);
            return OperateResult.CreateSuccessResult();
        }

        /// <summary>
        /// 切换通道
        /// </summary>
        /// <param name="channel">通道号</param>
        /// <param name="open">开或关</param>
        /// <returns>操作结果</returns>
        private OperateResult SwitchChannel(int channel,bool open=true)
        {
            OperateResult openResult;
            if (channel <= 19)
            {
                if (open)
                {
                    openResult = DevicesController.SwitchBoard.OpenChannel(1, channel);
                }
                else
                {
                    openResult = DevicesController.SwitchBoard.CloseChannel(1, channel);
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

        /// <summary>
        /// 验证电池Ng情况
        /// </summary>
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

        /// <summary>
        /// 初始化Plc
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 上传Mes
        /// </summary>
        /// <returns></returns>
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
                        context.RnDbOcv0.Add(item);
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

        /// <summary>
        /// 报警
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult SetAlarm()
        {
            var write3 = DevicesController.LocalPlc.Write(DevicesController.LocalPlcAddressCache["Send_3"], (short)2);
            return write3;
        }




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

        ///// <summary>
        ///// 保存新DataGridView结果到Excel文件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="testResult"></param>
        ///// <returns></returns>
        //private (DataTable, Dictionary<string, string>) GetDataTableAndDicFromTestResult<T>(List<T> testResult) where T : class
        //{
        //    DataTable dt = new DataTable();
        //    Type type = typeof(T);
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    List<PropertyInfo> propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
        //    foreach (PropertyInfo propertyInfo in propertyInfos)
        //    {
        //        var attr = propertyInfo.GetCustomAttribute(typeof(TableColumnNameAttribute))?.As<TableColumnNameAttribute>();
        //        if (attr != null && attr.Enable)
        //        {
        //            if (propertyInfo.GetCustomAttribute(typeof(IgnoreWhenOutputExcelAttribute)) == null)
        //            {
        //                dic.Add(propertyInfo.Name, attr.ColumnName);
        //                dt.Columns.Add(propertyInfo.Name, typeof(string));
        //            }
        //        }
        //    }
        //    foreach (var item in testResult)
        //    {
        //        DataRow row = dt.NewRow();
        //        foreach (PropertyInfo propertyInfo in propertyInfos)
        //        {
        //            var attr = propertyInfo.GetCustomAttribute(typeof(TableColumnNameAttribute))?.As<TableColumnNameAttribute>();
        //            if (attr != null && attr.Enable)
        //            {
        //                if (propertyInfo.GetCustomAttribute(typeof(IgnoreWhenOutputExcelAttribute)) == null)
        //                {
        //                    PropertyInfo findProperty = item.GetPropertyObj(propertyInfo.Name);
        //                    if (findProperty != null)
        //                    {
        //                        var obj = item.GetPropertyByName<object>(propertyInfo.Name);
        //                        row[propertyInfo.Name] = obj + "";
        //                    }
        //                }
        //            }
        //        }

        //        dt.Rows.Add(row);
        //    }
        //    return (dt, dic);
        //}

        ///// <summary>
        ///// 保存测试结果
        ///// </summary>
        //private OperateResult SaveTestData(string trayCode)
        //{
        //    var pathTuple = GetSaveExcelFilePath(trayCode);
        //    try
        //    {
        //        //(DataTable, Dictionary<string, string>) formatData = GetDataTableAndDicFromTestResult(_work.TrayModel.BatteryCells);
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