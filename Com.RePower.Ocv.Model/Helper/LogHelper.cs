using CLDC.Framework.Log;
using log4net;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Helper
{
    public static class LogHelper
    {
		private static ILog _uiLog = Log.getMessageFile("UiLog");

		public static ILog UiLog
		{
			get { return _uiLog; }
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
