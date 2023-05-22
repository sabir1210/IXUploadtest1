using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using static BDU.UTIL.Enums;

namespace AutoApp
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
                    ChromeOptions chromeOption = new ChromeOptions();
                    string location = @"./";
                    chromeOption.AddArguments("--disable-extensions");
                    return new ChromeDriver(location, chromeOption);
                default:
                    ChromeOptions chromeOptiondf = new ChromeOptions();
                    string locationDf = @"./";
                    chromeOptiondf.AddArguments("--disable-extensions");
                    return new ChromeDriver(locationDf, chromeOptiondf);
            }
        }
    }

}
