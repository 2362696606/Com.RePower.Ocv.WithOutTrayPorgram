using AutoMapper;
using Com.RePower.Ocv.Model.DataBaseContext;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.DbContext;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Services;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works
{
    public partial class MainWork : MainWorkAbstract
    {
        public MainWork(Tray tray
            , DevicesController devicesController
            , LocalTestResultDbContext localContext
            , IMapper mapper
            , IWmsService? wmsService = null
            , OcvSceneContext? sceneContext = null)
        {
            Tray = tray;
            DevicesController = devicesController;
            LocalContext = localContext;
            Mapper = mapper;
            WmsService = wmsService;
            SceneContext = sceneContext;
        }


        private bool _isScheduledCalibration;
        /// <summary>
        /// 已预约校准
        /// </summary>
        public bool IsScheduledCalibration
        {
            get { return _isScheduledCalibration; }
            private set { SetProperty(ref _isScheduledCalibration, value); }
        }

        public SettingManager SettingManager
        {
            get { return SettingManager.Instance; }
        }
        public Tray Tray { get; }
        public DevicesController DevicesController { get; }
        public LocalTestResultDbContext LocalContext { get; }
        public IMapper Mapper { get; }
        public IWmsService? WmsService { get; }
        public OcvSceneContext? SceneContext { get; }

        protected override OperateResult DoWork()
        {
            while(true)
            {
                DoPauseOrStop();
                //连接硬件
                var connectResult = ConnectDevices();
                if (connectResult.IsFailed)
                    return connectResult;
                if (!IsScheduledCalibration)
                {
                    DoPauseOrStop();
                    //测试准备
                    var testPreParationResult = TestPreparation();
                    if (testPreParationResult.IsFailed)
                        return testPreParationResult;
                    DoPauseOrStop();
                    //等待Plc请求测试
                    var waitPlcRequestTestResult = WaitPlcRequestTest();
                    if (waitPlcRequestTestResult.IsFailed)
                        return waitPlcRequestTestResult;
                    DoPauseOrStop();
                    //读取托盘条码
                    var getTrayCodeResult = GetTrayCode();
                    if (getTrayCodeResult.IsFailed)
                        return getTrayCodeResult;
                    DoPauseOrStop();
                    //获取电芯条码
                    var getBatteriesInfoResult = GetBatteriesInfo();
                    if (getBatteriesInfoResult.IsFailed)
                        return getBatteriesInfoResult;
                    DoPauseOrStop();
                    var sendStartTestResult = SendStartTest();
                    if (sendStartTestResult.IsFailed)
                        return sendStartTestResult;
                    DoPauseOrStop();
                    //测试电池
                    var testBatteriesResult = TestBatteries();
                    if (testBatteriesResult.IsFailed)
                        return testBatteriesResult;
                    DoPauseOrStop();
                    var verfyKValueResult = VerifyKValue();
                    if (verfyKValueResult.IsFailed)
                        return verfyKValueResult;
                    DoPauseOrStop();
                    var saveToLocationResult = SaveToLocation();
                    if (saveToLocationResult.IsFailed)
                        return saveToLocationResult;
                    DoPauseOrStop();
                    var saveToSceneDbResult = SaveToSceneDb();
                    if (saveToSceneDbResult.IsFailed)
                        return saveToSceneDbResult;
                    DoPauseOrStop();
                    //var uploadToMesResult = UploadTestResultToMes();
                    //if (uploadToMesResult.IsFailed)
                    //    return uploadToMesResult;
                    //DoPauseOrStop();
                    var unBindingResult = UnBindingTray();
                    if (unBindingResult.IsFailed)
                        return unBindingResult;
                    DoPauseOrStop();
                    var waitRequestUbBindResult = WaitRequestUnBinding();
                    if (waitRequestUbBindResult.IsFailed)
                        return waitRequestUbBindResult;
                    var sendUnBindResult = SendStartUnBinding();
                    if (sendUnBindResult.IsFailed)
                        return sendUnBindResult;
                    var waitUnBindComplateResult = WaitUnBindComplate();
                    if (waitUnBindComplateResult.IsFailed)
                        return waitUnBindComplateResult;
                    var sendUnBindComplateResult = SendUnBindComplate();
                    if (sendUnBindComplateResult.IsFailed)
                        return sendUnBindComplateResult;
                    var uploadToWmsResult = UploadTestResultToWms();
                    if (uploadToWmsResult.IsFailed)
                        return uploadToWmsResult;
                    DoPauseOrStop();
                    var requestAllLocateCellResult = RequestAllLocateCellToWms();
                    if (requestAllLocateCellResult.IsFailed)
                        return requestAllLocateCellResult;
                    DoPauseOrStop();
                    var sendAllTestComplateResult = SendAllTestComplate();
                    if (sendAllTestComplateResult.IsFailed)
                        return sendAllTestComplateResult;

                    LogHelper.UiLog.Info("等待拆盘标志位归零");
                    string address = SettingManager.PlcValueCacheSetting?["拆盘标志位"]?.Address ?? string.Empty;
                    if (string.IsNullOrEmpty(address))
                        return OperateResult.CreateFailedResult("无法获取\"拆盘标志位\"地址");
                    var writeResult = DevicesController.Plc?.Wait(address, (short)0) ?? OperateResult.CreateFailedResult("Plc实例为null");
                    if (writeResult.IsFailed)
                    {
                        return writeResult;
                    }
                    LogHelper.UiLog.Info("成功等待拆盘标志位归零，开始下一轮"); 
                }
                else
                {
                    var waitResult = WaitRequestCalibration();
                    if (waitResult.IsFailed) return waitResult;
                    var writeResult = SendStartCalibration();
                    if (writeResult.IsFailed) return writeResult;
                    waitResult = WaitRequestMeasure();
                    if (waitResult.IsFailed) return waitResult;
                    var doResult = DoCalibration();
                    if (doResult.IsFailed) return doResult;
                    writeResult = SendMeasureComplate();
                    if (writeResult.IsFailed) return writeResult;
                }
            }
        }
    }
}
