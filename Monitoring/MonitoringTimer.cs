using System;
using System.Linq;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ServiceBase.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring
{
    public class MonitoringTimer : IMonitoringTimer
    {
        public ILog Logger { get; set; }
        public Configuration.Configuration Configuration { get; set; }
        public IServiceTimerFactory ServiceTimerFactory { get; set; }
        public IMonitorFactory MonitorFactory { get; set; }
        public IAction Action { get; set; }



        protected IServiceTimer Timer { get; set; }

        public MonitoringTimer(Configuration.Configuration configuration, ILog logger, IServiceTimerFactory serviceTimerFactory, IMonitorFactory monitorFactory, IAction action)
        {
            Logger = logger;
            Configuration = configuration;
            ServiceTimerFactory = serviceTimerFactory;
            MonitorFactory = monitorFactory;
            Action = action;

#if DEBUG
            var period = new TimeSpan(0, 0, 1);
#else
            var period = Configuration.Timer.Interval;
#endif
            Timer = ServiceTimerFactory.CreateTimer(period, true, DoMonitoring);
        }


        public void StartMonitoring()
        {
            Logger.Trace(LogNumbers.StartingMonitor, "Starting Network Monitor");
            Timer.StartTimer(false);
        }

        public void StopMonitoring()
        {
            Logger.Trace(LogNumbers.StoppingMonitor, "Starting Network Monitor");
            Timer.StopTimer();
        }

        private void DoMonitoring(object state)
        {
            try
            {
                var monitors = MonitorFactory.CreateMonitors();
                foreach (var monitor in monitors)
                {
                    Logger.Trace(LogNumbers.RunningMonitor, string.Format("Running monitor \"{0}\"", monitor.Name));
                    var canShutDown = monitor.CanShutdown();

                    Logger.Trace(LogNumbers.MonitorResult, string.Format("Monitor \"{0}\" returned: {1}", monitor.Name, canShutDown ? "Shutdown" : "No shutdown"));
                    if (!canShutDown)
                    {
                        Logger.Trace(LogNumbers.SkippingMonitors, string.Format("Monitor \"{0}\" prevents shutdown. Skipping further monitors.", monitor.Name));
                        return;
                    }
                }
                
                //No monitor has prevented shutdown -> we can perform the action
                Action.PerformAction();
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.MonitoringException, ex, string.Format("While performing the network monitoring activities, an error occured: {0}", ex));
            }
        }
    }
}