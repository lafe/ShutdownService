using System;
using System.Net;
using System.Net.NetworkInformation;
using lafe.Logging.Interface;
using lafe.ShutdownService.Monitoring.Interface;

namespace lafe.ShutdownService.Monitoring.NetworkMonitoring
{
    /// <summary>
    /// Checks if a computer is online using a Ping command
    /// </summary>
    public class NetworkPing : IOnlineCheck
    {
        public ILog Logger { get; private set; }

        public string Ip { get; private set; }

        public TimeSpan Timeout { get; private set; }

        public NetworkPing(string ip, TimeSpan timeout, ILog logger)
        {
            Logger = logger;
            Ip = ip;
            Logger.Trace(LogNumbers.InitNetworkPing, "Initialized Network Ping class");
            Timeout = timeout;
        }

        public string Address { get { return Ip; }}

        public bool IsOnline()
        {
            var timeout = Convert.ToInt32(Timeout.TotalMilliseconds);
            if (timeout <= 0)
            {
                Logger.Warn(LogNumbers.TimeoutZero, "Timeout was equal or less than zero milliseconds. Timeout has been reset to 1000ms.");
                timeout = 1000;
            }

            Logger.Trace(LogNumbers.CheckingOnlineState,string.Format("Checking online state of computer with IP {0}", Ip));
            var address = GetAddress();

            if (address == null)
            {
                Logger.Warn(LogNumbers.NoIpAddress, string.Format("The IP address \"{0}\" could not be converted. Assuming that the computer is online.", Ip));
                return true;
            }

            var ping = new Ping();

            Logger.Trace(LogNumbers.SendingPing, string.Format("Sending ping to address {0} with a timeout of {1}ms", address.ToString(), timeout));
            var result = ping.Send(address, timeout);

            if (result == null)
            {
                Logger.Trace(LogNumbers.PingResultNull, "Result of ping is null. Assuming that the computer is online.");
                return true;
            }

            Logger.Trace(LogNumbers.PingComplete, string.Format("Result of ping to address {0}: {1}", address, result.Status));

            var isOnline = result.Status == IPStatus.Success;

            if (isOnline)
            {
                Logger.Trace(LogNumbers.RoundTripTime, string.Format("Round trip time of ping to address {0}: {1}ms", address, result.RoundtripTime));
            }

            return isOnline;
        }

        protected virtual IPAddress GetAddress()
        {
            try
            {

                Logger.Trace(LogNumbers.ConvertingIp, "Converting IP address");
                var address = IPAddress.Parse(Ip);
                Logger.Trace(LogNumbers.ConvertedIp, string.Format("IP address converted: {0}", address.ToString()));
                return address;
            }
            catch (FormatException ex)
            {
                Logger.Error(LogNumbers.InvalidIpAddress, ex, string.Format("The given IP addres \"{0}\" could not be parsed: {1}", Ip, ex));
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.IpAddressParsingException, ex, string.Format("While parsing the given IP addres \"{0}\" an error occured: {1}", Ip, ex));
                throw;
            }
        }
    }
}