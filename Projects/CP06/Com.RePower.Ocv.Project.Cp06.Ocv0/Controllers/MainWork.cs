using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Extensions;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.Cp06.Ocv0.Controllers
{
    public partial class MainWork : MainWorkAbstract
    {
        public MainWork(DevicesController devicesController,Tray tray)
        {
            DevicesController = devicesController;
            Tray = tray;
        }

        public DevicesController DevicesController { get; }
        public Tray Tray { get; }

        protected override OperateResult DoWork()
        {
            while(true)
            {
                var testPreParationResult = TestPreparation();
                if(testPreParationResult.IsFailed)
                    return testPreParationResult;
                DoPauseOrStop();
                var getTrayCodeResult = GetTrayCode();
                if(getTrayCodeResult.IsFailed)
                    return getTrayCodeResult;
                DoPauseOrStop();
            }
        }

        /// <summary>
        /// 测试准备
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult TestPreparation()
        {
            if(!DevicesController.Plc.IsConnected)
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
            if(!DevicesController.DMM.IsConnected)
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
            if(!DevicesController.Ohm.IsConnected)
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
            if(!DevicesController.SwitchBoard.IsConnected)
            {
                LogHelper.UiLog.Info("连接切换板");
                var result = DevicesController.SwitchBoard.Connect();
                if(result.IsSuccess)
                {
                    LogHelper.UiLog.Info("成功连接切换板");
                }
                else
                {
                    return result;
                }
            }
            LogHelper.UiLog.Info("PcToPlc.测试标志位写入0");
            var writeResult = DevicesController.Plc.Write(DevicesController.PlcAddressCache["测试标志位"], 0);
            if (writeResult.IsFailed)
            {
                return writeResult;
            }
            return OperateResult.CreateSuccessResult();
        }
        /// <summary>
        /// 获取托盘条码
        /// </summary>
        /// <returns>操作结果</returns>
        private OperateResult GetTrayCode()
        {
            LogHelper.UiLog.Info("读取托盘条码");
            var readTrayCodeResult = DevicesController.Plc.ReadString(DevicesController.PlcAddressCache["托盘条码"], 20);
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
    }
}
