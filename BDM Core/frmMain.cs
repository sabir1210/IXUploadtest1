using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDU.RobotDesktop;
using BDU.RobotWeb;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;
using NLog;
using System.IO;
using WebDriverManager;
using static BDU.UTIL.BDUUtil;
using OpenQA.Selenium;

namespace servr.integratex.ui
{
    public partial class frmMain : Form
    {


        //Regions & labelling created
        // logging library,  NLOG
        #region "Object Initialization & Global variables"
        private Logger _logger = LogManager.GetCurrentClassLogger();
        // private readonly ILogger<frmMain> _logger;
        private BDUService _bduservice = new BDUService();
        List<int> allowedIds = new List<int>() { 1, 2, 3, 4 };
        bool longRunningTaskworking = false;
        public DesktopHandler _desktopHandler;
       
        //  public INodeJSService _nodeJSService;        
        public WebHandler _webHandler;
        private System.Windows.Forms.Form currentForm;
        // System.Threading.Tasks.Task cmsTasker = null;
        private System.Windows.Forms.Timer tmrSyncGuestXData = new System.Windows.Forms.Timer();
        // AiIntegrationTasker ait;
        #endregion
        #region "Constructor & Load"
        public frmMain()
        {
            InitializeComponent();
            //   Global Initiatlization
            this.AllowDrop = true;
            BDU.UTIL.GlobalApp.API_URI = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["apiUrl"]).Trim();
            
