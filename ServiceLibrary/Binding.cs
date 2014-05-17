using lafe.Logging;
using lafe.Logging.Interface;
using lafe.ServiceBase;
using lafe.ServiceBase.Interface;
using lafe.ShutdownService.Configuration;
using lafe.ShutdownService.Configuration.Interface;
using lafe.ShutdownService.Monitoring;
using lafe.ShutdownService.Monitoring.Interface;
using lafe.ShutdownService.Monitoring.NetworkMonitoring;
using lafe.ShutdownService.Monitoring.Resolver;
using Ninject;
using Ninject.Modules;

namespace lafe.ShutdownService.ServiceLibrary
{
    internal class Binding : NinjectModule
    {
        public override void Load()
        {
            var logger = LoggingService.GetLoggingService();
            Bind<ILog>().ToConstant(logger);
            Bind<IConfigurationLoader>().To<ConfigurationLoader>();

            var config = Kernel.Get<IConfigurationLoader>().LoadConfig();
            Bind<Configuration.Configuration>().ToConstant(config);

            Bind<IServiceTimerFactory>().To<ServiceTimerFactory>();

            Bind<IDnsResolver>().To<DnsResolver>();
            Bind<IOnlineCheckFactory>().To<NetworkPingFactory>();
            Bind<IMonitorFactory>().To<MonitorFactory>();
            Bind<INetworkMonitorFactory>().To<NetworkMonitorFactory>();
            Bind<IMonitoringTimer>().To<MonitoringTimer>().InSingletonScope();
            Bind<IAction>().To<ShutdownAction>();
        }
    }
}
