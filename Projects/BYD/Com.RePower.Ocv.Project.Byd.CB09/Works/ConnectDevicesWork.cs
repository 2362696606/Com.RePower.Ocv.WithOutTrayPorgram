using Com.RePower.Device.Ohm.Impl.Hioki_BT3562;
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
            var connectResult = Ohm.Connect();
            if(connectResult.IsFailed)
                return connectResult;
            if (Ohm is HiokiBt3562Impl tempOhm)
            {
                LogHelper.UiLog.Info("正在初始化内阻仪");
                var setResult = tempOhm.SetRang();
                if (setResult.IsFailed) return setResult;
                Thread.Sleep(200);
                setResult = tempOhm.SetInitiateContinuous();
                if (setResult.IsFailed) return setResult;
                LogHelper.UiLog.Info("初始化内阻仪成功");
            }
        }
        if (!SwitchBoard.IsConnected)
        {
            LogHelper.UiLog.Info("连接切换版");
            var connectResult = SwitchBoard.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        if (!TemperatureSensor.IsConnected)
        {
            LogHelper.UiLog.Info("连接温度传感器");
            var connectResult = TemperatureSensor.Connect();
            if(connectResult.IsFailed)
                return connectResult;
        }
        return OperateResult.CreateSuccessResult();
    }
}