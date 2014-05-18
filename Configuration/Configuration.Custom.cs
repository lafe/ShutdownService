using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lafe.ShutdownService.Configuration
{
    public partial class MonitoredRange
    {
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Type, Address);
        }
    }

    public partial class MonitoredRanges
    {
        /// <summary>
        /// The value of <see cref="NetworkTimeout"/> as <see cref="TimeSpan"/>
        /// </summary>
        /// <remarks>If no network timeout has been specified, a default value of 30s is returned</remarks>
        public TimeSpan Timeout
        {
            get
            {
                return string.IsNullOrWhiteSpace(NetworkTimeout) ?
                        new TimeSpan(0,0,30) : 
                        XmlConvert.ToTimeSpan(NetworkTimeout);    
            }
        }

    }
}
