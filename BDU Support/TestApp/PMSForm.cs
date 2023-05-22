using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinFormTestApp;
using BDU.UTIL;

namespace WindowsFormsTestApplication
{
    public partial class PMSForm : Form
    {
        public PMSForm()
        {
            InitializeComponent();
            LoadData();
            EnableFrom(this, true);
        }
        private async void LoadData()
        {
            // var hotelList = await new BDUService().getHotelslist(0, 0);
            List<hotels> htl = new List<hotels>();
            htl.Add(new hotels { id = 1, name = "Servr1", sclass = "5th", category="ZAHID Rasheed",created_at=DateTime.Now });
            htl.Add(new hotels { id = 2, name = "Al-Madina", sclass = "5th", category = "ZAHID Sal", created_at = DateTime.Now });
            htl.Add(new hotels { id = 3, name = "Suriyah", sclass = "5th", category = "ZAHID Salnakia", created_at = DateTime.Now });
            htl.Add(new hotels { id = 4, name = "Haki Badr", sclass = "6th", category = "ZAHID Gulfam Rashedd", created_at = DateTime.Now });
            hotel_id.DataSource = htl;
            hotel_id.DisplayMember = "hotel_name";
            hotel_id.ValueMember = "id";
            List<students> studs = new List<students>();
            studs.Add(new students { id = 1, name = "Saleh Abdullah", sclass = "5th", category = "ZAHID Salnakia",standards="Unique" ,created_at = DateTime.Now , Description="Already Availed Session"});
            studs.Add(new students { id = 2, name = "Maria Abdullah",sclass="5th", category = "Zubaida Salnakia", standards = "Alpina", created_at = DateTime.Now, Description = "Already Availed Session" });
            studs.Add(new students { id = 3, name = "Qurash Abdullah", sclass = "5th", category = "Abid Salnakia", standards = "Universal", created_at = DateTime.Now, Description = "Already Availed Session" });
            studs.Add(new students { id = 4, name = "Shujah Badr", sclass = "6th", category = "ZAHID Fareed", standards = "Cristo", created_at = DateTime.Now, Description = "Already Availed Session" });
            uGrid.DataSource = studs;
            

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Controls.ClearControls();
           // EnableFrom(this, true);
        }

        public void EnableFrom(Form form, bool enable)
        {
            foreach (Control c in ((Control)this).Controls)
            {
                c.Enabled = enable;
            }
            btnCancel.Enabled = true;
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
            var msg = this.ValidateForm();
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
                EnableFrom(this, false);
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
    }
    public class hotels
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public string sclass { get; set; }
        public string category { get; set; }
        public string standards { get; set; }
        public DateTime created_at { get; set; } = System.DateTime.Now;        

    }
    public class students
    {
        public int id { get; set; } = 0;
        public string name { get; set; }
        public string sclass { get; set; }
        public string category { get; set; }
        public string standards { get; set; }
        public string Description { get; set; }
        public DateTime created_at { get; set; } = System.DateTime.Now;

    }
}
