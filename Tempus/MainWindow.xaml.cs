﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

using MahApps.Metro.Controls;

using Tempus.ViewModels;

namespace Tempus
{
    using System.Windows.Data;
    using Tempus.Properties;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IStopwatchClient, IDisposable
    {
        #region MainWindow Debugging Constant Definitions

        private const int ERROR_LEVEL = 1;
        private const string ERROR_CATEGORY = "Exception";

        #endregion MainWindow Debugging Constant Definitions

        #region MainWindow Windows Interop Supprting Definitions

        private const int SwShowNormal = 1;
        private const int SwShowMinimized = 2;
        private bool disposedValue;

        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WindowPlacement.WindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement.WindowPlacement lpPlacement);

        #endregion MainWindow Windows Interop Supprting Definitions

        #region MainWindow Class Constructor

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(this);

            ToggleButton += OnToggleStartStopButton;

            SizeChanged += OnWindowSizeChanged;
        }

        #endregion MainWindow Class Constructor

        public void OnStopwatchStarted(StopwatchEventArgs startEvent)
        {
            Debug.WriteLine("IStopwatchClient.OnStopwatchStarted() Called.");

            ElapsedTimeTextBox.Text = "00:00:00.00";
        }

        public void OnStopwatchStopped(StopwatchEventArgs stopEvent)
        {
            throw new NotImplementedException();
        }

        public void OnStopwatchUpdated(StopwatchEventArgs updateEvent)
        {
            throw new NotImplementedException();
        }

        public void OnStopwatchReset(StopwatchEventArgs resetEvent)
        {
            Debug.WriteLine("IStopwatchClient.OnStopwatchReset() Called.");
        }

        public void OnStopwatchPaused(StopwatchEventArgs pausedEvent)
        {
            throw new NotImplementedException();
        }

        public void OnStopwatchResumed(StopwatchEventArgs resumedEvent)
        {
            throw new NotImplementedException();
        }

        public void OnStopwatchOnError()
        {   
                throw new NotImplementedException();
        }   
            
        #region MainWindow Class Event Handler Implementations

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            try
            {
                var placement = Settings.Default.WindowPlacement;
                placement.length = Marshal.SizeOf(typeof(WindowPlacement.WindowPlacement));
                placement.flags = 0;
                placement.showCmd = (placement.showCmd == SwShowMinimized ? SwShowNormal : placement.showCmd);

                var hWindow = new WindowInteropHelper(this).Handle;
                SetWindowPlacement(hWindow, ref placement);
            }

            catch (Exception exception)
            {
                Debug.WriteLine($"Exception Message: {exception.Message}");
                Debug.WriteLine($"Exception Source: {exception.Source}");
                Debug.WriteLine($"Exception StackTrace: {exception.StackTrace}");

                if (Debugger.IsAttached)
                {
                    if (Debugger.IsLogging())
                    {
                        Debugger.Log(ERROR_LEVEL, ERROR_CATEGORY, exception.Message);
                        Debugger.Log(ERROR_LEVEL, ERROR_CATEGORY, exception.Source);
                        Debugger.Log(ERROR_LEVEL, ERROR_CATEGORY, exception.StackTrace);
                    }

                    Debugger.Break();
                }
            }
        }

        protected void OnWindowSizeChanged(object seneder, SizeChangedEventArgs sizeChanges)
        {
            var mainWindow = (Window)seneder;

            var trace = $"    {mainWindow.Name} ActualWidth: {ActualWidth.ToString("F2")} <<<<";
            Debug.WriteLine(trace);

            trace = $"    >>>> {mainWindow.Name} ActualHeight: {ActualHeight.ToString("F2")} <<<<";
            Debug.WriteLine(trace);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var hWindow = new WindowInteropHelper(this).Handle;

            GetWindowPlacement(hWindow, out WindowPlacement.WindowPlacement placement);
            Settings.Default.WindowPlacement = placement;
            Settings.Default.Save();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void OnElapsedTimeChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        #endregion MainWindow Class Event Handler Implementations

        public static readonly RoutedEvent ToggleButtonEvent =
        EventManager.RegisterRoutedEvent
        (
            name: "ToggleButton",
            routingStrategy: RoutingStrategy.Bubble,
            handlerType: typeof(RoutedEventHandler),
            ownerType: typeof(Button)
        );

        public event RoutedEventHandler ToggleButton
        {
            add { AddHandler(ToggleButtonEvent, value); }
            remove { RemoveHandler(ToggleButtonEvent, value); }
        }

        private void OnToggleStartStopButton(object sender, RoutedEventArgs e)
        {
            Button startStopButton = (Button)sender;
            var binding = new Binding();

            if ((string)startStopButton.ToolTip == "Start")
            {
                binding.Path = new PropertyPath("StopCommand"); //Name of the property in Datacontext
                startStopButton.SetBinding(Button.CommandProperty, binding);
                startStopButton.ToolTip = "Stop";
                startStopButton.Content = "pack://application:,,,/Resources/Images/Stop.png";
            }

            else
            {
                binding.Path = new PropertyPath("StartCommand"); //Name of the property in Datacontext
                startStopButton.SetBinding(Button.CommandProperty, binding);
                startStopButton.ToolTip = "Stsrt";
                startStopButton.Content = "pack://application:,,,/Resources/Images/Start.png";
            }
        }
    }
}