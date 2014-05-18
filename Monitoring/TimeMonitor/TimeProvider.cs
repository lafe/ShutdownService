using System;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.TimeMonitor
{
    public class TimeProvider : ITimeProvider
    {
        public ILog Logger { get; set; }

        public TimeProvider(ILog logger)
        {
            Logger = logger;
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}