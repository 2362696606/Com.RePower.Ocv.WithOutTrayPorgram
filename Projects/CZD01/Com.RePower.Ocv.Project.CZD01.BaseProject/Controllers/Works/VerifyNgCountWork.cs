using Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;
using Com.RePower.WpfBase;
using CommunityToolkit.Mvvm.Messaging;

namespace Com.RePower.Ocv.Project.CZD01.BaseProject.Controllers.Works;

public partial class MainWork
{
    protected virtual OperateResult VerifyNgCount()
    {
        var count = Tray.NgInfos.Count(x => x.IsNg);
        if (count > 4)
        {
            ManualResetEvent flag = new ManualResetEvent(false);
            Dictionary<string, object> parameters = new Dictionary<string, object>()
                { { "warringInfo", "当前托盘ng超过4个" }, { "flag", flag } };
            DoMethodMessage message = new DoMethodMessage()
            {
                MethodName = "DoWarring",
                Parameters = parameters
            };
            WeakReferenceMessenger.Default.Send<DoMethodMessage, string>(message, "DoMainViewMethod");
            flag.WaitOne();
        }
        return OperateResult.CreateSuccessResult();
    }
}