
using System;
using System.Diagnostics;

namespace Tempus.Timer
{
    internal sealed class TempusTimer : ITimer
    {
        private Stopwatch _stopwatch;

        public TempusTimer()
        {
            _stopwatch = new Stopwatch();
        }

        #region TempusTimer Implementation of ITimer Interface Properties

        public bool IsRunning { get => _stopwatch.IsRunning; }

        public bool IsHighResolution { get => Stopwatch.IsHighResolution; }

        public TimeSpan Elapsed { get => _stopwatch.Elapsed; }
       
        #endregion // TempusTimer Implementation of ITimer Interface Properties

        #region TempusTimer Implementation of ITimer Interface Methods

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        #endregion // TempusTimer Implementation of ITimer Interface Methods
    }

    public class TempusTimerFactory
    {
        public static ITimer InitializeITimer()
        {
            return new TempusTimer() as ITimer;
        }
    }
}
