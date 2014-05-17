namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface IMonitor
    {
        string Name { get; }

        /// <summary>
        /// Checks if the monitor allows shutdown
        /// </summary>
        /// <returns><c>true</c> if the monitor gives the go ahead to shutdown; <c>false</c> if a blocking condition has been found</returns>
        bool CanShutdown();
    }
}