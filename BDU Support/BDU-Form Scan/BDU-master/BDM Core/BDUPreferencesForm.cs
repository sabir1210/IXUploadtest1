using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bdu
{
    public partial class BDUPreferencesForm : Form
    {
        string apiUrl = System.Configuration.ConfigurationManager.AppSettings["apiUrl"];
        string securityToken = System.Configuration.ConfigurationManager.AppSettings["securityToken"];
        public BDUPreferencesForm()
        {
            InitializeComponent();
            btnSave.MouseEnter += new EventHandler(btnSave_MouseEnter);
            btnSave.MouseLeave += new EventHandler(btnSave_MouseLeave);

            btnCancel.MouseEnter += new EventHandler(btnCancel_MouseEnter);
            btnCancel.MouseLeave += new EventHandler(btnancel_MouseLeave);

            btnTestCall.MouseEnter += new EventHandler(btnTestRun_MouseEnter);
            btnTestCall.MouseLeave += new EventHandler(btnTestRun_MouseLeave);
            this.txtApiUrl.Text = apiUrl;
            this.txtToken.Text = securityToken;
        }
        void btnSave_MouseEnter(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnCancelHvr));
        }
        void btnancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnCancel)); ;
        }
        void btnTestRun_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestCall.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnTestRun_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestCall.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grp_APISettings_Configuration_Enter(object sender, EventArgs e)
        {

        }
    }
}
