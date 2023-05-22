using System;
using System.Collections.Generic;
using System.Linq;
using BDU.RobotWeb.EventCapture;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using static BDU.UTIL.Enums;
using WebDriverManager;

namespace BDU.RobotWeb
{
    public class WebHandler
    {
        #region "Object & variable declarion & intialization"
        private Logger _log = LogManager.GetCurrentClassLogger();
        public delegate void SaveDataEvent(MappingViewModel mappingViewModel);
        public event SaveDataEvent blazorScanDataSaveEvent;
        private BDUService _bduservice = new BDUService();
        // Update UI Event
        public delegate void UddateUIAboutDataEvent(UTIL.Enums.SYNC_MESSAGE_TYPES mType, string msg);
        public event UddateUIAboutDataEvent blazorUpdateUIAboutPMSEvent;
        // public SaveDataEvent DataSaved;
        // private BlazorWebEventListener _eventhandler = null;
        EventCapturingWebDriver iListner = null;
        //  EventFiringWebDriver _eventfDriver = null;
        public MappingViewModel _webData { get; set; }
        public int Undo { get; set; }
        // private MappingViewModel _formMapping = new MappingViewModel();
        public MappingViewModel _access = null; 
        public List<EntityFieldViewModel> _fields { get; set; }
        public bool LoggedIn { get; set; } = false;
        Dictionary<string, DateTime> IntervalControllerDic { get; set; }= new   Dictionary<string, DateTime>();
        public string currentURL { get; set; }
        public List<MappingViewModel> scanningEntities = null;
        private List<MappingDefinitionViewModel> definitionEntities = null;
        // private List<string> submitControls = new List<string> { "button","select", "form", "li", "a", "span" };// { "edit", "button","input", "pane" };
        private List<string> submitControls = new List<string> { "button", "checkbox", "form", "li", "a", "span" };
        public IWebElement scannAndCaptureElement = null;
        public string NoURL = "data:,";
       // BackgroundWorker bWorker =null;
        WebSession session { get; set; }
       // IWebDriver eventHandler;
        //public ROBOT_UI_STATUS ROBOTCurrentStatus = ROBOT_UI_STATUS.DEFAULT;
        public PMS_LOGGIN_STATUS MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;  
        bool started = false;
        #endregion      
        #region "Properties & Constructors"
        public string _ApplicationNameWithURL { get; set; }
        public string _ApplicationDefaultUrl { get; set; }

