
using System;

namespace Tempus.Timer
{
    public interface ITimer
    {
        #region ITimer Interface Properties

        bool IsRunning { get; }

        bool IsHighResolution { get; }

        TimeSpan Elapsed { get; }

        #endregion // ITimer Interface Properties

        #region ITimer Interface Methods

        abstract void Start();

        abstract void Stop();

        abstract void Reset();

        #endregion // ITimer Interface Methods

    }
}
