using System.Collections.Generic;
using OpenQA.Selenium.Appium.Windows;
using System;
using OpenQA.Selenium.Appium;
using System.IO;
using BDU.ViewModels;
using System.Diagnostics;
using static BDU.UTIL.Enums;
using NLog;
using BDU.UTIL;
using BDU.Services;

namespace BDU.RobotDesktop
{
    public class DesktopSession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/";
        private static Logger _log = LogManager.GetCurrentClassLogger();
        string pmsApp = UTIL.GlobalApp.PMS_Application_Path_WithName;
        public AppiumDriver<WindowsElement> desktopSession;
        private BDUService _bduservice = new BDUService();
        public List<EntityFieldViewModel> erroneousLs = null;
        public List<EntityFieldViewModel> warningLs = null;
        public string AutoAppPath
        {
            get
            {
                return Path.GetFullPath(pmsApp); //Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\RobotAutoApp\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe"));
                //return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe"));
            }
        }
        public bool isRootCapability { get; set; }
        //public DesktopSession()
        //{
        //    try
        //    {
        //        // var pmsApp = UTIL.GlobalApp.PMS_Application_Path_WithName;
        //        OpenQA.Selenium.Appium.AppiumOptions appOptions = new OpenQA.Selenium.Appium.AppiumOptions();
        //        string WindowHandle = "";
        //        if (File.Exists(pmsApp) && UTIL.GlobalApp.IS_PROCESS == "0")
        //        {
        //            appOptions.AddAdditionalCapability("app", Path.GetFullPath(pmsApp));
        //            isRootCapability = false;
        //        }
        //        else if (string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME) && UTIL.GlobalApp.IS_PROCESS == "1") {
        //            appOptions.AddAdditionalCapability("app", Path.GetFullPath(pmsApp));                
        //            isRootCapability = true;
        //        }
        //        else if (UTIL.GlobalApp.IS_PROCESS == "1" && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME))
        //        {
        //            Process[] localprocessName = Process.GetProcessesByName(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME);
        //            /*string v = "";
        //            try
        //            {
        //                v = localprocessName[0].MainModule.FileName;
        //            }
        //            catch { }
        //            if (v.ToUpper().Contains(pmsApp.ToUpper()))
        //            {
        //                WindowHandle = localprocessName[0].MainWindowHandle.ToString("x");                       
        //            }
        //            */
        //            if (localprocessName.Length > 0)
        //            {

        //                int processId = localprocessName[0].Id;
        //                appOptions.AddAdditionalCapability("app", "Root");
        //                //appOptions.AddAdditionalCapability("app", pmsApp);

        //                //appOptions.AddAdditionalCapability("appTopLevelWindow", WindowHandle);
        //                isRootCapability = true;

