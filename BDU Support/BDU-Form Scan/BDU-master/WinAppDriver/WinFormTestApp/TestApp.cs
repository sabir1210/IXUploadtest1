namespace WindowsFormsTestApplication
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using WinFormTestApp;

    #endregion

    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class TestApp : Form
    {
        #region Fields

        private readonly List<int> yearList = new List<int>
                                                       {
                                                          2010,2011,2012,2013,2014,2015,2016,2017,2018,2019,2020,2021,2022
                                                       };
        private readonly List<string> monthsList = new List<string>
                                                       {
                                                           "January",
                                                           "February",
                                                           "March",
                                                           "April",
                                                           "May",
                                                           "June",
                                                           "July",
                                                           "August",
                                                           "September",
                                                           "October",
                                                           "November",
                                                           "December"
                                                       };

        private readonly List<string> timeSizeList = new List<string>
                                                         {
                                                             "Day",
                                                             "Week",
                                                             "Decade",
                                                             "Month",
                                                             "Quarter",
                                                             "Year"
                                                         };

        #endregion

        #region Constructors and Destructors

        public TestApp()
        {
            this.InitializeComponent();

            this.cbEnable.Checked = true;

            // Fill TextComboBox
            this.cbDays.DataSource = this.timeSizeList;
            this.cbDays.SelectedIndex = 4;


            // Fill TextListBox
            this.tlbMonths.DataSource = this.yearList;
            this.tlbMonths.SelectedIndex = 3;

            // Fill CheckListBox
            this.clbMonths.DataSource = this.monthsList;

            // Fill DataGrid
        }

        #endregion

        #region Methods

        #endregion

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            //get value using expression, date, time, RTB ( Rich) , Masked Phone/ Data, read only, Disabled, 

            var check = this.cbSelectAll.Checked;
            for (int i = 0; i < this.clbMonths.Items.Count; ++i)
            {
                this.clbMonths.SetItemChecked(i, check);
            }
        }

        private void SetTextButton_Click(object sender, EventArgs e)
        {
            this.tbFirst.Text = @"Test";

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.tlbMonths.Enabled = this.cbEnable.Checked;

        }

        private void ChangeEnabledButton_Click(object sender, EventArgs e)
        {
            this.tbSecond.Enabled = !this.tbSecond.Enabled;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar));
        }

        private void tlbMonths_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.ClearControls();
            nudCount.Value = 0;
            foreach (int i in clbMonths.CheckedIndices)
            {
                clbMonths.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //lblID.Text = "";
            //txtID.Text = "";
            tbFirst.Text = "";
            tbSecond.Text = "";
            tbNum.Text = "";
            tbAddress.Text = "";
            mtbPhone.Text = "";
            rtbComments.Text = "";
            nudCount.Value = 0;
            dtpArrival.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            dtpTime.Value = new DateTime(1900,1,1,0,0,0);
            cbEnable.Checked = false;
            cbSelectAll.Checked = false;
            tlbMonths.SelectedIndex = -1;
            cbCity.SelectedIndex = -1;
            cbDays.SelectedIndex = -1;
            clbMonths.SelectedIndex = -1;
            rbMale.Checked = false;
            rbFemale.Checked = false;
        }

        private void tbHidden_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Form Submitted");
        }
    }
}
