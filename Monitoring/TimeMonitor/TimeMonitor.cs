using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;
using lafe.ShutdownService.Monitoring.NetworkMonitoring;

namespace lafe.ShutdownService.Monitoring.TimeMonitor
{
   public class TimeMonitor : IMonitor
    {
       public string Name
       {
           get
           {
               return "Time Monitor";
           }
       }

       public ILog Logger { get; set; }
       public ITimeProvider TimeProvider { get; set; }
       public Configuration.Configuration Configuration { get; set; }

       public TimeMonitor(Configuration.Configuration configuration, ILog logger, ITimeProvider timeProvider)
        {
            Logger = logger;
            Configuration = configuration;
            TimeProvider = timeProvider;
        }

       public bool CanShutdown()
       {
           return true;
       }
    }
}
