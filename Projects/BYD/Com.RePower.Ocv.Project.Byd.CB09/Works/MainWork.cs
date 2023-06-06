using System.Configuration;
using System.Reflection;
using AutoMapper;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works
{
    public partial class MainWork : MainWorkAbstract
    {
        protected readonly ITemperatureSensor TemperatureSensor;
        protected readonly IPlc Plc;
        protected readonly IDmm Dmm;
        protected readonly IOhm Ohm;
        protected readonly ISwitchBoard SwitchBoard;
        protected readonly Tray Tray;
        protected readonly IWmsService WmsService;
        protected readonly IMesService MesService;
        protected readonly IMapper Mapper;
        protected readonly PlcCacheSetting PlcCacheSetting;

        public MainWork(IPlc plc, IDmm dmm, IOhm ohm, ISwitchBoard switchBoard, ITemperatureSensor temperatureSensor,
            Tray tray, IWmsService wmsService, IMesService mesService, IMapper mapper)
        {
            TemperatureSensor = temperatureSensor;
            Plc = plc;
            Dmm = dmm;
            Ohm = ohm;
            SwitchBoard = switchBoard;
            Tray = tray;
            WmsService = wmsService;
            MesService = mesService;
            Mapper = mapper;
            PlcCacheSetting = PlcCacheSetting.Default;



            var s = CalibrationSetting.Default;
        }

        private bool _isMsaTest;
        /// <summary>
        /// 是否开启mas测试
        /// </summary>
        public bool IsMsaTest
        {
            get => _isMsaTest;
            set => SetProperty(ref _isMsaTest, value);
        }

        protected override OperateResult DoWork()
        {
            while (true)
            {
                #region 连接设备
                LogHelper.UiLog.Info("开始连接设备");
                var connectResult = ConnectDevices();
                if (connectResult.IsFailed)
                    return connectResult;
                LogHelper.UiLog.Info("连接设备成功");
                DoPauseOrStop();
                #endregion
                #region 开始PLC心跳
                KeepHeartbeat();
                #endregion
                #region 等待请求读取条码
                LogHelper.UiLog.Info("等待请求读取托盘条码");
                var waitResult = WaitTrayCodeRequest();
                if (waitResult.IsFailed)
                    return waitResult;
                LogHelper.UiLog.Info("等待请求读取托盘条码成功");
                DoPauseOrStop();
                #endregion
                #region 读取托盘条码
                LogHelper.UiLog.Info("读取托盘条码");
                var readResult = ReadTrayCode();
                if (readResult.IsFailed)
                    return readResult;
                LogHelper.UiLog.Info("读取托盘条码成功");
                DoPauseOrStop();
                #endregion
                #region 获取电芯条码
                LogHelper.UiLog.Info("获取电芯条码");
                var getBatteriesInfoResult = GetBatteriesInfo();
                if (getBatteriesInfoResult.IsFailed)
                    return getBatteriesInfoResult;
                LogHelper.UiLog.Info("获取电芯条码成功");
                #endregion
                #region 下发坐标
                LogHelper.UiLog.Info("下发坐标");
                var sendResult = SendCoordinates();
                if (sendResult.IsFailed)
                    return sendResult;
                LogHelper.UiLog.Info("下发坐标成功");
                DoPauseOrStop();
                #endregion
                #region 下发读码、下发坐标完成信号
                LogHelper.UiLog.Info("下发读码，下发坐标完成信号");
                sendResult = SendReadTrayCodeComplete();
                if (sendResult.IsFailed)
                    return sendResult;
                LogHelper.UiLog.Info("下发读码、下发坐标完成信号成功");
                DoPauseOrStop();
                #endregion
                #region 等待测试请求
                LogHelper.UiLog.Info("等待测试请求");
                waitResult = WaitTestRequest();
                if (waitResult.IsFailed)
                    return waitResult;
                LogHelper.UiLog.Info("等待测试请求成功");
                DoPauseOrStop();
                #endregion
                if (IsMsaTest)
                {
                    #region 执行msa测试
                    LogHelper.UiLog.Info("执行msa测试");
                    var msaResult = MsaTest();
                    if (msaResult.IsFailed)
                        return msaResult;
                    LogHelper.UiLog.Info("执行msa测试成功");
                    #endregion
                }
                else
                {
                    #region 测试电池
                    LogHelper.UiLog.Info("测试电池");
                    var testResult = TestBatteries();
                    if (testResult.IsFailed)
                        return testResult;
                    LogHelper.UiLog.Info("测试电池成功");
                    DoPauseOrStop();
                    #endregion
                    #region 验证Ng信息
                    LogHelper.UiLog.Info("验证Ng信息");
                    var verifyResult = VerifyNg();
                    if (verifyResult.IsFailed)
                        return verifyResult;
                    LogHelper.UiLog.Info("验证Ng信息成功");
                    #endregion
                    #region 复测
                    var retestResult = ReTest();
                    if (retestResult.IsFailed)
                        return retestResult;
                    #endregion
                    #region 上传结果
                    var uploadResult = UploadTestResult();
                    if (uploadResult.IsFailed)
                        return uploadResult;
                    #endregion
                    #region 测试完成
                    var testCompleteResult = TestComplete();
                    if (testCompleteResult.IsFailed) return testCompleteResult;
                    #endregion
                }
            }
            //return OperateResult.CreateSuccessResult();
        }
    }
}