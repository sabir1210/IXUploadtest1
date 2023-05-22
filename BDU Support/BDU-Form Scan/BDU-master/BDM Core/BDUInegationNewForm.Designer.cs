namespace bdu
{
    partial class BDUInegationNewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDUInegationNewForm));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblFieldName_Source = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.lblDataType_Source = new System.Windows.Forms.Label();
            this.txtXPath = new System.Windows.Forms.TextBox();
            this.lblMetaData_Source = new System.Windows.Forms.Label();
            this.lblMaxLength = new System.Windows.Forms.Label();
            this.txtMaxLength = new System.Windows.Forms.TextBox();
            this.lblDefaultValue = new System.Windows.Forms.Label();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.cmbDataType_Source = new System.Windows.Forms.ComboBox();
            this.grbSourceInterface = new System.Windows.Forms.GroupBox();
            this.rdbNo_Mandatory = new System.Windows.Forms.RadioButton();
            this.rdbYes_Mandatory = new System.Windows.Forms.RadioButton();
            this.lblMandatory = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.lblBooking = new System.Windows.Forms.Label();
            this.lblXField = new System.Windows.Forms.Label();
            this.lblMappingForm = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ttp_Menus = new System.Windows.Forms.ToolTip(this.components);
            this.pnlBottom.SuspendLayout();
            this.grbSourceInterface.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(782, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnAdd.Size = new System.Drawing.Size(114, 42);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = " &Add (+)";
            this.ttp_Menus.SetToolTip(this.btnAdd, "Save & Settings ");
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(661, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnCancel.Size = new System.Drawing.Size(114, 42);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.ttp_Menus.SetToolTip(this.btnCancel, "Cancel the action");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Attribute Name";
            this.columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Attribute Type";
            this.columnHeader2.Width = 105;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Default Value";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Name = "columnHeader4";
            this.columnHeader4.Text = "Meta Data";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Edit";
            this.columnHeader5.Width = 80;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnAdd);
            this.pnlBottom.Location = new System.Drawing.Point(-2, 564);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(919, 59);
            this.pnlBottom.TabIndex = 3;
            // 
            // lblFieldName_Source
            // 
            this.lblFieldName_Source.AutoSize = true;
            this.lblFieldName_Source.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFieldName_Source.ForeColor = System.Drawing.Color.Black;
            this.lblFieldName_Source.Location = new System.Drawing.Point(268, 39);
            this.lblFieldName_Source.Name = "lblFieldName_Source";
            this.lblFieldName_Source.Size = new System.Drawing.Size(83, 16);
            this.lblFieldName_Source.TabIndex = 2;
            this.lblFieldName_Source.Text = "Field Name ";
            // 
            // txtFieldName
            // 
            this.txtFieldName.BackColor = System.Drawing.Color.White;
            this.txtFieldName.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFieldName.ForeColor = System.Drawing.Color.Black;
            this.txtFieldName.Location = new System.Drawing.Point(371, 38);
            this.txtFieldName.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtFieldName.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(267, 25);
            this.txtFieldName.TabIndex = 1;
            // 
            // lblDataType_Source
            // 
            this.lblDataType_Source.AutoSize = true;
            this.lblDataType_Source.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDataType_Source.ForeColor = System.Drawing.Color.Black;
            this.lblDataType_Source.Location = new System.Drawing.Point(271, 134);
            this.lblDataType_Source.Name = "lblDataType_Source";
            this.lblDataType_Source.Size = new System.Drawing.Size(80, 16);
            this.lblDataType_Source.TabIndex = 2;
            this.lblDataType_Source.Text = "Data Type ";
            // 
            // txtXPath
            // 
            this.txtXPath.BackColor = System.Drawing.Color.White;
            this.txtXPath.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtXPath.ForeColor = System.Drawing.Color.Black;
            this.txtXPath.Location = new System.Drawing.Point(371, 85);
            this.txtXPath.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtXPath.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtXPath.Name = "txtXPath";
            this.txtXPath.Size = new System.Drawing.Size(267, 25);
            this.txtXPath.TabIndex = 2;
            // 
            // lblMetaData_Source
            // 
            this.lblMetaData_Source.AutoSize = true;
            this.lblMetaData_Source.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMetaData_Source.ForeColor = System.Drawing.Color.Black;
            this.lblMetaData_Source.Location = new System.Drawing.Point(300, 85);
            this.lblMetaData_Source.Name = "lblMetaData_Source";
            this.lblMetaData_Source.Size = new System.Drawing.Size(46, 16);
            this.lblMetaData_Source.TabIndex = 2;
            this.lblMetaData_Source.Text = "XPath";
            // 
            // lblMaxLength
            // 
            this.lblMaxLength.AutoSize = true;
            this.lblMaxLength.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMaxLength.ForeColor = System.Drawing.Color.Black;
            this.lblMaxLength.Location = new System.Drawing.Point(263, 231);
            this.lblMaxLength.Name = "lblMaxLength";
            this.lblMaxLength.Size = new System.Drawing.Size(88, 16);
            this.lblMaxLength.TabIndex = 2;
            this.lblMaxLength.Text = "Max Length ";
            // 
            // txtMaxLength
            // 
            this.txtMaxLength.BackColor = System.Drawing.Color.White;
            this.txtMaxLength.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMaxLength.ForeColor = System.Drawing.Color.Black;
            this.txtMaxLength.Location = new System.Drawing.Point(371, 229);
            this.txtMaxLength.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtMaxLength.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtMaxLength.Name = "txtMaxLength";
            this.txtMaxLength.Size = new System.Drawing.Size(267, 25);
            this.txtMaxLength.TabIndex = 5;
            // 
            // lblDefaultValue
            // 
            this.lblDefaultValue.AutoSize = true;
            this.lblDefaultValue.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDefaultValue.ForeColor = System.Drawing.Color.Black;
            this.lblDefaultValue.Location = new System.Drawing.Point(252, 331);
            this.lblDefaultValue.Name = "lblDefaultValue";
            this.lblDefaultValue.Size = new System.Drawing.Size(99, 16);
            this.lblDefaultValue.TabIndex = 2;
            this.lblDefaultValue.Text = "Default Value ";
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.BackColor = System.Drawing.Color.White;
            this.txtDefaultValue.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDefaultValue.ForeColor = System.Drawing.Color.Black;
            this.txtDefaultValue.Location = new System.Drawing.Point(371, 330);
            this.txtDefaultValue.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtDefaultValue.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(267, 25);
            this.txtDefaultValue.TabIndex = 7;
            // 
            // cmbDataType_Source
            // 
            this.cmbDataType_Source.BackColor = System.Drawing.Color.White;
            this.cmbDataType_Source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType_Source.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbDataType_Source.ForeColor = System.Drawing.Color.Black;
            this.cmbDataType_Source.FormattingEnabled = true;
            this.cmbDataType_Source.Items.AddRange(new object[] {
            "Integer",
            "Text",
            "Float",
            "Boolean",
            "Date",
            "DateTime"});
            this.cmbDataType_Source.Location = new System.Drawing.Point(371, 132);
            this.cmbDataType_Source.MaximumSize = new System.Drawing.Size(267, 0);
            this.cmbDataType_Source.MinimumSize = new System.Drawing.Size(267, 0);
            this.cmbDataType_Source.Name = "cmbDataType_Source";
            this.cmbDataType_Source.Size = new System.Drawing.Size(267, 26);
            this.cmbDataType_Source.TabIndex = 3;
            // 
            // grbSourceInterface
            // 
            this.grbSourceInterface.BackColor = System.Drawing.Color.Transparent;
            this.grbSourceInterface.Controls.Add(this.rdbNo_Mandatory);
            this.grbSourceInterface.Controls.Add(this.rdbYes_Mandatory);
            this.grbSourceInterface.Controls.Add(this.lblMandatory);
            this.grbSourceInterface.Controls.Add(this.txtExpression);
            this.grbSourceInterface.Controls.Add(this.label4);
            this.grbSourceInterface.Controls.Add(this.cmbDataType_Source);
            this.grbSourceInterface.Controls.Add(this.txtDefaultValue);
            this.grbSourceInterface.Controls.Add(this.lblDefaultValue);
            this.grbSourceInterface.Controls.Add(this.txtMaxLength);
            this.grbSourceInterface.Controls.Add(this.lblMaxLength);
            this.grbSourceInterface.Controls.Add(this.lblMetaData_Source);
            this.grbSourceInterface.Controls.Add(this.txtXPath);
            this.grbSourceInterface.Controls.Add(this.lblDataType_Source);
            this.grbSourceInterface.Controls.Add(this.txtFieldName);
            this.grbSourceInterface.Controls.Add(this.lblFieldName_Source);
            this.grbSourceInterface.Controls.Add(this.txtFormat);
            this.grbSourceInterface.Controls.Add(this.lblFormat);
            this.grbSourceInterface.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grbSourceInterface.ForeColor = System.Drawing.Color.Black;
            this.grbSourceInterface.Location = new System.Drawing.Point(5, 99);
            this.grbSourceInterface.Name = "grbSourceInterface";
            this.grbSourceInterface.Size = new System.Drawing.Size(904, 459);
            this.grbSourceInterface.TabIndex = 1;
            this.grbSourceInterface.TabStop = false;
            this.grbSourceInterface.Text = "PMS Field Details";
            // 
            // rdbNo_Mandatory
            // 
            this.rdbNo_Mandatory.AutoSize = true;
            this.rdbNo_Mandatory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdbNo_Mandatory.ForeColor = System.Drawing.Color.Black;
            this.rdbNo_Mandatory.Location = new System.Drawing.Point(524, 378);
            this.rdbNo_Mandatory.Name = "rdbNo_Mandatory";
            this.rdbNo_Mandatory.Size = new System.Drawing.Size(42, 20);
            this.rdbNo_Mandatory.TabIndex = 9;
            this.rdbNo_Mandatory.TabStop = true;
            this.rdbNo_Mandatory.Text = "No";
            this.rdbNo_Mandatory.UseVisualStyleBackColor = true;
            // 
            // rdbYes_Mandatory
            // 
            this.rdbYes_Mandatory.AutoSize = true;
            this.rdbYes_Mandatory.Checked = true;
            this.rdbYes_Mandatory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rdbYes_Mandatory.ForeColor = System.Drawing.Color.Black;
            this.rdbYes_Mandatory.Location = new System.Drawing.Point(401, 378);
            this.rdbYes_Mandatory.Name = "rdbYes_Mandatory";
            this.rdbYes_Mandatory.Size = new System.Drawing.Size(48, 20);
            this.rdbYes_Mandatory.TabIndex = 8;
            this.rdbYes_Mandatory.TabStop = true;
            this.rdbYes_Mandatory.Text = "Yes";
            this.rdbYes_Mandatory.UseVisualStyleBackColor = true;
            // 
            // lblMandatory
            // 
            this.lblMandatory.AutoSize = true;
            this.lblMandatory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMandatory.ForeColor = System.Drawing.Color.Black;
            this.lblMandatory.Location = new System.Drawing.Point(270, 378);
            this.lblMandatory.Name = "lblMandatory";
            this.lblMandatory.Size = new System.Drawing.Size(77, 16);
            this.lblMandatory.TabIndex = 2;
            this.lblMandatory.Text = "Mandatory";
            // 
            // txtExpression
            // 
            this.txtExpression.BackColor = System.Drawing.Color.White;
            this.txtExpression.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtExpression.ForeColor = System.Drawing.Color.Black;
            this.txtExpression.Location = new System.Drawing.Point(371, 179);
            this.txtExpression.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtExpression.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(267, 26);
            this.txtExpression.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(270, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Expression";
            // 
            // txtFormat
            // 
            this.txtFormat.BackColor = System.Drawing.Color.White;
            this.txtFormat.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFormat.ForeColor = System.Drawing.Color.Black;
            this.txtFormat.Location = new System.Drawing.Point(371, 280);
            this.txtFormat.MaximumSize = new System.Drawing.Size(267, 26);
            this.txtFormat.MinimumSize = new System.Drawing.Size(267, 26);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.Size = new System.Drawing.Size(267, 25);
            this.txtFormat.TabIndex = 6;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFormat.ForeColor = System.Drawing.Color.Black;
            this.lblFormat.Location = new System.Drawing.Point(293, 283);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(53, 16);
            this.lblFormat.TabIndex = 2;
            this.lblFormat.Text = "Format";
            // 
            // lblBooking
            // 
            this.lblBooking.AutoSize = true;
            this.lblBooking.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBooking.ForeColor = System.Drawing.Color.Black;
            this.lblBooking.Location = new System.Drawing.Point(379, 39);
            this.lblBooking.Name = "lblBooking";
            this.lblBooking.Size = new System.Drawing.Size(57, 16);
            this.lblBooking.TabIndex = 2;
            this.lblBooking.Text = "Booking";
            // 
            // lblXField
            // 
            this.lblXField.AutoSize = true;
            this.lblXField.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblXField.ForeColor = System.Drawing.Color.Black;
            this.lblXField.Location = new System.Drawing.Point(579, 40);
            this.lblXField.Name = "lblXField";
            this.lblXField.Size = new System.Drawing.Size(51, 16);
            this.lblXField.TabIndex = 2;
            this.lblXField.Text = "X Field";
            // 
            // lblMappingForm
            // 
            this.lblMappingForm.AutoSize = true;
            this.lblMappingForm.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMappingForm.ForeColor = System.Drawing.Color.Black;
            this.lblMappingForm.Location = new System.Drawing.Point(256, 39);
            this.lblMappingForm.Name = "lblMappingForm";
            this.lblMappingForm.Size = new System.Drawing.Size(121, 16);
            this.lblMappingForm.TabIndex = 2;
            this.lblMappingForm.Text = "Mapping Form -";
            // 
            // lblFieldName
            // 
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFieldName.ForeColor = System.Drawing.Color.Black;
            this.lblFieldName.Location = new System.Drawing.Point(475, 40);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(99, 16);
            this.lblFieldName.TabIndex = 2;
            this.lblFieldName.Text = "Field Name -";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMappingForm);
            this.panel1.Controls.Add(this.lblFieldName);
            this.panel1.Controls.Add(this.lblXField);
            this.panel1.Controls.Add(this.lblBooking);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 617);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(904, 94);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // BDUInegationNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(917, 623);
            this.Controls.Add(this.grbSourceInterface);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BDUInegationNewForm";
            this.ShowIcon = false;
            this.Text = "Advance Mapping";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BDUInegationNewForm_Load);
            this.pnlBottom.ResumeLayout(false);
            this.grbSourceInterface.ResumeLayout(false);
            this.grbSourceInterface.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblFieldName_Source;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label lblDataType_Source;
        private System.Windows.Forms.TextBox txtXPath;
        private System.Windows.Forms.Label lblMetaData_Source;
        private System.Windows.Forms.Label lblMaxLength;
        private System.Windows.Forms.TextBox txtMaxLength;
        private System.Windows.Forms.Label lblDefaultValue;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.ComboBox cmbDataType_Source;
        private System.Windows.Forms.GroupBox grbSourceInterface;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMandatory;
        private System.Windows.Forms.RadioButton rdbNo_Mandatory;
        private System.Windows.Forms.RadioButton rdbYes_Mandatory;
        private System.Windows.Forms.Label lblBooking;
        private System.Windows.Forms.Label lblXField;
        private System.Windows.Forms.Label lblMappingForm;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip ttp_Menus;
    }
}