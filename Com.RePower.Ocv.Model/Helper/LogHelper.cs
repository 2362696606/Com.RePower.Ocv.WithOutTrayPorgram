using CLDC.Framework.Log;
using log4net;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Com.RePower.Ocv.Model.Helper
{
    public static class LogHelper
    {
        public static ILog UiLog
        {
            get { return Log.getMessageFile("UiLog"); }
        }

        public static ILog WmsServiceLog
        {
            get { return Log.getMessageFile("WmsLog"); }
        }

        public static ILog MesServiceLog
        {
            get { return Log.getMessageFile("MesLog"); }
        }

        /// <summary>
        /// 流程错误详情日志
        /// </summary>
        public static ILog PlcLog
        {
            get { return Log.getMessageFile("PlcLog"); ; }
        }

        public static ILog WorkErrorDetailLog
        {
            get { return Log.getMessageFile("WorkErrorDetail"); ; }
        }

        public static void RegisterUiLogEvent(Action<object?, LoggingEvent> action)
        {
            var log = Log.getMessageFile("UiLog");
            LogImpl logImpl = (LogImpl)log;
            var logger = (Logger)logImpl.Logger;
            EventHandlerAppender? eventHandlerAppender = null;
            foreach (var item in logger.Appenders)
            {
                if (item.GetType() == typeof(EventHandlerAppender))
                {
                    eventHandlerAppender = (EventHandlerAppender)item;
                }
            }
            if (eventHandlerAppender == null)
            {
                EventHandlerAppender newAppender = new EventHandlerAppender();
                PatternLayout patternLayout = new PatternLayout("%n-----------%d -----------%n%m%n");
                patternLayout.ActivateOptions();
                newAppender.Layout = patternLayout;
                newAppender.ActivateOptions();
                logger.AddAppender(newAppender);
                eventHandlerAppender = newAppender;
            }
            eventHandlerAppender.LogginEventHandler += new EventHandler<LoggingEvent>(action);
        }
    }
}