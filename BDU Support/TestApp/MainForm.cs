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
using BDU.UTIL;

namespace WindowsFormsTestApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PMSTabForm frm = new PMSTabForm();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void btnPMS_Click(object sender, EventArgs e)
        {
            PMSForm frm = new PMSForm();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestApp frm = new TestApp();
            frm.ShowDialog(this);
            frm.Dispose();
        }

        private void btnTabbed_Click(object sender, EventArgs e)
        {
            PMSTabForm frm = new PMSTabForm();
            frm.ShowDialog(this);
            frm.Dispose();
        }
    }
}
