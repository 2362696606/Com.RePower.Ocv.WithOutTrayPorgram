using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    /// <summary>
    /// 连接设备
    /// </summary>
    /// <returns>连接结果</returns>
    protected virtual OperateResult ConnectDevices()
    {
        if (!Plc.IsConnected)
        {
            LogHelper.UiLog.Info("连接plc");
            var connectResult = Plc.Connect();
            if (connectResult.IsFailed)
                return connectResult;
        }
        if (!Dmm.IsConnected)
        {
            LogHelper.UiLog.Info("连接万用表");
            var connectResult = Dmm.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        if (!Ohm.IsConnected)
        {
            LogHelper.UiLog.Info("连接内阻仪");
            var connectResult = Dmm.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        if (!SwitchBoard.IsConnected)
        {
            LogHelper.UiLog.Info("连接切换版");
            var connectResult = SwitchBoard.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        return OperateResult.CreateSuccessResult();
    }
}