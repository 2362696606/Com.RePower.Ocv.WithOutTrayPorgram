﻿using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult SendCoordinates()
    {
        var otherSetting = new OtherSetting();
        LogHelper.UiLog.Info("下发左压合X轴坐标");
        var writeResult = _plc.Write(_plcCacheSetting["Group2"]["左压合X轴坐标"].Address, (float)otherSetting.LeftX);
        if (writeResult.IsFailed)
            return writeResult;
        LogHelper.UiLog.Info("下发左压合X轴坐标成功");
        LogHelper.UiLog.Info("下发左上下Z轴坐标");
        writeResult = _plc.Write(_plcCacheSetting["Group2"]["左上下Z轴坐标"].Address, (float)otherSetting.LeftZ);
        if(writeResult.IsFailed)
            return writeResult;
        LogHelper.UiLog.Info("下发左上下Z轴坐标成功");
        LogHelper.UiLog.Info("下发右压合X轴坐标");
        writeResult = _plc.Write(_plcCacheSetting["Group2"]["右压合X轴坐标"].Address, (float)otherSetting.RightX);
        if( writeResult.IsFailed)
            return writeResult;
        LogHelper.UiLog.Info("下发右压合X轴坐标成功");
        LogHelper.UiLog.Info("下发右上下Z轴坐标");
        writeResult = _plc.Write(_plcCacheSetting["Group2"]["右上下Z轴坐标"].Address, (float)otherSetting.RightZ);
        if(writeResult.IsFailed)
            return writeResult;
        LogHelper.UiLog.Info("下发右上下Z轴坐标成功");
        return OperateResult.CreateSuccessResult();
    }
}