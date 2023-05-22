using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bdu
{
    public partial class BDUInegationNewForm: Form
    {
        public BDUInegationNewForm()
        {
            InitializeComponent();
            btnAdd.MouseEnter += new EventHandler(btnSave_MouseEnter);
            btnAdd.MouseLeave += new EventHandler(btnSave_MouseLeave);

            btnCancel.MouseEnter += new EventHandler(btnCancel_MouseEnter);
            btnCancel.MouseLeave += new EventHandler(btnancel_MouseLeave);
        }
        void btnSave_MouseEnter(object sender, EventArgs e)
        {
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnCancelHvr));
        }
        void btnancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnCancel));
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BDUInegationNewForm_Load(object sender, EventArgs e)
        {
            PlaceLowerRight();
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

            this.Left = rightmost.WorkingArea.Right - this.Width - 5;
            this.Top = rightmost.WorkingArea.Bottom - this.Height - 5;
        }
    }
}
