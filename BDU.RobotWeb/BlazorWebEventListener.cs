using System.Linq;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using BDU.ViewModels;
using static BDU.UTIL.Enums;
using System.Collections.Generic;
using NLog;
using OpenQA.Selenium.Support.Events;//.events.WebDriverEventListener;
using System.Globalization;

namespace BDU.RobotWeb
{
    public class BlazorWebEventListener  : EventFiringWebDriver
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public delegate void PMSDataFoundEvent(MappingViewModel mappingViewModel);
        public event PMSDataFoundEvent blazorDataFoundSaveEvent;
        public List<MappingViewModel> scannableEntities = null;
        private List<MappingDefinitionViewModel> definitionEntities = null;
        
        private IWebDriver _driver;
        public  string ApplicationUrl = "";
       // public IWebDriver driver;      
        public string script;     
      //  EventFiringWebDriver eventHandler = null;
        public BlazorWebEventListener(IWebDriver driver) : base(driver)
        {
            this._driver = _driver;
          
            //  eventHandler = new EventFiringWebDriver(driver);
            // this.ExceptionThrown += new EventHandler<WebDriverExceptionEventArgs>(firingDriver_ExceptionThrown);
            // this.ElementClicked += new EventHandler<WebElementEventArgs>(firingDriver_ElementClicked);
            // this.FindElementCompleted += new EventHandler<FindElementEventArgs>(firingDriver_FindElementCompleted);
            // this.ElementClicking += new EventHandler<WebElementEventArgs>(firingDriver_ElementClicking);

        }
        public void intializeEventActionsData(List<MappingViewModel> entities)
        {
             scannableEntities = entities;
            if (scannableEntities != null)
            {
                definitionEntities = new List<MappingDefinitionViewModel>();
                foreach (MappingViewModel ety in scannableEntities)
                {
                    if (ety.status == (int)UTIL.Enums.STATUSES.Active)
                    {                       
                        foreach (FormViewModel frm in ety.forms)
                        {
                            if (frm.Status == (int)UTIL.Enums.STATUSES.Active) { 
                                foreach (EntityFieldViewModel flds in frm.fields)
                                {
                                    if (frm.Status == (int)UTIL.Enums.STATUSES.Active && flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE) {
                                        MappingDefinitionViewModel defEntity = new MappingDefinitionViewModel { entity_Id = ety.entity_Id, pmsformid = Convert.ToString(frm.pmspageid), xpath = Convert.ToString(ety.xpath) };
                                        defEntity.SubmitCaptureFieldId = flds.pms_field_name;
                                        defEntity.SubmitCaptureFieldXpath = flds.pms_field_xpath;
                                        definitionEntities.Add(defEntity);
                                    }  
                                }
                        }
                        }
                        
                    }//foreach (MappingViewModel ety in scanningEntities) {
                }

            }
           // return eventHandler;
            // eventHandler.Manage().Window
            // eventHandler.Navigate().GoToUrl(ApplicationUrl);
            //  eventHandler.Navigate().Refresh();
            //IWebElement element=  eventHandler.FindElement(By.Id("btnSubmit"));
            // element.Click();
            // fDriver.OnScriptExecuting += new Action<WebDriverScriptEventArgs>(firingDriver_OnScriptExecuting);
            //eventFiringDriver
        }
        ~BlazorWebEventListener()
        {
          //  this.ExceptionThrown -= new EventHandler<WebDriverExceptionEventArgs>(firingDriver_ExceptionThrown);
          //  eventHandler.ElementClicked -= new EventHandler<WebElementEventArgs>(firingDriver_ElementClicked);
           // eventHandler.FindElementCompleted -= new EventHandler<FindElementEventArgs>(firingDriver_FindElementCompleted);
            //this.ElementClicking -= new EventHandler<WebElementEventArgs>(firingDriver_ElementClicking);
           // eventHandler.ScriptExecuting -= new EventHandler<WebDriverScriptEventArgs>(OnScriptExecuting);
           // eventHandler = null;
            scannableEntities = null;
        }
        //private void firingDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        //{
        //    Console.WriteLine(e.ThrownException.Message);
        //}
        protected override void OnElementClicking(WebElementEventArgs e) {
            IWebElement control =e.Element;           
           // WebElement currentElement = e.Driver.SwitchTo().ActiveElement;
            if ((UTIL.GlobalApp.currentIntegratorXStatus==  ROBOT_UI_STATUS.READY  || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && scannableEntities != null && scannableEntities.Count() > 0 && definitionEntities != null && UTIL.GlobalApp.Authentication_Done)
            //if ( scannableEntities != null && scannableEntities.Count() > 0 && definitionEntities != null && UTIL.GlobalApp.Authentication_Done)
            {                
                if (control != null && (control.TagName.ToLower() == "input"|| e.Element.TagName=="button"))
                {
                    MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => Convert.ToString(x.pmsformid) == e.Driver.Url && (control.GetAttribute("id") == e.Driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)).GetAttribute("id")) || control.GetAttribute("name") == e.Driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)).GetAttribute("name")) .FirstOrDefault();
                    if (captureEntity != null)
                    {
                        MappingViewModel mapping = scannableEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        blazorDataFoundSaveEvent(mapping);
                    }

                }
            }
        }
        protected override void OnElementClicked(WebElementEventArgs e)
        {
            //Console.WriteLine(e.Element);
        }
        protected override void OnException(WebDriverExceptionEventArgs e)
        {
            Console.WriteLine(e.ThrownException.Message);
        }
        protected override void OnFindElementCompleted(FindElementEventArgs e)
        {
            //Console.WriteLine(e.Element);
        }
    
        private void firingDriver_ElementClicked(object sender, WebElementEventArgs e)
        {
            //Console.WriteLine(e.Element);
        }
        //private void firingDriver_ElementClicking(object sender, WebElementEventArgs e)
        //{
        //    if (scannableEntities!= null && scannableEntities.Count()> 0 && definitionEntities != null && UTIL.GlobalApp.Authentication_Done )
        //    { 
        //    IWebElement elm = (IWebElement)e;
        //    if (elm != null && elm.TagName.ToLower() == "input") {
        //        MappingDefinitionViewModel  captureEntity= definitionEntities.Where(x => x.pmsformid.ToLower() == e.Driver.Url.ToLower() && x.SubmitCaptureFieldId== e.Element.GetAttribute("id") || x.SubmitCaptureFieldXpath == e.Element.GetAttribute("name")).FirstOrDefault();
        //        if (captureEntity != null) {
        //         MappingViewModel mapping= scannableEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
        //                blazorDataFoundSaveEvent(mapping);
        //            }

        //    }
        //    }
        //    //switch (elm)
        //    //{
        //    //    case "submit":
        //    //        if (blazorDataFoundSaveEvent != null)
        //    //        {
        //    //            blazorDataFoundSaveEvent(new MappingViewModel());
        //    //        }
        //    //       // scannableEntities
        //    //        break;
        //    //    case "cancel":
        //    //        break;
        //    //    default:
        //    //        break;

        //    //}
        //    // Console.WriteLine(e.Element);
        //}

        protected override void OnNavigating(WebDriverNavigationEventArgs e)
        {
            //IJavaScriptExecutor javascriptDriver = driver as IJavaScriptExecutor;
            //if (javascriptDriver == null)
            //{
            //    throw new NotSupportedException("Underlying driver instance does not support executing javascript");
            //}

            //object[] unwrappedArgs = UnwrapElementArguments(e);
            //WebDriverScriptEventArgs eScript = new WebDriverScriptEventArgs(this.driver, script);
            //this.OnScriptExecuting(eScript);
            //object scriptResult = javascriptDriver.ExecuteScript(script, unwrappedArgs);
            //this.OnScriptExecuted(eScript);
            //return scriptResult;
        }
        //private void OnScriptExecuting(Object sender, System.EventArgs e)
        //{
        //    //IJavaScriptExecutor javascriptDriver = driver as IJavaScriptExecutor;
        //    //if (javascriptDriver == null)
        //    //{
        //    //    throw new NotSupportedException("Underlying driver instance does not support executing javascript");
        //    //}

        //    //object[] unwrappedArgs = UnwrapElementArguments(e);
        //    //WebDriverScriptEventArgs eScript = new WebDriverScriptEventArgs(this.driver, script);
        //    //this.OnScriptExecuting(eScript);
        //    //object scriptResult = javascriptDriver.ExecuteScript(script, unwrappedArgs);
        //    //this.OnScriptExecuted(eScript);
        //    //return scriptResult;
        //}


        //private void firingDriver_ScriptExecution(string script)
        //{
        //    //switch (e.GetType().FullName)
        //    //{
        //    //    case "submit":
        //    //        break;
        //    //    case "cancel":
        //    //        break;
        //    //    default:
        //    //        break;

        //    //}
        //    // Console.WriteLine(e.Element);
        //}
        ////Delegate override OnScriptExecuting(object sender, WebDriverScriptEventArgs e)
        ////{
        ////   // Console.WriteLine(e.Element);
        ////}

        //private void firingDriver_FindElementCompleted(object sender, FindElementEventArgs e)
        //{
        //    Console.WriteLine(e.FindMethod);
        //}


    }
}
