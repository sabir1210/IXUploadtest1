namespace bdu
{
    partial class BDUMappingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDUMappingForm));
            this.lblMappingEntity = new System.Windows.Forms.Label();
            this.btnXPath = new System.Windows.Forms.Button();
            this.cmbMappingEntity = new System.Windows.Forms.ComboBox();
            this.lblHotel = new System.Windows.Forms.Label();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.InterfaceMapping_Grid = new System.Windows.Forms.ListView();
            this.colCMSFieldName = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.colFieldName = new System.Windows.Forms.ColumnHeader();
            this.colDataType = new System.Windows.Forms.ColumnHeader();
            this.colLength = new System.Windows.Forms.ColumnHeader();
            this.colXPathId = new System.Windows.Forms.ColumnHeader();
            this.colDefaultValue = new System.Windows.Forms.ColumnHeader();
            this.colAction = new System.Windows.Forms.ColumnHeader();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTestRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grp_Presets = new System.Windows.Forms.GroupBox();
            this.btnUpLow = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnGetHotelMapping = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbHotelName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ttpMenus = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.grp_Presets.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMappingEntity
            // 
            this.lblMappingEntity.AutoSize = true;
            this.lblMappingEntity.BackColor = System.Drawing.Color.Transparent;
            this.lblMappingEntity.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMappingEntity.ForeColor = System.Drawing.Color.Black;
            this.lblMappingEntity.Location = new System.Drawing.Point(505, 89);
            this.lblMappingEntity.Name = "lblMappingEntity";
            this.lblMappingEntity.Size = new System.Drawing.Size(98, 16);
            this.lblMappingEntity.TabIndex = 2;
            this.lblMappingEntity.Text = "Mapping Form";
            // 
            // btnXPath
            // 
            this.btnXPath.BackColor = System.Drawing.Color.Transparent;
            this.btnXPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnXPath.BackgroundImage")));
            this.btnXPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnXPath.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnXPath.FlatAppearance.BorderSize = 0;
            this.btnXPath.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnXPath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnXPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnXPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXPath.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnXPath.ForeColor = System.Drawing.Color.Black;
            this.btnXPath.Location = new System.Drawing.Point(651, 128);
            this.btnXPath.MaximumSize = new System.Drawing.Size(120, 42);
            this.btnXPath.MinimumSize = new System.Drawing.Size(120, 42);
            this.btnXPath.Name = "btnXPath";
            this.btnXPath.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnXPath.Size = new System.Drawing.Size(120, 42);
            this.btnXPath.TabIndex = 2;
            this.btnXPath.Text = "&Load XPath";
            this.ttpMenus.SetToolTip(this.btnXPath, "Load XPath Finder");
            this.btnXPath.UseVisualStyleBackColor = false;
            this.btnXPath.Click += new System.EventHandler(this.btnXPath_Click);
            // 
            // cmbMappingEntity
            // 
            this.cmbMappingEntity.BackColor = System.Drawing.Color.White;
            this.cmbMappingEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappingEntity.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbMappingEntity.ForeColor = System.Drawing.Color.Black;
            this.cmbMappingEntity.FormattingEnabled = true;
            this.cmbMappingEntity.Items.AddRange(new object[] {
            "Booking",
            "Billing Details",
            "Check In",
            "Check Out"});
            this.cmbMappingEntity.Location = new System.Drawing.Point(616, 85);
            this.cmbMappingEntity.Name = "cmbMappingEntity";
            this.cmbMappingEntity.Size = new System.Drawing.Size(267, 26);
            this.cmbMappingEntity.TabIndex = 1;
            // 
            // lblHotel
            // 
            this.lblHotel.AutoSize = true;
            this.lblHotel.BackColor = System.Drawing.Color.Transparent;
            this.lblHotel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHotel.ForeColor = System.Drawing.Color.Black;
            this.lblHotel.Location = new System.Drawing.Point(25, 89);
            this.lblHotel.Name = "lblHotel";
            this.lblHotel.Size = new System.Drawing.Size(95, 16);
            this.lblHotel.TabIndex = 2;
            this.lblHotel.Text = "Hotel System";
            // 
            // cmbSource
            // 
            this.cmbSource.BackColor = System.Drawing.Color.White;
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbSource.ForeColor = System.Drawing.Color.Black;
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Items.AddRange(new object[] {
            "PMS (Web)",
            "PMS (Desktop)"});
            this.cmbSource.Location = new System.Drawing.Point(142, 85);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(267, 26);
            this.cmbSource.TabIndex = 1;
            // 
            // InterfaceMapping_Grid
            // 
            this.InterfaceMapping_Grid.BackColor = System.Drawing.Color.LightGray;
            this.InterfaceMapping_Grid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCMSFieldName,
            this.columnHeader6,
            this.colFieldName,
            this.colDataType,
            this.colLength,
            this.colXPathId,
            this.colDefaultValue,
            this.colAction});
            this.InterfaceMapping_Grid.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.InterfaceMapping_Grid.ForeColor = System.Drawing.Color.Black;
            this.InterfaceMapping_Grid.FullRowSelect = true;
            this.InterfaceMapping_Grid.GridLines = true;
            this.InterfaceMapping_Grid.HideSelection = false;
            this.InterfaceMapping_Grid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.InterfaceMapping_Grid.LabelEdit = true;
            this.InterfaceMapping_Grid.Location = new System.Drawing.Point(0, 194);
            this.InterfaceMapping_Grid.Name = "InterfaceMapping_Grid";
            this.InterfaceMapping_Grid.Size = new System.Drawing.Size(914, 345);
            this.InterfaceMapping_Grid.TabIndex = 5;
            this.InterfaceMapping_Grid.TileSize = new System.Drawing.Size(100, 80);
            this.InterfaceMapping_Grid.UseCompatibleStateImageBehavior = false;
            this.InterfaceMapping_Grid.View = System.Windows.Forms.View.Details;
            this.InterfaceMapping_Grid.VirtualListSize = 20;
            // 
            // colCMSFieldName
            // 
            this.colCMSFieldName.Name = "colCMSFieldName";
            this.colCMSFieldName.Text = "CMS Field Name";
            this.colCMSFieldName.Width = 145;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "";
            this.columnHeader6.Width = 3;
            // 
            // colFieldName
            // 
            this.colFieldName.Name = "colFieldName";
            this.colFieldName.Text = "Field Name";
            this.colFieldName.Width = 135;
            // 
            // colDataType
            // 
            this.colDataType.Name = "colDataType";
            this.colDataType.Text = "Data Type";
            this.colDataType.Width = 100;
            // 
            // colLength
            // 
            this.colLength.Name = "colLength";
            this.colLength.Text = "Length";
            this.colLength.Width = 65;
            // 
            // colXPathId
            // 
            this.colXPathId.Name = "colXPathId";
            this.colXPathId.Text = "XPath ID";
            this.colXPathId.Width = 165;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.Name = "colDefaultValue";
            this.colDefaultValue.Text = "Default Value";
            this.colDefaultValue.Width = 110;
            // 
            // colAction
            // 
            this.colAction.Name = "colAction";
            this.colAction.Text = "Action";
            this.colAction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colAction.Width = 180;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(776, 8);
            this.btnSave.MaximumSize = new System.Drawing.Size(114, 42);
            this.btnSave.MinimumSize = new System.Drawing.Size(114, 42);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnSave.Size = new System.Drawing.Size(114, 42);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.ttpMenus.SetToolTip(this.btnSave, "Save Mapping Local & CMS");
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(657, 10);
            this.btnCancel.MaximumSize = new System.Drawing.Size(114, 42);
            this.btnCancel.MinimumSize = new System.Drawing.Size(114, 42);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.btnCancel.Size = new System.Drawing.Size(114, 42);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.ttpMenus.SetToolTip(this.btnCancel, "Cancel ");
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnTestRun);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(-1, 542);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 59);
            this.panel1.TabIndex = 3;
            // 
            // btnTestRun
            // 
            this.btnTestRun.BackColor = System.Drawing.Color.Transparent;
            this.btnTestRun.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTestRun.BackgroundImage")));
            this.btnTestRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTestRun.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTestRun.FlatAppearance.BorderSize = 0;
            this.btnTestRun.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnTestRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTestRun.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTestRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestRun.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnTestRun.ForeColor = System.Drawing.Color.Black;
            this.btnTestRun.Location = new System.Drawing.Point(534, 10);
            this.btnTestRun.MaximumSize = new System.Drawing.Size(114, 42);
            this.btnTestRun.MinimumSize = new System.Drawing.Size(114, 42);
            this.btnTestRun.Name = "btnTestRun";
            this.btnTestRun.Size = new System.Drawing.Size(114, 42);
            this.btnTestRun.TabIndex = 2;
            this.btnTestRun.Text = "&Test Run";
            this.ttpMenus.SetToolTip(this.btnTestRun, "Interface Mapping Working Check");
            this.btnTestRun.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(899, 32);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // grp_Presets
            // 
            this.grp_Presets.Controls.Add(this.btnUpLow);
            this.grp_Presets.Controls.Add(this.panel2);
            this.grp_Presets.Controls.Add(this.btnApply);
            this.grp_Presets.Controls.Add(this.btnGetHotelMapping);
            this.grp_Presets.Controls.Add(this.cmbStatus);
            this.grp_Presets.Controls.Add(this.cmbHotelName);
            this.grp_Presets.Controls.Add(this.label2);
            this.grp_Presets.Controls.Add(this.label1);
            this.grp_Presets.Controls.Add(this.cmbMappingEntity);
            this.grp_Presets.Controls.Add(this.cmbSource);
            this.grp_Presets.Controls.Add(this.lblMappingEntity);
            this.grp_Presets.Controls.Add(this.lblHotel);
            this.grp_Presets.Controls.Add(this.btnXPath);
            this.grp_Presets.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grp_Presets.ForeColor = System.Drawing.Color.Black;
            this.grp_Presets.Location = new System.Drawing.Point(2, 2);
            this.grp_Presets.Name = "grp_Presets";
            this.grp_Presets.Size = new System.Drawing.Size(909, 189);
            this.grp_Presets.TabIndex = 6;
            this.grp_Presets.TabStop = false;
            this.grp_Presets.Text = "Presets";
            this.grp_Presets.Enter += new System.EventHandler(this.grp_Presets_Enter);
            // 
            // btnUpLow
            // 
            this.btnUpLow.BackColor = System.Drawing.Color.Transparent;
            this.btnUpLow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpLow.BackgroundImage")));
            this.btnUpLow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUpLow.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUpLow.FlatAppearance.BorderSize = 0;
            this.btnUpLow.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnUpLow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUpLow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUpLow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpLow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpLow.ForeColor = System.Drawing.Color.Black;
            this.btnUpLow.Location = new System.Drawing.Point(834, 125);
            this.btnUpLow.Name = "btnUpLow";
            this.btnUpLow.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.btnUpLow.Size = new System.Drawing.Size(50, 50);
            this.btnUpLow.TabIndex = 2;
            this.ttpMenus.SetToolTip(this.btnUpLow, "Adjust mapping order");
            this.btnUpLow.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Location = new System.Drawing.Point(24, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 2);
            this.panel2.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.Transparent;
            this.btnApply.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnApply.BackgroundImage")));
            this.btnApply.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnApply.ForeColor = System.Drawing.Color.Black;
            this.btnApply.Location = new System.Drawing.Point(774, 18);
            this.btnApply.MaximumSize = new System.Drawing.Size(114, 42);
            this.btnApply.MinimumSize = new System.Drawing.Size(114, 42);
            this.btnApply.Name = "btnApply";
            this.btnApply.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.btnApply.Size = new System.Drawing.Size(114, 42);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "&Apply";
            this.ttpMenus.SetToolTip(this.btnApply, "Apply | Overwrite");
            this.btnApply.UseVisualStyleBackColor = false;
            // 
            // btnGetHotelMapping
            // 
            this.btnGetHotelMapping.BackColor = System.Drawing.Color.Transparent;
            this.btnGetHotelMapping.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGetHotelMapping.BackgroundImage")));
            this.btnGetHotelMapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGetHotelMapping.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGetHotelMapping.FlatAppearance.BorderSize = 0;
            this.btnGetHotelMapping.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnGetHotelMapping.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGetHotelMapping.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGetHotelMapping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetHotelMapping.Font = new System.Drawing.Font("Verdana", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGetHotelMapping.ForeColor = System.Drawing.Color.Black;
            this.btnGetHotelMapping.Location = new System.Drawing.Point(616, 18);
            this.btnGetHotelMapping.MaximumSize = new System.Drawing.Size(155, 43);
            this.btnGetHotelMapping.MinimumSize = new System.Drawing.Size(155, 43);
            this.btnGetHotelMapping.Name = "btnGetHotelMapping";
            this.btnGetHotelMapping.Size = new System.Drawing.Size(155, 43);
            this.btnGetHotelMapping.TabIndex = 2;
            this.btnGetHotelMapping.Text = "&Get Hotel Presets";
            this.ttpMenus.SetToolTip(this.btnGetHotelMapping, "Load Selected Hotel Mappings");
            this.btnGetHotelMapping.UseVisualStyleBackColor = false;
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.White;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStatus.ForeColor = System.Drawing.Color.Black;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.cmbStatus.Location = new System.Drawing.Point(142, 133);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(267, 26);
            this.cmbStatus.TabIndex = 1;
            // 
            // cmbHotelName
            // 
            this.cmbHotelName.BackColor = System.Drawing.Color.White;
            this.cmbHotelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHotelName.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbHotelName.ForeColor = System.Drawing.Color.Black;
            this.cmbHotelName.FormattingEnabled = true;
            this.cmbHotelName.Items.AddRange(new object[] {
            "Servr"});
            this.cmbHotelName.Location = new System.Drawing.Point(142, 27);
            this.cmbHotelName.Name = "cmbHotelName";
            this.cmbHotelName.Size = new System.Drawing.Size(267, 26);
            this.cmbHotelName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(75, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hotel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(67, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Status";
            // 
            // BDUMappingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(911, 601);
            this.Controls.Add(this.grp_Presets);
            this.Controls.Add(this.InterfaceMapping_Grid);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BDUMappingForm";
            this.RightToLeftLayout = true;
            this.Text = "BDU - Presets & Mapping Details";
            this.Load += new System.EventHandler(this.BDUMappingForm_Load);
            this.panel1.ResumeLayout(false);
            this.grp_Presets.ResumeLayout(false);
            this.grp_Presets.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Label lblMappingEntity;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblHotel;
        private System.Windows.Forms.Button btnXPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView InterfaceMapping_Grid;
        private System.Windows.Forms.ColumnHeader colDefaultValue;
        private System.Windows.Forms.ColumnHeader colDataType;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ComboBox cmbMappingEntity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader colCMSFieldName;
        private System.Windows.Forms.ColumnHeader colFieldName;
        private System.Windows.Forms.ColumnHeader colLength;
        private System.Windows.Forms.ColumnHeader colXPathId;
        private System.Windows.Forms.ColumnHeader colAction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grp_Presets;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTestRun;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnGetHotelMapping;
        private System.Windows.Forms.ComboBox cmbHotelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUpLow;
        private System.Windows.Forms.ToolTip ttpMenus;
    }
}