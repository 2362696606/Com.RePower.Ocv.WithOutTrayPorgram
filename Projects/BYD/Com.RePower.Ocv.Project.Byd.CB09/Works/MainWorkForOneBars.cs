using AutoMapper;
using Com.RePower.DeviceBase.DMM;
using Com.RePower.DeviceBase.Ohm;
using Com.RePower.DeviceBase.Plc;
using Com.RePower.DeviceBase.SwitchBoard;
using Com.RePower.DeviceBase.TemperatureSensor;
using Com.RePower.Ocv.Model.Entity;
using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Serivces;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public class MainWorkForOneBars:MainWork
{

    public MainWorkForOneBars(IPlc plc, IDmm dmm, IOhm ohm, ISwitchBoard switchBoard,
        ITemperatureSensor temperatureSensor, Tray tray, IWmsService wmsService, IMesService mesService, IMapper mapper)
        : base(plc, dmm, ohm, switchBoard, temperatureSensor, tray, wmsService, mesService, mapper)
    {
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
            #region 等待请求读取托盘条码
            LogHelper.UiLog.Info("等待请求读取托盘条码");
            var waitResult = WaitTrayCodeRequest();
            if (waitResult.IsFailed) return waitResult;
            LogHelper.UiLog.Info("等待请求读取托盘条码成功");
            DoPauseOrStop();
            #endregion
            #region 读取托盘条码
            LogHelper.UiLog.Info("读取托盘条码");
            var readResult = ReadTrayCode();
            if (readResult.IsFailed) return readResult;
            LogHelper.UiLog.Info("读取托盘条码成功");
            DoPauseOrStop();
            #endregion
            #region 获取电芯条码
            LogHelper.UiLog.Info("获取电芯条码");
            var getResult = GetBatteriesInfo();
            if (getResult.IsFailed) return getResult;
            LogHelper.UiLog.Info("获取电芯条码成功");
            DoPauseOrStop();
            #endregion
            #region 下发可以开始测试
            LogHelper.UiLog.Info("下发可以开始测试");
            var writeResult = SendReadTrayCodeComplete();
            if (writeResult.IsFailed) return writeResult;
            LogHelper.UiLog.Info("下发可以开始测试成功"); 
            DoPauseOrStop();
            #endregion
            #region 等待请求测试
            LogHelper.UiLog.Info("等待请求测试");
            waitResult = WaitTestRequest();
            if (waitResult.IsFailed) return waitResult;
            LogHelper.UiLog.Info("等待请求测试成功");
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
                if (testResult.IsFailed) return testResult;
                LogHelper.UiLog.Info("测试电池成功");
                #endregion
                #region 验证Ng
                LogHelper.UiLog.Info("验证Ng");
                var verifyResult = VerifyNg();
                if (verifyResult.IsFailed) return verifyResult;
                LogHelper.UiLog.Info("验证Ng成功");
                #endregion
                #region 复测
                LogHelper.UiLog.Info("复测");
                var retestResult = ReTest();
                if (retestResult.IsFailed) return retestResult;
                LogHelper.UiLog.Info("复测成功");
                #endregion
                #region 上传测试结果
                LogHelper.UiLog.Info("上传测试结果");
                var uploadResult = UploadTestResult();
                if (uploadResult.IsFailed) return uploadResult;
                LogHelper.UiLog.Info("上传测试结果成功");
                #endregion  
                #region 测试完成
                var testCompleteResult = TestComplete();
                if (testCompleteResult.IsFailed) return testCompleteResult;
                #endregion
            }
            return OperateResult.CreateSuccessResult();
        }
    }

    protected override OperateResult WaitTrayCodeRequest()
    {
        return Plc.Wait(PlcCacheSetting["Group3"]["Read_1"].Address, (short)1,
            cancellation: FlowController.CancelToken);
    }

    protected override OperateResult ReadTrayCode()
    {
        var readResult = Plc.ReadString(PlcCacheSetting["Group3"]["Read_2"].Address,
            (ushort)PlcCacheSetting["Group3"]["Read_2"].Length);
        if(readResult.IsFailed) return readResult;
        var trayCode = readResult.Content;
        if (!string.IsNullOrEmpty(trayCode))
        {
            Tray.TrayCode = trayCode;
        }
        else
        {
            return OperateResult.CreateFailedResult("托盘条码为空");
        }
        return readResult;
    }

    protected override OperateResult SendReadTrayCodeComplete()
    {
        return Plc.Write(PlcCacheSetting["Group3"]["Send_1"].Address, (short)1);
    }

    protected override OperateResult WaitTestRequest()
    {
        return Plc.Wait(PlcCacheSetting["Group3"]["Read_1"].Address, (short)2,
            cancellation: FlowController.CancelToken);
    }
    protected override OperateResult SendRetestSignal()
    {
        return Plc.Write(PlcCacheSetting["Group3"]["Send_2"].Address, (short)2);
    }

    protected override OperateResult SendTestComplete()
    {
        LogHelper.UiLog.Info("下发测试完成");
        var writeResult = Plc.Write(PlcCacheSetting["Group3"]["Send_1"].Address, (short)2);
        if (writeResult.IsSuccess)
            LogHelper.UiLog.Info("下发测试完成信号成功");
        return writeResult;
    }

    protected override OperateResult SendOutPut()
    {
        LogHelper.UiLog.Info("下发出库");
        var writeResult = Plc.Write(PlcCacheSetting["Group3"]["Send_2"].Address, (short)1);
        if (writeResult.IsSuccess)
            LogHelper.UiLog.Info("下发出库信号成功");
        return writeResult;
    }
}