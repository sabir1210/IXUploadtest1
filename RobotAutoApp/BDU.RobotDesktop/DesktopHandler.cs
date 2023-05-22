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
using BDU.UTIL;
using BDU.Services;
using OpenQA.Selenium.Chrome;

namespace BDU.RobotDesktop
{
    public class DesktopHandler
    {
        #region "variables & objects declaration & intialization"
        private Logger _log = LogManager.GetCurrentClassLogger();
        public delegate void SaveDataEvent(MappingViewModel mappingViewModel);
        private BDUService _bduservice = new BDUService();
        public SaveDataEvent DataSaved;
        public int Undo { get; set; }
        // private delegate void ScanAndFoundData(UiEvent curUIEvent);

        public delegate void UddateUIAboutDataEvent(UTIL.Enums.SYNC_MESSAGE_TYPES mType, string msg);
        public event UddateUIAboutDataEvent blazorUpdateUIAboutPMSEvent;
        private GenerationClient _generationClient { get; set; }
        private Thread uiRecorderThread = null;
        private EventHandler<EventArgs> _generationClientHookProcedureEventHandler { get; set; }

        private EventHandler<UiEventEventArgs> _generationClientUiEventEventHandler { get; set; }
        Dictionary<string, DateTime> IntervalControllerDic { get; set; } = new Dictionary<string, DateTime>();
        string referenceValue = string.Empty;
        //  private MappingViewModel _formMapping = new MappingViewModel();
        public MappingViewModel _formData { get; set; }
        // Dictionary<string, string> keywordDict { get; set; }
        public MappingViewModel _access = null;
        public HybridWraper _hybridWraper = null;
        public List<MappingViewModel> ScanningEntities = null;
        HybridWraper hWraper = new HybridWraper();
        //  public UTIL.Enums.AUTOMATION_MODES automation_mode =  AUTOMATION_MODES.UIAUTOMATION;

        private List<MappingDefinitionViewModel> definitionEntities = null;
        private List<string> submitControls = new List<string> { "button", "pane" };// { "edit", "button", "pane" };
        DesktopSession session { get; set; }
        public Int16 automation_engine_Version { get; set; } = (int)UTIL.Enums.IX_ENGINE_VERSIONS.V2;
        public Int16 automation_mode { get; set; } = (int)UTIL.Enums.AUTOMATION_MODES.UIAUTOMATION;
        public Int16 automation_mode_Type { get; set; } = (int)UTIL.Enums.AUTOMATION_MODE_TYPE.APP;
        // NODE 
        //  public INodeJSService _nodeJSService;
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

