using System;
using System.Threading.Tasks;
using lafe.ShutdownService.Configuration;

namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface IOnlineCheckFactory
    {
        IOnlineCheck CreateIpCheck(string ipAddress, TimeSpan timeout);
        IOnlineCheck CreateDnsCheck(string dnsName, TimeSpan timeout);
        IOnlineCheck CreateCheck(RangeType type, string address, TimeSpan timeout);
    }
}