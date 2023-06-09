using Com.RePower.Ocv.Model.Helper;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Settings;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works;

public partial class MainWork
{
    protected virtual OperateResult VerifyNgCount()
    {
        var max = SettingManager.CurrentOtherSetting?.MaxErrorNg ?? 4;
        var count = Tray.NgInfos.Count(x => x.IsNg);
        if (count > max)
        {
            var sendResult = SendWarring(1);
            if (sendResult.IsFailed)
                LogHelper.UiLog.Warn(sendResult.Message);
            ManualResetEvent flag = new ManualResetEvent(false);
            Dictionary<string, object> parameters = new Dictionary<string, object>()
                { { "warringInfo", $"当前托盘ng超过{max}个" }, { "flag", flag } };
            DoMethodMessage message = new DoMethodMessage()
            {
                MethodName = "DoWarring",
                Parameters = parameters
            };
            WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message, "DoMainViewMethod");
            flag.WaitOne();
            sendResult = CleanWarring();
            if (sendResult.IsFailed)
                LogHelper.UiLog.Warn(sendResult.Message);
        }
        return OperateResult.CreateSuccessResult();
    }
}