        //            }                   
        //        }
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.APP_ARGUMENTS))
        //            appOptions.AddAdditionalCapability("appArguments", UTIL.GlobalApp.APP_ARGUMENTS);
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PLATFORM_NAME))
        //            appOptions.AddAdditionalCapability("platformName", UTIL.GlobalApp.PLATFORM_NAME);
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.DEVICE_NAME))
        //            appOptions.AddAdditionalCapability("deviceName", UTIL.GlobalApp.DEVICE_NAME);
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.START_IN))
        //            appOptions.AddAdditionalCapability("start_in", UTIL.GlobalApp.START_IN);
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.CUSTOM1))
        //        {
        //            string[] customCaps = UTIL.GlobalApp.CUSTOM1.Split(":");
        //            if (customCaps != null && customCaps.Length >= 2)
        //                appOptions.AddAdditionalCapability(customCaps[0].ToString(), customCaps[1].ToString());
        //        }
        //        if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.CUSTOM2))
        //        {
        //            string[] customCaps = UTIL.GlobalApp.CUSTOM2.Split(":");
        //            if (customCaps != null && customCaps.Length >= 2)
        //                appOptions.AddAdditionalCapability(customCaps[0].ToString(), customCaps[1].ToString());
        //        }
        //        //appOptions.AddAdditionalCapability("start_in", UTIL.GlobalApp.START_IN);
        //        //appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
        //        //appOptions.AddAdditionalCapability("platformName", "windows");
        //        //appOptions.AddAdditionalCapability("start_in", @"c:\e1-vhp");
        //       // appOptions.AddAdditionalCapability("applicationCacheEnabled", true);
        //        appOptions.AddAdditionalCapability("applicationCacheEnabled", false);
        //        appOptions.AddAdditionalCapability("requireWindowFocus", true);
        //        //  appOptions.AddAdditionalCapability("autoDismissAlerts", true);
        //        appOptions.AddAdditionalCapability("transition_animation_scale ", false);
        //        appOptions.AddAdditionalCapability("animator_duration_scale ", false);
        //        //   appOptions.AddAdditionalCapability("ms:experimental-webdriver", true);
        //        appOptions.AddAdditionalCapability("fullRest", false);
        //        appOptions.AddAdditionalCapability("getMatchedImageResult ", true);
        //        // appOptions.AddAdditionalCapability("ms:waitForAppLaunch", "25");
        //        //appOptions.AddAdditionalCapability("noReset", true);
        //        appOptions.AddAdditionalCapability("noReset", false);
        //        // appOptions.AddAdditionalCapability("fullReset", false);
        //        //appCapabilities..SetCapability("app", "Root");
        //        //.SetCapability("deviceName", "WindowsPC");
        //        //desktopSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appOptions);

        //        // desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions, TimeSpan.FromMinutes(2));
        //        desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions);
        //        // desktopSession.Manage().Timeouts().ImplicitWait= TimeSpan.FromMilliseconds(500);


        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Fatal("IX-Failure Detected", ex);
        //        _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
        //    }

        //}
        public DesktopSession()
        {
            try
            {
                // var pmsApp = UTIL.GlobalApp.PMS_Application_Path_WithName;
                OpenQA.Selenium.Appium.AppiumOptions appOptions = new OpenQA.Selenium.Appium.AppiumOptions();
                string WindowHandle = "";
                if (File.Exists(pmsApp) && UTIL.GlobalApp.IS_PROCESS == "0")
                {
                    appOptions.AddAdditionalCapability("app", Path.GetFullPath(pmsApp));
                    isRootCapability = false;
                }
                else if (string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME) && UTIL.GlobalApp.IS_PROCESS == "1")
                {
                    appOptions.AddAdditionalCapability("app", Path.GetFullPath(pmsApp));
                    isRootCapability = true;
                }
                else if (UTIL.GlobalApp.IS_PROCESS == "1" && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME))
                {
                    Process[] localprocessName = Process.GetProcessesByName(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME);
                    /*string v = "";
                    try
                    {
                        v = localprocessName[0].MainModule.FileName;
                    }
                    catch { }
                    if (v.ToUpper().Contains(pmsApp.ToUpper()))
                    {
                        WindowHandle = localprocessName[0].MainWindowHandle.ToString("x");
                    }
                    */
                    if (localprocessName.Length > 0)
                    {
                        int processId = localprocessName[0].Id;
                        appOptions.AddAdditionalCapability("app", "Root");
                        //appOptions.AddAdditionalCapability("app", pmsApp);
                        //appOptions.AddAdditionalCapability("appTopLevelWindow", WindowHandle);
                        isRootCapability = true;
                    }
                }
                appOptions.AddAdditionalCapability("app", "C:\\OpenEdge\\bin\\prowin32.exe");
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.APP_ARGUMENTS))
                    appOptions.AddAdditionalCapability("appArguments", UTIL.GlobalApp.APP_ARGUMENTS);
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PLATFORM_NAME))
                    appOptions.AddAdditionalCapability("platformName", UTIL.GlobalApp.PLATFORM_NAME);
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.DEVICE_NAME))
                    appOptions.AddAdditionalCapability("deviceName", UTIL.GlobalApp.DEVICE_NAME);
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.START_IN))
                    appOptions.AddAdditionalCapability("start_in", UTIL.GlobalApp.START_IN);
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.CUSTOM1))
                {
                    string[] customCaps = UTIL.GlobalApp.CUSTOM1.Split(":");
                    if (customCaps != null && customCaps.Length >= 2)
                        appOptions.AddAdditionalCapability(customCaps[0].ToString(), customCaps[1].ToString());
                }
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.CUSTOM2))
                {
                    string[] customCaps = UTIL.GlobalApp.CUSTOM2.Split(":");
                    if (customCaps != null && customCaps.Length >= 2)
                        appOptions.AddAdditionalCapability(customCaps[0].ToString(), customCaps[1].ToString());
                }
                //appOptions.AddAdditionalCapability("start_in", UTIL.GlobalApp.START_IN);
                //appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                //appOptions.AddAdditionalCapability("platformName", "windows");
                //appOptions.AddAdditionalCapability("start_in", @"c:\e1-vhp");
                // appOptions.AddAdditionalCapability("applicationCacheEnabled", true);
                appOptions.AddAdditionalCapability("applicationCacheEnabled", false);
                appOptions.AddAdditionalCapability("requireWindowFocus", true);
                //  appOptions.AddAdditionalCapability("autoDismissAlerts", true);
                appOptions.AddAdditionalCapability("transition_animation_scale ", false);
                appOptions.AddAdditionalCapability("animator_duration_scale ", false);
                //   appOptions.AddAdditionalCapability("ms:experimental-webdriver", true);
                appOptions.AddAdditionalCapability("fullRest", false);
                appOptions.AddAdditionalCapability("getMatchedImageResult ", true);
                // appOptions.AddAdditionalCapability("ms:waitForAppLaunch", "25");
                //appOptions.AddAdditionalCapability("noReset", true);
                appOptions.AddAdditionalCapability("noReset", false);
                // appOptions.AddAdditionalCapability("fullReset", false);
                //appCapabilities..SetCapability("app", "Root");
                //.SetCapability("deviceName", "WindowsPC");
                //desktopSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appOptions);
                // desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions, TimeSpan.FromMinutes(2));
                desktopSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions);
                // desktopSession.Manage().Timeouts().ImplicitWait= TimeSpan.FromMilliseconds(500);
            }
            catch (Exception ex)
            {
                _log.Fatal("IX-Failure Detected", ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
            }
        }

        ~DesktopSession()
        {
            if (desktopSession != null)
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
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    ex.StackTrace.ToString();
                }

                if (uiTarget != null)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }

            return uiTarget;
        }

        public AppiumWebElement FindElementByImageh(string path)
        {
            AppiumWebElement uiTarget = null;

            try
            {
                uiTarget = desktopSession.FindElementByImage(path);
            }
            catch
            {
            }

            return uiTarget;
        }


    }
}
