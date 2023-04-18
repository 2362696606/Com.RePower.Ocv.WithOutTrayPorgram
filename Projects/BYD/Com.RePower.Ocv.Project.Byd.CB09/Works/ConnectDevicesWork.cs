using Com.RePower.Ocv.Model.Helper;
using Com.RePower.WpfBase;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{
    protected virtual OperateResult ConnectDevices()
    {
        if (!_plc.IsConnected)
        {
            LogHelper.UiLog.Info("连接plc");
            var connectResult = _plc.Connect();
            if (connectResult.IsFailed)
                return connectResult;
        }
        if (!_dmm.IsConnected)
        {
            LogHelper.UiLog.Info("连接万用表");
            var connectResult = _dmm.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        if (!_ohm.IsConnected)
        {
            LogHelper.UiLog.Info("连接内阻仪");
            var connectResult = _dmm.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        if (!_switchBoard.IsConnected)
        {
            LogHelper.UiLog.Info("连接切换版");
            var connectResult = _switchBoard.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        return OperateResult.CreateSuccessResult();
    }
}