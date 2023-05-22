using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using BDU.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDU.UTIL;

namespace servr.integratex.ui
{
    public partial class BDUReservationDetail : Form
    {
        // public MappingViewModel objMapping = new MappingViewModel();
         public SQLiteMappingViewModel SqlModel = new SQLiteMappingViewModel();
        public BDUReservationDetail()
        {
            InitializeComponent();
        }       

        private void BDUReservationDetail_Load(object sender, EventArgs e)
        {
            try
            {
                PlaceLowerRight();

                //******************  Show Reservation data here*************************//
                this.lblConfirmationNo.Text = SqlModel.reference;
                if (SqlModel != null)
                {
                    
                    this.lblArrivalDate.Text = SqlModel.arrivaldate.ToString("MM/dd/yyyy");//.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault() == null ? "" : string.IsNullOrWhiteSpace("" + objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value) ? "" : Convert.ToDateTime(objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value).ToString("MM/dd/yyyy");
                    this.lblDepartureDate.Text = SqlModel.departuredate.ToString("MM/dd/yyyy");
                    this.lblPaymentAmount.Text = ""+(SqlModel.paymentamount<=0?"": " Paid: "+ Math.Round(SqlModel.paymentamount,2).ToString()) +" "+ (SqlModel.payableamount <= 0 ? "" : " Payable: " + Math.Round(SqlModel.payableamount, 2).ToString()) ;                   
                    this.lblRoomNo.Text = SqlModel.roomno.Contains("$") ? "" : SqlModel.roomno;  //string.IsNullOrWhiteSpace(objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value) ? objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault().value : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value;
                    this.lblName.Text = SqlModel.firstname;//.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("name")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("name")).FirstOrDefault().value;
                    this.lblLastName.Text = SqlModel.lastname;// objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault().value;
                    this.lblEmail.Text = SqlModel.email;// objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault().value;
                    this.lblRecievedFrom.Text = SqlModel.mode == (int)BDU.UTIL.Enums.SYNC_MODE.TO_CMS ? "PMS" : "GuestX";
                    this.lblEntityName.Text = SqlModel.entity_name + (SqlModel.undo<=0?"":" - Cancel");
                    // this.lbl.Text = objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault().value;
                    this.lblBookingRef.Text = SqlModel.reference;
                    this.lblVoucherNo.Text = SqlModel.voucherno;//.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault() == null ? "" : objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;  //objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;
                }
            }
            catch (Exception ex) { }
        }
        private void PlaceLowerRight()
        {
            Screen screen = Screen.FromControl(this);
            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) - 438),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) - 200)
            };
            //this.Left = rightmost.WorkingArea.Right - this.Width;
            //this.Top = rightmost.WorkingArea.Bottom - this.Height - 12;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Are you sure, want to close?", "Confirmation!");
            if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                this.Close();
            }
        }

        private void btn_MainMenuClose_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Are you sure, want to close?", "Confirmation!");
            if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                this.Close();
            }
        }
    }
}
