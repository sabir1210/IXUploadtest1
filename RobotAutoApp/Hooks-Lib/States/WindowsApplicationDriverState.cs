using OpenQA.Selenium.Appium.Windows;
using System;

namespace WinAppDriver.Generation.States
{
    internal static class WindowsApplicationDriverState
    {
        /// <summary>
        /// Windows Application Driver - Executable File Path
        /// </summary>
        internal static string WindowsApplicationDriverExecutableFile = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";

        /// <summary>
        /// Windows Application Driver - Url
        /// </summary>
        internal static string WindowsApplicationDriverRootUrl = "http://127.0.0.1:4723";

        /// <summary>
        /// Windows Application Driver - Time out
        /// </summary>
        internal static TimeSpan WindowsApplicationDriverImplicitWait = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Windows Application Driver - Desktop Driver
        /// </summary>
        internal static WindowsDriver<WindowsElement> WindowsApplicationDriverRootDriver = null;
    }
}