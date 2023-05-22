using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Appium;

namespace AutoApp
{
    public class DesktopSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/";
        public AppiumDriver<WindowsElement> desktopSession;

        public DesktopSession()
        {
            OpenQA.Selenium.Appium.AppiumOptions appOptions = new OpenQA.Selenium.Appium.AppiumOptions();
            appOptions.AddAdditionalCapability("app", "Root");
            //appOptions.AddAdditionalCapability("app", @"D:\Projects\Robot\BlazorT\RobotAutoApp\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe"); // \WinFormTestApp.exe");
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("requireWindowFocus", true);
            appOptions.AddAdditionalCapability("window_animation_scale", false);
            appOptions.AddAdditionalCapability("transition_animation_scale ", false);
            appOptions.AddAdditionalCapability("animator_duration_scale ", false);
            //appCapabilities..SetCapability("app", "Root");
            //.SetCapability("deviceName", "WindowsPC");
            //desktopSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appOptions);
            desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions);
        }

        ~DesktopSession()
        {
            desktopSession.Quit();
        }

        public AppiumDriver<WindowsElement> DesktopSessionElement
        {
            get { return desktopSession; }
        }

        public AppiumWebElement FindElementByAbsoluteXPath(string xPath, int nTryCount = 2)
        {
            AppiumWebElement uiTarget = null;

            while (nTryCount-- > 0)
            {
                try
                {
                    uiTarget = desktopSession.FindElementByXPath(xPath);
                }
                catch
                {
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(2000);
                }
            }

            return uiTarget;
        }


    }
}
