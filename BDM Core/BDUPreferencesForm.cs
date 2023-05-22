using System;
using BDU.UTIL;
using System.Drawing;
using System.Windows.Forms;
using BDU.Extensions;
using BDU.Services;
using BDU.ViewModels;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace servr.integratex.ui
{
    public partial class BDUPreferencesForm : System.Windows.Forms.Form
    {
        #region "Variables & initialization"
        BDUService service = new BDUService();
         private Logger _log = LogManager.GetCurrentClassLogger();
        //  ********************** GET PRESETS **********************************//
        //static HotelViewModel hptelPresets = API.PRESETS;       
        List<PreferenceViewModel> prstls = new List<PreferenceViewModel>();
        ConfigurationAndSettingsViewModel config = new ConfigurationAndSettingsViewModel();
        //  BDUService _service = new BDUService();
        public BDUPreferencesForm()
        {
            InitializeComponent();           
            this.txtApiUrl.Text = GlobalApp.API_URI;         
        }
        #endregion
      
        #region "Form actions {Load & Shown}"
        private void BDUPreferencesForm_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //this.txtStations.Enabled = false;
            this.btnSave.ForeColor = GlobalApp.Btn_InactiveColor;
            this.btnTestCall.ForeColor = GlobalApp.Btn_InactiveColor;
            this.btnCancel.ForeColor = GlobalApp.Btn_Cancel_InactiveColor;
            this.ControlBox = false;
            this.txtExePath.Text = BDU.UTIL.GlobalApp.APP_ARGUMENTS;
            this.txtStations.Text = BDU.UTIL.GlobalApp.STATION_NUMBER;
            // IX Setting load from Config

            this.txtApiUrl.Text = GlobalApp.API_URI;
            this.txtAPIInterval.Text = Convert.ToString(GlobalApp.AIService_Interval_Secs);
           
           // this.cmb.SelectedValue = Convert.ToInt16(GlobalApp.AUTOMATION_MODE_TYPE_CONFIG);
            //******************************* Fill Control Type***************************//
            this.cmbEngineVersion.ValueMember = "id";
            this.cmbEngineVersion.DisplayMember = "name";
            cmbEngineVersion.DataSource = GlobalApp.IX_ENGINE_VERSIONS();
            //******************************* Fill Control Type***************************//
            this.cmbAutomationModeType.ValueMember = "id";
            this.cmbAutomationModeType.DisplayMember = "name";
            cmbAutomationModeType.DataSource = GlobalApp.AUTOMATION_MODE_TYPE();

            this.cmbEngineVersion.SelectedValue = Convert.ToInt16(GlobalApp.IX_ENGINE_VERSION_CONFIG);
            this.cmbAutomationModeType.SelectedValue = Convert.ToInt16(GlobalApp.AUTOMATION_MODE_TYPE_CONFIG);

            if (GlobalApp.Authentication_Done)
            {
               // if (API.HotelList == null || API.HotelList.Count <= 0)
                    //API.HotelList = await _service.getHotelslist(GlobalApp.Hotel_id, (int)UTIL.Enums.STATUSES.Active);
                if (API.HotelList != null)
                {

                    // if (GlobalApp.Hotel_id > 0)
                    if (GlobalApp.Hotel_id > 0 && GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
                    {
                        this.txtApiUrl.Enabled = false;
                        this.txtAPIInterval.Enabled = false;
                        this.txtExePath.Enabled = false;
                        this.cmbAutomationModeType.Enabled = false;
                        this.cmbEngineVersion.Enabled = false;
                        this.txtStations.Enabled = false;
                    }
                    else if (GlobalApp.login_role == Enums.USERROLES.PMS_Staff && GlobalApp.isNew)
                    {
                        this.txtApiUrl.Enabled = false;
                        this.txtAPIInterval.Enabled = false;
                        this.txtExePath.Enabled = false;
                        this.cmbAutomationModeType.Enabled = false;
                        this.cmbEngineVersion.Enabled = false;
                        this.txtStations.Enabled = false;

                    }
                    else if (GlobalApp.login_role == Enums.USERROLES.Servr_Staff)
                    {
                        this.txtApiUrl.Enabled = true;
                        this.txtAPIInterval.Enabled = true;
                        //this.txtExePath.Enabled = true;
                        this.cmbAutomationModeType.Enabled = true;
                        this.cmbEngineVersion.Enabled = true;
                        this.txtStations.Enabled = false;
                    }
                }
            }
            // Process Preferences 
            if (API.PRESETS != null && API.PRESETS.preferences != null)
                prstls = API.PRESETS.preferences;
            if (prstls != null && prstls.Count > 0)
            {
                if (prstls.Where(x => x.key == Convert.ToString(this.tgl_Notification.Tag)).FirstOrDefault() != null)
                {
                    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.tgl_Notification.Tag)).FirstOrDefault();
                  
                    if (!string.IsNullOrWhiteSpace(notifi.value)  &&  Convert.ToInt16(notifi.value)==1)
                    {
                        this.tgl_Notification.ToggleState = ToggleButton.ToggleButtonState.ON;
                    }
                    else
                        this.tgl_Notification.ToggleState = ToggleButton.ToggleButtonState.OFF;
                }
                if (prstls.Where(x => x.key == Convert.ToString(this.tgl_AutoSync.Tag)).FirstOrDefault() != null)
                {
                    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.tgl_AutoSync.Tag)).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(notifi.value) && Convert.ToInt16(notifi.value) == 1)
                    {
                        this.tgl_AutoSync.ToggleState = ToggleButton.ToggleButtonState.ON;
                    }
                    else
                        this.tgl_AutoSync.ToggleState = ToggleButton.ToggleButtonState.OFF;
                    
                }
                if (prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelFirst.Tag)).FirstOrDefault() != null)
                {
                    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelFirst.Tag)).FirstOrDefault();
                    this.txtIntervalSecond_LevelFirst.Text = notifi.value;
                    this.txtColor_LevelFirst.Text = notifi.color;
                    this.txtColor_LevelFirst.BackColor = Color.FromName(""+notifi.color);
                }
                if (prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelSecond.Tag)).FirstOrDefault() != null)
                {
                    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelSecond.Tag)).FirstOrDefault();
                    this.txtIntervalSecond_LevelSecond.Text = notifi.value;
                    this.txtColor_LevelSecond.Text = notifi.color;
                    this.txtColor_LevelSecond.BackColor = Color.FromName("" + notifi.color);
                }
                if (prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelThird.Tag)).FirstOrDefault() != null)
                {
                    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelThird.Tag)).FirstOrDefault();
                    this.txtIntervalSecond_LevelThird.Text = notifi.value;
                    this.txtColor_LevelThird.Text = notifi.color;
                    this.txtColor_LevelThird.BackColor = Color.FromName("" + notifi.color);
                }

            }

            //***************************** Set API Data************************************//
            this.txtApiUrl.Text = GlobalApp.API_URI;
            // this.txtApiUrl.Enabled = false;
            //this.txtToken.Text = GlobalApp.JWT_Token;
            //this.txtToken.Enabled = false;

            // AUTO Sync is disabled bc its not in scope.
            //this.tgl_AutoSync.Enabled = false;
            Cursor.Current = Cursors.Default;
        }
        private void BDUPreferencesForm_Shown(object sender, EventArgs e)
        {
            // _log.Info("Preference started loading, at- " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
        }
        #endregion
        #region "Buttons actions {Cancel, Test Call & Save}"
        private void btnCancel_Click(object sender, EventArgs e)
        {

            Enums.MESSAGERESPONSETYPES res = ServrMessageBox.Confirm("Are you sure you would like to reset your settings?", "Confirmation!");

            if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                this.BDUPreferencesForm_Load(null, null);
            }
        }
        private async void btnTestCall_Click(object sender, EventArgs e)
        {
            try
            {
                _log.Info("Test run started, at- " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrWhiteSpace(txtApiUrl.Text) || txtApiUrl.Text.Length <= 10)
                {
                    ServrMessageBox.Error(string.Format("Please enter correct '{0}'", lblApiUrl.Text), "Failed");
                    this.txtApiUrl.Focus();
                }
                else
                {
                    try
                    {
                        BDUService apiClient = new BDUService();
                        UserViewModel uvm = await apiClient.getUserDetails();
                        if (uvm != null)
                        {
                            ServrMessageBox.ShowBox("Your API connection has been verified.", "Success");
                            //this.txtApiUrl.ForeColor = Color.ForestGreen;                   
                        }
                        else
                            ServrMessageBox.Error("Your API connection has failed. Please try again, or contact support@servrhotels.com for assistance", "Failed");

                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                        ServrMessageBox.Error(string.Format("Your API connection has failed, error -{0}", ex.Message), "Failed");

                    }
                }
            }

            catch (Exception ex)
            {
                // _log.Error(ex);
                ServrMessageBox.ShowBox(ex.StackTrace.ToString(), "ERROR");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (performValidation())
            {
                if (prstls != null && prstls.Count > 0)
                {
                    if (prstls.Where(x => x.key == Convert.ToString(this.tgl_Notification.Tag)).FirstOrDefault() != null)
                    {
                        PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.tgl_Notification.Tag)).FirstOrDefault();
                        notifi.value = Convert.ToString(this.tgl_Notification.ToggleState== ToggleButton.ToggleButtonState.ON?1:0);
                    }
                    if (prstls.Where(x => x.key == Convert.ToString(this.tgl_AutoSync.Tag)).FirstOrDefault() != null)
                    {
                        PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.tgl_AutoSync.Tag)).FirstOrDefault();
                        notifi.value = Convert.ToString(this.tgl_AutoSync.ToggleState == ToggleButton.ToggleButtonState.ON ? 1 : 0);  //Convert.ToString(this.tgl_AutoSync.Enabled);
                    }
                    if (prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelFirst.Tag)).FirstOrDefault() != null)
                    {
                        PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelFirst.Tag)).FirstOrDefault();
                        notifi.value = this.txtIntervalSecond_LevelFirst.Text;
                        notifi.color = this.txtColor_LevelFirst.Text;
                    }
                    if (prstls.Where(x => x.key.Trim() == Convert.ToString(this.txtIntervalSecond_LevelSecond.Tag)).FirstOrDefault() != null)
                    {
                        PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelSecond.Tag)).FirstOrDefault();
                        notifi.value = this.txtIntervalSecond_LevelSecond.Text;
                        notifi.color = this.txtColor_LevelSecond.Text;
                    }
                    if (prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelThird.Tag)).FirstOrDefault() != null)
                    {
                        PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtIntervalSecond_LevelThird.Tag)).FirstOrDefault();
                        notifi.value = this.txtIntervalSecond_LevelThird.Text;
                        notifi.color = this.txtColor_LevelThird.Text;
                    }
                    //if (prstls.Where(x => x.key == Convert.ToString(this.txtExePath.Tag)).FirstOrDefault() != null)
                    //{
                    //    PreferenceViewModel notifi = prstls.Where(x => x.key == Convert.ToString(this.txtExePath.Tag)).FirstOrDefault();
                    //    notifi.value = this.txtExePath.Text;
                    //    notifi.color = this.txtExePath.Text;
                    //}
                   
                    // ************************  Set to Global Preferences ***************************//
                    API.PRESETS.preferences = prstls;
                    //*********************** SAVE************************************//
                   ServrMessageBox.ShowBox("Your preferences have been saved.", "Prefrences");
                }
           
             if (GlobalApp.login_role== Enums.USERROLES.Servr_Staff && !string.IsNullOrWhiteSpace(GlobalApp.Hotel_Code) && !string.IsNullOrWhiteSpace(this.txtAPIInterval.Text))
                {
                    // ConfigurationAndSettingsViewModel config = new ConfigurationAndSettingsViewModel();
                    config.custom1 = GlobalApp.CUSTOM1;
                    // config.app = GlobalApp.API_URI;
                    config.automation_mode_type = Convert.ToInt32(this.cmbAutomationModeType.SelectedValue);
                    config.ix_engine_version = Convert.ToInt32(this.cmbEngineVersion.SelectedValue);
                    config.app = txtExePath.Text;
                    config.service_url = txtApiUrl.Text;
                    config.station_number = txtStations.Text;
                    config.sync_interval = Convert.ToInt32(this.txtAPIInterval.Text);
                    GlobalApp.API_URI = config.service_url;
                    GlobalApp.AIService_Interval_Secs = config.sync_interval;
                    GlobalApp.APP_ARGUMENTS = config.app;
                    GlobalApp.STATION_NUMBER = config.station_number;                    
                    GlobalApp.AUTOMATION_MODE_TYPE_CONFIG = config.automation_mode_type;
                    GlobalApp.IX_ENGINE_VERSION_CONFIG = config.ix_engine_version;
                    // config.service_url = txtApiUrl.Text;


                    // ************************  Set to Global Preferences ***************************//
                    API.IXSettings = config.DeepClone();
                    //*********************** SAVE************************************//
                   // servr.integratex.ui.ServrMessageBox.ShowBox("Your preferences have been saved.", "Prefrences");
                }
            }
        }
        /*------------Almas Perform Validation------------*/
        #endregion
        #region "Validation & Control events"
        private bool performValidation()
        {
            if (txtIntervalSecond_LevelFirst.Text.Length < 1 || txtIntervalSecond_LevelFirst.Text == "" || !System.Text.RegularExpressions.Regex.IsMatch(txtIntervalSecond_LevelFirst.Text, @"^[0-9]+$"))
            {
                //ServrMessageBox.Error("The following Input " + lblIntervalSecond_LevelFirst.Text + "is required");
                ServrMessageBox.Error(string.Format("Please enter correct '{0}' (a delay in seconds) as some PMS may have a loading period before we can access the next stage!", lblIntervalSecond_LevelFirst.Text));
                this.txtIntervalSecond_LevelFirst.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtApiUrl.Text) || this.txtApiUrl.Text.Length <= 10)
            {
                //ServrMessageBox.Error("The following Input " + lblIntervalSecond_LevelFirst.Text + "is required");
                ServrMessageBox.Error(string.Format("Please enter correct '{0}'", lblApiUrl.Text));
                this.txtApiUrl.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(this.txtAPIInterval.Text) || this.txtAPIInterval.Text.Length <= 0 || !System.Text.RegularExpressions.Regex.IsMatch(txtAPIInterval.Text, @"^[0-9]+$"))
            {
                //ServrMessageBox.Error("The following Input " + lblIntervalSecond_LevelFirst.Text + "is required");
                ServrMessageBox.Error(string.Format("Please enter correct '{0}'", lblApiUrl.Text));
                this.txtApiUrl.Focus();
                return false;
            }           
            else if (txtColor_LevelFirst.Text.Length < 3 || txtColor_LevelFirst.Text == "")
            {
                ServrMessageBox.Error(string.Format("Please enter correct '{0}'", lblColor_LevelFirst.Tag));
                this.txtColor_LevelFirst.Focus();
                return false;
            }
            else if (txtIntervalSecond_LevelSecond.Text.Length < 1 || txtIntervalSecond_LevelSecond.Text == "" || !System.Text.RegularExpressions.Regex.IsMatch(txtIntervalSecond_LevelSecond.Text, @"^[0-9]*$"))
            {
                ServrMessageBox.Error(string.Format("Please enter correct '{0}' (a delay in seconds) as some PMS may have a loading period before we can access the next stage!", lblIntervalSecond_LevelSecond.Text));
                this.txtIntervalSecond_LevelSecond.Focus();
                return false;
            }
            else if (txtColor_LevelSecond.Text.Length < 3 || txtColor_LevelSecond.Text == "")
            {
                ServrMessageBox.Error(string.Format("Please enter correct '{0}' to highlight the reservation data with respect to delay!", lblColor_LevelSecond.Tag));
                this.txtColor_LevelSecond.Focus();
                return false;
            }
            else if (txtIntervalSecond_LevelThird.Text.Length < 1 || txtIntervalSecond_LevelThird.Text == "" || !System.Text.RegularExpressions.Regex.IsMatch(txtIntervalSecond_LevelThird.Text, @"^[0-9]*$"))
            {
                ServrMessageBox.Error(string.Format("Please enter correct '{0}' (a delay in seconds) as some PMS may have a loading period before we can access the next stage!", lblIntervalSecond_LevelThird.Text));
                this.txtIntervalSecond_LevelThird.Focus();
                return false;
            }
            else if (txtColor_LevelThird.Text.Length < 3 || txtColor_LevelThird.Text == "")
            {
                ServrMessageBox.Error(string.Format("Please enter correct '{0}' to highlight the reservation data with respect to delay!", lblColor_LevelThird.Tag));
                this.txtColor_LevelThird.Focus();
                return false;
            }           
            //else if (Convert.ToInt32(cmbAutomationModeType.SelectedValue) == (int)UTIL.Enums.AUTOMATION_MODE_TYPE.PROCESS)
            //{
            //    ServrMessageBox.Error(string.Format("Please enter correct '{0}'", lblAutomationModeType.Text));
            //    this.txtExePath.Enabled = false;
            //    return false;
            //}
            return true;
        }
        
        private void cmbAutomationModeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbAutomationModeType.SelectedValue) ==((int)Enums.AUTOMATION_MODE_TYPE.PROCESS))
            {
                this.txtExePath.Enabled = true;
                GlobalApp.IS_PROCESS = "1";
            }           
            else
            {
                this.txtExePath.Enabled = false;
                GlobalApp.IS_PROCESS = "0";
            }
        }

        #endregion
    }
}
