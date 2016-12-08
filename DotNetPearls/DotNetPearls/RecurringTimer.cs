using System;
using System.Threading;

namespace DotNetPearls
{
    // This code was adopted from the following .NET course:
    // https://mva.microsoft.com/en-US/training-courses/advanced-net-threading-part-2-computebound-async-operations-16658?l=fG7K1fitC_2206218965
    public class RecurringTimer
    {
        private readonly Timer _timer;
        private readonly int _dueTime;

        /// <summary>
        /// Create an instance of a recurring timer that calls the callback at a specified interval. The interval starts when the callback has completed its work.
        /// </summary>
        /// <param name="callback">The method to call periodically.</param>
        /// <param name="dueTime">The interval between calls. Note that this is limited to int.MaxValue in milliseconds.</param>
        public RecurringTimer(TimerCallback callback, TimeSpan dueTime)
        {
            _dueTime = (int)Math.Min(dueTime.TotalMilliseconds, int.MaxValue);
            _timer = new Timer(WrapCallback(callback), null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(0, Timeout.Infinite);
        }

        private TimerCallback WrapCallback(TimerCallback callback)
        {
            return state =>
            {
                callback(null);
                _timer.Change(_dueTime, Timeout.Infinite);
            };
        }
    }
}
