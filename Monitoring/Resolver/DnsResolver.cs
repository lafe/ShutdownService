using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.Resolver
{
    public class DnsResolver : IDnsResolver
    {
        public ILog Logger { get; private set; }

        public DnsResolver(ILog logger)
        {
            Logger = logger;
        }

        public string Resolve(string dnsName)
        {
            Logger.Trace(LogNumbers.ResolvingDnsName, string.Format("Resolving DNS name \"{0}\"", dnsName));
            try
            {

                var hostAddress = Dns.GetHostAddresses(dnsName);
                if (hostAddress == null || hostAddress.Length == 0)
                {
                    return string.Empty;
                }

                var ip4Address = hostAddress.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
                var result = ip4Address != null
                    ? ip4Address.ToString()
                    : hostAddress[0].ToString();
                Logger.Trace(LogNumbers.DnsNameResolved, string.Format("DNS name {0} has been resolved to IP {1}", dnsName, result));
                return result;
            }
            catch (SocketException ex)
            {
                Logger.Trace(LogNumbers.DnsResolveSocketException, ex, string.Format("Socket Exception while trying to resolve the DNS name \"{0}\": {1}", dnsName, ex));
                return ConstValues.NotResolvable;
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.DnsResolveException, ex, string.Format("While trying to resolve the DNS name \"{0}\", an error occured: {1}", dnsName, ex));
                return string.Empty;
            }
        }
    }
}
