using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring
{
    public class MonitorFactory : IMonitorFactory
    {
        public ILog Logger { get; set; }

        public Configuration.Configuration Configuration { get; set; }
        public INetworkMonitorFactory NetworkMonitorFactory { get; set; }
        public ITimeMonitorFactory TimeMonitorFactory { get; set; }

        public MonitorFactory(ILog logger, Configuration.Configuration configuration, INetworkMonitorFactory networkMonitorFactory, ITimeMonitorFactory timeMonitorFactory)
        {
            Logger = logger;
            Configuration = configuration;
            NetworkMonitorFactory = networkMonitorFactory;
            TimeMonitorFactory = timeMonitorFactory;
        }

        public IEnumerable<IMonitor> CreateMonitors()
        {
            //Time monitor uses little resources: Use it first to skip other steps, if necessary
            Logger.Trace(LogNumbers.ReturningTimeMonitor, "Returning time monitor");
            yield return TimeMonitorFactory.CreateTimeMonitor();

            Logger.Trace(LogNumbers.ReturningNetworkMonitor, "Returning network monitor");
            yield return NetworkMonitorFactory.CreateNetworkMonitor();
        } 
    }
}
