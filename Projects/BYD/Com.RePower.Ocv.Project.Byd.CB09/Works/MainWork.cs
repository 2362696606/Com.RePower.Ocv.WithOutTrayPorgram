using System.Configuration;
using System.Reflection;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.Ocv.Project.ProjectBase.Controllers;
using Com.RePower.WpfBase;
using Newtonsoft.Json;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.Functions;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works
{
    public partial class MainWork : MainWorkAbstract
    {
        private readonly IPlc _plc;
        private readonly IDmm _dmm;
        private readonly IOhm _ohm;
        private readonly ISwitchBoard _switchBoard;
        private readonly Tray _tray;
        private readonly IWmsService _wmsService;
        private readonly PlcCacheSetting _plcCacheSetting;

        public MainWork(IPlc plc,IDmm dmm,IOhm ohm,ISwitchBoard switchBoard,Tray tray,IWmsService wmsService)
        {
            _plc = plc;
            _dmm = dmm;
            _ohm = ohm;
            _switchBoard = switchBoard;
            _tray = tray;
            _wmsService = wmsService;
            _plcCacheSetting = PlcCacheSetting.Default;
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
                #region 测试电池
                LogHelper.UiLog.Info("测试电池");
                var testResult = TestBatteries();
                if (testResult.IsFailed)
                    return testResult;
                LogHelper.UiLog.Info("测试电池成功"); 
                DoPauseOrStop();
                #endregion
                #region 验证Ng信息
                LogHelper.UiLog.Info("验证Ng信心");
                var verifyResult = VerifyNg();
                if (verifyResult.IsFailed)
                    return verifyResult;
                LogHelper.UiLog.Info("验证Ng信息成功"); 
                #endregion
                return OperateResult.CreateSuccessResult();
            }


            //return OperateResult.CreateSuccessResult();
        }
    }
}