using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDU.RobotDesktop;
using BDU.RobotWeb;
using BDU.Services;
using BDU.UI.Extensions;
using BDU.UI.Tasks;
using BDU.UTIL;
using BDU.ViewModels;
using NLog;
using Servr.UI;

namespace BDU.UI
{
    public partial class frmMain : Form
    {
        //Int32 lblSpa_Val;
        // logging library,  NLOG
        #region "Object Initialization & Global variables"
        private Logger _logger = LogManager.GetCurrentClassLogger();
        // private readonly ILogger<frmMain> _logger;
        private BDUService _bduservice = new BDUService();
        public DesktopHandler _desktopHandler;
        public WebHandler _webHandler;
        private System.Windows.Forms.Form currentForm;

        // AiIntegrationTasker ait;
        #endregion
        #region "Constructor & Load"
        public frmMain()
        {
            InitializeComponent();

            //   Global Initiatlization
            BDU.UTIL.GlobalApp.API_URI = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];
            BDU.UTIL.GlobalApp.AIService_Interval_Secs = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["AIServiceCallIntervalSecs"]);
            // Load Capabilities
            if (GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
            {
                BDU.UTIL.GlobalApp.APP_ARGUMENTS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["appArguments"]);
                BDU.UTIL.GlobalApp.PLATFORM_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["platformName"]);
                BDU.UTIL.GlobalApp.DEVICE_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["deviceName"]);
                BDU.UTIL.GlobalApp.START_IN = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["start_in"]);
                BDU.UTIL.GlobalApp.CUSTOM1 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["custom1"]);
                BDU.UTIL.GlobalApp.CUSTOM2 = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["custom2"]);
                BDU.UTIL.GlobalApp.PMS_Application_Path_WithName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["app"]);
                UTIL.GlobalApp.PMS_DESKTOP_PROCESS_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["pmsProcess"]);
                BDU.UTIL.GlobalApp.PMS_USER_NAME = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PMSUsr"]);
                BDU.UTIL.GlobalApp.PMS_USER_PWD = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PMSPwd"]);
                UTIL.GlobalApp.IS_PROCESS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["isProcess"]);
                UTIL.GlobalApp.RESERVATION_NOT_FOUND_IN_PMS = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RESERVATION_NOT_FOUND_IN_PMS"]);
            }

            // if (GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
            if (GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
            {
                startPMSSession(true, (UTIL.Enums.PMS_VERSIONS)Enum.Parse(typeof(UTIL.Enums.PMS_VERSIONS), GlobalApp.SYSTEM_TYPE)); // (UTIL.Enums.PMS_VERSIONS)GlobalApp.SYSTEM_TYPE);
            }
            // startPMSSession(true, (UTIL.Enums.PMS_VERSIONS)Enum.Parse(typeof(UTIL.Enums.PMS_VERSIONS), GlobalApp.SYSTEM_TYPE)); // (UTIL.Enums.PMS_VERSIONS)GlobalApp.SYSTEM_TYPE);

            API.AIData = new List<MappingViewModel>();
            // this.lblLastSyncedDateShow.Text = UTIL.GlobalApp.SyncTime_CMS.ToString("MM/dd/yy hh:mm:ss");
            /*  Page Events - START*/
            btn_BDUSync.MouseEnter += new EventHandler(btnSync_MouseEnter);
            btn_BDUSync.MouseLeave += new EventHandler(btnSync_MouseLeave);

            btn_Mapping.MouseEnter += new EventHandler(btnMapping_MouseEnter);
            btn_Mapping.MouseLeave += new EventHandler(btnMapping_MouseLeave);

            btnPreference.MouseEnter += new EventHandler(btnPreference_MouseEnter);
            btnPreference.MouseLeave += new EventHandler(btnPreference_MouseLeave);

            btn_Logout.MouseEnter += new EventHandler(btnLogOut_MouseEnter);
            btn_Logout.MouseLeave += new EventHandler(btnLogOut_MouseLeave);

            btnDemo.MouseEnter += new EventHandler(btnTutorial_MouseEnter);
            btnDemo.MouseLeave += new EventHandler(btnTutorial_MouseLeave);

            //  _logger = logger;
            /*  Page Events - END*/
            _logger.Info("IntegrateX started, at " + System.DateTime.Now.ToLocalTime().ToString());
            /*  LOAD Global Configuration Data - START*/


        }
        #endregion
        #region "PMS Session Starter"
        public void startPMSSession(bool start, UTIL.Enums.PMS_VERSIONS sType)
        {
            try
            {
                switch (sType)
                {
                    case Enums.PMS_VERSIONS.PMS_Desktop:

                        if (start && API.PRESETS.mappings != null)
                        {
                            _desktopHandler = new DesktopHandler();
                            // var mappingTemplate = _bduservice.TestFieldMapping();
                            MappingViewModel access = API.PRESETS.mappings.Where(x => x.id == (int)UTIL.Enums.ENTITIES.ACCESS).FirstOrDefault();
                            List<MappingViewModel> ls = new List<MappingViewModel>();
                            //    access = _bduservice.TestFieldMapping();// Test Only                          
                            ls.Add(access);
                            var started = _desktopHandler.StartDesktopSession(ls);
                            _desktopHandler.blazorUpdateUIAboutPMSEvent += updateReceivedFromPMS;
                            //  _desktopHandler.TestRun(access);

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
                                    UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                    _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                    //if(startedLogin)
                                    // _desktopHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                                    _desktopHandler.ScanningEntities = API.PRESETS.mappings.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC).ToList();
                                    //  _desktopHandler._formData = access;
                                    // _desktopHandler.TestRun(access);
                                    _desktopHandler.StartCaptering();
                                    _desktopHandler.DataSaved += DataFromPMSRecieved;
                                    _logger.Info("IntegrateX desktop bot engine started.");// Servr.UI.ServrMessageBox.ShowBox("Desktop AI Integration Engine Started.");
                                }
                            }// Start If
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
                            MappingViewModel access = API.PRESETS.mappings.Where(x => x.id == (int)UTIL.Enums.ENTITIES.ACCESS).FirstOrDefault();
                            _webHandler = new WebHandler(access);
                            _webHandler.blazorUpdateUIAboutPMSEvent += updateReceivedFromPMS;
                            //var webMapping = _bduservice.TestFieldMapping(Enums.PMS_VERSIONS.PMS_Web);
                            var started = _webHandler.StartSessionAndLogin(access);
                            _webHandler.MPSLoginStatus = Enums.PMS_LOGGIN_STATUS.LOGGED_IN;
                            if (started && _webHandler.MPSLoginStatus == Enums.PMS_LOGGIN_STATUS.LOGGED_IN || _webHandler.MPSLoginStatus == Enums.PMS_LOGGIN_STATUS.LOGGED_OUT)
                            {
                                _webHandler.scanningEntities = API.PRESETS.mappings.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC).ToList();
                                _webHandler.StartSession(_webHandler.scanningEntities);
                                _webHandler.intializeEventActionsData();
                                _webHandler.StartCapturing();
                                _webHandler.blazorScanDataSaveEvent += DataFromPMSRecieved;
                                //  _webHandler.st();
                                _logger.Info("IntegrateX web bot engine started.");
                            }
                            else
                            {
                                _webHandler.blazorScanDataSaveEvent -= DataFromPMSRecieved;
                                _webHandler.blazorUpdateUIAboutPMSEvent -= updateReceivedFromPMS;
                                ServrMessageBox.Error("PMS login failed", "Login");
                            }

                        }
                        else
                        {
                            _webHandler.StopSession();
                        }
                        break;

                }//  switch (sType) {

            }
            catch (Exception ex)
            {
                ServrMessageBox.Error(string.Format("PMS start failed, Error -{0}", ex.Message), "Login");
                _logger.Error(ex);
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
                _logger.Error(ex);

            }
        }
        void btnMapping_MouseEnter(object sender, EventArgs e)
        {
            this.btn_Mapping.BackColor = Color.FromArgb(244, 247, 252);
        }
        void btnMapping_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                this.btn_Mapping.BackColor = btn_Mapping.Tag.ToString().ToLower() == this.currentForm.Name.ToLower() ? Color.FromArgb(244, 247, 252) : Color.White;

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }

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
                _logger.Error(ex);

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
                _logger.Error(ex);

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
                _logger.Error(ex);
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
            bool bookingFound = false;
            if (mapping != null && API.AIData != null && mapping.status == (int)UTIL.Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(mapping.reference))
            {
                if (mapping != null && API.AIData != null && mapping.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && API.AIData.Where(x => x.reference == mapping.reference && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode).FirstOrDefault() == null)
                {
                    mapping.mode = (int)UTIL.Enums.SYNC_MODE.TO_CMS;
                    mapping.id = mapping.entity_Id;
                    mapping.entity_name = StringValueOf((UTIL.Enums.ENTITIES)mapping.entity_Id);
                    mapping.createdAt = UTIL.GlobalApp.CurrentLocalDateTime;
                    //  MappingViewModel inputEntity = mapping;
                    API.AIData.Add(mapping);
                    bookingFound = true;
                }
                else if (mapping != null && API.AIData != null && mapping.status == (int)UTIL.Enums.STATUSES.Active && API.AIData.Where(x => x.reference == mapping.reference && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode).FirstOrDefault() != null)
                {
                    //mapping.mode = (int)UTIL.Enums.SYNC_MODE.TO_CMS;
                    //mapping.id = mapping.entity_Id;
                    mapping.entity_name = StringValueOf((UTIL.Enums.ENTITIES)mapping.entity_Id);
                    // mapping.createdAt = UTIL.GlobalApp.CurrentDateTime;
                    // MappingViewModel inputEntity = mapping.Clone();
                    MappingViewModel updatedFound = API.AIData.Where(x => x.reference == mapping.reference && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && mapping.mode == x.mode).FirstOrDefault();
                    if (updatedFound != null)
                    {
                        updatedFound.status = (int)UTIL.Enums.RESERVATION_STATUS.DELETED;
                        mapping.createdAt = updatedFound.createdAt;
                        API.AIData.Remove(updatedFound);
                        API.AIData.Add(mapping);
                        bookingFound = true;
                    }


                }
            }

            if (this.InvokeRequired)
            {
                if (mapping != null && mapping.status != (int)UTIL.Enums.STATUSES.Active)
                {
                    this.Invoke((MethodInvoker)delegate {

                        Form frm = (Form)this.ActiveMdiChild;
                        if (frm != null && frm.Name.ToLower() == "BDUSyncForm")
                        {
                            BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                            bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("PMS scan failed, {0}", mapping.xpath.ToString()));
                        }
                    });
                }
                else if (bookingFound)
                {
                    this.Invoke((MethodInvoker)delegate {

                        Form frm = (Form)this.ActiveMdiChild;
                        if (frm != null && frm.Name == "BDUSyncForm")
                        {
                            BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                            bdufrm.fillDataGrid();
                            // bdufrm.btn_SyncAll_Click(bdufrm.btn_SyncAll, null);
                            bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Booking# '{0}' data recieved..", mapping.reference));
                        }
                    });
                }

            }//  if (this.InvokeRequired)
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
                    _logger.Error(ex);
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
            this.ResetDefaultSelection(sender as Button);
            BDUMappingForm frm = new BDUMappingForm();
            //frm.frmMain = this;
            frm._desktopHandler = _desktopHandler;
            frm._WebHandler = _webHandler;
            switchForm(frm);
            //pnl_LastSyncTime.Visible = false;
            pnl_SyncControl.Visible = false;
        }
        private void btnPreference_Click(object sender, EventArgs e)
        {
            this.ResetDefaultSelection(sender as Button);
            BDUPreferencesForm frm = new BDUPreferencesForm();
            switchForm(frm);
            // pnl_LastSyncTime.Visible = false;
            pnl_SyncControl.Visible = false;
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            // Button btn = sender as Button;
            this.ResetDefaultSelection(sender as Button);
            BDU.UTIL.Enums.MESSAGERESPONSETYPES res = Servr.UI.ServrMessageBox.Confirm("Are you sure, want to logout?", "Confirmation!");

            if (res == BDU.UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                _logger.Info("Logged out from IntegrateX");
                if (_webHandler != null)
                    _webHandler.StopSession();
                if (_desktopHandler != null)
                    _desktopHandler.StopDesktopSession();
                Application.Exit();


            }
            //pnl_LastSyncTime.Visible = false;
            pnl_SyncControl.Visible = false;
        }
        #endregion
        #region "load & Close Events"
        private async void frmMain_Load(object sender, EventArgs e)
        {
            currentForm = new BDUSyncForm(_desktopHandler, _webHandler);
            AiIntegrationTasker();
            this.btn_BDUSync_Click(this.btn_BDUSync, null);
            // PlaceLowerRight();
            //IEnumerable<HotelViewModel> hlist = await _bduservice.getHotelslist(0, (int)UTIL.Enums.STATUSES.Active);
            SyncControlPanel();
            //API.PRESETS = new HotelViewModel();           

            int borderRadius = 100;
            RectangleF Rect = new RectangleF(0, 0, this.Width - 10, this.Height - 10);
            GraphicsPath GraphPath = GraphicsExtension.GetRoundPath(Rect, borderRadius);
            this.Region = new Region(GraphPath);

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
            this.Invoke((MethodInvoker)delegate {

                Form frm = (Form)this.ActiveMdiChild;
                if (frm != null && frm.Name == "BDUSyncForm")
                {
                    BDUSyncForm bdufrm = (BDUSyncForm)this.ActiveMdiChild;
                    bdufrm.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("IntegrateX started successfuly, at - {0}", System.DateTime.Now.ToString("HH:mm:ss")));
                }
            });

            if (GlobalApp.Authentication_Done)
            {
                try
                {
                    if (API.HotelList == null || API.HotelList.Count <= 0)
                        API.HotelList = await _bduservice.getHotelslist(GlobalApp.Hotel_id, (int)UTIL.Enums.STATUSES.Active);

                    if (API.HotelList != null && API.HotelList.Count > 0 && (API.PMSVersionsList == null || API.PMSVersionsList.Count <= 0))
                    {
                        Thread.Sleep(500);
                        API.PMSVersionsList = await _bduservice.getPMSVersions();
                    }
                }
                catch (Exception) { }
            }


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
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            //this.Left = rightmost.WorkingArea.Right - this.Width - 5;
            //this.Top = rightmost.WorkingArea.Bottom - this.Height - 5;
        }

        private void btn_MainMenuClose_Click(object sender, EventArgs e)
        {
            UTIL.Enums.MESSAGERESPONSETYPES res = Servr.UI.ServrMessageBox.Confirm("Are you sure, want to logout?", "Confirmation!");

            if (res == UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
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

            if (pBtn.Name == this.btn_Mapping.Name && btn_Mapping.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btn_Mapping.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btn_Mapping.BackColor = Color.White;
            }

            if (pBtn.Name == this.btnPreference.Name && btnPreference.Tag.ToString().ToLower() == this.currentForm.Name.ToLower())
            {
                this.btnPreference.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                this.btnPreference.BackColor = Color.White;
            }

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
                UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com/spas/1");
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
                UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com/concierges");
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
                UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com/experience");
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
                UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com/restaurants/1");
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
            UTIL.GlobalApp.OpenBrowser("https://guestx.servrhotels.com//docs/IntegrateX/IntegrateXDemo.mp4");
        }
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = System.Windows.Forms.FormWindowState.Minimized;
            //this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        #region "Tasker"
        public async void AiIntegrationTasker()
        {
            if (GlobalApp.Authentication_Done && GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
            {
                var taskTimer = Task.Factory.StartNew(async () =>
                {

                    while (true)
                    {
                        if (UTIL.GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.READY || UTIL.GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.DEFAULT)
                        {
                            await CMSLongRunningTask();
                        }
                        await Task.Delay(BDU.UTIL.GlobalApp.AIService_Interval_Secs * 1000);
                    }
                },
                TaskCreationOptions.LongRunning);

                // );
            }
        }

        private async Task CMSLongRunningTask()
        {
            bool newDataFound = false;
            bool newStatsFound = false;
            if (GlobalApp.Authentication_Done && !GlobalApp.isNew)
            {
                // UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                //  List<MappingViewModel> retls =  _bduservice.TestFieldMapping( Enums.PMS_VERSIONS.PMS_Desktop);// (GlobalApp.Hotel_id, UTIL.GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS), (int)UTIL.Enums.STATUSES.InActive).AddHours(-30;
                List<MappingViewModel> retls = await _bduservice.getCMSDataService(GlobalApp.Hotel_id, UTIL.GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncBackTime_CMS).AddSeconds(0));
                GlobalApp.SyncBackTime_CMS = UTIL.GlobalApp.GetLastSyncTimeWithDifference(System.DateTime.UtcNow);
                // GlobalApp.las = UTIL.GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS);
                if (retls != null && API.AIData != null && retls.Any())
                {

                    foreach (MappingViewModel mvModl in retls)
                    {
                        if (mvModl.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC && API.AIData.Where(x => x.reference == mvModl.reference && mvModl.entity_Id == x.entity_Id && x.mode == mvModl.mode && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC).FirstOrDefault() == null)
                        {
                            mvModl.createdAt = UTIL.GlobalApp.CurrentLocalDateTime;
                            API.AIData.Add(mvModl);
                            newDataFound = true;
                        }
                        else if (mvModl.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC && API.AIData.Where(x => x.reference == mvModl.reference && x.mode == mvModl.mode && mvModl.entity_Id == x.entity_Id && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC).FirstOrDefault() != null)
                        {
                            MappingViewModel syncEnty = API.AIData.FirstOrDefault(s => s.reference == mvModl.reference && s.mode == mvModl.mode && mvModl.entity_Id == s.entity_Id && s.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && s.entity_type == (int)UTIL.Enums.ENTITY_TYPES.SYNC);

                            if (syncEnty != null)
                            {
                                mvModl.createdAt = syncEnty.createdAt;
                                mvModl.mode = syncEnty.mode;
                                syncEnty.status = (int)UTIL.Enums.RESERVATION_STATUS.DELETED;
                                API.AIData.Remove(syncEnty);
                                API.AIData.Add(mvModl);
                                newDataFound = true;
                            }

                        }
                        if (mvModl.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS && !API.AIData.Where(x => x.entity_Id == mvModl.entity_Id && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS).Any())
                        {
                            API.AIData.Add(mvModl);
                            newStatsFound = true;
                        }
                        else if (mvModl.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS && API.AIData.Where(x => x.entity_Id == mvModl.entity_Id && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS).Any())
                        {
                            MappingViewModel statsEnty = API.AIData.FirstOrDefault(x => x.entity_Id == mvModl.entity_Id && x.status == (int)UTIL.Enums.RESERVATION_STATUS.ACTIVE && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS);
                            if (statsEnty != null)
                                API.AIData.Remove(statsEnty);
                            API.AIData.Add(mvModl);
                            newStatsFound = true;
                        }
                    }
                    if (newDataFound || newStatsFound)
                    {
                        UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
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
                UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                // await Task.Delay(30000);
            }
        }
        #endregion
    }
}

