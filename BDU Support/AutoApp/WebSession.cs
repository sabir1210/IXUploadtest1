
using System.Linq;

using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Appium;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using BDU.ViewModels;
using static BDU.UTIL.Enums;

namespace AutoApp
{
    public class WebSession
    {
        private const string ApplicationUrl = "https://www.w3schools.com/tags/tryit.asp?filename=tryhtml5_input_type";
        public IWebDriver driver;
        //https://www.automatetheplanet.com/wp-content/uploads/2017/02/MostCompleteWebDriverCSharpCheetSheet.pdf
        public WebSession()
        {
            driver = WebDriverFactory.CreateWebDriver(WebBrowser.Chrome);
            driver.Navigate().GoToUrl(ApplicationUrl);
         IWebElement  element=  this.driver.FindElement(By.Id("username"));

            string title = this.driver.Title;
            // Get the current URL
            string url = this.driver.Url;
            // Get the current page HTML source
            string html = this.driver.PageSource;

            element.Click();
            element.SendKeys("zahid.nawaz");

            //element.Click();
            //element.SendKeys("someText");
            //element.Clear();
            //element.Submit();


        }

        ~WebSession()
        {
            driver.Quit();
        }

        public string FindValueWebElement(string identifier, int findOption= 1)
        {
            IWebElement element = null;

            switch (findOption)
            {
                case 1: // Input
                    element = this.driver.FindElement(By.Id(identifier));
                    return element.Text;
                    break;
                case 2:// Select
                    IWebElement select = this.driver.FindElement(By.Name(identifier));                   
                    SelectElement selectctrl = new SelectElement(select);
                    element = selectctrl.SelectedOption;
                    return element.Text;
                    break;
                case 3:// DTP
                    element = this.driver.FindElement(By.ClassName(identifier));
                    return element.Text;
                    break;
                case 4:
                    element = this.driver.FindElement(By.CssSelector(identifier));
                    return element.Text;
                    break;
                case 5:
                    element = this.driver.FindElement(By.XPath(identifier));
                    return element.Text;
                    break;
                default:
                    element = this.driver.FindElement(By.Id(identifier));
                    return element.Text;
                    break;
            }

          
            
        }
        public bool FillWebElements(MappingViewModel mappingViews)
        {
            try
            {

                IWebElement form = driver.FindElement(By.Id(mappingViews.pmsformid));
                form.Click();

                // Wait for visibility of an element
                //WebDriverWait wait = new WebDriverWait(driver,
                //TimeSpan.FromSeconds(30));
                //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(
                //By.XPath("//*[@id='tve_editor']/div[2]/div[2]/div/div")));
                IWebElement element = null;
                string formName = mappingViews.entity_name;
                foreach (EntityFieldViewModel field in mappingViews.forms.FirstOrDefault().fields.OrderBy(x => x.sr))
                {
                    switch (field.control_type)
                    {
                        case (int)CONTROL_TYPES.TEXTBOX: // Input
                            element = this.driver.FindElement(By.Id(field.pms_field_name));
                            element.SendKeys("" + field.value);
                            break;
                        case (int)CONTROL_TYPES.DATE: // Input
                            element = this.driver.FindElement(By.Id(field.pms_field_name));
                            element.SendKeys(field.value);

                            break;
                        case (int)CONTROL_TYPES.SELECT:// Select
                            element = this.driver.FindElement(By.Id(field.pms_field_name));
                            SelectElement select = new SelectElement(element);
                            select.SelectByValue(field.value);
                            break;
                        case (int)CONTROL_TYPES.RADIO:// Select
                            element = this.driver.FindElement(By.Name(field.pms_field_name));
                            SelectElement rdo = new SelectElement(element);
                            rdo.SelectByValue(field.value);
                            break;
                        case (int)CONTROL_TYPES.HIDDEN:// Select
                            element = this.driver.FindElement(By.Id(field.pms_field_name));
                            element.SendKeys("" + field.value);
                            break;
                        case (int)CONTROL_TYPES.ACTION:// Select
                            element = driver.FindElement(By.Id(field.pms_field_name));
                            element.Click();
                            //element.SendKeys("someText");
                            //element.Clear();
                            element.Submit();
                            break;
                        default:
                            element = this.driver.FindElement(By.Id(field.pms_field_name));
                            element.SendKeys(field.value);
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex) {                
                return false;
            }

        }

    }
}
