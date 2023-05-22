using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using BDU.UTIL;
using System.Windows.Forms;
using BDU.Services;
using NLog;
using BDU.ViewModels;
using System.Collections.Generic;

namespace servr.integratex.ui
{
    public partial class BDULoginForm : Form
    {
        #region "Initialize & Constructors"
        private Logger _log = LogManager.GetCurrentClassLogger();
        BDUService apiClient = new BDUService();
        public BDULoginForm()
        {
            InitializeComponent();
            //this.AllowDrop = true;
           // this.ControlBox = true;
            // txtPassword.Controls.Add(Pic_Eye);login
            txtPassword.PasswordChar = '*';
            //Subscribe to Event
            Pic_Eye.MouseDown += new MouseEventHandler(pictureBoxEye_MouseDown);
            Pic_Eye.MouseUp += new MouseEventHandler(pictureBoxEye_MouseUp);
            //string str = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
          //  _log.Info("IntegrateX Started");

        }
        #endregion
        #region "Form level events"
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        private void pictureBoxEye_MouseDown(object sender, MouseEventArgs e)
        {
             txtPassword.PasswordChar = '\0';
        }

        private void pictureBoxEye_MouseUp(object sender, MouseEventArgs e)
        {
             txtPassword.PasswordChar = '*';
        }
        private async void btnLogin_Click(object sender, EventArgs e)
         {           
            try {
                Cursor.Current = Cursors.WaitCursor;
                
                await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("Login attempted, by user {0} ", this.txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                // _log.Info(string.Format("Login attempted by user using account {0} - at {1}", this.txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                _log.Info(string.Format("Login attempted by user using account {0} - at {1}" , this.txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                if (txtUserName.Text == "")
                {
                   
                    ServrMessageBox.Error(string.Format("Please enter your {0}", txtUserName.Tag), "Login Attempt Failed");
                    this.txtUserName.Focus();
                }
                else if (txtPassword.Text == "")
                {
                    ServrMessageBox.Error(string.Format("Please enter your {0} ", txtPassword.Tag), "Login Attempt Failed");
                    this.txtPassword.Focus();
                }
                else
                {
                    // Cursor.Current = Cursors.WaitCursor;
                    // Perform Authentication using BDU API Service,for Almas
                    this.btnLogin.Enabled = false;
                    BDU.ViewModels.ResponseViewModel res = await apiClient.Login(txtUserName.Text, txtPassword.Text);

                    // Check Response && Authorization
                    if (res.status && GlobalApp.Authentication_Done && res.status_code.ToLower() == ((int)Enums.ERROR_CODE.SUCCESS).ToString())
                    {
                        // **************************  Login Success *****************************//

                        //****************** GET Hotel Presets************************************//
                        try
                        {
                            HotelViewModel hotel = null;
                            if(GlobalApp.login_role== Enums.USERROLES.PMS_Staff)
                            { 
                             hotel = await apiClient.getHotelPresets(GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)Enums.PMS_VERSIONS.PMS_Desktop, string.Empty);
                                if (hotel == null || Convert.ToInt32(hotel.version) <= 0) {
                                    await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = "Unable to load hotel preset ", log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                                    ServrMessageBox.Error("Unfortunately hotel PMS integration is not complete yet, please try later or contact: support@servrhotels.com", "Failed");
                                    this.btnLogin.Enabled = true;
                                    return;
                                }

                            }
                            if (hotel != null)
                            {
                                GlobalApp.isNew = false;
                                API.PRESETS = hotel;
                                API.ClonedPRESETS = hotel;
                                GlobalApp.PMS_Version_No = hotel.id;
                                GlobalApp.PMS_Version = hotel.version;
                                GlobalApp.SYSTEM_TYPE = hotel.system_type;
                                GlobalDiagnosticsContext.Set("hotelname",string.IsNullOrWhiteSpace(hotel.hotel_name)? hotel.code: hotel.hotel_name);
                            }
                            else
                            {
                               // _log.Warn(string.Format("Preset not found, loading default, email- {0}", this.txtUserName.Text));
                                //hotel = new HotelViewModel();
                                //hotel.id = GlobalApp.Hotel_id;
                                GlobalApp.isNew = true;                               
                                hotel = await apiClient.LoadDefaultPresets(GlobalApp.Hotel_id,string.Empty); //GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop, string.Empty)
                                API.PRESETS = hotel;
                                API.ClonedPRESETS = hotel;
                            }
                           // GlobalApp.SYSTEM_TYPE = ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString();
                        }
                        catch (Exception ex)   {
                            _log.Info(ex);
                            ServrMessageBox.Error(string.Format("Unfortunately we couldn't load hotel mappings & settings, please try again or contact: support@servrhotels.com, Error {0}", ex.Message), "Failed");
                            this.btnLogin.Enabled = true;
                            return;
                        }
                        //****************** GET LOVs Global************************************//
                        try
                        {
                            List<LOVViewModel> lovs = null;

                            lovs = await apiClient.getLOVData();// (GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop, string.Empty);
                            API.LOVData = lovs;
                            // GlobalApp.SYSTEM_TYPE = ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString();
                        }
                        catch (Exception ex)
                        {
                            _log.Info(ex);
                            await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = string.Format("Unable to load configuration data , Error {0}", ex.Message), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                            ServrMessageBox.Error(string.Format("Unable to load reservation status data , Error {0}", ex.Message), "Failed");
                           // return;
                        }
                        _log.Info(string.Format("user - {0}, login time is {1} ", txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel {id= new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("user {0} logged in successfully", this.txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                        WindowNotifications.windowNotification(message: string.Format("user - {0}, login time is {1} ", txtUserName.Text, "IntegrateX Login"), icon: servr.integratex.ui.Properties.Resources.complete_Blue);
                        // **************************  Load MDI Form *****************************//
                        frmMain frm = new frmMain();
                        frm.FormClosed += new FormClosedEventHandler(frmLogIn_FormClosed);
                        this.btnLogin.Enabled = true;
                        SQLiteDbManager.purgeData(0, GlobalApp.CurrentLocalDateTime.AddDays(-40), GlobalApp.CurrentLocalDateTime.AddDays(-BDU.UTIL.GlobalApp.IX_SQLITE_DATA_PURGE_DAYS_INTERVAL_DAYS));
                        frm.Show();
                        this.Hide();
                    }
                    else if (res.status_code == ((int)Enums.ERROR_CODE.FAILED).ToString())
                    {
                        _log.Error(string.Format("User {0} login failed, try later or contact admin.", txtUserName.Text));
                        ServrMessageBox.Error(string.Format("We couldn't find the account '{0}' you have provided, please try again, or contact: support@servrhotels.com", txtUserName.Text), "Login Failed");
                        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.FAIL.ToString(), log_detail = string.Format("We couldn't find the account '{0}' you have provided, please try again, or contact: support@servrhotels.com", txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                    }
                    else if (res.status_code.ToLower() == ((int)Enums.ERROR_CODE.ERROR_DATA).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                    {
                        _log.Error(res.message);
                        ServrMessageBox.Error(string.Format("We couldn't find the account with the email or password you have provided, please try again, or contact: support@servrhotels.com"), "Login Failed");
                    }
                    else
                    {
                        _log.Error(res.message);
                       await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = ""+res.message, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                        WindowNotifications.windowNotification(  string.Format("Authentication failed, API Error  {0} ", res.message, "Login Failed"));
                        ServrMessageBox.Error(string.Format("Authentication failed, API Error  {0} ", res.message), "Login Failed");
                    }

                }
               // Cursor.Current = Cursors.Default;
            }

            catch (Exception ex)
            {
                _log.Error(ex);
                // _log.Fatal("Unfortunately IX-Failure Detected", ex);                
                ServrMessageBox.Error("IntegrateX is currently offline or global configuration retreival failed!","Internet Connectivity");
            }
            finally
            {
                if(btnLogin!= null)
                this.btnLogin.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }       
        private void frmLogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            this.Close();
        }
        private async void BDULoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                PlaceLowerRight();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                this.lblVersion.Text= string.Format("Version {0}", fvi.FileVersion);
                //  BDUService apiClient = new BDUService();
                GlobalApp.LogEmailSettings = await apiClient.loadLogMailSettings(BDU.UTIL.GlobalApp.SPECIAL_EMAIL_ACCOUNT, BDU.UTIL.GlobalApp.SPECIAL_EMAIL_ACCOUNT_PWD);
                //***************************** MAIL SETTINGS  ***********************************/
                if (GlobalApp.LogEmailSettings != null)
                {

                    
                    EmailConfigurationsViewModel emailConfig= (EmailConfigurationsViewModel) GlobalApp.LogEmailSettings;
                   // GlobalApp.SyncBackTime_CMS = emailConfig.utctime;
                    GlobalDiagnosticsContext.Set("hotelname", GlobalApp.Hotel_Code);
                    GlobalDiagnosticsContext.Set("subjectcritical", emailConfig.subjectcritical);// "IntegrateX Critical Error");
                    GlobalDiagnosticsContext.Set("subjecttechnical", emailConfig.subjecttechnical);// "IntegrateX Error");
                    GlobalDiagnosticsContext.Set("smtpuser", emailConfig.smtpuser);// "no-reply@servrho tels.com");
                    GlobalDiagnosticsContext.Set("smtppassword", emailConfig.smtppassword); // "Servrhotels@123");
                    GlobalDiagnosticsContext.Set("smtpserver", emailConfig.smtpserver);// "smtp.servrhotels.com");
                    GlobalDiagnosticsContext.Set("smtpport", emailConfig.smtpport);
                    GlobalDiagnosticsContext.Set("enablessl", bool.Parse(emailConfig.enablessl));//  "false");
                    GlobalDiagnosticsContext.Set("mailfrom", emailConfig.mailfrom);// "no-reply@servrhotels.com");
                    GlobalDiagnosticsContext.Set("mailtorevenueteam", emailConfig.mailtorevenueteam);// "znawazch@gmail.com");
                    GlobalDiagnosticsContext.Set("mailtotechnicalteam", emailConfig.mailtotechnicalteam);// "znawazch@gmail.com");
                }
                //***************************** MAIL SETTING END ***********************************/
               // _log.Info(string.Format("IntegrateX login process started, at {0} " , GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
               
                this.btnLogin.ForeColor = GlobalApp.Btn_InactiveColor;
                //**************************** Set Global variable for one time loading only********************//
                GlobalApp.API_URI = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ServrMessageBox.ShowBox(ex.StackTrace.ToString(), "ERROR");
            }
        }
        private void PlaceLowerRight()
        {
            int borderRadius = 103;
            RectangleF Rect = new RectangleF(0, 0, this.Width - 3, this.Height - 3);
            GraphicsPath GraphPath = GraphicsExtension.GetRoundPath(Rect, borderRadius);
            this.Region = new Region(GraphPath);

        }
        private void btn_LoginClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void lbl_ForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {      
            try
            {
                GlobalApp.OpenBrowser("https://guestx.servrhotels.com/forgot-password");                
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
               // _log.Error(ex);
                MessageBox.Show("Unable to load forgot password GuestX link." + ex.Message);
            }
        }
        private void btn_Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                this.btnLogin_Click(null, null);
        }
        #endregion
    }
}
