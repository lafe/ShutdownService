using System;

namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface ITimeProvider
    {
        DateTime GetCurrentTime();
    }
}