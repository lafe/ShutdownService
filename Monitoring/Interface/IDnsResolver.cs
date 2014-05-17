using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lafe.ShutdownService.Monitoring.Interface
{
    public interface IDnsResolver
    {
        /// <summary>
        /// Resolves the specified DNS name to an IP
        /// </summary>
        /// <param name="dnsName">DNS Name of the computer.</param>
        /// <returns>One IP that belongs to the <paramref name="dnsName"/></returns>
        string Resolve(string dnsName);
    }
}
