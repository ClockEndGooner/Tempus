
using System;
using System.Runtime.Serialization;

namespace Tempus.ViewModels
{
    public enum StopwatchState
    {
        Stopped = 0,
        Running,
        Paused,
        Resumed,
        Reset,                                                                                            
        TimingLap
    }

    public class StopwatchEventArgs : EventArgs
    {
        public DateTime OccuredOnUTC { get; private set; }
        public string Message { get; private set; }
        public StopwatchState State { get; private set; }
        public DateTime StartedOn { get; private set; }
        public TimeSpan ElapsedTime { get; private set; }

        public static StopwatchEventArgs InstantiateStopwatchEventArgs(string message, StopwatchState state, DateTime startedOn, TimeSpan elapsedTime)
        {
            return new StopwatchEventArgs
            {
                OccuredOnUTC = DateTime.UtcNow,
                Message = message,
                State = state,
                StartedOn = startedOn,
                ElapsedTime = elapsedTime
            };
        }
    }

    public class StopwatchException : Exception 
    {
        public DateTime OccuredOnUTC { get; private set; }

        public StopwatchState State { get; set; }

        public DateTime StartedOn { get; set; }

        public TimeSpan TimeSpan { get; set; }

        #region Class Constructors

        public StopwatchException() : base()
        {
            OccuredOnUTC = DateTime.UtcNow; 
        }

        public StopwatchException(string message) : base(message)
        {
            OccuredOnUTC = DateTime.UtcNow;
        }

        public StopwatchException(string message, Exception exception) : base(message, exception)
        {
            OccuredOnUTC = DateTime.UtcNow;
        }

        protected StopwatchException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
            throw new NotImplementedException();
        }

        #endregion // Class Constructors
    }

    public interface IStopwatchClient : IDisposable
    {
        void OnStopwatchStarted(StopwatchEventArgs startEvent);
        void OnStopwatchStopped(StopwatchEventArgs stopEvent);
        void OnStopwatchUpdated(StopwatchEventArgs updateEvent);
        void OnStopwatchReset(StopwatchEventArgs resetEvent);
        void OnStopwatchPaused(StopwatchEventArgs pausedEvent);
        void OnStopwatchResumed(StopwatchEventArgs resumedEvent);
        void OnStopwatchOnError();
    }
}
