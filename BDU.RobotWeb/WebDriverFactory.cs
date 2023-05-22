using System;
using System.Collections;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using static BDU.UTIL.Enums;

namespace BDU.RobotWeb
{
    /// <summary>
    /// Factory for creating WebDriver for various browsers. Blazor Technologies Inc
    /// </summary>
    public static class WebDriverFactory
    {
        /// <summary>
        /// Initilizes IWebDriver base on the given WebBrowser name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IWebDriver CreateWebDriver(WebBrowser name)
        {
            //ChromeDriverManager.getInstance().setup();
            //FirefoxDriverManager.getInstance().setup();
            //OperaDriverManager.getInstance().setup();
            //PhantomJsDriverManager.getInstance().setup();
            //EdgeDriverManager.getInstance().setup();
            //InternetExplorerDriverManager.getInstance().setup();
            string driverPath = Environment.CurrentDirectory;
            ChromeDriverService chromeDriverService;
            #if (RELEASE)
                if (Directory.Exists(UTIL.GlobalApp.APPLICATION_DRIVERS_PATH))
                driverPath=UTIL.GlobalApp.APPLICATION_DRIVERS_PATH; 
#endif

            switch (name)
            {
                case WebBrowser.Firefox:
                    return new FirefoxDriver();
                case WebBrowser.IE:
                case WebBrowser.InternetExplorer:
                    InternetExplorerOptions ieOption = new InternetExplorerOptions();
                    ieOption.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOption.EnsureCleanSession = true;
                    ieOption.RequireWindowFocus = true;
                    return new InternetExplorerDriver(@"./", ieOption);
                case WebBrowser.Chrome:
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                   // OpenQA.Selenium.IWebDriver driver = new ChromeDriver();
                    chromeDriverService = ChromeDriverService.CreateDefaultService();
//#if (DEBUG)
//                    chromeDriverService = ChromeDriverService.CreateDefaultService();
//#elif (RELEASE)
//             if (Directory.Exists(UTIL.GlobalApp.APPLICATION_DRIVERS_PATH)){               
//                chromeDriverService = ChromeDriverService.CreateDefaultService(UTIL.GlobalApp.APPLICATION_DRIVERS_PATH);
//                }else
//                chromeDriverService = ChromeDriverService.CreateDefaultService();                
//#endif
                    chromeDriverService.HideCommandPromptWindow = true;
                    chromeDriverService.SuppressInitialDiagnosticInformation = true;
                    //  Hashtable map = new Hashtable();

                    // Add some elements to the hash table. There are no
                    // duplicate keys, but some of the values are duplicates.



                    //chromeOptions.AddArgument("disable-popup-blocking");

                    // map.Add("profile.default_content_setting_values.notifications", 1);
                    // options.AddAdditionalCapability(, map);
                    // options.AddArgument("ms:experimental-webdriver", "true");
                    options.AddExcludedArgument("disable-popup-blocking");

                    //  map = { "profile.default_content_settings.popups": 1};
                    // options.add_experimental_option("prefs", start-maximized);
                    //  ChromeDriver driver = new ChromeDriver(options);
                    //  chromeOptions.AddArgument("--incognito");
                    //   string location = @"./";
                    //   // chromeOption.BinaryLocation = @"D:\Shared\BDU-Core\BDU.RobotWeb\chromedriver.exe";
                    ////   chromeOption.AddArguments("--disable-extensions");
                    //   //chromeOption.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, UnhandledPromptBehavior.Accept);
                    //   // DesiredCapabilities cap = new DesiredCapabailities();
                    //   chromeOption.AddArguments("--test-type", "--ignore-certificate-errors");
                    //   chromeOption.AddArguments("--disable-web-security");
                    //   chromeOption.AddArguments("--allow-running-insecure-content");
                    // chromeOption.AddArgument("--disable-notifications");
                    // chromeOption.AddArguments("--disable-extensions");
                    // return new ChromeDriver(location, chromeOption);
                    // prefs = { "profile.default_content_setting_values.notifications" : 1}
                    // chromeOption.ad("prefs", prefs) Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"))
                    return new ChromeDriver(chromeDriverService, options);
                default:
                    ChromeOptions chromeOptiondf = new ChromeOptions();
                    string locationDf =  @".\";
                    chromeOptiondf.AddArgument("--start-maximized");
                    chromeOptiondf.AddArguments("--disable-extensions");
                    chromeOptiondf.AddArgument("headless");
                    chromeOptiondf.AddArguments("--disable-web-security");
                    chromeOptiondf.AddArguments("--allow-running-insecure-content");
                    // chromeOptiondf.AddArguments("ms:experimental-webdriver", "true");
                    return new ChromeDriver(locationDf, chromeOptiondf);
            }
        }
    }

}
