﻿using AutoMapper;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Mes;
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
        private int _msaCount = 1;
        /// <summary>
        /// 复测计数
        /// </summary>
        private int _retestCount = 0;
        public DevicesController DevicesController { get; }
        public SettingManager SettingManager { get; }
        public Tray Tray { get; }
        public IWmsService WmsService { get; }
        public IMesService? MesService { get; }
        public IMapper Mapper { get; }

        public MainWork(DevicesController devicesController
            , SettingManager settingManager
            , Tray tray
            , IMapper mapper
            , IWmsService wmsService
            , IMesService? mesService = null)
        {
            DevicesController = devicesController;
            SettingManager = settingManager;
            Tray = tray;
            WmsService = wmsService;
            MesService = mesService;
            Mapper = mapper;
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
                //判断是否需要复测
                while(JudgeRetest())
                {
                    //等待plc复测信号
                    var waitPlcRequestRetestResult = WaitPlcRequestRetest();
                    if (waitPlcRequestRetestResult.IsFailed)
                        return waitPlcRequestRetestResult;
                    DoPauseOrStop();
                    //测试电池
                    var testBatteriesResult1 = TestBatteries();
                    if(testBatteriesResult1.IsFailed)
                        return testBatteriesResult1;
                    DoPauseOrStop();
                    //验证Ng情况
                    var validateNgInfoResult1 = ValidateNgInfo();
                    if (validateNgInfoResult1.IsFailed)
                        return validateNgInfoResult1;
                    DoPauseOrStop();
                }
                //向plc发送测试结果
                var sendPlcNgInfoResult = SendPlcNgInfo();
                if(sendPlcNgInfoResult.IsFailed)
                    return sendPlcNgInfoResult;
                DoPauseOrStop();
                //发送测试完成
                var sendPlcTestComplateResult = SendPlcTestComplete();
                if (sendAxisCoordinatesResult.IsFailed)
                    return sendPlcTestComplateResult;
                DoPauseOrStop();
                //保存到本地数据库
                var saveToLocationResult = SaveToLocation();
                if(saveToLocationResult.IsFailed)
                    return saveToLocationResult;
                DoPauseOrStop();
                var uploadTestResultToMesResult = UploadTestResultToMes();
                if(uploadTestResultToMesResult.IsFailed)
                    return uploadTestResultToMesResult;
                DoPauseOrStop();
                var sendPlcOutboundResult = SendPlcOutbound();
                if(sendPlcOutboundResult.IsFailed)
                    return sendPlcOutboundResult;
                DoPauseOrStop();
            }
        }
        /// <summary>
        /// 判断是否需要复测
        /// </summary>
        /// <returns>true:需要复测;false:不需要复测</returns>

        public bool JudgeRetest()
        {
            if ((SettingManager.CurrentTestOption?.IsDoRetest ?? false)
                && (_retestCount < (SettingManager.CurrentTestOption?.RetestTimes ?? int.MinValue))
                && (Tray.NgInfos.Any(x => x.IsNg)))
                return true;
            return false;
        }
    }
}
