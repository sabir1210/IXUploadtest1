
namespace servr.integratex.ui
{
    partial class BDUHistoryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDUHistoryForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_GridBack = new BDU.Extensions.SPanel();
            this.grvHistoryData = new System.Windows.Forms.DataGridView();
            this.ColId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReservationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColGuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColRoomNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCurrentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColArrivalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColReceiveTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new BDU.Extensions.ServrButton();
            this.txtKeywordSearch = new BDU.Extensions.ServrInputControl();
            this.pnl_Bottom = new BDU.Extensions.SPanel();
            this.lblRecords = new System.Windows.Forms.Label();
            this.tgl_Active_Inactive = new BDU.Extensions.ToggleButton();
            this.pnl_GridBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvHistoryData)).BeginInit();
            this.pnl_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_GridBack
            // 
            this.pnl_GridBack.BackColor = System.Drawing.Color.White;
            this.pnl_GridBack.BorderColor = System.Drawing.Color.White;
            this.pnl_GridBack.Controls.Add(this.grvHistoryData);
            this.pnl_GridBack.Edge = 20;
            resources.ApplyResources(this.pnl_GridBack, "pnl_GridBack");
            this.pnl_GridBack.Name = "pnl_GridBack";
            // 
            // grvHistoryData
            // 
            this.grvHistoryData.AllowUserToAddRows = false;
            this.grvHistoryData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grvHistoryData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.grvHistoryData, "grvHistoryData");
            this.grvHistoryData.BackgroundColor = System.Drawing.Color.White;
            this.grvHistoryData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvHistoryData.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenHorizontal;
            this.grvHistoryData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 9, 0, 15);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvHistoryData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grvHistoryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvHistoryData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColId,
            this.colReservationNo,
            this.colEntity,
            this.ColGuestName,
            this.ColRoomNo,
            this.ColCurrentStatus,
            this.ColSource,
            this.ColArrivalDate,
            this.ColReceiveTime});
            this.grvHistoryData.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvHistoryData.DefaultCellStyle = dataGridViewCellStyle11;
            this.grvHistoryData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grvHistoryData.GridColor = System.Drawing.Color.White;
            this.grvHistoryData.MultiSelect = false;
            this.grvHistoryData.Name = "grvHistoryData";
            this.grvHistoryData.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvHistoryData.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(184)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            this.grvHistoryData.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.grvHistoryData.RowTemplate.Height = 40;
            this.grvHistoryData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvHistoryData.TabStop = false;
            // 
            // ColId
            // 
            this.ColId.DataPropertyName = "uid";
            this.ColId.Frozen = true;
            resources.ApplyResources(this.ColId, "ColId");
            this.ColId.Name = "ColId";
            // 
            // colReservationNo
            // 
            this.colReservationNo.DataPropertyName = "Reference";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.colReservationNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colReservationNo.Frozen = true;
            resources.ApplyResources(this.colReservationNo, "colReservationNo");
            this.colReservationNo.Name = "colReservationNo";
            this.colReservationNo.ReadOnly = true;
            // 
            // colEntity
            // 
            this.colEntity.DataPropertyName = "entity";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.colEntity.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.colEntity, "colEntity");
            this.colEntity.Name = "colEntity";
            this.colEntity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ColGuestName
            // 
            this.ColGuestName.DataPropertyName = "guestname";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColGuestName.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.ColGuestName, "ColGuestName");
            this.ColGuestName.Name = "ColGuestName";
            // 
            // ColRoomNo
            // 
            this.ColRoomNo.DataPropertyName = "roomno";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColRoomNo.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.ColRoomNo, "ColRoomNo");
            this.ColRoomNo.Name = "ColRoomNo";
            // 
            // ColCurrentStatus
            // 
            this.ColCurrentStatus.DataPropertyName = "Status";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColCurrentStatus.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.ColCurrentStatus, "ColCurrentStatus");
            this.ColCurrentStatus.Name = "ColCurrentStatus";
            // 
            // ColSource
            // 
            this.ColSource.DataPropertyName = "mode";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColSource.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.ColSource, "ColSource");
            this.ColSource.Name = "ColSource";
            // 
            // ColArrivalDate
            // 
            this.ColArrivalDate.DataPropertyName = "ArrivalDate";
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColArrivalDate.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.ColArrivalDate, "ColArrivalDate");
            this.ColArrivalDate.Name = "ColArrivalDate";
            // 
            // ColReceiveTime
            // 
            this.ColReceiveTime.DataPropertyName = "TransactionDate";
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.ColReceiveTime.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.ColReceiveTime, "ColReceiveTime");
            this.ColReceiveTime.Name = "ColReceiveTime";
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
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RootElement = null;
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtKeywordSearch
            // 
            this.txtKeywordSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtKeywordSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtKeywordSearch.BorderSize = 1;
            this.txtKeywordSearch.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            resources.ApplyResources(this.txtKeywordSearch, "txtKeywordSearch");
            this.txtKeywordSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtKeywordSearch.Name = "txtKeywordSearch";
            this.txtKeywordSearch.PasswordChar = '\0';
            this.txtKeywordSearch.PlaceHolderText = "";
            this.txtKeywordSearch.Tag = "KeywordSearch";
            this.txtKeywordSearch.textboxRadius = 18;
            this.txtKeywordSearch.UseSystemPasswordChar = false;
            this.txtKeywordSearch.TextChanged += new System.EventHandler(this.txtKeywordSearch_TextChanged);
            // 
            // pnl_Bottom
            // 
            this.pnl_Bottom.BackColor = System.Drawing.Color.White;
            this.pnl_Bottom.BorderColor = System.Drawing.Color.White;
            this.pnl_Bottom.Controls.Add(this.lblRecords);
            this.pnl_Bottom.Controls.Add(this.btnCancel);
            this.pnl_Bottom.Edge = 20;
            resources.ApplyResources(this.pnl_Bottom, "pnl_Bottom");
            this.pnl_Bottom.Name = "pnl_Bottom";
            // 
            // lblRecords
            // 
            resources.ApplyResources(this.lblRecords, "lblRecords");
            this.lblRecords.Name = "lblRecords";
            // 
            // tgl_Active_Inactive
            // 
            this.tgl_Active_Inactive.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
            this.tgl_Active_Inactive.ActiveText = "Active  ";
            this.tgl_Active_Inactive.BackColor = System.Drawing.Color.Transparent;
            this.tgl_Active_Inactive.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.tgl_Active_Inactive, "tgl_Active_Inactive");
            this.tgl_Active_Inactive.ForeColor = System.Drawing.Color.White;
            this.tgl_Active_Inactive.InActiveColor = System.Drawing.Color.Red;
            this.tgl_Active_Inactive.InActiveText = "   All  ";
            this.tgl_Active_Inactive.Name = "tgl_Active_Inactive";
            this.tgl_Active_Inactive.SliderColor = System.Drawing.Color.Blue;
            this.tgl_Active_Inactive.SlidingAngle = 8;
            this.tgl_Active_Inactive.Tag = "2";
            this.tgl_Active_Inactive.TextColor = System.Drawing.Color.White;
            this.tgl_Active_Inactive.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;
            this.tgl_Active_Inactive.ToggleStyle = BDU.Extensions.ToggleButton.ToggleButtonStyle.IOS;
            this.tgl_Active_Inactive.ButtonStateChanged += new BDU.Extensions.ToggleButton.ToggleButtonStateChanged(this.tgl_Active_Inactive_ButtonStateChanged);
            // 
            // BDUHistoryForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.tgl_Active_Inactive);
            this.Controls.Add(this.pnl_Bottom);
            this.Controls.Add(this.txtKeywordSearch);
            this.Controls.Add(this.pnl_GridBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BDUHistoryForm";
            this.Tag = "BDUHistoryForm";
            this.Load += new System.EventHandler(this.BDUHistoryForm_Load);
            this.pnl_GridBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvHistoryData)).EndInit();
            this.pnl_Bottom.ResumeLayout(false);
            this.pnl_Bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BDU.Extensions.SPanel pnl_GridBack;
        private System.Windows.Forms.DataGridView grvHistoryData;
        private BDU.Extensions.ServrButton btnCancel;
        private BDU.Extensions.ServrInputControl txtKeywordSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTime;
        private BDU.Extensions.SPanel pnl_Bottom;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReservationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColGuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColRoomNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCurrentStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColArrivalDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColReceiveTime;
        private BDU.Extensions.ToggleButton tgl_Active_Inactive;
    }
}