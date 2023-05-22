using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bdu
{
    public partial class BDUSyncForm : Form
    {
        public BDUSyncForm()
        {
            InitializeComponent();
            btnClearVisualIndicator.MouseEnter += new EventHandler(btnClearVisualIndicator_MouseEnter);
            btnClearVisualIndicator.MouseLeave += new EventHandler(btnClearVisualIndicator_MouseLeave);

            btnPushToAllPMS.MouseEnter += new EventHandler(btnPushToAllPMS_MouseEnter);
            btnPushToAllPMS.MouseLeave += new EventHandler(btnPushToAllPMS_MouseLeave);
        }
        void btnClearVisualIndicator_MouseEnter(object sender, EventArgs e)
        {
            this.btnClearVisualIndicator.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnBigDefaultHvr1));
        }
        void btnClearVisualIndicator_MouseLeave(object sender, EventArgs e)
        {
            this.btnClearVisualIndicator.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnBigDefault1));
        }
        void btnPushToAllPMS_MouseEnter(object sender, EventArgs e)
        {
            this.btnPushToAllPMS.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnBigDefaultHvr1));
        }
        void btnPushToAllPMS_MouseLeave(object sender, EventArgs e)
        {
            this.btnPushToAllPMS.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnBigDefault1)); ;
        }
        internal void switchForm()
        {
            throw new NotImplementedException();
        }
    }
}
