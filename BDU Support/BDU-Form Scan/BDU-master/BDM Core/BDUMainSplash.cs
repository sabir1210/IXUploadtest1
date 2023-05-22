using bdu.Properties;
using System;
using System.Collections.Generic;
using DBU.ViewModels;
using System.Linq;

using System.Windows.Forms;

namespace bdu
{
    public partial class BDUMainSplash : Form
    {
        Form activeForm;
        private List<string> OpenForms = new List<string>();
        HotelViewModel HotelViewModel = new DBU.ViewModels.HotelViewModel();
        public BDUMainSplash()
        {
           
            InitializeComponent();
            btnSync.MouseEnter += new EventHandler(btnSync_MouseEnter);
            btnSync.MouseLeave += new EventHandler(btnSync_MouseLeave);            

            btnMapping.MouseEnter += new EventHandler(btnMapping_MouseEnter);
            btnMapping.MouseLeave += new EventHandler(btnMapping_MouseLeave);

            btnClone.MouseEnter += new EventHandler(btnClone_MouseEnter);
            btnClone.MouseLeave += new EventHandler(btnClone_MouseLeave);

            btnPreference.MouseEnter += new EventHandler(btnPreference_MouseEnter);
            btnPreference.MouseLeave += new EventHandler(btnPreference_MouseLeave);
            
            btnLogOut.MouseEnter += new EventHandler(btnLogOut_MouseEnter);
            btnLogOut.MouseLeave += new EventHandler(btnLogOut_MouseLeave);

            btnCenter_Sync.MouseEnter += new EventHandler(btnCenter_Sync_MouseEnter);
            btnCenter_Sync.MouseLeave += new EventHandler(btnCenter_Sync_MouseLeave);

            btnCenter_Mapping.MouseEnter += new EventHandler(btnCenter_Mapping_MouseEnter);
            btnCenter_Mapping.MouseLeave += new EventHandler(btnCenter_Mapping_MouseLeave);

            btnCenter_Clone.MouseEnter += new EventHandler(btnCenter_Clone_MouseEnter);
            btnCenter_Clone.MouseLeave += new EventHandler(btnCenter_Clone_MouseLeave);

            btnCenter_Preference.MouseEnter += new EventHandler(btnCenter_Preference_MouseEnter);
            btnCenter_Preference.MouseLeave += new EventHandler(btnCenter_Preference_MouseLeave);
            
            btnCenter_LogOut.MouseEnter += new EventHandler(btnCenter_LogOut_MouseEnter);
            btnCenter_LogOut.MouseLeave += new EventHandler(btnCenter_LogOut_MouseLeave);

            btnDemo.MouseEnter += new EventHandler(btnTutorial_MouseEnter);
            btnDemo.MouseLeave += new EventHandler(btnTutorial_MouseLeave);
            this.lblDate.Text = System.DateTime.Today.ToString("MM/dd/yyyy");
            this.lblHotelName.Text = HotelViewModel.hotel_name;
        }

        void btnSync_MouseEnter(object sender, EventArgs e)
        {
            this.btnSync.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnSync_MouseLeave(object sender, EventArgs e)
        {
            this.btnSync.BackgroundImage = null;
        }              
        void btnMapping_MouseEnter(object sender, EventArgs e)
        {
            this.btnMapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnMapping_MouseLeave(object sender, EventArgs e)
        {
            this.btnMapping.BackgroundImage = null;
        }
        void btnClone_MouseEnter(object sender, EventArgs e)
        {
            this.btnClone.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnClone_MouseLeave(object sender, EventArgs e)
        {
            this.btnClone.BackgroundImage = null;
        }
        void btnPreference_MouseEnter(object sender, EventArgs e)
        {
            this.btnPreference.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnPreference_MouseLeave(object sender, EventArgs e)
        {
            this.btnPreference.BackgroundImage = null;
        }
        void btnTutorial_MouseEnter(object sender, EventArgs e)
        {
            this.btnDemo.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnTutorial_MouseLeave(object sender, EventArgs e)
        {
            this.btnDemo.BackgroundImage = null;
        }
        void btnLogOut_MouseEnter(object sender, EventArgs e)
        {
            this.btnLogOut.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnLogOut_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogOut.BackgroundImage = null;
        }

        void btnCenter_Sync_MouseEnter(object sender, EventArgs e)
        {
            this.btnCenter_Sync.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonHover));
        }
        void btnCenter_Sync_MouseLeave(object sender, EventArgs e)
        {
            this.btnCenter_Sync.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Button));
        }

        void btnCenter_Mapping_MouseEnter(object sender, EventArgs e)
        {
            this.btnCenter_Mapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonHover));
        }
        void btnCenter_Mapping_MouseLeave(object sender, EventArgs e)
        {
            this.btnCenter_Mapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Button));
        }
        void btnCenter_Clone_MouseEnter(object sender, EventArgs e)
        {
            this.btnCenter_Clone.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonHover));
        }
        void btnCenter_Clone_MouseLeave(object sender, EventArgs e)
        {
            this.btnCenter_Clone.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Button));
        }
        void btnCenter_Preference_MouseEnter(object sender, EventArgs e)
        {
            this.btnCenter_Preference.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonHover));
        }
        void btnCenter_Preference_MouseLeave(object sender, EventArgs e)
        {
            this.btnCenter_Preference.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Button));
        }
        void btnCenter_LogOut_MouseEnter(object sender, EventArgs e)
        {
            this.btnCenter_LogOut.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonHover));
        }
        void btnCenter_LogOut_MouseLeave(object sender, EventArgs e)
        {
            this.btnCenter_LogOut.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Button));
        }


        private void BDUMainSplash_Load(object sender, EventArgs e)
        {
            PlaceLowerRight();
        }
        private void switchForm(Form frm)
        {
            if (OpenForms.Count() == 1)
            {
                MessageBox.Show(string.Format("Form is already opened, first close the form.!", frm.Text.Trim() == string.Empty ? frm.Name : frm.Text), "Warning");
                return;
            }
            OpenForms.Add(frm.Name);
            frm.FormClosed += (sender, e) => { OpenForms.Remove(frm.Name); };
            frm.Show();
            activeForm = frm;
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
            this.Left = rightmost.WorkingArea.Right - this.Width + 5;
            this.Top = rightmost.WorkingArea.Bottom - this.Height + 5;
        }            
        private void btnCenter_Sync_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }

        private void btnCenter_Clone_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }
        private void btnCenter_Mapping_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }
        private void btnCenter_Preference_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }
        private void btnCenter_LogOut_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();
            frm.SelectedForm = ((Button)sender).Name;
            switchForm(frm);
        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void btnTutorial_Click(object sender, EventArgs e)
        {           
            var uri = "https://www.youtube.com/watch?v=zblQdr-Qd1c";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void btnCenter_LogOut_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, want to logout?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, want to logout?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

       
    }
}
