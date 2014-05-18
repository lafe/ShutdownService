using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.TimeMonitor
{
    public class TimeMonitorFactory : ITimeMonitorFactory
    {
        public ILog Logger { get; set; }
        public ITimeProvider TimeProvider { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public TimeMonitorFactory(Configuration.Configuration configuration, ILog logger, ITimeProvider timeProvider)
        {
            Logger = logger;
            Configuration = configuration;
            TimeProvider = timeProvider;
        }

        public IMonitor CreateTimeMonitor()
        {
            return new TimeMonitor(Configuration, Logger, TimeProvider);
        }
    }
}