                BDU.UTIL.GlobalApp.TEST_RESERVATION_NO = (""+System.Configuration.ConfigurationManager.AppSettings["testreservation"]).Trim();
            BDU.UTIL.GlobalApp.AIService_Interval_Secs = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AIServiceCallIntervalSecs"]);
            BDU.UTIL.GlobalApp.IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS = Convert.ToInt32(string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["IXPMSExecutionTimeOutSecs"])?"60": System.Configuration.ConfigurationManager.AppSettings["IXPMSExecutionTimeOutSecs"]);
            BDU.UTIL.GlobalApp.IX_SQLITE_DATA_PURGE_DAYS_INTERVAL_DAYS = Convert.ToInt32(string.IsNullOrWhiteSpace(System.Configuration.ConfigurationManager.AppSettings["SQLITEDATAPURGEDAYSINTERVALDAYS"]) ? "15" : System.Configuration.ConfigurationManager.AppSettings["SQLITEDATAPURGEDAYSINTERVALDAYS"]);  
            // Load Capabilities
            GlobalApp.AUTOMATION_MODE_CONFIG = (int)Enums.AUTOMATION_MODES.UIAUTOMATION;
            GlobalApp.AUTOMATION_MODE_TYPE_CONFIG = (int)Enums.AUTOMATION_MODE_TYPE.APP;
            GlobalApp.APPLICATION_DRIVERS_CHROME_DRIVER = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["browserDriver"]);
            GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["allowedReceivingEntities"]);
            allowedIds= GlobalApp.ReceivingEntities();
            if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
            {
                GlobalApp.APP_ARGUMENTS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["appArguments"]);
                GlobalApp.PLATFORM_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["platformName"]);
                GlobalApp.DEVICE_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["deviceName"]);
                GlobalApp.START_IN = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["start_in"]);
                GlobalApp.CUSTOM1 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["custom1"]);
                GlobalApp.CUSTOM2 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["custom2"]);
                GlobalApp.PMS_Application_Path_WithName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["app"]);
                GlobalApp.PMS_DESKTOP_PROCESS_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["pmsProcess"]);
                GlobalApp.PMS_USER_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PMSUsr"]);
                GlobalApp.PMS_USER_PWD = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PMSPwd"]);
                GlobalApp.IS_PROCESS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["isProcess"]);
                GlobalApp.RESERVATION_NOT_FOUND_IN_PMS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RESERVATION_NOT_FOUND_IN_PMS"]);
                GlobalApp.STATION_NUMBER = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["station_number"]);
                GlobalApp.Hotel_Code = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["hotel_code"]);
                GlobalApp.UIVInstalPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["UIVInstalPath"]);
                if (!string.IsNullOrWhiteSpace("" + System.Configuration.ConfigurationManager.AppSettings["automation_mode"]))
                {
                    GlobalApp.AUTOMATION_MODE_CONFIG = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["automation_mode"]);
                }
                if (!string.IsNullOrWhiteSpace("" + System.Configuration.ConfigurationManager.AppSettings["IS_PROCESS"]) && GlobalApp.IS_PROCESS=="1")
                {
                    GlobalApp.AUTOMATION_MODE_TYPE_CONFIG = Convert.ToInt32(GlobalApp.IS_PROCESS);
                }
              //  UTIL.GlobalApp.AUTOMATION_MODE_TYPE_CONFIG = (int)UTIL.Enums.AUTOMATION_MODE_TYPE.APP;
                if (GlobalApp.AUTOMATION_MODE_CONFIG == (int)BDU.UTIL.Enums.AUTOMATION_MODES.HYBRID && !string.IsNullOrWhiteSpace(GlobalApp.UIVInstalPath) && !Directory.Exists(GlobalApp.UIVInstalPath) )
                {
                   
                    // var rootFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    // String command = Path.Combine(pathRoot, @"WinAppDriverUiRecorder.exe");//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                    _logger.Error(string.Format("{0} path does not exists! ", GlobalApp.UIVInstalPath));
                    ServrMessageBox.Error(string.Format("Hybrid system installation path is not correct, please verify {0}", ""+GlobalApp.UIVInstalPath));
                    return;
                }
            }// For Desktop Only

            // if (GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
            if (GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
            {
                startPMSSession(true, (Enums.PMS_VERSIONS)Enum.Parse(typeof(Enums.PMS_VERSIONS), GlobalApp.SYSTEM_TYPE)); // (UTIL.Enums.PMS_VERSIONS)GlobalApp.SYSTEM_TYPE);
            }
            // startPMSSession(true, (UTIL.Enums.PMS_VERSIONS)Enum.Parse(typeof(UTIL.Enums.PMS_VERSIONS), GlobalApp.SYSTEM_TYPE)); // (UTIL.Enums.PMS_VERSIONS)GlobalApp.SYSTEM_TYPE);

           
            // this.lblLastSyncedDateShow.Text = UTIL.GlobalApp.SyncTime_CMS.ToString("MM/dd/yy hh:mm:ss");
            /*  Page Events - START*/
            btn_BDUSync.MouseEnter += new EventHandler(btnSync_MouseEnter);
            btn_BDUSync.MouseLeave += new EventHandler(btnSync_MouseLeave);

            //btn_Mapping.MouseEnter += new EventHandler(btnMapping_MouseEnter);
            //btn_Mapping.MouseLeave += new EventHandler(btnMapping_MouseLeave);

            btnPreference.MouseEnter += new EventHandler(btnPreference_MouseEnter);
            btnPreference.MouseLeave += new EventHandler(btnPreference_MouseLeave);

            btn_Logout.MouseEnter += new EventHandler(btnLogOut_MouseEnter);
            btn_Logout.MouseLeave += new EventHandler(btnLogOut_MouseLeave);

            btnDemo.MouseEnter += new EventHandler(btnTutorial_MouseEnter);
            btnDemo.MouseLeave += new EventHandler(btnTutorial_MouseLeave);
            API.ErrorReferences = new List<ErrorViewModel>();

            this.tmrSyncGuestXData.Tick += new System.EventHandler(this.tmrSyncData_Tick);
            tmrSyncGuestXData.Interval = BDU.UTIL.GlobalApp.AIService_Interval_Secs * 3000;
            tmrSyncGuestXData.Enabled = false;
           
        }
        private async void frmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (GlobalApp.Authentication_Done)
                {
                    try
                    {
                        if (API.HotelList == null || API.HotelList.Count <= 0)
                            API.HotelList = await _bduservice.getHotelslist(GlobalApp.Hotel_id, (int)Enums.STATUSES.Active);

                        if (API.HotelList != null && API.HotelList.Count > 0 && (API.PMSVersionsList == null || API.PMSVersionsList.Count <= 0))
                        {
                            Thread.Sleep(500);
                            API.PMSVersionsList = await _bduservice.getPMSVersions(GlobalApp.PMS_Version_No);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Fatal("IX-Failure Detected", ex);
                    }
                }

                //this.btn_BDUSync_Click(this.btn_BDUSync, null);
                if (!GlobalApp.isNew && GlobalApp.Authentication_Done && GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
                {
                    tmrSyncGuestXData.Enabled = true;
                    tmrSyncGuestXData.Start();
                    GlobalApp.SyncBackTime_CMS = GlobalApp.GetLastSyncTimeWithDifference(System.DateTime.UtcNow);

                }
                _logger.Info("IntegrateX started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex) { }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion
        #region "PMS Session Starter"
        public async void startPMSSession(bool start, Enums.PMS_VERSIONS sType)
        {
            try
            {
               // tmrSyncGuestXData.Start();
                switch (sType)
                {
                    case Enums.PMS_VERSIONS.PMS_Desktop:

                        if (start && API.PRESETS.mappings != null)
                        {
                            _desktopHandler = new DesktopHandler();
                            MappingViewModel access = API.PRESETS.mappings.Where(x => x.id == (int)Enums.ENTITIES.ACCESS).FirstOrDefault();
                            List<MappingViewModel> ls = new List<MappingViewModel>();
                            //    access = _bduservice.TestFieldMapping();// Test Only                          
                            ls.Add(access);
                            //********************* For Hybrid************************************//
                            if (GlobalApp.AUTOMATION_MODE_CONFIG == (int)Enums.AUTOMATION_MODES.HYBRID)
                            {
                                try
                                {
                                    //   var varRes = await _nodeJSService.InvokeFromFileAsync<string>(@"D:\Shared\BDUV3.0\BDM Core\bin\Debug\net5.0-windows\Drivers\blazorclix.js", args: new string[] { "localprofile", @"D:\Shared\Temp\AppiumWindows\node\ui.vision.html", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "text.txt" }); //{ "google.com", "DRX" });
                                   // var started = _desktopHandler.StartDesktopSession(ls);
                                    _desktopHandler.blazorUpdateUIAboutPMSEvent += updateReceivedFromPMS;
                                    var started = _desktopHandler.StartDesktopSession(ls,Enums.AUTOMATION_MODES.HYBRID);
                                    _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("Hybrid PMS started, hotel - {0} ", GlobalApp.Hotel_Name), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                                }
                                catch (Exception ex)
                                {
                                    _logger.Error(ex);
                                    _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = string.Format("Hybrid PMS failed,  hotel- {0}, error detail-{1} ", GlobalApp.Hotel_Name, ex.StackTrace.ToString()), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                                    throw new Exception("Unable to run the PMS, might be problem with PMS path, license or connection, Please contact:contact@servrhotels.com for assistance!");
                                }
                            }// Node Based Implementation
                            else
                            {
                                //********************* Hybrid End************************************//
                                // var mappingTemplate = _bduservice.TestFieldMapping();
                                var started = _desktopHandler.StartDesktopSession(ls);
                                _desktopHandler.blazorUpdateUIAboutPMSEvent += updateReceivedFromPMS;
                                //  _desktopHandler.TestRun(access);
                                if (!started)
                                {
                                    _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = string.Format("PMS Desktop is not loaded successfully,  hotel- {0} ", GlobalApp.Hotel_Name), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                                    throw new Exception("Unable to run the PMS, might be problem with PMS path, license or connection, Please contact: contact@servrhotels.com for assistance!");
                                }
                                //  var startedLogin = _desktopHandler.StartSessionAndLogin(access);
                                //_desktopHandler._formData = access;
                                //  UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                //  _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                if (started)
                                {
                                    var startedLogin = _desktopHandler.StartSessionAndLogin(access);
                                    //_desktopHandler._formData = access;
                                    // UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                    // _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                    if (startedLogin)
                                    {
                                        GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                        _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                        //if(startedLogin)
                                        // _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                   
                                    }
                                }// Start If
                            } // Desktop Automation Modes

                            //**********************************Common Desktop Operation******************************************//
                            _desktopHandler.ScanningEntities = API.PRESETS.mappings.Where(x => x.status == (int)Enums.STATUSES.Active && x.entity_type == (int)Enums.ENTITY_TYPES.SYNC).ToList();
                            //  _desktopHandler._formData = access;
                            // _desktopHandler.TestRun(access);
                            _desktopHandler.StartCaptering();
                            _desktopHandler.DataSaved += DataFromPMSRecieved;
                            _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format(" PMS Desktop started successfully,  hotel- {0} ", GlobalApp.Hotel_Name), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                           // _logger.Info(string.Format("IntegrateX desktop bot engine started. at {0}", GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));// servr.integratex.ui.ServrMessageBox.ShowBox("Desktop AI Integration Engine Started.");
                        }
                        else
                        {
                            _desktopHandler.StopDesktopSession();
                        }
                        break;
                    case Enums.PMS_VERSIONS.PMS_Web:
                        if (start && API.PRESETS.mappings != null)
                        {
                            // _webHandler = new WebHandler(BDU.UTIL.GlobalApp.PMS_WEB_Application_URL);
                            MappingViewModel access = API.PRESETS.mappings.Where(x => x.id == (int)Enums.ENTITIES.ACCESS).FirstOrDefault();
                            _webHandler = new WebHandler(access);
                            _webHandler.blazorUpdateUIAboutPMSEvent += updateReceivedFromPMS;
                            //var webMapping = _bduservice.TestFieldMapping(Enums.PMS_VERSIONS.PMS_Web);
                            var started = _webHandler.StartSessionAndLogin(access);
                           // _webHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                            if (started && _webHandler.MPSLoginStatus == Enums.PMS_LOGGIN_STATUS.LOGGED_IN )//|| _webHandler.MPSLoginStatus == Enums.PMS_LOGGIN_STATUS.LOGGED_OUT)
                            {
                                _webHandler.scanningEntities = API.PRESETS.mappings.Where(x => x.status == (int)Enums.STATUSES.Active && x.entity_type == (int)Enums.ENTITY_TYPES.SYNC).ToList();
                                _webHandler.StartSession(_webHandler.scanningEntities);
                                _webHandler.intializeEventActionsData();
                                _webHandler.StartCapturing();
                                _webHandler.blazorScanDataSaveEvent += DataFromPMSRecieved;
                                //  _webHandler.st();
                              //  _logger.Info(string.Format("IntegrateX web bot engine started. at {0}", GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                            }
                            else
                            {
                                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = string.Format(" PMS Desktop is not loaded successfully,  hotel- {0} ", GlobalApp.Hotel_Name), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                                _webHandler.blazorScanDataSaveEvent -= DataFromPMSRecieved;
                                _webHandler.blazorUpdateUIAboutPMSEvent -= updateReceivedFromPMS;
                                ServrMessageBox.warning("PMS login attempt failed, please check PMS access credentials and try again!!");
                            }

                        }
                        else
                        {
                            _webHandler.StopSession();
                           // tmrSyncGuestXData.Stop();
                        }
                        break;

                }//  switch (sType) {

            }
            catch (Exception ex)
            {
                ServrMessageBox.Error(string.Format("Unable to run the PMS, might be problem with PMS path, license or connection, Please contact: contact@servrhotels.com for assistance!"), "Login");
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = string.Format("PMS loading is failed,  hotel- {0}, error - {1} ", GlobalApp.Hotel_Name, ex.Message + "-"+ ex.StackTrace.ToString()), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                // _logger.Fatal("IX-Failure Detected", ex);
                Cursor.Current = Cursors.Default;
                _logger.Error(ex);
                if (GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
                {
                    BDULoginForm login = new BDULoginForm();
                    Application.Exit();
                }
            }
        }

        #endregion
        #region "Events"
        void btnSync_MouseEnter(object sender, EventArgs e)
        {
            //this.btn_BDUSync.BackColor = Color.FromArgb(231, 231, 231);
            this.btn_BDUSync.BackColor = Color.FromArgb(244, 247, 252);

        }
        void btnSync_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.btn_BDUSync.BackColor = btn_BDUSync.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;

            }
            catch (Exception ex)
            {
               // _logger.Error(ex);

            }
        }
        //void btnMapping_MouseEnter(object sender, EventArgs e)
        //{
        //    this.btn_Mapping.BackColor = Color.FromArgb(244, 247, 252);
        //}
        //void btnMapping_MouseLeave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.btn_Mapping.BackColor = btn_Mapping.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;

        //    }
        //    catch (Exception ex)
        //    {
        //       // _logger.Error(ex);

        //    }
        //}

        void btnPreference_MouseEnter(object sender, EventArgs e)
        {
            this.btnPreference.BackColor = Color.FromArgb(244, 247, 252);
        }
        void btnPreference_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.btnPreference.BackColor = btnPreference.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;
            }
            catch (Exception ex)
            {
               // _logger.Error(ex);

            }
        }
        void btnTutorial_MouseEnter(object sender, EventArgs e)
        {
            this.btnDemo.BackColor = Color.FromArgb(244, 247, 252);
        }
        void btnTutorial_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.btnDemo.BackColor = btnDemo.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;
            }
            catch (Exception ex)
            {
               // _logger.Error(ex);

            }
        }

        void btnLogOut_MouseEnter(object sender, EventArgs e)
        {
            this.btn_Logout.BackColor = Color.FromArgb(244, 247, 252);
        }
        void btnLogOut_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.btn_Logout.BackColor = btn_Logout.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;
            }
            catch (Exception ex)
            {
               // _logger.Error(ex);
            }
        }


        #endregion
        // Form Open & Closing handler WebElement username=driver.findElement(By.xpath("//TableLayout[@index='1']/TableRow[@index='0']/EditText[@index='0']"));
        #region "Forms navigations hanlder"
        private void switchForm(Form frm)
        {

            if (currentForm == null)
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.ControlBox = false;
                frm.MdiParent = this;
                frm.FormClosed += (sender, e) => { currentForm = frm; };
                frm.Show();
                currentForm = frm;
            }
            else if (currentForm.Name != frm.Name && currentForm != null)
            {
                currentForm.Hide();
                frm.WindowState = FormWindowState.Maximized;
                frm.ControlBox = false;
                frm.MdiParent = this;
                frm.FormClosed += (sender, e) => { currentForm = frm; };
                frm.Show();
                currentForm = frm;
            }
            else if (currentForm.Name.ToLower() == frm.Name.ToLower())
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.ControlBox = false;
                frm.MdiParent = this;
                frm.FormClosed += (sender, e) => { currentForm = frm; };
                frm.Show();
                currentForm = frm;
            }
        }
        private void DataFromPMSRecieved(MappingViewModel mapping)
        {
            try
            {
                bool bookingFound = false;
                //  Shortcut keys
                if (mapping != null)
                {
                    if (_webHandler != null && _webHandler.Undo > 0) mapping.undo = 1;
                    if (_desktopHandler != null && _desktopHandler.Undo > 0) mapping.undo = 1;

                    
                    // UNDO Code inducted here
                    if (mapping.undo <= 0 && mapping != null && mapping.entity_Id == (int)BDU.UTIL.Enums.ENTITIES.CHECKIN || mapping.entity_Id == (int)BDU.UTIL.Enums.ENTITIES.CHECKOUT)
                    {
                        foreach (FormViewModel frm in mapping.forms.Where(x => x.Status == (int)Enums.APPROVAL_STATUS.NEW_ISSUED))
                        {
                            EntityFieldViewModel fld = frm.fields.Where(x => x.status == (int)Enums.APPROVAL_STATUS.NEW_ISSUED && x.field_desc.ToLower().Contains(BDUConstants.UNDU_FIELD_NAME.ToLower())).FirstOrDefault();
                            if (fld != null && !string.IsNullOrWhiteSpace(fld.value))
                            {
                                if (fld.value == "1")
                                {
                                    mapping.undo = 1;
                                }
                            }
                            if (mapping.undo >= 1) break;
                        }
                    }//if (mapping.entity_Id == (int)BDU.UTIL.Enums.ENTITIES.CHECKIN || mapping.entity_Id == (int)BDU.UTIL.Enums.ENTITIES.CHECKOUT)
                }// If Mapping found
                List<SQLiteMappingViewModel> SQlData = SQLiteDbManager.loadSQLiteData(dtFrom: GlobalApp.CurrentDateTime.AddDays(-1), dtTo: GlobalApp.CurrentDateTime, currentEntity: 0, syncstatus: (int)BDU.UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED);
                if (mapping != null && SQlData != null && mapping.status == (int)Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(mapping.reference))
                {
                    mapping.entity_name = StringValueOf((Enums.ENTITIES)mapping.entity_Id);
                    if (mapping != null && SQlData != null && mapping.status == (int)Enums.RESERVATION_STATUS.ACTIVE && SQlData.Where(x => x.reference == mapping.reference && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode && x.entity_id== mapping.entity_Id).FirstOrDefault() == null)
                    {
                        mapping.mode = (int)Enums.SYNC_MODE.TO_CMS;
                        mapping.id = mapping.entity_Id;
                        // mapping.uid = new Random().Next(10000, 90000);
                        mapping.createdAt = GlobalApp.CurrentLocalDateTime;                       
                        List<MappingViewModel> mList = new List<MappingViewModel>();
                        mList.Add(mapping);
                        SQLiteDbManager.InsertSQLiteData(mList);
                        mList = null;
                        // API.AIData.Add(mapping);
                        bookingFound = true;
                    }
                    else if (mapping != null && SQlData != null && mapping.status == (int)Enums.STATUSES.Active && SQlData.Where(x => x.reference == mapping.reference && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode && x.entity_id == mapping.entity_Id).FirstOrDefault() != null)
                    {
                        SQLiteMappingViewModel sqlMappingFound = SQlData.Where(x => x.reference == mapping.reference && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode && x.entity_id == mapping.entity_Id).FirstOrDefault();
                        mapping.createdAt = BDU.UTIL.GlobalApp.CurrentLocalDateTime;
                        mapping.mode = (int)Enums.SYNC_MODE.TO_CMS;
                        mapping.uid = sqlMappingFound.id;   // This is Unique key in reservations SQLite                    
                        mapping.id = mapping.entity_Id;
                  
                                SQLiteDbManager.InsertSQLiteData(mapping, true);
                        _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("Reservation update reference# {0} received", mapping.reference), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                        bookingFound = true;                       


                    }
                }
                GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                if (this.InvokeRequired)
                {
                    Form frm = (Form)this.ActiveMdiChild;
                    if (mapping != null && mapping.status != (int)Enums.STATUSES.Active)
                    {
                        this.Invoke((MethodInvoker)delegate {

                            //Form frm = (Form)this.ActiveMdiChild;
                            if (frm != null && frm.Name.ToLower() == "BDUSyncForm")
                            {
                                BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                                bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("PMS scan failed, {0}", mapping.xpath.ToString()));
                            }
                        });
                        if (bookingFound && frm.Name.ToLower() != "BDUSyncForm") {
                            GlobalApp.IX_LAST_MESSAGE = string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name);
                            System.Threading.Thread.Sleep(500);
                            WindowNotifications.windowNotification(message: string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name), icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 1);
                        }
                    }
                    else if (bookingFound)
                    {
                        this.Invoke((MethodInvoker)delegate {

                            //Form frm = (Form)this.ActiveMdiChild;
                            if (frm != null && frm.Name == "BDUSyncForm")
                            {
                                BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                                bdufrm.fillDataGrid();
                                // bdufrm.btn_SyncAll_Click(bdufrm.btn_SyncAll, null);
                                bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name));
                            }
                        });
                        if (bookingFound && frm.Name.ToLower() != "BDUSyncForm")
                        {
                            GlobalApp.IX_LAST_MESSAGE = string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name);
                            System.Threading.Thread.Sleep(200);
                            WindowNotifications.windowNotification(message: string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name), icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 1);
                        }
                    }

                }//  if (this.InvokeRequired)
                else if(mapping != null && bookingFound )
                {
                    WindowNotifications.windowNotification(message: string.Format("Reservation# {0}, {1} data recieved..", mapping.reference, mapping.entity_name), icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 3);                  
                }
                SQlData = null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                WindowNotifications.windowNotification(message: string.Format("Reservation# {0}, {1} data recieved failed..", mapping.reference, mapping.entity_name), icon: servr.integratex.ui.Properties.Resources.error, notifyType: 1);
            }
            if (_webHandler != null) _webHandler.Undo = 0;
            if (_desktopHandler != null) _desktopHandler.Undo = 0;
        }
        private void updateReceivedFromPMS(Enums.SYNC_MESSAGE_TYPES mType, string updateMsg)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(updateMsg))
                    {
                        this.Invoke((MethodInvoker)delegate {

                            Form frm = (Form)this.ActiveMdiChild;
                            if (frm != null && frm.Name == "BDUSyncForm")
                            {
                                BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                                bdufrm.udpdateBuilitin(mType, updateMsg);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    WindowNotifications.windowNotification(message: string.Format("Data retreival from PMS failed,Detail- {0}!",ex.Message),icon: servr.integratex.ui.Properties.Resources.error, notifyType:3);
                    _logger.Fatal("Unfortunately IX-Failure detected", ex);
                   // _logger.Error(ex);
                }
            }
        }

        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        private void btn_BDUSync_Click(object sender, EventArgs e)
        {
            BDUSyncForm frm = new BDUSyncForm(_desktopHandler, _webHandler);
            frm.frmMain = this;
            if (this.currentForm == null)
                this.currentForm = frm;
            this.ResetDefaultSelection(sender as Button);

            switchForm(frm);
            //pnl_LastSyncTime.Visible = true;
            pnl_SyncControl.Visible = true;
        }
        private void btn_Mapping_Click(object sender, EventArgs e)
        {
            if (BDU.UTIL.GlobalApp.login_role == BDU.UTIL.Enums.USERROLES.PMS_Staff)
            {
                bool loginStatus = false;
                Login login = new Login();
                login.ShowDialog();
                if (login.loginStatus)
                {
                    WindowNotifications.windowNotification(message: "Login success!", icon: servr.integratex.ui.Properties.Resources.complete_Blue);
                    this.ResetDefaultSelection(sender as Button);
                    BDUPreference_Mapping frm = new BDUPreference_Mapping();
                    frm._desktopHandler = _desktopHandler;
                    frm._WebHandler = _webHandler;
                    switchForm(frm);
                    pnl_SyncControl.Visible = false;
                }// login status failed
                else
                {
                    WindowNotifications.windowNotification(message: "Login failed!", icon: servr.integratex.ui.Properties.Resources.error);
                }
            }
            else if (BDU.UTIL.GlobalApp.login_role == BDU.UTIL.Enums.USERROLES.Servr_Staff)
            {
                this.ResetDefaultSelection(sender as Button);
                BDUPreference_Mapping frm = new BDUPreference_Mapping();
                frm._desktopHandler = _desktopHandler;
                frm._WebHandler = _webHandler;
                switchForm(frm);
                pnl_SyncControl.Visible = false;

            }
        }
        private void btnPreference_Click(object sender, EventArgs e)
        {

            if (BDU.UTIL.GlobalApp.login_role == BDU.UTIL.Enums.USERROLES.PMS_Staff)
            {
                bool loginStatus = false;
                Login login = new Login();
                login.ShowDialog();
                if (login.loginStatus)
                {
                    WindowNotifications.windowNotification(message: "Login success!", icon: servr.integratex.ui.Properties.Resources.complete_Blue);
                    this.ResetDefaultSelection(sender as Button);
                    BDUPreference_Mapping frm = new BDUPreference_Mapping();                   
                    frm._desktopHandler = _desktopHandler;
                    frm._WebHandler = _webHandler;
                    switchForm(frm);
                    pnl_SyncControl.Visible = false;
                }// login status failed
                else {
                    WindowNotifications.windowNotification(message: "Login failed!", icon: servr.integratex.ui.Properties.Resources.error);
                }
            }
            else if (BDU.UTIL.GlobalApp.login_role == BDU.UTIL.Enums.USERROLES.Servr_Staff) {
                this.ResetDefaultSelection(sender as Button);
                BDUPreference_Mapping frm = new BDUPreference_Mapping();
                frm._desktopHandler = _desktopHandler;
                frm._WebHandler = _webHandler;
                switchForm(frm);
                pnl_SyncControl.Visible = false;

            }
            //Login.LoginConfirm();
            //this.ResetDefaultSelection(sender as Button);
            //if ((1 == 2))
            //{
            //    this.ResetDefaultSelection(sender as Button);
            //    BDUMapping_PreferenceForm frm = new BDUMapping_PreferenceForm();
            //    switchForm(frm);
            //    pnl_SyncControl.Visible = false;
            //}
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            // Button btn = sender as Button;
            this.ResetDefaultSelection(sender as Button);
            Enums.MESSAGERESPONSETYPES res = ServrMessageBox.Confirm("Are you sure, want to logout?", "Confirmation!");

            if (res == BDU.UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                _bduservice.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("Logged out by user {0} received", GlobalApp.UserName), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.UserName } });
                _logger.Info(string.Format("Logged out from IntegrateX, at {0}", GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));

                try
                {
                    if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString() && _webHandler != null)
                        _webHandler.StopSession();
                    else if (_desktopHandler != null && (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString()))
                        _desktopHandler.StopDesktopSession();
                }
                catch (Exception ex) { _logger.Error(ex); }
                WindowNotifications.windowNotification(message:"You are logged out from IntegrateX!",icon: servr.integratex.ui.Properties.Resources.error);
                Application.Exit();
            }
            //pnl_LastSyncTime.Visible = false;
            pnl_SyncControl.Visible = false;
        }
        #endregion
        #region "load & Close Events"
        private async void frmMain_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MainFormRound();
            this.btn_BDUSync_Click(this.btn_BDUSync, null);
            currentForm = new BDUSyncForm(_desktopHandler, _webHandler);
           // AiIntegrationTasker();
           
            
            // PlaceLowerRight();
            //IEnumerable<HotelViewModel> hlist = await _bduservice.getHotelslist(0, (int)UTIL.Enums.STATUSES.Active);
            SyncControlPanel();
            //API.PRESETS = new HotelViewModel();           
           
            API.AIData = new List<MappingViewModel>();

            foreach (Control control in this.Controls)
            {
                // #2
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    // #3
                    client.BackColor = Color.White;
                    // 4#
                    break;
                }
            }
            //this.Invoke((MethodInvoker)delegate {

            //    Form frm = (Form)this.ActiveMdiChild;
            //    if (frm != null && frm.Name == "BDUSyncForm")
            //    {
            //        BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
            //        bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("IntegrateX started successfuly, at - {0}", System.DateTime.Now.ToString("HH:mm:ss")));
            //    }
            //});

            //if (GlobalApp.Authentication_Done)
            //{
            //    try
            //    {
            //        if (API.HotelList == null || API.HotelList.Count <= 0)
            //            API.HotelList = await _bduservice.getHotelslist(GlobalApp.Hotel_id, (int)Enums.STATUSES.Active);

            //        if (API.HotelList != null && API.HotelList.Count > 0 && (API.PMSVersionsList == null || API.PMSVersionsList.Count <= 0))
            //        {
            //            Thread.Sleep(500);
            //            API.PMSVersionsList = await _bduservice.getPMSVersions();
            //        }
            //    }
            //    catch (Exception ex) {
            //        _logger.Fatal("IX-Failure Detected", ex);
            //    }
            //}
            //_logger.Info("IntegrateX started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            //this.btn_BDUSync_Click(this.btn_BDUSync, null);
            //if (GlobalApp.Authentication_Done && GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
            //{
            //    tmrSyncGuestXData.Enabled = true;
            //    tmrSyncGuestXData.Start();

            //}
           
        }
        private void MainFormRound()
        {
            int borderRadius = 100;
            RectangleF Rect = new RectangleF(0, 0, this.Width - 10, this.Height - 10);
            GraphicsPath GraphPath = GraphicsExtension.GetRoundPath(Rect, borderRadius);
            this.Region = new Region(GraphPath);

        }
        //public static void DrawBorder(System.Drawing.Graphics graphics, System.Drawing.Rectangle bounds, System.Drawing.Color leftColor, int leftWidth, System.Windows.Forms.ButtonBorderStyle leftStyle, System.Drawing.Color topColor, int topWidth, System.Windows.Forms.ButtonBorderStyle topStyle, System.Drawing.Color rightColor, int rightWidth, System.Windows.Forms.ButtonBorderStyle rightStyle, System.Drawing.Color bottomColor, int bottomWidth, System.Windows.Forms.ButtonBorderStyle bottomStyle);
        protected override void OnLoad(EventArgs e)
        {
            var mdiclient = this.Controls.OfType<MdiClient>().Single();
            this.SuspendLayout();
            mdiclient.SuspendLayout();
            var hdiff = mdiclient.Size.Width - mdiclient.ClientSize.Width;
            var vdiff = mdiclient.Size.Height - mdiclient.ClientSize.Height;
            var size = new Size(mdiclient.Width + hdiff, mdiclient.Height + vdiff);
            var location = new Point(mdiclient.Left - (hdiff / 2), mdiclient.Top - (vdiff / 2));
            mdiclient.Dock = DockStyle.None;
            mdiclient.Size = size;
            mdiclient.Location = location;
            mdiclient.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            mdiclient.ResumeLayout(true);
            this.ResumeLayout(true);
            base.OnLoad(e);
        }      

        private void btn_MainMenuClose_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = ServrMessageBox.Confirm("Are you sure, want to logout?", "Confirmation!");

            if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                WindowNotifications.windowNotification(message:"You are logged out from IntegrateX!",title:"Servr IntegrateX");
                Application.Exit();
            }
        }
        #endregion
        #region "BDU Service Links"
        private void ResetDefaultSelection(Button pBtn)
        {
            // this.SelectedForm = pBtn.Name;
            if (pBtn.Name == this.btn_BDUSync.Name && btn_BDUSync.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btn_BDUSync.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btn_BDUSync.BackColor = Color.White;
            }

            if (pBtn.Name == this.btnPreference.Name && btnPreference.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btnPreference.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btnPreference.BackColor = Color.White;
            }          

            //if (pBtn.Name == this.btnPreference.Name && btnPreference.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            //{
            //    this.btnPreference.BackColor = Color.FromArgb(244, 247, 252);
            //}
            //else
            //{
            //    this.btnPreference.BackColor = Color.White;
            //}

            if (pBtn.Name == this.btnDemo.Name && btnDemo.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btnDemo.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btnDemo.BackColor = Color.White;
            }

            if (pBtn.Name == this.btn_Logout.Name && btn_Logout.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btn_Logout.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btn_Logout.BackColor = Color.White;
            }
        }
        private void btnSpa_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(lblSpa_Val.Text.Trim()) > 0)
            {
                GlobalApp.OpenBrowser("https://guestx.servrhotels.com/spas/1");
            }
            else
            {
                btnSpa.Enabled = false;
            }
        }

        private void btn_Concierge_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(lblConcierge_Val.Text.Trim()) > 0)
            {
                GlobalApp.OpenBrowser("https://guestx.servrhotels.com/concierges");
            }
            else
            {
                btn_Concierge.Enabled = false;
            }
        }

        private void btn_Experiences_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt16(lbl_Experiences_Val.Text.Trim()) > 0)
            {
                GlobalApp.OpenBrowser("https://guestx.servrhotels.com/experience");
            }
            else
            {
                btn_Experiences.Enabled = false;
                //ServrMessageBox.Error("The following Input is required");
            }
        }
        private void btn_Restaurans_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt16(lbl_RestaurensVal.Text.Trim()) > 0)
            {
                GlobalApp.OpenBrowser("https://guestx.servrhotels.com/restaurants/1");
            }
            else
            {
                btn_Restaurans.Enabled = false;
                //ServrMessageBox.Error("The following Input is required");
            }
        }
        private void SyncControlPanel()
        {
            if (Convert.ToInt16(lblSpa_Val.Text.Trim()) > 0)
            {
                btnSpa.Enabled = true;
            }
            else
            {
                btnSpa.Enabled = false;
            }
            if (Convert.ToInt16(lblConcierge_Val.Text.Trim()) > 0)
            {
                btn_Concierge.Enabled = true;
            }
            else
            {
                btn_Concierge.Enabled = false;
            }
            if (Convert.ToInt16(lbl_Experiences_Val.Text.Trim()) > 0)
            {
                btn_Experiences.Enabled = true;
            }
            else
            {
                btn_Experiences.Enabled = false;
            }
            if (Convert.ToInt16(lbl_RestaurensVal.Text.Trim()) > 0)
            {
                btn_Restaurans.Enabled = true;
            }
            else
            {
                btn_Restaurans.Enabled = false;
            }
        }
        private void btnDemo_Click(object sender, EventArgs e)
        {
            //UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com/forgot-password");
            GlobalApp.OpenBrowser("https://guestx.servrhotels.com//docs/IntegrateX/IntegrateXDemo.mp4");
        }
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            //this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        #region "Tasker"      
        private async void tmrSyncData_Tick(object sender, EventArgs e)
        {
            if (!GlobalApp.isNew && GlobalApp.Authentication_Done && GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
            {
                try
            {
                    
                   // if ((GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.DEFAULT) )
                   // {
                        tmrSyncGuestXData.Stop();
                        tmrSyncGuestXData.Enabled = false;
                        await CMSLongRunningTask();
                        tmrSyncGuestXData.Interval = BDU.UTIL.GlobalApp.AIService_Interval_Secs * 1000;
                        tmrSyncGuestXData.Enabled = true;
                        tmrSyncGuestXData.Start();
                   // }
                
            }
            catch (Exception ex) {
                    tmrSyncGuestXData.Enabled = true;
                    tmrSyncGuestXData.Start();
                }
            }
        }
        private async Task CMSLongRunningTask()
        {
            bool newDataFound = false;
            bool newStatsFound = false;
            try
            {
               // _logger.Error("Long Running Timer Call Started");
                if (GlobalApp.Authentication_Done && !GlobalApp.isNew && !longRunningTaskworking)
                {
                    // UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                    //  List<MappingViewModel> retls =  _bduservice.TestFieldMapping( Enums.PMS_VERSIONS.PMS_Desktop);// (GlobalApp.Hotel_id, UTIL.GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS), (int)UTIL.Enums.STATUSES.InActive).AddHours(-30;
                    //List<MappingViewModel> retls = await _bduservice.getCMSDataService(GlobalApp.Hotel_id, GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncBackTime_CMS).AddSeconds(0));
                    List<MappingViewModel> retls = await _bduservice.getCMSDataService(GlobalApp.Hotel_id, GlobalApp.SyncBackTime_CMS.AddSeconds(0));
                    GlobalApp.SyncBackTime_CMS = GlobalApp.GetLastSyncTimeWithDifference(System.DateTime.UtcNow);
                   // th.lblLastSyncedDateShow.Text = GlobalApp.SyncTime_CMS.ToString(GlobalApp.date_time_format);
                    longRunningTaskworking = true;
                    // GlobalApp.las = UTIL.GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS);
                    // if (retls != null && API.AIData != null && retls.Any())
                    if (retls != null && retls.Any())
                    {
                        List<SQLiteMappingViewModel> SQlData = SQLiteDbManager.loadSQLiteData(dtFrom: GlobalApp.CurrentDateTime.AddDays(-1), dtTo: GlobalApp.CurrentDateTime, currentEntity: 0, syncstatus: (int)BDU.UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED);
                        foreach (MappingViewModel mvModl in retls)
                        {
                            if (retls.Count > 4 && allowedIds.Contains(mvModl.entity_Id))
                            {
                                if (mvModl.entity_type == (int)Enums.ENTITY_TYPES.SYNC && SQlData.Where(x => x.reference == mvModl.reference && mvModl.entity_Id == x.entity_id && x.mode == mvModl.mode && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)Enums.ENTITY_TYPES.SYNC).FirstOrDefault() == null)
                                {
                                    //mvModl.createdAt = GlobalApp.CurrentLocalDateTime;
                                    //API.AIData.Add(mvModl);
                                    SQLiteDbManager.InsertSQLiteData(mvModl);
                                    newDataFound = true;
                                }
                                else if (mvModl.entity_type == (int)Enums.ENTITY_TYPES.SYNC && mvModl.status == (int)Enums.RESERVATION_STATUS.ACTIVE)// && SQlData.Where(x => x.reference == mvModl.reference && x.mode == mvModl.mode && mvModl.entity_Id == x.entity_id && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)Enums.ENTITY_TYPES.SYNC).FirstOrDefault() != null)
                                {
                                    SQLiteMappingViewModel syncEnty = SQlData.FirstOrDefault(s => s.reference == mvModl.reference && s.mode == mvModl.mode && mvModl.entity_Id == s.entity_id && s.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE && s.entity_type == (int)Enums.ENTITY_TYPES.SYNC);
                                    if (syncEnty != null)
                                    {
                                        mvModl.createdAt = syncEnty.transactiondate;
                                        mvModl.mode = syncEnty.mode;
                                        mvModl.uid = syncEnty.id;
                                        API.AIData.Add(mvModl);
                                        // Delete & Insert new
                                        SQLiteDbManager.InsertSQLiteData(mvModl, true);
                                        newDataFound = true;
                                    }
                                }
                            }//if (GlobalApp.ReceivingEntities().Contains(mvModl.entity_Id))
                            if (mvModl.entity_type == (int)Enums.ENTITY_TYPES.STATS && !API.AIData.Where(x => x.entity_Id == mvModl.entity_Id && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)Enums.ENTITY_TYPES.STATS).Any())
                            {
                                API.AIData.Add(mvModl);
                                newStatsFound = true;
                            }
                            else if (mvModl.entity_type == (int)Enums.ENTITY_TYPES.STATS && API.AIData.Where(x => x.entity_Id == mvModl.entity_Id && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)Enums.ENTITY_TYPES.STATS).Any())
                            {
                                MappingViewModel statsEnty = API.AIData.FirstOrDefault(x => x.entity_Id == mvModl.entity_Id && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)Enums.ENTITY_TYPES.STATS);
                                if (statsEnty != null)
                                    API.AIData.Remove(statsEnty);
                                API.AIData.Add(mvModl);
                                newStatsFound = true;
                            }
                        }
                        SQlData = null;
                        if (newDataFound || newStatsFound)
                        {
                            GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                            this.Invoke((MethodInvoker)async delegate {

                                Form frm = (Form)this.ActiveMdiChild;
                                if (frm != null && frm.Name == "BDUSyncForm")
                                {
                                    BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                                    if (newStatsFound && newDataFound)
                                        await bdufrm.fillDataGrid();
                                    if (newStatsFound)
                                        await bdufrm.fillStatsControls();
                                    // bdufrm.Refresh();
                                }
                            });
                        }// if (newDataFound)
                    }
                    GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;

                    // await Task.Delay(30000);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally {
                longRunningTaskworking = false;
            }
        }
        #endregion        
    }
}

