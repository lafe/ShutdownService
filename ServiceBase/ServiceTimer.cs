using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using lafe.Logging.Interface;
using lafe.ServiceBase.Interface;

namespace lafe.ServiceBase
{
    public class ServiceTimer : IServiceTimer
    {
        public ILog Logger { get; set; }

        public Action<object> TimerCallback { get; private set; }

        protected Timer Timer { get; set; }

        public TimeSpan TimerPeriod { get; set; }

        /// <summary>
        /// If set, the timer will pause itself while the callback is executed. After the callback is executed, the timer resumes.
        /// </summary>
        public bool AutoPause { get; set; }

        public ServiceTimer(ILog logger, Action<object> callback)
            : this(new TimeSpan(0, 15, 0), logger, callback)
        {

        }

        public ServiceTimer(TimeSpan period, ILog logger, Action<object> callback)
            : this(new TimeSpan(0, 15, 0), false, logger, callback)
        {
        }

        public ServiceTimer(TimeSpan period, bool autoPause, ILog logger, Action<object> callback)
        {
            Logger = logger;
            this.TimerCallback = callback;
            this.AutoPause = autoPause;

            Logger.Trace(LogNumbers.ConsturctorWithPeriod, string.Format("Storing timer period of {0}", period.ToString()));
            TimerPeriod = period;

            Timer = new Timer(Callback);
        }


        private void Callback(object state)
        {
            try
            {
                Logger.Trace(LogNumbers.TimerFired, "Timer fired");
                StopTimer();
                Logger.Trace(LogNumbers.PausedTimer, "Pausing timer callback has been performed");

                TimerCallback.Invoke(state);
                Logger.Trace(LogNumbers.TimerCallbackComplete, "Timer callback complete");
            }
            catch (Exception ex)
            {
                Logger.Error(LogNumbers.TimerCallbackException, ex, string.Format("While performing the timer callback, an error occured: {0}", ex));
            }
            finally
            {
                StartTimer(false);
                Logger.Trace(LogNumbers.ResumedTimer, "Resumed timer");
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <param name="immediateCheck">if set to <c>true</c> an immediate check will be performed; otherwise, the timer will wait until the timeout has been reached.</param>
        public virtual void StartTimer(bool immediateCheck)
        {
            Logger.Trace(LogNumbers.StartingTimer, immediateCheck ? "Starting timer with immediate check" : "Starting timer with delayed check");
            var dueTime = immediateCheck ? new TimeSpan(0, 0, 1) : TimerPeriod;
            ChangeTimer(dueTime, TimerPeriod);

            Logger.Trace(LogNumbers.StartedTimer, string.Format("Timer started"));
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public virtual void StopTimer()
        {
            Logger.Trace(LogNumbers.StoppingTimer, "Stopping Timer");
            ChangeTimer(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            Logger.Trace(LogNumbers.TimerStopped, "Timer stopped");
        }

        /// <summary>
        /// Changes the timer
        /// </summary>
        /// <param name="dueTime">The time when the timer fires for the firs time</param>
        /// <param name="period">The time between firing events</param>
        protected virtual void ChangeTimer(TimeSpan dueTime, TimeSpan period)
        {
            Logger.Trace(LogNumbers.ChangingTimer, "Changing timer");
            if (Timer == null)
            {
                Logger.Fatal(LogNumbers.CannotChangeTimerNull, "Cannot change the timer, because the Timer object is null.");
                throw new NullReferenceException("Timer is null");
            }
            Timer.Change(dueTime, period);
            Logger.Trace(LogNumbers.ChangedTimer, string.Format("Timer changed. New values:\nDue Time: {0}\nPeriod: {1}", dueTime.ToString(), period.ToString()));
        }
    }
}
