// <copyright file="BlazorTimer.cs" company="Infotecs">
// Copyright (c) Infotecs. All rights reserved.
// </copyright>

namespace Infotecs.SPA_blazor
{
    using System;
    using System.Timers;

    /// <summary>
    /// Класс для работы с таймером.
    /// </summary>
    public class BlazorTimer
    {
        /// <summary>
        /// Таймер.
        /// </summary>
        public Timer Timer;

        /// <summary>
        /// Событие срабатывания таймера.
        /// </summary>
        public event Action OnElapsed;

        /// <summary>
        /// Установка таймера.
        /// </summary>
        /// <param name="interval">Время задержки.</param>
        public void SetTimer(double interval)
        {
            Timer = new Timer(interval);
            Timer.Elapsed += NotifyTimerElapsed;
            Timer.Enabled = true;
        }

        private void NotifyTimerElapsed(object source, ElapsedEventArgs e)
        {
            OnElapsed?.Invoke();
            Timer.Enabled = false;
            Timer.Dispose();
        }
    }
}
