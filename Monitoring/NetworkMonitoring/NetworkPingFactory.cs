using System;
using lafe.Logging.Interface;
using lafe.ShutdownService.Configuration;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.NetworkMonitoring
{
    public class NetworkPingFactory : IOnlineCheckFactory
    {
        public ILog Logger { get; private set; }
        public IDnsResolver DnsResolver { get; private set; }
        
        public NetworkPingFactory(ILog log, IDnsResolver dnsResolver)
        {
            Logger = log;
            DnsResolver = dnsResolver;
        }

        public IOnlineCheck CreateIpCheck(string ipAddress, TimeSpan timeout)
        {
            Logger.Trace(LogNumbers.CreateIpCheck, string.Format("Creating IP Check for address {0}", ipAddress));
            return new NetworkPing(ipAddress, timeout, Logger);
        }

        public IOnlineCheck CreateDnsCheck(string dnsName, TimeSpan timeout)
        {
            Logger.Trace(LogNumbers.CreateDnsCheck, string.Format("Creating DNS Check for name \"{0}\"", dnsName));
            var ip = DnsResolver.Resolve(dnsName);
            if (string.IsNullOrWhiteSpace(ip))
            {
                Logger.Warn(LogNumbers.IpNull, string.Format("The resolved IP for the DNS name \"{0}\" was empty. Skipping creation of network monitor.", dnsName));
                return null;
            }
            return new NetworkPing(ip, timeout, Logger);
        }
        
        public IOnlineCheck CreateCheck(RangeType type, string address, TimeSpan timeout)
        {
            switch (type)
            {
               
                case RangeType.Ip:
                    return  CreateIpCheck(address, timeout);
                case RangeType.Dns:
                    return CreateDnsCheck(address, timeout);
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }
    }
}