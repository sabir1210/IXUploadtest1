using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;

namespace servr.integratex.ui
{
    public partial class BDUHistoryForm : Form
    {
        #region "Variables & Constructors"
        List<SQLiteMappingViewModel> hData = null;
        public BDUHistoryForm()
        {
            InitializeComponent();
            grvHistoryData.AutoGenerateColumns = false;
        }
        #endregion
        #region "Form level mthods"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Are you sure, want to close?", "Confirmation!");
            //if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            //{
                this.Close();
           // }
        }

        private void BDUHistoryForm_Load(object sender, EventArgs e)
        {
            PlaceLowerRight();
            //grvHistoryData.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grvHistoryData.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
            grvHistoryData.EnableHeadersVisualStyles = false;
            grvHistoryData.AutoGenerateColumns = false;

            if (( (GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT)))
            {
                try
                {
                    grvHistoryData.AutoGenerateColumns = false;
                    grvHistoryData.DataSource = null;
                    hData = SQLiteDbManager.loadSQLiteHistoryData(dtFrom: GlobalApp.CurrentDateTime.AddDays(-30), dtTo: GlobalApp.CurrentDateTime);
                    // {
                    if (hData != null && hData.Any())
                    {
                        var rs = (from r in hData
                                  where r.syncstatus == (tgl_Active_Inactive.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON ? 1 : r.syncstatus)
                                  select new {
                                      uid = r.id,
                                      reference = r.reference,
                                      entity = r.entity_name + (r.undo <= 0 ? "" : "-Undo"),
                                      guestname = r.lastname + " " + r.firstname,
                                      Status = StringValueOf((Enums.RESERVATION_STATUS)r.syncstatus),
                                      RoomNo = r.roomno,
                                      mode = (r.mode == 0 ? 1 : r.mode) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS ? "PMS" : "GuestX",
                                      TransactionDate = r.transactiondate.ToString("MM/dd/yyyy HH:mm:ss"),
                                     // DepartureDate = r.departuredate.ToString("MM/dd/yyyy"),
                                      ArrivalDate = r.arrivaldate.ToString("MM/dd/yyyy")

                                  }).ToList();
                        grvHistoryData.DataSource = rs.Distinct().OrderByDescending(x => x.TransactionDate).ToList();
                        this.lblRecords.Text = string.Format("Records : {0}", rs.Count().ToString());
                    }
                    
                }
                catch (Exception ex)
                {
                    // _log.Error(ex);
                }

                

            }
            
        }
        #endregion
        #region "Custom methods"
        public static string StringValueOf(Enum value)
        {
            try
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
            catch (Exception ex) {
                return "";
            }
        }
        private void PlaceLowerRight()
        {
            Screen screen = Screen.FromControl(this);
            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) - 423),                
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) - 200)
            };
        }
        #endregion
        #region "Control events"
        private void txtKeywordSearch_TextChanged(object sender, EventArgs e)
        {
            grvHistoryData.DataSource = null;
            grvHistoryData.AutoGenerateColumns = false;
            this.lblRecords.Text = string.Format("Records : {0}", Convert.ToString(0));
            if (hData != null && hData.Any())
            {
                if (hData != null && hData.Any())
                {
                    var rs = (from r in hData
                              where r.syncstatus == (tgl_Active_Inactive.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON ? 1: r.syncstatus )
                              && (Convert.ToString(r.reference).ToLower().Contains(this.txtKeywordSearch.Text.ToLower()) || Convert.ToString(r.voucherno).ToLower().Contains(this.txtKeywordSearch.Text.ToLower()) || ("" + r.lastname).ToLower().Contains(this.txtKeywordSearch.Text.ToLower()) || ("" + r.firstname).ToLower().Contains(this.txtKeywordSearch.Text.ToLower()) || (""+r.roomno).ToLower().Contains(this.txtKeywordSearch.Text.ToLower()))
                              select new {
                                  uid = r.id,
                                  reference = r.reference,
                                  entity = r.entity_name,
                                  guestname = r.lastname + " " + r.firstname,
                                  Status = StringValueOf((Enums.RESERVATION_STATUS)r.syncstatus),
                                  RoomNo = r.roomno,
                                  mode = (r.mode == 0 ? 1 : r.mode) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS ? "PMS" : "GuestX",
                                  TransactionDate = r.transactiondate.ToString("MM/dd/yyyy HH:mm:ss"),
                                  //DepartureDate = r.departuredate.ToString("MM/dd/yyyy"),
                                  ArrivalDate = r.arrivaldate.ToString("MM/dd/yyyy")
                              }).ToList();
                    grvHistoryData.DataSource = rs.Distinct().OrderByDescending(x => x.TransactionDate).ToList();
                    this.lblRecords.Text = string.Format("Records : {0}", rs.Count().ToString());
                }
            }
        }

        private void tgl_Active_Inactive_ButtonStateChanged(object sender, BDU.Extensions.ToggleButton.ToggleButtonStateEventArgs e)
        {
            if (((GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.READY || GlobalApp.currentIntegratorXStatus == Enums.ROBOT_UI_STATUS.DEFAULT)))
            {
                try
                {
                    grvHistoryData.AutoGenerateColumns = false;
                    grvHistoryData.DataSource = null;
                    // hData = SQLiteDbManager.loadSQLiteHistoryData(dtFrom: GlobalApp.CurrentDateTime.AddDays(-30), dtTo: GlobalApp.CurrentDateTime);
                    // {
                    if (hData != null && hData.Any())
                    {
                        var rs = (from r in hData
                                  where r.syncstatus == (tgl_Active_Inactive.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON ? r.syncstatus : 1)
                                  select new {
                                      uid = r.id,
                                      reference = r.reference,
                                      entity = r.entity_name,
                                      guestname = r.lastname + " " + r.firstname,
                                      Status = StringValueOf((Enums.RESERVATION_STATUS)r.syncstatus),
                                      RoomNo = r.roomno,
                                      mode = (r.mode == 0 ? 1 : r.mode) == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS ? "PMS" : "GuestX",
                                      TransactionDate = r.transactiondate.ToString("MM/dd/yyyy HH:mm:ss"),
                                      // DepartureDate = r.departuredate.ToString("MM/dd/yyyy"),
                                      ArrivalDate = r.arrivaldate.ToString("MM/dd/yyyy")

                                  }).ToList();
                        grvHistoryData.DataSource = rs.Distinct().OrderByDescending(x => x.TransactionDate).ToList();
                        this.lblRecords.Text = string.Format("Records : {0}", rs.Count().ToString());
                    }

                }
                catch (Exception ex)
                {
                    // _log.Error(ex);
                }



            }

        }
        #endregion
    }
}
