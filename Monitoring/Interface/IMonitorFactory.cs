using System.Collections.Generic;

namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface IMonitorFactory
    {
        IEnumerable<IMonitor> CreateMonitors();
    }
}