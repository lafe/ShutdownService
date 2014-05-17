using System;

namespace lafe.Logging.Interface
{
    public interface ILog
    {
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }

        void Debug(int number, Exception exception);
        void Debug(int number, string format, params object[] args);
        void Debug(int number, Exception exception, string format, params object[] args);
        void Error(int number, Exception exception);
        void Error(int number, string format, params object[] args);
        void Error(int number, Exception exception, string format, params object[] args);
        void Fatal(int number, Exception exception);
        void Fatal(int number, string format, params object[] args);
        void Fatal(int number, Exception exception, string format, params object[] args);
        void Info(int number, Exception exception);
        void Info(int number, string format, params object[] args);
        void Info(int number, Exception exception, string format, params object[] args);
        void Trace(int number, Exception exception);
        void Trace(int number, string format, params object[] args);
        void Trace(int number, Exception exception, string format, params object[] args);
        void Warn(int number, Exception exception);
        void Warn(int number, string format, params object[] args);
        void Warn(int number, Exception exception, string format, params object[] args);
    }
}