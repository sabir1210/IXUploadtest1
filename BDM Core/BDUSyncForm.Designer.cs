using System;

namespace servr.integratex.ui
{
    partial class BDUSyncForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDUSyncForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grvSyncData = new System.Windows.Forms.DataGridView();
            this.srCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.entityCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionCol = new System.Windows.Forms.DataGridViewImageColumn();
            this.colIdReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ttp_Menus = new System.Windows.Forms.ToolTip(this.components);
            this.btn_CheckOut = new System.Windows.Forms.Button();
            this.btn_CheckIn = new System.Windows.Forms.Button();
            this.btn_SyncAll = new System.Windows.Forms.Button();
            this.btn_BillingDetail = new System.Windows.Forms.Button();
            this.pnl_LastSyncTime = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLastSyncedDateShow = new System.Windows.Forms.Label();
            this.btn_SyncAllPMS = new BDU.Extensions.ServrButton();
            this.btn_AutoSync = new BDU.Extensions.ServrButton();
            this.btn_ClearIndicators = new BDU.Extensions.ServrButton();
            this.btn_Reservation = new System.Windows.Forms.Button();
            this.lblUpdatesBuilitin = new System.Windows.Forms.Label();
            this.btn_PicShowMessage = new System.Windows.Forms.Button();
            this.pnl_Bottom = new BDU.Extensions.SPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_BackGrid = new BDU.Extensions.SPanel();
            this.btnHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grvSyncData)).BeginInit();
            this.pnl_LastSyncTime.SuspendLayout();
            this.pnl_Bottom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnl_BackGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // grvSyncData
            // 
            this.grvSyncData.AllowUserToAddRows = false;
            this.grvSyncData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grvSyncData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.grvSyncData, "grvSyncData");
            this.grvSyncData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvSyncData.BackgroundColor = System.Drawing.Color.White;
            this.grvSyncData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvSyncData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.grvSyncData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 9, 4, 15);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvSyncData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grvSyncData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvSyncData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srCol,
            this.colId,
            this.entityCol,
            this.colMode,
            this.TimeCol,
            this.actionCol,
            this.colIdReference,
            this.colEntityId});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvSyncData.DefaultCellStyle = dataGridViewCellStyle8;
            this.grvSyncData.GridColor = System.Drawing.Color.White;
            this.grvSyncData.MultiSelect = false;
            this.grvSyncData.Name = "grvSyncData";
            this.grvSyncData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvSyncData.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.grvSyncData.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.grvSyncData.RowTemplate.Height = 40;
            this.grvSyncData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvSyncData.TabStop = false;
            this.grvSyncData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSyncData_CellClick);
            this.grvSyncData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvSyncData_CellContentDoubleClick);
            this.grvSyncData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grvSyncData_CellFormatting);
            this.grvSyncData.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.grvSyncData_RowPrePaint);
            this.grvSyncData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grvSyncData_KeyDown);
            // 
            // srCol
            // 
            this.srCol.DataPropertyName = "uid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.srCol.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.srCol, "srCol");
            this.srCol.Name = "srCol";
            this.srCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colId
            // 
            this.colId.DataPropertyName = "reference";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.colId.DefaultCellStyle = dataGridViewCellStyle4;
            this.colId.FillWeight = 265.7143F;
            resources.ApplyResources(this.colId, "colId");
            this.colId.Name = "colId";
            // 
            // entityCol
            // 
            this.entityCol.DataPropertyName = "entity";
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.entityCol.DefaultCellStyle = dataGridViewCellStyle5;
            this.entityCol.FillWeight = 17.14285F;
            resources.ApplyResources(this.entityCol, "entityCol");
            this.entityCol.Name = "entityCol";
            // 
            // colMode
            // 
            this.colMode.DataPropertyName = "mode";
            resources.ApplyResources(this.colMode, "colMode");
            this.colMode.Name = "colMode";
            // 
            // TimeCol
            // 
            this.TimeCol.DataPropertyName = "createdAt";
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.TimeCol.DefaultCellStyle = dataGridViewCellStyle6;
            this.TimeCol.FillWeight = 17.14285F;
            resources.ApplyResources(this.TimeCol, "TimeCol");
            this.TimeCol.Name = "TimeCol";
            // 
            // actionCol
            // 
            this.actionCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.actionCol.DataPropertyName = "image";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(5);
            this.actionCol.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.actionCol, "actionCol");
            this.actionCol.Image = ((System.Drawing.Image)(resources.GetObject("actionCol.Image")));
            this.actionCol.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.actionCol.Name = "actionCol";
            this.actionCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.actionCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colIdReference
            // 
            resources.ApplyResources(this.colIdReference, "colIdReference");
            this.colIdReference.Name = "colIdReference";
            // 
            // colEntityId
            // 
            this.colEntityId.DataPropertyName = "entity_id";
            resources.ApplyResources(this.colEntityId, "colEntityId");
            this.colEntityId.Name = "colEntityId";
            // 
            // btn_CheckOut
            // 
            this.btn_CheckOut.BackgroundImage = global::servr.integratex.ui.Properties.Resources.TabButton_Default;
            resources.ApplyResources(this.btn_CheckOut, "btn_CheckOut");
            this.btn_CheckOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CheckOut.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_CheckOut.FlatAppearance.BorderSize = 0;
            this.btn_CheckOut.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckOut.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckOut.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_CheckOut.Name = "btn_CheckOut";
            this.btn_CheckOut.TabStop = false;
            this.btn_CheckOut.Tag = "4";
            this.ttp_Menus.SetToolTip(this.btn_CheckOut, resources.GetString("btn_CheckOut.ToolTip"));
            this.btn_CheckOut.UseVisualStyleBackColor = false;
            this.btn_CheckOut.Click += new System.EventHandler(this.btn_CheckOut_Click);
            // 
            // btn_CheckIn
            // 
            this.btn_CheckIn.BackgroundImage = global::servr.integratex.ui.Properties.Resources.TabButton_Default;
            resources.ApplyResources(this.btn_CheckIn, "btn_CheckIn");
            this.btn_CheckIn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CheckIn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_CheckIn.FlatAppearance.BorderSize = 0;
            this.btn_CheckIn.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckIn.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckIn.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_CheckIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_CheckIn.Name = "btn_CheckIn";
            this.btn_CheckIn.TabStop = false;
            this.btn_CheckIn.Tag = "2";
            this.ttp_Menus.SetToolTip(this.btn_CheckIn, resources.GetString("btn_CheckIn.ToolTip"));
            this.btn_CheckIn.UseVisualStyleBackColor = false;
            this.btn_CheckIn.Click += new System.EventHandler(this.btn_CheckIn_Click);
            // 
            // btn_SyncAll
            // 
            this.btn_SyncAll.BackgroundImage = global::servr.integratex.ui.Properties.Resources.TabButton_Default;
            resources.ApplyResources(this.btn_SyncAll, "btn_SyncAll");
            this.btn_SyncAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SyncAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_SyncAll.FlatAppearance.BorderSize = 0;
            this.btn_SyncAll.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_SyncAll.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_SyncAll.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_SyncAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_SyncAll.Name = "btn_SyncAll";
            this.btn_SyncAll.TabStop = false;
            this.btn_SyncAll.Tag = "0";
            this.ttp_Menus.SetToolTip(this.btn_SyncAll, resources.GetString("btn_SyncAll.ToolTip"));
            this.btn_SyncAll.UseVisualStyleBackColor = false;
            this.btn_SyncAll.Click += new System.EventHandler(this.btn_SyncAll_Click);
            // 
            // btn_BillingDetail
            // 
            this.btn_BillingDetail.BackgroundImage = global::servr.integratex.ui.Properties.Resources.TabButton_Default;
            resources.ApplyResources(this.btn_BillingDetail, "btn_BillingDetail");
            this.btn_BillingDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BillingDetail.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_BillingDetail.FlatAppearance.BorderSize = 0;
            this.btn_BillingDetail.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_BillingDetail.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_BillingDetail.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_BillingDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_BillingDetail.Name = "btn_BillingDetail";
            this.btn_BillingDetail.TabStop = false;
            this.btn_BillingDetail.Tag = "3";
            this.ttp_Menus.SetToolTip(this.btn_BillingDetail, resources.GetString("btn_BillingDetail.ToolTip"));
            this.btn_BillingDetail.UseVisualStyleBackColor = false;
            this.btn_BillingDetail.Click += new System.EventHandler(this.btn_BillingDetail_Click);
            // 
            // pnl_LastSyncTime
            // 
            this.pnl_LastSyncTime.Controls.Add(this.label1);
            this.pnl_LastSyncTime.Controls.Add(this.lblLastSyncedDateShow);
            resources.ApplyResources(this.pnl_LastSyncTime, "pnl_LastSyncTime");
            this.pnl_LastSyncTime.Name = "pnl_LastSyncTime";
            this.ttp_Menus.SetToolTip(this.pnl_LastSyncTime, resources.GetString("pnl_LastSyncTime.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label1.Name = "label1";
            this.ttp_Menus.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // lblLastSyncedDateShow
            // 
            resources.ApplyResources(this.lblLastSyncedDateShow, "lblLastSyncedDateShow");
            this.lblLastSyncedDateShow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblLastSyncedDateShow.Name = "lblLastSyncedDateShow";
            this.ttp_Menus.SetToolTip(this.lblLastSyncedDateShow, resources.GetString("lblLastSyncedDateShow.ToolTip"));
            // 
            // btn_SyncAllPMS
            // 
            this.btn_SyncAllPMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_SyncAllPMS.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_SyncAllPMS.BorderColor = System.Drawing.Color.White;
            this.btn_SyncAllPMS.BorderRadius = 36;
            this.btn_SyncAllPMS.BorderSize = 0;
            this.btn_SyncAllPMS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SyncAllPMS.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_SyncAllPMS, "btn_SyncAllPMS");
            this.btn_SyncAllPMS.ForeColor = System.Drawing.Color.White;
            this.btn_SyncAllPMS.Name = "btn_SyncAllPMS";
            this.btn_SyncAllPMS.RootElement = null;
            this.btn_SyncAllPMS.TextColor = System.Drawing.Color.White;
            this.ttp_Menus.SetToolTip(this.btn_SyncAllPMS, resources.GetString("btn_SyncAllPMS.ToolTip"));
            this.btn_SyncAllPMS.UseVisualStyleBackColor = false;
            this.btn_SyncAllPMS.Click += new System.EventHandler(this.btn_SyncAllPMS_Click);
            // 
            // btn_AutoSync
            // 
            this.btn_AutoSync.BackColor = System.Drawing.Color.Gray;
            this.btn_AutoSync.BackgroundColor = System.Drawing.Color.Gray;
            this.btn_AutoSync.BorderColor = System.Drawing.Color.White;
            this.btn_AutoSync.BorderRadius = 36;
            this.btn_AutoSync.BorderSize = 0;
            this.btn_AutoSync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AutoSync.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_AutoSync, "btn_AutoSync");
            this.btn_AutoSync.ForeColor = System.Drawing.Color.White;
            this.btn_AutoSync.Name = "btn_AutoSync";
            this.btn_AutoSync.RootElement = null;
            this.btn_AutoSync.Tag = "autosynoff";
            this.btn_AutoSync.TextColor = System.Drawing.Color.White;
            this.ttp_Menus.SetToolTip(this.btn_AutoSync, resources.GetString("btn_AutoSync.ToolTip"));
            this.btn_AutoSync.UseVisualStyleBackColor = false;
            this.btn_AutoSync.Click += new System.EventHandler(this.btn_AutoSync_Click);
            // 
            // btn_ClearIndicators
            // 
            this.btn_ClearIndicators.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_ClearIndicators.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_ClearIndicators.BorderColor = System.Drawing.Color.White;
            this.btn_ClearIndicators.BorderRadius = 36;
            this.btn_ClearIndicators.BorderSize = 0;
            this.btn_ClearIndicators.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ClearIndicators.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_ClearIndicators, "btn_ClearIndicators");
            this.btn_ClearIndicators.ForeColor = System.Drawing.Color.White;
            this.btn_ClearIndicators.Name = "btn_ClearIndicators";
            this.btn_ClearIndicators.RootElement = null;
            this.btn_ClearIndicators.TextColor = System.Drawing.Color.White;
            this.ttp_Menus.SetToolTip(this.btn_ClearIndicators, resources.GetString("btn_ClearIndicators.ToolTip"));
            this.btn_ClearIndicators.UseVisualStyleBackColor = false;
            this.btn_ClearIndicators.Click += new System.EventHandler(this.btn_ClearIndicators_Click);
            // 
            // btn_Reservation
            // 
            this.btn_Reservation.BackgroundImage = global::servr.integratex.ui.Properties.Resources.TabButton_Default;
            resources.ApplyResources(this.btn_Reservation, "btn_Reservation");
            this.btn_Reservation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Reservation.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Reservation.FlatAppearance.BorderSize = 0;
            this.btn_Reservation.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Reservation.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Reservation.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Reservation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Reservation.Name = "btn_Reservation";
            this.btn_Reservation.TabStop = false;
            this.btn_Reservation.Tag = "1";
            this.ttp_Menus.SetToolTip(this.btn_Reservation, resources.GetString("btn_Reservation.ToolTip"));
            this.btn_Reservation.UseVisualStyleBackColor = false;
            this.btn_Reservation.Click += new System.EventHandler(this.btn_Reservation_Click);
            // 
            // lblUpdatesBuilitin
            // 
            resources.ApplyResources(this.lblUpdatesBuilitin, "lblUpdatesBuilitin");
            this.lblUpdatesBuilitin.BackColor = System.Drawing.Color.White;
            this.lblUpdatesBuilitin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblUpdatesBuilitin.Name = "lblUpdatesBuilitin";
            // 
            // btn_PicShowMessage
            // 
            this.btn_PicShowMessage.BackColor = System.Drawing.Color.Transparent;
            this.btn_PicShowMessage.BackgroundImage = global::servr.integratex.ui.Properties.Resources.wait;
            resources.ApplyResources(this.btn_PicShowMessage, "btn_PicShowMessage");
            this.btn_PicShowMessage.FlatAppearance.BorderSize = 0;
            this.btn_PicShowMessage.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_PicShowMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_PicShowMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_PicShowMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_PicShowMessage.Name = "btn_PicShowMessage";
            this.btn_PicShowMessage.TabStop = false;
            this.btn_PicShowMessage.UseVisualStyleBackColor = false;
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.White;
            this.pnl_Bottom.BorderColor = System.Drawing.Color.White;
            this.pnl_Bottom.Controls.Add(this.lblUpdatesBuilitin);
            this.pnl_Bottom.Controls.Add(this.btn_PicShowMessage);
            this.pnl_Bottom.Edge = 20;
            resources.ApplyResources(this.pnl_Bottom, "pnl_Bottom");
            this.pnl_Bottom.Name = "pnl_Bottom";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel2.Controls.Add(this.btn_Reservation);
            this.panel2.Controls.Add(this.btn_BillingDetail);
            this.panel2.Controls.Add(this.btn_CheckOut);
            this.panel2.Controls.Add(this.btn_CheckIn);
            this.panel2.Controls.Add(this.btn_SyncAll);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pnl_BackGrid
            // 
            this.pnl_BackGrid.BackColor = System.Drawing.Color.White;
            this.pnl_BackGrid.BorderColor = System.Drawing.Color.White;
            this.pnl_BackGrid.Controls.Add(this.grvSyncData);
            this.pnl_BackGrid.Edge = 20;
            resources.ApplyResources(this.pnl_BackGrid, "pnl_BackGrid");
            this.pnl_BackGrid.Name = "pnl_BackGrid";
            // 
            // btnHistory
            // 
            resources.ApplyResources(this.btnHistory, "btnHistory");
            this.btnHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.TabStop = false;
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // BDUSyncForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ControlBox = false;
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.pnl_LastSyncTime);
            this.Controls.Add(this.btn_SyncAllPMS);
            this.Controls.Add(this.btn_AutoSync);
            this.Controls.Add(this.btn_ClearIndicators);
            this.Controls.Add(this.pnl_BackGrid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnl_Bottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BDUSyncForm";
            this.ShowIcon = false;
            this.Tag = "frmSync";
            this.Load += new System.EventHandler(this.BDUSyncForm_Load);
            this.Shown += new System.EventHandler(this.BDUSyncForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BDUSyncForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grvSyncData)).EndInit();
            this.pnl_LastSyncTime.ResumeLayout(false);
            this.pnl_LastSyncTime.PerformLayout();
            this.pnl_Bottom.ResumeLayout(false);
            this.pnl_Bottom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnl_BackGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.ColumnHeader columnHeader28;
        //private System.Windows.Forms.ColumnHeader columnHeader27;
        //private System.Windows.Forms.ColumnHeader columnHeader26;
        //private System.Windows.Forms.ColumnHeader columnHeader25;
        //private System.Windows.Forms.ColumnHeader columnHeader24;
        //private System.Windows.Forms.ColumnHeader columnHeader23;
        //private System.Windows.Forms.ColumnHeader columnHeader22;
        //private System.Windows.Forms.ColumnHeader columnHeader21;
        //private System.Windows.Forms.ColumnHeader columnHeader20;
        //private System.Windows.Forms.ColumnHeader columnHeader19;
        //private System.Windows.Forms.ColumnHeader columnHeader18;
        //private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.TabPage tabPage1;
        //private System.Windows.Forms.ColumnHeader columnHeader2;
        //private System.Windows.Forms.ColumnHeader columnHeader1;
        //private System.Windows.Forms.ColumnHeader columnHeader16;
        //private System.Windows.Forms.ColumnHeader columnHeader15;
        //private System.Windows.Forms.ColumnHeader columnHeader14;
        //private System.Windows.Forms.ColumnHeader columnHeader13;
        //private System.Windows.Forms.ColumnHeader columnHeader12;
        //private System.Windows.Forms.ColumnHeader columnHeader11;
        //private System.Windows.Forms.ColumnHeader columnHeader10;
        //private System.Windows.Forms.ColumnHeader columnHeader9;
        //private System.Windows.Forms.ColumnHeader columnHeader8;
        //private System.Windows.Forms.ColumnHeader columnHeader7;
        //private System.Windows.Forms.ColumnHeader columnHeader5;
        //private System.Windows.Forms.ColumnHeader columnHeader4;
        //private System.Windows.Forms.ColumnHeader columnHeader3;
        //private System.Windows.Forms.ColumnHeader columnHeader29;
        //private System.Windows.Forms.ColumnHeader columnHeader30;
        //private System.Windows.Forms.ColumnHeader columnHeader31;
        //private System.Windows.Forms.ColumnHeader columnHeader32;
        //private System.Windows.Forms.ColumnHeader columnHeader33;
        //private System.Windows.Forms.ColumnHeader columnHeader34;
        //private System.Windows.Forms.ColumnHeader columnHeader35;
        //private System.Windows.Forms.ColumnHeader columnHeader36;
        //private System.Windows.Forms.ColumnHeader columnHeader37;
        //private System.Windows.Forms.ColumnHeader columnHeader38;
       // private System.Windows.Forms.ListView List;
        //private System.Windows.Forms.ColumnHeader columnHeader6;
        //private System.Windows.Forms.ColumnHeader columnHeader39;
        //private System.Windows.Forms.ColumnHeader columnHeader40;
        //private System.Windows.Forms.ColumnHeader columnHeader41;
       // private System.Windows.Forms.ListView listView2;
        //private System.Windows.Forms.ColumnHeader columnHeader42;
        //private System.Windows.Forms.ColumnHeader columnHeader43;
        //private System.Windows.Forms.ColumnHeader columnHeader44;
        //private System.Windows.Forms.ColumnHeader columnHeader45;
        //private System.Windows.Forms.ColumnHeader columnHeader46;
        //private System.Windows.Forms.ColumnHeader columnHeader47;
        //private System.Windows.Forms.ColumnHeader columnHeader48;
        //private System.Windows.Forms.ColumnHeader columnHeader49;
        //private System.Windows.Forms.ColumnHeader columnHeader50;
        //private System.Windows.Forms.ColumnHeader columnHeader51;
        //private System.Windows.Forms.ColumnHeader columnHeader52;
        //private System.Windows.Forms.ColumnHeader columnHeader53;
        //private System.Windows.Forms.ColumnHeader columnHeader54;
        //private System.Windows.Forms.ColumnHeader columnHeader55;
        //private System.Windows.Forms.ColumnHeader columnHeader56;
        //private System.Windows.Forms.ColumnHeader columnHeader57;
        //private System.Windows.Forms.ColumnHeader columnHeader58;
        //private System.Windows.Forms.ColumnHeader columnHeader59;
        //private System.Windows.Forms.ColumnHeader columnHeader60;
        //private System.Windows.Forms.ColumnHeader columnHeader61;
        //private System.Windows.Forms.ColumnHeader columnHeader62;
        //private System.Windows.Forms.ColumnHeader columnHeader63;
        //private System.Windows.Forms.ColumnHeader columnHeader64;
        //private System.Windows.Forms.ColumnHeader columnHeader65;
     
        private System.Windows.Forms.ToolTip ttp_Menus;
       // private System.Windows.Forms.DataGridView grvSyncDataXX;
        private System.Windows.Forms.Button btn_PicShowMessage;
        private System.Windows.Forms.Label lblUpdatesBuilitin;
       
        private System.Windows.Forms.DataGridView grvSyncData;
       // private BDU.Extensions.ServrButton servrButton1;
        private BDU.Extensions.SPanel pnl_Bottom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_CheckOut;
        private System.Windows.Forms.Button btn_CheckIn;
        private BDU.Extensions.SPanel pnl_BackGrid;
        private System.Windows.Forms.Button btn_BillingDetail;
        private BDU.Extensions.ServrButton btn_SyncAllPMS;
        private BDU.Extensions.ServrButton btn_AutoSync;
        private BDU.Extensions.ServrButton btn_ClearIndicators;
        private System.Windows.Forms.Panel pnl_LastSyncTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLastSyncedDateShow;
        public System.Windows.Forms.Button btn_SyncAll;
        private System.Windows.Forms.Button btn_Reservation;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn srCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeCol;
        private System.Windows.Forms.DataGridViewImageColumn actionCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdReference;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntityId;
        //  private System.Windows.Forms.Button button1;
    }
}