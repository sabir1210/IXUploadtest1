
namespace servr.integratex.ui
{
    partial class MappingForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_PresetMapping = new BDU.Extensions.SPanel();
            this.cmbPMSType = new System.Windows.Forms.ComboBox();
            this.cmbpmsVersion = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbMappingHotel = new System.Windows.Forms.ComboBox();
            this.cmbMappingEntity = new System.Windows.Forms.ComboBox();
            this.btn_DownRow = new System.Windows.Forms.Button();
            this.btnGetPresets = new BDU.Extensions.ServrButton();
            this.chb_PMSVersion = new System.Windows.Forms.CheckBox();
            this.btn_UpRow = new System.Windows.Forms.Button();
            this.txtPMSVersion = new BDU.Extensions.ServrInputControl();
            this.btn_PresetMapping = new BDU.Extensions.ServrButton();
            this.pnl_Bottom = new BDU.Extensions.SPanel();
            this.btnScan = new BDU.Extensions.ServrButton();
            this.btnAddField = new BDU.Extensions.ServrButton();
            this.btnSave = new BDU.Extensions.ServrButton();
            this.btnSave_Complete = new BDU.Extensions.ServrButton();
            this.btnCancel = new BDU.Extensions.ServrButton();
            this.btnTestRun = new BDU.Extensions.ServrButton();
            this.pnl_GridBack = new BDU.Extensions.SPanel();
            this.grvMappingData = new System.Windows.Forms.DataGridView();
            this.coSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdvanceMapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCMSFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPmsFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colmandatory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.colAdvance = new System.Windows.Forms.DataGridViewImageColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_unmapped = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_PresetMapping.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            this.pnl_GridBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvMappingData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_PresetMapping
            // 
            this.pnl_PresetMapping.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_PresetMapping.AutoScroll = true;
            this.pnl_PresetMapping.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_PresetMapping.BackColor = System.Drawing.Color.White;
            this.pnl_PresetMapping.BorderColor = System.Drawing.Color.White;
            this.pnl_PresetMapping.Controls.Add(this.cmbPMSType);
            this.pnl_PresetMapping.Controls.Add(this.cmbpmsVersion);
            this.pnl_PresetMapping.Controls.Add(this.cmbStatus);
            this.pnl_PresetMapping.Controls.Add(this.cmbMappingHotel);
            this.pnl_PresetMapping.Controls.Add(this.cmbMappingEntity);
            this.pnl_PresetMapping.Controls.Add(this.btn_DownRow);
            this.pnl_PresetMapping.Controls.Add(this.btnGetPresets);
            this.pnl_PresetMapping.Controls.Add(this.chb_PMSVersion);
            this.pnl_PresetMapping.Controls.Add(this.btn_UpRow);
            this.pnl_PresetMapping.Controls.Add(this.txtPMSVersion);
            this.pnl_PresetMapping.Controls.Add(this.btn_PresetMapping);
            this.pnl_PresetMapping.Edge = 20;
            this.pnl_PresetMapping.Location = new System.Drawing.Point(3, 4);
            this.pnl_PresetMapping.MaximumSize = new System.Drawing.Size(878, 138);
            this.pnl_PresetMapping.MinimumSize = new System.Drawing.Size(878, 138);
            this.pnl_PresetMapping.Name = "pnl_PresetMapping";
            this.pnl_PresetMapping.Size = new System.Drawing.Size(878, 138);
            this.pnl_PresetMapping.TabIndex = 47;
            // 
            // cmbPMSType
            // 
            this.cmbPMSType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPMSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPMSType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPMSType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPMSType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbPMSType.FormattingEnabled = true;
            this.cmbPMSType.IntegralHeight = false;
            this.cmbPMSType.Location = new System.Drawing.Point(14, 52);
            this.cmbPMSType.MaximumSize = new System.Drawing.Size(269, 0);
            this.cmbPMSType.MinimumSize = new System.Drawing.Size(269, 0);
            this.cmbPMSType.Name = "cmbPMSType";
            this.cmbPMSType.Size = new System.Drawing.Size(269, 27);
            this.cmbPMSType.TabIndex = 81;
            this.cmbPMSType.Tag = "PMS Type";
            this.cmbPMSType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMappingEntity_DrawItem);
            // 
            // cmbpmsVersion
            // 
            this.cmbpmsVersion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpmsVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpmsVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbpmsVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbpmsVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbpmsVersion.FormattingEnabled = true;
            this.cmbpmsVersion.IntegralHeight = false;
            this.cmbpmsVersion.Location = new System.Drawing.Point(14, 8);
            this.cmbpmsVersion.MaximumSize = new System.Drawing.Size(269, 0);
            this.cmbpmsVersion.MinimumSize = new System.Drawing.Size(269, 0);
            this.cmbpmsVersion.Name = "cmbpmsVersion";
            this.cmbpmsVersion.Size = new System.Drawing.Size(269, 27);
            this.cmbpmsVersion.TabIndex = 82;
            this.cmbpmsVersion.Tag = "PMS Version";
            this.cmbpmsVersion.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMappingHotel_DrawItem);
            this.cmbpmsVersion.SelectedIndexChanged += new System.EventHandler(this.cmbpmsVersion_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.IntegralHeight = false;
            this.cmbStatus.Location = new System.Drawing.Point(472, 52);
            this.cmbStatus.MaximumSize = new System.Drawing.Size(324, 0);
            this.cmbStatus.MinimumSize = new System.Drawing.Size(324, 0);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(324, 27);
            this.cmbStatus.TabIndex = 80;
            this.cmbStatus.Tag = "Status";
            this.cmbStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMappingHotel_DrawItem);
            // 
            // cmbMappingHotel
            // 
            this.cmbMappingHotel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMappingHotel.DropDownHeight = 500;
            this.cmbMappingHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappingHotel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMappingHotel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbMappingHotel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbMappingHotel.FormattingEnabled = true;
            this.cmbMappingHotel.IntegralHeight = false;
            this.cmbMappingHotel.Location = new System.Drawing.Point(472, 8);
            this.cmbMappingHotel.MaximumSize = new System.Drawing.Size(324, 0);
            this.cmbMappingHotel.MinimumSize = new System.Drawing.Size(324, 0);
            this.cmbMappingHotel.Name = "cmbMappingHotel";
            this.cmbMappingHotel.Size = new System.Drawing.Size(324, 27);
            this.cmbMappingHotel.TabIndex = 79;
            this.cmbMappingHotel.Tag = "Mapping Hotel";
            this.cmbMappingHotel.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMappingHotel_DrawItem);
            this.cmbMappingHotel.SelectedIndexChanged += new System.EventHandler(this.cmbMappingHotel_SelectedIndexChanged);
            // 
            // cmbMappingEntity
            // 
            this.cmbMappingEntity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMappingEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappingEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMappingEntity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbMappingEntity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.cmbMappingEntity.FormattingEnabled = true;
            this.cmbMappingEntity.IntegralHeight = false;
            this.cmbMappingEntity.Location = new System.Drawing.Point(472, 101);
            this.cmbMappingEntity.MaximumSize = new System.Drawing.Size(324, 0);
            this.cmbMappingEntity.MinimumSize = new System.Drawing.Size(324, 0);
            this.cmbMappingEntity.Name = "cmbMappingEntity";
            this.cmbMappingEntity.Size = new System.Drawing.Size(324, 27);
            this.cmbMappingEntity.TabIndex = 80;
            this.cmbMappingEntity.Tag = "Mapping Hotel";
            this.cmbMappingEntity.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMappingEntity_DrawItem);
            this.cmbMappingEntity.SelectedIndexChanged += new System.EventHandler(this.cmbMappingEntity_SelectedIndexChanged);
            // 
            // btn_DownRow
            // 
            this.btn_DownRow.BackColor = System.Drawing.Color.Transparent;
            this.btn_DownRow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DownRow.BackgroundImage")));
            this.btn_DownRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_DownRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DownRow.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_DownRow.FlatAppearance.BorderSize = 0;
            this.btn_DownRow.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_DownRow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_DownRow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_DownRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DownRow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_DownRow.ForeColor = System.Drawing.Color.Black;
            this.btn_DownRow.Location = new System.Drawing.Point(817, 77);
            this.btn_DownRow.MaximumSize = new System.Drawing.Size(50, 50);
            this.btn_DownRow.MinimumSize = new System.Drawing.Size(50, 50);
            this.btn_DownRow.Name = "btn_DownRow";
            this.btn_DownRow.Padding = new System.Windows.Forms.Padding(2);
            this.btn_DownRow.Size = new System.Drawing.Size(50, 50);
            this.btn_DownRow.TabIndex = 44;
            this.btn_DownRow.UseVisualStyleBackColor = false;
            this.btn_DownRow.Click += new System.EventHandler(this.btn_DownRow_Click);
            // 
            // btnGetPresets
            // 
            this.btnGetPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnGetPresets.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnGetPresets.BorderColor = System.Drawing.Color.White;
            this.btnGetPresets.BorderRadius = 36;
            this.btnGetPresets.BorderSize = 0;
            this.btnGetPresets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetPresets.FlatAppearance.BorderSize = 0;
            this.btnGetPresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetPresets.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnGetPresets.ForeColor = System.Drawing.Color.White;
            this.btnGetPresets.Location = new System.Drawing.Point(303, 8);
            this.btnGetPresets.MaximumSize = new System.Drawing.Size(150, 36);
            this.btnGetPresets.MinimumSize = new System.Drawing.Size(150, 36);
            this.btnGetPresets.Name = "btnGetPresets";
            this.btnGetPresets.RootElement = null;
            this.btnGetPresets.Size = new System.Drawing.Size(150, 36);
            this.btnGetPresets.TabIndex = 3;
            this.btnGetPresets.Text = "&Get Presets";
            this.btnGetPresets.TextColor = System.Drawing.Color.White;
            this.btnGetPresets.UseVisualStyleBackColor = false;
            this.btnGetPresets.Click += new System.EventHandler(this.btnGetPresets_Click);
            // 
            // chb_PMSVersion
            // 
            this.chb_PMSVersion.AutoSize = true;
            this.chb_PMSVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chb_PMSVersion.Location = new System.Drawing.Point(311, 101);
            this.chb_PMSVersion.Name = "chb_PMSVersion";
            this.chb_PMSVersion.Size = new System.Drawing.Size(120, 24);
            this.chb_PMSVersion.TabIndex = 43;
            this.chb_PMSVersion.Text = "PMS Version";
            this.chb_PMSVersion.UseVisualStyleBackColor = true;
            // 
            // btn_UpRow
            // 
            this.btn_UpRow.BackColor = System.Drawing.Color.Transparent;
            this.btn_UpRow.BackgroundImage = global::servr.integratex.ui.Properties.Resources.up_arrow_Gray;
            this.btn_UpRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_UpRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_UpRow.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_UpRow.FlatAppearance.BorderSize = 0;
            this.btn_UpRow.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_UpRow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_UpRow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_UpRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UpRow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_UpRow.ForeColor = System.Drawing.Color.Black;
            this.btn_UpRow.Location = new System.Drawing.Point(817, 10);
            this.btn_UpRow.MaximumSize = new System.Drawing.Size(50, 50);
            this.btn_UpRow.MinimumSize = new System.Drawing.Size(50, 50);
            this.btn_UpRow.Name = "btn_UpRow";
            this.btn_UpRow.Padding = new System.Windows.Forms.Padding(2);
            this.btn_UpRow.Size = new System.Drawing.Size(50, 50);
            this.btn_UpRow.TabIndex = 9;
            this.btn_UpRow.UseVisualStyleBackColor = false;
            this.btn_UpRow.Click += new System.EventHandler(this.btn_UpRow_Click);
            // 
            // txtPMSVersion
            // 
            this.txtPMSVersion.BackColor = System.Drawing.Color.Transparent;
            this.txtPMSVersion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtPMSVersion.BorderSize = 1;
            this.txtPMSVersion.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.txtPMSVersion.Font = new System.Drawing.Font("Roboto Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPMSVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtPMSVersion.Location = new System.Drawing.Point(14, 93);
            this.txtPMSVersion.MaximumSize = new System.Drawing.Size(269, 40);
            this.txtPMSVersion.MinimumSize = new System.Drawing.Size(269, 40);
            this.txtPMSVersion.Name = "txtPMSVersion";
            this.txtPMSVersion.PasswordChar = '\0';
            this.txtPMSVersion.PlaceHolderText = "";
            this.txtPMSVersion.Size = new System.Drawing.Size(269, 40);
            this.txtPMSVersion.TabIndex = 1;
            this.txtPMSVersion.Tag = "PMS Version";
            this.txtPMSVersion.textboxRadius = 18;
            this.txtPMSVersion.UseSystemPasswordChar = false;
            // 
            // btn_PresetMapping
            // 
            this.btn_PresetMapping.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_PresetMapping.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_PresetMapping.BorderColor = System.Drawing.Color.White;
            this.btn_PresetMapping.BorderRadius = 36;
            this.btn_PresetMapping.BorderSize = 0;
            this.btn_PresetMapping.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_PresetMapping.FlatAppearance.BorderSize = 0;
            this.btn_PresetMapping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PresetMapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_PresetMapping.ForeColor = System.Drawing.Color.White;
            this.btn_PresetMapping.Location = new System.Drawing.Point(303, 49);
            this.btn_PresetMapping.MaximumSize = new System.Drawing.Size(150, 36);
            this.btn_PresetMapping.MinimumSize = new System.Drawing.Size(150, 36);
            this.btn_PresetMapping.Name = "btn_PresetMapping";
            this.btn_PresetMapping.RootElement = null;
            this.btn_PresetMapping.Size = new System.Drawing.Size(150, 36);
            this.btn_PresetMapping.TabIndex = 6;
            this.btn_PresetMapping.Text = "&Preset Mapping";
            this.btn_PresetMapping.TextColor = System.Drawing.Color.White;
            this.btn_PresetMapping.UseVisualStyleBackColor = false;
            this.btn_PresetMapping.Visible = false;
            this.btn_PresetMapping.Click += new System.EventHandler(this.btn_PresetMapping_Click);
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Bottom.AutoScroll = true;
            this.pnl_Bottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_Bottom.BackColor = System.Drawing.Color.White;
            this.pnl_Bottom.BorderColor = System.Drawing.Color.White;
            this.pnl_Bottom.Controls.Add(this.btnScan);
            this.pnl_Bottom.Controls.Add(this.btnAddField);
            this.pnl_Bottom.Controls.Add(this.btnSave);
            this.pnl_Bottom.Controls.Add(this.btnSave_Complete);
            this.pnl_Bottom.Controls.Add(this.btnCancel);
            this.pnl_Bottom.Controls.Add(this.btnTestRun);
            this.pnl_Bottom.Edge = 20;
            this.pnl_Bottom.Location = new System.Drawing.Point(4, 481);
            this.pnl_Bottom.MaximumSize = new System.Drawing.Size(878, 53);
            this.pnl_Bottom.MinimumSize = new System.Drawing.Size(878, 53);
            this.pnl_Bottom.Name = "pnl_Bottom";
            this.pnl_Bottom.Size = new System.Drawing.Size(878, 53);
            this.pnl_Bottom.TabIndex = 48;
            // 
            // btnScan
            // 
            this.btnScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnScan.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnScan.BorderColor = System.Drawing.Color.White;
            this.btnScan.BorderRadius = 36;
            this.btnScan.BorderSize = 0;
            this.btnScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScan.Enabled = false;
            this.btnScan.FlatAppearance.BorderSize = 0;
            this.btnScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnScan.ForeColor = System.Drawing.Color.White;
            this.btnScan.Location = new System.Drawing.Point(15, 8);
            this.btnScan.MaximumSize = new System.Drawing.Size(125, 37);
            this.btnScan.MinimumSize = new System.Drawing.Size(125, 37);
            this.btnScan.Name = "btnScan";
            this.btnScan.RootElement = null;
            this.btnScan.Size = new System.Drawing.Size(125, 37);
            this.btnScan.TabIndex = 16;
            this.btnScan.Text = "Scan";
            this.btnScan.TextColor = System.Drawing.Color.White;
            this.btnScan.UseVisualStyleBackColor = false;
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnAddField.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnAddField.BorderColor = System.Drawing.Color.White;
            this.btnAddField.BorderRadius = 36;
            this.btnAddField.BorderSize = 0;
            this.btnAddField.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddField.FlatAppearance.BorderSize = 0;
            this.btnAddField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddField.ForeColor = System.Drawing.Color.White;
            this.btnAddField.Location = new System.Drawing.Point(425, 8);
            this.btnAddField.MaximumSize = new System.Drawing.Size(125, 37);
            this.btnAddField.MinimumSize = new System.Drawing.Size(125, 37);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.RootElement = null;
            this.btnAddField.Size = new System.Drawing.Size(125, 37);
            this.btnAddField.TabIndex = 15;
            this.btnAddField.Text = "&Add Field";
            this.btnAddField.TextColor = System.Drawing.Color.White;
            this.btnAddField.UseVisualStyleBackColor = false;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnSave.BorderColor = System.Drawing.Color.White;
            this.btnSave.BorderRadius = 36;
            this.btnSave.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(562, 8);
            this.btnSave.MaximumSize = new System.Drawing.Size(125, 37);
            this.btnSave.MinimumSize = new System.Drawing.Size(125, 37);
            this.btnSave.Name = "btnSave";
            this.btnSave.RootElement = null;
            this.btnSave.Size = new System.Drawing.Size(125, 37);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSave_Complete
            // 
            this.btnSave_Complete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnSave_Complete.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnSave_Complete.BorderColor = System.Drawing.Color.White;
            this.btnSave_Complete.BorderRadius = 36;
            this.btnSave_Complete.BorderSize = 0;
            this.btnSave_Complete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave_Complete.FlatAppearance.BorderSize = 0;
            this.btnSave_Complete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave_Complete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave_Complete.ForeColor = System.Drawing.Color.White;
            this.btnSave_Complete.Location = new System.Drawing.Point(699, 8);
            this.btnSave_Complete.MaximumSize = new System.Drawing.Size(165, 37);
            this.btnSave_Complete.MinimumSize = new System.Drawing.Size(165, 37);
            this.btnSave_Complete.Name = "btnSave_Complete";
            this.btnSave_Complete.RootElement = null;
            this.btnSave_Complete.Size = new System.Drawing.Size(165, 37);
            this.btnSave_Complete.TabIndex = 14;
            this.btnSave_Complete.Text = "Save && &Complete";
            this.btnSave_Complete.TextColor = System.Drawing.Color.White;
            this.btnSave_Complete.UseVisualStyleBackColor = false;
            this.btnSave_Complete.Click += new System.EventHandler(this.btnSave_Complete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BorderColor = System.Drawing.Color.White;
            this.btnCancel.BorderRadius = 36;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.Location = new System.Drawing.Point(151, 8);
            this.btnCancel.MaximumSize = new System.Drawing.Size(125, 37);
            this.btnCancel.MinimumSize = new System.Drawing.Size(125, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RootElement = null;
            this.btnCancel.Size = new System.Drawing.Size(125, 37);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Reset";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTestRun
            // 
            this.btnTestRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnTestRun.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnTestRun.BorderColor = System.Drawing.Color.White;
            this.btnTestRun.BorderRadius = 36;
            this.btnTestRun.BorderSize = 0;
            this.btnTestRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestRun.FlatAppearance.BorderSize = 0;
            this.btnTestRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestRun.ForeColor = System.Drawing.Color.White;
            this.btnTestRun.Location = new System.Drawing.Point(289, 8);
            this.btnTestRun.MaximumSize = new System.Drawing.Size(125, 37);
            this.btnTestRun.MinimumSize = new System.Drawing.Size(125, 37);
            this.btnTestRun.Name = "btnTestRun";
            this.btnTestRun.RootElement = null;
            this.btnTestRun.Size = new System.Drawing.Size(125, 37);
            this.btnTestRun.TabIndex = 11;
            this.btnTestRun.Text = "&Test Run";
            this.btnTestRun.TextColor = System.Drawing.Color.White;
            this.btnTestRun.UseVisualStyleBackColor = false;
            this.btnTestRun.Click += new System.EventHandler(this.btnTestRun_Click);
            // 
            // pnl_GridBack
            // 
            this.pnl_GridBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_GridBack.AutoScroll = true;
            this.pnl_GridBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_GridBack.BackColor = System.Drawing.Color.White;
            this.pnl_GridBack.BorderColor = System.Drawing.Color.White;
            this.pnl_GridBack.Controls.Add(this.grvMappingData);
            this.pnl_GridBack.Edge = 20;
            this.pnl_GridBack.Location = new System.Drawing.Point(3, 146);
            this.pnl_GridBack.MaximumSize = new System.Drawing.Size(878, 331);
            this.pnl_GridBack.MinimumSize = new System.Drawing.Size(878, 331);
            this.pnl_GridBack.Name = "pnl_GridBack";
            this.pnl_GridBack.Size = new System.Drawing.Size(878, 331);
            this.pnl_GridBack.TabIndex = 46;
            // 
            // grvMappingData
            // 
            this.grvMappingData.AllowUserToAddRows = false;
            this.grvMappingData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grvMappingData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grvMappingData.BackgroundColor = System.Drawing.Color.White;
            this.grvMappingData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvMappingData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.grvMappingData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 9, 4, 15);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvMappingData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grvMappingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvMappingData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coSerial,
            this.colAdvanceMapping,
            this.colCMSFieldName,
            this.colPmsFieldName,
            this.colmandatory,
            this.colDefaultValue,
            this.colEdit,
            this.colDelete,
            this.colAdvance,
            this.Id,
            this.entity,
            this.parent_Id,
            this.col_unmapped});
            this.grvMappingData.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvMappingData.DefaultCellStyle = dataGridViewCellStyle15;
            this.grvMappingData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grvMappingData.GridColor = System.Drawing.Color.White;
            this.grvMappingData.Location = new System.Drawing.Point(5, 4);
            this.grvMappingData.MaximumSize = new System.Drawing.Size(868, 322);
            this.grvMappingData.MinimumSize = new System.Drawing.Size(868, 322);
            this.grvMappingData.MultiSelect = false;
            this.grvMappingData.Name = "grvMappingData";
            this.grvMappingData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvMappingData.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.grvMappingData.RowHeadersWidth = 4;
            this.grvMappingData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.White;
            this.grvMappingData.RowsDefaultCellStyle = dataGridViewCellStyle17;
            this.grvMappingData.RowTemplate.Height = 40;
            this.grvMappingData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grvMappingData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvMappingData.Size = new System.Drawing.Size(868, 322);
            this.grvMappingData.TabIndex = 4;
            this.grvMappingData.TabStop = false;
            this.grvMappingData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvMappingData_CellContentClick);
            this.grvMappingData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grvMappingData_CellFormatting);
            this.grvMappingData.Paint += new System.Windows.Forms.PaintEventHandler(this.grvMappingData_Paint);
            this.grvMappingData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvMappingData_KeyDown);
            // 
            // coSerial
            // 
            this.coSerial.DataPropertyName = "fieldsr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.coSerial.DefaultCellStyle = dataGridViewCellStyle3;
            this.coSerial.HeaderText = "";
            this.coSerial.MinimumWidth = 2;
            this.coSerial.Name = "coSerial";
            this.coSerial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.coSerial.ToolTipText = "Serial #";
            this.coSerial.Visible = false;
            this.coSerial.Width = 40;
            // 
            // colAdvanceMapping
            // 
            this.colAdvanceMapping.DataPropertyName = " AdvanceMapping";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gray;
            this.colAdvanceMapping.DefaultCellStyle = dataGridViewCellStyle4;
            this.colAdvanceMapping.HeaderText = " Advance Mapping";
            this.colAdvanceMapping.MinimumWidth = 2;
            this.colAdvanceMapping.Name = "colAdvanceMapping";
            this.colAdvanceMapping.Visible = false;
            // 
            // colCMSFieldName
            // 
            this.colCMSFieldName.DataPropertyName = "field_desc";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Gray;
            this.colCMSFieldName.DefaultCellStyle = dataGridViewCellStyle5;
            this.colCMSFieldName.HeaderText = "Guestx Field Name";
            this.colCMSFieldName.MinimumWidth = 2;
            this.colCMSFieldName.Name = "colCMSFieldName";
            this.colCMSFieldName.ReadOnly = true;
            this.colCMSFieldName.ToolTipText = "GuestX Field Name";
            this.colCMSFieldName.Width = 200;
            // 
            // colPmsFieldName
            // 
            this.colPmsFieldName.DataPropertyName = "pms_field_name";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Gray;
            this.colPmsFieldName.DefaultCellStyle = dataGridViewCellStyle6;
            this.colPmsFieldName.HeaderText = "Field Name";
            this.colPmsFieldName.MinimumWidth = 2;
            this.colPmsFieldName.Name = "colPmsFieldName";
            this.colPmsFieldName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPmsFieldName.Width = 230;
            // 
            // colmandatory
            // 
            this.colmandatory.DataPropertyName = "mandatory";
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Gray;
            this.colmandatory.DefaultCellStyle = dataGridViewCellStyle7;
            this.colmandatory.HeaderText = "Mandatory";
            this.colmandatory.MinimumWidth = 2;
            this.colmandatory.Name = "colmandatory";
            this.colmandatory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colmandatory.ToolTipText = "Field requirement";
            this.colmandatory.Width = 190;
            // 
            // colDefaultValue
            // 
            this.colDefaultValue.DataPropertyName = "default_value";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Gray;
            this.colDefaultValue.DefaultCellStyle = dataGridViewCellStyle8;
            this.colDefaultValue.HeaderText = "Default Value";
            this.colDefaultValue.MinimumWidth = 2;
            this.colDefaultValue.Name = "colDefaultValue";
            this.colDefaultValue.ToolTipText = "Value for default usage ";
            this.colDefaultValue.Width = 140;
            // 
            // colEdit
            // 
            this.colEdit.DataPropertyName = "edit";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Transparent;
            this.colEdit.DefaultCellStyle = dataGridViewCellStyle9;
            this.colEdit.HeaderText = "";
            this.colEdit.Image = global::servr.integratex.ui.Properties.Resources.edit_gray;
            this.colEdit.MinimumWidth = 2;
            this.colEdit.Name = "colEdit";
            this.colEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colEdit.ToolTipText = "Edit Full Row";
            this.colEdit.Visible = false;
            this.colEdit.Width = 40;
            // 
            // colDelete
            // 
            this.colDelete.DataPropertyName = "delete";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.NullValue = null;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.colDelete.DefaultCellStyle = dataGridViewCellStyle10;
            this.colDelete.HeaderText = "";
            this.colDelete.Image = global::servr.integratex.ui.Properties.Resources.delete_gray;
            this.colDelete.MinimumWidth = 2;
            this.colDelete.Name = "colDelete";
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colDelete.ToolTipText = "Delete Full Row";
            this.colDelete.Width = 45;
            // 
            // colAdvance
            // 
            this.colAdvance.DataPropertyName = "advance";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.NullValue = null;
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            this.colAdvance.DefaultCellStyle = dataGridViewCellStyle11;
            this.colAdvance.HeaderText = "";
            this.colAdvance.Image = global::servr.integratex.ui.Properties.Resources.edit_gray;
            this.colAdvance.MinimumWidth = 2;
            this.colAdvance.Name = "colAdvance";
            this.colAdvance.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAdvance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.colAdvance.ToolTipText = "Advance Mapping";
            this.colAdvance.Width = 45;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "fuid";
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Gray;
            this.Id.DefaultCellStyle = dataGridViewCellStyle12;
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 2;
            this.Id.Name = "Id";
            this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Id.Visible = false;
            // 
            // entity
            // 
            this.entity.DataPropertyName = "entity_id";
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Gray;
            this.entity.DefaultCellStyle = dataGridViewCellStyle13;
            this.entity.HeaderText = "entity";
            this.entity.MinimumWidth = 2;
            this.entity.Name = "entity";
            this.entity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.entity.Visible = false;
            // 
            // parent_Id
            // 
            this.parent_Id.DataPropertyName = "parentId";
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.parent_Id.DefaultCellStyle = dataGridViewCellStyle14;
            this.parent_Id.HeaderText = "Parent";
            this.parent_Id.MinimumWidth = 2;
            this.parent_Id.Name = "parent_Id";
            this.parent_Id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.parent_Id.Visible = false;
            // 
            // col_unmapped
            // 
            this.col_unmapped.DataPropertyName = "is_unmapped";
            this.col_unmapped.HeaderText = "";
            this.col_unmapped.Name = "col_unmapped";
            this.col_unmapped.Visible = false;
            // 
            // MappingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnl_PresetMapping);
            this.Controls.Add(this.pnl_Bottom);
            this.Controls.Add(this.pnl_GridBack);
            this.MaximumSize = new System.Drawing.Size(885, 540);
            this.MinimumSize = new System.Drawing.Size(885, 540);
            this.Name = "MappingForm";
            this.Size = new System.Drawing.Size(885, 540);
            this.Load += new System.EventHandler(this.MappingForm_Load);
            this.pnl_PresetMapping.ResumeLayout(false);
            this.pnl_PresetMapping.PerformLayout();
            this.pnl_Bottom.ResumeLayout(false);
            this.pnl_GridBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvMappingData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BDU.Extensions.SPanel pnl_PresetMapping;
        private System.Windows.Forms.ComboBox cmbPMSType;
        private System.Windows.Forms.ComboBox cmbpmsVersion;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbMappingHotel;
        private System.Windows.Forms.ComboBox cmbMappingEntity;
        private System.Windows.Forms.Button btn_DownRow;
        private BDU.Extensions.ServrButton btnGetPresets;
        private System.Windows.Forms.CheckBox chb_PMSVersion;
        private System.Windows.Forms.Button btn_UpRow;
        private BDU.Extensions.ServrInputControl txtPMSVersion;
        private BDU.Extensions.ServrButton btn_PresetMapping;
        private BDU.Extensions.SPanel pnl_Bottom;
        private BDU.Extensions.ServrButton btnScan;
        private BDU.Extensions.ServrButton btnAddField;
        private BDU.Extensions.ServrButton btnSave;
        private BDU.Extensions.ServrButton btnSave_Complete;
        private BDU.Extensions.ServrButton btnCancel;
        private BDU.Extensions.ServrButton btnTestRun;
        private BDU.Extensions.SPanel pnl_GridBack;
        private System.Windows.Forms.DataGridView grvMappingData;
        private System.Windows.Forms.DataGridViewTextBoxColumn coSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdvanceMapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCMSFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPmsFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colmandatory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDefaultValue;
        private System.Windows.Forms.DataGridViewImageColumn colEdit;
        private System.Windows.Forms.DataGridViewImageColumn colDelete;
        private System.Windows.Forms.DataGridViewImageColumn colAdvance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn entity;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_unmapped;
    }
}
