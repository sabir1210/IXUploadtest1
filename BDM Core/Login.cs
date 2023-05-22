using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;
using Microsoft.Extensions.Logging;
using NLog;

namespace servr.integratex.ui
{
    public partial class Login : Form
    {
        #region "Initialize & Constructors"
        private Logger _log = LogManager.GetCurrentClassLogger();
        BDUService apiClient = new BDUService();
        static Login logingForm;      
        static Enums.MESSAGERESPONSETYPES BUTTON_RESPONSE;
       public bool loginStatus= false;
        //int disposeFormTimer;
        public Login()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
            //Subscribe to Event
            Pic_Eye.MouseDown += new MouseEventHandler(pictureBoxEye_MouseDown);
            Pic_Eye.MouseUp += new MouseEventHandler(pictureBoxEye_MouseUp);
        }       
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES LoginConfirm()
        {
            logingForm = new Login();
            logingForm.ShowDialog();
            //if (messageBox != null)
            //{
            //    messageType = 2;
            //    messageBox.ShowDialog();

            //}
            return BUTTON_RESPONSE;
        }
        private void pictureBoxEye_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '\0';
        }

        private void pictureBoxEye_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }
        private async void Messagebox_Load(object sender, EventArgs e)
        {

            try
            {
                PlaceLowerRight();              
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
            try
            {

                int borderRadius = 103;
                RectangleF Rect = new RectangleF(0, 0, this.Width - 3, this.Height - 3);
                GraphicsPath GraphPath = GraphicsExtension.GetRoundPath(Rect, borderRadius);
                this.Region = new Region(GraphPath);
            }
            catch (Exception ex) { }

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // throw new Exception("IX failed, Mapping not loading");
                await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("Login attempted, by user {0} ", this.txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                // _log.Info(string.Format("Login attempted by user using account {0} - at {1}", this.txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                _log.Info(string.Format("Login attempted by user using account {0} - at {1}", this.txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
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
                    Cursor.Current = Cursors.WaitCursor;
                    // Perform Authentication using BDU API Service,for Almas

                    ResponseViewModel res = await apiClient.preferenceLogin(txtUserName.Text, txtPassword.Text);

                    // Check Response && Authorization
                    if (res.status_code.ToLower() == ((int)Enums.ERROR_CODE.SUCCESS).ToString())
                    {
                        // **************************  Login Success *****************************//

                        //****************** GET Hotel Presets************************************//
                        //try
                        //{
                        //    HotelViewModel hotel = null;
                        //    //if (GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
                        //    //{
                        //    //    hotel = await apiClient.getHotelPresets(GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)Enums.PMS_VERSIONS.PMS_Desktop, string.Empty);
                        //    //    if (hotel == null || Convert.ToInt32(hotel.version) <= 0)
                        //    //    {
                        //    //        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = "Unable to load hotel preset ", log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                        //    //        ServrMessageBox.Error("Unfortunately hotel PMS integration is not complete yet, please try later or contact: support@servrhotels.com", "Failed");
                        //    //        return;
                        //    //    }

                        //    //}
                        //    if (hotel != null)
                        //    {
                        //        GlobalApp.isNew = false;
                        //        API.PRESETS = hotel;
                        //        API.ClonedPRESETS = hotel;
                        //        GlobalApp.PMS_Version_No = hotel.id;
                        //        GlobalApp.PMS_Version = hotel.version;
                        //        GlobalApp.SYSTEM_TYPE = hotel.system_type;
                        //        GlobalDiagnosticsContext.Set("hotelname", string.IsNullOrWhiteSpace(hotel.hotel_name) ? hotel.code : hotel.hotel_name);
                        //    }
                        //    else
                        //    {
                        //        // _log.Warn(string.Format("Preset not found, loading default, email- {0}", this.txtUserName.Text));
                        //        //hotel = new HotelViewModel();
                        //        //hotel.id = GlobalApp.Hotel_id;
                        //        GlobalApp.isNew = true;
                        //        hotel = await apiClient.LoadDefaultPresets(GlobalApp.Hotel_id, string.Empty); //GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop, string.Empty)
                        //        API.PRESETS = hotel;
                        //        API.ClonedPRESETS = hotel;
                        //    }
                        //    // GlobalApp.SYSTEM_TYPE = ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString();
                        //}
                        //catch (Exception ex)
                        //{
                        //    _log.Info(ex);
                        //    ServrMessageBox.Error(string.Format("Unfortunately we couldn't load hotel mappings & settings, please try again or contact: support@servrhotels.com, Error {0}", ex.Message), "Failed");
                        //    Cursor.Current = Cursors.Default;
                        //    return;
                        //}
                        //****************** GET LOVs Global************************************//
                        //try
                        //{
                        //    List<LOVViewModel> lovs = null;

                        //    lovs = await apiClient.getLOVData();// (GlobalApp.Hotel_id, GlobalApp.Hotel_Code, (int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop, string.Empty);
                        //    API.LOVData = lovs;
                        //    // GlobalApp.SYSTEM_TYPE = ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString();
                        //}
                        //catch (Exception ex)
                        //{
                        //    _log.Info(ex);
                        //    await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.ERROR.ToString(), log_detail = string.Format("Unable to load configuration data , Error {0}", ex.Message), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                        //    ServrMessageBox.Error(string.Format("Unable to load reservation status data , Error {0}", ex.Message), "Failed");
                        //    // return;
                        //}
                        _log.Info(string.Format("user - {0}, login time is {1} ", txtUserName.Text, GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED)));
                        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.INFO.ToString(), log_detail = string.Format("user {0} logged in successfully", this.txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                        WindowNotifications.windowNotification(message: string.Format("user - {0},preference login time is {1} ", txtUserName.Text, "IntegrateX Login"), icon: servr.integratex.ui.Properties.Resources.complete_Blue);
                        // **************************  Load MDI Form *****************************//
                        //BDUMapping_PreferenceForm frm = new BDUMapping_PreferenceForm();
                        //Cursor.Current = Cursors.Default;
                        //frm.Show();
                        //this.Hide();
                        loginStatus = true;
                        // BDUMapping_PreferenceForm frm = new BDUMapping_PreferenceForm();
                        //  frm.Show();
                        this.Hide();                       
                    }
                    else if (res.status_code == ((int)Enums.ERROR_CODE.FAILED).ToString())
                    {
                        loginStatus = false;
                        _log.Error(string.Format("User {0} login failed, try later or contact admin.", txtUserName.Text));
                        ServrMessageBox.Error(string.Format("We couldn't find the account {0} you have provided, please try again, or contact: support@servrhotels.com", txtUserName.Text), "Login Failed");
                        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.FAIL.ToString(), log_detail = string.Format("We couldn't find the account {0} you have provided, please try again, or contact: support@servrhotels.com", txtUserName.Text), log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = this.txtUserName.Text } });
                    }
                    else if (res.status_code.ToLower() == ((int)Enums.ERROR_CODE.ERROR_DATA).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                    {
                        _log.Error(res.message);
                        ServrMessageBox.Error(string.Format("We couldn't find the account with the email or password you have provided, please try again, or contact: support@servrhotels.com"), "Login Failed");
                    }
                    else
                    {
                        _log.Error(res.message);
                        await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = "" + res.message, log_source_system = BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME, action_user_by = GlobalApp.PMS_USER_NAME } });
                        WindowNotifications.windowNotification(string.Format("Authentication failed, API Error  {0} ", res.message, "Login Failed"));
                        ServrMessageBox.Error(string.Format("Authentication failed, API Error  {0} ", res.message), "Login Failed");
                    }

                }
                Cursor.Current = Cursors.Default;
            }
            
            catch (Exception ex)
            {
                if ((""+ex.Message).ToLower().Contains("connection"))
                    ServrMessageBox.Error("Unable to connect with guestX server, check internet connection & try again.", "Connection Failed");
                else
                    ServrMessageBox.Error(ex.Message, "Login attempt failed.");
               // _log.Error(ex);
                // _log.Fatal("Unfortunately IX-Failure Detected", ex);
                await apiClient.saveAppLog(new List<AppLogViewModel>() { new AppLogViewModel { id = new Random().Next(20000, 50000), log_source = BDUConstants.LOG_SOURCE_GUESTX, hotel_id = Convert.ToInt32(GlobalApp.Hotel_id), severity = BDU.UTIL.Enums.SEVERITY_LEVEL.CRITICAL.ToString(), log_detail = ex.ToString(), log_source_system = BDUConstants.LOG_SOURCE_GUESTX, action_user_by = GlobalApp.PMS_USER_NAME } });
               
                loginStatus = false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            //BDUMapping_PreferenceForm frm = new BDUMapping_PreferenceForm();
            //frm.ShowDialog();
        }

        private void btn_LoginClose_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = ServrMessageBox.Confirm("Are you sure, want to close?", "Confirmation!");

            if (res == BDU.UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                this.Close();
            }
        }

        private void Pic_Eye_Click(object sender, EventArgs e)
        {
           // if (e.KeyChar = (Char)Keys.Enter)
                this.btnLogin_Click(null, null);
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = ServrMessageBox.Confirm("Are you sure, want to close?", "Confirmation!");

            if (res == BDU.UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                this.Close();
            }
               
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                this.btnLogin_Click(null, null);
        }
    }
}