using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using WinAppDriver.Generation.States;
using WinAppDriver.Generation.UiElements.Models;
using WinAppDriver.Generation.WindowsDrivers.Models;

namespace WinAppDriver.Generation.WindowsDrivers.Extensions
{
    public static class WindowsDriversExtensions
    {
        /// <summary>
        /// Creates a Windows Application Driver based on the capabilities passed
        /// </summary>
        /// <param name="driverCapabilities"></param>
        /// <returns></returns>
        public static WindowsDriver<WindowsElement> GetDriverByCapabilities(List<WindowsApplicationDriverCapability> driverCapabilities)
        {
            OpenQA.Selenium.Appium.AppiumOptions appOptions = new OpenQA.Selenium.Appium.AppiumOptions();
            appOptions.AddAdditionalCapability("app", @"D:\Projects\Robot\BlazorT\BDU\WinAppDriver\WinFormTestApp\bin\Debug\net5.0-windows\WinFormTestApp.exe");
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("requireWindowFocus", true);
            appOptions.AddAdditionalCapability("window_animation_scale", false);
            appOptions.AddAdditionalCapability("transition_animation_scale ", false);
            appOptions.AddAdditionalCapability("animator_duration_scale ", false);

            //DesiredCapabilities appCapabilities = new DesiredCapabilities();

            //driverCapabilities.ForEach(capability =>
            //{
            //    appCapabilities.SetCapability(capability.Name, capability.Value);
            //});

            WindowsDriver<WindowsElement> driver = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverState.WindowsApplicationDriverRootUrl), appOptions, TimeSpan.FromSeconds(30));
            driver.Manage().Timeouts().ImplicitWait = WindowsApplicationDriverState.WindowsApplicationDriverImplicitWait;

            return driver;
        }

        /// <summary>
        /// Returns a Windows Application Driver based on the UiElement passed
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public static WindowsDriver<WindowsElement> GetDriverByUiElement(WindowsDriver<WindowsElement> driver, UiElement uiElement)
        {
            var uiWindow = WindowsElementsExtensions.GetFirstElementByCondition(driver, uiElement);
            string menuHandle = (int.Parse(uiWindow.GetAttribute("NativeWindowHandle"))).ToString("x");

            return GetDriverByMenuHandle(menuHandle);
        }

        /// <summary>
        /// Supporting function to return a Windows Application Driver based on a menu handle
        /// </summary>
        /// <param name="menuHandle"></param>
        /// <returns></returns>
        private static WindowsDriver<WindowsElement> GetDriverByMenuHandle(string menuHandle)
        {
            var windowCapabilities = new List<WindowsApplicationDriverCapability>
            {
                new WindowsApplicationDriverCapability
                {
                    Name = "appTopLevelWindow",
                    Value = menuHandle
                }
            };

            WindowsDriver<WindowsElement> driver = GetDriverByCapabilities(windowCapabilities);
            driver.Manage().Timeouts().ImplicitWait = WindowsApplicationDriverState.WindowsApplicationDriverImplicitWait;

            return driver;
        }
    }
}