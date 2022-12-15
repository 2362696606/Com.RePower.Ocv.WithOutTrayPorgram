using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.Helper
{
    internal class EventHandlerAppender : AppenderSkeleton
    {
        public event EventHandler<LoggingEvent>? LogginEventHandler;
        protected override void Append(LoggingEvent loggingEvent)
        {
            if (LogginEventHandler != null)
            {
                RenderLoggingEvent(loggingEvent);
                LogginEventHandler.Invoke(this, loggingEvent);
            }
        }
    }
}
