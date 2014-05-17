namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface INetworkMonitor
    {
        void StartMonitoring();
        void StopMonitoring();
    }
}