        public WebHandler(MappingViewModel access)
        {
           // _ApplicationUrl = ApplicationUrl;
            session = new WebSession(Enums.WebBrowser.Chrome);            // Event handler
                                                                          // bWorker  = new BackgroundWorker();
            iListner = new EventCapturingWebDriver(session.driver);          
            session.driver = iListner;

            session._access = access;
            _access = access;
            _fields = (from f in access.forms
                      from flds in f.fields
                      where flds.status == (int)UTIL.Enums.STATUSES.Active                       
                       select new EntityFieldViewModel {
                          control_type = flds.control_type,
                          data_type = flds.data_type,
                           field_desc = flds.field_desc,
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

            EntityFieldViewModel fld = _fields.Where(x => x.field_desc.ToLower() == "applicationwithname").FirstOrDefault();
            if (fld != null)
            {
                _ApplicationDefaultUrl = fld.default_value;
                _ApplicationNameWithURL = fld.default_value;
                session.ApplicationUrl = _ApplicationNameWithURL;
            }
            // Fill DataGrid
        }
        public WebHandler(string ApplicationUrl )
        {           
            try
            {
                session = new WebSession(UTIL.Enums.WebBrowser.Chrome);
                iListner = new EventCapturingWebDriver(session.driver);
                //inputListener.ElementClickCaptured += Driver_ElementClickCaptured;
                //inputListener.ElementKeyPressCaptured += Driver_ElementKeyPressCaptured;
                session.driver = iListner;
                //Actions action = new Actions(session.driver);
                //action.
                //action.Click()                   
                //    .keyUp(Keys.Enter)
                //    .keyUp(Keys.Down)
                //    .Cl
                //    .contextClick()
                //    .build()
                //    .perform();

                // Thread.Sleep(5000);
                

                // _eventhandler.blazorDataFoundSaveEvent += DataRecievedEventHandler;
                MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
        }
        #endregion
        #region "Private local methods"
        private bool StartServer()
        {
            try
            {
                _log.Info(string.Format("IntegrateX Web AI engine started.., at- {0}", GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
               // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS session intialization started..");
                if (session == null)
                {
                    session = new WebSession(Enums.WebBrowser.Chrome);                    
                    session.StartServer();
                    iListner = new EventCapturingWebDriver(session.driver);                 
                    session.driver = iListner;
                    MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                }
                else if (session.driver == null) {
                    session.StartServer();
                    iListner = new EventCapturingWebDriver(session.driver);                  
                    session.driver = iListner;
                }

                started = true;
                
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                started = false;
                return false;
            }           
            return true;
        }
        private bool StopServer()
        {
            var stopped = true;
            try
            {
                if (session != null)
                {
                    _log.Info("IntegrateX Web AI engine stop started.., at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                    session.StopServer();
                    session = null;
                    MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;                 
                }
                if (iListner != null)
                {
                    iListner = null;
                  
                }
                _log.Info("IntegrateX Web AI engine stopped, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                stopped = false;
            }          
            started = stopped?false:true;
            return stopped;
        }
        #endregion
        #region "Login Methods"
        public bool StartSession(List<MappingViewModel> ls)
        {
            bool started = false;
            try
            {
                session._bduservice = _bduservice;
                if (started == false && StartServer())
                {
                    // session = new WebSession("");
                    if (session != null)
                    {

                        scanningEntities = ls;
                        session.scanningEntities = scanningEntities;
                        //  _eventhandler.scannableEntities = scanningEntities;
                        if (scanningEntities != null)
                        {
                            definitionEntities = new List<MappingDefinitionViewModel>();
                            foreach (MappingViewModel ety in ls.Where(x=>x.status== (int)UTIL.Enums.STATUSES.Active))
                            {
                               // if (ety.status == (int)UTIL.Enums.STATUSES.Active)
                               // {
                                    foreach (FormViewModel frm in ety.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active))
                                    {
                                      //  if (frm.Status == (int)UTIL.Enums.STATUSES.Active)
                                       // {
                                            foreach (EntityFieldViewModel flds in frm.fields.Where(x=>x.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE && x.status== (int)UTIL.Enums.STATUSES.Active))
                                            {
                                              //  if (frm.Status == (int)UTIL.Enums.STATUSES.Active && flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE)
                                               // {
                                                    MappingDefinitionViewModel defEntity = new MappingDefinitionViewModel { entity_Id = ety.entity_Id, pmsformid = Convert.ToString(frm.pmspageid), xpath = Convert.ToString(ety.xpath) };
                                                    defEntity.SubmitCaptureFieldId = flds.pms_field_name;
                                                    defEntity.SubmitCaptureFieldXpath = flds.pms_field_xpath;
                                                    defEntity.SubmitCaptureFieldExpression = flds.pms_field_expression;
                                                    definitionEntities.Add(defEntity.DCopy());
                                               // }
                                            }
                                       // }
                                    }

                               // }//foreach (MappingViewModel ety in scanningEntities) {
                            }

                        }
                        started = true;
                    }
                }
            }// Try Block
            catch (Exception ex)
            {
                //TODO private Logger _log = LogManager.GetCurrentClassLogger();
                _log.Fatal("IX-Failure login", ex);
            }

            return started;
        }

        public bool StartSessionAndLogin(MappingViewModel mapping)
        {
            bool started = false;
            if (started == false && StartServer())
            {
                _access = mapping.DCopy();
                // session = new WebSession("");
                if (session != null && MPSLoginStatus== PMS_LOGGIN_STATUS.LOGGED_OUT)
                {
                   ResponseViewModel res= this.login();
                    if (res.status)
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                    }
                    else
                    {
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                      //  _access = mapping;
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;
                    }
                    // this.TestRun(mapping);
                    // SetupGenerationClient();
                    started = true;
                }
                this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS session intialization started..");
            }
            return started;
        }
        public void StopSession()
        {
            //StopCaptering();
            StopServer();
            scanningEntities = null;
           // _formMapping = null;
            _webData = null;
            if(iListner!= null)
            iListner.Dispose();
            iListner = null;
            started = false;
            _access = null;

        }
        private ResponseViewModel login()
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                //_log.Info("IX web login started running");
                _log.Info("IX web login started running, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS login started..");
                if (MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT)
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                    res = session.login(_access);
                    if (res.status)
                    {
                        // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS login success.");
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else
                        MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;// not log & logged out is same
                }
                _log.Info("IntegrateX web login has completed successfully, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = string.Format("PMS loading is failed,  hotel- {0}, error - {1} ", GlobalApp.Hotel_Name, ex.Message + "-" + ex.StackTrace.ToString()), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                _log.Error(ex);
            }
            finally
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS login finished..");
            }
            //Console.WriteLine("* End *");
            return res;
        }
        #endregion
        #region Set Data To Form
        #region "External Call Methods"

        public ResponseViewModel FeedDataToWebForm(MappingViewModel data)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                _log.Info("Push data to PMS started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                //_log.Info("Push data to PMS started");
                if (session != null && session.driver != null && (this.MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT || session.driver.Url.ToLower().Contains(this.NoURL)))
                {
                    res = this.login();
                    if (res.status)
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.ERROR_STATUS;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                    }
                }

                if (session != null && session.driver != null && UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY) {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS, "GuestX reservation data integration with PMS started..!");
                    res = session.FeedDataToWebForm(data);
                }                   
                else {
                    res.message = "PMS is not running or logged out or license expired, Please try aagain or contact contact@servrhotels.com";
                    res.status_code = UTIL.Enums.ERROR_CODE.FAILED.ToString();
                }
                _log.Info("Push data completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Fatal("IX-Failure login", ex);
               // _log.Error(ex);
            }
            finally
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "GuestX reservation data integration with PMS completed.");
            }
            //Console.WriteLine("* End *");
            return res;
        }
        public ResponseViewModel FeedAllDataToWebForm(List<MappingViewModel> ls, bool fillOption= false)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                _log.Info("Integrate all data to PMS started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
               // _log.Info("Push bulk data to PMS started");
                if (session != null && session.driver != null && (this.MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT || session.driver.Url.ToLower().Contains(this.NoURL)))
                {
                    res = this.login();
                    if (res.status)
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.ERROR_STATUS;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                    }
                }

