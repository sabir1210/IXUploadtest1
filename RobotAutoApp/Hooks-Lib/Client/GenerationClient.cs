using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WinAppDriver.Generation.Client.Models;
using WinAppDriver.Generation.Events.Hook;
using WinAppDriver.Generation.Events.Hook.Models;
using WinAppDriver.Generation.Events.Keyboard;
using WinAppDriver.Generation.Events.Native;
using WinAppDriver.Generation.Events.Native.Models;
using WinAppDriver.Generation.States;
using WinAppDriver.Generation.UiAutomationElements;
using WinAppDriver.Generation.UiEvents.Models;
using WinAppDriver.Generation.WindowsDrivers.Extensions;
using WinAppDriver.Generation.WindowsDrivers.Models;

namespace WinAppDriver.Generation.Client
{
    /// <summary>
    ///     Creates client for Automation
    /// </summary>
    public class GenerationClient
    {
        private Process _rootDriverExecutableProcess;

        public GenerationClient(GenerationClientSettings settings)
        {
            UtilityState.IgnoreProcessId = settings.ProcessId;

            UiAutomationCom.SetTransactionTimeout(settings.AutomationTransactionTimeout);
            WindowsApplicationDriverState.WindowsApplicationDriverImplicitWait = settings.AutomationTransactionTimeout;

            InitializeDPIAwareness();
        }

        /// <summary>
        ///     Returns State of Client (IsHookProcedurePaused)
        /// </summary>
        public bool IsPaused
        {
            get
            {
                if (HookConstants.IsHookProcedurePaused) return true;

                return false;
            }
        }

        public event EventHandler<UiEventEventArgs> GenerationUiEventEventHandler;

        public event EventHandler<HookEventEventArgs> GenerationHookEventEventHandler;

        public event EventHandler<EventArgs> GenerationHookProcedureEventHandler;

        /// <summary>
        ///     UiEvent EventHandler - UiEvent [On Click]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationUiEventEventHandler(object sender, UiEventEventArgs e)
        {
            GenerationUiEventEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Hook EventHandler - UiElement [On Hover]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationHookEventEventHandler(object sender, HookEventEventArgs e)
        {
            GenerationHookEventEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Keyboard Hook EventHandler - IsHookProcedurePaused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationHookProcedureEventHandler(object sender, EventArgs e)
        {
            GenerationHookProcedureEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Corrects DPI Awareness for Windows Environment
        /// </summary>
        /// <remarks>
        ///     https://stackoverflow.com/questions/50239138/dpi-awareness-unaware-in-one-release-system-aware-in-the-other
        ///     https://stackoverflow.com/questions/43537990/wpf-clickonce-dpi-awareness-per-monitor-v2/43537991#43537991
        /// </remarks>
        private void InitializeDPIAwareness()
        {
            if (Environment.OSVersion.Version >= new Version(6, 3, 0))
            {
                if (Environment.OSVersion.Version >= new Version(10, 0, 15063))
                {
                    NativeMethods.SetProcessDpiAwarenessContext((int)DpiAwarenessContextTypes
                        .Context_Per_Monitor_Aware_V2);
                }
                else
                {
                    NativeMethods.SetProcessDpiAwareness(DpiAwarenessProcessTypes.Process_Per_Monitor_Dpi_Aware);
                }
            }
            else
            {
                NativeMethods.SetProcessDPIAware();
            }
        }

        /// <summary>
        /// Initialize WinAppDriver Executable
        /// </summary>
        private void InitializeRootDriverExecutable()
        {
            if (File.Exists(WindowsApplicationDriverState.WindowsApplicationDriverExecutableFile))
            {
                // Kill
                var windowsApplicationDrivers = Process.GetProcesses().
                    Where(pr => pr.MainWindowTitle.ToLower().Contains("winappdriver.exe")).ToList();

                windowsApplicationDrivers.ForEach(windowsApplicationDriver =>
                {
                    windowsApplicationDriver.Kill();
                });

                System.Threading.Thread.Sleep(50);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = WindowsApplicationDriverState.WindowsApplicationDriverExecutableFile,
                    WorkingDirectory = Path.GetDirectoryName(WindowsApplicationDriverState.WindowsApplicationDriverExecutableFile),
                };

                _rootDriverExecutableProcess = new Process
                {
                    StartInfo = startInfo
                };
                _rootDriverExecutableProcess.Start();
            }
            else
            {
                throw new Exception("Driver is not installed.");
            }
        }

        /// <summary>
        /// Initialize WinAppDriver Desktop Driver
        /// </summary>
        private void InitializeRootDriverUrl()
        {
            try
            {
                var driverCapabilities = new List<WindowsApplicationDriverCapability>
                {
                    new WindowsApplicationDriverCapability
                    {
                        Name = "app",
                        Value = "Root"
                    },
                    new WindowsApplicationDriverCapability
                    {
                        Name = "deviceName",
                        Value = "WindowsPC"
                    }
                };

                WindowsApplicationDriverState.WindowsApplicationDriverRootDriver = WindowsDriversExtensions.GetDriverByCapabilities(driverCapabilities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                throw new Exception("Driver is not running.");
            }
        }

        /// <summary>
        /// Terminates AutomationCOM object
        /// </summary>
        private void TerminateAutomation()
        {
            UiAutomationCom.Terminate();
        }

        public void InitializePlayback()
        {
            InitializeRootDriverExecutable();
            InitializeRootDriverUrl();
        }

        public void TerminatePlayback()
        {
            if (_rootDriverExecutableProcess != null)
            {
                if (!_rootDriverExecutableProcess.HasExited)
                {
                    _rootDriverExecutableProcess.Kill();
                }

                _rootDriverExecutableProcess.Dispose();
            }
        }

        public void InitializeRecording(HookEventHandlerSettings hookEventHandlerSettings)
        {
            HookEventHandler.Initialize(hookEventHandlerSettings);
            HookEventHandler.UiEventEventHandler += PostGenerationUiEventEventHandler;
            HookEventHandler.HookElementEventHandler += PostGenerationHookEventEventHandler;
            KeyboardHook.HookProcedureEventHandler += PostGenerationHookProcedureEventHandler;
        }

        public void TerminateRecording()
        {
            HookEventHandler.UiEventEventHandler -= PostGenerationUiEventEventHandler;
            HookEventHandler.HookElementEventHandler -= PostGenerationHookEventEventHandler;
            KeyboardHook.HookProcedureEventHandler -= PostGenerationHookProcedureEventHandler;

            HookEventHandler.Terminate();
        }

        public void Terminate()
        {
            TerminateRecording();
            TerminatePlayback();
            TerminateAutomation();
           
        }

        /// <summary>
        ///     Changes State of Client (IsHookProcedurePaused)
        /// </summary>
        public void Pause()
        {
            HookConstants.IsHookProcedurePaused = !HookConstants.IsHookProcedurePaused;
        }
    }
}