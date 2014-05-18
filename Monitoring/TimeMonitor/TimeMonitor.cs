using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ShutdownService.Configuration;
using lafe.ShutdownService.Monitoring.Interface;
using lafe.ShutdownService.Monitoring.NetworkMonitoring;

namespace lafe.ShutdownService.Monitoring.TimeMonitor
{
    public class TimeMonitor : IMonitor
    {
        public string Name
        {
            get
            {
                return "Time Monitor";
            }
        }

        public ILog Logger { get; set; }
        public ITimeProvider TimeProvider { get; set; }
        public Configuration.Configuration Configuration { get; set; }

        public TimeMonitor(Configuration.Configuration configuration, ILog logger, ITimeProvider timeProvider)
        {
            Logger = logger;
            Configuration = configuration;
            TimeProvider = timeProvider;
        }

        public bool CanShutdown()
        {
            if (Configuration.MonitoredTimes == null || Configuration.MonitoredTimes.Length == 0)
            {
                Logger.Trace(LogNumbers.NoMonitoredTimes, "No times configured that should be monitored. Skipping check.");
                return true;
            }

            var currentTime = TimeProvider.GetCurrentTime();
            Logger.Trace(LogNumbers.UsedTime, string.Format("Using time \"{0}\" as reference value for comparison", currentTime));

            var canShutdown = true;
            foreach (var monitoredTime in Configuration.MonitoredTimes)
            {
                Logger.Trace(LogNumbers.CheckingTimeCondition, string.Format("Checking monitoring condition: {0}", monitoredTime));
                if (!CompareWeekday(currentTime, monitoredTime))
                {
                    Logger.Trace(LogNumbers.WeekdayMismatch, "The weekday does not match the current weekday. Monitoring condition is skipped.");
                    continue;
                }

                if (!CompareTime(currentTime, monitoredTime))
                {
                    Logger.Trace(LogNumbers.TimeMismatch, "The current time does not match the specified time. Monitoring condition is skipped.");
                    continue;
                }

                Logger.Trace(LogNumbers.TimeMatch, string.Format("Conditions of current time matchtes the monitoring condition \"{0}\". Preventing shutdown.", monitoredTime));
                canShutdown = false;
                break;
            }

            return canShutdown;
        }

        /// <summary>
        /// Checks if the current weekday is part of the <paramref name="monitoredTime"/>
        /// </summary>
        /// <param name="currentTime">The current <see cref="DateTime"/> that contains the weekday</param>
        /// <param name="monitoredTime">The <see cref="MonitoredTime"/> that contains the current condition</param>
        /// <returns><c>true</c> if the weekday of <paramref name="currentTime"/> is part of <paramref name="monitoredTime"/></returns>
        private bool CompareWeekday(DateTime currentTime, MonitoredTime monitoredTime)
        {
            return monitoredTime.Weekdays.IsActiveOn(currentTime.DayOfWeek);
        }

        /// <summary>
        /// Checks if the current time is part of the <paramref name="monitoredTime"/>
        /// </summary>
        /// <param name="currentTime">The current <see cref="DateTime"/></param>
        /// <param name="monitoredTime">The <see cref="MonitoredTime"/> that contains the current condition</param>
        /// <returns><c>true</c> if the time of <paramref name="currentTime"/> is between <see cref="MonitoredTime.StartTime"/> and <see cref="MonitoredTime.EndTime"/> of <paramref name="monitoredTime"/></returns>
        private bool CompareTime(DateTime currentTime, MonitoredTime monitoredTime)
        {
            var startTime = currentTime.Date.Add(monitoredTime.StartTime.TimeOfDay);
            var endTime = currentTime.Date.Add(monitoredTime.EndTime.TimeOfDay);

            //Check if time range spans midnight
            if (endTime < startTime)
            {
                endTime = endTime.AddDays(1);
            }

            var result = startTime <= currentTime 
                         && currentTime <= endTime;
            return result;
        }

    }
}
