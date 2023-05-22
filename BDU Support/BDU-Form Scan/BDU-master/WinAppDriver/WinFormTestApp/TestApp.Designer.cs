namespace WindowsFormsTestApplication
{
    partial class TestApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbComments = new System.Windows.Forms.RichTextBox();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.dtpArrival = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.mtbPhone = new System.Windows.Forms.MaskedTextBox();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.clbMonths = new System.Windows.Forms.CheckedListBox();
            this.cbSelectAll = new System.Windows.Forms.CheckBox();
            this.CheckComboBox = new System.Windows.Forms.ComboBox();
            this.tbSecond = new System.Windows.Forms.TextBox();
            this.ChangeEnabledButton = new System.Windows.Forms.Button();
            this.tlbMonths = new System.Windows.Forms.ListBox();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.cbDays = new System.Windows.Forms.ComboBox();
            this.tbFirst = new System.Windows.Forms.TextBox();
            this.SetTextButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFemale = new System.Windows.Forms.RadioButton();
            this.rbMale = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.tbReadonly = new System.Windows.Forms.TextBox();
            this.tbHidden = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbComments
            // 
            this.rtbComments.Location = new System.Drawing.Point(370, 341);
            this.rtbComments.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rtbComments.Name = "rtbComments";
            this.rtbComments.Size = new System.Drawing.Size(191, 69);
            this.rtbComments.TabIndex = 5;
            this.rtbComments.Text = "my comments";
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(64, 390);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(187, 23);
            this.dtpTime.TabIndex = 4;
            this.dtpTime.Value = new System.DateTime(2021, 5, 4, 12, 55, 0, 0);
            // 
            // dtpArrival
            // 
            this.dtpArrival.Location = new System.Drawing.Point(64, 364);
            this.dtpArrival.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.dtpArrival.Name = "dtpArrival";
            this.dtpArrival.Size = new System.Drawing.Size(221, 23);
            this.dtpArrival.TabIndex = 4;
            this.dtpArrival.Value = new System.DateTime(2021, 5, 4, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 342);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Comments";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(3, 393);
            this.lblTime.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(34, 15);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "Time";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 365);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 307);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Masked Phone";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 337);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "City";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(3, 307);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(49, 15);
            this.lblAddress.TabIndex = 3;
            this.lblAddress.Text = "Address";
            // 
            // mtbPhone
            // 
            this.mtbPhone.Location = new System.Drawing.Point(375, 308);
            this.mtbPhone.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.mtbPhone.Mask = "(999) 000-0000";
            this.mtbPhone.Name = "mtbPhone";
            this.mtbPhone.Size = new System.Drawing.Size(148, 23);
            this.mtbPhone.TabIndex = 2;
            this.mtbPhone.Text = "300123456";
            // 
            // cbCity
            // 
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Items.AddRange(new object[] {
            "Lahore",
            "Karachi",
            "Islamabad",
            "Multan"});
            this.cbCity.Location = new System.Drawing.Point(64, 337);
            this.cbCity.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(149, 23);
            this.cbCity.TabIndex = 1;
            this.cbCity.Text = "Islamabad";
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(64, 308);
            this.tbAddress.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(149, 23);
            this.tbAddress.TabIndex = 0;
            this.tbAddress.Text = "some address";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(49, 5);
            this.txtID.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(46, 23);
            this.txtID.TabIndex = 64;
            this.txtID.Text = "1002";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(11, 8);
            this.lblID.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(31, 15);
            this.lblID.TabIndex = 63;
            this.lblID.Text = "1001";
            // 
            // clbMonths
            // 
            this.clbMonths.FormattingEnabled = true;
            this.clbMonths.Location = new System.Drawing.Point(314, 150);
            this.clbMonths.Name = "clbMonths";
            this.clbMonths.Size = new System.Drawing.Size(231, 94);
            this.clbMonths.TabIndex = 58;
            // 
            // cbSelectAll
            // 
            this.cbSelectAll.AutoSize = true;
            this.cbSelectAll.Location = new System.Drawing.Point(313, 122);
            this.cbSelectAll.Name = "cbSelectAll";
            this.cbSelectAll.Size = new System.Drawing.Size(84, 19);
            this.cbSelectAll.TabIndex = 57;
            this.cbSelectAll.Text = "All months";
            this.cbSelectAll.UseVisualStyleBackColor = true;
            // 
            // CheckComboBox
            // 
            this.CheckComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CheckComboBox.Enabled = false;
            this.CheckComboBox.FormattingEnabled = true;
            this.CheckComboBox.Location = new System.Drawing.Point(313, 90);
            this.CheckComboBox.Name = "CheckComboBox";
            this.CheckComboBox.Size = new System.Drawing.Size(233, 23);
            this.CheckComboBox.TabIndex = 56;
            // 
            // tbSecond
            // 
            this.tbSecond.Location = new System.Drawing.Point(313, 60);
            this.tbSecond.Name = "tbSecond";
            this.tbSecond.Size = new System.Drawing.Size(233, 23);
            this.tbSecond.TabIndex = 55;
            this.tbSecond.Text = "TextBox2";
            // 
            // ChangeEnabledButton
            // 
            this.ChangeEnabledButton.Location = new System.Drawing.Point(313, 27);
            this.ChangeEnabledButton.Name = "ChangeEnabledButton";
            this.ChangeEnabledButton.Size = new System.Drawing.Size(233, 27);
            this.ChangeEnabledButton.TabIndex = 54;
            this.ChangeEnabledButton.Text = "Change enabled to TextBox2";
            this.ChangeEnabledButton.UseVisualStyleBackColor = true;
            // 
            // tlbMonths
            // 
            this.tlbMonths.FormattingEnabled = true;
            this.tlbMonths.ItemHeight = 15;
            this.tlbMonths.Location = new System.Drawing.Point(7, 149);
            this.tlbMonths.Name = "tlbMonths";
            this.tlbMonths.Size = new System.Drawing.Size(233, 94);
            this.tlbMonths.TabIndex = 53;
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(49, 122);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(134, 19);
            this.cbEnable.TabIndex = 52;
            this.cbEnable.Text = "IsEnabledTextListBox";
            this.cbEnable.UseVisualStyleBackColor = true;
            // 
            // cbDays
            // 
            this.cbDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDays.FormattingEnabled = true;
            this.cbDays.Location = new System.Drawing.Point(7, 90);
            this.cbDays.Name = "cbDays";
            this.cbDays.Size = new System.Drawing.Size(233, 23);
            this.cbDays.TabIndex = 51;
            // 
            // tbFirst
            // 
            this.tbFirst.Location = new System.Drawing.Point(7, 60);
            this.tbFirst.Name = "tbFirst";
            this.tbFirst.Size = new System.Drawing.Size(233, 23);
            this.tbFirst.TabIndex = 50;
            this.tbFirst.Text = "TextBox1";
            // 
            // SetTextButton
            // 
            this.SetTextButton.Location = new System.Drawing.Point(7, 27);
            this.SetTextButton.Name = "SetTextButton";
            this.SetTextButton.Size = new System.Drawing.Size(233, 27);
            this.SetTextButton.TabIndex = 49;
            this.SetTextButton.Text = "Set \'Text\' to TextBox 1";
            this.SetTextButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFemale);
            this.groupBox1.Controls.Add(this.rbMale);
            this.groupBox1.Location = new System.Drawing.Point(7, 243);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.groupBox1.Size = new System.Drawing.Size(233, 46);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gender";
            // 
            // rbFemale
            // 
            this.rbFemale.AutoSize = true;
            this.rbFemale.Location = new System.Drawing.Point(151, 21);
            this.rbFemale.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rbFemale.Name = "rbFemale";
            this.rbFemale.Size = new System.Drawing.Size(63, 19);
            this.rbFemale.TabIndex = 26;
            this.rbFemale.TabStop = true;
            this.rbFemale.Text = "Female";
            this.rbFemale.UseVisualStyleBackColor = true;
            // 
            // rbMale
            // 
            this.rbMale.AutoSize = true;
            this.rbMale.Checked = true;
            this.rbMale.Location = new System.Drawing.Point(65, 21);
            this.rbMale.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.rbMale.Name = "rbMale";
            this.rbMale.Size = new System.Drawing.Size(51, 19);
            this.rbMale.TabIndex = 26;
            this.rbMale.TabStop = true;
            this.rbMale.Text = "Male";
            this.rbMale.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(457, 441);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 30);
            this.button1.TabIndex = 62;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(477, 266);
            this.nudCount.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(67, 23);
            this.nudCount.TabIndex = 61;
            this.nudCount.Value = new decimal(new int[] {
            99,
            0,
            0,
            0});
            // 
            // tbNum
            // 
            this.tbNum.Location = new System.Drawing.Point(314, 266);
            this.tbNum.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(82, 23);
            this.tbNum.TabIndex = 60;
            this.tbNum.Text = "123";
            // 
            // tbReadonly
            // 
            this.tbReadonly.Location = new System.Drawing.Point(102, 5);
            this.tbReadonly.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbReadonly.Name = "tbReadonly";
            this.tbReadonly.ReadOnly = true;
            this.tbReadonly.Size = new System.Drawing.Size(55, 23);
            this.tbReadonly.TabIndex = 65;
            this.tbReadonly.Text = "1003";
            // 
            // tbHidden
            // 
            this.tbHidden.Location = new System.Drawing.Point(158, 6);
            this.tbHidden.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tbHidden.Name = "tbHidden";
            this.tbHidden.Size = new System.Drawing.Size(55, 23);
            this.tbHidden.TabIndex = 66;
            this.tbHidden.Text = "1004";
            this.tbHidden.Visible = false;
            this.tbHidden.TextChanged += new System.EventHandler(this.tbHidden_TextChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(370, 441);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(58, 30);
            this.btnSubmit.TabIndex = 67;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // TestApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(570, 494);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbHidden);
            this.Controls.Add(this.tbReadonly);
            this.Controls.Add(this.rtbComments);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.dtpArrival);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.SetTextButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.clbMonths);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSelectAll);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.mtbPhone);
            this.Controls.Add(this.CheckComboBox);
            this.Controls.Add(this.cbCity);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.tbSecond);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ChangeEnabledButton);
            this.Controls.Add(this.tbFirst);
            this.Controls.Add(this.tlbMonths);
            this.Controls.Add(this.cbDays);
            this.Controls.Add(this.cbEnable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(586, 537);
            this.MinimumSize = new System.Drawing.Size(586, 404);
            this.Name = "TestApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PMS Data Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbComments;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.DateTimePicker dtpArrival;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.MaskedTextBox mtbPhone;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.CheckedListBox clbMonths;
        private System.Windows.Forms.CheckBox cbSelectAll;
        private System.Windows.Forms.ComboBox CheckComboBox;
        private System.Windows.Forms.TextBox tbSecond;
        private System.Windows.Forms.Button ChangeEnabledButton;
        private System.Windows.Forms.ListBox tlbMonths;
        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.ComboBox cbDays;
        private System.Windows.Forms.TextBox tbFirst;
        private System.Windows.Forms.Button SetTextButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFemale;
        private System.Windows.Forms.RadioButton rbMale;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.TextBox tbReadonly;
        private System.Windows.Forms.TextBox tbHidden;
        private System.Windows.Forms.Button btnSubmit;
    }
}

