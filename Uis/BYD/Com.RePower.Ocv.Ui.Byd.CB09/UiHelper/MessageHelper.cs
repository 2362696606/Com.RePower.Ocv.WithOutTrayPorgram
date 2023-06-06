using System;
using System.Collections.Generic;
using Com.RePower.Ocv.Project.Byd.CB09.Messages;

namespace Com.RePower.Ocv.Ui.Byd.CB09.UiHelper;

public static class MessageHelper
{
    public static void DoMethod(object recipient, DoMethodMessage message)
    {
        var objType = recipient.GetType();
        var methodInfo = objType.GetMethod(message.MethodName, new Type[] { typeof(Dictionary<string, object>) });
        object[] parameters = new object[] { message.Parameters };
        if (methodInfo != null)
        {
            methodInfo.Invoke(recipient, parameters);
        }
    }
}