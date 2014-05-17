using System;
using lafe.Logging.Interface;
using NLog;
using NLog.Config;

namespace lafe.Logging
{
    public class LoggingService : Logger, ILog
    {
        private const string LoggerName = "NLogLogger";

        public static ILog GetLoggingService()
        {
            var logger = (ILog)LogManager.GetLogger("NLogLogger", typeof(LoggingService));
            logger.Info(LogNumbers.LoadingLoggingService, "Logging service loaded");
            return logger;
        }

        public void Debug(int number, string format, params object[] args)
        {
            Debug(number, null, format, args);
        }

        public void Debug(int number, Exception exception)
        {
            Debug(number, exception, string.Empty);
        }

        public void Debug(int number, Exception exception, string format, params object[] args)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Debug, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }

        public void Error(int number, string format, params object[] args)
        {
            Error(number, null, format, args);
        }

        public void Error(int number, Exception exception)
        {
            Error(number, exception, string.Empty);
        }

        public void Error(int number, Exception exception, string format, params object[] args)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Error, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }

        public void Fatal(int number, Exception exception)
        {
            Fatal(number, exception, string.Empty);
        }

        public void Fatal(int number, string format, params object[] args)
        {
            Fatal(number, null, format, args);
        }

        public void Fatal(int number, Exception exception, string format, params object[] args)
        {
            if (!IsFatalEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Fatal, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }


        public void Info(int number, Exception exception)
        {
            Info(number, exception, string.Empty);
        }

        public void Info(int number, string format, params object[] args)
        {
            Info(number, null, format, args);
        }

        public void Info(int number, Exception exception, string format, params object[] args)
        {
            if (!IsInfoEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Info, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }

        public void Trace(int number, Exception exception)
        {
            Trace(number, exception, string.Empty);
        }
        public void Trace(int number, string format, params object[] args)
        {
            Trace(number, null, format, args);
        }

        public void Trace(int number, Exception exception, string format, params object[] args)
        {
            if (!IsTraceEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Trace, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }

        public void Warn(int number, string format, params object[] args)
        {
            Warn(number, null, format, args);
        }

        public void Warn(int number, Exception exception)
        {
            Warn(number, exception, string.Empty);
        }

        public void Warn(int number, Exception exception, string format, params object[] args)
        {
            if (!IsWarnEnabled)
            {
                return;
            }

            var logEvent = GetLogEvent(number, LoggerName, LogLevel.Warn, exception, format, args);
            Log(typeof(LoggingService), logEvent);
        }

        private LogEventInfo GetLogEvent(int number, string loggerName, LogLevel level, Exception exception, string format, object[] args)
        {
            var assemblyProp = string.Empty;
            var classProp = string.Empty;
            var methodProp = string.Empty;
            var messageProp = string.Empty;
            var innerMessageProp = string.Empty;

            var logEvent = new LogEventInfo(level, loggerName, string.Format(format, args));

            if (exception != null)
            {
                assemblyProp = exception.Source;
                classProp = exception.TargetSite.DeclaringType.FullName;
                methodProp = exception.TargetSite.Name;
                messageProp = exception.Message;

                if (exception.InnerException != null)
                {
                    innerMessageProp = exception.InnerException.Message;
                }
            }

            logEvent.Properties["number"] = number;
            logEvent.Properties["error-source"] = assemblyProp;
            logEvent.Properties["error-class"] = classProp;
            logEvent.Properties["error-method"] = methodProp;
            logEvent.Properties["error-message"] = messageProp;
            logEvent.Properties["inner-error-message"] = innerMessageProp;

            return logEvent;
        }
    }
}
