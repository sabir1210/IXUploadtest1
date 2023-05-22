using System.Linq;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using BDU.ViewModels;
using static BDU.UTIL.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using NLog;
using OpenQA.Selenium.Interactions;
using BDU.UTIL;
using BDU.Services;
using WebDriverManager;
using OpenQA.Selenium.Chrome;

namespace BDU.RobotWeb
{
    public class WebSession
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        public string ApplicationUrl = "";
        public string pmsReferenceExpression = "";
        public IWebDriver driver = new ChromeDriver(@"C:\movies\aii\BDM Core\Drivers");
        public BDUService _bduservice;
        public List<MappingViewModel> scanningEntities = null;
        public string referenceValue = string.Empty;
        public List<EntityFieldViewModel> erroneousLs = null;
        public List<EntityFieldViewModel> warningLs = null;
        public MappingViewModel _access = null;
        public List<EntityFieldViewModel> _fields { get; set; }
       

        public WebSession(WebBrowser browser)
        {
            try
            {
                if (driver == null)

                    //IWebDriver webdriver = new ChromeDriver(@"C:\movies\aii\BDM Core\Drivers");
                driver = WebDriverFactory.CreateWebDriver(browser);
                // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(90);
                // Temporarily
                // driver.Navigate().GoToUrl("http://localhost:8666/index.html");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }

        ~WebSession()
        {
            if (driver != null)
                driver.Quit();
        }
        public bool StartServer()
        {
            try
            {
                _log.Info("Web UI Automation engine started.");
                if (driver != null)
                {
                    driver = WebDriverFactory.CreateWebDriver(WebBrowser.Chrome);
                    // driver.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(90);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                return false;
            }
            return true;
        }
        public bool StopServer()
        {
            var stopped = true;
            try
            {
                _log.Info("process kill requested");
                Process[] proc = Process.GetProcessesByName("chromedriver.exe");
                proc[0].Kill();
                if (driver != null)
                {
                    driver.Quit();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                stopped = false;
            }
            return stopped;
        }
        public string LoadDataFromWeb(string identifier, int findOption = 1)
        {
            IWebElement element = null;
            _log.Info("Loading data from web PMS started..");
            switch (findOption)
            {
                case 1: // Input
                    element = this.driver.FindElement(By.Id(identifier));
                    return element.Text;
                //  break;
                case 2:// Select
                    IWebElement select = this.driver.FindElement(By.Name(identifier));
                    SelectElement selectctrl = new SelectElement(select);
                    element = selectctrl.SelectedOption;
                    return element.Text;
                //   break;
                case 3:// DTP
                    element = this.driver.FindElement(By.ClassName(identifier));
                    return element.Text;
                //  break;
                case 4:
                    element = this.driver.FindElement(By.CssSelector(identifier));
                    return element.Text;
                //  break;
                case 5:
                    element = this.driver.FindElement(By.XPath(identifier));
                    return element.Text;
                // break;
                default:
                    element = this.driver.FindElement(By.Id(identifier));
                    return element.Text;
                    // break;
            }



        }
        private void prepareFieldsCollection(MappingViewModel data, bool isFeed = false)
        {
            try
            {
                _fields = (from f in data.forms
                           from flds in f.fields
                           where flds.status == (int)UTIL.Enums.STATUSES.Active && flds.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL
                           select new EntityFieldViewModel
                           {
                               control_type = flds.control_type,
                               data_type = flds.data_type,
                               // id = flds.id,
                               fuid = flds.fuid,
                               parent_field_id = flds.parent_field_id,
                               pms_field_expression = flds.pms_field_expression,
                               pms_field_name = flds.pms_field_name,
                               pms_field_xpath = flds.pms_field_xpath,
                               entity_id = flds.entity_id,
                               custom_tag = flds.custom_tag,
                               sr = flds.sr,
                               status = flds.status,// = flds.save_status,
                               value = flds.value,
                               mandatory = flds.mandatory,
                               is_reference = flds.is_reference,
                               is_unmapped = flds.is_unmapped,
                               default_value = flds.default_value
                           }).ToList();
                // Process data for billing enable disable based on expression
                if (isFeed && data.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS)
                {
                    List<EntityFieldViewModel> fList = new List<EntityFieldViewModel>();
                    foreach (FormViewModel frm in data.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active)) //(x => x.status == (int)UTIL.Enums.STATUSES.InActive && !string.IsNullOrWhiteSpace(x.custom_tag) && ("" + x.custom_tag).Contains(BDUConstants.CUSTOM_TAG_EBILLS_KEYWORD_ROOT)))
                    {
                        fList.AddRange(frm.fields);
                    }
                    if (fList != null && fList.Any())
                    {
                        // make all default disable
                        foreach (EntityFieldViewModel fld in fList.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(x.custom_tag) && ("" + x.custom_tag).Length > 2))
                        {
                            fld.status = (int)UTIL.Enums.STATUSES.InActive;
                        }
                        //  Enable billing payment if there is some amoutn to be added
                        List<EntityFieldViewModel> pFlds = fList.Where(x => x.fuid == BDUConstants.PAYMENT_SERVICES_BILL_PAYMENT && !string.IsNullOrWhiteSpace(x.custom_tag) && !string.IsNullOrWhiteSpace(x.value) && x.value != "0").ToList();
                        if (pFlds.Any())
                        {
                            foreach (EntityFieldViewModel pItemfld in fList.Where(x => x.status == (int)UTIL.Enums.STATUSES.InActive && !string.IsNullOrWhiteSpace(x.custom_tag) && x.custom_tag.Contains(BDUConstants.CUSTOM_TAG_PBILL_KEYWORD_ROOT)))
                            {
                                pItemfld.status = (int)UTIL.Enums.STATUSES.Active;
                            }

                        }//  if (pFlds.Any()) {
                        double additionalServiceAmount = 0.0;
                        string additionalServiceDesc = string.Empty;
                        // For Addition Services
                        List<EntityFieldViewModel> aFlds = fList.Where(x => BDUConstants.PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES.Contains(x.fuid) && !string.IsNullOrWhiteSpace(x.custom_tag) && !string.IsNullOrWhiteSpace(x.value) && x.value != "0").ToList();
                        if (aFlds.Any())
                        {
                            foreach (EntityFieldViewModel pItemfld in fList.Where(x => !string.IsNullOrWhiteSpace(x.custom_tag) && ("" + x.custom_tag).Contains(BDUConstants.CUSTOM_TAG_EBILLS_KEYWORD_ROOT)))
                            {
                                double n = 0;
                                pItemfld.status = (int)UTIL.Enums.STATUSES.Active;
                                if (!string.IsNullOrWhiteSpace(pItemfld.value) && Double.TryParse(pItemfld.value, out n))
                                {                                    
                                    additionalServiceAmount += Convert.ToDouble(pItemfld.value);
                                    additionalServiceDesc = string.IsNullOrWhiteSpace(additionalServiceDesc) ? string.Format("{0}:{1}", String.Concat("" + pItemfld.field_desc, "", " with Taxes") , pItemfld.value) : additionalServiceDesc + "," + string.Format(" {0}:{1}", String.Concat("" + pItemfld.field_desc, "", " with Taxes"), pItemfld.value);
                                }
                            }
                            // Get all payments and concatenat sum & description need to append with all
                            // Set Grand Sum
                            if (additionalServiceAmount > 0.0)
                            {
                                EntityFieldViewModel additionalServiceAmntFld = fList.Where(t => t.fuid == BDUConstants.ADDITIONAL_SERVICES_AMOUNT && t.entity_id == data.entity_Id).FirstOrDefault();
                                if (additionalServiceAmntFld != null)
                                {
                                    additionalServiceAmntFld.value = Math.Round(additionalServiceAmount, 1).ToString();
                                    additionalServiceAmntFld.status = (int)UTIL.Enums.STATUSES.Active;
                                }
                            }
                            // Additional Description
                            if (!string.IsNullOrWhiteSpace(additionalServiceDesc))
                            {
                                EntityFieldViewModel additionalServiceDescFld = fList.Where(t => t.fuid == BDUConstants.PAYMENT_SERVICES_BILL_PAYMENT_DESC && t.entity_id == data.entity_Id).FirstOrDefault();
                                if (additionalServiceDescFld != null)
                                {
                                    additionalServiceDescFld.value = additionalServiceDesc;
                                    additionalServiceDescFld.status = (int)UTIL.Enums.STATUSES.Active;
                                }
                            }
                        }//  if (pFlds.Any()) {
                    }

                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }

        public void fillKeywordsDictionary(List<EntityFieldViewModel> flds, bool forTestRun = false) // Keyword Fill Up
        {
            try
            {
                if (flds != null)
                {
                    List<EntityFieldViewModel> keyWordFields = flds.Where(x => Convert.ToString(x.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX) && x.control_type != (int)Enums.CONTROL_TYPES.NOCONTROL).ToList();
                    foreach (EntityFieldViewModel fld in keyWordFields)
                    {
                        EntityFieldViewModel keywordFld = null;

                        // REference
                        if (!forTestRun && Convert.ToString(fld.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX))// Not for Test
                        {
                            if (Convert.ToString(fld.default_value).Trim() == BDUConstants.REFERENCE_KEYWORD)
                            {
                                keywordFld = flds.Where(x => Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.REFERENCE && x.is_reference == (int)UTIL.Enums.STATUSES.Active).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value) && keywordFld.value != "0")
                                    {
                                        fld.default_value = keywordFld.value;
                                    }
                                    else if (!string.IsNullOrWhiteSpace(this.referenceValue))
                                        fld.default_value = this.referenceValue;

                                }
                            }
                            // Guest Name
                            else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.GUEST_NAME_KEYWORD) //BDU.UTIL.BDUConstants.GUEST_NAME == fld.field_desc || BDU.UTIL.BDUConstants.NEW_GUEST_NAME == fld.field_desc || BDU.UTIL.BDUConstants.NEW_GUEST_NAME == fld.field_desc)
                            {
                                keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.GUEST_NAME || Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_GUEST_NAME)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                        fld.value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;

                                }
                            }
                            else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.BILL_DESC_KEYWORD)// BDU.UTIL.BDUConstants.BILL_DESCRIPTION_FIELD_DESC.ToLower() == ("" + fld.field_desc).ToLower())
                            {
                                keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.BILL_DESCRIPTION_FIELD_DESC)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                        fld.value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;

                                }
                            }
                            else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.BILL_AMOUNT_KEYWORD)
                            // else if (BDU.UTIL.BDUConstants.BILL_AMOUNT_FIELD_DESC.ToLower() == ("" + fld.field_desc).ToLower())
                            {
                                keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.BILL_AMOUNT_FIELD_DESC)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                        fld.value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;

                                }
                            }
                            // else if (BDU.UTIL.BDUConstants.ROOM_NO == fld.field_desc || BDU.UTIL.BDUConstants.NEW_ROOM_NO == fld.field_desc)
                            else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.ROOM_NO_KEYWORD)
                            {
                                keywordFld = flds.Where(x => x.entity_id == fld.entity_id && Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_ROOM_NO).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                        fld.value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;

                                }
                            }
                            else if (BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower() == ("" + fld.default_value).ToLower())
                            {
                                keywordFld = flds.Where(x => x.is_unmapped == (Int32)UTIL.Enums.STATUSES.InActive && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.GUEST_NAME || Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_GUEST_NAME)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                        fld.value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;
                                }
                            }
                        }
                        else
                        {// for Test                           
                            if (!string.IsNullOrWhiteSpace(fld.default_value) && Convert.ToString(fld.pms_field_expression).Contains(fld.default_value))
                            {
                                if (Convert.ToString(fld.pms_field_expression).Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                                {
                                    string[] keywordKeyValues = fld.pms_field_expression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                                    foreach (string keywordKeyValue in keywordKeyValues)
                                    {
                                        if (!(keywordKeyValue.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_FEED) || keywordKeyValue.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_SCAN)))
                                        {
                                            if (keywordKeyValue.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                                            {
                                                string[] KeyValues = keywordKeyValue.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                                                if (KeyValues.Length > 1)
                                                {
                                                    if (Convert.ToString(KeyValues[0]).ToLower() == fld.default_value.ToLower())
                                                        fld.default_value = KeyValues[1];

                                                }
                                            }
                                        }
                                    }
                                    fld.pms_field_expression = string.Empty;
                                }// Multiple
                                else if (Convert.ToString(fld.pms_field_expression).Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                                {
                                    if (!(fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_FEED) || fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_SCAN)))
                                    {
                                        string[] KeyValues = fld.pms_field_expression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                                        if (KeyValues.Length > 1)
                                        {
                                            if (Convert.ToString(KeyValues[0]).ToLower() == Convert.ToString(fld.default_value).ToLower())
                                            {
                                                fld.default_value = KeyValues[1];
                                                fld.value = string.IsNullOrWhiteSpace(fld.value) ? KeyValues[1] : fld.value;
                                                fld.pms_field_expression = string.Empty;
                                            }
                                        }
                                    }
                                }// SINGLE Case
                            }
                        }
                    }// ForEach
                }// if(flds!= null) { 

            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
        public bool IsNumeric(string input)
        {
            try
            {
                int test;
                return int.TryParse(input, out test);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //ZAHID
        public ResponseViewModel RetrievalFormDataFromWeb(MappingViewModel fillableEntity)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {

                _log.Info(string.Format("Data retreival process started, at {0}", System.DateTime.UtcNow.ToUniversalTime()));
                if (driver != null)
                {
                    IWebElement element = null;
                    string formName = fillableEntity.entity_name;
                    //******************* Fill Fields Collection
                    this.referenceValue = string.Empty;
                    // driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
                    this.prepareFieldsCollection(fillableEntity);
                    IJavaScriptExecutor jsi = (IJavaScriptExecutor)driver;
                    // Get All fields from 
                    foreach (FormViewModel frm in fillableEntity.forms)
                    {
                        fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE).ToList(), false);
                        DateTime lastProcesstime = DateTime.Now;
                        // List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && (string.IsNullOrWhiteSpace(x.pms_field_expression) || !Convert.ToString(x.pms_field_expression).Contains(UTIL.BDUConstants.SCAN_NOT_REQUIRED))).OrderBy(X => X.sr).ToList();
                        List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && (x.scan == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED)).OrderBy(X => X.sr).ToList();
                        foreach (EntityFieldViewModel field in candidateFlds)
                        {
                            lastProcesstime = DateTime.Now;
                            // this.FindWebElement(field, withFillOption); 
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && (field.fuid == GlobalApp.Hotel_id_GUID_CheckOut_Status || field.fuid == GlobalApp.Hotel_id_GUID_CheckIn_Status || field.fuid == GlobalApp.Hotel_id_GUID_Reservation_Status || field.fuid == GlobalApp.Hotel_id_GUID_Billing_Status))
                            {
                                //IWait<IWebDriver> waitA = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                                //waitA.Until(ExpectedConditions.ElementSelectionStateToBe(driver.FindElement(By.XPath(field.pms_field_xpath))));
                                System.Threading.Thread.Sleep(800);
                                string entityId = Convert.ToString(jsi.ExecuteScript(field.pms_field_expression));
                                if (!string.IsNullOrWhiteSpace(entityId) && this.IsNumeric(entityId) && entityId != field.entity_id.ToString())
                                {
                                    //  fillableEntity.entity_Id = Convert.ToInt32(entityId);
                                    MappingViewModel mapping = scanningEntities.Where(x => x.entity_Id == Convert.ToInt32(entityId)).FirstOrDefault();
                                    if (mapping != null)
                                    {
                                        fillableEntity = mapping.DCopy();
                                        this.RetrievalFormDataFromWeb(fillableEntity);
                                    }
                                }  // if (!string.IsNullOrWhiteSpace(entityId)  && entityId!= field.entity_id.ToString()) {
                            }
                            switch (field.control_type)
                            {
                                case (int)CONTROL_TYPES.TEL:
                                case (int)CONTROL_TYPES.TEXTBOX: // Input
                                case (int)CONTROL_TYPES.INCREMENT:
                                    if (!string.IsNullOrWhiteSpace(field.pms_field_name) || !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                                        element = FindFetchWebElement(field);
                                    //if(field.is_reference==1)
                                    //System.Threading.Thread.Sleep(2000);
                                    //IWait<IWebDriver> waitR = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                                    //waitR.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                    if (element != null && (!string.IsNullOrWhiteSpace(element.Text) || !string.IsNullOrWhiteSpace(element.GetAttribute("value"))))
                                        field.value = string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text;
                                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                                    {
                                        IWait<IWebDriver> waitC = new WebDriverWait(driver, TimeSpan.FromSeconds(80.00));
                                        waitC.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                        //IJavaScriptExecutor jsi = (IJavaScriptExecutor)driver; 
                                        string expressionValue = Convert.ToString(jsi.ExecuteScript(field.pms_field_expression));
                                        field.value = expressionValue;
                                    };
                                    break;
                                case (int)CONTROL_TYPES.HIDDEN:// "text": // Input
                                    if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.GET_INPUT_FROM_JSEXPRESSION))
                                    {
                                        field.value = Convert.ToString(jsi.ExecuteScript(field.pms_field_expression));
                                    }
                                    else
                                    {
                                        element = FindFetchWebElement(field);
                                        if (element != null && (!string.IsNullOrWhiteSpace(element.Text) || !string.IsNullOrWhiteSpace(element.GetAttribute("value"))))
                                            field.value = string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text;
                                    }
                                    break;
                                case (int)CONTROL_TYPES.DATE:// "text": // Input                                
                                    element = FindFetchWebElement(field);



                                    //var testchk0 = element.Text;
                                    //var testchk = DateTime.Parse(element.Text.ToString());
                                    //var testchk2 = DateTime.Parse(element.Text.ToString()).ToString("MM.dd.yyyy");
                                    //var testchk1 = element.GetAttribute("value");
                                    //System.DateTime  date = DateTime.Parse(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text);

                                    if (element != null && (!string.IsNullOrWhiteSpace(element.Text) || !string.IsNullOrWhiteSpace(element.GetAttribute("value"))))
                                    {
                                        try
                                        {
                                            //var sub = element.Text;
                                            //var sub1 = element.GetAttribute("value");
                                            var testchk0 = element.Text;
                                            if(testchk0 != null)
                                            {
                                               // var testchk = DateTime.Parse(element.Text.ToString());
                                                var testchk2 = DateTime.Parse(element.Text.ToString()).ToString("MM.dd.yyyy");

                                                System.DateTime date = DateTime.ParseExact(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : testchk2.ToString(), field.format, System.Globalization.CultureInfo.InvariantCulture);
                                                field.value = date.ToString(UTIL.GlobalApp.date_format);

                                            }
                                            else
                                            {
                                                System.DateTime date = DateTime.ParseExact(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text, field.format, System.Globalization.CultureInfo.InvariantCulture);
                                                field.value = date.ToString(UTIL.GlobalApp.date_format);
                                            }

                                            //System.DateTime  date = DateTime.Parse(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text);
                                            //System.DateTime date = DateTime.ParseExact(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text, field.format, System.Globalization.CultureInfo.InvariantCulture);
                                            //field.value = date.ToString(UTIL.GlobalApp.date_format);

                                            //Noman changes
                                     



                                        }
                                        catch (Exception ex)
                                        {
                                            field.value = element.GetAttribute("value");
                                        }
                                    }
                                    break;
                                case (int)CONTROL_TYPES.DATETIME:// "text": // Input                                
                                    element = FindFetchWebElement(field);
                                    //  var dateess = field.value;
                                    
                                    //field.value(Convert.ToDateTime(element.GetAttribute("value")));

                                    if (element != null && (!string.IsNullOrWhiteSpace(element.Text) || !string.IsNullOrWhiteSpace(element.GetAttribute("value"))))
                                    {
                                        try
                                        {
                                            System.DateTime date = DateTime.ParseExact(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text, field.format, System.Globalization.CultureInfo.InvariantCulture);
                                            field.value = date.ToString(UTIL.GlobalApp.date_time_format);
                                        }
                                        catch (Exception ex)
                                        {
                                            field.value = element.GetAttribute("value");
                                        }
                                    }
                                    break;
                                case (int)CONTROL_TYPES.CHECKBOX:// "text": // Input
                                    element = FindFetchWebElement(field);
                                    if (element != null && !string.IsNullOrWhiteSpace(element.Text))
                                        field.value = element.Selected.ToString();
                                    break;
                                case (int)CONTROL_TYPES.SELECT:// "select":// Select                                                            
                                    element = FindFetchWebElement(field);
                                    try
                                    {
                                        if (element != null && !string.IsNullOrWhiteSpace(element.GetAttribute("value")))//&& !string.IsNullOrWhiteSpace(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text) && (string.IsNullOrWhiteSpace(string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text)).ToString().ToLower().Contains("select)")
                                        {
                                            string selectOption = string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text;
                                            if (!string.IsNullOrWhiteSpace(selectOption))
                                            {
                                                SelectElement selectCtrl = new SelectElement(element);
                                                if (selectCtrl != null && selectCtrl.AllSelectedOptions.Count > 0)
                                                {
                                                    field.value = selectCtrl.SelectedOption.Text;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex) { }//
                                    break;
                                case (int)CONTROL_TYPES.LISTBOX:// "select":// Select
                                                                //element = FindWebElement(field);
                                    element = FindFetchWebElement(field);
                                    try
                                    {

                                        if (element != null && !string.IsNullOrWhiteSpace(element.GetAttribute("value")))
                                        {
                                            SelectElement selectCtrl = new SelectElement(element);
                                            field.value = selectCtrl.SelectedOption.Text;
                                        }
                                    }
                                    catch (Exception ex) { }//
                                    break;
                                case (int)CONTROL_TYPES.RADIO:// "select":// Select
                                                              //element = FindWebElement(field);
                                    element = FindFetchWebElement(field);
                                    try
                                    {
                                        if (element != null && !string.IsNullOrWhiteSpace(element.GetAttribute("value")))
                                        {
                                            SelectElement rdo = new SelectElement(element);
                                            field.value = rdo.SelectedOption.Text;
                                            // field.value = element.Text;
                                        }
                                    }
                                    catch (Exception ex) { }//
                                    break;
                                case (int)CONTROL_TYPES.RADIO_GROUP:// RQDIO
                                                                    //element = FindWebElement(field);
                                    element = FindFetchWebElement(field);
                                    if (element != null)
                                    {
                                        IList<IWebElement> rdos = driver.FindElements(By.Name(field.pms_field_name));
                                        IWebElement selElement = rdos.Where(x => x.Selected).FirstOrDefault();
                                        field.value = selElement.Text;
                                    }
                                    break;

                                case (int)CONTROL_TYPES.ACTION:// Select
                                    element = FindFetchWebElement(field);
                                    // element = driver.FindElement(By.Id(field.pms_field_name));
                                    if (element != null)
                                    {
                                        element.Click();
                                        if (element.TagName != "span")
                                            element.Submit();
                                    }
                                    break;
                                case (int)CONTROL_TYPES.BUTTON:
                                case (int)CONTROL_TYPES.ANCHOR:// Select
                                    try
                                    {
                                        element = FindFetchWebElement(field);
                                        // element = driver.FindElement(By.Id(field.pms_field_name));
                                        if (element != null)
                                        {

                                            element.Click();
                                            IWait<IWebDriver> waitA = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                                            waitA.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                            //element.Submit();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _log.Error(ex);
                                    }
                                    break;
                                case (int)CONTROL_TYPES.NOCONTROL:// Select
                                    if (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.GET_INPUT_FROM_JSEXPRESSION && !string.IsNullOrWhiteSpace(field.pms_field_expression))
                                    {

                                        IWait<IWebDriver> waitC = new WebDriverWait(driver, TimeSpan.FromSeconds(80.00));
                                        waitC.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                        //IJavaScriptExecutor jsi = (IJavaScriptExecutor)driver; 
                                        string expressionValue = Convert.ToString(jsi.ExecuteScript(field.pms_field_expression));
                                        field.value = expressionValue;
                                        if (field.is_reference == (int)UTIL.Enums.STATUSES.Active)
                                        {
                                            if (string.IsNullOrWhiteSpace(expressionValue) || expressionValue.Contains("new"))
                                            {
                                                // System.Threading.Thread.Sleep(2000);
                                                //IJavaScriptExecutor jsi = (IJavaScriptExecutor)driver;
                                                expressionValue = Convert.ToString(jsi.ExecuteScript(field.pms_field_expression));
                                                field.value = expressionValue;
                                                referenceValue = expressionValue;
                                                fillableEntity.reference = referenceValue;
                                            }
                                            else
                                            {
                                                field.value = expressionValue;
                                                referenceValue = expressionValue;
                                                fillableEntity.reference = referenceValue;
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    element = FindFetchWebElement(field);
                                    if (element != null && (field.control_type != (int)UTIL.Enums.CONTROL_TYPES.FRAME && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.URL && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE))
                                    {
                                        if (!string.IsNullOrEmpty(element.Text) || !string.IsNullOrEmpty(element.GetAttribute("value")))
                                            field.value = string.IsNullOrWhiteSpace(element.Text) ? element.GetAttribute("value") : element.Text;
                                    }
                                    break;
                            }
                            this.driver.SwitchTo().ActiveElement();
                            if (field.is_reference == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY && !string.IsNullOrWhiteSpace(field.value))
                            {
                                referenceValue = field.value.Trim();
                                fillableEntity.reference = referenceValue;
                            }

                            TimeSpan ts = DateTime.Now - lastProcesstime;
                            if (ts.TotalMilliseconds < 500)
                                System.Threading.Thread.Sleep(500);
                        }

                    }
                    if (string.IsNullOrWhiteSpace(referenceValue) || referenceValue.Contains("new"))
                    {

                        if (!string.IsNullOrWhiteSpace(this.pmsReferenceExpression) || referenceValue.Contains("new"))
                        {
                            System.Threading.Thread.Sleep(2000);
                            //IJavaScriptExecutor jsi = (IJavaScriptExecutor)driver;
                            fillableEntity.reference = Convert.ToString(jsi.ExecuteScript(this.pmsReferenceExpression));
                            referenceValue = fillableEntity.reference;
                        }
                        
                        if (string.IsNullOrWhiteSpace(referenceValue) && (fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKIN || fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS || fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKOUT))
                        {
                            //Random generator = new Random();
                            //String rndomn = generator.Next(0, 100000000).ToString("D8");
                            //fillableEntity.reference = string.IsNullOrWhiteSpace(fillableEntity.reference) ? rndomn : fillableEntity.reference;
                            foreach (ViewModels.FormViewModel frm in fillableEntity.forms.Where(x => x.Status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                            {
                                foreach (ViewModels.EntityFieldViewModel fld in frm.fields.Where(x => x.status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED && x.scan == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                                {
                                    if (fld.field_desc.ToLower().Contains("room number") && !string.IsNullOrWhiteSpace(fld.value) && !fld.value.Contains(BDUConstants.SPECIAL_KEYWORD_PREFIX))
                                    {
                                        fillableEntity.reference = fld.value;
                                        fillableEntity.roomno = fld.value;
                                        referenceValue = fld.value;
                                        break;
                                    }
                                }// Inner Each
                                if (!string.IsNullOrWhiteSpace(fillableEntity.roomno) && !string.IsNullOrWhiteSpace(fillableEntity.reference))
                                    break;
                            }// Outer Each  
                        }//
                    }
                    rs.jsonData = fillableEntity;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Retrieval of form data completed.";
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = ex.Message;
            }
            return rs;



        }
        public ResponseViewModel FeedDataToWebForm(MappingViewModel mapping)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                _log.Info("Sync to PMS started..");
                if (driver != null)
                {
                    MappingViewModel model = mapping.DCopy();
                    IWebElement element = null;
                    string formName = model.entity_name;
                    //******************* Fill Fields Collection
                    // driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
                    List<MappingViewModel> lsmapping = new List<MappingViewModel>();
                    lsmapping.Add(model);
                    this.prepareFieldsCollection(model, true);
                    ResponseViewModel res = TestRunMapping(lsmapping);

                    // Form Collection can be
                    if (res.status)
                    {
                        //foreach (FormViewModel frm in model.forms)
                        //{
                        //    foreach (EntityFieldViewModel field in frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(x => x.sr))
                        //    {
                        //        //switch (field.control_type)
                        //        //{

                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX:
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.TEL: // Input                          
                        //        //        element = this.FindAndFillWebElement(field, true);
                        //        //        if (element != null)
                        //        //            element.SendKeys("" + (Convert.ToInt16(field.mandatory) > 0 && string.IsNullOrWhiteSpace(field.value) ? field.default_value : field.value));
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.DATE: // "date": // Input
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //            element.SendKeys("" + (Convert.ToInt16(field.mandatory) > 0 && string.IsNullOrWhiteSpace(field.value) ? field.default_value : field.value));
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.DATETIME:
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //            element.SendKeys("" + (Convert.ToInt16(field.mandatory) > 0 && string.IsNullOrWhiteSpace(field.value) ? field.default_value : field.value));
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.RADIO_GROUP:
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //        {
                        //        //            SelectElement select = new SelectElement(element);
                        //        //            select.SelectByValue(field.value);
                        //        //        }
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.RADIO:
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //        {
                        //        //            SelectElement select = new SelectElement(element);
                        //        //            select.SelectByValue(field.value);
                        //        //        }
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.LISTBOX:
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //        {
                        //        //            SelectElement select = new SelectElement(element);
                        //        //            select.SelectByText(field.value);
                        //        //        }
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.SELECT:
                        //        //        element = this.FindWebElement(field);
                        //        //        if (element != null)
                        //        //        {
                        //        //            SelectElement select = new SelectElement(element);
                        //        //            select.SelectByText(field.value);
                        //        //        }
                        //        //        break;
                        //        //    case (int)UTIL.Enums.CONTROL_TYPES.ACTION:// Select
                        //        //        element = this.FindWebElement(field);
                        //        //        //  element = driver.FindElement(By.Id(field.pms_field_name));
                        //        //        if (element != null)
                        //        //        {
                        //        //            element.Click();
                        //        //            element.Submit();
                        //        //        }
                        //        //        break;
                        //        //    default:
                        //        //        element = this.FindWebElement(field);
                        //        //        // element = this.driver.FindElement(By.Id(field.pms_field_name));
                        //        //        if (element != null)
                        //        //            element.SendKeys(field.value);
                        //        //        break;
                        //        //}
                        //    }
                        //}
                        rs.jsonData = model;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Successfully form filled.";
                    }//if (res.status) {
                    else
                    {
                        rs.jsonData = model;
                        rs.status = false;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                        rs.message = res.message;
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = ex.Message;
            }
            return rs;

        }
        private bool feedElement(EntityFieldViewModel field, IWebElement element)
        {

            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                // 
                string fieldValue = field.mandatory > 0 && string.IsNullOrWhiteSpace(field.value) ? "" + field.default_value : "" + field.value;
                switch (field.control_type)
                {

                    case (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX:
                        if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT && !string.IsNullOrWhiteSpace(field.pms_field_expression) && !field.pms_field_expression.Contains(BDUConstants.SPECIAL_KEYWORD_PREFIX))
                        {
                            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript(field.pms_field_expression).Equals("true"));
                            IWait<IWebDriver> waitC = new WebDriverWait(driver, TimeSpan.FromSeconds(5.00));
                            waitC.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.Manual_Input)
                        {
                            // 
                            // driver.FindElement(By.TagName("body")).SendKeys("Keys.ESCAPE");
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.stop();");
                            // ((IJavaScriptExecutor)driver).ExecuteScript("window.stop();");
                            // System.Threading.Thread.Sleep(3000);
                            if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(field.pms_field_xpath)));
                            }
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT)
                        {
                            System.Threading.Thread.Sleep(50);
                            element.SendKeys(Keys.Control + "a");     //select all text in textbox                                             
                            element.SendKeys(fieldValue);
                            IWait<IWebDriver> waitC = new WebDriverWait(driver, TimeSpan.FromSeconds(5.00));
                            waitC.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            System.Threading.Thread.Sleep(50);
                            // element.SendKeys(Keys.Enter);
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_CONFIRM)
                        {
                            element.SendKeys(Keys.Control + "a");     //select all text in textbox                                             
                            element.SendKeys(fieldValue);
                            System.Threading.Thread.Sleep(100);
                            element.SendKeys(Keys.Enter);
                            System.Threading.Thread.Sleep(200);
                            // element.SendKeys(Keys.Enter);
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SHORTCUT_KEY)
                        {
                            if (!string.IsNullOrWhiteSpace(field.default_value))
                            {
                                Actions actns = new Actions(driver);
                                actns.SendKeys(element, field.default_value);
                                System.Threading.Thread.Sleep(50);
                            }
                            
                            // element.SendKeys(Keys.Enter);
                        }
                        else if (element != null && !string.IsNullOrWhiteSpace(fieldValue))
                        {
                            // System.Threading.Thread.Sleep(50);
                            IWait<IWebDriver> waitT = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitT.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            element.SendKeys(Keys.Control + "a");     //select all text in textbox                                             
                            element.SendKeys(fieldValue);
                            if (!string.IsNullOrWhiteSpace(fieldValue))
                            {
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitRG.Until(ExpectedConditions.TextToBePresentInElementValue(element, fieldValue));
                            }
                            // element.SendKeys(Keys.Enter);
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.TEL: // Input                          
                                                            // element = this.FindWebElement(field);
                        if (element != null && !string.IsNullOrWhiteSpace(fieldValue))
                        {
                            element.SendKeys(Keys.Control + "a");     //select all text in textbox                                               
                            element.SendKeys(fieldValue);
                            IWait<IWebDriver> waitT = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitT.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        //element.SendKeys(Keys.Enter);
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.GRID: // Input                          
                                                             // element = this.FindWebElement(field);
                        if (element != null)
                        {
                            //  IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                            // executor.ExecuteScript("arguments[0].click();", element);
                            Actions gRow = new Actions(driver);

                            //Double click on element
                            //IWebElement ele = driver.findElement(By.xpath("XPath of the element"));
                            gRow.DoubleClick(element).Perform();
                            //Actions actions = new Actions(driver);
                            //actions.MoveToElement(element).Perform();

                            //// wait for the element to be visible before clicking on it
                            //WebDriverWait wait = new WebDriverWait(driver, 10);
                            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element)).click();
                            //element.SendKeys(Keys.Control + "a");
                            //element.SendKeys(Keys.Enter);
                            //element.Click();
                            IWait<IWebDriver> waitT = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                            waitT.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        //element.SendKeys(Keys.Enter);
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.DATE: // "date": // Input
                                                             // element = this.FindWebElement(field);
                        if (element != null)
                        {
                            System.Threading.Thread.Sleep(300);
                            string dt = string.Empty;
                            if (string.IsNullOrWhiteSpace(fieldValue) && Convert.ToString(field.default_value).ToLower().Contains("d"))
                                dt = ProduceValueFromDateExpression(field);
                            else
                                dt = Convert.ToDateTime(fieldValue).ToString(field.format);

                            Actions gRow = new Actions(driver);
                            gRow.DoubleClick(element).Perform();
                            WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                            waitP.Until(ExpectedConditions.ElementToBeClickable(element));
                            element.SendKeys(Keys.Control + "a");     //select all text in textbox
                                                                      // element.SendKeys.chord(Keys.Control + "a");     //select all text in textbox
                            System.Threading.Thread.Sleep(500);
                            IWait<IWebDriver> waitT = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                            waitT.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            element.SendKeys(dt.ToString());

                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.DATETIME:
                        //  element = this.FindWebElement(field);
                        if (element != null)
                        {
                            System.Threading.Thread.Sleep(300);

                            WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitP.Until(ExpectedConditions.ElementToBeSelected(element));
                            DateTime dt = System.DateTime.Now;
                            if (string.IsNullOrWhiteSpace(fieldValue) && Convert.ToString(field.default_value).ToLower().Contains("d"))
                            {
                                // string dt = ProduceValueFromDateExpression(field);
                                element.SendKeys(Keys.Control + "a");
                                element.SendKeys(ProduceValueFromDateExpression(field));
                            }
                            else
                            {
                                dt = Convert.ToDateTime(fieldValue).ToLocalTime();
                                //  = Convert.ToDateTime(fieldValue).ToLocalTime();
                                // var date = js.ExecuteScript("return (new Date(" + dt.Year + "-" + (dt.Month + 1) + "-" + dt.Date + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second + ")).toLocaleDateString()");
                                element.SendKeys(Keys.Control + "a");     //select all text in textbox
                                                                          // element.SendKeys(Keys.Backspace); //delete it                       
                                element.SendKeys(dt.Year + "-" + (dt.Month + 1) + "-" + dt.Date + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second);
                            }
                            //IWait<IWebDriver> waitDT = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(3));
                            //waitDT.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.CHECKBOX:
                        // element = this.FindWebElement(field);
                        if (element != null)
                        {
                            element.Click();
                            // SelectElement select = new SelectElement(element);
                            element.SendKeys(fieldValue);
                            IWait<IWebDriver> waitCB = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(15));
                            waitCB.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.RADIO_GROUP:
                        // element = this.FindWebElement(field);
                        if (element != null)
                        {
                            // SelectElement select = new SelectElement(element);
                            element.Click();
                            IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitRG.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.BUTTON:
                        if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {
                            try
                            {
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitRG.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                                // element.Submit();

                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                waitRG.Until(ExpectedConditions.ElementToBeClickable(element));
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                    IWait<IWebDriver> waitBS = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                    // waitBS.Until(ExpectedConditions.AlertIsPresent());

                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {                         

                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CANCEL)
                        {
                            IWait<IWebDriver> waita = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                            waita.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.RIGHT_CLICK)
                        {
                            IWait<IWebDriver> waita = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                            waita.Until(ExpectedConditions.ElementToBeClickable(element));
                            Actions actns = new Actions(driver);
                            actns.ContextClick(element).Click();
                        }                    

                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT)
                        {
                            try
                            {
                                IWait<IWebDriver> waita = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                waita.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            }
                            catch (Exception ex)
                            {
                                IWait<IWebDriver> waita = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                waita.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.XPath(field.pms_field_xpath)))).Click();
                                // driver.FindElement(By.XPath(field.pms_field_xpath))
                            }
                            System.Threading.Thread.Sleep(800);

                        }
                        else if (element != null)
                        {
                            try
                            {
                                // element.Click();
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                waitRG.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {                          

                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.URL:
                        // element = this.FindWebElement(field);
                        if (element != null && !driver.Url.ToLower().Contains(fieldValue.ToLower()) && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.LOAD)
                        {

                            try
                            {
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitRG.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                driver.Navigate().GoToUrl(fieldValue);
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {     
                        }
                        else if (element != null && !driver.Url.ToLower().Contains(fieldValue.ToLower()) && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.FETCH)
                        {
                            try
                            {
                                IWait<IWebDriver> waitRG = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                                waitRG.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                driver.Navigate().GoToUrl(fieldValue);
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(15.00));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {   
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CANCEL)
                        {
                            try
                            {
                                driver.Navigate().GoToUrl(fieldValue);
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {   
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.RADIO:
                        // element = this.FindWebElement(field);
                        if (element != null)
                        {
                            SelectElement select = new SelectElement(element);
                            select.SelectByValue(fieldValue);
                            IWait<IWebDriver> waitRD = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
                            waitRD.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.LISTBOX:
                        element = this.FindWebElement(field);
                        if (element != null && string.IsNullOrWhiteSpace(fieldValue))
                        {
                            SelectElement select = new SelectElement(element);

                            string[] options = fieldValue.Split(",");
                            if (options != null && options.Length > 0)
                            {
                                foreach (string option in options)
                                {
                                    select.SelectByText(option);
                                    element.SendKeys(Keys.Enter);
                                    IWait<IWebDriver> waitLB = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                    waitLB.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                                }
                            }
                            else
                            {
                                select.SelectByText(fieldValue);
                                IWait<IWebDriver> waitRVYV = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitRVYV.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                        }

                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.ANCHOR:
                        // element = this.FindWebElement(field);
                        if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {

                            try
                            {
                                element.Submit();
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {   
                            //IWait<IWebDriver> waitBS = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(60));
                            //waitBS.Until(ExpectedConditions.AlertIsPresent());
                        }
                        else if (element != null && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.LOAD)
                        {
                            try
                            {
                                element.Click();
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try { 

                        }
                        else if (element != null)
                        {
                            try
                            {
                                // ANCHOR
                                element.Click();
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                                waitCNL.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();
                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try { 

                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.SELECT:
                        System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                        if (element != null && !string.IsNullOrWhiteSpace(fieldValue))
                        {
                            SelectElement select = new SelectElement(element);
                            IWebElement option = null;
                            try
                            {
                                option = select.Options.Where(x => x.Text.Contains(fieldValue)).FirstOrDefault();
                            }
                            catch (Exception ex)
                            {
                                // TODO
                            }
                            if (option != null)
                                select.SelectByText(option.Text);
                            else
                                select.SelectByText(fieldValue);
                        }
                        break;
                    case (int)UTIL.Enums.CONTROL_TYPES.ACTION:// Select
                                                              //  element = this.FindWebElement(field);
                                                              //  element = driver.FindElement(By.Id(field.pms_field_name));
                        if (element != null)
                        {
                            try
                            {
                                element.Click();
                                // element.Submit();
                                IWait<IWebDriver> waitCNL = new WebDriverWait(driver, TimeSpan.FromSeconds(15.00));
                                waitCNL.Until(ExpectedConditions.ElementExists(By.Id(element.GetAttribute("id")))).Submit();
                            }
                            catch (UnhandledAlertException u)
                            {
                                driver.SwitchTo().Alert().Accept();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.ToLower().Contains("alert"))
                                {
                                    driver.SwitchTo().Alert().Accept();

                                }
                                else
                                {
                                    throw ex;
                                }
                            }// try {                          
                        }
                        break;
                    default:
                        if (element != null && !string.IsNullOrWhiteSpace(fieldValue))
                        {
                            element.SendKeys(Keys.Control + "a");
                            //element.SendKeys(Keys.Shift + Keys.Tab);
                            element.SendKeys(fieldValue);
                            IWait<IWebDriver> waitR = new WebDriverWait(driver, TimeSpan.FromSeconds(10.00));
                            waitR.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                        }
                        break;
                }
            }
            catch (UnhandledAlertException u)
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                if (!ex.Message.ToLower().Contains("stale"))
                    return false;
                //rs.jsonData = null;
                //rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                //rs.message = ex.Message;
            }
            return true;

        }
        private string ProduceValueFromDateExpression(EntityFieldViewModel fld)
        {
            string value = string.IsNullOrWhiteSpace(fld.value) && (fld.mandatory == (int)UTIL.Enums.STATUSES.Active || fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OPTIONAL) ? "" + fld.default_value : "" + fld.value; ;
            if (!string.IsNullOrWhiteSpace(fld.default_value) && fld.control_type == (int)CONTROL_TYPES.DATE)
            {
                DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                int dateFactor = 0;
                if (string.IsNullOrWhiteSpace(fld.value) && !string.IsNullOrWhiteSpace(fld.default_value) && fld.default_value.ToLower().Contains("d"))
                {
                    if (fld.default_value.Contains("+"))
                        dateFactor = Convert.ToInt32(fld.default_value.Split('+')[1]);
                    else if (fld.default_value.Contains("-"))
                    {
                        dateFactor = Convert.ToInt32(fld.default_value.Split('-')[1]);
                        dateFactor = dateFactor * -1;
                    }
                    dt = dt.AddDays(dateFactor);
                }
                else if (string.IsNullOrWhiteSpace(fld.value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("d"))
                {
                    dt = DateTime.Parse(fld.default_value);
                }
                else
                    dt = DateTime.Parse(fld.value);

                value = dt.ToString(fld.format);
            }
            else if (!string.IsNullOrWhiteSpace(value) && fld.control_type == (int)CONTROL_TYPES.DATETIME)
            {
                DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                int dateFactor = 0;
                if (string.IsNullOrWhiteSpace(fld.value) && !string.IsNullOrWhiteSpace(fld.default_value) && fld.default_value.ToLower().Contains("d"))
                {
                    if (fld.default_value.Contains("+"))
                        dateFactor = Convert.ToInt32(fld.default_value.Split('+')[1]);
                    else if (fld.default_value.Contains("-"))
                    {
                        dateFactor = Convert.ToInt32(fld.default_value.Split('-')[1]);
                        dateFactor = dateFactor * -1;
                    }
                    dt = dt.AddDays(dateFactor);
                    value = dt.ToString(fld.format);
                }
                else if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("d"))
                {
                    dt = DateTime.Parse(fld.default_value);
                    value = dt.ToString(fld.format);
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    dt = DateTime.Parse(value);
                    value = dt.ToString(fld.format);
                }

                // value = dt.ToString(fld.format);
            }
            else if (!string.IsNullOrWhiteSpace(value) && fld.data_type == (int)DATA_TYPES.TIME && (fld.control_type == (int)CONTROL_TYPES.DATETIME))
            {
                DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                int timeFactor = 0;
                if (string.IsNullOrWhiteSpace(fld.value) && !string.IsNullOrWhiteSpace(fld.default_value) && fld.default_value.ToLower().Contains("t"))
                {
                    if (fld.default_value.Contains("+"))
                        timeFactor = Convert.ToInt32(fld.default_value.Split('+')[1]);
                    else if (fld.default_value.Contains("-"))
                    {
                        timeFactor = Convert.ToInt32(fld.default_value.Split('-')[1]);
                        timeFactor = timeFactor * -1;
                    }
                    dt = dt.AddHours(timeFactor);
                    value = dt.ToString(fld.format);
                }
                else if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("t"))
                {
                    dt = DateTime.Parse(fld.default_value);
                    value = dt.ToString(fld.format);

                }
                else if (string.IsNullOrWhiteSpace(value))
                {

                    dt = DateTime.Parse(value);
                    value = dt.ToString(fld.format);
                }
            }

            if (fld.format.Contains("/"))
                value = value.Replace("-", "/");
            if (fld.format.Contains("-"))
                value = value.Replace("/", "-");
            return value;
        }
        public ResponseViewModel TestRunMapping(List<MappingViewModel> mappings, bool withFillOption = false)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                _log.Info(string.Format("Test run started, at {0}", System.DateTime.UtcNow.ToUniversalTime()));

                if (driver != null)
                {
                    erroneousLs = new List<EntityFieldViewModel>();
                    warningLs = new List<EntityFieldViewModel>();
                    //******************* Fill Fields Collection
                    foreach (MappingViewModel mapping in mappings)
                    {
                        DateTime lastProcesstime = DateTime.Now;
                        if (mapping.entity_type == (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT || mapping.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC)
                        {
                            this.referenceValue = string.Empty;
                            MappingViewModel mData = mapping.DCopy();
                            this.prepareFieldsCollection(mData, true);
                            // Form Collection can be
                            foreach (FormViewModel frm in mData.forms)
                            {
                                fillKeywordsDictionary(frm.fields, true);
                                foreach (EntityFieldViewModel field in frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(x => x.sr))
                                {
                                    lastProcesstime = DateTime.Now;
                                    // this.FindWebElement(field, withFillOption);                                   
                                    this.FindAndFillWebElement(field, withFillOption);
                                    TimeSpan ts = DateTime.Now - lastProcesstime;
                                    if (ts.TotalMilliseconds < UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS)
                                        System.Threading.Thread.Sleep(UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS);

                                }
                            }
                        }
                    }//foreach (MappingViewModel map in mappings)
                    if (this.erroneousLs != null && this.erroneousLs.Count > 0)
                    {
                        rs.jsonData = mappings;
                        rs.status = false;
                        string[] array = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                    }
                    else
                    {
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Test run success.";
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = string.Format("Test run failed,{0} ", ex.Message);
            }
            return rs;

        }
        // Submit form
        public ResponseViewModel feedDataToWebForm(List<MappingViewModel> mappings, bool withFillOption = false)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                string entityName = string.Empty;
                _log.Info(string.Format("Submit started, at {0}", System.DateTime.UtcNow.ToUniversalTime()));
                //   List<MappingViewModel> mLs = new List<MappingViewModel>();
                this.referenceValue = string.Empty;
                if (driver != null)
                {
                    erroneousLs = new List<EntityFieldViewModel>();

                    //******************* Fill Fields Collection
                    foreach (MappingViewModel mapping in mappings)
                    {
                        MappingViewModel map = mapping;// mapping.DCopy(); already closed
                        this.referenceValue = map.reference;
                        entityName = UTIL.GlobalApp.StringValueOf((Enums.ENTITIES)mapping.entity_Id);
                        if (map.entity_type == (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT || map.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC)
                        {
                            this.prepareFieldsCollection(map, true);
                            // Form Collection can be
                            foreach (FormViewModel frm in map.forms)
                            {
                                DateTime lastProcesstime = DateTime.Now;
                                fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE).ToList(), false);

                                // List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && (string.IsNullOrWhiteSpace(x.pms_field_expression) || !Convert.ToString(x.pms_field_expression).Contains(UTIL.BDUConstants.FEED_NOT_REQUIRED))).OrderBy(X => X.sr).ToList();
                                List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(X => X.sr).ToList();
                                foreach (EntityFieldViewModel field in candidateFlds)//frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(x => x.sr))
                                {

                                    lastProcesstime = DateTime.Now;
                                    // this.FindWebElement(field, withFillOption);                                   
                                    this.FindAndFillWebElement(field, withFillOption);
                                    TimeSpan ts = DateTime.Now - lastProcesstime;
                                    if (ts.TotalMilliseconds < UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS)
                                        System.Threading.Thread.Sleep(UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS);

                                }
                            }
                        }
                        map.saves_status = (int)UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                        //    mLs.Add(map);
                    }//foreach (MappingViewModel map in mappings)

                    if (withFillOption && (this.erroneousLs.Where(x => x.mandatory == (int)UTIL.Enums.STATUSES.Active && x.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).Count() > 0))
                    {

                        rs.jsonData = null;
                        rs.status = false;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.NO_DATA).ToString();
                        //string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        // rs.message = string.Format("Reservation# {0} is not available for this action or cancelled", this.referenceValue);
                        rs.message = string.Format("Reservation# {0} for {1} is not available or cancelled", this.referenceValue, entityName);
                        // rs.message = "Failed to submit data to PMS system.";
                    }
                    else if (this.erroneousLs != null && this.erroneousLs.Count > 0)
                    {
                        rs.jsonData = mappings;
                        rs.status = false;
                        string[] array = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format("PMS missing Fields,{0}...", string.Join(",", array));
                    }
                    else
                    {
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Submitted successfully.";
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Submit failed.";
                }//

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = string.Format("Submit failed, failure detail - {0} ", ex.Message);
            }
            return rs;

        }
        public ResponseViewModel TestRunAccess(List<MappingViewModel> mappings, bool withFillOption = false)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                _log.Info(string.Format("Test run started, at {0}", UTIL.GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                if (driver != null)
                {
                    erroneousLs = new List<EntityFieldViewModel>();
                    //******************* Fill Fields Collection
                    foreach (MappingViewModel map in mappings)
                    {
                        this.referenceValue = string.Empty;
                        if (map.entity_type == (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT || map.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC)
                        {
                            this.prepareFieldsCollection(map, true);
                            // Form Collection can be
                            foreach (FormViewModel frm in map.forms)
                            {
                                fillKeywordsDictionary(frm.fields, true);
                                List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(X => X.sr).ToList();
                                foreach (EntityFieldViewModel field in candidateFlds)//frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(x => x.sr))
                                {
                                    // this.FindWebElement(field, withFillOption);
                                    // System.Threading.Thread.Sleep(75);
                                    this.FindAndFillWebElement(field, withFillOption);
                                }
                            }
                        }
                    }//foreach (MappingViewModel map in mappings)
                    if (this.erroneousLs != null && this.erroneousLs.Count > 0)
                    {
                        rs.jsonData = mappings;
                        rs.status = false;
                        string[] array = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                    }
                    else
                    {
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Test run success.";
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = string.Format("Test run failed,{0} ", ex.Message);
            }
            return rs;

        }
        public ResponseViewModel login(MappingViewModel access)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                _log.Info(string.Format("Test run started, at {0}", System.DateTime.UtcNow.ToUniversalTime()));

                if (driver != null)
                {
                    erroneousLs = new List<EntityFieldViewModel>();
                    //******************* Fill Fields Collection                   
                    if (access.entity_type == (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT)
                    {
                        this.prepareFieldsCollection(access, true);
                        // Form Collection can be
                        foreach (FormViewModel frm in access.forms)
                        {
                            DateTime lastProcesstime = DateTime.Now;


                            IWait<IWebDriver> waitDC = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
                            waitDC.Until(driverX => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                            foreach (EntityFieldViewModel field in frm.fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed != (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.NOT_REQUIRED).OrderBy(x => x.sr))
                            {
                                lastProcesstime = DateTime.Now;
                                // this.FindWebElement(field, withFillOption);                                   
                                this.FindAndFillWebElement(field, true);
                                TimeSpan ts = DateTime.Now - lastProcesstime;
                                if (ts.TotalMilliseconds <= UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS)
                                    System.Threading.Thread.Sleep(UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS);

                            }
                        }
                    }

                    if (this.erroneousLs != null && this.erroneousLs.Count > 0)
                    {
                        rs.jsonData = access;
                        rs.status = false;
                        string[] array = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format("PMS missing Fields,{0}...", string.Join(",", array));
                    }
                    else
                    {
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Test run success.";
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//

            }
            catch (UnhandledAlertException ex)
            {
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = string.Format("Test run failed,{0} ", ex.Message);
            }
            return rs;

        }
        public ResponseViewModel TestRunEntityFields(List<EntityFieldViewModel> fields)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                erroneousLs = new List<EntityFieldViewModel>();
                //******************* Fill Fields Collection
                if (driver != null)
                {
                    erroneousLs = new List<EntityFieldViewModel>();
                    // Form Collection can be
                    // driver.Url= driver.Navigate().GoToUrl(applicationUrl)
                    foreach (EntityFieldViewModel field in fields.Where(x => x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE).OrderBy(x => x.sr))
                    {
                        // Skip no data , unnecessary
                        if ((field.data_type == (int)UTIL.Enums.DATA_TYPES.NO_DATA && field.mandatory == 0))
                        {
                            //TODO
                        }
                        else
                            this.FindWebElement(field);
                    }


                    if (this.erroneousLs != null && this.erroneousLs.Count > 0)
                    {
                        rs.jsonData = this.erroneousLs;
                        rs.status = false;
                        string[] array = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        string[] allAarray = this.erroneousLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).ToArray();
                        _log.Error("The following fields are missing, " + string.Join(",", allAarray));
                        rs.message = string.Format("PMS missing Fields,{0}...", string.Join(",", array));
                    }
                    else if (this.warningLs != null && this.warningLs.Count > 0)
                    {
                        rs.jsonData = this.warningLs;
                        rs.status = false;
                        string[] array = this.warningLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        string[] allAarray = this.warningLs.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).ToArray();
                        _log.Error("The following fields are missing / invalid, " + string.Join(",", allAarray));
                        rs.message = string.Format("PMS missing / invalid Fields, {0}...", string.Join(",", array));
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.WARNING).ToString();
                    }
                    else
                    {
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Test run success.";
                    }
                }// if (driver != null) {
                else
                {
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Test run failed.";
                }//
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = string.Format("Test run failed,{0} ", ex.Message);
            }
            return rs;

        }
        private IWebElement FindWebElement(EntityFieldViewModel field, bool withFillOption = false)
        {
            IWebElement element = null;
            //  IWebElement pElement = null;
            EntityFieldViewModel parent = null;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            try
            {
                if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.URL && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.LOAD)
                {
                    // Expression will be used for reference value for performing test run.
                    string url = field.default_value;
                    if (field.default_value.Contains("$reference") && !string.IsNullOrWhiteSpace("" + field.pms_field_expression))
                        url = url.Replace("", "" + field.pms_field_expression);
                    driver.Navigate().GoToUrl(url);
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
                }
                else if (field.status == (int)UTIL.Enums.STATUSES.Active && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT)
                {
                    element = this.driver.FindElement(By.XPath(field.pms_field_xpath));

                    if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        String javascript = field.pms_field_expression;
                        js.ExecuteScript(javascript, element);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && !string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    parent = _fields.Where(x => x.fuid == Convert.ToString(field.parent_field_id)).FirstOrDefault();
                    //******************* SWITCH to Parent Field***************************
                    if (parent != null)
                    {
                        string parentXpath = string.IsNullOrWhiteSpace(parent.pms_field_xpath) ? parent.pms_field_name : parent.pms_field_xpath;
                        if (!string.IsNullOrWhiteSpace(parentXpath))
                        {
                            this.driver.SwitchTo().Frame(driver.FindElement(By.XPath(parentXpath)));// driver.findElement(By.name("iFrameTitle")));
                            if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                            {
                                element = this.driver.FindElement(By.Id(field.pms_field_name));
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && withFillOption)
                            {
                                element = this.driver.FindElement(By.CssSelector(field.pms_field_expression));
                            }
                            else
                                element = this.driver.FindElement(By.Name(field.pms_field_name));

                            // this.driver.SwitchTo().DefaultContent();
                        }
                    }
                    else
                    {
                        erroneousLs.Add(field);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                    {
                        element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        element = this.driver.FindElement(By.Id(field.pms_field_name));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        element = this.driver.FindElement(By.ClassName(field.pms_field_expression));
                    }
                    else
                        element = this.driver.FindElement(By.Name(field.pms_field_name));
                }
                else if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                    element = this.driver.FindElement(By.Id(field.pms_field_name));
            }
            catch (Exception ex)
            {
                _log.Warn(ex);
                if (this.erroneousLs != null && (field.mandatory >= 1 || !string.IsNullOrWhiteSpace(field.default_value)))
                    this.erroneousLs.Add(field);
                else if (this.warningLs != null)
                    this.warningLs.Add(field);
            }
            finally
            {
                this.driver.SwitchTo().DefaultContent();
            }

            return element;
        }
        public string getCurrentURL()
        {
            return driver != null ? driver.Url.ToLower() : string.Empty;
        }
        //string element  = driver.FindElement(By.ClassName("button button--chromeless")).GetAttribute("data-action-value");
        private IWebElement FindAndFillWebElement(EntityFieldViewModel field, bool withFillOption = false)
        {
            IWebElement element = null;
            // IWebElement pElement = null;
            EntityFieldViewModel parent = null;
            try
            {
                if (field.mandatory == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                {
                    WebDriverWait waitP = new WebDriverWait(this.driver, TimeSpan.FromSeconds(45));
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.XPath(field.pms_field_xpath))));
                        }
                        else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Id(field.pms_field_name))));
                        }
                        else
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Name(field.pms_field_name))));
                        }


                        if (element == null)
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Id(field.pms_field_name))));
                        if (element != null && !withFillOption)
                            return element;

                    }
                    catch (Exception ex) { }
                    if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        //WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                        // element = (IWebElement)((IJavaScriptExecutor)this.driver).ExecuteScript(field.pms_field_expression);
                        Boolean res = (Boolean)waitP.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript(field.pms_field_expression));
                        if (res != null && res == false) throw new Exception("Data not found");
                        // element = waitP.Until(ExpectedConditions.ElementExists((By.CssSelector(field.pms_field_expression))));                       
                    }
                }
                if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.URL || field.control_type == (int)UTIL.Enums.CONTROL_TYPES.PAGE) && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.LOAD)
                {

                    // string url= string.IsNullOrWhiteSpace(this.referenceValue)? (string.IsNullOrWhiteSpace("" + field.default_value)? ("" + field.pms_field_expression): "" + field.default_value)) : ("" + field.default_value).Replace("$reference",this.referenceValue);
                    string url = field.default_value;
                    if (url.Contains("$reference") && !string.IsNullOrWhiteSpace("" + field.pms_field_expression) && string.IsNullOrWhiteSpace(this.referenceValue))
                        url = url.Replace("$reference", "" + field.pms_field_expression);
                    else if (field.default_value.Contains("$reference"))
                        url = url.Replace("$reference", this.referenceValue);
                    if (!driver.Url.Contains(url) && !string.IsNullOrWhiteSpace(url))
                    {
                        if (driver.Url != url)
                        {
                            driver.Navigate().GoToUrl(url);
                            try
                            {
                                WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
                                waitP.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                            }
                            catch (UnhandledAlertException ex)
                            {
                                IAlert alert = driver.SwitchTo().Alert();
                                // Alert present; set the flag
                                alert.Accept();
                                // if present consume the alert

                                _log.Error(ex);
                            }
                        }
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT)
                {
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                    {
                        WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                        element = waitP.Until(ExpectedConditions.ElementExists((By.XPath(field.pms_field_xpath))));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {

                        WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                        element = waitP.Until(ExpectedConditions.ElementExists((By.Id(field.pms_field_name))));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        WebDriverWait waitP = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                        // element = (IWebElement)((IJavaScriptExecutor)this.driver).ExecuteScript(field.pms_field_expression);
                        element = (IWebElement)waitP.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript(field.pms_field_expression));
                    }


                    if (withFillOption && element != null)
                    {
                        this.feedElement(field, element);
                        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT)
                {
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        element = this.driver.FindElement(By.Id(field.pms_field_name));
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        element = (IWebElement)((IJavaScriptExecutor)this.driver).ExecuteScript(field.pms_field_expression);
                    }


                    if (withFillOption && element != null)
                    {
                        this.feedElement(field, element);
                        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.GRID && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK)
                {
                    if (!string.IsNullOrWhiteSpace(field.default_value))
                    {
                        if (field.default_value.Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                        {
                            string[] values = field.default_value.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                            foreach (string item in values)
                            {
                                if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                                    field.pms_field_xpath = string.Format(field.pms_field_xpath, item);
                                else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                                    field.pms_field_expression = string.Format(field.pms_field_expression, item);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                                field.pms_field_xpath = string.Format(field.pms_field_xpath, field.default_value);
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                                field.pms_field_expression = string.Format(field.pms_field_expression, field.default_value);
                        }

                    }
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        element = this.driver.FindElement(By.Id(field.pms_field_name));
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        element = (IWebElement)((IJavaScriptExecutor)this.driver).ExecuteScript(field.pms_field_expression);
                    }


                    if (withFillOption && element != null)
                    {
                        this.feedElement(field, element);
                        //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && !string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    parent = _fields.Where(x => x.fuid == Convert.ToString(field.parent_field_id)).FirstOrDefault();
                    //******************* SWITCH to Parent Field***************************
                    if (parent != null)
                    {
                        string parentXpath = string.IsNullOrWhiteSpace(parent.pms_field_xpath) ? parent.pms_field_name : parent.pms_field_xpath;
                        if (!string.IsNullOrWhiteSpace(parentXpath))
                        {
                            this.driver.SwitchTo().Frame(driver.FindElement(By.XPath(parentXpath)));// driver.findElement(By.name("iFrameTitle")));
                            if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                                if (withFillOption && element != null)
                                {
                                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                                    this.feedElement(field, element);
                                }
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                            {
                                element = this.driver.FindElement(By.Id(field.pms_field_name));
                                if (withFillOption && element != null)
                                {
                                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                                    this.feedElement(field, element);
                                }
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && withFillOption)
                            {
                                element = this.driver.FindElement(By.CssSelector(field.pms_field_expression));
                                if (withFillOption && element != null)
                                {
                                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                                    this.feedElement(field, element);
                                }
                            }
                            else
                            {
                                element = this.driver.FindElement(By.Name(field.pms_field_name));
                                if (withFillOption && element != null)
                                {
                                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                                    this.feedElement(field, element);
                                }
                            }
                        }
                    }
                    else
                    {
                        erroneousLs.Add(field);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                    {
                        element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                        if (withFillOption && element != null)
                        {
                            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                            this.feedElement(field, element);

                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        element = this.driver.FindElement(By.Id(field.pms_field_name));
                        if (withFillOption && element != null)
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
                            this.feedElement(field, element);

                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        element = this.driver.FindElement(By.ClassName(field.pms_field_expression));
                        if (withFillOption && element != null)
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
                            this.feedElement(field, element);
                        }
                    }
                    else
                    {
                        element = this.driver.FindElement(By.Name(field.pms_field_name));
                        if (withFillOption && element != null)
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
                            this.feedElement(field, element);

                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                {
                    element = this.driver.FindElement(By.Id(field.pms_field_name));
                    if (withFillOption && element != null)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                        this.feedElement(field, element);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Warn(ex);

                if (ex.Message.ToLower().Contains("alert"))
                    driver.SwitchTo().Alert().Dismiss();
                // this.erroneousLs.Add(field);
                if (this.erroneousLs != null && (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY || !string.IsNullOrWhiteSpace(field.default_value)))
                    this.erroneousLs.Add(field);
                else if (this.warningLs != null)
                    this.warningLs.Add(field);
            }
            finally
            {
                this.driver.SwitchTo().DefaultContent();
            }

            return element;
        }
        private IWebElement FindFetchWebElement(EntityFieldViewModel field)
        {
            IWebElement element = null;
            //  IWebElement pElement = null;
            EntityFieldViewModel parent = null;
            try
            {
                if (field.mandatory == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                {
                    WebDriverWait waitP = new WebDriverWait(this.driver, TimeSpan.FromSeconds(30));
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.XPath(field.pms_field_xpath))));
                        }
                        else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Id(field.pms_field_name))));

                        }
                        else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.CssSelector(field.pms_field_expression))));
                        }
                        else
                        {
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Name(field.pms_field_name))));
                        }
                        if (element == null)
                            element = waitP.Until(ExpectedConditions.ElementExists((By.Id(field.pms_field_name))));
                        if (element != null)
                            return element;
                    }
                    catch (Exception ex) { }
                }
                if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.URL || field.control_type == (int)UTIL.Enums.CONTROL_TYPES.PAGE) && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.LOAD)
                {
                    if (!driver.Url.ToLower().Contains(("" + field.default_value).ToLower()) && !string.IsNullOrWhiteSpace(field.default_value))
                    {
                        WebDriverWait waitP = new WebDriverWait(this.driver, TimeSpan.FromSeconds(20.00));
                        waitP.Until(driver1 => ((IJavaScriptExecutor)this.driver).ExecuteScript("return document.readyState").Equals("complete"));

                        this.driver.Navigate().GoToUrl("" + field.default_value);

                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT)
                {
                    element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                }
                else if (field.status == (int)UTIL.Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    parent = _fields.Where(x => x.fuid == Convert.ToString(field.parent_field_id)).FirstOrDefault();
                    //******************* SWITCH to Parent Field***************************
                    if (parent != null)
                    {
                        string parentXpath = string.IsNullOrWhiteSpace(parent.pms_field_xpath) ? parent.pms_field_name : parent.pms_field_xpath;
                        if (!string.IsNullOrWhiteSpace(parentXpath))
                        {
                            this.driver.SwitchTo().Frame(this.driver.FindElement(By.XPath(parentXpath)));// driver.findElement(By.name("iFrameTitle")));
                            if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                            {
                                element = this.driver.FindElement(By.Id(field.pms_field_name));
                            }
                            else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                element = this.driver.FindElement(By.CssSelector(field.pms_field_expression));
                            }
                            else
                            {
                                element = this.driver.FindElement(By.Name(field.pms_field_name));
                            }
                        }
                    }
                    else
                    {
                        erroneousLs.Add(field);
                    }
                }
                else if (field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && string.IsNullOrWhiteSpace(field.parent_field_id))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(8.00));
                    wait.Until(driver1 => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                    if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                    {
                        element = this.driver.FindElement(By.XPath(field.pms_field_xpath));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        element = this.driver.FindElement(By.Id(field.pms_field_name));
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_expression))
                    {
                        element = this.driver.FindElement(By.ClassName(field.pms_field_expression));
                    }
                    else
                    {
                        element = this.driver.FindElement(By.Name(field.pms_field_name));
                    }
                }
                else if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                {
                    element = this.driver.FindElement(By.Id(field.pms_field_name));
                }
            }
            catch (Exception ex)
            {
                _log.Warn(ex);

                if (ex.Message.ToLower().Contains("alert"))
                    driver.SwitchTo().Alert().Dismiss();
                // this.erroneousLs.Add(field);
                if (this.erroneousLs != null && (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY || !string.IsNullOrWhiteSpace(field.default_value)))
                    this.erroneousLs.Add(field);
                else if (this.warningLs != null)
                    this.warningLs.Add(field);
            }
            //finally
            //{
            //    this.driver.SwitchTo().DefaultContent();
            //}

            return element;
        }
        //private IWebElement FindElement(EntityFieldViewModel field)
        //{
        //    try
        //    {
        //        IWebElement element = null;

        //            switch (field.data_type)
        //            {
        //             case (int)DATA_TYPES.INT: // Input
        //                if (!string.IsNullOrWhiteSpace(field.parent_field_id) ){ 

        //                }else if(!string.IsNullOrWhiteSpace(field.pms_field_xpath))
        //                    element = this.driver.FindElement(By.Id(field.pms_field_name));
        //                   // element.SendKeys("" + field.value);
        //                    break;
        //            case (int)DATA_TYPES.TEXT: // Input
        //                    element = this.driver.FindElement(By.XPath(field.pms_field_name));
        //                    break;
        //            case (int)DATA_TYPES.DATE: // Input
        //                element = this.driver.FindElement(By.Id(field.pms_field_name));
        //                break;
        //            case (int)DATA_TYPES.ARRAY_INT:// Select
        //                    element = this.driver.FindElement(By.Id(field.pms_field_name));
        //                    break;
        //                case (int)DATA_TYPES.DATETIME:// Select
        //                    element = this.driver.FindElement(By.Name(field.pms_field_name));                           
        //                    break;
        //            case (int)DATA_TYPES.JSON:// Select
        //                element = this.driver.FindElement(By.Name(field.pms_field_name));
        //                break;
        //            case (int)DATA_TYPES.ARRAY_TEXT:// Select
        //                    element = driver.FindElement(By.Id(field.pms_field_name));
        //                   // element.Click();                           
        //                  //  element.Submit();
        //                    break;
        //                default:
        //                    element = this.driver.FindElement(By.Id(field.pms_field_name));
        //                   // element.SendKeys(field.value);
        //                    break;
        //            }

        //        return element;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Warn(ex);
        //        return null;
        //    }

        //}
    }
}
