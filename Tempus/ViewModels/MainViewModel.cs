
using System; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Threading;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Tempus.Timer;

namespace Tempus.ViewModels
{
    public sealed class MainViewModel : ObservableObject
    {
        private ITimer _stopwatch;
        private string _elapsedTime;
        private RelayCommand _startCommand;
        private RelayCommand _stopCommand;
        private RelayCommand _resetCommand;
        private IStopwatchClient _stopwatchClient;

        public MainViewModel(IStopwatchClient stopwatch)
        {   
            _stopwatch = TempusTimerFactory.InitializeITimer();
            _elapsedTime = string.Empty;

            _stopwatchClient = stopwatch;

            _startCommand = null;
            _stopCommand = null;
            _resetCommand = null;

            ResetStopwatch();
        }

        public void InitializeStopwatch()
        {
            var dispatcherTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };

            dispatcherTimer.Tick += OnDispatcherTimerTick;
            dispatcherTimer.Start();
            
            _stopwatch.Reset();

           // UpdateElapsedTime();
        }

        public void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            var hours = _stopwatch?.Elapsed.Hours.ToString("00");
            var minutes = _stopwatch?.Elapsed.Minutes.ToString("00");
            var seconds = _stopwatch?.Elapsed.Seconds.ToString("00");
            var milliseconds = _stopwatch?.Elapsed.Milliseconds;

            ElapsedTime = $"{hours}:{minutes}:{seconds}.{milliseconds.Value.ToString("000")}";
            // ElapsedTime = $"{hours}:{minutes}:{seconds}";
        }

        public bool IsStopwatchRunning { get => _stopwatch.IsRunning; }

        public IRelayCommand StartCommand
        {
            get
            {
                return _startCommand != null ? _startCommand : new RelayCommand(StartStopwatch);
            }
        }

        private void StartStopwatch()
        {
            InitializeStopwatch();

            _stopwatch.Start();
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
            _stopwatch.Stop();
        }

        public IRelayCommand ResetCommand
        {
            get
            {
                return _resetCommand != null ? _resetCommand : new RelayCommand(() => { ResetStopwatch(); });
            }
        }

        public void ResetStopwatch()
        {
            _stopwatch.Reset();

            ElapsedTime = "00:00.00";
        }

        public string ElapsedTime
        {
            get => _elapsedTime;

            set => SetProperty(ref _elapsedTime, value);
        }
    }
}
