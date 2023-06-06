using System;
using System.Collections.Generic;
using Com.RePower.Ocv.Project.CZD01.BaseProject.Messages;

namespace Com.RePower.Ocv.Ui.CZD01.BaseUi.UiHelper;

public static class MessageHelper
{
    public static void DoMethod(object recipient, DoMethodMessage message)
    {
        var objType = recipient.GetType();
        var methodInfo = objType.GetMethod(message.MethodName, new Type[] { typeof(Dictionary<string, object>) });
        object[] parameters = { message.Parameters };
        if (methodInfo != null)
        {
            methodInfo.Invoke(recipient, parameters);
        }
    }
}