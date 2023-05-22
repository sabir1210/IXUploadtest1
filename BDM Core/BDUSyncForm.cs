using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BDU.UTIL;
using BDU.ViewModels;
using BDU.RobotWeb;
using BDU.Services;
using NLog;
using System.Linq;
using BDU.RobotDesktop;
using System.Threading.Tasks;
using static BDU.UTIL.Enums;
using System.Windows.Threading;
using System.ComponentModel;
using System.Reflection;

namespace servr.integratex.ui
{
    public partial class BDUSyncForm : Form
    { // logging library,  NLOG
        #region "Variables & Initialization"
        Control control;
        // private const int WM_PAINT = 0xF;
        private Logger _log = LogManager.GetCurrentClassLogger();
        private BDUService _bduservice = new BDUService();
        public frmMain frmMain = null;
        DesktopHandler _desktopHandler;
        WebHandler _webHandler;
        private List<PreferenceViewModel> preferences = null;
        private System.Windows.Forms.Timer tmrSyncData = new System.Windows.Forms.Timer();
        private bool Indicators = true;
        private bool loadingData = true;
        Button btnAutoSync = null;
        public static Boolean AutoSyncIndicator = false;
       // System.Threading.Tasks.Task syncTasker = null;
        //  private bool SyncAll = false;
        private Int32 currentEntity = 0;// All


        public BDUSyncForm(DesktopHandler desktopHandler, WebHandler webHandler)
        {
            InitializeComponent();
            control = lblUpdatesBuilitin; //this can be any control
            //  ds = API.AIData;
            //API.AIData = new List<MappingViewModel>();
            _desktopHandler = desktopHandler;
            _webHandler = webHandler;
            btnAutoSync = this.btn_AutoSync;
            if(API.ErrorReferences==null)
            API.ErrorReferences = new List<ErrorViewModel>();           
            this.tmrSyncData.Tick += new System.EventHandler(this.tmrSyncData_Tick);
            tmrSyncData.Interval = BDU.UTIL.GlobalApp.AIService_Interval_Secs * 2000;
            tmrSyncData.Enabled = false;
          //  _log.Info("IntegrateX sync started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
        }
        #endregion
        #region "Events"

        #endregion
        #region "Form level events"
        private async void BDUSyncForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (API.PRESETS.preferences != null)
                {
                    this.preferences = API.PRESETS.preferences;
                }
                if (GlobalApp.Authentication_Done)// && !GlobalApp.isNew )//&& GlobalApp.Hotel_id > 0)
                {
                    // this.StartTask(null, null);
                    await fillDataGrid();
                }

                // udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, "IntegrateX is ready.");