        #region "Login & Constructor call methods"
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
        public bool StartSessionAndLogin(MappingViewModel mapping)
        {
            bool started = false;
            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS system launching...");
            if (started == false && StartServer())
            {

                if (session != null && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT)
                {
                    ResponseViewModel res = new ResponseViewModel();
                    _formData = mapping.DCopy();
                    if (UTIL.GlobalApp.IS_PROCESS == "1")
                    {
                        UTIL.GlobalApp.AUTOMATION_MODE_TYPE_CONFIG = (int)UTIL.Enums.AUTOMATION_MODE_TYPE.PROCESS;
                        res = this.ProcessLogin(_formData);
                        started = true;
                    }
                    else
                        res = this.login(_formData);
                    if (res.status)
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS session intialized successfully.");
                        started = true;
                    }
                    else
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                        _access = _formData;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.ERROR, "PMS session intialization failed.");

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
                _log.Fatal("IX-Failure login", ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                // _log.Error(ex);
            }
            finally
            {
                SetUiForRecorderUiStateIsReady();
                this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS login finished..");
            }
            //Console.WriteLine("* End *");
            return res;
        }
        public bool StartDesktopSession(List<MappingViewModel> mapping, UTIL.Enums.AUTOMATION_MODES automation_mode = AUTOMATION_MODES.UIAUTOMATION)
        {
            bool started = false;
           // elementDict = new Dictionary<string, AppiumWebElement>();
            switch (automation_mode)
            {
                case AUTOMATION_MODES.UIAUTOMATION:
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

                            EntityFieldViewModel fldR = _fields.Where(x => x.field_desc == "applicationwithname").FirstOrDefault();
                            if (fldR != null)
                            {
                                _ApplicationDefaultUrl = fldR.default_value;
                                _ApplicationNameWithURL = fldR.default_value;
                                UTIL.GlobalApp.PMS_Application_Path_WithName = _ApplicationNameWithURL;
                            }
                            SetupGenerationClient();
                            started = true;
                        }
                    }
                    break;
                case AUTOMATION_MODES.HYBRID:
                    // session = new DesktopSession();
                    // if (session != null && session.desktopSession != null)
                    // {
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
                    _hybridWraper = new HybridWraper();
                    SetupGenerationClient();
                    started = true;
                    //   }                   
                    break;
                default:
                    // Default
                    break;
            }// End switch
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
                    rs.message = "Login completed successfully.";

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
                rs.message = "PMS system is not running or failed to load!";
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
                        if (localprocessName.Length > 0)
                        {
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
                rs.message = "Login completed successfully.";



            }
            else
            {
                rs.status = false;
                rs.jsonData = null;
                rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                rs.message = "PMS System is not running or load failed!";
            }
            SetUiForRecorderUiStateIsReady();
            return rs;

        }
        #endregion
        #region "Test Run"
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
                    if (session.isRootCapability)
                    {
                        //if(string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName)
                        if (!string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName))
                        {
                            root = session.FindElementByAbsoluteXPath(BDU.UTIL.GlobalApp.PMS_Application_Path_WithName);
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
                    //else if (errorFields != null && errorFields.Where(x => x.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).FirstOrDefault() != null)
                    //{
                    //    rs.status = false;
                    //    rs.jsonData = JsonSerializer.Serialize(errorFields);
                    //    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                    //    //string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                    //    rs.message = string.Format(UTIL.GlobalApp.RESERVATION_NOT_FOUND_IN_PMS, model.reference);
                    //}
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
                _log.Fatal("IX-Failure login", ex);
             //   _log.Error(ex);
            }
            finally
            {
                SetUiForRecorderUiStateIsReady();
                TestEnty = null;
            }
            return rs;

        }
        public ResponseViewModel TestRunHybrid(MappingViewModel model)
        {
            MappingViewModel TestEnty = null;
            ResponseViewModel rs = new ResponseViewModel();
            try
            {
                SetUiForRecorderUiStateDataFeeding();
                TestEnty = model.DCopy();

                List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                //var errorFields = testRunHybrid(TestEnty, true);
                errorFields = this.testRunEntityLevelHybrid(TestEnty, false);
                // Test Run Results
                // var errorFields = result.Result;
                if (errorFields == null)
                {
                    rs.status = true;
                    rs.jsonData = null;
                    rs.status = false;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Test run success.";

                }
                else if (errorFields.Count == 0)
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
        //private List<EntityFieldViewModel> CacheFormElements()
        //{
        //    var errorFields = new List<EntityFieldViewModel>();
        //    foreach (var form in _formData.forms)
        //    {
        //        var errors = CacheFormElements(form.fields, form);
        //        errorFields.AddRange(errors);
        //    }
        //    return errorFields;
        //}
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
        #region "Hybrid"
        private List<EntityFieldViewModel> testRunHybrid(MappingViewModel formData, bool needFill = false)
        {
            var errorFields = new List<EntityFieldViewModel>();
            try
            {

                HybridMappingViewModel mapping = new HybridMappingViewModel { Name = formData.reference, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                List<UICommands> ls = new List<UICommands>();


                // ls.Add(new UICommands { Command = "XRunAndWait", Target = txtNodeExpression.Text, Value = "Value", Description = "" });
                //  mapping.Commands = ls;
                //  string macro = JsonSerializer.Serialize(mapping);
                //  string macroFile = "tx.json";// string.Format("{0}.{1}", Guid.NewGuid().ToString(), "json");
                // jsonFile = string.Format(@"C:\Users\zahid.nawaz.BTDOMAIN\Desktop\uivision\macros\{0}", macroFile);
                // File.WriteAllText(jsonFile, macro);

                // new List<UICommands>() { Command = "XRunAndWait", XDesktopAutomation=true  Target = "https://www.google.com", Value = "Value", Description = "" });.
                //var response = await httpClient.PostAsync(GlobalApp.API_URI + API.POST_CMS_DATA, jsonContent);
                // var varRes = await nodeJSService.InvokeFromFileAsync<Result>(@"D:\Shared\RPANode\blazornodecli.js", args: new[] { testTobeExecuted }); //{ "google.com", "DRX" });
                //var varRes = await nodeJSService.InvokeFromFileAsync<object>(@"D:\Shared\RPANode\examples\module.js", args: new object[] { json }); //{ "google.com", "DRX" });
                //var varRes = await nodeJSService.InvokeFromFileAsync<object>(@"D:\Shared\RPANode\examples\customuivisioncli.js", args: new object[] { json }); //{ "google.com", "DRX" });
                //var macroJson = JsonSerializer.Serialize(mapping);
                // var varRes =  _nodeJSService.InvokeFromFileAsync<object>(@"D:\Shared\Temp\AppiumWindows\RPANode\examples\blazorcli.js", args: new object[] { formData.entity_Id, @"D:\Shared\Temp\AppiumWindows\node\ui.vision.html", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" }); //{ "google.com", "DRX" });
                var varRes = string.Empty;// _nodeJSService.InvokeFromFileAsync<object>(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_NODE_WRAPER, args: new object[] { formData.entity_Id, UTIL.GlobalApp.APPLICATION_DRIVERS_UIVISIONCLI, UTIL.GlobalApp.APPLICATION_DRIVERS_CHROME_DRIVER }); // @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" }); //{ "google.com", "DRX" });
                return new List<EntityFieldViewModel>();
                //  strBuilder.AppendLine("Blazor CLI Response -" + Convert.ToString(varRes));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
            //if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT)
            //{
            /*
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
            */
            return errorFields;
        }
        private List<EntityFieldViewModel> testRunEntityLevelHybrid(MappingViewModel entity, bool needFill = false)
        {
            List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
            // ResponseViewModel HybridResp = new ResponseViewModel();
            try
            {
                List<EntityFieldViewModel> allEntityFields = new List<EntityFieldViewModel>();
                HybridMappingViewModel mapping = new HybridMappingViewModel { Name = entity.reference, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                List<UICommands> cmdls = new List<UICommands>();
                foreach (FormViewModel frm in entity.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(X => X.sort_order))
                {
                    fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList(), needFill);
                    List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.feed == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(X => X.sr).ToList();
                    allEntityFields.AddRange(candidateFlds);

                }// Outer foreach
                if (allEntityFields != null && allEntityFields.Any())
                {
                    string macro = string.Format("{0}_{1}", entity.entity_Id.ToString(), ((int)UTIL.Enums.AUTOMATION_TRAGET_ACTVITY.FEED).ToString());
                    string automationFile = _hybridWraper.HybridAutomationFeedCommands(allEntityFields, macro, false);
                    errorFields = _hybridWraper.ProcessShellBasedHybridFeedResponse(allEntityFields, string.Format("{0}.json", macro), true);
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
            return errorFields;
        }
        #endregion
        private List<EntityFieldViewModel> CacheFormElementsWithFill(MappingViewModel formData, bool needFill = false)
        {
            var errorFields = new List<EntityFieldViewModel>();
            if (UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SCANNING)
            {

                // Do Masking and enable disable of billing fields
                this.prepareFieldsCollection(formData, needFill);

                foreach (FormViewModel frm in formData.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(X => X.sort_order))
                {
                    fillKeywordsDictionary(frm.fields);
                    session.desktopSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(BDUConstants.AUTOMATION_FIND_REF_CONTROL_WAIT_SECS);
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
                            //if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active && errorFields.Count == 1 && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                            //{
                            //    errorFields.Clear();
                            //    errorFields.Add(field);
                            //    return errorFields;
                            //}
                            //else 
                            if (element == null && field.mandatory == (int)UTIL.Enums.STATUSES.Active)
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
            if (form.fields != null && form.fields.Count() > 0)            {
                
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
                    }
                }
            }
            return errorFields;
        }
        private AppiumWebElement FindAndCachedElement(AppiumWebElement root, EntityFieldViewModel field, FormViewModel form)
        {
            AppiumWebElement element= null;
            //elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
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
                }
                catch (Exception exp)
                {
                    _log.Error(exp);
                    element = null;                   
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
                if (root == null && session.desktopSession != null && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_Application_Path_WithName))
                    root = session.desktopSession.FindElementByXPath(UTIL.GlobalApp.PMS_Application_Path_WithName);
                if (root != null)
                {
                    AppiumWebElement element;
                    string formName = fillableEntity.entity_name;
                    //******************* Fill Fields Collection
                    this.referenceValue = string.Empty;
                    // Get All fields from 
                    foreach (FormViewModel frm in fillableEntity.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(x => x.sort_order))
                    {
                        fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList(), false);
                        System.Threading.Thread.Sleep(200);
                        // List<EntityFieldViewModel> flds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.action_type != (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && (string.IsNullOrWhiteSpace(x.pms_field_expression) || !Convert.ToString(x.pms_field_expression).Contains(UTIL.BDUConstants.SCAN_NOT_REQUIRED))).OrderBy(x => x.sr).ToList();
                        List<EntityFieldViewModel> flds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.PAGE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.action_type != (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && x.scan == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(x => x.sr).ToList();
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
                                                Thread.Sleep(100);
                                            element = FindScanningElementWithFill(root, field);
                                            if (element != null)
                                            {
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

                        if ((string.IsNullOrWhiteSpace(referenceValue) || referenceValue.Contains("new")) && (fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKIN || fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS || fillableEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKOUT))
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
                                if (!string.IsNullOrWhiteSpace(fillableEntity.roomno))
                                    break;
                            }// Outer Each  
                        }//
                   

                    rs.jsonData = fillableEntity;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Retrieval of reservation data completed.";
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
        public ResponseViewModel RetrievalFormDataFromHybrid(MappingViewModel scanEntity)
        {
            ResponseViewModel rs = new ResponseViewModel();
            List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
            try
            {
                _log.Info(string.Format("Data retreival process started, at {0}", UTIL.GlobalApp.CurrentLocalDateTime));
                //  if (root == null && session.desktopSession != null && !string.IsNullOrWhiteSpace(UTIL.GlobalApp.PMS_Application_Path_WithName))
                //     root = session.desktopSession.FindElementByXPath(UTIL.GlobalApp.PMS_Application_Path_WithName);
                if (UTIL.GlobalApp.AUTOMATION_MODE_CONFIG == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID)
                {
                    //  AppiumWebElement element;
                    string formName = scanEntity.entity_name;
                    //******************* Fill Fields Collection
                    this.referenceValue = string.Empty;

                    // Get All fields from 
                    List<EntityFieldViewModel> allEntityFields = new List<EntityFieldViewModel>();
                    HybridMappingViewModel mapping = new HybridMappingViewModel { Name = scanEntity.reference, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    List<UICommands> cmdls = new List<UICommands>();
                    foreach (FormViewModel frm in scanEntity.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active).OrderBy(X => X.sort_order))
                    {
                        fillKeywordsDictionary(frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active).ToList(), true);
                        List<EntityFieldViewModel> candidateFlds = frm.fields.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.action_type != (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && x.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && x.scan == (int)UTIL.Enums.PMS_ACTION_REQUIREMENT_TYPE.REQUIRED).OrderBy(X => X.sr).ToList();
                        allEntityFields.AddRange(candidateFlds);

                    }// Outer foreach
                    if (allEntityFields != null && allEntityFields.Any())
                    {
                        string logFile = string.Format("{0}.{1}", Guid.NewGuid().ToString(), "txt");
                        // Macro for test execution

                        string macro = string.Format("{0}_{1}", scanEntity.entity_Id.ToString(), ((int)UTIL.Enums.AUTOMATION_TRAGET_ACTVITY.SCAN).ToString());

                        // For Scan FillWithValue is i
                        string hybridTest = _hybridWraper.HybridAutomationScanCommands(allEntityFields, macro, true, false);
                        //  hybridTest = "localprofile";
                        if (!string.IsNullOrWhiteSpace(hybridTest))
                        {
                            /******************************** HYBRID SCAN PROCESS STARTED*****************************************/
                            List<EntityFieldViewModel> error = _hybridWraper.ProcessShellBasedHybridResponse(allEntityFields, string.Format("{0}.json", macro), true);

                            // Process Reference Column
                            if (allEntityFields.Where(x => x.is_reference == 1 && !string.IsNullOrWhiteSpace(x.value)).Any())
                            {
                                this.referenceValue = allEntityFields.Where(x => x.is_reference == 1 && !string.IsNullOrWhiteSpace(x.value)).FirstOrDefault().value;
                                scanEntity.reference = this.referenceValue;
                            }
                            /*****************  UIVision Call - START*******************************/
                            //  JsonDocument varRes = await _nodeJSService.InvokeFromFileAsync<JsonDocument>(@"D:\Shared\Temp\AppiumWindows\RPANode\examples\blazor31.js", args: new object[] { hybridTest, @"D:\Shared\Temp\AppiumWindows\node\ui.vision.html", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", logFile }); //{ "google.com", "DRX" });
                            // var varRes = await _nodeJSService.InvokeFromFileAsync<string>(@"D:\Shared\BDUV3.0\BDM Core\bin\Debug\net5.0-windows\Drivers\blazorclix.js", args: new string[] { "localprofile", @"D:\Shared\Temp\AppiumWindows\node\ui.vision.html", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", logFile }); //{ "google.com", "DRX" });
                            // JsonDocument varRes = await _nodeJSService.InvokeFromFileAsync<JsonDocument>(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_NODE_WRAPER, args: new string[] { macro, UTIL.GlobalApp.APPLICATION_DRIVERS_UIVISIONCLI, UTIL.GlobalApp.APPLICATION_DRIVERS_CHROME_DRIVER, logFile }); //{ "google.com", "DRX" });
                            // string json = "[' Status = OK\n###\n[status] Playing macro /macros/localprofile.json\n[info] Executing:  | open | http://192.168.18.4:8666/ |  | \n[info] Executing:  | store | Salam Nazir Columbus | blazor | \n[info] Executing:  | echo | {blazor} |  | \n[echo] {blazor}\n[info] Executing:  | type | id=cardholder_name | ${blazor} | \n[info] Executing:  | executeScript_Sandbox | var blazortest=[]; blazortest.push(1290); return blazortest; | blazortest | \n[info] Executing:  | store | 1009 | ${blazortest[1]} | \n[info] Executing:  | echo | ##YTE_HYGT_KIJU_POPIU:${blazortest}## |  | \n[echo] ##a9605982-34b5-40dc-9187-ab4763192f83:1290##\n[info] Executing:  | comment | executeScript_Sandbox //  |  | \n[info] Macro completed (Runtime 3.02s)']";                                                                                                                                                                                                                                                                                                            //var objects = System.Text.Json.JsonDocument.Parse(varRes); // parse as array  

                            /*****************  UIVision Call - END *******************************/
                            // for Feed, Feed will be False
                            // errorFields = _hybridWraper.GetControlValues(allEntityFields, json, true);
                        }
                    }
                    //Auto reference no 
                    if (string.IsNullOrWhiteSpace(referenceValue)  && (scanEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKIN || scanEntity.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS || scanEntity.entity_Id == (int)UTIL.Enums.ENTITIES.CHECKOUT))
                    {
                        //Random generator = new Random();
                        //String rndomn = generator.Next(0, 100000000).ToString("D8");
                        //fillableEntity.reference = string.IsNullOrWhiteSpace(fillableEntity.reference) ? rndomn : fillableEntity.reference;
                        foreach (ViewModels.FormViewModel frm in scanEntity.forms.Where(x => x.Status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                        {
                            foreach (ViewModels.EntityFieldViewModel fld in frm.fields.Where(x => x.status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED && x.scan == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                            {
                                if (fld.field_desc.ToLower().Contains("room number") && !string.IsNullOrWhiteSpace(fld.value) && !fld.value.Contains(BDUConstants.SPECIAL_KEYWORD_PREFIX))
                                {
                                    scanEntity.reference = fld.value;
                                    scanEntity.roomno = fld.value;
                                    referenceValue = fld.value;
                                    break;
                                }
                            }// Inner Each
                            if (!string.IsNullOrWhiteSpace(scanEntity.roomno) && !string.IsNullOrWhiteSpace(scanEntity.reference))
                                break;
                        }// Outer Each  
                        if (string.IsNullOrWhiteSpace(scanEntity.reference) && !string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.TEST_RESERVATION_NO)) {
                            scanEntity.reference = BDU.UTIL.GlobalApp.TEST_RESERVATION_NO;
                            referenceValue = BDU.UTIL.GlobalApp.TEST_RESERVATION_NO;
                        }
                    }//

                    rs.jsonData = scanEntity;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Retrieval of form reservation data completed.";
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
                    //if (field.is_reference == (int)BDU.UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY && field.action_type != (int)BDU.UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                    //    session.desktopSession.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(BDUConstants.AUTOMATION_FIND_REF_CONTROL_WAIT_SECS);
                    //else
                    //    session.desktopSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(BDUConstants.AUTOMATION_FIND_CONTROL_WAIT_SECS);
                    if (field.is_reference == (int)BDU.UTIL.Enums.FIELD_REQUIREMENT_TYPE.MANDATORY && field.action_type == (int)BDU.UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL)
                    {
                        WebDriverWait rWait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, BDUConstants.AUTOMATION_FIND_REF_CONTROL_WAIT_SECS));
                        rWait.IgnoreExceptionTypes(typeof(WebDriverException));
                        element= rWait.Until(Driver =>
                        {
                            try { 
                                if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                                {
                                    var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();
                                    // element = root.FindElement(By.Id(field.pms_field_name));
                                    if (elements != null && elements.Count > 0)
                                    {
                                        element = elements[0];
                                    }
                                    if (element == null)
                                    {
                                        elements = root.FindElements(By.Name(field.pms_field_name)).ToList();
                                    }
                                    if (elements != null && elements.Count > 0)
                                    {
                                        element = elements[0];
                                    }
                                    if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                                    {
                                        //  var ctrls = root.FindElements(By.XPath(field.pms_field_xpath));
                                        element = root.FindElementByXPath(field.pms_field_xpath);
                                    }
                                }
                                else if (root != null)
                                {
                                    element = root.FindElementByXPath(field.pms_field_xpath);
                                }
                            }catch(Exception ex)
                            {
                                element = null;
                            };
                            return element;
                        });

                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(field.pms_field_name))
                        {
                            var elements = root.FindElementsByAccessibilityId(field.pms_field_name).ToList();
                            // element = root.FindElement(By.Id(field.pms_field_name));
                            if (elements != null && elements.Count > 0)
                            {
                                element = elements[0];
                            }
                            if (element == null)
                            {
                                elements = root.FindElements(By.Name(field.pms_field_name)).ToList();
                            }
                            if (elements != null && elements.Count > 0)
                            {
                                element = elements[0];
                            }
                            if (element == null && !string.IsNullOrWhiteSpace(field.pms_field_xpath))
                            {
                                //  var ctrls = root.FindElements(By.XPath(field.pms_field_xpath));
                                element = root.FindElementByXPath(field.pms_field_xpath);
                            }
                        }
                        else if (root != null)
                        {
                            element = root.FindElementByXPath(field.pms_field_xpath);
                        }
                    }// Non reference fields
                    if (element != null)
                    {
                        string fldValue = string.IsNullOrWhiteSpace(field.value) ? field.default_value : field.value;
                        if (element != null && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            // element.Click();
                            // System.Threading.Thread.Sleep(40);
                        }
                        else if (element != null && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                            // element.Submit();
                        }
                        else if (element!= null && field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON)
                        {
                            WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 2));
                            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            // element.Click();
                            //System.Threading.Thread.Sleep(40);
                        }
                    }
                    //if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                    //    elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    _log.Warn(exp);
                    element = null;
                }
            }
            return element;
        }

        private AppiumWebElement FindAndCachedElementWithFill(AppiumWebElement root, EntityFieldViewModel field, FormViewModel form, bool needFill = false)
        {
            AppiumWebElement element=null;
            //elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            //if (element != null)
            //{
            //    try
            //    {
            //        string tag = element.TagName;
            //    }
            //    catch (Exception ex)
            //    {
            //        element = null;
            //    }
            //}
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
                                System.Threading.Thread.Sleep(150);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                element.Click();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(150);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                element.Click();
                            }
                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.RIGHT_CLICK)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(150);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                Actions actns = new Actions(root.WrappedDriver);
                                actns.ContextClick(element).Click();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(150);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                Actions actns = new Actions(root.WrappedDriver);
                                actns.ContextClick(element).Click();
                            }
                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SHORTCUT_KEY)
                        {
                            if ((!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression)) && !string.IsNullOrWhiteSpace(field.default_value))
                            {
                                System.Threading.Thread.Sleep(100);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).SendKeys(field.default_value);
                                Actions actns = new Actions(root.WrappedDriver);
                                actns.SendKeys(element, field.default_value);
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression) && !string.IsNullOrWhiteSpace(field.default_value))
                            {
                                System.Threading.Thread.Sleep(100);
                                //WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                //wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                Actions actns = new Actions(root.WrappedDriver);
                                actns.SendKeys(element,field.default_value);
                            }
                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(150);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                System.Threading.Thread.Sleep(500);
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(150);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                                System.Threading.Thread.Sleep(500);
                            }

                            //  System.Threading.Thread.Sleep(100);
                        }
                        else if (field.control_type == (int)UTIL.Enums.CONTROL_TYPES.BUTTON && field.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT)
                        {
                            if (!string.IsNullOrWhiteSpace(field.pms_field_expression) && this.controlExistanceCheckFromExpression("" + field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(50);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Submit();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(50);
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
                                System.Threading.Thread.Sleep(50);
                                WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 12));
                                wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
                            }
                            else if (string.IsNullOrWhiteSpace(field.pms_field_expression))
                            {
                                System.Threading.Thread.Sleep(50);
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
                    //if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                    //    elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

                }
                catch (Exception exp)
                {
                    _log.Error(exp);
                    element = null;
                }
            }
            return (element== null?null:element);
        }
        #endregion

        #region Events
        private bool StartServer()
        {
            Boolean pathFound = false;
            try
            {
                _log.Info("PMS Server starting.., at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                
                String command = Path.Combine(rootFolder + @"Drivers\WinAppDriver.exe");//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                string driverCompletePath = command;                                                                        //  String command = @"D:\Shared\BDUV3.0\RobotAutoApp\Driver\WinAppDriver.exe";
                if (System.IO.File.Exists(command))
                {
                    driverCompletePath = command;
                    pathFound = true;
                }
                if (!pathFound && System.IO.File.Exists(Path.Combine(rootFolder + @"WinAppDriver.exe")))
                {
                    driverCompletePath = Path.Combine(rootFolder + @"WinAppDriver.exe");
                    pathFound = true;
                }
                if (!pathFound && System.IO.File.Exists(Path.Combine(Path.GetFullPath(Environment.CurrentDirectory), @"Drivers\WinAppDriver.exe")))
                {
                    driverCompletePath = Path.Combine(Path.GetFullPath(Environment.CurrentDirectory), @"Drivers\WinAppDriver.exe");
                    pathFound = true;
                }
                if (pathFound)
                {
                    Process p = new Process();
                    // Configure the process using the StartInfo properties.
                    p.StartInfo.FileName = driverCompletePath;

                    var process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                    startInfo.FileName = driverCompletePath;// "cmd.exe";
                    // startInfo.WorkingDirectory = @"..\..\..\..\RobotAutoApp\Driver\";
                    startInfo.WorkingDirectory = rootFolder;
                   // startInfo.Arguments = $" / C WinAppDriver 4723 > log.txt";
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.CreateNoWindow = true;
                    startInfo.UseShellExecute = false;
                    process.StartInfo = startInfo;
                    process.Start();
                }
                _log.Info("PMS Server start process completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                return false;
            }
            return true;
        }
        private bool StopServer()
        {
            var stopped = true;
            try
            {
                _log.Info("PMS Server stopped, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                // _log.Info("PMS Server start requested.");
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
            if (ScanningEntities != null && (definitionEntities == null || !definitionEntities.Any()))
            {
               // _log.Info("Data scan watch started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                definitionEntities = new List<MappingDefinitionViewModel>();
                foreach (MappingViewModel ety in ScanningEntities.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active))
                {
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
            if (_generationClient == null)
            {
                this.SetupGenerationClient();
            }
            _generationClient.InitializeRecording(new HookEventHandlerSettings { HasGraphicThreadLoop = true });
           
        }
        public void StartCapteringAuto()
        {
           
            //SetUiForRecorderUiStateIsRecording();
            SetUiForRecorderUiStateIsReady();
            if (_generationClient == null)
            {
                this.SetupGenerationClient();
            }
            _generationClient.InitializeRecording(new HookEventHandlerSettings { HasGraphicThreadLoop = true });

        }

        public void StartCaptering(List<MappingViewModel> lst)
        {
            _log.Info("Data scan watch started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
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
        }

        public void StopCapturingWithTermination()
        {
            SetUiForRecorderUiStateIsStopped();
            SetUiForRecorderUiStateIsDefault();            
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

                 _generationClientUiEventEventHandler = GenerationClientUiEvent;
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
            if (GlobalApp.AUTOMATION_MODE_CONFIG != (int)Enums.AUTOMATION_MODES.HYBRID && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && e.UiEvent != null && e.UiEvent.UiElement != null && submitControls.Contains(e.UiEvent.UiElement.LocalizedControl) && definitionEntities.Where(x => x.SubmitCaptureFieldId == e.UiEvent.UiElement.Name).FirstOrDefault() != null)
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
                //Thread thread = new Thread(() => ScanAndCaptureFound(e.UiEvent));
                //thread.Start();   
                if (uiRecorderThread == null )
                {
                    uiRecorderThread = new Thread(() => ScanAndCaptureFound(e.UiEvent));
                    uiRecorderThread.IsBackground = true;
                    uiRecorderThread.SetApartmentState(ApartmentState.STA);
                    uiRecorderThread.Start();
                }
                                
                else if (uiRecorderThread != null && !uiRecorderThread.IsAlive)
                {
                    //uiRecorderThread.Abort();
                    uiRecorderThread = new Thread(() => ScanAndCaptureFound(e.UiEvent));
                    uiRecorderThread.IsBackground = true;
                    uiRecorderThread.SetApartmentState(ApartmentState.STA);
                    uiRecorderThread.Start();
                }
                else if (uiRecorderThread != null && uiRecorderThread.IsAlive && uiRecorderThread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    uiRecorderThread.Start();

                }
                else if (uiRecorderThread != null && uiRecorderThread.IsAlive && uiRecorderThread.ThreadState == System.Threading.ThreadState.Suspended || (uiRecorderThread.ThreadState == System.Threading.ThreadState.Stopped))
                {
                    uiRecorderThread.Resume();
                }
                else if (uiRecorderThread != null && uiRecorderThread.IsAlive && uiRecorderThread.ThreadState == System.Threading.ThreadState.Running)
                {
                    uiRecorderThread.Join();
                }
                //ScanAndFoundData scan = ScanAndCaptureFound;
                //scan(e.UiEvent);
            }
        }

        private void ScanAndCaptureFound(UiEvent curUIEvent)
        {
            // if ((MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            if (UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            {
                try
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
                    if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
                    {
                        DateTime dt;
                        IntervalControllerDic.TryGetValue("LastActivity", out dt);
                        if (dt.AddSeconds(25) >= DateTime.Now) return;
                    }
                    //PauseGenerationClient();
                    string controlName = string.IsNullOrWhiteSpace(curUIEvent.UiElement.Name) ? curUIEvent.UiElement.AutomationId : curUIEvent.UiElement.Name;
                    MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => controlName.ToLower() == x.SubmitCaptureFieldId.ToLower() && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldXpath) || root.FindElementsByXPath(x.SubmitCaptureFieldXpath).Count>0)).FirstOrDefault();
                    if (captureEntity != null)
                    {
                        //try
                        //{
                        //    WebDriverWait wait = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 10));
                        //    wait.Until(ExpectedConditions.AlertIsPresent());
                        //}
                        //catch (Exception ex) { }
                        // captureEntity.SubmitCaptureFieldExpression = string.Empty;                       
                        if (!string.IsNullOrWhiteSpace(captureEntity.SubmitCaptureFieldExpression) && (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER) || captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER)))
                        {
                            if (!this.SubmitAndCaptureSecondaryParameterCheck(captureEntity))
                                return;
                        }
                        SetUiForRecorderUiStateIsRecording();
                        MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "IntegrateX is scanning for data to integrate, please wait...");
                            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
                            {
                                this.referenceValue = string.Empty;
                                MappingViewModel mObj = new MappingViewModel();
                                mObj = mapping.DCopy();
                                mObj.createdAt = UTIL.GlobalApp.CurrentLocalDateTime;
                                this.addOrUpdate(IntervalControllerDic, "LastActivity", System.DateTime.Now);
                                _log.Info("Reservation found & reservation data scan started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                                ResponseViewModel res = RetrievalFormDataFrom(mObj);
                                if (res.status && mObj != null && !string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan completed, sending reservation# '{0}' to IntegrateX.", mObj.reference));

                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                        this.DataSaved(mObj);
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process stopped, no data found.");
                                }
                                else if (res.status && mObj != null && mObj.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS && string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    if (string.IsNullOrWhiteSpace(mObj.reference))
                                    {
                                        Random generator = new Random();
                                        String rndomn = generator.Next(0, 100000000).ToString("D8");
                                        mObj.reference = rndomn;
                                    }
                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                    {
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan completed, sending {0} to IntegrateX.", mObj.reference));
                                        this.DataSaved(mObj);
                                    }
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan stopped, no candidate data found");
                                }
                                else
                                {
                                    // mObj.status = 0;
                                    mObj.xpath = res.message;
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan stopped, no candidate data found.");
                                    // this.DataSaved(null);
                                }
                                mObj = null;
                                mapping = null;
                                //SetUiForRecorderUiStateIsDefault();
                            }
                        }///  if (mapping != null)  
                        _log.Info("Data retreival from form scan finished and data sent for user interface, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                finally
                {
                     ResumeGenerationClient();
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                }
            }
        }
        public void ScanAndCaptureFoundManual(int scanEntity)
        {
            // if ((MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            if (UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN  && definitionEntities != null && definitionEntities.Count > 0)
            {
                try
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
                    if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
                    {
                        DateTime dt;
                        IntervalControllerDic.TryGetValue("LastActivity", out dt);
                        if (dt.AddSeconds(25) >= DateTime.Now) return;
                    }
                    
                    /* END- Determine this form is for which entity, Retrieve entity id */
                   // MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => controlName.ToLower() == x.SubmitCaptureFieldId.ToLower() && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldXpath) || root.FindElementsByXPath(x.SubmitCaptureFieldXpath).Count > 0)).FirstOrDefault();
                    if (scanEntity> 0)
                    {
                                            
                     
                        SetUiForRecorderUiStateIsRecording();
                        MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == scanEntity).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "IntegrateX is scanning for data to integrate, please wait...");
                            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
                            {
                                this.referenceValue = string.Empty;
                                MappingViewModel mObj = new MappingViewModel();
                                mObj = mapping.DCopy();
                                mObj.createdAt = UTIL.GlobalApp.CurrentLocalDateTime;
                                this.addOrUpdate(IntervalControllerDic, "LastActivity", System.DateTime.Now);
                                _log.Info("Reservation found & reservation data scan started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                                ResponseViewModel res = RetrievalFormDataFrom(mObj);
                                if (res.status && mObj != null && !string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan completed, sending {0} to IntegrateX.", mObj.reference));

                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                        this.DataSaved(mObj);
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process stopped, no data found.");
                                }
                                else if (res.status && mObj != null && mObj.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS && string.IsNullOrWhiteSpace(mObj.reference))
                                {
                                    if (string.IsNullOrWhiteSpace(mObj.reference))
                                    {
                                        Random generator = new Random();
                                        String rndomn = generator.Next(0, 100000000).ToString("D8");
                                        mObj.reference = rndomn;
                                    }
                                    if (!string.IsNullOrWhiteSpace(mObj.reference) && !mObj.reference.ToLower().Contains("new"))
                                    {
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending reservation# {0} data to IntegrateX.", mObj.reference));
                                        this.DataSaved(mObj);
                                    }
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan stopped, no reservation data found");
                                }
                                else
                                {
                                    // mObj.status = 0;
                                    mObj.xpath = res.message;
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan stopped, no reservation data found.");
                                    // this.DataSaved(null);
                                }
                                mObj = null;
                                mapping = null;
                                //SetUiForRecorderUiStateIsDefault();
                            }
                        }///  if (mapping != null)  
                        _log.Info("Data retreival from form scan finished and data sent to IntegrateX, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
                finally
                {
                    ResumeGenerationClient();
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                }
            }
        }
        void addOrUpdate(Dictionary<string, DateTime> dic, string key, DateTime newValue)
        {

            try
            {
                DateTime dt;
                if (dic.TryGetValue(key, out dt))
                {
                    // yay, value exists!
                    dic[key] = newValue;
                }
                else
                {
                    // darn, lets add the value
                    dic.Add(key, newValue);
                }
            }
            catch (Exception ex) { };
        }
        public void HybridScanAndCaptureFound(string scanCommand, int enity = (int)UTIL.Enums.ENTITIES.RESERVATIONS)
        {
            MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
            // if ((MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) && curUIEvent != null && curUIEvent.UiElement != null && submitControls.Contains(curUIEvent.UiElement.LocalizedControl) && definitionEntities != null && definitionEntities.Count > 0)
            if (MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && scanCommand == "scanme" && definitionEntities != null && definitionEntities.Count > 0)
            {
                try
                {
                    if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
                    {
                        DateTime dt;
                        IntervalControllerDic.TryGetValue("LastActivity", out dt);
                        if (dt.AddSeconds(60) >= DateTime.Now) return;
                    }
                    _log.Info("Hybrid data scan started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                    //PauseGenerationClient();
                    // string controlName = string.IsNullOrWhiteSpace(curUIEvent.UiElement.Name) ? curUIEvent.UiElement.AutomationId : curUIEvent.UiElement.Name;
                    var captureEntity = definitionEntities.Where(x => x.entity_Id == enity).FirstOrDefault();
                    if (captureEntity != null)
                    {
                        SetUiForRecorderUiStateIsRecording();
                        MappingViewModel mapping = ScanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "PMS reservation data scan process started..");
                            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
                            {
                                this.referenceValue = string.Empty;
                                MappingViewModel scanEntity = new MappingViewModel();
                                scanEntity = mapping.DCopy();
                                scanEntity.createdAt = System.DateTime.Now;// UTIL.GlobalApp.CurrentLocalDateTime;
                                ResponseViewModel res = this.RetrievalFormDataFromHybrid(scanEntity);
                                if (res.status && scanEntity != null && !string.IsNullOrWhiteSpace(scanEntity.reference))
                                {
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS reservation data scan completed, sending reservation# {0} data to IntegrateX.", scanEntity.reference));


                                    if (!string.IsNullOrWhiteSpace(scanEntity.reference) && !scanEntity.reference.ToLower().Contains("new"))
                                        this.DataSaved(scanEntity);
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS reservation data scan & retrieval process finished with incomplete data..");
                                }
                                else if (res.status && scanEntity != null && scanEntity.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS && string.IsNullOrWhiteSpace(scanEntity.reference))
                                {
                                    // this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending '{0}' to IntegrateX.", mObj.reference));

                                    if (string.IsNullOrWhiteSpace(scanEntity.reference) && !string.IsNullOrWhiteSpace(BDU.UTIL.GlobalApp.TEST_RESERVATION_NO))
                                    {                                   
                                        scanEntity.reference = BDU.UTIL.GlobalApp.TEST_RESERVATION_NO;
                                    } else if (string.IsNullOrWhiteSpace(scanEntity.reference)) {
                                        Random generator = new Random();
                                        String rndomn = generator.Next(0, 100000000).ToString("D8");
                                        scanEntity.reference = rndomn;
                                    }
                                    if (!string.IsNullOrWhiteSpace(scanEntity.reference) && !scanEntity.reference.ToLower().Contains("new"))
                                    {
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan complete, sending {0} to IntegrateX.", scanEntity.reference));
                                        this.DataSaved(scanEntity);
                                    }
                                    else
                                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan & retrieval process finished with incomplete data..");
                                }
                                else
                                {
                                    // mObj.status = 0;
                                    scanEntity.xpath = res.message;
                                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan and retrieval process completed..");
                                    // this.DataSaved(null);
                                }
                                scanEntity = null;
                                mapping = null;
                                //SetUiForRecorderUiStateIsDefault();
                            }
                        }///  if (mapping != null)                       
                    }
                    _log.Info("Hybrid data scan from form finished, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
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

        #region Autoscan events & set data to form
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

                        if (pMeter.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                        {
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
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
                        else if (pMeter.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER))
                        {
                            string[] spMeters = pMeter.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER);
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

                else if (expression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                {
                    string[] spMeters = expression.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
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
                    string[] spMeters = expression.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER);
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

            if (expression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
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
                                        if (root.FindElements(By.Name(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                        {
                                            rStatus = true;
                                        }
                                    }
                                    break;
                                case "xpath":
                                    if (CDriver.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                        {
                                            rStatus = true;
                                        }
                                    }
                                    break;
                                case "automationid":
                                    if (CDriver.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                                    {
                                        if (root.FindElements(By.Id(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                        {
                                            rStatus = true;
                                        }
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
                                if (root.FindElements(By.Name(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                {
                                    rStatus = true;
                                }

                            }
                            break;
                        case "xpath":
                            if (CDriver.FindElements(By.XPath(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                if (root.FindElements(By.XPath(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                {
                                    rStatus = true;
                                }

                            }
                            break;
                        case "automationid":
                            if (CDriver.FindElements(By.Id(Convert.ToString(spMeters[1]).Trim())).Count > 0)
                            {
                                if (root.FindElements(By.Id(Convert.ToString(spMeters[1]))).FirstOrDefault().Enabled)
                                {
                                    rStatus = true;
                                }

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
            List<bool> orResults = new List<bool>();
            // bool found = false;
            try
            {
                List<string> rootExpression = new List<string>();
                List<string> partialExpressions = new List<string>();


                if (captureEntity.SubmitCaptureFieldExpression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_OR_OPERATOR_DELIIMETER))
                    rootExpression = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.SCAN_EXPRESSION_OR_OPERATOR_DELIIMETER).ToList();
                else
                    rootExpression.Add(captureEntity.SubmitCaptureFieldExpression);
                if (rootExpression != null && rootExpression.Any())
                {
                    foreach (string rpExpression in rootExpression)
                    {
                        partialExpressions.Clear();
                        if (("" + rpExpression).Trim().Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
                            partialExpressions = ("" + rpExpression).Trim().Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER).ToList();
                        else
                            partialExpressions.Add(("" + rpExpression).Trim());

                        if (partialExpressions != null && partialExpressions.Any())
                        {
                            //string[] rootExpression = captureEntity.SubmitCaptureFieldExpression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
                            foreach (string expression in partialExpressions)
                            {
                                if (expression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                                {
                                    rStatus = this.EvaluateRPAValidationNotEqualExpression(expression);
                                }
                                else if (expression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER))
                                {
                                    rStatus = this.EvaluateRPAValidationEqualExpression(expression);
                                }
                                if (!rStatus) break;
                                // if (found) break;
                            }//foreach (string pMeter in parameters) {
                        }// Next Root Expression
                        orResults.Add(rStatus);
                    }

                }// Root Expression

                if (orResults.Any() && orResults.Where(x => x.Equals(true)).Count() > 0)
                    rStatus = true;
            }
            catch (Exception ex)
            {
                rStatus = false;
            }
            //root.WrappedDriver.SwitchTo().DefaultContent();
            return rStatus;

        }
        public bool EvaluateRPAValidationEqualExpression(string keyValuePairExpression)
        {
            bool rStatus = false;
            try
            {

                if (keyValuePairExpression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER))
                {
                    rStatus = false;
                    string[] evaExpression = keyValuePairExpression.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_DELIMETER);
                    switch (Convert.ToString(evaExpression[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name(Convert.ToString(evaExpression[1]).Trim())).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.Name(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = true;
                            }
                            break;
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(evaExpression[1]).Trim())).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.XPath(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = true;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(evaExpression[1]).Trim())).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.Id(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = true;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO
            }
            return rStatus;

        }
        public bool EvaluateRPAValidationNotEqualExpression(string keyValuePairExpression)
        {
            bool rStatus = true;
            try
            {
                if (keyValuePairExpression.Contains(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER))
                {
                    rStatus = true;
                    string[] evaExpression = keyValuePairExpression.Split(UTIL.BDUConstants.SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER);
                    switch (Convert.ToString(evaExpression[0]).ToLower().Trim())
                    {
                        case "name":
                            if (root.FindElements(By.Name(Convert.ToString(evaExpression[1]))).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.Name(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = false;
                            }
                            break;   
                        case "xpath":
                            if (root.FindElements(By.XPath(Convert.ToString(evaExpression[1]).Trim())).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.XPath(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = false;
                            }
                            break;
                        case "automationid":
                            if (root.FindElements(By.Id(Convert.ToString(evaExpression[1]).Trim())).FirstOrDefault() != null)
                            {
                                if (root.FindElements(By.Id(Convert.ToString(evaExpression[1]))).FirstOrDefault().Enabled)
                                    rStatus = false;
                            }
                            break; 
                    }
                }

            }
            catch (Exception ex)
            {
                //TODO
            }
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
                List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                this.addOrUpdate(IntervalControllerDic, "LastActivity", System.DateTime.Now);
                // Need Verification, HYBRID
                this.referenceValue = forData.reference;
                if (UTIL.GlobalApp.AUTOMATION_MODE_CONFIG == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID)
                {
                    errorFields = this.testRunEntityLevelHybrid(forData, true);
                }
                else
                    errorFields = CacheFormElementsWithFill(forData, true);

                if (errorFields.Count == 0)
                {
                    rs.jsonData = null;
                    rs.status = true;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                    rs.message = "Successfully form filled.";
                }
                else if (errorFields.Count==1 && (errorFields.Where(x => x.mandatory == (int)UTIL.Enums.STATUSES.Active && x.action_type==(int)UTIL.Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).Count() > 0))
                {

                    rs.jsonData = null;
                    rs.status_code = ((int)UTIL.Enums.ERROR_CODE.NO_DATA).ToString();                    
                    //string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                    rs.message = string.Empty;
                    // rs.message = "Failed to submit data to PMS system.";
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

            }

            return rs;
        }
        private void prepareFieldsCollection(MappingViewModel data, bool isFeed = false)
        {
            try
            {
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

                        }//  if(pFlds.Any()) {
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
                                    additionalServiceDesc = string.IsNullOrWhiteSpace(additionalServiceDesc) ? string.Format("{0}:{1}", String.Concat("" + pItemfld.field_desc, "", " with Taxes"), pItemfld.value) : additionalServiceDesc + "," + string.Format(" {0}:{1}", String.Concat("" + pItemfld.field_desc, "", " with Taxes"), pItemfld.value);
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

        public ResponseViewModel FeedDataToDesktopForm(List<MappingViewModel> entls)
        {
            ResponseViewModel rs = new ResponseViewModel();
            var status = false;
            try
            {
                if (UTIL.GlobalApp.AUTOMATION_MODE_CONFIG == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID)
                {
                    foreach (MappingViewModel mapping in entls)
                    {
                        List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                        MappingViewModel formDataToFillup = mapping;// mapping.DCopy(); Already Cloned
                        this.referenceValue = formDataToFillup.reference;
                        errorFields = this.testRunEntityLevelHybrid(formDataToFillup, false);
                        if (errorFields.Count == 0)
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                            rs.message = "Successfully form filled.";
                        }
                        if (errorFields.Count() == 1 && errorFields.Where(x => x.action_type == (int)Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).Any())
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.NO_DATA).ToString();
                            rs.message = string.Format("Reservation# {0} for {1} is not available or already cancelled", this.referenceValue, UTIL.GlobalApp.StringValueOf((Enums.ENTITIES)mapping.entity_Id));
                        }
                        else if (errorFields.Where(x => x.mandatory == (int)UTIL.Enums.STATUSES.Active).Count() > 0)
                        {
                            rs.jsonData = null;
                            rs.status = false;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.FAILED).ToString();
                            string[] array = errorFields.Select(x => string.IsNullOrEmpty(x.field_desc) ? x.pms_field_xpath : x.field_desc).Take(3).ToArray();
                            rs.message = string.Format("Missing / Invalid Fields, {0}...", string.Join(",", array));
                        }
                    }
                }
                else
                {
                this.SetUiForRecorderUiStateDataFeeding();
                if (session != null && session.desktopSession != null)
                {
                    foreach (MappingViewModel mapping in entls)
                    {
                        List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                        MappingViewModel formDataToFillup = mapping;// mapping.DCopy(); Already Cloned
                        this.referenceValue = formDataToFillup.reference;                     
                        //For Hybrid Call Hybrid Function                      
                            errorFields = CacheFormElementsWithFill(formDataToFillup, true);
                        

                        if (errorFields.Count == 0)
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                            rs.message = "Successfully form filled.";
                        }
                        if (errorFields.Count()==1 && errorFields.Where(x => x.action_type == (int)Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).Any())
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.NO_DATA).ToString();
                            rs.message = string.Format("Reservation# {0} for {1} is not available or already cancelled", this.referenceValue, UTIL.GlobalApp.StringValueOf((Enums.ENTITIES)mapping.entity_Id));
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

                       // formDataToFillup = null;
                    }
                }
            }// try
            }// try
            catch (Exception ex)
            {
                _log.Error(ex);

            }
            finally
            {
                this.SetUiForRecorderUiStateIsReady();
            }

            return rs;
        }
        public ResponseViewModel FeedDataToDesktopFormAuto(List<MappingViewModel> entls)
        {
            ResponseViewModel rs = new ResponseViewModel();
            var status = false;
            try
            {
                //this.SetUiForRecorderUiStateDataFeeding();
                if (session != null && session.desktopSession != null)
                {
                    foreach (MappingViewModel mapping in entls)
                    {
                        List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
                        MappingViewModel formDataToFillup = mapping;// mapping.DCopy(); Already Cloned
                        this.referenceValue = formDataToFillup.reference;

                        //For Hybrid Call Hybrid Function
                        if (UTIL.GlobalApp.AUTOMATION_MODE_CONFIG == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID)
                        {
                            errorFields = this.testRunEntityLevelHybrid(formDataToFillup, true);
                        }
                        else
                        {
                            errorFields = CacheFormElementsWithFill(formDataToFillup, true);
                        }

                        if (errorFields.Count == 0)
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString();
                            rs.message = "Successfully form filled.";
                        }
                       else if (errorFields.Count<= 2 && errorFields.Where(x => x.action_type == (int)Enums.CONTROl_ACTIONS.MANDATORY_CONTROL).Any())
                        {
                            rs.jsonData = null;
                            rs.status = true;
                            rs.status_code = ((int)UTIL.Enums.ERROR_CODE.NO_DATA).ToString();
                            rs.message = string.Format("Reservation# {0} for {1} is not available or cancelled", this.referenceValue, UTIL.GlobalApp.StringValueOf((Enums.ENTITIES)mapping.entity_Id));
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

                        // formDataToFillup = null;
                    }
                }
            }// try
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            

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
            //try
            //{
            //    if (flds != null)
            //    {
            //        List<EntityFieldViewModel> keyWordFields = flds.Where(x => Convert.ToString(x.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX) && x.control_type != (int)Enums.CONTROL_TYPES.NOCONTROL).ToList();
            //        foreach (EntityFieldViewModel fld in keyWordFields)
            //        {
            //            EntityFieldViewModel keywordFld = null;

            //            // REference
            //            if (!forTestRun && Convert.ToString(fld.default_value).Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_PREFIX))// Not for Test
            //            {
            //                if (Convert.ToString(fld.default_value).Trim() == BDUConstants.REFERENCE_KEYWORD)
            //                {
            //                    keywordFld = flds.Where(x => Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.REFERENCE && x.is_reference == (int)UTIL.Enums.STATUSES.Active).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value) && keywordFld.value != "0")
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                        }
            //                        else if (!string.IsNullOrWhiteSpace(this.referenceValue))
            //                            fld.default_value = this.referenceValue;

            //                    }
            //                }
            //                // Guest Name
            //                else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.GUEST_NAME_KEYWORD) //BDU.UTIL.BDUConstants.GUEST_NAME == fld.field_desc || BDU.UTIL.BDUConstants.NEW_GUEST_NAME == fld.field_desc || BDU.UTIL.BDUConstants.NEW_GUEST_NAME == fld.field_desc)
            //                {
            //                    keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.GUEST_NAME || Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_GUEST_NAME)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value))
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                        }
            //                        else
            //                            fld.default_value = string.Empty;

            //                    }
            //                }
            //                else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.BILL_DESC_KEYWORD)// BDU.UTIL.BDUConstants.BILL_DESCRIPTION_FIELD_DESC.ToLower() == ("" + fld.field_desc).ToLower())
            //                {
            //                    keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.BILL_DESCRIPTION_FIELD_DESC)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value))
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                        }
            //                        else
            //                            fld.default_value = string.Empty;

            //                    }
            //                }
            //                else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.BILL_AMOUNT_KEYWORD)
            //                // else if (BDU.UTIL.BDUConstants.BILL_AMOUNT_FIELD_DESC.ToLower() == ("" + fld.field_desc).ToLower())
            //                {
            //                    keywordFld = flds.Where(x => x.entity_id == fld.entity_id && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.BILL_AMOUNT_FIELD_DESC)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value))
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                        }
            //                        else
            //                            fld.default_value = string.Empty;

            //                    }
            //                }
            //                // else if (BDU.UTIL.BDUConstants.ROOM_NO == fld.field_desc || BDU.UTIL.BDUConstants.NEW_ROOM_NO == fld.field_desc)
            //                else if (Convert.ToString(fld.default_value).Trim() == BDUConstants.ROOM_NO_KEYWORD)
            //                {
            //                    keywordFld = flds.Where(x => x.entity_id == fld.entity_id && Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_ROOM_NO).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value))
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                        }
            //                        else
            //                            fld.default_value = string.Empty;

            //                    }
            //                }
            //                else if (BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower() == ("" + fld.default_value).ToLower())
            //                {
            //                    keywordFld = flds.Where(x => x.is_unmapped == (Int32)UTIL.Enums.STATUSES.InActive && (Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.GUEST_NAME || Convert.ToString(x.field_desc) == BDU.UTIL.BDUConstants.NEW_GUEST_NAME)).FirstOrDefault();// && Convert.ToString(x.default_value).ToLower().Contains(BDU.UTIL.BDUConstants.GUEST_NAME_KEYWORD.ToLower())).FirstOrDefault();
            //                    if (keywordFld != null)
            //                    {
            //                        if (!string.IsNullOrWhiteSpace(keywordFld.value))
            //                        {
            //                            fld.default_value = keywordFld.value;
            //                            fld.value = keywordFld.value;
            //                        }
            //                        else
            //                            fld.default_value = string.Empty;
            //                    }
            //                }
            //            }
            //            else
            //            {// for Test                           
            //                if (!string.IsNullOrWhiteSpace(fld.default_value) && Convert.ToString(fld.pms_field_expression).Contains(fld.default_value))
            //                {
            //                    if (Convert.ToString(fld.pms_field_expression).Contains(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER))
            //                    {
            //                        string[] keywordKeyValues = fld.pms_field_expression.Split(UTIL.BDUConstants.EXPRESSION_KEYWRODS_DELIMETER);
            //                        foreach (string keywordKeyValue in keywordKeyValues)
            //                        {
            //                            if (!(keywordKeyValue.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_FEED) || keywordKeyValue.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_SCAN)))
            //                            {
            //                                if (keywordKeyValue.Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
            //                                {
            //                                    string[] KeyValues = keywordKeyValue.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
            //                                    if (KeyValues.Length > 1)
            //                                    {
            //                                        if (Convert.ToString(KeyValues[0]).ToLower() == fld.default_value.ToLower())
            //                                            fld.default_value = KeyValues[1];
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }// Multiple
            //                    else if (Convert.ToString(fld.pms_field_expression).Contains(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER))
            //                    {
            //                        if (!(fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_FEED) || fld.pms_field_expression.Contains(UTIL.BDUConstants.SPECIAL_KEYWORD_SCAN)))
            //                        {
            //                            string[] KeyValues = fld.pms_field_expression.Split(UTIL.BDUConstants.EXPRESSION_VALUE_DELIMETER);
            //                            if (KeyValues.Length > 1)
            //                            {
            //                                if (Convert.ToString(KeyValues[0]).ToLower() == Convert.ToString(fld.default_value).ToLower())
            //                                    fld.default_value = KeyValues[1];
            //                            }
            //                        }
            //                    }// SINGLE Case
            //                }
            //            }
            //        }// ForEach
            //    }// if(flds!= null) { 

            //}
            //catch (Exception ex)
            //{
            //    _log.Error(ex);
            //}
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
            //elementDict.TryGetValue(field.fuid + "_" + Convert.ToString(field.entity_id), out element);
            //if (element != null)
            //    try { string tag = element.TagName; } catch (Exception) { element = null; };
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
                  //  if (element != null && !elementDict.ContainsKey(field.fuid + "_" + Convert.ToString(field.entity_id)))
                     //   elementDict.Add(field.fuid + "_" + Convert.ToString(field.entity_id), element);

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
                        if (element.Text == value || element.GetAttribute("Value") == value)
                        {
                            return value;
                        }
                        element.SendKeys(Keys.Control + "a");
                        element.SendKeys(Keys.Delete);
                        element.Clear();
                    }
                    else if (element.Enabled && !string.IsNullOrWhiteSpace(element.Text))
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
                    if (element != null && !string.IsNullOrWhiteSpace(value))
                    {
                        // element.Clear();
                        element.SendKeys(value);
                    }


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
                    if (element != null && !string.IsNullOrWhiteSpace(value))
                    {
                        // element.Clear();
                        element.SendKeys(value);
                        WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 5));
                        waitInput.Until(ExpectedConditions.TextToBePresentInElementValue(element, value));
                    }


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
                        if (element != null && !string.IsNullOrWhiteSpace(value))
                        {
                            // element.Clear();
                            element.SendKeys(value);
                        }

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
                        if (element != null && !string.IsNullOrWhiteSpace(value))
                        {
                            // element.Clear();
                            element.SendKeys(value);
                        }


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
                        if (element != null && !string.IsNullOrWhiteSpace(value))
                        {
                            // element.Clear();
                            element.SendKeys(value);
                        }


                        //var tm = GetDate(value);
                        // element.SendKeys(Keys.Control + "a");
                        //element.SendKeys(time);
                    }
                    else
                    {

                        //element.SendKeys(Keys.Control + "a");
                        if (element != null && !string.IsNullOrWhiteSpace(element.Text) && !string.IsNullOrWhiteSpace(value))
                        {
                            element.Clear();
                            element.SendKeys(value);
                            WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 4));
                            waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                        }
                        else if (!string.IsNullOrWhiteSpace(value))
                        {
                            element.SendKeys(value);
                            WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 4));
                            waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                        }
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
                    //List<AppiumWebElement> rows = element.FindElements(By.XPath("//tr[contains('PRINTFIL')]//td[2]")).ToList();
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

                    try
                    {
                        if (element != null && !string.IsNullOrWhiteSpace(value))
                        {
                            //element.Clear();
                            element.SendKeys(value);
                            WebDriverWait waitInput = new WebDriverWait(session.desktopSession, new TimeSpan(0, 0, 10));
                            waitInput.Until(ExpectedConditions.TextToBePresentInElement(element, value));
                        }

                    }
                    catch (Exception eXr)
                    {
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
            //  element.Clear();
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
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return selected;
        }
        #endregion

        #region Get Data From & form nested controls 
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
                                    this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "IntegrateX has started scan for data to integrate, please wait...");
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
