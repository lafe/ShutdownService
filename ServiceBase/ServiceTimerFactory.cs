using System;
using lafe.Logging.Interface;
using lafe.ServiceBase.Interface;

namespace lafe.ServiceBase
{
    public class ServiceTimerFactory : IServiceTimerFactory
    {
        public ILog Logger { get; private set; }

        public ServiceTimerFactory(ILog logger)
        {
            Logger = logger;
        }

        public IServiceTimer CreateTimer(Action<object> callback)
        {
            Logger.Trace(LogNumbers.CreatingTimer, "Creating Timer");
            return new ServiceTimer(Logger, callback);
        }

        public IServiceTimer CreateTimer(TimeSpan period, Action<object> callback)
        {
            Logger.Trace(LogNumbers.CreatingTimer, "Creating Timer");
            return new ServiceTimer(period, false, Logger, callback);
        }

        public IServiceTimer CreateTimer(TimeSpan period, bool autoPause, Action<object> callback)
        {
            Logger.Trace(LogNumbers.CreatingTimer, "Creating Timer");
            return new ServiceTimer(period, autoPause, Logger, callback);
        }
    }
}