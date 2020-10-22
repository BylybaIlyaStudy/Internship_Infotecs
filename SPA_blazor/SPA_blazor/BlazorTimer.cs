using System;
using System.Timers;

namespace Infotecs.SPA_blazor
{
    public class BlazorTimer
    {
        public Timer _timer;

        public void SetTimer(double interval)
        {
            _timer = new Timer(interval);
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Enabled = true;
        }

        public event Action OnElapsed;

        private void NotifyTimerElapsed(Object source, ElapsedEventArgs e)
        {
            OnElapsed?.Invoke();
            _timer.Enabled = false;
            _timer.Dispose();
        }
    }
}
