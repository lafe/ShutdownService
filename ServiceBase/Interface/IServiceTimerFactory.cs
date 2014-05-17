using System;

namespace lafe.ServiceBase.Interface
{
    public interface IServiceTimerFactory
    {
        /// <summary>
        /// Creates a new timer
        /// </summary>
        /// <param name="callback">The <see cref="Action"/> that should be executed, when the timer fires.</param>
        /// <returns>A new <see cref="IServiceTimer"/> that offers period callbacks</returns>
        IServiceTimer CreateTimer(Action<object> callback);

        /// <summary>
        /// Creates a new timer
        /// </summary>
        /// <param name="period">The period in which the timer should be executed</param>
        /// <param name="callback">The <see cref="Action"/> that should be executed, when the timer fires.</param>
        /// <returns>A new <see cref="IServiceTimer"/> that offers period callbacks</returns>
        IServiceTimer CreateTimer(TimeSpan period, Action<object> callback); 
        
        /// <summary>
        /// Creates a new timer
        /// </summary>
        /// <param name="period">The period in which the timer should be executed</param>
        /// <param name="autoPause">If set to <c>true</c>, the timer will sleep while performing the <paramref name="callback"/>. Afterwards, the timer will resume automatically</param>
        /// <param name="callback">The <see cref="Action"/> that should be executed, when the timer fires.</param>
        /// <returns>A new <see cref="IServiceTimer"/> that offers period callbacks</returns>
        IServiceTimer CreateTimer(TimeSpan period, bool autoPause, Action<object> callback);
    }
}