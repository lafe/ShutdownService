namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface INetworkMonitorFactory
    {
        IMonitor CreateNetworkMonitor();
    }
}