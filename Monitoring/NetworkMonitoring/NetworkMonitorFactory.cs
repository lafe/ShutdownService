using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.NetworkMonitoring
{
    public class NetworkMonitorFactory : INetworkMonitorFactory
    {
        public ILog Logger { get; set; }
        public IOnlineCheckFactory OnlineCheckFactory { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public NetworkMonitorFactory(Configuration.Configuration configuration, ILog logger, IOnlineCheckFactory onlineCheckFactory)
        {
            Logger = logger;
            OnlineCheckFactory = onlineCheckFactory;
            Configuration = configuration;
        }

        public IMonitor CreateNetworkMonitor()
        {
            return new NetworkMonitor(Configuration, Logger, OnlineCheckFactory);
        }
    }
}