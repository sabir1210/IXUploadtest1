using System.Text.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WinAppDriver.Generation.UiEvents.Models;
using OpenQA.Selenium.Interactions;
using BDU.ViewModels;
using static BDU.UTIL.BDUUtil;
using WinAppDriver.Generation.Client;
using static BDU.UTIL.Enums;
using WinAppDriver.Generation.Client.Models;
using System.Diagnostics;
using WinAppDriver.Generation.Events.Hook.Models;
using System.Threading;
using System.Drawing;
using NLog;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using OpenQA.Selenium.Appium.Windows;

namespace BDU.RobotDesktop
{
    public class DesktopHandler
    {
        #region "variables & objects declaration & intialization"
        private Logger _log = LogManager.GetCurrentClassLogger();
        public delegate void SaveDataEvent(MappingViewModel mappingViewModel);
        public SaveDataEvent DataSaved;

        // private delegate void ScanAndFoundData(UiEvent curUIEvent);

        public delegate void UddateUIAboutDataEvent(UTIL.Enums.SYNC_MESSAGE_TYPES mType, string msg);
        public event UddateUIAboutDataEvent blazorUpdateUIAboutPMSEvent;
        private GenerationClient _generationClient { get; set; }

        private EventHandler<EventArgs> _generationClientHookProcedureEventHandler { get; set; }

        private EventHandler<UiEventEventArgs> _generationClientUiEventEventHandler { get; set; }

        // private bool MasterReferenceFound = true;

        //   private ROBOT_UI_STATUS _recorderUiState { get; set; }

        string referenceValue = string.Empty;
        //  private MappingViewModel _formMapping = new MappingViewModel();
        public MappingViewModel _formData { get; set; }
        // Dictionary<string, string> keywordDict { get; set; }
        public MappingViewModel _access = null;
        public List<MappingViewModel> ScanningEntities = null;
        private List<MappingDefinitionViewModel> definitionEntities = null;
        private List<string> submitControls = new List<string> { "button", "pane" };// { "edit", "button", "pane" };
        DesktopSession session { get; set; }
        AppiumWebElement root { get; set; }
        //List<AppiumWebElement> elementDict { get; set; }
        Dictionary<string, AppiumWebElement> elementDict { get; set; }
        public PMS_LOGGIN_STATUS MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
        public string _ApplicationNameWithURL { get; set; }
        public string _ApplicationDefaultUrl { get; set; }
        public List<EntityFieldViewModel> _fields { get; set; }
        #endregion
        //  bool isSubmiting = false;
        //bool isDataFeeded = false;
        public string rootFolder
        {
            get
            {
                return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
            }
        }

