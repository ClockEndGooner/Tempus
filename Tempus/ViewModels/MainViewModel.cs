
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading;
using Tempus.Models;

namespace Tempus.ViewModels
{
    public sealed class MainViewModel : ObservableObject
    {
        private StopwatchModel _stopwatch;
        private string _elapsedTime;
        private RelayCommand _startCommand;
        private RelayCommand _stopCommand;
        private RelayCommand _resetCommand;
        private IStopwatchClient _stopwatchClient;
        private bool _quit;

        public MainViewModel(IStopwatchClient stopwatch)
        {
            _stopwatch = new StopwatchModel();
            _elapsedTime = string.Empty;

            _stopwatchClient = stopwatch;

            _startCommand = null;
            _stopCommand = null;
            _resetCommand = null;

            _quit = false;

            ElapsedTime = "12:34.56";
        }

        public void InitializeStopwatch()
        {
            TimerCallback timerCallback = UpdateTimeCallback;
            var updateTimer = new Timer(timerCallback, null, 0, 10);
            var i = 0;

            while (!_quit)
            {
                Thread.Sleep(1000);

                if (i >= 10)
                {
                    _quit = true;
                }

                else
                {
                    i++;
                }
            }

            UpdateElapsedTime();
        }

        private void UpdateTimeCallback(object state)
        {
            UpdateElapsedTime();
        }

        private void UpdateElapsedTime()
        {
            SetElapsedTimeFormat();
        }

        public bool IsStopwatchRunning { get => _stopwatch.Running; }

        public IRelayCommand StartCommand
        {
            get
            {
                return _startCommand != null ? _startCommand : new RelayCommand(StartStopwatch);
            }
        }

        private void StartStopwatch()
        {
            ElapsedTime = "00:00.00";

            _stopwatch.Running = true;

            InitializeStopwatch();

            var startedEvent =
            StopwatchEventArgs.InstantiateStopwatchEventArgs("Tempus Stopwatch has been Stopped", StopwatchState.Stopped,
                                                             _stopwatch.StartTime, _stopwatch.Elapsed);

            _stopwatchClient.OnStopwatchStarted(startedEvent);
        }

        public IRelayCommand StopCommand
        {
            get
            {
                return _stopCommand != null ? _stopCommand : new RelayCommand(StopStopwatch);
            }
        }

        private void StopStopwatch()
        {
            _stopwatch.Running = false;
            _quit = true;

            var stoppedEvent =
            StopwatchEventArgs.InstantiateStopwatchEventArgs("Tempus Stopwatch has been Stopped", StopwatchState.Stopped,
                                                             _stopwatch.StartTime, _stopwatch.Elapsed);

            _stopwatchClient.OnStopwatchStopped(stoppedEvent);
        }

        public IRelayCommand ResetCommand
        {
            get
            {
                return _resetCommand != null ? _resetCommand : new RelayCommand(() => { ResetStopwatch(); });
            }
        }

        private void ResetStopwatch()
        {
            _stopwatch.Running = false;

            ElapsedTime = "00:00.00";

            var resetEventArgs =
            StopwatchEventArgs.InstantiateStopwatchEventArgs("Tempus Stopwatch has been Reset",
                                                             StopwatchState.Reset, _stopwatch.StartTime, _stopwatch.Elapsed);

            _stopwatchClient.OnStopwatchReset(resetEventArgs);
        }

        public string ElapsedTime
        {
            get => _elapsedTime;

            set => SetProperty(ref _elapsedTime, value);
        }

        private void SetElapsedTimeFormat()
        {
            var hours = _stopwatch.Elapsed.Hours;
            var minutes = _stopwatch.Elapsed.Minutes;
            var seconds = _stopwatch.Elapsed.Seconds;
            var tenths = Convert.ToInt32(_stopwatch.Elapsed.Milliseconds / 100M);

            var formattedTime = $"{hours.ToString("D2")}:{minutes.ToString("D2")}:"
                                + $"{seconds.ToString("D2")}.{tenths.ToString()}";

            ElapsedTime = formattedTime;
        }
    }
}