                // AUTO ON/ OFF
                PreferenceViewModel pref = API.PRESETS.preferences.Where(x => x.key == "1").FirstOrDefault();
                if (!GlobalApp.isNew && pref != null)
                {
                    AutoSyncIndicator = Convert.ToBoolean(Convert.ToByte(pref.value));
                    if (AutoSyncIndicator)
                    {
                        btnAutoSync.Tag = "autosynon";
                        btnAutoSync.BackColor = Color.FromArgb(32, 168, 216);
                        tmrSyncData.Enabled = true;
                        tmrSyncData.Start();
                    }
                    else if (!AutoSyncIndicator)
                    {
                        tmrSyncData.Enabled = false;
                        tmrSyncData.Stop();
                        btnAutoSync.Tag = "autosynoff";
                        btnAutoSync.BackColor = Color.Gray;
                        btnAutoSync.ForeColor = Color.White;
                    }
                }
                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, GlobalApp.IX_LAST_MESSAGE);
                WindowNotifications.windowNotification(GlobalApp.IX_LAST_MESSAGE, icon: servr.integratex.ui.Properties.Resources.complete_Blue);

            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally
            {
                loadingData = false;
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
        private void BDUSyncForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (GlobalApp.Authentication_Done && !GlobalApp.isNew && BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY )
            {
                if (_desktopHandler != null || _webHandler != null)
                {                   
                    Cursor.Current = Cursors.WaitCursor;
                    if (e.Control && e.KeyCode == Keys.R)
                    {
                        if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                        {

                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.RESERVATIONS);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)), icon: servr.integratex.ui.Properties.Resources.info);
                                   // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Data of {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)));
                                    break;
                            }
                        }
                        else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            // Cursor.Current = Cursors.WaitCursor;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.RESERVATIONS);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)), icon: servr.integratex.ui.Properties.Resources.info);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Data of {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)));
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.RESERVATIONS);                                 
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)), icon: servr.integratex.ui.Properties.Resources.info);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Data of {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.RESERVATIONS)));
                                    break;
                            }
                            // Cursor.Current = Cursors.Default;
                        }//_desktopHandler
                    }
                    else if (e.Control && e.KeyCode == Keys.Enter)// CheckIn
                    {
                        if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                        {

                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                   // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)));  
                                    break;
                            }

                        }
                        else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            // Cursor.Current = Cursors.WaitCursor;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                   // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN))); 
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:
                                  //  _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)));
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                            // Cursor.Current = Cursors.Default;
                        }//_desktopHandler
                    }
                     else if (e.Control && e.KeyCode == Keys.B)// Billing
                        {
                            if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                            {

                                switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                                {
                                    case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                        _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.BILLINGDETAILS);
                                        fillDataGrid(true);
                                        WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)), icon: servr.integratex.ui.Properties.Resources.info);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)));
                                    break;
                                }

                            }
                            else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                            {
                                // Cursor.Current = Cursors.WaitCursor;
                                switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                                {
                                    case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                        _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.BILLINGDETAILS);
                                        fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)), icon: servr.integratex.ui.Properties.Resources.info);
                                   // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)));
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.BILLINGDETAILS);                                  
                                    fillDataGrid(true);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)));
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.BILLINGDETAILS)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                                // Cursor.Current = Cursors.Default;
                            }//_desktopHandler
                        }
                    else if (e.Control && e.KeyCode == Keys.X)// CheckOut
                    {
                        if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                        {

                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    //this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)));  
                                    break;
                            }

                        }
                        else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            // Cursor.Current = Cursors.WaitCursor;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)));  //  string.Format("Reservation scan has been completed successfully."));
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:
                                   // _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation {0} scan has been completed successfully.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)));  
                                    break;
                            }
                            // Cursor.Current = Cursors.Default;
                        }//_desktopHandler
                    }
                    else if (e.Control && e.KeyCode == Keys.U)// ChecIn UND
                    {
                       
                        if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                        {
                            _webHandler.Undo = 1;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                            _webHandler.Undo = 0;
                        }
                        else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            _desktopHandler.Undo = 1;
                            // Cursor.Current = Cursors.WaitCursor;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                    _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:
                                    // _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.CHECKIN);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKIN)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                            _desktopHandler.Undo = 0;
                        }//_desktopHandler                        
                    }
                    else if (e.Control && e.KeyCode == Keys.Delete)// CheckOut UND
                    {
                       
                        if (_webHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                        {
                            _webHandler.Undo = 1;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:
                                   
                                    _webHandler.SubmitAndScan((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                            _webHandler.Undo =0;
                        }
                        else if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            _desktopHandler.Undo = 1;
                            // Cursor.Current = Cursors.WaitCursor;
                            switch (GlobalApp.AUTOMATION_MODE_CONFIG)
                            {
                                case (int)Enums.AUTOMATION_MODES.UIAUTOMATION:                                   
                                    _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                                case (int)Enums.AUTOMATION_MODES.HYBRID:                                  
                                    // _desktopHandler.ScanAndCaptureFoundManual((int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    _desktopHandler.HybridScanAndCaptureFound("scanme", (int)BDU.UTIL.Enums.ENTITIES.CHECKOUT);
                                    fillDataGrid(true);
                                    WindowNotifications.windowNotification(string.Format("Undo data of {0} scan has been completed.", StringValueOf((Enums.ENTITIES)Enums.ENTITIES.CHECKOUT)), icon: servr.integratex.ui.Properties.Resources.info);
                                    break;
                            }
                            _desktopHandler.Undo = 0;
                            // Cursor.Current = Cursors.Default;
                        }//_desktopHandler
                    }
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    ServrMessageBox.Error("PMS is not running, might be logged out, , Please contact: contact@servrhotels.com for assistance!", "Failed");
                }

            }
        }
        private void lbl_FormClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void grvSyncData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && BDU.UTIL.GlobalApp.login_role== USERROLES.PMS_Staff )
                {

                    //grvSyncData.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    if (e.ColumnIndex == grvSyncData.Columns["actionCol"].Index && (BDU.UTIL.GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT))
                    {
                        BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                        DataGridViewRow dRow = grvSyncData.Rows[e.RowIndex];
                        string referenceRecord = "" + dRow.Cells["colId"].Value;
                        string entityName = "" + dRow.Cells["entityCol"].Value;
                        Int16 entity_id = Convert.ToInt16("" + dRow.Cells["colEntityId"].Value);
                        Int64 uid = Convert.ToInt64(dRow.Cells["srCol"].Value);
                        if (Convert.ToInt16(dRow.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                        {
                            try
                            {
                                if (ServrMessageBox.Confirm(string.Format("Are you sure you would like to send reservation# {0} {1} data to GuestX?", referenceRecord, entityName), "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                                {
                                    this.udpdateBuilitin(SYNC_MESSAGE_TYPES.WAIT, string.Format("Reservation# {0} {1} integration with GuestX started.", referenceRecord, entityName));
                                    // MappingViewModel mcModel = API.AIData.Where(x => x.uid == uid && x.reference == referenceRecord && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                    MappingViewModel mcModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(uid, entity_id);// && x.reference == referenceRecord && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                    if (mcModel != null && mcModel.forms != null)
                                    {
                                        // mcModel.status =(int) UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                                        List<MappingViewModel> Datals = new List<MappingViewModel>();
                                        Datals.Add(mcModel);
                                        ResponseViewModel res = await _bduservice.saveCMSData(BDU.UTIL.GlobalApp.Hotel_id, Datals);
                                        if (res != null && res.status)
                                        {
                                            //mcModel.status = (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                                            //API.AIData.Remove(mcModel);
                                            
                                            int purgedCount = SQLiteDbManager.updateReservationStatus(uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                            //int purgedCount = SQLiteDbManager.purgeData(uid, GlobalApp.CurrentLocalDateTime.AddDays(-5), GlobalApp.CurrentLocalDateTime);
                                            //BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                            await fillDataGrid(true);
                                            // GlobalApp.SyncTime_CMS = GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS);
                                            // this.lblLastSyncedDateShow.Text = GlobalApp.SyncTime_CMS.ToString(GlobalApp.date_time_format);
                                            this.udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0} {1} integration has been completed successfully.", referenceRecord, entityName));
                                        }
                                        else
                                        {
                                            this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed, please try again, or contact support@servrhotels.com for assistance.", referenceRecord, entityName));
                                        }
                                    }
                                    else
                                    {
                                        this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed, please try again, or contact support@servrhotels.com for assistance.", referenceRecord, entityName));                                       
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex);
                               // servr.integratex.ui.ServrMessageBox.Error(string.Format("The Sync with GuestX has failed, please try again, or contact support@servrhotels.com for assistance, error - {0}", ex.Message), "Error");
                                this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed.", referenceRecord, entityName));  //"Failed, click icon to see more details.");
                                                                                                                                                             // MessageBox.Show(("Pushed to PMS : " + e.RowIndex.ToString() + "  button Clicked : ") + e.ColumnIndex.ToString());
                            }

                        }
                        else if (Convert.ToInt16(dRow.Cells["colMode"].Value) != (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            try
                            {
                                BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS;
                                if (ServrMessageBox.Confirm(string.Format("Are you sure you would like to send reservation# {0} {1} data to PMS?", referenceRecord, entityName) , "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                                {
                                    this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("Reservation# {0} {1} integration with PMS has started..", referenceRecord, entityName));
                                    // MappingViewModel mcModel = API.AIData.Where(x => x.uid == uid && x.reference == referenceRecord && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                    MappingViewModel mcModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(uid, entity_id);
                                    _desktopHandler.StopCaptering();
                                    List<MappingViewModel> pmsData = new List<MappingViewModel>();
                                    pmsData.Add(mcModel);
                                    ResponseViewModel res = _desktopHandler.FeedDataToDesktopForm(pmsData);
                                   
                                    if (res.status || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                    {
                                        //mcModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                        //API.AIData.Remove(mcModel);
                                        int purgedCount = SQLiteDbManager.updateReservationStatus(mcModel.uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                       // int purgedCount = SQLiteDbManager.purgeData(uid, GlobalApp.CurrentLocalDateTime.AddDays(-5), GlobalApp.CurrentLocalDateTime);
                                        if (purgedCount > 0 && res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                        {
                                            udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);                                          
                                        }
                                        else
                                        {
                                            this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0} {1} integration with PMS completed successfully..", referenceRecord, entityName));
                                           // BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                            await fillDataGrid(true);
                                            if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == mcModel.uid).Any())
                                            {
                                                API.ErrorReferences.RemoveAll(x => x.id == mcModel.uid);
                                            }
                                        }                                     
                                       
                                    }
                                    else
                                    {
                                      //  ServrMessageBox.Error(res.message, "Error");
                                        this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration with PMS failed, reservation might be deleted or not available.", referenceRecord, entityName));
                                        API.ErrorReferences.Add(new ErrorViewModel { id = mcModel.uid, entity_id = mcModel.entity_Id, mode = mcModel.mode, reference = mcModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                    }
                                    _desktopHandler.StartCaptering();
                                }
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex);
                            }
                        }
                        else if (Convert.ToInt16(dRow.Cells["colMode"].Value) != (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS && GlobalApp.SYSTEM_TYPE != ((int)BDU.UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                        {
                            BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS;
                            if (ServrMessageBox.Confirm(string.Format("Are you sure to integrate reservation# {0} {1} data with PMS?", referenceRecord, entityName), "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                            {
                                // Enums.SYNC_MESSAGE_TYPES.WAIT
                                this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("Reservation# {0} {1} integration with PMS has started...", referenceRecord, entityName));  //"PMS Web AI. integration process started...");
                                MappingViewModel mcModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(uid, entity_id);                                                                                                                     // MappingViewModel access = API.AIData.Where(x => x.id == (int)UTIL.Enums.ENTITIES.ACCESS).FirstOrDefault();
                                List<MappingViewModel> pmsData = new List<MappingViewModel>();
                                // pmsData.Add(access);
                                pmsData.Add(mcModel);
                                // _webHandler._webData = mcModel;
                                // _webHandler.StartSession(pmsData);/ At Login time session is start here no need to send again push entity
                                ResponseViewModel res = _webHandler.FeedAllDataToWebForm(pmsData, true);
                                if (res.status || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                {
                                    // mcModel.status = (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                                    // API.AIData.Add(mcModel);
                                    //API.AIData.Remove(mcModel);
                                    int deletedCount = SQLiteDbManager.updateReservationStatus(mcModel.uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                    //int purgedCount = SQLiteDbManager.purgeData(uid, GlobalApp.CurrentLocalDateTime.AddDays(-5), GlobalApp.CurrentLocalDateTime);
                                    if (deletedCount > 0 && res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                    {
                                        udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);
                                    }
                                    else
                                    {
                                        _webHandler.StartCapturing();
                                       // BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                        await fillDataGrid(true);
                                        if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == mcModel.uid).Any())
                                        {
                                            API.ErrorReferences.RemoveAll(x => x.id == mcModel.uid);
                                        }
                                        this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0} {1} integration with PMS completed successfully.", referenceRecord, entityName)); //"PMS Web AI.Integration completed successfully.");
                                    }                                   
                                }
                                else
                                {
                                   // ServrMessageBox.Error(res.message, "Error");
                                    this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration with PMS failed, details- {2}, reservation might be deleted or not available.", referenceRecord, entityName, res.message));
                                    API.ErrorReferences.Add(new ErrorViewModel {id= mcModel.uid, entity_id = mcModel.entity_Id, mode = mcModel.mode, reference = mcModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                    // servr.integratex.ui.ServrMessageBox.Error(res.message, "Error");
                                }
                            }
                        }
                    }
                }
                loadingData = false;
                System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                // return true;
            }
            catch (Exception ex) { _log.Error(ex); }
            finally
            {
                if (BDU.UTIL.GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.READY)
                {
                   // BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                    if(GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString()  && _desktopHandler != null)
                        _desktopHandler.StartCaptering();
                    else if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString() && _webHandler != null)
                        _webHandler.StartCapturing();
                    BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                }
               

            }
        }
        #endregion
        #region "Load Events"
        private async void BDUSyncForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.KeyPreview = true;
                Button btnAutoSync = this.btn_AutoSync;
                grvSyncData.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.lblLastSyncedDateShow.Text = TimeZone.CurrentTimeZone.ToLocalTime(GlobalApp.SyncBackTime_CMS).ToString(GlobalApp.date_time_format);
                this.btn_SyncAll.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                this.btn_SyncAll.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                // udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.WAIT, "Data loading start.....");
                // tabAll.BorderStyle = BorderStyle.None;
                grvSyncData.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                grvSyncData.EnableHeadersVisualStyles = false;
                List<MappingViewModel> ls = new List<MappingViewModel>();
                grvSyncData.AutoGenerateColumns = false;
                this.ControlBox = false;
                // var l_ls = await _bduservice.getCMSData(GlobalApp.Hotel_id, GlobalApp.GetLastSyncTimeWithDifference(GlobalApp.SyncTime_CMS), (int)UTIL.Enums.STATUSES.InActive);
                // API.AIData.AddRange(l_ls);
           /*
                if (API.PRESETS.preferences != null)
                {
                    this.preferences = API.PRESETS.preferences;
                }
                if (GlobalApp.Authentication_Done && !GlobalApp.isNew && GlobalApp.Hotel_id > 0)
                {
                    // this.StartTask(null, null);
                    await fillDataGrid();
                }

                // udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, "IntegrateX is ready.");

                // AUTO ON/ OFF
                PreferenceViewModel pref = API.PRESETS.preferences.Where(x => x.key == "1").FirstOrDefault();
                if (pref != null)
                {
                    AutoSyncIndicator = Convert.ToBoolean(Convert.ToByte(pref.value));
                    if (AutoSyncIndicator)
                    {
                        btnAutoSync.Tag = "autosynon";
                        btnAutoSync.BackColor = Color.FromArgb(32, 168, 216);
                        tmrSyncData.Enabled = true;
                        tmrSyncData.Start();
                    }
                    else if (!AutoSyncIndicator)
                    {
                        tmrSyncData.Enabled = false;
                        tmrSyncData.Stop();
                        btnAutoSync.Tag = "autosynoff";
                        btnAutoSync.BackColor = Color.Gray;
                        btnAutoSync.ForeColor = Color.White;
                    }
                }
                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, "IntegrateX is ready.");
           */
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            finally {
                loadingData = false;
            }
        }
        #endregion
        #region "Task Events"       

        #endregion
        #region "Custom Methods"
        // public async Task<bool> fillDataGrid()
        //public async Task<bool> fillDataGrid(Boolean forceUpdate= false)
        //{
        //    //ServrMessageBox.ShowBox("show Grid data");
        //    this.Invoke((MethodInvoker)delegate {
        //        //this.lblLastSyncedDateShow.Text = TimeZone.CurrentTimeZone.ToLocalTime(GlobalApp.SyncBackTime_CMS).ToString(GlobalApp.date_time_format);
        //        if ((!loadingData && (GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT)) || forceUpdate)
        //        {
        //            try
        //            {
        //                grvSyncData.DataSource = null;
        //                //  lock (this)
        //                // {
        //                if (API.AIData != null)
        //                {
        //                    var rs = (from r in API.AIData
        //                                  // from f in r.forms
        //                              where r.status == (int)Enums.RESERVATION_STATUS.ACTIVE && r.entity_Id == (currentEntity == 0 ? r.entity_Id : currentEntity) && r.entity_type == (int)Enums.ENTITY_TYPES.SYNC
        //                              select new {
        //                                  uid = r.uid,
        //                                  reference = r.reference, //string.IsNullOrWhiteSpace(r.reference) || r.reference == "0" ? f.fields.OrderByDescending(t => t.is_reference).OrderBy(x => x.sr).Take(1).FirstOrDefault().default_value : r.reference,//
        //                                                           //reference=(f.fields.Where(x=>x.is_reference==1 && !string.IsNullOrWhiteSpace(x.value)).FirstOrDefault()==null?f.fields.Where(t=>!string.IsNullOrWhiteSpace(t.value)).FirstOrDefault(): f.fields.Where(x => x.is_reference == 1 && !string.IsNullOrWhiteSpace(x.value)).FirstOrDefault()).value,
        //                                  entity = r.entity_name,
        //                                  mode = r.mode == 0 ? 1 : r.mode,
        //                                  createdAt = r.createdAt.ToString("MM/dd/yyyy HH:mm:ss")
        //                              }).ToList();
        //                    grvSyncData.DataSource = rs.Distinct().OrderByDescending(x => x.createdAt).ToList();
        //                    // await fillStatsControls();
        //                    fillStatsControls();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //               // _log.Error(ex);
        //            }

        //            loadingData = false;

        //        }
        //    });
        //    return true;
        //}
        public async Task<bool> fillDataGrid(Boolean forceUpdate = false)
        {
            grvSyncData.AutoGenerateColumns= false;
            this.Invoke((MethodInvoker)delegate {
               
                if ((!loadingData && (GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT)) || forceUpdate)
                {
                    try
                    {
                        grvSyncData.DataSource = null;
                       List<SQLiteMappingViewModel> gData= SQLiteDbManager.loadSQLiteData(dtFrom:GlobalApp.CurrentDateTime.AddDays(-1), dtTo:GlobalApp.CurrentDateTime, currentEntity:(Int16)currentEntity, syncstatus:(int) BDU.UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED);
                        // {
                        if (gData != null && gData.Any())
                        {
                            var rs = (from r in gData
                                         select new {
                                          uid = r.id,
                                             entity_id = r.entity_id,
                                             reference = r.reference, //string.IsNullOrWhiteSpace(r.reference) || r.reference == "0" ? f.fields.OrderByDescending(t => t.is_reference).OrderBy(x => x.sr).Take(1).FirstOrDefault().default_value : r.reference,//
                                                                   //reference=(f.fields.Where(x=>x.is_reference==1 && !string.IsNullOrWhiteSpace(x.value)).FirstOrDefault()==null?f.fields.Where(t=>!string.IsNullOrWhiteSpace(t.value)).FirstOrDefault(): f.fields.Where(x => x.is_reference == 1 && !string.IsNullOrWhiteSpace(x.value)).FirstOrDefault()).value,
                                          entity = r.entity_name,
                                          mode = r.mode == 0 ? 1 : r.mode,
                                          createdAt = r.transactiondate.ToString("MM/dd/yyyy HH:mm:ss")
                                      }).ToList();
                            grvSyncData.DataSource = rs.Distinct().OrderByDescending(x => x.createdAt).ToList();
                            // await fillStatsControls();
                        }
                        fillReservationStatsControls(gData);
                    }
                    catch (Exception ex)
                    {
                        // _log.Error(ex);
                    }

                    loadingData = false;

                }
                this.lblLastSyncedDateShow.Text = TimeZone.CurrentTimeZone.ToLocalTime(GlobalApp.SyncBackTime_CMS).ToString(GlobalApp.date_time_format);
            });
            return true;
        }
        public async Task<bool> fillReservationStatsControls(List<SQLiteMappingViewModel> data)
        {
            if (!loadingData && (data == null || !data.Any()))
                data= SQLiteDbManager.loadSQLiteData(dtFrom: GlobalApp.CurrentDateTime.AddDays(-1), dtTo: GlobalApp.CurrentDateTime, currentEntity: (Int16)currentEntity, syncstatus: (int)BDU.UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED);

            if (!loadingData && data != null && data.Any())
            {
                int tabCountAll = data.Where(x => Convert.ToInt32(x.entity_type) == (int)Enums.ENTITY_TYPES.SYNC).Count();
                this.btn_SyncAll.Text = "All (" + tabCountAll.ToString() + " | 0)";

                int tabReservationsCount = data.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.RESERVATIONS && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
                this.btn_Reservation.Text = "Reservations (" + tabReservationsCount.ToString() + " | 0)";

                int tabBillingCount = data.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.BILLINGDETAILS && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
                this.btn_BillingDetail.Text = "Billing Details (" + tabBillingCount.ToString() + " | 0)";

                int tabCheckInCount = data.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.CHECKIN && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
                this.btn_CheckIn.Text = "Check In (" + tabCheckInCount.ToString() + " | 0)";

                int tabCheckOutCount = data.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.CHECKOUT && x.syncstatus == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
                this.btn_CheckOut.Text = "Check Out (" + tabCheckOutCount.ToString() + " | 0)";
            }
            else
            {
                this.btn_SyncAll.Text = "All (0 | 0)";
                this.btn_Reservation.Text = "Reservations (0 | 0)";
                this.btn_BillingDetail.Text = "Billing Details (0 | 0)";
                this.btn_CheckOut.Text = "Check Out (0 | 0)";
                this.btn_CheckIn.Text = "Check In (0 | 0)";
            }


            if (!loadingData && API.AIData != null && API.AIData.Any())
            {
                var stats = (from r in API.AIData
                             from f in r.forms
                             from flds in f.fields
                             where r.entity_type == (int)Enums.ENTITY_TYPES.STATS && r.status == (int)Enums.RESERVATION_STATUS.ACTIVE
                             select new { entity_id = r.entity_Id, value = flds.value }).ToList();

                if (stats != null && stats.Count > 0)
                {
                    //int tabBillingCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS).Sum(s => Convert.ToInt32(s.value));
                    //this.tabBilling.Text = "All (" + tabBillingCount.ToString() + "| 0)";

                    int spasCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.SPAS).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lblSpa_Val.Text = spasCount.ToString();
                    int conceirgeCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.CONCEIRGE).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lblConcierge_Val.Text = conceirgeCount.ToString();
                    int experienceCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.EXPERIENCES).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lbl_Experiences_Val.Text = experienceCount.ToString();
                    int restauranCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.RESTAURANS).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lbl_RestaurensVal.Text = restauranCount.ToString();
                }//if (stats != null && stats.Count > 0)
            }
            else
            {
                frmMain.lblSpa_Val.Text = (0).ToString();
                frmMain.lblConcierge_Val.Text = (0).ToString();
                frmMain.lbl_Experiences_Val.Text = (0).ToString();
                frmMain.lbl_RestaurensVal.Text = (0).ToString();
            }

            return true;

        }
        public async Task<bool> fillStatsControls()
        {

            //if (!loadingData && API.AIData != null && API.AIData.Any() )
            //{
            //    int tabCountAll = API.AIData.Where(x => Convert.ToInt32(x.entity_type) == (int)Enums.ENTITY_TYPES.SYNC && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
            //    this.btn_SyncAll.Text = "All (" + tabCountAll.ToString() + " | 0)";

            //    int tabReservationsCount = API.AIData.Where(x => Convert.ToInt32(x.entity_Id) == (int)Enums.ENTITIES.RESERVATIONS && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
            //    this.btn_Reservation.Text = "Reservations (" + tabReservationsCount.ToString() + " | 0)";

            //    int tabBillingCount = API.AIData.Where(x => Convert.ToInt32(x.entity_Id) == (int)Enums.ENTITIES.BILLINGDETAILS && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
            //    this.btn_BillingDetail.Text = "Billing Details (" + tabBillingCount.ToString() + " | 0)";

            //    int tabCheckInCount = API.AIData.Where(x => Convert.ToInt32(x.entity_Id) == (int)Enums.ENTITIES.CHECKIN && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
            //    this.btn_CheckIn.Text = "Check In (" + tabCheckInCount.ToString() + " | 0)";

            //    int tabCheckOutCount = API.AIData.Where(x => Convert.ToInt32(x.entity_Id) == (int)Enums.ENTITIES.CHECKOUT && x.status == (int)Enums.RESERVATION_STATUS.ACTIVE).Count();
            //    this.btn_CheckOut.Text = "Check Out (" + tabCheckOutCount.ToString() + " | 0)";
            //}
            //else
            //{
            //    this.btn_SyncAll.Text = "All (0 | 0)";
            //    this.btn_Reservation.Text = "Reservations (0 | 0)";
            //    this.btn_BillingDetail.Text = "Billing Details (0 | 0)";
            //    this.btn_CheckOut.Text = "Check Out (0 | 0)";
            //    this.btn_CheckIn.Text = "Check In (0 | 0)";
            //}


            if (!loadingData && API.AIData != null && API.AIData.Any())
            {
                var stats = (from r in API.AIData
                             from f in r.forms
                             from flds in f.fields
                             where r.entity_type == (int)Enums.ENTITY_TYPES.STATS && r.status == (int)Enums.RESERVATION_STATUS.ACTIVE
                             select new { entity_id = r.entity_Id, value = flds.value }).ToList();

                if (stats != null && stats.Count > 0)
                {
                    //int tabBillingCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS).Sum(s => Convert.ToInt32(s.value));
                    //this.tabBilling.Text = "All (" + tabBillingCount.ToString() + "| 0)";

                    int spasCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.SPAS).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lblSpa_Val.Text = spasCount.ToString();
                    int conceirgeCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.CONCEIRGE).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lblConcierge_Val.Text = conceirgeCount.ToString();
                    int experienceCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.EXPERIENCES).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lbl_Experiences_Val.Text = experienceCount.ToString();
                    int restauranCount = stats.Where(x => Convert.ToInt32(x.entity_id) == (int)Enums.ENTITIES.RESTAURANS).Sum(s => Convert.ToInt32(s.value));
                    frmMain.lbl_RestaurensVal.Text = restauranCount.ToString();
                }//if (stats != null && stats.Count > 0)
            }
            else
            {
                frmMain.lblSpa_Val.Text = (0).ToString();
                frmMain.lblConcierge_Val.Text = (0).ToString();
                frmMain.lbl_Experiences_Val.Text = (0).ToString();
                frmMain.lbl_RestaurensVal.Text = (0).ToString();
            }

            return true;

        }
        //private void Messagebox_Load(object sender, EventArgs e)
        #endregion
        #region "Builitin & Button Events"       
        public void udpdateBuilitin(BDU.UTIL.Enums.SYNC_MESSAGE_TYPES mtypes, string msg)
        {
            // lock (this) { 
            try
            {              
                this.Invoke((MethodInvoker)delegate {
                    this.lblUpdatesBuilitin.Text = ( mtypes == Enums.SYNC_MESSAGE_TYPES.INFO ? (string.IsNullOrWhiteSpace(GlobalApp.IX_LAST_MESSAGE)?msg: GlobalApp.IX_LAST_MESSAGE): msg);
                    switch (mtypes)
                    {
                        case Enums.SYNC_MESSAGE_TYPES.COMPLETE:
                            this.btn_PicShowMessage.BackgroundImage = servr.integratex.ui.Properties.Resources.complete_Blue;
                            this.btn_PicShowMessage.BackgroundImageLayout = ImageLayout.Stretch;
                            this.btn_PicShowMessage.FlatStyle = FlatStyle.Flat;
                            GlobalApp.IX_LAST_MESSAGE = msg;
                            WindowNotifications.windowNotification(message: msg, icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 1);
                            break;
                        case Enums.SYNC_MESSAGE_TYPES.INFO:
                            this.btn_PicShowMessage.BackgroundImage = servr.integratex.ui.Properties.Resources.info;
                            this.btn_PicShowMessage.BackgroundImageLayout = ImageLayout.Stretch;
                            this.btn_PicShowMessage.FlatStyle = FlatStyle.Flat;
                            WindowNotifications.windowNotification(message: msg, icon: servr.integratex.ui.Properties.Resources.info, notifyType: 1);
                            break;
                        case Enums.SYNC_MESSAGE_TYPES.ERROR:
                            this.btn_PicShowMessage.BackgroundImage = servr.integratex.ui.Properties.Resources.error;
                            this.btn_PicShowMessage.BackgroundImageLayout = ImageLayout.Stretch;
                            this.btn_PicShowMessage.FlatStyle = FlatStyle.Flat;
                            GlobalApp.IX_LAST_MESSAGE = msg;
                            WindowNotifications.windowNotification(message: msg, icon: servr.integratex.ui.Properties.Resources.error, notifyType: 3);
                            break;
                        case Enums.SYNC_MESSAGE_TYPES.WAIT:
                            this.btn_PicShowMessage.BackgroundImage = servr.integratex.ui.Properties.Resources.wait;
                            this.btn_PicShowMessage.BackgroundImageLayout = ImageLayout.Stretch;
                            this.btn_PicShowMessage.FlatStyle = FlatStyle.Flat;
                            GlobalApp.IX_LAST_MESSAGE = msg;
                            WindowNotifications.windowNotification(message: msg, icon: servr.integratex.ui.Properties.Resources.wait, notifyType: 2);
                            break;
                        case Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_CMS:
                        case Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS:
                            this.btn_PicShowMessage.BackgroundImage = servr.integratex.ui.Properties.Resources.wait;
                            this.btn_PicShowMessage.BackgroundImageLayout = ImageLayout.Stretch;
                            this.btn_PicShowMessage.FlatStyle = FlatStyle.Flat;
                            GlobalApp.IX_LAST_MESSAGE = msg;
                            WindowNotifications.windowNotification(message: msg, icon: servr.integratex.ui.Properties.Resources.wait, notifyType: 2);
                            break;
                    }
                });// Invoke Required
            }
            catch (Exception ex)
            {
               // _log.Error(ex);
            }
            //  }// Lock
        }
        #endregion
        #region "Grid Events & Tab Filters"  
        private void grvSyncData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (this.preferences != null)
            {
            try
            {
                DataGridViewRow dr = this.grvSyncData.Rows[e.RowIndex];
                //  foreach (DataGridViewRow dr in grvSyncData.Rows)
                // {
                if (!dr.IsNewRow && dr.Index >= 0)
                {
                        DateTime taskTime = Convert.ToDateTime(dr.Cells["TimeCol"].Value);                       
                            PreferenceViewModel preference = getPreferenceSlab(taskTime);
                    if (preference != null && preference.color != null && Indicators)
                        try
                        {
                            //Almas Comments This
                            dr.DefaultCellStyle.ForeColor = GlobalApp.Btn_ServrBlackColor;
                            //dr.DefaultCellStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(preference.color);
                        }
                        catch (Exception ex) { }
                    else
                        dr.DefaultCellStyle.ForeColor = GlobalApp.Btn_ServrBlackColor;
                }
            }
            catch (Exception) { }

            // }
        }
        }

        private void grvSyncData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    if (grvSyncData.SelectedRows.Count > 0 && GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT)
                    {
                        //grvSyncData.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        if ((GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT))
                        {
                            GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                            DataGridViewRow dRow = grvSyncData.SelectedRows[0];
                            string referenceRecord = "" + dRow.Cells["colId"].Value;
                            string entityName = "" + dRow.Cells["entityCol"].Value;
                            Int64 uid = Convert.ToInt64(dRow.Cells["srCol"].Value);
                            try
                            {
                                if (ServrMessageBox.Confirm(string.Format("Are you sure you would like to delete {0} {1}?", referenceRecord, entityName), "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                                {
                                    udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.WAIT, string.Format("Delete of reservation# {0} {1} started...", referenceRecord, entityName));
                                    //MappingViewModel mcModel = API.AIData.Where(x => x.uid == uid && x.reference == referenceRecord && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                    if (uid> 0)
                                    {                                        
                                        int deletedCount = SQLiteDbManager.updateReservationStatus(uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.DELETED);
                                        // SQLiteDbManager.purgeData(uid, GlobalApp.CurrentLocalDateTime.AddDays(-2), GlobalApp.CurrentLocalDateTime);
                                        //mcModel.status = (int)Enums.RESERVATION_STATUS.DELETED;
                                        //API.AIData.Remove(mcModel);
                                        // GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                        if (deletedCount > 0) { 
                                            fillDataGrid(true);
                                            udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation#{0} {1} has been deleted successfully.", referenceRecord, entityName));
                                    }
                                        else
                                            udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation#{0} {1} has been deleted failed.", referenceRecord, entityName));
                                    }
                                }// Confirm
                            }
                            catch (Exception ex)
                            {
                                _log.Error(ex);
                                servr.integratex.ui.ServrMessageBox.Error(string.Format("Delete failed, error - {0}", ex.Message), "Error");
                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} has not been deleted, please try again or contact: support@servrhotels.com for assistance.", referenceRecord, entityName));  //"Failed, click icon to see more details.");
                                                                                                                                                                                                                              // MessageBox.Show(("Pushed to PMS : " + e.RowIndex.ToString() + "  button Clicked : ") + e.ColumnIndex.ToString());
                            }
                        }
                    }
                    // return true;
                }
                catch (Exception ex) { _log.Error(ex); }
                finally
                {
                    GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                }
            }// Outer If Close
        }
        private void ResetDefaultSelection(Button pBtn)
        {
            try
            {
                // this.SelectedForm = pBtn.Name;           
                if (pBtn.Name == this.btn_SyncAll.Name)
                {
                    this.btn_SyncAll.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_SyncAll.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_SyncAll.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_SyncAll.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51); 
                }
                if (pBtn.Name == this.btn_Reservation.Name)
                {
                    this.btn_Reservation.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_Reservation.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_Reservation.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_Reservation.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                }
                if (pBtn.Name == this.btn_CheckIn.Name)
                {
                    this.btn_CheckIn.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_CheckIn.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_CheckIn.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_CheckIn.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                }
                if (pBtn.Name == this.btn_BillingDetail.Name)
                {
                    this.btn_BillingDetail.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_BillingDetail.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_BillingDetail.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_BillingDetail.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                }
                if (pBtn.Name == this.btn_CheckOut.Name)
                {
                    this.btn_CheckOut.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_CheckOut.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_CheckOut.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_CheckOut.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
        public async void btn_SyncAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentEntity = Convert.ToInt32(btn_SyncAll.Tag);
                await fillDataGrid();
                this.ResetDefaultSelection(sender as Button);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
        private async void btn_Reservation_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentEntity = Convert.ToInt32(btn_Reservation.Tag);
                await fillDataGrid();
                this.ResetDefaultSelection(sender as Button);
            }
            catch (Exception ex) { _log.Error(ex); }
        }
        private async void btn_CheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentEntity = Convert.ToInt32(btn_CheckIn.Tag);
                await fillDataGrid();
                this.ResetDefaultSelection(sender as Button);
            }
            catch (Exception ex) { _log.Error(ex); }
        }

        private async void btn_CheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentEntity = Convert.ToInt32(btn_CheckOut.Tag);
                await fillDataGrid();
                this.ResetDefaultSelection(sender as Button);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        private async void btn_BillingDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentEntity = Convert.ToInt32(btn_BillingDetail.Tag);
                await fillDataGrid();
                this.ResetDefaultSelection(sender as Button);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        private void grvSyncData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                try
                {
                    if (grvSyncData.Rows.Count> 0 && grvSyncData.Columns[e.ColumnIndex].Name.Equals("actionCol"))// && grvSyncData.SelectedRows.Count > 0)
                    {
                        DataGridViewRow dr = grvSyncData.Rows[e.RowIndex];

                        if (dr!= null && grvSyncData.SelectedRows.Count> 0  && grvSyncData.SelectedRows[0].Index == e.RowIndex)
                        {
                           // e.Value = servr.integratex.ui.Properties.Resources.syn_white;
                            if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                            {
                                e.Value = Properties.Resources.left_Arrow_White; //servr.integratex.ui.Properties.Resources.arrowright;
                            }
                            else if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_PMS)
                            {
                                //  gImage = servr.integratex.ui.Properties.Resources.syn_gray;
                                e.Value = Properties.Resources.right_Arrow_White;
                            }
                        }
                        else if(dr != null)
                        {
                            // e.Value = servr.integratex.ui.Properties.Resources.syn_gray;
                            if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                            {
                                e.Value = Properties.Resources.left_Arrow; //servr.integratex.ui.Properties.Resources.arrowright;
                            }
                            else if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_PMS)
                            {
                                //  gImage = servr.integratex.ui.Properties.Resources.syn_gray;
                                e.Value = Properties.Resources.right_Arrow;
                            }
                        }
                        //string name = drv.Row["Name"].ToString();
                        //if (dr != null)
                        //{
                        //    if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                        //    {
                        //        e.Value = Properties.Resources.left_Arrow_White; //servr.integratex.ui.Properties.Resources.arrowright;
                        //    }
                        //    else if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_PMS)
                        //    {
                        //        //  gImage = servr.integratex.ui.Properties.Resources.syn_gray;
                        //        e.Value = Properties.Resources.right_Arrow_White;
                        //    }
                        //}
                    }
                    // else 
                    //if (grvSyncData.Columns[e.ColumnIndex].Name.Equals("actionCol"))
                    //{
                    //    DataGridViewRow dr = grvSyncData.Rows[e.RowIndex];
                    //    if (dr != null)
                    //    {
                    //        if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                    //        {
                    //            e.Value = Properties.Resources.left_Arrow; //servr.integratex.ui.Properties.Resources.arrowright;
                    //        }
                    //        else if (Convert.ToInt16(dr.Cells["colMode"].Value) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_PMS)
                    //        {
                    //            //  gImage = servr.integratex.ui.Properties.Resources.syn_gray;
                    //            e.Value = Properties.Resources.right_Arrow;
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                }
            }

        }
        private PreferenceViewModel getPreferenceSlab(DateTime taskTime)
        {
            PreferenceViewModel pref = null;
            try
            {
                Double interval = (GlobalApp.CurrentDateTime - taskTime).TotalSeconds;

                if (this.preferences != null)
                {
                    pref = this.preferences.Where(x => Convert.ToInt32(x.key) >= 2 && Convert.ToDouble(x.value) <= interval).OrderByDescending(o => Convert.ToDouble(o.value)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return pref;
        }
        #endregion
        #region "Form Level Actions Sync All, Clear Indicators"
        private void btn_ClearIndicators_Click(object sender, EventArgs e)
        {
            Indicators = Indicators ? false : true;
            //  Apply indicator colors
            if (this.preferences != null)
            {
                foreach (DataGridViewRow dr in grvSyncData.Rows)
                {
                    if (!dr.IsNewRow && dr.Index >= 0)
                    {
                        DateTime taskTime = Convert.ToDateTime(dr.Cells["TimeCol"].Value);
                        PreferenceViewModel preference = getPreferenceSlab(taskTime);
                        if (preference != null && preference.color != null && !Indicators)
                        {
                            try
                            {
                                //Almas Comments This
                                dr.DefaultCellStyle.ForeColor = GlobalApp.Btn_ServrBlackColor;
                                //dr.DefaultCellStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml(preference.color);
                            }
                            catch (Exception ex)
                            {
                                //TODO
                            }
                        }
                        else
                            dr.DefaultCellStyle.ForeColor = GlobalApp.Btn_ServrBlackColor;
                    }
                }
            }
        }

        private async void btn_SyncAllPMS_Click(object sender, EventArgs e)
        {
            try
            {
               
                List<SQLiteMappingViewModel> IXData = SQLiteDbManager.loadSQLiteData(GlobalApp.CurrentLocalDateTime.AddDays(-1), GlobalApp.CurrentLocalDateTime);
                if (IXData != null && this.grvSyncData.RowCount > 0 )
                {
                    if (ServrMessageBox.Confirm("Are you sure to start Integrate All process?", "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                    {


                        if (API.AIData != null && API.AIData.Count > 0 && (GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT))
                        {
                                    List<SQLiteMappingViewModel> pmsls = IXData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.syncstatus == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_PMS).OrderBy(so => so.id).Take(4).ToList();
                                    List<SQLiteMappingViewModel> cmsls = IXData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.syncstatus == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_CMS).OrderBy(so => so.id).ToList();
                                    //List<MappingViewModel> pmsls = API.AIData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.status == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_PMS ).Take(4).ToList();
                                    //List<MappingViewModel> cmsls = API.AIData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.status == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_CMS).ToList();
                                    if (cmsls != null && cmsls.Any())
                            {
                                if (IXData != null && IXData.Any() && BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT)
                                {
                                    BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                                    foreach (SQLiteMappingViewModel model in IXData.Where(x => x.syncstatus == (int)BDU.UTIL.Enums.STATUSES.Active).OrderBy(o => o.entity_id).OrderBy(so => so.transactiondate))
                                    {
                                       
                                            if (model.mode == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS)
                                            {
                                                try
                                                {
                                                MappingViewModel dModel = SQLiteDbManager.loadSQLiteFullData(model.id, status: 1);
                                                // this.udpdateBuilitin(SYNC_MESSAGE_TYPES.WAIT, string.Format("Reservation# '{0}' sync with GuestX started.", model.reference));
                                                List<MappingViewModel> Datals = new List<MappingViewModel>();
                                                    Datals.Add(dModel);
                                                    ResponseViewModel cmsRes = await _bduservice.saveCMSData(BDU.UTIL.GlobalApp.Hotel_id, Datals);
                                                    if (dModel!= null && cmsRes != null && cmsRes.status)
                                                    {
                                                        //model.status = (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                                                        //API.AIData.Remove(model);
                                                    int deletedCount = SQLiteDbManager.updateReservationStatus(dModel.uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                                    //  BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                                    await fillDataGrid(true);
                                                        udpdateBuilitin(SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0} {1} integration has been completed successfully.", model.reference, model.entity_name));
                                                    if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == dModel.uid).Any())
                                                    {
                                                        API.ErrorReferences.RemoveAll(x => x.id == dModel.uid);
                                                    }
                                                }
                                                    else
                                                    {
                                                    API.ErrorReferences.Add(new ErrorViewModel {id= dModel.uid, entity_id = model.entity_id, mode = model.mode, reference = model.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                                    udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed, please try again, or contact support@servrhotels.com for assistance.", model.reference, model.entity_name));

                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    _log.Error(ex);
                                                    // servr.integratex.ui.ServrMessageBox.Error(string.Format("The integration to GuestX has failed, please try again, or contact support@servrhotels.com for assistance, error {0}", ex.Message), "Error");
                                                    udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed. please try again, or contact support@servrhotels.com for assistance.", model.reference, model.entity_name));  //"Failed, click icon to see more details.");
                                                if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id== model.id).Any())
                                                {
                                                    API.ErrorReferences.RemoveAll(x => x.id == model.id);
                                                }                                                                                                                       // MessageBox.Show(("Pushed to PMS : " + e.RowIndex.ToString() + "  button Clicked : ") + e.ColumnIndex.ToString());
                                            }
                                            }
                                        System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL-50);
                                    }

                                }
                            }//if (cmsls != null && cmsls.Any())                          
                            if (_desktopHandler != null && pmsls != null && pmsls.Any() && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                            {
                                ResponseViewModel res = null;
                                GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS;
                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS, string.Format("Integration of {0} reservation(s) started..", pmsls.Count));
                                _desktopHandler.StopCapturingWithTermination();
                                foreach (SQLiteMappingViewModel item in pmsls.OrderBy(x=>x.transactiondate))
                                {
                                    if (item.syncstatus == (int)BDU.UTIL.Enums.RESERVATION_STATUS.ACTIVE && (API.ErrorReferences == null || API.ErrorReferences.Where(x => x.id== item.id).Count() < 2))
                                    {
                                        List<MappingViewModel> mData = new List<MappingViewModel>();
                                        MappingViewModel dModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(item.id, item.entity_id);
                                        mData.Add(dModel);
                                        res = _desktopHandler.FeedDataToDesktopFormAuto(mData);
                                        if (res.status_code == ((int)Enums.ERROR_CODE.SUCCESS).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                        {
                                            
                                            if (dModel != null)
                                            {
                                                int deletedCount = SQLiteDbManager.updateReservationStatus(dModel.uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                                //mcModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                                //API.AIData.Remove(mcModel);
                                            }
                                            if (res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                            {
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);                                               
                                            }
                                            else
                                            {
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Integration of reservation# {0} {1} completed successfully.", item.reference, item.entity_name));
                                                if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == dModel.uid).Any())
                                                {
                                                    foreach (ErrorViewModel error in API.ErrorReferences) {
                                                        ErrorViewModel deleteCandidate= API.ErrorReferences.Where(x => x.id == error.id).FirstOrDefault();
                                                        if(deleteCandidate!= null)
                                                        API.ErrorReferences.Remove(deleteCandidate);
                                                    }
                                                   // API.ErrorReferences.RemoveAll(x => x.reference == item.reference && x.entity_id == item.entity_Id && x.mode == item.mode);
                                                }
                                            }
                                            // Datagrid Refresh
                                            await fillDataGrid(true);
                                        }
                                        else if (!string.IsNullOrWhiteSpace(res.message) && !res.status)
                                        {
                                            ServrMessageBox.Error(res.message, "Failed");
                                            API.ErrorReferences.Add(new ErrorViewModel {id= dModel.uid, entity_id = item.entity_id, mode = item.mode, reference = item.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                        }
                                    }
                                    System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                                }// foreach
                                _desktopHandler.StartCaptering();
                            }
                            else if (_webHandler != null && pmsls != null && pmsls.Any() && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                            {
                                ResponseViewModel res = null;
                                string currentReference = string.Empty;
                                GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS;
                                _webHandler.StopCaptering();
                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_CMS, string.Format("Integration of {0} reservation(s) started..", pmsls.Count));

                                foreach (SQLiteMappingViewModel item in pmsls.OrderBy(x => x.transactiondate))
                                {
                                    if (item.syncstatus == (int)BDU.UTIL.Enums.RESERVATION_STATUS.ACTIVE && ((API.ErrorReferences == null || API.ErrorReferences.Count<=0) || API.ErrorReferences.Where(x => x.id == item.id).Count() < 2))
                                    {
                                        List<MappingViewModel> pmsData = new List<MappingViewModel>();
                                        //pmsData.Add(item.DCopy());
                                        MappingViewModel dModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(item.id, item.entity_id);
                                        pmsData.Add(dModel);
                                        res = _webHandler.FeedAllDataToWebForm(pmsData, true);
                                        if (res.status_code == ((int)Enums.ERROR_CODE.SUCCESS).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                        {
                                            //MappingViewModel mcModel = API.AIData.Where(x => x.uid == item.uid && x.reference == item.reference && x.entity_Id== item.entity_Id && x.mode == item.mode && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                            ////_desktopHandler.StopCaptering();
                                            //mcModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            //item.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            //API.AIData.RemoveAll(x => x.uid == item.ide);
                                            //item.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            //API.AIData.Remove(item);
                                            int processedCount = SQLiteDbManager.updateReservationStatus(dModel.uid, (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                            if (res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                            {
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);
                                            }
                                            else
                                            {
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Integration of reservation# {0} {1} completed successfully.", item.reference, item.entity_name));
                                                if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == dModel.uid).Any())
                                                {

                                                    foreach (ErrorViewModel error in API.ErrorReferences)
                                                    {
                                                        ErrorViewModel deleteCandidate = API.ErrorReferences.Where(x => x.id == error.id).FirstOrDefault();
                                                        if (deleteCandidate != null)
                                                            API.ErrorReferences.Remove(deleteCandidate);
                                                    }
                                                    // API.ErrorReferences.RemoveAll(x => x.reference == item.reference && x.entity_id == item.entity_Id && x.mode == item.mode);
                                                }
                                            }
                                            await fillDataGrid(true);
                                        }
                                        else if (!string.IsNullOrWhiteSpace(res.message) && !res.status)
                                        {
                                            API.ErrorReferences.Add(new ErrorViewModel {id= dModel.uid, entity_id = item.entity_id, mode = item.mode, reference = item.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                            ServrMessageBox.Error(res.message, "Failed");
                                        }
                                    }
                                    System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                                }// foreach
                                _webHandler.intializeEventActionsData();
                            }
                        }// Condition for Already not bussy
                        else
                        {
                            ServrMessageBox.Error("The IntegrateX Integration task has failed, please try again or check your mapping settings, if issues persist, please contact: support@servrhotels.com", "Error");
                        }
                    }//if (ServrMessageBox.Confirm("Are you sure, Pushed to PMS?", "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                }// Condition for existance check
                System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
            }
            catch (Exception ex)
            {
                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("integrateX Integration task failed. Error -{0}, please try again, or contact support@servrhotels.com for assistance.", ex.Message));
            }
            finally
            {
                if (BDU.UTIL.GlobalApp.currentIntegratorXStatus != Enums.ROBOT_UI_STATUS.READY)
                {
                    BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                    if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString() && _desktopHandler != null)
                        _desktopHandler.StartCaptering();
                    else if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString() && _webHandler != null)
                        _webHandler.StartCapturing();

                    BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                }
               
                // fillDataGrid();
            }
        }
        private async void btn_AutoSync_Click(object sender, EventArgs e)
        {
            btnAutoSync = (sender as Button);
            if (!AutoSyncIndicator)
            {
                btnAutoSync.BackColor = Color.FromArgb(32, 168, 216);
                btnAutoSync.Tag = "autosynon";
                AutoSyncIndicator = true;
                tmrSyncData.Enabled = true;
                tmrSyncData.Start();
                WindowNotifications.windowNotification(message: "Auto Integration mode is turned On.", icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 1);
            }
            else if (AutoSyncIndicator)
            {
                btnAutoSync.BackColor = Color.Gray;
                btnAutoSync.ForeColor = Color.White;
                AutoSyncIndicator = false;
                tmrSyncData.Enabled = false;
                tmrSyncData.Stop();
                btnAutoSync.Tag = "autosynoff";
                WindowNotifications.windowNotification(message: "Auto Integration mode is turned OFF.", icon: servr.integratex.ui.Properties.Resources.complete_Blue, notifyType: 1);
                // return;
            }
        }
        #endregion
        private async void tmrSyncData_Tick(object sender, EventArgs e)
        {
            try
            {
                // if (AutoSyncIndicator && (BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && BDU.UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS && API.AIData != null && API.AIData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.status == 1).Any())
                if (AutoSyncIndicator && (BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT) && BDU.UTIL.GlobalApp.currentIntegratorXStatus != ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS)
                {
                    tmrSyncData.Stop();
                    tmrSyncData.Enabled = false;
                    await CMSdataSyncRunningTask();
                    tmrSyncData.Interval = BDU.UTIL.GlobalApp.AIService_Interval_Secs * 1000;
                    tmrSyncData.Enabled = true;
                    tmrSyncData.Start();
                }
            }
            catch (Exception ex) {
                if (AutoSyncIndicator)
                {
                    tmrSyncData.Enabled = true;
                    tmrSyncData.Start();
                }
            }
            
        }
        private async Task CMSdataSyncRunningTask()
        {
            if (GlobalApp.Authentication_Done && !GlobalApp.isNew && AutoSyncIndicator && BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY && GlobalApp.login_role == USERROLES.PMS_Staff)
            {
              List<SQLiteMappingViewModel> IXData= SQLiteDbManager.loadSQLiteData(GlobalApp.CurrentLocalDateTime.AddDays(-1), GlobalApp.CurrentLocalDateTime);
                if (IXData != null && IXData.Where(x=> x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.syncstatus==1).Any() && IXData.Any() && BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == ROBOT_UI_STATUS.DEFAULT)
                    {
                    try
                    {
                        List<SQLiteMappingViewModel> pmsls = IXData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.syncstatus == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_PMS).OrderBy(so => so.id).Take(3).ToList();
                        List<SQLiteMappingViewModel> cmsls = IXData.Where(x => x.entity_type == (int)Enums.ENTITY_TYPES.SYNC && x.syncstatus == (int)Enums.STATUSES.Active && x.mode == (int)Enums.SYNC_MODE.TO_CMS).OrderBy(so => so.id).Take(3).ToList();
                        if (cmsls != null && cmsls.Any())
                        {
                            BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_GUESTX;
                            foreach (SQLiteMappingViewModel model in cmsls)
                            {                               
                                try
                                {
                                    // btnAutoSync.Enabled = false;
                                    udpdateBuilitin(SYNC_MESSAGE_TYPES.WAIT, string.Format("Reservation# {0} {1} integration with GuestX started.", model.reference, ""+model.entity_name));
                                    List<MappingViewModel> Datals = new List<MappingViewModel>();
                                    MappingViewModel dmodel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(model.id, model.entity_id); ;//: (int)BDU.UTIL.Enums.STATUSES.Active);
                                    Datals.Add(dmodel);
                                    ResponseViewModel res = await _bduservice.saveCMSData(BDU.UTIL.GlobalApp.Hotel_id, Datals);
                                    if (res != null && res.status)
                                    {
                                       // model.status = (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED;
                                       // API.AIData.Remove(model);
                                      int deletedCount =  SQLiteDbManager.updateReservationStatus(model.id, (int)Enums.RESERVATION_STATUS.PROCESSED);
                                        await Task.Run(() =>
                                        {
                                            Invoke((Action)(() =>
                                            {
                                                //BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                                fillDataGrid(true);
                                               // grvSyncData.Refresh();
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# {0} {1} integration has been completed successfully.", model.reference, model.entity_name));
                                            }));

                                        });                                       
                                    }
                                  //  GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                }
                                catch (Exception ex)
                                {
                                    //btnAutoSync.Enabled = true;
                                    _log.Error(ex);
                                    // servr.integratex.ui.ServrMessageBox.Error(string.Format("The integration to GuestX has failed, please try again, or contact support@servrhotels.com for assistance, error {0}", ex.Message), "Error");
                                    udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, string.Format("Reservation# {0} {1} integration has failed. IntegrateX will try again!", model.reference, ""+model.entity_name));  //"Failed, click icon to see more details.");
                                                                                                                                                                 // MessageBox.Show(("Pushed to PMS : " + e.RowIndex.ToString() + "  button Clicked : ") + e.ColumnIndex.ToString());
                                }
                                System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                            }
                        }
                        if ((BDU.UTIL.GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || BDU.UTIL.GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT) && pmsls != null && pmsls.Any())
                        {
                            if (_desktopHandler != null && pmsls != null && pmsls.Any() && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                            {
                                string strCurrentBooking = string.Empty;
                                ResponseViewModel res = null;
                                GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS;
                                
                               _desktopHandler.StopCapturingWithTermination();
                                foreach (SQLiteMappingViewModel dModel in pmsls.OrderBy(x => x.id))
                                {
                                    List<ErrorViewModel> errorls = API.ErrorReferences;
                                    if (dModel.syncstatus == (int)BDU.UTIL.Enums.RESERVATION_STATUS.ACTIVE && ((API.ErrorReferences == null || API.ErrorReferences.Count <= 0) || API.ErrorReferences.Where(x => x.entity_id == dModel.entity_id && x.reference == dModel.reference && x.mode == dModel.mode && x.Status == 1).Count() < 2))
                                    {
                                        await Task.Run(() =>
                                        {
                                            Invoke((Action)(() =>
                                            {
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_PMS, string.Format("Reservation# {0} {1} started.", dModel.reference, dModel.entity_name));
                                            }));

                                        });
                                       
                                        strCurrentBooking = dModel.reference;
                                        List<MappingViewModel> mData = new List<MappingViewModel>();
                                       // mData.Add(dModel.DCopy());
                                        MappingViewModel fModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(dModel.id, dModel.entity_id);
                                        //fModel.uid = dModel.id;
                                        mData.Add(fModel);
                                        res = _desktopHandler.FeedDataToDesktopFormAuto(mData);
                                       
                                        if (res.status_code == ((int)Enums.ERROR_CODE.SUCCESS).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                        {
                                            //MappingViewModel fModel = API.AIData.Where(x => x.uid == dModel.uid && x.reference == mModel.reference && x.entity_Id == mModel.entity_Id && x.mode== mModel.mode &&  x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                            ////_desktopHandler.StopCaptering();

                                           // fModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            if (fModel != null)
                                            {
                                                // fModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                                //API.AIData.Remove(fModel);
                                               // int purgedCount = SQLiteDbManager.purgeData(fModel.id, GlobalApp.CurrentLocalDateTime.AddDays(-5), GlobalApp.CurrentLocalDateTime);
                                                int deletedCount = SQLiteDbManager.updateReservationStatus(fModel.uid, status: (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                            }

                                            if (res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                            {
                                                // API.ErrorReferences.Add(new ErrorViewModel { entity_id = mModel.entity_Id, mode = mModel.mode, reference = mModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);
                                            }
                                            else
                                            {
                                                                                               
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Integration of reservation#{0} {1} completed successfully.", fModel.reference, fModel.entity_name));
                                            }
                                            
                                            if (errorls != null && errorls.Where(x => x.id == fModel.uid).Any())
                                                {
                                                foreach (ErrorViewModel error in errorls.Where(x => x.id == fModel.uid))
                                                {
                                                    ErrorViewModel fError = errorls.Where(x => x.id == error.id).FirstOrDefault();
                                                    if (fError != null)
                                                    API.ErrorReferences.Remove(error);
                                                }
                                                
                                                //API.ErrorReferences.RemoveAll(x=>x.reference == mModel.reference && x.entity_id == mModel.entity_Id && x.mode == mModel.mode);
                                                }
                                            //await fillDataGrid(true);
                                            await Task.Run(() =>
                                            {
                                                Invoke((Action)(() =>
                                                {
                                                    //BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                                    fillDataGrid(true);
                                                    // grvSyncData.Refresh();
                                                    //udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# '{0}' {1} sync has been completed successfully.", model.reference, model.entity_name));
                                                }));

                                            });
                                            //grvSyncData.Refresh();
                                        }
                                        else if (!string.IsNullOrWhiteSpace(res.message) && !res.status)
                                        {
                                            API.ErrorReferences.Add(new ErrorViewModel {id= dModel.id,  entity_id = dModel.entity_id, mode = dModel.mode, reference = dModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                            udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.ERROR, res.message);
                                            
                                        }
                                        mData = null;
                                    }//// Errronous
                                    System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                                }// For Each looop
                                 //
                                 // GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                               // _desktopHandler.StartCaptering();
                            }
                            else if (_webHandler != null && pmsls != null && pmsls.Any() && GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString())
                            {
                                List<ErrorViewModel> errorls = API.ErrorReferences;
                                ResponseViewModel res = null;
                                _webHandler.StopCaptering();
                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.PUSHING_TO_CMS, string.Format("Integration of {0} reservation(s) started.", pmsls.Count));
                                foreach (SQLiteMappingViewModel dModel in pmsls.OrderBy(x => x.id))
                                {
                                    if (dModel.syncstatus == (int)BDU.UTIL.Enums.RESERVATION_STATUS.ACTIVE && ((API.ErrorReferences == null || API.ErrorReferences.Count <= 0) || API.ErrorReferences.Where(x => x.id == dModel.id).Count() < 2))
                                    {
                                        List<MappingViewModel> lsModel = new List<MappingViewModel>();
                                        MappingViewModel fModel = SQLiteDbManager.loadSQLiteFullDataWithAllFields(dModel.id, dModel.entity_id);                                        
                                        lsModel.Add(fModel);
                                        res = _webHandler.FeedAllDataToWebFormAuto(lsModel, true);
                                       
                                        if (res.status_code == ((int)Enums.ERROR_CODE.SUCCESS).ToString() || res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                        {
                                            int deletedCount = SQLiteDbManager.updateReservationStatus(fModel.uid, status: (int)BDU.UTIL.Enums.RESERVATION_STATUS.PROCESSED);
                                            //status: (int)BDU.UTIL.Enums.STATUSES.InActive
                                            //// MappingViewModel fModel = API.AIData.Where(x => x.uid == mModel.uid && x.reference == mModel.reference && x.mode == mModel.mode && x.entity_Id == mModel.entity_Id && x.status == (int)Enums.STATUSES.Active).FirstOrDefault();
                                            // //_desktopHandler.StopCaptering();

                                            //// mModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            // if (fModel != null)
                                            // {
                                            //     fModel.status = (int)Enums.RESERVATION_STATUS.PROCESSED;
                                            //     API.AIData.Remove(fModel);
                                            // }
                                            //GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                            // this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Sync of '{0}' reservation(s) with PMS Web completed successfully.", pmsls.Count()));
                                            if (res.status_code == ((int)Enums.ERROR_CODE.NO_DATA).ToString())
                                            {
                                                // API.ErrorReferences.Add(new ErrorViewModel { entity_id = mModel.entity_Id, mode = mModel.mode, reference = mModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                                udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, res.message);
                                            }
                                            else
                                            {
                                                this.udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Integration of reservation# {0} {1} completed successfully.", dModel.reference, dModel.entity_name));
                                                if (API.ErrorReferences != null && API.ErrorReferences.Where(x => x.id == fModel.uid).Any())
                                                {
                                                    foreach (ErrorViewModel error in errorls.Where(x => x.id == fModel.uid))// && x.entity_id == mModel.entity_Id && x.mode == mModel.mode))
                                                    {
                                                        ErrorViewModel fError = errorls.Where(x => x.id == error.id).FirstOrDefault();
                                                        if (fError != null)
                                                            API.ErrorReferences.Remove(error);
                                                    }
                                                    // API.ErrorReferences.RemoveAll(x => x.reference == mModel.reference && x.entity_id == mModel.entity_Id && x.mode == mModel.mode);
                                                }
                                            }
                                            // await  fillDataGrid(true);
                                            await Task.Run(() =>
                                            {
                                                Invoke((Action)(() =>
                                                {
                                                    //BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                                    fillDataGrid(true);
                                                    // grvSyncData.Refresh();
                                                   // udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.COMPLETE, string.Format("Reservation# '{0}' {1} sync has been completed successfully.", model.reference, model.entity_name));
                                                }));

                                            });
                                            // grvSyncData.Refresh();
                                        }
                                        else if (!string.IsNullOrWhiteSpace(res.message) && !res.status)
                                        {
                                            API.ErrorReferences.Add(new ErrorViewModel {id= dModel.id, entity_id = dModel.entity_id, mode = dModel.mode, reference = dModel.reference, hotel_id = GlobalApp.Hotel_id, Status = (int)BDUUtil.COMMON_STATUS.ACTIVE, time = System.DateTime.Now });
                                            udpdateBuilitin(Enums.SYNC_MESSAGE_TYPES.INFO, string.Format(GlobalApp.RESERVATION_NOT_FOUND_IN_PMS, dModel.reference));
                                        }
                                        lsModel = null;
                                    }// Errronous
                                    System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);
                                }// For Each looop
                               // GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                                _webHandler.intializeEventActionsData();
                            }
                        }
                        // btnAutoSync.Enabled = true;
                        System.Threading.Thread.Sleep(BDU.UTIL.BDUConstants.AUTO_SYNC_PROCESS_WAIT_INTERVAL);

                    }
                catch (Exception ex) { _log.Error(ex); }
                finally
                {
                   // if (BDU.UTIL.GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.SYNCHRONIZING_WITH_PMS )
                   // {
                        BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                        if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Desktop).ToString() && _desktopHandler != null) {

                            await Task.Run(() =>
                            {
                                Invoke((Action)(() =>
                                {
                                    _desktopHandler.StartCaptering();
                                }));

                            });
                        }
                           
                        else if (GlobalApp.SYSTEM_TYPE == ((int)Enums.PMS_VERSIONS.PMS_Web).ToString() && _webHandler != null)
                            _webHandler.StartCapturing();

                        BDU.UTIL.GlobalApp.currentIntegratorXStatus = Enums.ROBOT_UI_STATUS.READY;
                        //}
                    }
                }//  if (API.AIData != null && API.AIData.Any() && BDU.UTIL.GlobalApp.currentIntegratorXStatus
            }
            // await Task.Delay(30000);
        }

        private void btnReservationDetail_Click(object sender, EventArgs e)
        {
            BDUReservationDetail frm = new BDUReservationDetail();
            frm.ShowDialog();
        }

        private void grvSyncData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 )
            {
                DataGridViewRow dRow = grvSyncData.Rows[e.RowIndex];
                if (dRow != null &&  !string.IsNullOrWhiteSpace("" + dRow.Cells["srCol"].Value))
                {
                    
                        Int64 UniqueId = Convert.ToInt64("" + dRow.Cells["srCol"].Value); // Unique Id
                    //string referenceRecord = "" + dRow.Cells["colId"].Value;
                    //string entityname = ""+dRow.Cells["entityCol"].Value;
                    //int mode = Convert.ToInt16(dRow.Cells["colMode"].Value);
                    SQLiteMappingViewModel mapping = SQLiteDbManager.loadSQLiteDataDetailed(UniqueId);//.Where(x => x.mode == mode && x.reference == referenceRecord && x.entity_type == (int)BDU.UTIL.Enums.ENTITY_TYPES.SYNC && x.entity_name == entityname).FirstOrDefault();
                                                                                                                                            // MappingViewModel mapping = SQLiteDbManager.loadSQLiteFullData(UniqueId, (int)BDU.UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED);//.Where(x => x.mode == mode && x.reference == referenceRecord && x.entity_type == (int)BDU.UTIL.Enums.ENTITY_TYPES.SYNC && x.entity_name == entityname).FirstOrDefault();
                    if (mapping != null && (!string.IsNullOrWhiteSpace(mapping.reference) || !string.IsNullOrWhiteSpace(mapping.roomno)))
                    {
                        BDUReservationDetail detail = new BDUReservationDetail();
                        detail.SqlModel = mapping;
                        detail.Show();
                        mapping = null;
                    }
                }

            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            BDUHistoryForm frm = new BDUHistoryForm();
            frm.ShowDialog();
        }
    }
}