        public DesktopHandler()
        {

        }
        #region "External Call Methods"
        public bool StartSessionAndLogin(MappingViewModel mapping)
        {
            bool started = false;
            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS application launching...");
            if (started == false && StartServer())
            {

                if (session != null && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT)
                {
                    ResponseViewModel res = new ResponseViewModel();
                    _formData = mapping.DCopy();
                    if (UTIL.GlobalApp.IS_PROCESS == "1")
                    {

                        res = this.ProcessLogin(_formData);
                        started = true;
                    }
                    else
                        res = this.login(_formData);
                    if (res.status)
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS session intialization finished..");
                        started = true;
                    }
                    else
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                        _access = _formData;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.ERROR, "PMS session intialization failed..");

                    }
                    // this.TestRun(mapping);
                    // SetupGenerationClient();

                }

            }
            return started;
        }
        private ResponseViewModel login()
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                _log.Info("Login service requested");
                // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS login started..");
                if (MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT)
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                    res = this.login(_access);
                    if (res.status)
                    {
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS login success..");
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;// not log & logged out is same
                }
            }
            catch (Exception ex)
            {
                // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.ERROR, "PMS failed..");
                _log.Error(ex);
            }
            finally
            {
                SetUiForRecorderUiStateIsReady();
                this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS login finished..");
            }
            //Console.WriteLine("* End *");
            return res;
        }
        public bool StartDesktopSession(List<MappingViewModel> mapping)
        {
            bool started = false;
            if (StartServer())
            {
                session = new DesktopSession();

                if (session != null && session.desktopSession != null)
                {
                    // _access = mapping[0];
                    _fields = (from f in mapping[0].forms
                               from flds in f.fields
                               where flds.status == (int)UTIL.Enums.STATUSES.Active
                               select new EntityFieldViewModel
                               {
                                   control_type = flds.control_type,
                                   data_type = flds.data_type,
                                   fuid = flds.fuid,
                                   parent_field_id = flds.parent_field_id,
                                   pms_field_expression = flds.pms_field_expression,
                                   pms_field_name = flds.pms_field_name,
                                   pms_field_xpath = flds.pms_field_xpath,
                                   entity_id = flds.entity_id,
                                   sr = flds.sr,
                                   //  save_status = flds.save_status,
                                   value = flds.value,
                                   mandatory = flds.mandatory,
                                   is_reference = flds.is_reference,
                                   is_unmapped = flds.is_unmapped,
                                   default_value = flds.default_value
                               }).ToList();

                    EntityFieldViewModel fld = _fields.Where(x => x.field_desc == "applicationwithname").FirstOrDefault();
                    if (fld != null)
                    {
                        _ApplicationDefaultUrl = fld.default_value;
                        _ApplicationNameWithURL = fld.default_value;
                        UTIL.GlobalApp.PMS_Application_Path_WithName = _ApplicationNameWithURL;
                    }

                    elementDict = new Dictionary<string, AppiumWebElement>();
                    // keywordDict = new Dictionary<string, string>();
                    //  _formMapping = mapping[0];

                    //TestRun();
                    SetupGenerationClient();
                    started = true;
                }
            }
            return started;
        }
        public void StopDesktopSession()
        {
            StopCaptering();
            StopServer();
            // _formMapping = null;
            _formData = null;
        }
        public ResponseViewModel login(MappingViewModel model)
        {
            ResponseViewModel rs = new ResponseViewModel();
            SetUiForRecorderUiStateDataFeeding();
            //   _formData = model.DCopy();
            if (model != null)
            {

                // root = session.desktopSession.FindElements(By.XPath("*/*"))[0]
                if (session.isRootCapability)
                {
                    
                    if (model.forms != null && !string.IsNullOrWhiteSpace(model.forms.FirstOrDefault().pmspagename))
                    {
                        root = session.FindElementByAbsoluteXPath(model.forms.FirstOrDefault().pmspagename);
                    }
                    else
                    {
                        root = session.FindElementByAbsoluteXPath(model.xpath);
                    }

                }
                else if (session != null && session.desktopSession != null)
                {
                    //  root = session.desktopSession.FindElements(By.XPath(model.forms.FirstOrDefault().pmspagename))[0];
                    //root = session.desktopSession.FindElements(By.XPath(model.forms.FirstOrDefault().pmspagename))[0];
                    //root = session.desktopSession.FindElementByName("TestApp"); 
                    //  root = session.desktopSession.FindElementByName("PMSForm");
                    // if(session.desktopSession.FindElementsByAccessibilityId("PMSForm").Count>0)
                    //    root = session.desktopSession.FindElementByAccessibilityId("PMSForm");
                    // else if(session.desktopSession.FindElementsByName("PMSForm").Count > 0)
                    //     root = session.desktopSession.FindElementByName("PMSForm");
                    //else if (session.desktopSession.FindElementsByXPath("PMSForm").Count > 0)
                    //     root = session.desktopSession.FindElementByXPath("PMSForm");
                    // else
                    root = session.desktopSession.FindElementByXPath(".//*");
                    //if (model.forms != null && !string.IsNullOrWhiteSpace(model.forms.FirstOrDefault().pmspagename))
                    // root = session.desktopSession.FindElementByXPath("//*");// session.desktopSession.FindElements(By.XPath("*/*"))[0];// session.desktopSession.FindElementByXPath(model.forms.FirstOrDefault().pmspagename);

                }
            }
            if (root != null)
            {

                //  Apply code from config

                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_USER_PWD) && model != null && (model.entity_Id == (int)UTIL.Enums.ENTITIES.ACCESS || model.id == (int)UTIL.Enums.ENTITIES.ACCESS))
                {
                    if (model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("code")).FirstOrDefault() != null)
                    {
                        EntityFieldViewModel fld = model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("code")).FirstOrDefault();
                        fld.default_value = UTIL.GlobalApp.PMS_USER_PWD;
                    }
                }
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_USER_NAME) && model != null && (model.entity_Id == (int)UTIL.Enums.ENTITIES.ACCESS || model.id == (int)UTIL.Enums.ENTITIES.ACCESS))
                {
                    if (model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("user")).FirstOrDefault() != null)
                    {
                        EntityFieldViewModel flduser = model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("user")).FirstOrDefault();
                        flduser.default_value = UTIL.GlobalApp.PMS_USER_NAME;
                    }
                }
                var errorFields = CacheFormElementsWithFill(model, true);
                if (errorFields.Count == 0)
                {
                    rs.status = true;
                    rs.jsonData = null;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Login successfully completed.";

                }
                else
                {
                    rs.status = false;
                    rs.jsonData = JsonSerializer.Serialize(errorFields);
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    var message = "Following fields are failed\n\r";
                    foreach (var field in errorFields.Where(x => x.mandatory == 1))
                    {
                        message += field.pms_field_name + "\n\r";
                    }
                    rs.message = message;
                }
            }
            else
            {
                rs.status = false;
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = "PMS application is not running!";
            }
            SetUiForRecorderUiStateIsReady();
            return rs;

        }
        public ResponseViewModel ProcessLogin(MappingViewModel model)
        {
            ResponseViewModel rs = new ResponseViewModel();
            SetUiForRecorderUiStateDataFeeding();
            //   _formData = model.DCopy();
            if (model != null)
            {

                // root = session.desktopSession.FindElements(By.XPath("*/*"))[0]
                if (session.isRootCapability)
                {
                    if (UTIL.GlobalApp.IS_PROCESS == "1" && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME))
                    {
                        Process[] localprocessName = Process.GetProcessesByName(UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME);
                        if (localprocessName.Length > 0) {
                            root = session.FindElementByAbsoluteXPath(UTIL.GlobalApp.PMS_Application_Path_WithName);
                        }
                    }
                    else if (UTIL.GlobalApp.IS_PROCESS == "1" && model.forms != null && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_Application_Path_WithName))
                    {
                        root = session.FindElementByAbsoluteXPath(model.forms.FirstOrDefault().pmspagename);
                    }
                    else if (model.forms != null && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_Application_Path_WithName))
                    {
                        root = session.FindElementByAbsoluteXPath(UTIL.GlobalApp.PMS_Application_Path_WithName);
                    }
                    else
                    {
                        root = session.FindElementByAbsoluteXPath(model.xpath);
                    }
                }
                else if (session != null && session.desktopSession != null)
                {
                    //  root = session.desktopSession.FindElements(By.XPath(model.forms.FirstOrDefault().pmspagename))[0];
                    //root = session.desktopSession.FindElements(By.XPath(model.forms.FirstOrDefault().pmspagename))[0];
                    //root = session.desktopSession.FindElementByName("TestApp"); 
                    //  root = session.desktopSession.FindElementByName("PMSForm");
                    // if(session.desktopSession.FindElementsByAccessibilityId("PMSForm").Count>0)
                    //    root = session.desktopSession.FindElementByAccessibilityId("PMSForm");
                    // else if(session.desktopSession.FindElementsByName("PMSForm").Count > 0)
                    //     root = session.desktopSession.FindElementByName("PMSForm");
                    //else if (session.desktopSession.FindElementsByXPath("PMSForm").Count > 0)
                    //     root = session.desktopSession.FindElementByXPath("PMSForm");
                    // else
                    root = session.desktopSession.FindElementByXPath(".//*");
                    //if (model.forms != null && !string.IsNullOrWhiteSpace(model.forms.FirstOrDefault().pmspagename))
                    // root = session.desktopSession.FindElementByXPath("//*");// session.desktopSession.FindElements(By.XPath("*/*"))[0];// session.desktopSession.FindElementByXPath(model.forms.FirstOrDefault().pmspagename);

                }
            }
            if (root != null)
            {

                //  Apply code from config

                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_USER_PWD) && model != null && (model.entity_Id == (int)UTIL.Enums.ENTITIES.ACCESS || model.id == (int)UTIL.Enums.ENTITIES.ACCESS))
                {
                    if (model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("code")).FirstOrDefault() != null)
                    {
                        EntityFieldViewModel fld = model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("code")).FirstOrDefault();
                        fld.default_value = UTIL.GlobalApp.PMS_USER_PWD;
                    }
                }
                if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_USER_NAME) && model != null && (model.entity_Id == (int)UTIL.Enums.ENTITIES.ACCESS || model.id == (int)UTIL.Enums.ENTITIES.ACCESS))
                {
                    if (model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("user")).FirstOrDefault() != null)
                    {
                        EntityFieldViewModel flduser = model.forms[0].fields.Where(x => x.field_desc.ToLower().Contains("user")).FirstOrDefault();
                        flduser.default_value = UTIL.GlobalApp.PMS_USER_NAME;
                    }
                }
              
                    rs.status = true;
                    rs.jsonData = null;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Login successfully completed.";

               
               
            }
            else
            {
                rs.status = false;
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = "PMS System is not running!";
            }
            SetUiForRecorderUiStateIsReady();
            return rs;

        }
        public ResponseViewModel TestRun(MappingViewModel model)
        {
            MappingViewModel TestEnty = null;
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                SetUiForRecorderUiStateDataFeeding();
                TestEnty = model.DCopy();
                if (model != null && session != null)
                {

                    // root = session.desktopSession.FindElements(By.XPath("*/*"))[0]
                    if (session.isRootCapability )
                    {
                        //if (TestEnty.forms != null && !string.IsNullOrWhiteSpace(TestEnty.forms.FirstOrDefault().pmspagename))
                        //{
                        //    root = session.FindElementByAbsoluteXPath(TestEnty.forms.FirstOrDefault().pmspagename);
                        //}
                        //else
                        //{
                        //    root = session.FindElementByAbsoluteXPath(TestEnty.xpath);
                        //}
                        //if(string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName)
                       if (!string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName))
                            {
                                root = session.FindElementByAbsoluteXPath(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName);
                           // root= session.desktopSession.FindElement(By.XPath(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName));
                        }
                            else
                            {
                                root = session.FindElementByAbsoluteXPath(".//*");
                            }
                    }
                    else
                    {
                        //if (session.desktopSession.FindElementsByAccessibilityId("PMSForm").Count > 0)
                        //    root = session.desktopSession.FindElementByAccessibilityId("PMSForm");
                        //else if (session.desktopSession.FindElementsByName("PMSForm").Count > 0)
                        //    root = session.desktopSession.FindElementByName("PMSForm");
                        //else if (session.desktopSession.FindElementsByXPath("PMSForm").Count > 0)
                        //    root = session.desktopSession.FindElementByXPath("PMSForm");
                        //else
                        root = session.desktopSession.FindElement(By.XPath(".//*"));
                    }
                }
                if (root != null)
                {
                    var errorFields = CacheFormElementsWithFillTestRun(TestEnty, true);
                    if (errorFields.Count == 0)
                    {
                        rs.status = true;
                        rs.jsonData = null;
                        rs.status = true;
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        rs.message = "Test run success.";

                    }
                    else if (errorFields != null && errorFields.Where(x => x.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).FirstOrDefault() != null)
                    {
                        rs.status = false;
                        rs.jsonData = JsonSerializer.Serialize(errorFields);
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                        //string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format(UTIL.GlobalApp.RESERVATION_NOT_FOUND_IN_PMS, model.reference);
                    }
                    else if (errorFields != null)
                    {
                        rs.status = false;
                        rs.jsonData = JsonSerializer.Serialize(errorFields);
                        rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                        string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                        rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                    }
                }
                else
                {
                    rs.status = false;
                    rs.jsonData = null;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    rs.message = "Application or Server not running";
                }
            }
            catch (Exception ex)
            {
                rs.status = false;
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = "Application or Server not running";
                _log.Error(ex);
            }
            finally
            {
                SetUiForRecorderUiStateIsReady();
                TestEnty = null;
            }
            return rs;

        }
        #endregion
        #region "Core desktop control methods"
        private List<EntityFieldViewModel> CacheFormElements()
        {
            var errorFields = new List<EntityFieldViewModel>();
            foreach (var form in _formData.forms)
            {
                var errors = CacheFormElements(form.fields, form);
                errorFields.AddRange(errors);
            }
            return errorFields;
        }
        private List<EntityFieldViewModel> CacheFormElementsWithFillTestRun(MappingViewModel formData, bool needFill = false)
        {
            var errorFields = new List<EntityFieldViewModel>();
            //if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT)
            //{

            foreach (FormViewModel frm in formData.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(X => X.sort_order))
            {
                fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList(), true);
                List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(X => X.sr).ToList();
                foreach (EntityFieldViewModel field in candidateFlds) //rm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(X => X.sr))
                {
                    if (field.Fields != null && field.Fields.Count > 0)
                    {
                        var errors = CacheFormElementsWithFill(frm, needFill);
                        errorFields.AddRange(errors);
                    }
                    else
                    {
                        var element = FindAndCachedElementWithFill(root, field, frm, needFill);
                        if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                        {
                            errorFields.Clear();
                            errorFields.Add(field);
                            return errorFields;
                        }
                        else if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
                        {
                            errorFields.Add(field);
                        }
                    }
                }


                //var errors = CacheFormElementsWithFill(form.fields, form, needFill);
                //errorFields.AddRange(errors);
            }// Outer foreach
            //}//if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA)
            return errorFields;
        }
        private List<EntityFieldViewModel> CacheFormElementsWithFill(MappingViewModel formData, bool needFill = false)
        {
            var errorFields = new List<EntityFieldViewModel>();
            if (UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SCANNING)
            {

                foreach (FormViewModel frm in formData.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(X => X.sort_order))
                {
                    fillKeywordsDictionary(frm.fields);
                    List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(X => X.sr).ToList();
                    foreach (EntityFieldViewModel field in candidateFlds) //rm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(X => X.sr))
                    {
                        if (field.Fields != null && field.Fields.Count > 0)
                        {
                            var errors = CacheFormElementsWithFill(frm, needFill);
                            errorFields.AddRange(errors);
                        }
                        else
                        {
                            var element = FindAndCachedElementWithFill(root, field, frm, needFill);
                            if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                            {
                                errorFields.Clear();
                                errorFields.Add(field);
                                return errorFields;
                            }
                            else if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
                            {
                                errorFields.Add(field);
                            }
                        }
                    }
                }// Outer foreach
            }//if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA)
            return errorFields;
        }
        private List<EntityFieldViewModel> CacheFormElementsWithFill(FormViewModel form, bool needFill = false)
        {
            var errorFields = new List<EntityFieldViewModel>();
            if (form.fields != null && form.fields.Count() > 0)
            {
                foreach (EntityFieldViewModel field in form.fields.OrderBy(X => X.sr))
                {
                    if (field.Fields != null && field.Fields.Count > 0)
                    {
                        var errors = CacheFormElementsWithFill(form, needFill);
                        errorFields.AddRange(errors);
                    }
                    else
                    {
                        var element = FindAndCachedElementWithFill(root, field, form, needFill);
                        if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                        {
                            errorFields.Clear();
                            errorFields.Add(field);
                            return errorFields;
                        }
                        else if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
                        {
                            errorFields.Add(field);
                        }
                    }
                }
            }
            return errorFields;
        }
        private List<EntityFieldViewModel> CacheFormElements(ICollection<EntityFieldViewModel> fields, FormViewModel form)
        {
            var errorFields = new List<EntityFieldViewModel>();
            if (form.fields != null && form.fields.Count() > 0)
            {
                foreach (var field in form.fields.OrderBy(X => X.sr))
                {
                    if (field.Fields != null && field.Fields.Count > 0)
                    {
                        var errors = CacheFormElements(field.Fields, form);
                        errorFields.AddRange(errors);
                    }
                    else
                    {
                        var element = FindAndCachedElement(root, field, form);
                        if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                        {
                            errorFields.Clear();
                            errorFields.Add(field);
                            return errorFields;
                        }
                        else if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
                        {
                            errorFields.Add(field);
                        }
                        //if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
                        //{
                        //    errorFields.Add(field);
                        //}
                    }
                }
            }
            return errorFields;
        }
        private AppiumWebElement FindAndCachedElement(AppiumWebElement root, EntityFieldViewModel field, FormViewModel form)
        {
            AppiumWebElement element;
            elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            if (element == null)
            {
                try
                {
                    // element = root.FindElementByAccessibilityId(field.pms_field_name);
                    if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();

                        if (elements != null && elements.Count == 0)
                        {
                            element = root.FindElementByName(field.pms_field_name);
                        }
                        else if (elements != null)
                        {
                            element = elements[0];
                        }
                        else if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            element = root.FindElementByXPath(field.pms_field_xpath);
                        else
                        {
                            element = null;// throw new Exception(string.Format("{0} not found", field.field_desc));
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        element = root.FindElementByXPath(field.pms_field_xpath);
                    if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                        elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    _log.Error(exp);
                    element = null;
                    //field not mapped
                    //if (element == null && !string.IsNullOrEmpty(form.pmspagename))
                    //{
                    //    try
                    //    {
                    //        var tabControl = root.FindElementByName(form.pmspagename);
                    //        tabControl.Click();
                    //        session.desktopSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
                    //        element = root.FindElementByAccessibilityId(field.pms_field_name);
                    //        if (!elementDict.ContainsKey(field.fuid))
                    //            elementDict.Add(field.fuid, element);
                    //        //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0,0,1));
                    //        //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(field.name)));
                    //        //var tab = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(field.name)));
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        _log.Error(ex);
                    //        element = null;
                    //    }
                    //}
                }
            }
            return element;
        }

        public ResponseViewModel RetrievalFormDataFrom(MappingViewModel fillableEntity)
        {
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                _log.Info(string.Format("Data retreival process started, at {0}", UTIL.GlobalApp.CurrentLocalDateTime));
                if (root== null && session.desktopSession != null && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_Application_Path_WithName))
                    root = session.desktopSession.FindElementByXPath(UTIL.GlobalApp.PMS_Application_Path_WithName);
                if (root != null)
                {
                    AppiumWebElement element;
                    string formName = fillableEntity.entity_name;
                    //******************* Fill Fields Collection
                    this.referenceValue = string.Empty;
                    // driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
                    // this.prepareFieldsCollection(fillableEntity);
                    // Get All fields from 
                    foreach (FormViewModel frm in fillableEntity.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(x => x.sort_order))
                    {
                        fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList(), false);
                        System.Threading.Thread.Sleep(1000);
                        // List<EntityFieldViewModel> flds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.action_type != (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && (string.IsNullOrWhiteSpace(x.pms_field_expression) || !Convert.ToString(x.pms_field_expression).Contains(UTIL.BDUConstants.SCAN_NOT_REQUIRED))).OrderBy(x => x.sr).ToList();
                        List<EntityFieldViewModel> flds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.action_type != (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && x.scan != (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.NOT_REQUIRED).OrderBy(x => x.sr).ToList();
                        if (flds != null)
                        {
                            foreach (EntityFieldViewModel field in flds)
                            {
                                try
                                {
                                    string fldData = string.Empty;
                                    switch (field.control_type)
                                    {
                                        case (int)CONTROL_TYPES.TEL:
                                        case (int)CONTROL_TYPES.TEXTBOX: // Input
                                        case (int)CONTROL_TYPES.HIDDEN:
                                        case (int)CONTROL_TYPES.INCREMENT:
                                            if (field.is_reference == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED)
                                                System.Threading.Thread.Sleep(500);
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                //Research Work
                                                //var history = root.FindElementsByClassName("ProClientWin");//.Where(i => i.TagName == "ControlType.Custom").ToListhistory.findElement(By.TagName("//TableLayout[@index='1']/TableRow[@index='0']/EditText[@index='0']"));
                                                //if (history != null)
                                                //{
                                                //    var win = history[0].FindElementByClassName("ProFrame");
                                                //  // var frmWin = win.FindElementByTagName("ProFrame");
                                                //  var ele=  root.WrappedDriver.SwitchTo().Window(win.WrappedDriver.CurrentWindowHandle);
                                                //    //root.WrappedDriver.SwitchTo().Window(history.WrappedDriver.CurrentWindowHandle);
                                                //    // session.desktopSession.SwitchTo().Window(history.WrappedDriver.CurrentWindowHandle);
                                                //    var rootGrid = win.FindElementsByXPath(@".//*");
                                                //    //   var ele1 = history.WrappedDriver.FindElements(By.TagName(@"Pane"));//history.FindElement(By.XPath("//ListItem[contains(@Text, '1')]"));
                                                //    //   WindowsElement ele2 =( ele1[2] as AppiumWebElement).WrappedDriver.FindElement(By.TagName(@"Pane")) as WindowsElement;
                                                //    //   var ele3 = ele2.WrappedDriver.FindElement(By.TagName(@"Pane"));
                                                //    //   var ele4 = ele3.FindElements(By.XPath(@"//*")).Where(i => i.TagName == "ControlType.Pane");
                                                //    ////  var username = history.FindElement(By.XPath(@"//TableLayout/*'"));
                                                //    // var rows = history.FindElementsByXPath("*/*");
                                                //    foreach (AppiumWebElement row in rootGrid)
                                                //    {
                                                //        var gRows = row.FindElementsByXPath(@"*/*");


                                                //        foreach (AppiumWebElement cells in gRows)
                                                //        {
                                                //            var eles = cells.FindElements(By.XPath(@"*/*"));
                                                //            if (cells.Text.Contains("1"))
                                                //            {
                                                //                string str = cells.Text;
                                                //                // row.Click();                                                             
                                                //                break;
                                                //            }
                                                //        }
                                                //    }
                                                //}
                                                fldData = GetElementData(element, field);
                                                field.value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.GRID:
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);
                                                field.value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.DATE:// "text": // Input
                                                                     //element = FindWebElement(field);
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);

                                                try
                                                {
                                                    System.DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                                                    dt = DateTime.ParseExact(fldData, field.format, CultureInfo.CurrentCulture);
                                                    if (dt.Year > 1900)
                                                        field.value = dt.ToString(UTIL.GlobalApp.date_format);
                                                }
                                                catch (Exception)
                                                {
                                                    field.value = fldData;
                                                }

                                                //try
                                                //{
                                                //    System.DateTime date = Convert.ToDateTime(string.IsNullOrWhiteSpace(fldData) ? UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_format) : fldData);
                                                //    field.value = date.ToString(UTIL.GlobalApp.date_format);
                                                //}
                                                //catch (Exception ex)
                                                //{
                                                //    field.value = fldData; 
                                                //}

                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_format);
                                            }

                                            break;
                                        case (int)CONTROL_TYPES.DATETIME:// "text": // Input
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);

                                                try
                                                {
                                                    System.DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                                                    dt = DateTime.ParseExact(fldData, field.format, CultureInfo.CurrentCulture);
                                                    if (dt.Year > 1900)
                                                        field.value = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                }
                                                catch (Exception)
                                                {
                                                    field.value = fldData;
                                                }

                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_time_format);
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.SELECT:
                                        case (int)CONTROL_TYPES.CHECKBOX:// "text": // Input
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);
                                                string value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                                field.value = value;
                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = string.IsNullOrWhiteSpace(field.default_value) ? "0" : field.default_value;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.LISTBOX:// "select":// Select
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);
                                                string value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                                field.value = value;
                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = field.default_value;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.RADIO:// "select":// Select
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);
                                                string value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                                field.value = value;
                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = string.IsNullOrWhiteSpace(field.default_value) ? "0" : field.default_value;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.RADIO_GROUP:// RQDIO
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
                                                fldData = GetElementData(element, field);
                                                string value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                                field.value = value;
                                            }
                                            else if (field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY)
                                            {
                                                field.value = string.IsNullOrWhiteSpace(field.default_value) ? "0" : field.default_value;
                                            }
                                            break;
                                        case (int)CONTROL_TYPES.ACTION:// Select
                                            element = FindScanningElementWithFill(root, field);
                                            // element = driver.FindElement(By.Id(field.pms_field_name));
                                            if (element != null)
                                            {
                                                element.Click();
                                                element.Submit();
                                            }
                                            break;
                                        default:
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null && (field.control_type != (int)UTIL.Enums.CONTROL_TYPES.FORM && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.URL && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE))
                                            {
                                                fldData = GetElementData(element, field);
                                                field.value = string.IsNullOrWhiteSpace(fldData) && field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY ? field.default_value : fldData;
                                            }
                                            break;
                                    }

                                    // Here need to get Reference column

                                    if (field.is_reference == (int)UTIL.Enums.STATUSES.Active)
                                    {
                                        if (!string.IsNullOrWhiteSpace(fldData))
                                        {
                                            field.value = fldData;
                                            referenceValue = fldData;
                                            fillableEntity.reference = fldData;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (field.mandatory == (int)UTIL.Enums.STATUSES.Active && string.IsNullOrWhiteSpace(field.value))
                                    {
                                        if (string.IsNullOrWhiteSpace(field.value))
                                            field.value = field.default_value;
                                    }
                                }
                            }// Fields foreach
                        }
                    }
                    //Auto reference no 
                    if (string.IsNullOrWhiteSpace(this.referenceValue) || referenceValue.Contains("new"))
                    {
                        Random generator = new Random();
                        String rndomn = generator.Next(1000, 9999).ToString("D4");
                        referenceValue = rndomn;
                        fillableEntity.reference = string.IsNullOrWhiteSpace(fillableEntity.reference) ? rndomn : fillableEntity.reference;
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
        private AppiumWebElement FindScanningElementWithFill(AppiumWebElement root, EntityFieldViewModel field)
        {
            AppiumWebElement element = null;//Disabled bc it need proper caching,  always emty or bad data
                                            // elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            if (element == null)
            {
                try
                {

                    if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();
                        // element = root.FindElement(By.Id(field.pms_field_name));
                        if (elements != null)
                        {
                            element = elements[0];
                        }
                        if (element == null)
                        {
                            elements = root.FindElements(By.Name(field.pms_field_name)).ToList();
                        }
                        if (elements != null)
                        {
                            element = elements[0];
                        }
                        if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                        {
                            //  var ctrls = root.FindElements(By.XPath(field.pms_field_xpath));
                            element = root.FindElement(By.XPath(field.pms_field_xpath));
                        }
                    }
                    else
                    {
                        element = root.FindElement(By.XPath(field.pms_field_xpath));
                    }

                    if (element != null)
                    {
                        string fldValue = string.IsNullOrWhiteSpace(field.value) ? field.default_value : field.value;
                        if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 1));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            // element.Click();
                            // System.Threading.Thread.Sleep(40);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 1));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                            // element.Submit();
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 1));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            // element.Click();
                            //System.Threading.Thread.Sleep(40);
                        }
                    }
                    if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                        elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    _log.Error(exp);
                    element = null;
                }
            }
            return element;
        }
        private AppiumWebElement FindAndCachedElementWithFill(AppiumWebElement root, EntityFieldViewModel field, FormViewModel form, bool needFill = false)
        {
            AppiumWebElement element;
            elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            if (element != null)
            {
                try
                {
                    string tag = element.TagName;
                }
                catch (Exception ex)
                {
                    element = null;
                }
            }
            if (element == null || needFill)
            {
                try
                {
                    if (element == null)
                    {

                        if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        {
                            var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();
                            // var elements = root.FindElements(By.Id(field.pms_field_name));
                            if (elements != null && elements.Any())
                            {
                                element = elements[0];
                            }
                            if (element == null)
                            {
                                // elements= root.FindElementsByName(field.pms_field_name);
                                elements = root.FindElements(By.Name(field.pms_field_name)).ToList();
                                element = elements != null && elements.Any() ? elements[0] : null;
                            }
                            if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                elements = root.FindElements(By.XPath(field.pms_field_xpath)).ToList();
                                element = elements != null && elements.Any() ? elements[0] : null;
                            }
                        }
                        else
                        {
                            // element = root.FindElementByXPath(field.pms_field_xpath);
                            element = root.FindElement(By.XPath(field.pms_field_xpath));    
                        }
                    }// Element not cached then find condition

                    // Applying Validation Rule for Mandatory Control
                    if (element != null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL && !element.Enabled)
                        element = null;
                    if (needFill && element != null)
                    {
                        string fldValue = string.IsNullOrWhiteSpace(field.value) && field.mandatory == (int)UTIL.Enums.STATUSES.Active ? field.default_value : field.value;
                        if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(100);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                element.Click();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(100);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                element.Click();
                            }
                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(100);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 20));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                System.Threading.Thread.Sleep(800);
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(100);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 20));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                System.Threading.Thread.Sleep(800);
                            }

                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                            }

                            // System.Threading.Thread.Sleep(10);

                            //element.Submit();
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            }
                            // System.Threading.Thread.Sleep(10);

                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(fldValue) || field.mandatory == (int)UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY || (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OPTIONAL && !string.IsNullOrWhiteSpace(field.default_value)))
                            {

                                this.SetElement(element, field);
                                if (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_CONFIRM && field.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE)
                                {
                                    System.Threading.Thread.Sleep(100);
                                    // element.SendKeys(Keys.End);                                   
                                    element.SendKeys(Keys.Enter);
                                    //WebDriverWait waitl = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                                    //waitl.Until(ExpectedConditions.ElementToBeClickable(element));
                                }
                            }
                            //  element.SendKeys(fldValue);
                        }
                    }
                    if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                        elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    _log.Error(exp);
                    element = null;
                }
            }
            return element;
        }
        #endregion

        #region Events
        private bool StartServer()
        {
            try
            {
                _log.Warn("PMS Server start requested.");
                String command = Path.Combine(rootFolder + @"Drivers\WinAppDriver.exe");//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                                                                                        //  String command = @"D:\Shared\BDUV3.0\RobotAutoApp\Driver\WinAppDriver.exe";
                if (System.IO.File.Exists(command))
                {
                    Process p = new Process();
                    // Configure the process using the StartInfo properties.
                    p.StartInfo.FileName = command;

                    var process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.FileName = "cmd.exe";
                    // startInfo.WorkingDirectory = @"..\..\..\..\RobotAutoApp\Driver\";
                    startInfo.WorkingDirectory = Path.Combine(rootFolder, @"\Drivers\");
                    startInfo.Arguments = $" / C WinAppDriver 4723 > log.txt";
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.UseShellExecute = true;
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return false;
            }
            return true;
        }
        private bool StopServer()
        {
            var stopped = true;
            try
            {
                _log.Info("PMS Server start requested.");
                Process[] proc = Process.GetProcessesByName("WinAppDriver.exe");
                proc[0].Kill();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                stopped = false;
            }
            return stopped;
        }

        public void StartCaptering()
        {


            if (ScanningEntities != null)
            {
                definitionEntities = new List<MappingDefinitionViewModel>();
                foreach (MappingViewModel ety in ScanningEntities.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active))
                {
                    //if (ety.status == (int)UTIL.Enums.STATUSES.Active)
                    //{
                    foreach (FormViewModel frm in ety.forms)
                    {
                        List<EntityFieldViewModel> fldsls = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE).ToList();
                        foreach (EntityFieldViewModel flds in fldsls)
                        {
                            MappingDefinitionViewModel defEntity = new MappingDefinitionViewModel { entity_Id = (ety.entity_Id == 0 ? Convert.ToInt32(ety.id) : ety.entity_Id), pmsformid = Convert.ToString(frm.pmspageid), xpath = Convert.ToString(ety.xpath) };
                            defEntity.SubmitCaptureFieldId = flds.pms_field_name;
                            defEntity.SubmitCaptureFieldXpath = flds.pms_field_xpath;
                            defEntity.SubmitCaptureFieldExpression = flds.pms_field_expression;
                            definitionEntities.Add(defEntity);
                        }

                    }
                }//foreach (MappingViewModel ety in scanningEntities) {

            }


            //SetUiForRecorderUiStateIsRecording();
            SetUiForRecorderUiStateIsReady();
            _generationClient.InitializeRecording(new HookEventHandlerSettings { HasGraphicThreadLoop = true });
        }

        public void StartCaptering(List<MappingViewModel> lst)
        {

            ScanningEntities = lst;
            if (ScanningEntities != null)
            {
                definitionEntities = new List<MappingDefinitionViewModel>();
                foreach (MappingViewModel ety in ScanningEntities)
                {
                    if (ety.status == (int)UTIL.Enums.STATUSES.Active)
                    {
                        MappingDefinitionViewModel defEntity = new MappingDefinitionViewModel { entity_Id = ety.entity_Id, pmsformid = ety.pmsformid, xpath = ety.xpath };
                        foreach (FormViewModel frm in ety.forms)
                        {
                            if (frm.Status == (int)UTIL.Enums.STATUSES.Active)
                            {
                                foreach (EntityFieldViewModel flds in frm.fields)
                                {
                                    if (frm.Status == (int)UTIL.Enums.STATUSES.Active && flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE)
                                    {
                                        defEntity.SubmitCaptureFieldId = flds.pms_field_name;
                                        defEntity.SubmitCaptureFieldXpath = flds.pms_field_xpath;
                                    }
                                }
                            }
                        }
                        // definitionEntities.Add(defEntity);
                        // SetUiForRecorderUiStateIsReady();
                    }//foreach (MappingViewModel ety in scanningEntities) {
                }

            }


            SetUiForRecorderUiStateIsReady();
            _generationClient.InitializeRecording(new HookEventHandlerSettings { HasGraphicThreadLoop = true });
        }
        public void StopCaptering()
        {
            SetUiForRecorderUiStateIsStopped();
            SetUiForRecorderUiStateIsDefault();
            if (_generationClient != null) // siddique
            {
                _generationClient.TerminateRecording();
            }

            // lastUIEvent = null;
            //ResetForm();
        }

        private void SetupGenerationClient()
        {
            if (_generationClient == null)
            {
                _generationClient = new GenerationClient(new GenerationClientSettings
                {
                    ProcessId = Process.GetCurrentProcess().Id,
                    AutomationTransactionTimeout = new TimeSpan(0, 1, 0),
                });

                _generationClientHookProcedureEventHandler = GenerationClientHookProcedure;
                // scanAndSubmitActionFound += GenerationClientHookProcedure;
                _generationClient.GenerationHookProcedureEventHandler += _generationClientHookProcedureEventHandler;

                _generationClientUiEventEventHandler = GenerationClientUiEvent;
                _generationClient.GenerationUiEventEventHandler += _generationClientUiEventEventHandler;
            }
        }
        private void PauseGenerationClient()
        {
            if (_generationClient != null)
            {

                // _generationClientHookProcedureEventHandler = GenerationClientHookProcedure;
                _generationClient.GenerationHookProcedureEventHandler -= _generationClientHookProcedureEventHandler;

                // _generationClientUiEventEventHandler = GenerationClientUiEvent;
                _generationClient.GenerationUiEventEventHandler -= _generationClientUiEventEventHandler;
            }
        }
        private void ResumeGenerationClient()
        {
            if (_generationClient != null)
            {

                // _generationClientHookProcedureEventHandler = GenerationClientHookProcedure;
                _generationClient.GenerationHookProcedureEventHandler += _generationClientHookProcedureEventHandler;

                // _generationClientUiEventEventHandler = GenerationClientUiEvent;
                _generationClient.GenerationUiEventEventHandler += _generationClientUiEventEventHandler;
            }
        }
        private void GenerationClientHookProcedure(object sender, EventArgs e)
        {
            if (_generationClient.IsPaused)
            {
                SetUiForRecorderUiStateIsStopped();
            }
            else
            {
                SetUiForRecorderUiStateIsReady();
                //SetUiForRecorderUiStateIsRecording();
            }
        }

        private void SetUiForRecorderUiStateIsStopped()
        {
            // _recorderUiState = ROBOT_UI_STATUS.STOPPED;
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.STOPPED;
        }

        private void SetUiForRecorderUiStateIsRecording()
        {
            try
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
                //if (scanThread != null)
                //    scanThread.Suspend();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            //  _recorderUiState = ROBOT_UI_STATUS.SCANNING;
        }

        private void SetUiForRecorderUiStateIsDefault()
        {
            // _recorderUiState = ROBOT_UI_STATUS.DEFAULT;
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;
        }
        private void SetUiForRecorderUiStateIsReady()
        {

            try
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                //if (scanThread != null)
                //    scanThread.Resume();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

        }
        private void SetUiForRecorderUiStateDataFeeding()
        {
            // _recorderUiState = ROBOT_UI_STATUS.FEEDING_DATA;
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
        }

        /// <summary>
        /// Handler for new Events being created from WinAppDriver.Generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerationClientUiEvent(object sender, UiEventEventArgs e)
        {
            if (MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && e.UiEvent != null && e.UiEvent.UiElement != null && submitControls.Contains(e.UiEvent.UiElement.LocalizedControl) && definitionEntities.Where(x => x.SubmitCaptureFieldId == e.UiEvent.UiElement.Name).FirstOrDefault() != null)
            {
                //ScanAndFoundData scan = new ScanAndFoundData(ScanAndCaptureFound);
                //scan.Invoke(e.UiEvent);
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
                Thread thread = new Thread(() => ScanAndCaptureFound(e.UiEvent));
                thread.Start();
                //ScanAndFoundData scan = ScanAndCaptureFound;
                //scan(e.UiEvent);
            }



        }

        private void ScanAndCaptureFound(UiEvent curUIEvent)
        
        {

            // if ((MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            if (MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            {
                try
                {
                    //PauseGenerationClient();
                    string controlName = string.IsNullOrWhiteSpace(curUIEvent.UiElement.Name) ? curUIEvent.UiElement.AutomationId : curUIEvent.UiElement.Name;
                    MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => controlName.ToLower() == x.SubmitCaptureFieldId.ToLower() && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldXpath) || root.FindElementsByXPath(x.SubmitCaptureFieldXpath).Any()) ).FirstOrDefault();
                    if (captureEntity != null)
                    {
                        try
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 55));
                            wait.Until(ExpectedConditions.AlertIsPresent());

                        }
                        catch (Exception ex) { }
                        // captureEntity.SubmitCaptureFieldExpression = string.Empty;
                        // Need to Check other parameters to avoid wrong attemp like GO
                        if (!string.IsNullOrWhiteSpace(captureEntity.SubmitCaptureFieldExpression) && captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                        {

                            if (this.SubmitAndCaptureSecondarNotEqualyParameterCheck(captureEntity))
                                return;
                        }
                        else if (!string.IsNullOrWhiteSpace(captureEntity.SubmitCaptureFieldExpression))
                        {
                            if (!this.SubmitAndCaptureSecondaryParameterCheck(captureEntity))
                                return;
                        }

                        SetUiForRecorderUiStateIsRecording();
                        MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "PMS Data scan process started..");
                            //isSubmiting = true;

                            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
                            {
                                this.referenceValue = string.Empty;
                                MappingViewModel mObj = new MappingViewModel();
                                mObj = mapping.DCopy();
                                mObj.createdAt = UTIL.GlobalApp.CurrentLocalDateTime;
                                ResponseViewModel res = RetrievalFormDataFrom(mObj);
                                if (res.status && mObj != null && !string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending '{0}' to IntegrateX.", mObj.reference));

                                    //if (string.IsNullOrWhiteSpace(mObj.reference))
                                    //{
                                    //    Random generator = new Random();
                                    //    String rndomn = generator.Next(0, 100000000).ToString("D8");
                                    //    mObj.reference = rndomn;
                                    //}
                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                        this.DataSaved(mObj);
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process finished with incomplete data..");
                                }
                                else if (res.status && mObj != null && mObj.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS && string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    // this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending '{0}' to IntegrateX.", mObj.reference));

                                    if (string.IsNullOrWhiteSpace(mObj.reference))
                                    {
                                        Random generator = new Random();
                                        String rndomn = generator.Next(0, 100000000).ToString("D8");
                                        mObj.reference = rndomn;
                                    }
                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                    {
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending '{0}' to IntegrateX.", mObj.reference));
                                        this.DataSaved(mObj);
                                    }
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process finished with incomplete data..");
                                }
                                else
                                {
                                    // mObj.status = 0;
                                    mObj.xpath = res.message;
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan and retrieval process completed..");
                                    // this.DataSaved(null);
                                }
                                mObj = null;
                                mapping = null;
                                //SetUiForRecorderUiStateIsDefault();
                            }
                        }///  if (mapping != null)                       
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                finally
                {
                    // ResumeGenerationClient();
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                }
            }
        }
        //private void MouseKeyEventHandler(UiEvent curUIEvent)
        //{

        //    if ((MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
        //    {
        //        try
        //        {
        //            PauseGenerationClient();

        //            string controlName = string.IsNullOrWhiteSpace(curUIEvent.UiElement.Name) ? curUIEvent.UiElement.AutomationId : curUIEvent.UiElement.Name;


        //            MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => controlName.ToLower() == "submit".ToLower()).FirstOrDefault();

        //            if (captureEntity != null)
        //            {
        //                captureEntity.SubmitCaptureFieldExpression = string.Empty;
        //                // Need to Check other parameters to avoid wrong attemp like GO
        //                if (!string.IsNullOrWhiteSpace(captureEntity.SubmitCaptureFieldExpression))
        //                {
        //                    if (!this.SubmitAndCaptureSecondaryParameterCheck(captureEntity))
        //                        return;
        //                }

        //                SetUiForRecorderUiStateIsRecording();
        //                MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
        //                if (mapping != null)
        //                {
        //                    this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "PMS Data scan process started..");
        //                    //isSubmiting = true;

        //                    if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
        //                    {
        //                        ResponseViewModel res = RetrievalFormDataFrom(mapping);
        //                        if (res.status && mapping != null)
        //                        {
        //                            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending '{0}' to IntegrateX.", mapping.reference));
        //                            //if (!string.IsNullOrWhiteSpace(session.pmsReferenceExpression) || mapping.reference.Contains("new"))
        //                            //{
        //                            //    System.Threading.Thread.Sleep(2000);                               
        //                            //    mapping.reference = "121308865";
        //                            //    //session.referenceValue = mapping.reference;
        //                            //}

        //                            if (string.IsNullOrWhiteSpace(mapping.reference))
        //                            {
        //                                Random generator = new Random();
        //                                String rndomn = generator.Next(0, 100000000).ToString("D8");
        //                                mapping.reference = rndomn;
        //                            }
        //                            if (!mapping.reference.ToLower().Contains("new"))
        //                                this.DataSaved(mapping);
        //                            else
        //                                this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process finished with incomplete data..");
        //                        }
        //                        else
        //                        {
        //                            mapping.status = 0;
        //                            mapping.xpath = res.message;
        //                            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, "PMS data scan & retrieval process completed..");
        //                            // this.DataSaved(null);
        //                        }
        //                        mapping = null;
        //                        //SetUiForRecorderUiStateIsDefault();
        //                    }
        //                }///  if (mapping != null)
        //                //  lastUIEvent = null;
        //                // lastUIEvent = curUIEvent; // will be scanned on next focus
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _log.Error(ex);
        //        }
        //        finally
        //        {
        //            ResumeGenerationClient();
        //            SetUiForRecorderUiStateIsReady();
        //        }
        //    }
        //}
        #endregion

        #region Set Data To Form
        public bool controlExistanceCheckFromExpression(string expression)
        {
            bool rStatus = false;
            try
            {
                // IWebDriver CDriver =  root.WrappedDriver.SwitchTo().Window(root.WrappedDriver.CurrentWindowHandle);
                // captureEntity.SubmitCaptureFieldExpression= captureEntity.SubmitCaptureFieldExpression.Replace("feed=0", "").Replace(";", "");
                if (expression.Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                {
                    string[] parameters = expression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                    foreach (string pMeter in parameters)
                    {

                        // var item = CDriver.FindElements(By.Name("Guest Card Files"));

                        if (pMeter.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                        {
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                            switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                            {
                                case "name":
                                    if (root.FindElements(By.Name(Convert.ToString(spMeters[1]))).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "xpath":
                                    if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "automationid":
                                    if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                            }
                            if (rStatus)
                            {
                                //rStatus = rStatus ? false : true;
                                break;
                            }
                        }
                        else if (pMeter.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                        {
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                            switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                            {
                                case "name":
                                    if (root.FindElements(By.Name(Convert.ToString(spMeters[1]))).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "xpath":
                                    if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "automationid":
                                    if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                            }
                        }
                        if (rStatus) break;
                    }//foreach (string pMeter in parameters) {
                }

                else if (expression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                {
                    string[] spMeters = expression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                    switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name((Convert.ToString(spMeters[1]).Trim()))).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;

                    }
                    //rStatus = rStatus ? false : true;
                }
                else if (expression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                {
                    string[] spMeters = expression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                    switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name((Convert.ToString(spMeters[1]).Trim()))).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                rStatus = false;
            }

            if (expression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                rStatus = rStatus ? false : true;
            //root.WrappedDriver.SwitchTo().DefaultContent();
            return rStatus;

        }
        public bool SubmitAndCaptureSecondarNotEqualyParameterCheck(MappingDefinitionViewModel captureEntity)
        {
            bool rStatus = false;
            try
            {

                IWebDriver CDriver = root.WrappedDriver.SwitchTo().Window(root.WrappedDriver.CurrentWindowHandle);
                // captureEntity.SubmitCaptureFieldExpression= captureEntity.SubmitCaptureFieldExpression.Replace("feed=0", "").Replace(";", "");
                if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                {
                    string[] parameters = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                    foreach (string pMeter in parameters)
                    {

                        // var item = CDriver.FindElements(By.Name("Guest Card Files"));

                        if (pMeter.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                        {
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                            switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                            {
                                case "name":
                                    if (CDriver.FindElements(By.Name(Convert.ToString(spMeters[1]))).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "xpath":
                                    if (CDriver.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                                case "automationid":
                                    if (CDriver.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        rStatus = true;
                                    }
                                    break;
                            }
                        }
                        if (rStatus) break;
                    }//foreach (string pMeter in parameters) {
                }

                else if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                {
                    string[] spMeters = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                    switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                    {
                        case "name":
                            if (CDriver.FindElements(By.Name((Convert.ToString(spMeters[1]).Trim()))).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "xpath":
                            if (CDriver.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;
                        case "automationid":
                            if (CDriver.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                rStatus = true;
                            }
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                rStatus = false;
            }
            //root.WrappedDriver.SwitchTo().DefaultContent();
            return rStatus;

        }
        public bool SubmitAndCaptureSecondaryParameterCheck(MappingDefinitionViewModel captureEntity)
        {
            bool rStatus = false;
            bool found = false;
            try
            {

                //  IWebDriver CDriver = root.WrappedDriver.SwitchTo().Window(root.WrappedDriver.CurrentWindowHandle);
                // captureEntity.SubmitCaptureFieldExpression= captureEntity.SubmitCaptureFieldExpression.Replace("feed=0", "").Replace(";", "");
                if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                {
                    string[] parameters = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                    foreach (string pMeter in parameters)
                    {

                        // var item = CDriver.FindElements(By.Name("Guest Card Files"));

                        if (pMeter.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                        {
                            rStatus = true;
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                            switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                            {
                                case "name":
                                    if (root.FindElements(By.Name(Convert.ToString(spMeters[1]))).Count > 0)
                                    {
                                        found = true;
                                        rStatus = false;
                                    }
                                    break;
                                case "xpath":
                                    if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        found = true;
                                        rStatus = false;
                                    }
                                    break;
                                case "automationid":
                                    if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        found = true;
                                        rStatus = false;
                                    }
                                    break;
                            }
                        }
                        else if (pMeter.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                        {
                            rStatus = false;
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                            switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                            {
                                case "name":
                                    if (root.FindElements(By.Name(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        found = true;
                                        rStatus = true;
                                    }
                                    break;
                                case "xpath":
                                    if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        found = true;
                                        rStatus = true;
                                    }
                                    break;
                                case "automationid":
                                    if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        found = true;
                                        rStatus = true;
                                    }
                                    break;
                            }
                        }
                        if (found) break;
                    }//foreach (string pMeter in parameters) {
                }

                else if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                {
                    rStatus = true;
                    string[] spMeters = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                    switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name((Convert.ToString(spMeters[1]).Trim()))).Count > 0)
                            {
                                found = true;
                                rStatus = false;
                            }
                            break;
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                found = true;
                                rStatus = false;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                found = true;
                                rStatus = false;
                            }
                            break;

                    }
                }
                else if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                {
                    rStatus = false;
                    string[] spMeters = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                    switch (Convert.ToString(spMeters[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                found = true;
                                rStatus = true;
                            }
                            break;
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                found = true;
                                rStatus = true;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                found = true;
                                rStatus = true;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                rStatus = false;
            }
            //root.WrappedDriver.SwitchTo().DefaultContent();
            return rStatus;

        }
        public ResponseViewModel FeedDataToDesktopForm(MappingViewModel formData)
        {
            ResponseViewModel rs = new ResponseViewModel();
            //var formData = LoadData();            
            //var root = session.FindElementByAbsoluteXPath(formData.xpath);
            if (session != null && session.desktopSession != null)
            {
                MappingViewModel forData = formData.DCopy();
                List<EntityFieldViewModel> errorFields = CacheFormElementsWithFill(forData, true);

                if (errorFields.Count == 0)
                {
                    rs.jsonData = null;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Successfully form filled.";
                }
                else if (errorFields.Where(x => x.mandatory == (int)UTIL.Enums.STATUSES.Active).Count() > 0)
                {

                    rs.jsonData = null;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                    rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                    // rs.message = "Failed to submit data to PMS system.";
                }
                //if (SetFormData(formDataToFillup, root))
                //{
                //    status = true;
                //    rs.jsonData = null;
                //    rs.status = true;
                //    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                //    rs.message = "Successfully form filled.";
                //}
            }
            //if (status && !string.IsNullOrEmpty(formData.submit_action))
            //{
            //    var submitElement = session.desktopSession.FindElementByAccessibilityId(formData.submit_action);
            //    if (submitElement != null)
            //    {
            //        submitElement.Click();
            //    }
            //}
            //if (!status)
            //{

            //    rs.status = false;
            //    rs.jsonData = JsonSerializer.Serialize(formData);
            //    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
            //    rs.message = "Error: form could not be filled.";
            //}
            return rs;
        }


        public ResponseViewModel FeedDataToDesktopForm(List<MappingViewModel> entls)
        {
            ResponseViewModel rs = new ResponseViewModel();
            var status = false;
            try
            {
                this.SetUiForRecorderUiStateDataFeeding();
                if (session != null && session.desktopSession != null)
                {
                    foreach (MappingViewModel mapping in entls)
                    {
                        List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                        MappingViewModel formDataToFillup = mapping.DCopy();
                        this.referenceValue = formDataToFillup.reference;
                        errorFields = CacheFormElementsWithFill(formDataToFillup, true);
                        if (errorFields.Count == 0)
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                            rs.message = "Successfully form filled.";
                        }
                        else if (errorFields.Where(x => x.mandatory == (int)UTIL.Enums.STATUSES.Active).Count() > 0)
                        {

                            rs.jsonData = null;
                            rs.status = false;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                            string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                            rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                            // rs.message = "Failed to submit data to PMS system.";
                        }
                        //if (SetFormData(formDataToFillup, root))
                        //{
                        //    status = true;
                        //    rs.jsonData = null;
                        //    rs.status = true;
                        //    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                        //    rs.message = "Successfully form filled.";
                        //}
                        //if (!status)
                        //{
                        //    rs.status = false;
                        //    rs.jsonData = JsonSerializer.Serialize(formDataToFillup);
                        //    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                        //    rs.message = "Error: form could not be filled.";
                        //}
                        formDataToFillup = null;
                    }
                }
            }// try
            catch (Exception ex)
            {
                _log.Error(ex);

            }
            finally
            {
                this.SetUiForRecorderUiStateIsReady();
            }
            //if (status && !string.IsNullOrEmpty(formData.submit_action))
            //{
            //    var submitElement = session.desktopSession.FindElementByAccessibilityId(formData.submit_action);
            //    if (submitElement != null)
            //    {
            //        submitElement.Click();
            //    }
            //}
            return rs;
        }

        public bool SetFormData(MappingViewModel formData, AppiumWebElement root) // rename by siddique
        {
            _log.Info("PMS form data feed started.");
            bool bSuccess = false;
            try
            {
                foreach (FormViewModel frm in formData.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active))
                {
                    fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList());

                    // List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && (string.IsNullOrWhiteSpace(x.pms_field_expression) || !Convert.ToString(x.pms_field_expression).Contains(UTIL.BDUConstants.FEED_NOT_REQUIRED))).OrderBy(X => X.sr).ToList();
                    List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed != (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.NOT_REQUIRED).OrderBy(X => X.sr).ToList();
                    if (candidateFlds != null)
                        SetFieldData(candidateFlds, root);
                    candidateFlds = null;
                }
                //test complete
                bSuccess = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            _log.Info("PMS form data feed finished.");
            return bSuccess;
        }

        public void fillKeywordsDictionary(List<EntityFieldViewModel> flds, bool forTestRun = false) // Keyword Fill Up
        {
            try
            {
                if (flds != null)
                {
                    List<EntityFieldViewModel> keyWordFields = flds.Where(x => Convert.ToString(x.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX)).ToList();

                    foreach (EntityFieldViewModel fld in keyWordFields)
                    {
                        EntityFieldViewModel keywordFld = null;

                        // REference
                        if (!forTestRun && Convert.ToString(fld.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX))// Not for Test
                        {
                            if (BDU.UTIL.BDUConstants.RES_REFERENCE == fld.field_desc)
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
                            else if (BDU.UTIL.BDUConstants.GUEST_NAME == fld.field_desc)
                            {
                                keywordFld = flds.Where(x => Convert.ToString(x.fuid) == fld.fuid && Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.GUEST_NAME).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
                                    }
                                    else
                                        fld.default_value = string.Empty;

                                }
                            }
                            else if (BDU.UTIL.BDUConstants.ROOM_NO == fld.field_desc)
                            {
                                keywordFld = flds.Where(x => Convert.ToString(x.fuid) == fld.fuid && Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.ROOM_NO).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
                                if (keywordFld != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(keywordFld.value))
                                    {
                                        fld.default_value = keywordFld.value;
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
                                }// Multiple
                                else if (Convert.ToString(fld.pms_field_expression).Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
                                {
                                    if (!(fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_FEED) || fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_SCAN)))
                                    {
                                        string[] KeyValues = fld.pms_field_expression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
                                        if (KeyValues.Length > 1)
                                        {
                                            if (Convert.ToString(KeyValues[0]).ToLower() == Convert.ToString(fld.default_value).ToLower())
                                                fld.default_value = KeyValues[1];
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

        public bool SetFieldData(ICollection<EntityFieldViewModel> fields, AppiumWebElement root) // rename by siddique
        {
            bool bSuccess = false;
            try
            {
                // Check Applied for active & execution in defined order
                foreach (EntityFieldViewModel fld in fields)//.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL).OrderBy(O => O.sr))
                {
                    if (fld.Fields != null && fld.Fields.Count > 0)
                    {
                        SetFieldData(fld.Fields, root);
                    }
                    else
                    {
                        //AppiumWebElement element = session.desktopSession.FindElementByAccessibilityId(data.fieldname);CORE
                        AppiumWebElement element = GetCachedElement(root, fld);
                        if (element != null && (!string.IsNullOrWhiteSpace(fld.value) || (fld.mandatory == (int)UTIL.Enums.STATUSES.Active || fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OPTIONAL)))
                        {
                            SetElement(element, fld);
                            System.Threading.Thread.Sleep(6);
                            if (fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_CONFIRM && fld.status == (int)UTIL.Enums.STATUSES.Active)
                            {
                                element.SendKeys(Keys.End);
                                System.Threading.Thread.Sleep(5);
                                element.SendKeys(Keys.Enter);
                            }
                            else if (fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_OK && fld.status == (int)UTIL.Enums.STATUSES.Active)
                            {
                                System.Threading.Thread.Sleep(5);
                                IAlert alert = session.desktopSession.SwitchTo().Alert();
                                if (alert != null)
                                    alert.Accept();
                                session.desktopSession.SwitchTo().DefaultContent();
                            }
                            else if (fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OK && fld.status == (int)UTIL.Enums.STATUSES.Active)
                            {
                                System.Threading.Thread.Sleep(5);
                                IAlert alert = session.desktopSession.SwitchTo().Alert();
                                if (alert != null)
                                    alert.Accept();
                                session.desktopSession.SwitchTo().DefaultContent();
                                // element.SendKeys(Keys.Enter);
                            }
                        }
                    }
                    //Paste generated code here
                }
                bSuccess = true;
                // isDataFeeded = bSuccess;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return bSuccess;
        }

        private AppiumWebElement GetCachedElement(AppiumWebElement root, EntityFieldViewModel field)
        {
            AppiumWebElement element = null;
            elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            if (element != null)
                try { string tag = element.TagName; } catch (Exception) { element = null; };
            if (element == null)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                    {
                        // var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();

                        if (root.FindElementsByAccessibilityId(field.pms_field_name).Count > 0)
                        {
                            element = root.FindElementByAccessibilityId(field.pms_field_name);
                        }
                        else if (root.FindElementsByName(field.pms_field_name).Count > 0)
                        {
                            element = root.FindElementByName(field.pms_field_name);
                        }
                        else if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath) && root.FindElementsByXPath(field.pms_field_xpath).Count > 0)
                        {
                            element = root.FindElementByXPath(field.pms_field_xpath);
                        }
                        else
                        {
                            element = null;
                        }
                    }
                    else
                    {
                        element = root.FindElementByXPath(field.pms_field_xpath);
                    }
                    if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                        elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    //field not mapped
                    //if (element == null && !string.IsNullOrEmpty(field.pms_field_name))
                    //{
                    //    try
                    //    {

                    //        var tabCard2 = root.FindElementByName(field.pms_field_name);
                    //        tabCard2.Click();
                    //        session.desktopSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(100);
                    //        element = root.FindElementByAccessibilityId(field.pms_field_name);
                    //        if (!elementDict.ContainsKey(field.fuid))
                    //            elementDict.Add(field.fuid, element);
                    //        //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0,0,1));
                    //        //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id(field.name)));
                    //        //var tab = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(field.name)));
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        _log.Error(ex);
                    //        element = null;
                    //    }
                    //}
                    _log.Error(exp);
                    element = null;
                }
            }
            return element;
        }

        //public MappingViewModel LoadData()
        //{
        //    var fieldsJson = Path.Combine(rootFolder, @"Data.json");
        //    string json = File.ReadAllText(fieldsJson);
        //    Console.WriteLine(json);
        //    Console.Write("Loading Data");
        //    var data = JsonSerializer.Deserialize<MappingViewModel>(json);
        //    return data;
        //}

        private string SetElement(AppiumWebElement element, EntityFieldViewModel fld)
        {
            // var value = fieldData.value;(fld.mandatory == (int)UTIL.Enums.STATUSES.Active || fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OPTIONAL)
            var value = string.IsNullOrWhiteSpace(fld.value) && (fld.mandatory == (int)UTIL.Enums.STATUSES.Active || fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_OPTIONAL) ? "" + fld.default_value : "" + fld.value;
            string data = "";
            string output = "";
            if (element != null)
            {
                try
                {
                    if (element.Enabled && (element.TagName == "ControlType.Text" || element.TagName == "ControlType.Edit") && !string.IsNullOrWhiteSpace(value))
                    {
                        //Actions actions = new Actions(session.desktopSession);
                       // actions.DoubleClick(element).Perform();
                       // element.SendKeys(Keys.dou(Keys.CONTROL, "a") Keys.Control + "a"); ;//;
                       // element.SendKeys(Keys.Delete);
                      //element.SendKeys(Keys.Control + "a");
                        element.Clear();
                    }
                    else if (element.Enabled)
                        element.Clear();
                    // element.SendKeys(Keys.Control + "a");     //select all text in textbox 

                }
                catch (Exception ex)
                {
                    // TODO
                }
            }
            if (element != null && element.TagName == "ControlType.Text")
            {
                Paste(element, value);
                //WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 4));
                //waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));

            }
            else if (element != null && element.TagName == "ControlType.Table")
            {
                // List<AppiumWebElement> cells = element.FindElements(By.XPath(".//*")).ToList();
                List<AppiumWebElement> gRows = element.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Custom").ToList();
                foreach (AppiumWebElement row in gRows)
                {
                    if (row.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Edit").Where(c => c.Text.Contains(value)).Any())
                    {
                        //Actions select = new Actions(row.WrappedDriver);
                        row.Click();
                        //select.SendKeys(Keys.Enter);
                        //select.Perform();
                        // bool sel = row.Selected;
                        break;
                    }
                }

            }
            else if (element != null && element.TagName == "ControlType.Edit")
            {
                if (element.Enabled)
                {

                    if (!string.IsNullOrWhiteSpace(value) && fld.control_type == (int)CONTROL_TYPES.DATE)
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
                        else if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("d"))
                        {
                            dt = DateTime.Parse(fld.default_value);
                        }
                        else
                            dt = DateTime.Parse(value);

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
                        if (string.IsNullOrWhiteSpace(fld.value) && !string.IsNullOrWhiteSpace(fld.default_value) && fld.default_value.ToLower().Contains("h"))
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
                        else if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("h"))
                        {
                            dt = DateTime.Parse(fld.default_value);
                            value = dt.ToString(fld.format);

                        }
                        else if (string.IsNullOrWhiteSpace(value))
                        {

                            dt = DateTime.Parse(value);
                            value = dt.ToString(fld.format);
                        }


                        //var tm = GetDate(value);
                        // element.SendKeys(Keys.Control + "a");
                        //element.SendKeys(time);
                    }

                    if (fld.format.Contains("/"))
                        value = value.Replace("-", "/");
                    if (fld.format.Contains("-"))
                        value = value.Replace("/", "-");
                    //Actions actions = new Actions(session.desktopSession);
                    //actions.DoubleClick(element).Perform();
                    //element.SendKeys(Keys.Delete);
                    element.Clear();
                    //element.SendKeys(Keys.Control + "a");
                    element.SendKeys(value);
                }


            }
            else if (element != null && element.TagName == "ControlType.CheckBox")
            {
                element.Clear();
                String checkBoxToggleState = element.GetAttribute("Toggle.ToggleState");
                if (checkBoxToggleState.Equals("0"))
                {
                    element.Click();
                }
            }
            else if (element != null && element.TagName == "ControlType.List")
            {
                var values = value.Split(",");
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                var checkBoxes = element.FindElements(By.XPath("*/*"));
                var selCheckBoxes = checkBoxes.Where(c => values.Contains(c.GetAttribute("Name"))).ToArray();
                if (checkBoxes != null && checkBoxes.Count() > 0)
                {

                    SetChildren(selCheckBoxes, values);
                }
                else
                {                 

                    //Actions actions = new Actions(session.desktopSession);
                    //actions.DoubleClick(element).Perform();
                   // element.SendKeys(Keys.Delete);
                    element.Clear();
                    //element.SendKeys(Keys.Control + "a");
                    element.SendKeys(value);
                    WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                    waitInput.Until(ExpectedConditions.TextToBePresentInElementValue(element, value));
                }
            }
            else if (element != null && element.TagName == "ControlType.Spinner")
            {
                var numeric = element.FindElementByTagName("Edit");
                numeric.Clear();
                Paste(numeric, value);
                WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
            }
            else if (element != null && element.TagName == "ControlType.ComboBox")
            {

                var children = element.FindElements(By.XPath("*/*"));
                if (children.Count() == 2 && children[0].TagName == "ControlType.Spinner" && children[1].TagName == "ControlType.Edit")
                {
                    // children[1].Clear();
                    //children[1].SendKeys(Keys.Control + "a");
                    children[1].Clear();
                    children[1].SendKeys(value);
                    //WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                    //waitInput.Until(ExpectedConditions.TextToBePresentInElement(children[1], value));
                }
                else
                {
                    // element.Clear();
                    //Paste(element, value);
                    if (!string.IsNullOrWhiteSpace(value) && (fld.control_type == (int)CONTROL_TYPES.DATE))
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
                        else if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(fld.default_value) && !fld.default_value.ToLower().Contains("d"))
                        {
                            dt = DateTime.Parse(fld.default_value);
                        }
                        else
                            dt = DateTime.Parse(value);

                        value = dt.ToString(fld.format);

                        if (fld.format.Contains("/"))
                            value = value.Replace("-", "/");
                        if (fld.format.Contains("-"))
                            value = value.Replace("/", "-");

                        //element.SendKeys(Keys.Control + "a");
                        element.Clear();
                        element.SendKeys(value);
                    }
                    else if (!string.IsNullOrWhiteSpace(value) && (fld.control_type == (int)CONTROL_TYPES.DATETIME))
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
                        if (fld.format.Contains("/"))
                            value = value.Replace("-", "/");
                        if (fld.format.Contains("-"))
                            value = value.Replace("/", "-");
                       // element.SendKeys(Keys.Control + "a");
                        element.Clear();
                        element.SendKeys(value);
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

                        if (fld.format.Contains("/"))
                            value = value.Replace("-", "/");
                        if (fld.format.Contains("-"))
                            value = value.Replace("/", "-");
                       // element.SendKeys(Keys.Control + "a");
                        element.Clear();
                        element.SendKeys(value);
                        //var tm = GetDate(value);
                        // element.SendKeys(Keys.Control + "a");
                        //element.SendKeys(time);
                    }
                    else
                    {

                        //element.SendKeys(Keys.Control + "a");
                        element.Clear();
                        element.SendKeys(value);
                        WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                        waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                    }
                }
            }
            else if (element != null && element.TagName == "ControlType.RadioButton")
            {
                var children = element.FindElements(By.XPath("*/*"));
                if (children != null && children.Count() > 0)
                {
                    SetChildren(children, value.Split(","));
                }
                else
                {
                    // LeftClick on RadioButton "Male" at (34,24)
                    String checkBoxToggleState = element.GetAttribute("Toggle.ToggleState");
                    if (checkBoxToggleState != null && value == "True")
                    {
                        element.Click();
                        element.SendKeys(" ");
                    }
                }
            }
            else if (element != null && element.TagName == "ControlType.Pane")
            {
                if (fld.control_type == (int)UTIL.Enums.CONTROL_TYPES.GRID && fld.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_CONFIRM)
                {
                    List<AppiumWebElement> rows = element.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Custom").ToList();
                    foreach (AppiumWebElement row in rows)
                    {
                        List<AppiumWebElement> gRows = row.FindElements(By.XPath(@"//*")).Where(i => i.TagName == "ControlType.Edit").ToList();


                        foreach (AppiumWebElement cells in gRows)
                        {
                            if (cells.Text.Contains(value))
                            {
                                //Actions select = new Actions(row.WrappedDriver);
                                row.Click();
                                //select.SendKeys(Keys.Enter);
                                //select.Perform();
                                // bool sel = row.Selected;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 1));
                    //waitInput.Until(ExpectedConditions.ElementToBeClickable(element));
                    //element.SendKeys(Keys.Control + "a");
                    element.Clear();
                    element.SendKeys(value);
                    try
                    {
                        WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 10));
                        waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                    }catch(Exception eXr){ 
                    //
                    }
                }// Pane previous behavior
            }
            else if (element != null && element.TagName == "ControlType.Document")
            {
                element.Click();
                Paste(element, value);
                WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 1));
                waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                //element.SendKeys(value);
            }
            else if (element != null && element.TagName == "ControlType.Button")
            {

                // element.Click();
                WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                waitInput.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                //element.SendKeys(value);
            }

            return data;
        }

        private void Paste(AppiumWebElement element, string value)
        {
            //System.Windows.Forms.Clipboard.SetDataObject(value, false, 3, 100);
            TextCopy.ClipboardService.SetText(value);
           // element.SendKeys(Keys.Control + "a");
            element.Clear();
            element.SendKeys(OpenQA.Selenium.Keys.Control + 'v');
            //element.SendKeys(value);
        }
        public List<string> SetChildren(IReadOnlyCollection<AppiumWebElement> children, string[] value)
        {
            var selected = new List<string>();
            foreach (var child in children)
            {
                try
                {
                    if (!child.Displayed)
                    {
                        Actions action = new Actions(child.WrappedDriver);
                        action.MoveToElement(child).Build().Perform();
                    }
                    var t = child.Text;
                    var n = child.GetAttribute("Name");
                    if (value.Contains(t) || value.Contains(n))
                    {
                        //child.Click();
                        child.Click();
                        child.SendKeys(" ");
                        //Actions action = new Actions(child.WrappedDriver);
                        //action.DoubleClick(child);
                        //action.Build().Perform();
                        //action.Perform();
                        //action.SendKeys(" ").Build().Perform();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return selected;
        }
        #endregion

        #region Get Data From Form 
        public void SaveFormData(MappingViewModel formData)
        {
            //Need to fix this Error:
            //[The process cannot access the file (Data.json) because it is being used by another process.]
            var dataJson = Path.Combine(rootFolder, @"Data.json");
            var json = JsonSerializer.Serialize(formData);
            File.WriteAllText(dataJson, json);
            StopCaptering();
        }
        public bool ScanRemainingData()
        {
            _log.Info("* Start *");
            bool bSuccess = false;
            try
            {
                foreach (var form in _formData.forms)
                {
                    ScanRemainingData(form.fields);
                }
                //for (int i = 0; i < formData.fields.Count; i++)

                //test complete
                bSuccess = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            _log.Info("* finished *");
            return bSuccess;
        }
        public void ScanRemainingData(ICollection<EntityFieldViewModel> fields)
        {
            foreach (var field in fields)
            {
                if (field.Fields != null && field.Fields.Count > 0)
                {
                    ScanRemainingData(field.Fields);
                }
                else
                {
                    if (string.IsNullOrEmpty(field.value))
                    {
                        try
                        {
                            var controlData = CheckElement(root, field);
                            //var data = _formData.forms.FirstOrDefault().fields.FirstOrDefault(d => d.pms_field_name == field.pms_field_name);
                            //if (data != null)
                            {
                                field.value = controlData;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
            }
        }

        //public bool GetUIFieldData(UiEvent uiEvent)
        //{
        //    if (uiEvent.UiElement.Value != null)
        //    {
        //        var fieldName = uiEvent.UiElement.AutomationId;
        //        var fieldPart = uiEvent.UiElement.AutomationId.Split(".");
        //        if (fieldPart.Length > 1)
        //            fieldName = fieldPart[1];
        //        var fieldData = _formData.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
        //        var formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
        //        if (formfield != null && fieldData != null)
        //        {
        //            fieldData.value = uiEvent.UiElement.Value;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public FIELD_TYPES SetFieldData(UiEvent uiEvent)
        {
            var type = FIELD_TYPES.UNKNOWN;
            if (uiEvent != null)
            {
                if (uiEvent.UiElement.Value != null)
                {
                    try
                    {
                        EntityFieldViewModel formfield = GetDataField(uiEvent);
                        try
                        {
                            if (formfield != null)
                            {
                                var data = CheckElement(root, formfield);
                                formfield.value = data;
                                type = FIELD_TYPES.DATA;
                            }
                        }
                        catch (Exception ex)
                        {
                            _log.Error(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }
                }
            }
            return type;
        }

        public FIELD_TYPES GetFieldType(UiEvent uiEvent)
        {
            var type = FIELD_TYPES.UNKNOWN;
            if (uiEvent != null && uiEvent.UiElement.Value != null && (uiEvent.UiElement.ClassName.ToLower().Contains("button") || uiEvent.UiElement.ClassName.ToLower().Contains("text") || uiEvent.UiElement.ClassName.ToLower().Contains("edit") || uiEvent.UiElement.ClassName.ToLower().Contains("pane"))) if (uiEvent != null && uiEvent.UiElement.Value != null && (uiEvent.UiElement.ClassName.ToLower().Contains("button") || uiEvent.UiElement.ClassName.ToLower().Contains("text") || uiEvent.UiElement.ClassName.ToLower().Contains("edit") || uiEvent.UiElement.ClassName.ToLower().Contains("pane")))
                {
                    //if (uiEvent.UiElement.Value != null)
                    //{
                    try
                    {
                        if (definitionEntities.Count > 1)
                        {
                            MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => uiEvent.UiElement.Equals(root.FindElementByAccessibilityId(x.SubmitCaptureFieldId))).FirstOrDefault();
                            if (captureEntity != null)
                            {
                                MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                                if (mapping != null)
                                {
                                    this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "PMS Data scan process started..");
                                    type = FIELD_TYPES.SUBMIT_AND_SCAN;
                                    // this.bWorker.ReportProgress(100);
                                }
                            }
                        }
                        else
                        {
                            type = FIELD_TYPES.DATA;
                        }
                        //EntityFieldViewModel field = GetDataField(uiEvent);
                        //    if (field != null)
                        //    {
                        //        if (field.control_type == (int)CONTROL_TYPES.ACTION)
                        //        {
                        //            if (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        //            {
                        //                type = FIELD_TYPES.SUBMIT;
                        //            }
                        //            else if (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE)
                        //            {
                        //                type = FIELD_TYPES.SUBMIT_AND_SCAN;
                        //            }
                        //            else if (field.value.ToUpper() == CONTROl_ACTIONS.CANCEL.ToString())
                        //            {
                        //                type = FIELD_TYPES.CANCEL;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            type = FIELD_TYPES.DATA;
                        //        }
                        //    }
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }

                }
            return type;
        }
        public EntityFieldViewModel GetDataField(UiEvent uiEvent)
        {
            var fieldName = uiEvent.UiElement.AutomationId;
            EntityFieldViewModel field = null;
            foreach (FormViewModel form in _formData.forms)
            {
                field = GetDataField(form.fields, fieldName);
                if (field != null) break;
            }
            if (field == null)
            {
                fieldName = uiEvent.UiElement.ParentAutomationId;
                if (!string.IsNullOrWhiteSpace(fieldName))
                {
                    foreach (FormViewModel form in _formData.forms)
                    {
                        field = GetDataField(form.fields, fieldName);
                        if (field != null) break;
                    }
                }

            }
            //var fieldPart = fieldName.Split(".");
            //if (fieldPart.Length > 1)
            //    fieldName = fieldPart[1];
            return field;
        }
        public EntityFieldViewModel GetDataField(ICollection<EntityFieldViewModel> fieldsls, string fuid)
        {
            EntityFieldViewModel dataField = fieldsls.Where(f => f.fuid == fuid).FirstOrDefault();
            if (dataField == null)
            {
                List<EntityFieldViewModel> multiFields = fieldsls.Where(f => f.Fields != null && f.Fields.Count > 0).ToList();
                foreach (EntityFieldViewModel field in multiFields)
                {
                    dataField = GetDataField(field.Fields, field.fuid);
                    if (field != null) break;
                }
            }
            return dataField;
        }
        private string CheckElement(AppiumWebElement root, EntityFieldViewModel field)
        {
            AppiumWebElement element = GetCachedElement(root, field);
            //AppiumWebElement element = root.FindElementByAccessibilityId(field.pms_field_name);
            return GetElementData(element, field);
        }
        //public void GetElementData(EntityFieldViewModel field)
        //{
        //    AppiumWebElement element = GetCachedElement(root, field);
        //    var dataField = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == field.pms_field_name);
        //    MessageBox.Show(GetElementData(element, dataField));
        //}
        private string GetElementData(AppiumWebElement element, EntityFieldViewModel field)
        {

            string data = "";
            string output = "";
            if (element != null)
            {
                if (field.control_type == (int)CONTROL_TYPES.IMAGE)
                {
                    var v = element.GetAttribute("visual");
                    var foundResults = root.FindElementsByAccessibilityId("pic_box");
                }
                if (element.TagName == "ControlType.List")
                {
                    List<AppiumWebElement> children = element.FindElements(By.XPath("*/*")).ToList();
                    //var children1 = element.FindElementsByTagName("CheckBox");// (By.XPath("*/*"));
                    if (children.Count > 0)
                    {
                        var vels = children.Where(c => c.Selected).Select(c => c.GetAttribute("Name")).ToArray();
                        data = string.Join(", ", vels);
                    }
                    else
                    {
                        data = GetFieldData(element, field);
                    }
                }
                else if (element.TagName == "ControlType.Spinner")
                {
                    var child = element.FindElement(By.TagName("Edit"));
                    if (child != null)
                    {
                        data = GetFieldData(child, field);
                    }
                }
                else if (element != null && element.TagName == "ControlType.Table")
                {
                    if (field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.PROCESS_AND_GET)
                    {
                        List<AppiumWebElement> gRows = element.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Custom").ToList();
                        foreach (AppiumWebElement row in gRows)
                        {
                            if (row.Selected)
                            {
                                List<AppiumWebElement> cells = row.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Header").Where(c => Convert.ToString(c.Text).Replace(",", "").Contains("resNo")).ToList();
                                if (cells != null)
                                    data = Convert.ToString(cells[0].Text).ToLower().Replace(",", "");
                                else
                                    data = string.Empty;
                                break;
                            }
                        }
                    }
                }
                else if (element != null && element.TagName == "ControlType.Pane" && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.PROCESS_AND_GET)
                {
                    data = this.referenceValue;
                    // root.WrappedDriver.SwitchTo().Frame(element.WrappedDriver.CurrentWindowHandle);
                    WindowsElement ele = (WindowsElement)root.FindElementByClassName("ProFrame");
                    // WindowsElement ele=(WindowsElement) root.FindElementByName("Resident Guests");
                    List<AppiumWebElement> gRows = ele.FindElements(By.XPath(@".//child::*")).Where(i => i.TagName == "ControlType.Edit").ToList();
                    foreach (AppiumWebElement row in gRows)
                    {
                        //if (row.Selected)
                        //{
                        List<AppiumWebElement> cells = row.FindElements(By.XPath(@".//child::*")).Where(i => i.TagName == "ControlType.Pane").Where(c => Convert.ToString(c.Text).Replace(",", "").ToLower().Contains("resno")).ToList();
                        if (cells != null && cells.Count > 0)
                            data = Convert.ToString(cells[0].Text).ToLower().Replace(",", "");
                        else
                            data = "21410";// string.Empty;
                        break;
                        //}
                    }
                    //  var hGrid = root.FindElementsByName("History");//.Where(i => i.TagName == "ControlType.Custom").ToList();

                    //List<AppiumWebElement> gRows = hGrid[0].FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Custom").ToList();
                    //foreach (AppiumWebElement row in gRows)
                    //{
                    //    if (row.Selected)
                    //    {
                    //        List<AppiumWebElement> cells = row.FindElements(By.XPath(@".//*")).Where(i => i.TagName == "ControlType.Header").Where(c => Convert.ToString(c.Text).Replace(",", "").ToLower().Contains("resno")).ToList();
                    //        if (cells != null)
                    //            data = Convert.ToString(cells[0].Text).ToLower().Replace(",", "");
                    //        else
                    //            data = "21069";// string.Empty;
                    //        break;
                    //    }
                    //}

                }
                else
                {
                    data = GetFieldData(element, field);
                }
                // output = element.TagName + " ( " + element.GetAttribute("AutomationId") + " ) - " + data + Environment.NewLine;
            }

            return data;
        }
        protected string getRefImage(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        private string GetFieldData(AppiumWebElement element, EntityFieldViewModel field)
        {
            var data = string.Empty;
            // string expVal = (""+field.pms_field_expression).ToLower().Contains("feed") || ("" + field.pms_field_expression).ToLower().Contains("scan");
            //var expression = ( string.IsNullOrWhiteSpace(field.pms_field_expression) || (""+field.pms_field_expression).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX) ) ? "Text" : ("" + field.pms_field_expression).Trim();
            var expression = "Text";
            PropertyInfo result = typeof(AppiumWebElement).GetProperty(expression);
            if (result != null)
                data = result.GetValue(element).ToString();

            if ((field.data_type == (int)UTIL.Enums.DATA_TYPES.DATE) && !string.IsNullOrWhiteSpace(data))

            {
                try
                {
                    System.DateTime date = UTIL.GlobalApp.CurrentDateTime;
                    date = DateTime.ParseExact(data, field.format, CultureInfo.CurrentCulture);
                    if (date.Year > 1900)
                        data = date.ToString(UTIL.GlobalApp.date_format);
                }
                catch (Exception)
                {
                    // data = DateTime.TryParse(data,(UTIL.GlobalApp.date_format);
                }

            }
            else if (field.data_type == (int)UTIL.Enums.DATA_TYPES.DATETIME && !string.IsNullOrWhiteSpace(data))// "time" && !string.IsNullOrWhiteSpace(data))
            {

                try
                {
                    System.DateTime dt = UTIL.GlobalApp.CurrentDateTime;
                    dt = DateTime.ParseExact(data, field.format, CultureInfo.CurrentCulture);
                    if (dt.Year > 1900)
                        data = dt.ToString(UTIL.GlobalApp.date_time_format);
                }
                catch (Exception)
                {
                    // data = DateTime.TryParse(data,(UTIL.GlobalApp.date_format);
                }

            }
            return Convert.ToString(data);// ToString();
        }
        private string GetDate(string data)
        {
            var dt = DateTime.Now;
            DateTime.TryParse(data, out dt);
            var dateStr = dt.ToString("MM/dd/yyyy");
            return dateStr;
        }

        private string GetTime(string data)
        {
            var dt = DateTime.Now;
            DateTime.TryParse(data, out dt);
            var dateStr = dt.ToString("HH:mm:ss:tt");
            return dateStr;
        }
        #endregion

        #region "Custom Calculation Methods"

        private MappingViewModel FieldJson()
        {
            var fieldsJson = Path.Combine(rootFolder, @"EntityAndFieldMappings_BookComplete.json");
            string json = File.ReadAllText(fieldsJson);
            var form = JsonSerializer.Deserialize<List<MappingViewModel>>(json);
            return form[0];
        }
        public AppiumWebElement GetFocued()
        {
            try
            {
                ////var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var originalTab = session.desktopSession.SwitchTo();
                //var originalTab1 = session.desktopSession.SwitchTo().Frame("tabPage1");
                //var originalTab2 = session.desktopSession.SwitchTo().Window(session.desktopSession.WindowHandles.First());
                //var e2 = session.DesktopSessionElement.SwitchTo().ActiveElement();
            }
            catch (Exception ex)
            { }
            try
            {
                //var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var currentElement = root.WrappedDriver.SwitchTo().ActiveElement();
            }
            catch (Exception ex)
            { }
            try
            {
                //var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var e = root.FindElementsByXPath("//*[@focused='true']").FirstOrDefault();
                //return e;
            }
            catch (Exception ex)
            { }
            return null;
        }
        #endregion

    }
}
