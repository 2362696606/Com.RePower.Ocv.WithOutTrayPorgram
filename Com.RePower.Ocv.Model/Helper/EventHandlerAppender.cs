using log4net.Appender;
using log4net.Core;

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