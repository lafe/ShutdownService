using System;

namespace lafe.ServiceBase.Interface
{
    public interface IServiceTimer
    {
        /// <summary>
        /// Gets or sets the period of the timer.
        /// </summary>
        TimeSpan TimerPeriod { get; set; }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <param name="immediateCheck">if set to <c>true</c> an immediate check will be performed; otherwise, the timer will wait until the timeout has been reached.</param>
        void StartTimer(bool immediateCheck);

        /// <summary>
        /// Stops the timer
        /// </summary>
        void StopTimer();

    }
}