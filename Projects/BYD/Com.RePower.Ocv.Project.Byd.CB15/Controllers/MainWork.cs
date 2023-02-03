using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB15.Services.Wms;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Byd.CB15.Controllers
{
    public class MainWork : MainWorkAbstract
    {
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
                var testPreParationResult = TestPreparation();
                if (testPreParationResult.IsFailed)
                    return testPreParationResult;
                DoPauseOrStop();
                var getTrayCodeResult = GetTrayCode();
                if (getTrayCodeResult.IsFailed)
                    return getTrayCodeResult;
                DoPauseOrStop();
                var sendAxisCoordinatesResult = SendAxisCoordinates();
                if (sendAxisCoordinatesResult.IsFailed)
                    return sendAxisCoordinatesResult;
                DoPauseOrStop();
                var getBatteriesInfoResult = GetBatteriesInfo();
                if (getBatteriesInfoResult.IsFailed)
                    return getBatteriesInfoResult;
                DoPauseOrStop();
                var testBatteriesResult = TestBatteries();
                if (testBatteriesResult.IsFailed)
                    return testBatteriesResult;
                DoPauseOrStop(); 
            }
        }

        /// <summary>
        /// 测试所有电池
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult TestBatteries()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取电芯条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetBatteriesInfo()
        {
            var getTrayInfoResult = WmsService.GetTechnologyInfoByBarCode();
            if(getTrayInfoResult.IsFailed)
                return getTrayInfoResult;
            var resultEntity = getTrayInfoResult.Content;
        }
        /// <summary>
        /// 下发X1,X2轴坐标
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private OperateResult SendAxisCoordinates()
        {
            LogHelper.UiLog.Info("下发轴坐标");
            float x1 = SettingManager.CurrentOtherSetting?.X1AxisCoordinates ?? (float)0;
            float x2 = SettingManager.CurrentOtherSetting?.X2AxisCoordinates ?? (float)0;
            LogHelper.UiLog.Info($"X1轴坐标写入{x1}");
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["X1轴坐标"], x1);
            if (writeResult.IsFailed)
                return writeResult;
            LogHelper.UiLog.Info($"X2轴坐标写入{x2}");
            var writeResult1 = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["X2轴坐标"], x2);
            if (writeResult1.IsFailed)
                return writeResult;
            LogHelper.UiLog.Info($"测试标志写1");
            var writeResult2 = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)1);
            if (writeResult2.IsFailed)
                return writeResult;
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取托盘条码
        /// </summary>
        /// <returns></returns>
        private OperateResult GetTrayCode()
        {
            LogHelper.UiLog.Info("读取托盘条码");
            var readTrayCodeResult = DevicesController.Plc.ReadString(SettingManager.CurrentPlcAddressCache["上托盘条码信息"], 32);
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
        /// 测试准备
        /// </summary>
        /// <returns></returns>
        private OperateResult TestPreparation()
        {
            if (!DevicesController.Plc.IsConnected)
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
            if (!(DevicesController.DMM?.IsConnected ?? true))
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
            if (!(DevicesController.Ohm?.IsConnected ?? true))
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
            if (!(DevicesController.SwitchBoard?.IsConnected ?? true))
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if (result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接切换板");
                }
                else
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("测试标志位写入0");
            var writeResult = DevicesController.Plc.Write(SettingManager.CurrentPlcAddressCache["测试标志"], (short)0);
            if (writeResult.IsFailed)
            {
                return writeResult;
            }
            LogHelper.UiLog.Info("正在等待PLC请求测试");
            var waitResult = DevicesController.Plc.Wait(SettingManager.CurrentPlcAddressCache["请求测试"], (short)1, cancellation: this.FlowController.CancelToken);
            if (waitResult.IsFailed)
            {
                return waitResult;
            }
            LogHelper.UiLog.Info("完成准备流程");
            return OperateResult.CreateSuccessResult();
        }
    }
}