                if (session != null && session.driver != null && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SCANNING && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.ERROR_STATUS)
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                    this.addOrUpdate(IntervalControllerDic, "LastActivity", System.DateTime.Now);
                   // IntervalControllerDic.Add("LastActivity", System.DateTime.Now);
                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS, "GuestX data integration with PMS started, Please wait...");
                    res = session.feedDataToWebForm(ls, fillOption);
                    if (!res.status && res.status_code != ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                        res.status_code = ((int)Enums.ERROR_CODE.FAILED).ToString();
                }
                else
                {
                    res.message = "GuestX data integration with PMS started, Please wait...";
                    res.status_code = UTIL.Enums.ERROR_CODE.FAILED.ToString();
                }
                _log.Info("GuestX data integration completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Fatal("IX-Failure login", ex);
               // _log.Error(ex);
            }
            finally
            {
                if(res.status)
                session.driver.Navigate().GoToUrl(session.driver.Url);
               // session.driver.Navigate(session.driver.Url);
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
               // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS data fill up finished..");
            }           
            return res;
        }
        public ResponseViewModel FeedAllDataToWebFormAuto(List<MappingViewModel> ls, bool fillOption = false)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                _log.Info("Integrate all data to PMS started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                // _log.Info("Push bulk data to PMS started");
                if (session != null && session.driver != null && (this.MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT || session.driver.Url.ToLower().Contains(this.NoURL)))
                {
                    res = this.login();
                    if (res.status)
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.ERROR_STATUS;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                    }
                }

                if (session != null && session.driver != null && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SCANNING && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.ERROR_STATUS)
                {
                    UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                    this.addOrUpdate(IntervalControllerDic, "LastActivity", System.DateTime.Now);
                    // IntervalControllerDic.Add("LastActivity", System.DateTime.Now);
                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS, "GuestX data sync with PMS started, Please wait...");
                    res = session.feedDataToWebForm(ls, fillOption);
                    //if (!res.status)
                    //    res.status_code = ((int)Enums.ERROR_CODE.FAILED).ToString();
                }
                else
                {
                    res.message = "PMS is not running or logged out or license expired, Please try again or contact: contact@servrhotels.com for assistance.";
                    res.status_code = UTIL.Enums.ERROR_CODE.FAILED.ToString();
                }
                _log.Info("Integrate all data process completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Fatal("IX-Failure login", ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                // _log.Error(ex);
            }
            finally
            {
                if (res.status)
                    session.driver.Navigate().GoToUrl(session.driver.Url);
                // session.driver.Navigate(session.driver.Url);
                //UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                // this.blazorUddateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.COMPLETE, "PMS data fill up finished..");
            }
            return res;
        }
        public ResponseViewModel TestRun(List<EntityFieldViewModel> fields)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;                
                _log.Info("Test run started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                if (session != null && session.driver != null && (this.MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_OUT || session.driver.Url.ToLower().Contains(this.NoURL)))
                {
                    res = this.login();
                    if (res.status)
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_IN;
                    }
                    else
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.ERROR_STATUS;
                        this.MPSLoginStatus = PMS_LOGGIN_STATUS.LOGGED_OUT;
                    }
                }
                if (fields != null && started)
                    res = session.TestRunEntityFields(fields);
                _log.Info("Test run completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                _log.Error(ex);
            }
            finally
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
            }
            return res;
        }

        void addOrUpdate(Dictionary<string, DateTime> dic, string key, DateTime newValue)
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

        public ResponseViewModel TestRun(List<MappingViewModel> data, bool withFillOption=false)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {                
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.FEEDING_DATA;
                if (StartServer())
                {
                    this.addOrUpdate(IntervalControllerDic,"LastActivity", System.DateTime.Now);
                    this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS test run started..");
                    if (!withFillOption)
                        res = session.TestRunMapping(data, withFillOption);
                    else { 
                        res = session.TestRunAccess(data, withFillOption);
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
            }
            finally
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY;
            }
            return res;
        }
        public void intializeEventActionsData()
        {
            if (this.MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN && ( UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT))
            {
                UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.READY; 
            }
        }
        public async  void StartCapturing()
        {
            if (iListner != null)
            {
                try {                 
                // _eventhandler.intializeEventActionsData(scanningEntities);//(new HookEventHandlerSettings { HasGraphicThreadLoop = true });
                if ((UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && scanningEntities != null && scanningEntities.Count() > 0 && definitionEntities != null && UTIL.GlobalApp.Authentication_Done)
                {
                    _log.Info("Started capturing data, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                    iListner.ElementClickCaptured += Driver_ElementClickCaptured;
                    iListner.ElementSubmitCaptured += Driver_SubmitElementClickCaptured;
                    session.driver = iListner;
                    session.driver.Navigate().GoToUrl(session.driver.Url);
                }
                  
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }
          
            
        }
       // https://stackoverflow.com/questions/59262263/how-to-keep-track-of-mouse-events-and-position-using-selenium-and-javascript-in
       
     
        public void StopCaptering()
        
        {
            SetUiForRecorderUiStateIsStopped();
           
            if (iListner != null ) // siddique
            {               
                session.driver = iListner;           
            }
            SetUiForRecorderUiStateIsDefault();
        }
        #endregion
        private void SetUiForRecorderUiStateIsStopped()
        {
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.STOPPED;
        }

        private void SetUiForRecorderUiStateIsRecording()
        {
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.SCANNING;
        }

        private void SetUiForRecorderUiStateIsDefault()
        {
            UTIL.GlobalApp.currentIntegratorXStatus = ROBOT_UI_STATUS.DEFAULT;
        }

        #endregion
        public void DataRecievedEventHandler(MappingViewModel mapping)
        {
            // Retrieve Form Data
            try
            {
               // if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA) return; 
                SetUiForRecorderUiStateIsRecording();
                if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING && MPSLoginStatus == PMS_LOGGIN_STATUS.LOGGED_IN)
                {
                    IJavaScriptExecutor jsi = (IJavaScriptExecutor)session.driver;

                    ResponseViewModel res = session.RetrievalFormDataFromWeb(mapping);
                    if (res.status) {
                        mapping = (MappingViewModel)res.jsonData;
                    }
                    if (res.status && mapping != null)
                    {                   
                        if ((!string.IsNullOrWhiteSpace(session.pmsReferenceExpression) &&  string.IsNullOrWhiteSpace(mapping.reference)) || (""+mapping.reference).Contains("new"))
                        {
                            System.Threading.Thread.Sleep(UTIL.BDUConstants.AUTOMATION_COMMON_DELAY_WEB_MSECS);                            
                            mapping.reference = Convert.ToString(jsi.ExecuteScript(session.pmsReferenceExpression));
                            session.referenceValue = mapping.reference;
                            if(!string.IsNullOrWhiteSpace(mapping.reference))
                            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("PMS data scan completed, sending {0} {1} to IntegrateX.", mapping.reference, mapping.entity_name));
                        }
                        if (!string.IsNullOrWhiteSpace(mapping.reference) && !mapping.reference.ToLower().Contains("new"))
                            this.blazorScanDataSaveEvent(mapping);
                        else
                            this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.INFO, "PMS data scan stopped, no candidate data found.");
                    }
                    else
                    {
                        mapping.status = 0;
                        mapping.xpath = res.message;
                        this.blazorUpdateUIAboutPMSEvent(UTIL.Enums.SYNC_MESSAGE_TYPES.ERROR, "PMS data scan & retrieval process completed..");
                        this.blazorScanDataSaveEvent(null);
                    }
                    mapping = null;                   
                    SetUiForRecorderUiStateIsDefault();
                }
            }// Catch
            catch (Exception ex)
            {
                _log.Error("IX-Failure login", ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
            }
            finally {
                SetUiForRecorderUiStateIsDefault();
            }
        }
        #region "User Input Event Captures"

        public void SubmitAndScan(int scanEntity)
        {
            if (UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.FEEDING_DATA && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SCANNING && UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS)
            {
                if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
                {
                    DateTime dt;
                    IntervalControllerDic.TryGetValue("LastActivity", out dt);
                    if (dt.AddSeconds(25) >= DateTime.Now) return;
                }
                // Boolean isActual = true;
              //  if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return;
                // if ( UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return;
                try
                {

                    IWait<IWebDriver> waitDC = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                    waitDC.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
                   
                    if (scanEntity>0)
                    {
                        // scannAndCaptureElement = element;
                        MappingViewModel mapping = scanningEntities.Where(x => x.entity_Id == scanEntity && x.status == (int)BDU.UTIL.Enums.RESERVATION_STATUS.ACTIVE).FirstOrDefault();
                        if (mapping != null)
                        {

                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "IntegrateX is scanning for data to integrate, please wait...");                           
                            mapping.status = (int)UTIL.Enums.STATUSES.Active;
                            _webData = mapping.DCopy();
                            // Call Data Recieved
                            _log.Info(string.Format("Entity {0} data scan started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED), _webData.entity_name));
                            DataRecievedEventHandler(_webData);
                            // iListner.ElementClickCaptured += Driver_ElementClickCaptured;
                            session.driver = iListner;

                            // this.bWorker.ReportProgress(100);
                        }
                    }


                }
                catch (NoSuchElementException u)
                {
                    //TODO
                    scannAndCaptureElement = null;
                }
                catch (StaleElementReferenceException u)
                {
                    //TODO
                    scannAndCaptureElement = null;
                }
                catch (UnhandledAlertException u)
                {
                    // session.driver.SwitchTo().Alert().Accept();
                    session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToLower().Contains("alert"))
                    {
                        session.driver.SwitchTo().Alert().Accept();
                        session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
                        // element = session.driver.SwitchTo().ActiveElement();
                    }
                    _log.Error( ex);
                    _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                }
                finally
                {
                    scannAndCaptureElement = null;
                }
            }// Outer Check

        }

        private void Driver_ElementClickCaptured(object sender, WebElementCapturedMouseEventArgs e)
        {
            
            //System.Threading.Thread.Sleep(2000);
            //IWait<IWebDriver> waitPageA = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10.00));
            //waitPageA.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
            if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
            {
                DateTime dt;
                IntervalControllerDic.TryGetValue("LastActivity", out dt);
                if (dt.AddSeconds(60) >= DateTime.Now) return;
            }
            Boolean isActual = true;
            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING ||UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return  ;
           // if ( UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return;
            try
            {
               // ((IJavaScriptExecutor)session.driver).ExecuteScript("window.stop();");
                // System.Threading.Thread.Sleep(1200);
                IWait<IWebDriver> waitDC = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                waitDC.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
                scannAndCaptureElement = null;
                //  e.preventDefault();
                IWebElement element =null;
                try { 
                    string elementName = e.Element.TagName;
                    element = e.Element;

                }
                catch (StaleElementReferenceException sExp)
                {
                    
                    element = null;
                    isActual = false;
                    //TODO
                    foreach (MappingDefinitionViewModel mView in definitionEntities)
                    {
                        //System.Threading.Thread.Sleep(3000);
                        //if (element == null && ("" + mView.pmsformid).ToLower() == session.driver.Url.ToLower() && !string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldXpath))
                        if (element == null && (session.driver.Url.ToLower().Contains(("" + mView.pmsformid).ToLower()) && !string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldXpath)))
                        {
                            if (session.driver.FindElements(By.XPath(mView.SubmitCaptureFieldXpath)).Count > 0)
                                element = session.driver.FindElement(By.XPath(mView.SubmitCaptureFieldXpath));
                        }// Xpath If
                         // if (!string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldId))
                        if (element == null && ("" + mView.pmsformid).ToLower() == session.driver.Url.ToLower() && !string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldId))
                        {
                            if (session.driver.FindElements(By.Id(mView.SubmitCaptureFieldId)).Count > 0)
                                element = session.driver.FindElement(By.Id(mView.SubmitCaptureFieldId));
                        }// Outer If

                        if (element != null && element.Displayed) break;

                    }// For Each
                } // Try Catch             
                if (element== null || (!isActual && element.TagName == "form")) return;
                // if (element != null && element.TagName.ToLower() == "button" && element.GetAttribute("type") == "submit")
               if (element != null && (this.submitControls.Contains(element.TagName.ToLower()) || this.submitControls.Contains(element.GetAttribute("type"))) && (element.GetAttribute("type") == "submit"))
                {
                    ((IJavaScriptExecutor)session.driver).ExecuteScript("return window.stop;");
                   // return;
                }
                if (element != null && !string.IsNullOrWhiteSpace(element.TagName) && (this.submitControls.Contains(element.TagName.ToLower()) || this.submitControls.Contains(element.GetAttribute("type"))) && definitionEntities != null && definitionEntities.Count > 0)
                {
                    //MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => (element.Equals(session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath))))).FirstOrDefault();
                    MappingDefinitionViewModel captureEntity = null;
                    if (!isActual && element.TagName.ToLower() != "form")
                    {
                        var capturedls = definitionEntities.Where(x => session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower()) && string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression));
                        if (capturedls != null && capturedls.Count() == 1)
                            captureEntity = capturedls.FirstOrDefault();
                        else
                            captureEntity = definitionEntities.Where(x => (string.IsNullOrWhiteSpace(x.pmsformid) || session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower())) && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression) ? true : Convert.ToString((((IJavaScriptExecutor)session.driver).ExecuteScript(x.SubmitCaptureFieldExpression))) == "True")).FirstOrDefault();
                    }

                    else if (isActual)
                    {
                        IWait<IWebDriver> waitA = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                        waitA.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
                        //captureEntity = definitionEntities.Where(x => (element.Equals(session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null)) && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression) ? true : Convert.ToString((((IJavaScriptExecutor)session.driver).ExecuteScript(x.SubmitCaptureFieldExpression))) == "True")).FirstOrDefault();
                        captureEntity = definitionEntities.Where(x => ((((element.Equals(session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null)) && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression) ? true : Convert.ToString((((IJavaScriptExecutor)session.driver).ExecuteScript(x.SubmitCaptureFieldExpression))) == "True")) || (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count <= 0 && !string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression) && (Convert.ToString((((IJavaScriptExecutor)session.driver).ExecuteScript(x.SubmitCaptureFieldExpression))) == "True"))))).FirstOrDefault();
                    }
                    else
                    {
                        IWait<IWebDriver> waitA = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                        waitA.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
                        captureEntity = definitionEntities.Where(x => (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null) != null && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).TagName == element.TagName && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).Location == element.Location && (string.IsNullOrWhiteSpace(x.pmsformid) || session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower()))).FirstOrDefault();
                    }
                    // old
                   // if (isActual)
                   //     captureEntity = definitionEntities.Where(x => (element.Equals(session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null))).FirstOrDefault();
                   //else
                   //      captureEntity = definitionEntities.Where(x => (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)):null) != null && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).TagName== element.TagName && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).Location == element.Location  && ("" + x.pmsformid).ToLower() == session.driver.Url.ToLower()).FirstOrDefault();
                   
                    if (captureEntity != null)
                    {                        
                        scannAndCaptureElement = element;
                        MappingViewModel mapping = scanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "PMS data scan process started..");
                            mapping.status = (int)UTIL.Enums.STATUSES.Active;
                            _webData = mapping.DCopy();
                            // Call Data Recieved
                            _log.Info(string.Format("Entity {0} data scan started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED), _webData.entity_name));
                            DataRecievedEventHandler(_webData);
                            // iListner.ElementClickCaptured += Driver_ElementClickCaptured;
                            session.driver = iListner;

                            // this.bWorker.ReportProgress(100);
                        }
                    }

                }
            }
            catch (NoSuchElementException u)
            {
                //TODO
                scannAndCaptureElement = null;
            }
            catch (StaleElementReferenceException u)
            {
                //TODO
                scannAndCaptureElement = null;
            }
            catch (UnhandledAlertException u)
            {
                // session.driver.SwitchTo().Alert().Accept();
                session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("alert"))
                {
                    session.driver.SwitchTo().Alert().Accept();
                    session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    // element = session.driver.SwitchTo().ActiveElement();
                }
                _log.Error(ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = ex.Message + "" + ex.StackTrace, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
            }
            finally {
                scannAndCaptureElement = null;
            }
            
        }
        private void Driver_SubmitElementClickCaptured(object sender, WebElementCapturedMouseEventArgs e)
        {

            if (IntervalControllerDic != null && IntervalControllerDic.Count > 0)
            {
                DateTime dt;
                IntervalControllerDic.TryGetValue("LastActivity", out dt);
                if (dt.AddSeconds(120) >= DateTime.Now) return;
            }
            Boolean isActual = true;
            if (UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.FEEDING_DATA || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return;
            // if ( UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SCANNING || UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS) return;
            try
            {
                scannAndCaptureElement = null;

                // Alert Handling
                try
                {
                    WebDriverWait wait = new WebDriverWait(session.driver, TimeSpan.FromSeconds(8));
                    if (wait.Until(ExpectedConditions.AlertIsPresent()) != null)
                    {
                        return;
                    }
                }
                catch (Exception exp)
                {
                    //TODO LOG
                }
                    //  e.preventDefault();
                    IWebElement element = null;
                try
                {
                    string elementName = e.Element.TagName;
                    element = e.Element;
                    if (!this.submitControls.Contains(elementName))
                    {
                        return;
                    }
                    
                }
                catch (StaleElementReferenceException sExp)
                {
                    element = null;
                    isActual = false;
                    //TODO
                    foreach (MappingDefinitionViewModel mView in definitionEntities)
                    {

                        if (element == null && ("" + mView.pmsformid).ToLower() == session.driver.Url.ToLower() && !string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldXpath))
                        {
                            if (session.driver.FindElements(By.XPath(mView.SubmitCaptureFieldXpath)).Count > 0)
                                element = session.driver.FindElement(By.XPath(mView.SubmitCaptureFieldXpath));
                        }// Xpath If
                         // if (!string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldId))
                        if (element == null && ("" + mView.pmsformid).ToLower() == session.driver.Url.ToLower() && !string.IsNullOrWhiteSpace(mView.SubmitCaptureFieldId))
                        {
                            if (session.driver.FindElements(By.Id(mView.SubmitCaptureFieldId)).Count > 0)
                                element = session.driver.FindElement(By.Id(mView.SubmitCaptureFieldId));
                        }// Outer If

                        if (element != null && element.Displayed) break;

                    }// For Each
                } // Try Catch             

                // if (element != null && element.TagName.ToLower() == "button" && element.GetAttribute("type") == "submit")
                if (element != null && (this.submitControls.Contains(element.TagName.ToLower()) || this.submitControls.Contains(element.GetAttribute("type"))) && (element.GetAttribute("type") == "submit"))
                {
                    ((IJavaScriptExecutor)session.driver).ExecuteScript("return window.stop;");
                    // return;
                }
                if (element != null && !string.IsNullOrWhiteSpace(element.TagName) && (this.submitControls.Contains(element.TagName.ToLower()) || this.submitControls.Contains(element.GetAttribute("type"))) && definitionEntities != null && definitionEntities.Count > 0)
                {
                    //MappingDefinitionViewModel captureEntity = definitionEntities.Where(x => (element.Equals(session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath))))).FirstOrDefault();
                    System.Threading.Thread.Sleep(600);
                    MappingDefinitionViewModel captureEntity = null;
                    if (isActual && element.TagName.ToLower() == "form")
                    {
                        var capturedls = definitionEntities.Where(x => session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower()) && string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression));
                        if (capturedls != null && capturedls.Count() == 1)
                            captureEntity = capturedls.FirstOrDefault();
                        else
                            captureEntity = definitionEntities.Where(x => session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower()) && (string.IsNullOrWhiteSpace(x.SubmitCaptureFieldExpression) ? true : Convert.ToString((((IJavaScriptExecutor)session.driver).ExecuteScript(x.SubmitCaptureFieldExpression))) == "True")).FirstOrDefault();
                    }

                    else if (isActual)
                    {
                        IWait<IWebDriver> waitA = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                        waitA.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));
                        captureEntity = definitionEntities.Where(x => (element.Equals(session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null))  && session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower())).FirstOrDefault();
                    }
                    else
                    {
                        IWait<IWebDriver> waitA = new WebDriverWait(session.driver, TimeSpan.FromSeconds(10));
                        waitA.Until(driverX => ((IJavaScriptExecutor)session.driver).ExecuteScript("return document.readyState").Equals("complete"));

                        captureEntity = definitionEntities.Where(x => (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null) != null && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).TagName == element.TagName && (session.driver.FindElements(By.XPath(x.SubmitCaptureFieldXpath)).Count > 0 ? session.driver.FindElement(By.XPath(x.SubmitCaptureFieldXpath)) : null).Location == element.Location && session.driver.Url.ToLower().Contains("" + x.pmsformid.ToLower())).FirstOrDefault();
                    }

                    if (captureEntity != null)
                    {
                        scannAndCaptureElement = element;
                        MappingViewModel mapping = scanningEntities.Where(x => x.entity_Id == captureEntity.entity_Id).FirstOrDefault();
                        if (mapping != null)
                        {
                            this.blazorUpdateUIAboutPMSEvent(SYNC_MESSAGE_TYPES.WAIT, "IntegrateX is scanning for data to integrate, please wait...");
                            mapping.status = (int)UTIL.Enums.STATUSES.Active;
                            _webData = mapping.DCopy();
                            // Call Data Recieved
                            DataRecievedEventHandler(_webData);
                            // iListner.ElementClickCaptured += Driver_ElementClickCaptured;
                            session.driver = iListner;

                            // this.bWorker.ReportProgress(100);
                        }
                    }

                }
                _log.Info(string.Format("Entity {0} data scan completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED), _webData == null ? "" : _webData.entity_name));
            }
            catch (NoSuchElementException u)
            {
                //TODO
                scannAndCaptureElement = null;
            }
            catch (StaleElementReferenceException u)
            {
                //TODO
                scannAndCaptureElement = null;
            }
            catch (UnhandledAlertException u)
            {
                // session.driver.SwitchTo().Alert().Accept();
                session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
               // _log.Error(ex);// string.Format("Entity {0} data scan completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED), _webData.entity_name));
                if (ex.Message.ToLower().Contains("alert"))
                {
                    session.driver.SwitchTo().Alert().Accept();
                    session.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    // element = session.driver.SwitchTo().ActiveElement();
                }
                _log.Error(ex);
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format(" PMS Desktop started successfully,  hotel- {0} ", GlobalApp.Hotel_Name), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
            }
            finally
            {
                scannAndCaptureElement = null;
            }

        }       
        #endregion
    }
}
