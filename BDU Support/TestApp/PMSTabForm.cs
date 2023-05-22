using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormTestApp;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;

namespace WindowsFormsTestApplication
{
    public partial class PMSTabForm : Form
    {
        public List<HotelViewModel> hList= new List<HotelViewModel>();
         BDUService _service = new BDUService();
        public PMSTabForm()
        {
            InitializeComponent();
            LoadData();
            //EnableFrom(this, false);
        }
        private async void LoadData()
        {    
            hList.Add(new HotelViewModel { id = 0, hotel_name = "--Select Hotel--", version = "Desktop V1.0" });
            hList.Add(new HotelViewModel { id = 1, hotel_name = "Servr", version = "Desktop V1.0" });
            hList.Add(new HotelViewModel { id = 2, hotel_name = "Agus", version = "Desktop V1.0" });
            hotel_id.DataSource = hList;
                    hotel_id.DisplayMember = "hotel_name";
                    hotel_id.ValueMember = "id"; 
          
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Controls.ClearControls();
            EnableFrom(this, true);
        }

        public void EnableFrom(Form form, bool enable)
        {
            foreach (Control c in ((Control)this).Controls)
            {
                c.Enabled = enable;
            }
            btnCancel.Enabled = true;
            pageCard.Enabled = true;
            pageInfo.Enabled = true;
            //tabData.Enabled = true;
        }

        private string ValidateForm()
        {
            var errMsg = "";
            if (reference.Text == "")
            {
                errMsg += "Reference\n\r";
            }
            if (hotel_id.SelectedIndex < 0)
            {
                errMsg += "Hotel\n\r";
            }
            if (arrival_date.Value.ToString() == "")
            {
                errMsg += "Arrival Date\n\r";
            }
            if (phone_number.Text == "")
            {
                errMsg += "Phone Number\n\r";
            }
            if (room_number.Text == "")
            {
                errMsg += "Room Number\n\r";
            }
            if (status.Text == "")
            {
                errMsg += "Status\n\r";
            }
            if (errMsg != "")
                errMsg = "Follfowing fields are missing\n\r" + errMsg;
            return errMsg;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var msg = "";// ValidateForm();
            if (msg != "")
            {
                MessageBox.Show(
                    msg,
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    (MessageBoxOptions)0x40000);
            }
            else
            {
               // EnableFrom(this, false);
                MessageBox.Show(
                    "Form Submitted",
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    (MessageBoxOptions)0x40000);
                //MessageBox.Show(this, "Form Submitted");
            }  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
    }
}
