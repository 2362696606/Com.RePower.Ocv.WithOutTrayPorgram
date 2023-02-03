using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Wms;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers.Works
{
    public partial class MainWork : MainWorkAbstract
    {
        /// <summary>
        /// 是否是msa测试
        /// </summary>
        [ObservableProperty]
        private bool _isMsaTest;
        /// <summary>
        /// msa计数
        /// </summary>
        private int _msaCount;
        /// <summary>
        /// 复测计数
        /// </summary>
        private int _retestCount;
        public DevicesController DevicesController { get; }
        public SettingManager SettingManager { get; }
        public Tray Tray { get; }
        public IWmsService WmsService { get; }

        public MainWork(DevicesController devicesController
            , SettingManager settingManager
            , Tray tray
            , IWmsService wmsService)
        {
            DevicesController = devicesController;
            SettingManager = settingManager;
            Tray = tray;
            WmsService = wmsService;
        }

        protected override OperateResult DoWork()
        {
            while (true)
            {
                DoPauseOrStop();
                //测试准备
                var testPreParationResult = TestPreparation();
                if (testPreParationResult.IsFailed)
                    return testPreParationResult;
                DoPauseOrStop();
                //获取托盘条码
                var getTrayCodeResult = GetTrayCode();
                if (getTrayCodeResult.IsFailed)
                    return getTrayCodeResult;
                DoPauseOrStop();
                //下发x1，x2轴坐标
                var sendAxisCoordinatesResult = SendAxisCoordinates();
                if (sendAxisCoordinatesResult.IsFailed)
                    return sendAxisCoordinatesResult;
                DoPauseOrStop();
                //获取电芯条码
                var getBatteriesInfoResult = GetBatteriesInfo();
                if (getBatteriesInfoResult.IsFailed)
                    return getBatteriesInfoResult;
                DoPauseOrStop();
                //等待Plc请求测试
                var waitPlcRequestTestResult = WaitPlcRequestTest();
                if (waitPlcRequestTestResult.IsFailed)
                    return waitPlcRequestTestResult;
                DoPauseOrStop();
                //测试电池
                var testBatteriesResult = TestBatteries();
                if (testBatteriesResult.IsFailed)
                    return testBatteriesResult;
                DoPauseOrStop();
                //验证Ng情况
                var validateNgInfoResult = ValidateNgInfo();
                if(validateNgInfoResult.IsFailed)
                    return validateNgInfoResult;
                DoPauseOrStop();
            }
        }
    }
}
