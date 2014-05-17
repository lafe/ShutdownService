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

        public MonitorFactory(ILog logger, Configuration.Configuration configuration, INetworkMonitorFactory networkMonitorFactory)
        {
            Logger = logger;
            Configuration = configuration;
            NetworkMonitorFactory = networkMonitorFactory;
        }

        public IEnumerable<IMonitor> CreateMonitors()
        {
            yield return NetworkMonitorFactory.CreateNetworkMonitor();
        } 
    }
}
