using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lafe.ShutdownService.Configuration
{
    public partial class TimerConfiguration
    {
        /// <summary>
        /// Refresh interval of the timer
        /// </summary>
        public TimeSpan Interval
        {
            get
            {
                return string.IsNullOrWhiteSpace(CheckInterval) ?
                        new TimeSpan(0, 15, 0) :
                        XmlConvert.ToTimeSpan(CheckInterval);
            }
        }
    }

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
                        new TimeSpan(0, 0, 30) :
                        XmlConvert.ToTimeSpan(NetworkTimeout);
            }
        }
    }

    public partial class MonitoredTime
    {
        public override string ToString()
        {
            var result = string.Format("{0}: {1} - {2}", Weekdays, StartTime.ToShortTimeString(), EndTime.ToShortTimeString());
            return result;
        }
    }

    public partial class Weekdays
    {
        public bool IsAll { get { return All != null; } }
        public bool IsMonday { get { return Monday != null; } }
        public bool IsTuesday { get { return Tuesday != null; } }
        public bool IsWednesday { get { return Wednesday != null; } }
        public bool IsThursday { get { return Thursday != null; } }
        public bool IsFriday { get { return Friday != null; } }
        public bool IsSaturday { get { return Saturday != null; } }
        public bool IsSunday { get { return Sunday != null; } }

        public bool IsActiveOn(DayOfWeek dayOfWeek)
        {
            return IsAll ||
                   (dayOfWeek == DayOfWeek.Monday && IsMonday) ||
                   (dayOfWeek == DayOfWeek.Tuesday && IsTuesday) ||
                   (dayOfWeek == DayOfWeek.Wednesday && IsWednesday) ||
                   (dayOfWeek == DayOfWeek.Thursday && IsThursday) ||
                   (dayOfWeek == DayOfWeek.Friday && IsFriday) ||
                   (dayOfWeek == DayOfWeek.Saturday && IsSaturday) ||
                   (dayOfWeek == DayOfWeek.Sunday && IsSunday);
        }

        public override string ToString()
        {
            var result = new List<string>();
            if (IsAll)
            {
                return "All";
            }

            if (IsMonday) { result.Add("Monday"); }
            if (IsTuesday) { result.Add("Tuesday"); }
            if (IsWednesday) { result.Add("Wednesday"); }
            if (IsThursday) { result.Add("Thursday"); }
            if (IsFriday) { result.Add("Friday"); }
            if (IsSaturday) { result.Add("Saturday"); }
            if (IsSunday) { result.Add("Sunday"); }

            return string.Join(", ", result);
        }
    }
}
