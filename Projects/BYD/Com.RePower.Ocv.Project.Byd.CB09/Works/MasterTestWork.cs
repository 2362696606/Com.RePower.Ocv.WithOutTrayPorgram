using System.Diagnostics.Eventing.Reader;
using Com.RePower.Device.Plc.Impl;
using Com.RePower.Ocv.Project.Byd.CB09.Messages;
using Com.RePower.Ocv.Project.Byd.CB09.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Com.RePower.Ocv.Project.Byd.CB09.Works;

public partial class MainWork
{

    public virtual void DoMasterTest()
    {
        var calibrationSetting = CalibrationSetting.Default;
        var connectedResult = ConnectDevices();
        if (connectedResult.IsFailed)
        {
            return;
        }
        foreach (var item in calibrationSetting.CalibrationItems)
        {
            CloseAllChannel();
            int channel = item.Channel;
            OpenChannelByBattery(channel);
            var readResult = Ohm.ReadRes();
            if (readResult.IsFailed)
            {
                item.AutoCalibrationValue = 0;
                CalibrationChangedMessage param = new CalibrationChangedMessage()
                {
                    CalibrationItem = item,
                    ReadResValue = 0,
                    SubValue = 0
                };
                DoMethodMessage message = new DoMethodMessage()
                {
                    MethodName = "CalibrationChange",
                    Parameters = new Dictionary<string, object>(),
                };
                message.Parameters.Add("ChangeItem", param);
                WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message,
                    "CalibrationChangeMessage");
            }
            else
            {
                var readValue = readResult.Content;
                double subValue = (Com.RePower.Ocv.Model.Extensions.DoubleExtensions.Sub(item.StandRes, readValue) ??
                                   throw new Exception("计算异常"));
                item.AutoCalibrationValue = subValue;
                calibrationSetting.SaveChanged();
                CalibrationChangedMessage param = new CalibrationChangedMessage()
                {
                    CalibrationItem = item,
                    ReadResValue = readValue,
                    SubValue = subValue,
                };
                DoMethodMessage message = new DoMethodMessage()
                {
                    MethodName = "CalibrationChange",
                    Parameters = new Dictionary<string, object>(),
                };
                message.Parameters.Add("ChangeItem", param);
                WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message,
                    "CalibrationChangeMessage");
            }
        }
        
    }
    public virtual bool CanDoMasterTest()
    {
        if (this.WorkStatus == 0 && TestOption.Default.IsTestRes)
        {
            return true;
        }
        return false;
    }
}