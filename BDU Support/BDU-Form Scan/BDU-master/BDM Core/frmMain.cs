
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
namespace bdu
{
    public partial class frmMain : Form
    {
        Form activeForm;
        private List<string> OpenForms = new List<string>();
        public string SelectedForm = "btn_BDUSync";


        public frmMain()
        {
            InitializeComponent();
            btn_BDUSync.MouseEnter += new EventHandler(btnSync_MouseEnter);
            btn_BDUSync.MouseLeave += new EventHandler(btnSync_MouseLeave);

            btn_Mapping.MouseEnter += new EventHandler(btnMapping_MouseEnter);
            btn_Mapping.MouseLeave += new EventHandler(btnMapping_MouseLeave);

            btn_Clone.MouseEnter += new EventHandler(btnClone_MouseEnter);
            btn_Clone.MouseLeave += new EventHandler(btnClone_MouseLeave);

            btnPreference.MouseEnter += new EventHandler(btnPreference_MouseEnter);
            btnPreference.MouseLeave += new EventHandler(btnPreference_MouseLeave);

            btn_Logout.MouseEnter += new EventHandler(btnLogOut_MouseEnter);
            btn_Logout.MouseLeave += new EventHandler(btnLogOut_MouseLeave);

            btnDemo.MouseEnter += new EventHandler(btnTutorial_MouseEnter);
            btnDemo.MouseLeave += new EventHandler(btnTutorial_MouseLeave);
        }

        void btnSync_MouseEnter(object sender, EventArgs e)
        {
            this.btn_BDUSync.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnSync_MouseLeave(object sender, EventArgs e)
        {
            this.btn_BDUSync.BackgroundImage = null;
        }
        void btnMapping_MouseEnter(object sender, EventArgs e)
        {
            this.btn_Mapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnMapping_MouseLeave(object sender, EventArgs e)
        {
            this.btn_Mapping.BackgroundImage = null;
        }
        void btnClone_MouseEnter(object sender, EventArgs e)
        {
            this.btn_Clone.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnClone_MouseLeave(object sender, EventArgs e)
        {
            this.btn_Clone.BackgroundImage = null;
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
            this.btn_Logout.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ButtonRound));
        }
        void btnLogOut_MouseLeave(object sender, EventArgs e)
        {
            this.btn_Logout.BackgroundImage = null;
        }
        private void switchForm(Form frm)
        {
            if (OpenForms.Count() == 1)
            {
                MessageBox.Show(string.Format("Form is already opened, first close the form.!", frm.Text.Trim() == string.Empty ? frm.Name : frm.Text), "Warning");
                return;
            }
            OpenForms.Add(frm.Name);
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.FormClosed += (sender, e) => { OpenForms.Remove(frm.Name); };
            frm.Show();
            activeForm = frm;
        }       
        private void btn_Clone_Click(object sender, EventArgs e)
        {
            BDUMappingForm frm = new BDUMappingForm();
            switchForm(frm);
        }       
        private void btn_BDUSync_Click(object sender, EventArgs e)
        {
            BDUSyncForm frm = new BDUSyncForm();
            switchForm(frm);
        }
        private void btn_Mapping_Click(object sender, EventArgs e)
        {
            BDUMappingForm frm = new BDUMappingForm();
            switchForm(frm);
        }  
        private void btnPreference_Click(object sender, EventArgs e)
        {
            BDUPreferencesForm frm = new BDUPreferencesForm();
            switchForm(frm);
        }
        private void btn_Logout_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure, want to logout?", "Confirmation!", MessageBoxButtons.YesNoCancel,        MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            PlaceLowerRight();
            int x = 10;

            switch (SelectedForm)
            {
                case "btnCenter_Sync":
                    this.btn_BDUSync_Click(sender, null);// Console.WriteLine("Value of x is 5");
                    break;
                case "btnSync":
                    this.btn_BDUSync_Click(sender, null);// Console.WriteLine("Value of x is 5");
                    break;
                case "btnCenter_Mapping":
                    this.btn_Mapping_Click(sender, null);
                    break;
                case "btnMapping":
                    this.btn_Mapping_Click(sender, null);// Console.WriteLine("Value of x is 5");
                    break;
                case "btnCenter_Clone":
                    this.btn_Mapping_Click(sender, null);
                    break;
                case "btnClone":
                    this.btn_Mapping_Click(sender, null);
                    break;
                case "btnCenter_Preference":
                    this.btnPreference_Click(sender, null);
                    break;
                case "btnPreference":
                    this.btnPreference_Click(sender, null);
                    break;
                case "btnCenter_LogOut":
                    this.btn_Logout_Click(sender, null);
                    break;
                default:
                    Console.WriteLine("Unknown value");
                    break;
            }
            MdiClient ctlMDI;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctlMDI = (MdiClient)ctl;
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }            
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

            this.Left = rightmost.WorkingArea.Right - this.Width+5;
            this.Top = rightmost.WorkingArea.Bottom - this.Height+5;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void btnTutorial_Click(object sender, EventArgs e)
        {

        }
    }
}

