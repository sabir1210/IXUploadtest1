using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutoApp
{
    public class DesktopSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/";
        public WindowsDriver<WindowsElement> desktopSession;

        public DesktopSession()
        {
            OpenQA.Selenium.Appium.AppiumOptions appOptions = new OpenQA.Selenium.Appium.AppiumOptions();
            appOptions.AddAdditionalCapability("app", "Root");
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("requireWindowFocus", true);
            appOptions.AddAdditionalCapability("window_animation_scale", false);
            appOptions.AddAdditionalCapability("transition_animation_scale ", false);
            appOptions.AddAdditionalCapability("animator_duration_scale ", false);
            //appCapabilities..SetCapability("app", "Root");
            //.SetCapability("deviceName", "WindowsPC");
            desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions);
        }

        ~DesktopSession()
        {
            desktopSession.Quit();
        }

        public WindowsDriver<WindowsElement> DesktopSessionElement
        {
            get { return desktopSession; }
        }

        public WindowsElement FindElementByAbsoluteXPath(string xPath, int nTryCount = 2)
        {
            WindowsElement uiTarget = null;

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
