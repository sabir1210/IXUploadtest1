using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bdu
{
    public partial class BDULoginForm : Form
    {
        public BDULoginForm()
        {
            InitializeComponent();
            btnLogin.MouseEnter += new EventHandler(btnLogin_MouseEnter);
            btnLogin.MouseLeave += new EventHandler(btnLogin_MouseLeave);
        }
        void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnLoginHvr));
        }
        void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnLogin));
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            BDUMainSplash frm = new BDUMainSplash();
            frm.FormClosed += new FormClosedEventHandler(frmLogIn_FormClosed);
            frm.Show();
            this.Hide();
        }
        private void frmLogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
        private void BDULoginForm_Load(object sender, EventArgs e)
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
            this.Left = rightmost.WorkingArea.Right - this.Width+5;
            this.Top = rightmost.WorkingArea.Bottom - this.Height+5;
        }        
    }
}
