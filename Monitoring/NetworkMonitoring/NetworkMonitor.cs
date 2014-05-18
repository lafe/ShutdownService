using System;
using System.Linq;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.NetworkMonitoring
{
    public class NetworkMonitor : IMonitor
    {
        public ILog Logger { get; set; }
        public IOnlineCheckFactory OnlineCheckFactory { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public NetworkMonitor(Configuration.Configuration configuration, ILog logger, IOnlineCheckFactory onlineCheckFactory)
        {
            Logger = logger;
            OnlineCheckFactory = onlineCheckFactory;
            Configuration = configuration;
        }

        public string Name { get { return "Network Monitor"; } }

        public bool CanShutdown()
        {
            try
            {
                var defaultTimeOut = Configuration.MonitoredRanges.Timeout;
                var onlineCheckerCreatorTasks = Configuration.MonitoredRanges.MonitoredRange.Select(range => OnlineCheckFactory.CreateCheck(range.Type, range.Address, defaultTimeOut)).ToList();

                if (onlineCheckerCreatorTasks.Count == 0)
                {
                    Logger.Warn(LogNumbers.NoCheckInstancesFound, "No monitored ranges found. Skipping check.");
                    return false;
                }

                var results = new System.Collections.Concurrent.ConcurrentBag<Tuple<string, bool>>();
                Parallel.ForEach(onlineCheckerCreatorTasks, onlineCheck =>
                {
                    try
                    {
                        if (onlineCheck == null)
                        {
                            return;
                        }

                        var isOnline = onlineCheck.IsOnline();
                        results.Add(new Tuple<string, bool>(onlineCheck.Address, isOnline));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(LogNumbers.ErrorInOnlineCheck, ex, string.Format("An error occured while trying to determine the online status of the computer with address \"{0}\": {1}", onlineCheck.Address, ex));
                        results.Add(new Tuple<string, bool>(onlineCheck.Address, true));
                    }
                });

                if (results.Count == 0 && onlineCheckerCreatorTasks.Any())
                {
                    //Sometimes zero results are reported...

                    Logger.Warn(LogNumbers.ResultEmpty, "No results have been retrieved from the computers. Assuming that at least one computer is online and aborting check.");
                    return false;
                }

                var anyComputerOnline = results.Any(item => item.Item2);

                if (anyComputerOnline)
                {
                    var stillOnline = string.Join(", ", results.Where(item => item.Item2).Select(item => item.Item1));

                    Logger.Info(LogNumbers.ComputerStillOnline, string.Format("Cannot shut down. The following addresses are online: {0}", stillOnline));
                    return false;
                }

                Logger.Info(LogNumbers.NoComputerOnline, "All computers are offline or cannot be reached in the specified timeout. Go ahead from Network monitor!");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.MonitoringException, ex, string.Format("While performing the network monitoring activities, an error occured: {0}", ex));
                return false;
            }
        }
    }
}
