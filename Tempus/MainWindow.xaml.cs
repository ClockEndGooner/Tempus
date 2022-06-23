
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

using MahApps.Metro.Controls;

using Tempus.Properties;

namespace Tempus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region MainWindow Debugging Constant Definitions

        private const int ERROR_LEVEL = 1;
        private const string ERROR_CATEGORY = "Exception";

        #endregion MainWindow Debugging Constant Definitions

        #region MainWindow Windows Interop Supprting Definitions 

        private const int SwShowNormal = 1;
        private const int SwShowMinimized = 2;

        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WindowPlacement.WindowPlacement lpwndpl);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement.WindowPlacement lpPlacement);

        #endregion MainWindow Windows Interop Supprting Definitions 

        #region MainWindow Class Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion MainWindow Class Constructor

        #region MainWindow Class Event Handler Implementations

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            try
            {
                var placement = Settings.Default.WindowPlacement;
                placement.length = Marshal.SizeOf(typeof (WindowPlacement.WindowPlacement));
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var hWindow = new WindowInteropHelper(this).Handle;

            GetWindowPlacement(hWindow, out WindowPlacement.WindowPlacement placement);
            Settings.Default.WindowPlacement = placement;
            Settings.Default.Save();
        }

        #endregion MainWindow Class Event Handler Implementations
    }
}
