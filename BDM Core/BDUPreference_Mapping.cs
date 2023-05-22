using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDU.RobotDesktop;
using BDU.RobotWeb;

namespace servr.integratex.ui
{
    public partial class BDUPreference_Mapping : System.Windows.Forms.Form
    {
        public DesktopHandler _desktopHandler = null;
        public WebHandler _WebHandler = null;
        private object _log;
        private Int32 currentEntity = 0;// All
        public BDUPreference_Mapping()
        {
            InitializeComponent();
        }
        private void btn_Preference_Click(object sender, EventArgs e)
        {
            this.currentEntity = Convert.ToInt32(btn_Preference.Tag);           
            this.ResetDefaultSelection(sender as Button);
            mappingForm.Hide();
            preferenceSettingcs.Show();
            preferenceSettingcs.BringToFront();
            this.ResetDefaultSelection(sender as Button);
        }

        private void BDUTabControl_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.KeyPreview = true;
            preferenceSettingcs.Show();
            mappingForm.Hide();           
            this.btn_Preference.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
            //this.btn_Preference.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
            //this.btn_Mapping.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
            //this.btn_Mapping.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
        }

        private void btn_Mapping_Click(object sender, EventArgs e)
        {
            preferenceSettingcs.Hide();
            mappingForm._desktopHandler = this._desktopHandler;
            mappingForm._WebHandler = this._WebHandler;
            mappingForm.Show();
            mappingForm.BringToFront();
            this.ResetDefaultSelection(sender as Button);
        }
        private void ResetDefaultSelection(Button pBtn)
        {
            try
            {
                // this.SelectedForm = pBtn.Name;           
                if (pBtn.Name == this.btn_Mapping.Name)
                {
                    this.btn_Mapping.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_Mapping.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_Mapping.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_Mapping.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51); ;
                }
                if (pBtn.Name == this.btn_Preference.Name)
                {
                    this.btn_Preference.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_LightBlue;
                    this.btn_Preference.ForeColor = System.Drawing.Color.FromArgb(90, 184, 235);
                }
                else
                {
                    this.btn_Preference.BackgroundImage = servr.integratex.ui.Properties.Resources.TabButton_Default;
                    this.btn_Preference.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                } 
            }
            catch (Exception ex)
            {
                
            }
        }

       
    }
}
