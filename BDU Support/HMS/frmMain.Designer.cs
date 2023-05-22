using System.Windows.Forms;

namespace BDU.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        ToolTip tooltip = new ToolTip();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btn_Logout = new BDU.Extensions.ServrButton();
            this.btn_BDUSync = new BDU.Extensions.ServrButton();
            this.btnDemo = new BDU.Extensions.ServrButton();
            this.pnl_SyncControl = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblNewOrderName = new System.Windows.Forms.Label();
            this.lbl_RestaurensVal = new System.Windows.Forms.LinkLabel();
            this.lbl_Experiences_Val = new System.Windows.Forms.LinkLabel();
            this.lblConcierge_Val = new System.Windows.Forms.LinkLabel();
            this.btn_Experiences = new BDU.Extensions.ServrButton();
            this.btn_Concierge = new BDU.Extensions.ServrButton();
            this.lblSpa_Val = new System.Windows.Forms.LinkLabel();
            this.btnSpa = new BDU.Extensions.ServrButton();
            this.btn_Restaurans = new BDU.Extensions.ServrButton();
            this.btnPreference = new BDU.Extensions.ServrButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Mapping = new BDU.Extensions.ServrButton();
            this.Pic_HotelLogo = new System.Windows.Forms.PictureBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem24 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem26 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem27 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem28 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem29 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem30 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem31 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem32 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem33 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem34 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem35 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem36 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem37 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem38 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem39 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem40 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem41 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem42 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem43 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem44 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem45 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem46 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem47 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem48 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem49 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem50 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem51 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem52 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem53 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem54 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem55 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem56 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem57 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem58 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem59 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem60 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem61 = new System.Windows.Forms.ToolStripMenuItem();
            this.ttp_Menus = new System.Windows.Forms.ToolTip(this.components);
            this.btn_MainMenuClose = new System.Windows.Forms.Button();
            this.btn_Minimize = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem62 = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent.SuspendLayout();
            this.pnl_SyncControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_HotelLogo)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.btn_Logout);
            this.pnlContent.Controls.Add(this.btn_BDUSync);
            this.pnlContent.Controls.Add(this.btnDemo);
            this.pnlContent.Controls.Add(this.pnl_SyncControl);
            this.pnlContent.Controls.Add(this.btnPreference);
            this.pnlContent.Controls.Add(this.pictureBox1);
            this.pnlContent.Controls.Add(this.btn_Mapping);
            this.pnlContent.Controls.Add(this.Pic_HotelLogo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(0);
            this.pnlContent.MaximumSize = new System.Drawing.Size(187, 697);
            this.pnlContent.MinimumSize = new System.Drawing.Size(187, 697);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(187, 697);
            this.pnlContent.TabIndex = 6;
            // 
            // btn_Logout
            // 
            this.btn_Logout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Logout.BackColor = System.Drawing.Color.White;
            this.btn_Logout.BackgroundColor = System.Drawing.Color.White;
            this.btn_Logout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_Logout.BorderRadius = 40;
            this.btn_Logout.BorderSize = 0;
            this.btn_Logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Logout.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Logout.FlatAppearance.BorderSize = 0;
            this.btn_Logout.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Logout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Logout.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Logout.Image = global::BDU.UI.Properties.Resources.LogOut;
            this.btn_Logout.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Logout.Location = new System.Drawing.Point(22, 360);
            this.btn_Logout.MaximumSize = new System.Drawing.Size(182, 45);
            this.btn_Logout.MinimumSize = new System.Drawing.Size(182, 45);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Padding = new System.Windows.Forms.Padding(8, 0, 30, 1);
            this.btn_Logout.RootElement = null;
            this.btn_Logout.Size = new System.Drawing.Size(182, 45);
            this.btn_Logout.TabIndex = 28;
            this.btn_Logout.TabStop = false;
            this.btn_Logout.Tag = "ServrMessageBox";
            this.btn_Logout.Text = "Log Out";
            this.btn_Logout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Logout.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Logout.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Logout, "LogOut & Close application");
            this.btn_Logout.UseVisualStyleBackColor = false;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // btn_BDUSync
            // 
            this.btn_BDUSync.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_BDUSync.BackColor = System.Drawing.Color.White;
            this.btn_BDUSync.BackgroundColor = System.Drawing.Color.White;
            this.btn_BDUSync.BorderColor = System.Drawing.Color.Empty;
            this.btn_BDUSync.BorderRadius = 40;
            this.btn_BDUSync.BorderSize = 0;
            this.btn_BDUSync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BDUSync.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_BDUSync.FlatAppearance.BorderSize = 0;
            this.btn_BDUSync.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_BDUSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_BDUSync.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_BDUSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BDUSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_BDUSync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_BDUSync.Image = global::BDU.UI.Properties.Resources.Sync;
            this.btn_BDUSync.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BDUSync.Location = new System.Drawing.Point(22, 155);
            this.btn_BDUSync.MaximumSize = new System.Drawing.Size(182, 45);
            this.btn_BDUSync.MinimumSize = new System.Drawing.Size(182, 45);
            this.btn_BDUSync.Name = "btn_BDUSync";
            this.btn_BDUSync.Padding = new System.Windows.Forms.Padding(8, 0, 30, 1);
            this.btn_BDUSync.RootElement = null;
            this.btn_BDUSync.Size = new System.Drawing.Size(182, 45);
            this.btn_BDUSync.TabIndex = 24;
            this.btn_BDUSync.TabStop = false;
            this.btn_BDUSync.Tag = "BDUSyncForm";
            this.btn_BDUSync.Text = "Sync";
            this.btn_BDUSync.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_BDUSync.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_BDUSync.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_BDUSync, "Push & Pull Data");
            this.btn_BDUSync.UseVisualStyleBackColor = false;
            this.btn_BDUSync.Click += new System.EventHandler(this.btn_BDUSync_Click);
            // 
            // btnDemo
            // 
            this.btnDemo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDemo.BackColor = System.Drawing.Color.White;
            this.btnDemo.BackgroundColor = System.Drawing.Color.White;
            this.btnDemo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnDemo.BorderRadius = 40;
            this.btnDemo.BorderSize = 0;
            this.btnDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDemo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDemo.FlatAppearance.BorderSize = 0;
            this.btnDemo.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnDemo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDemo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnDemo.Image = global::BDU.UI.Properties.Resources.video_play;
            this.btnDemo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDemo.Location = new System.Drawing.Point(22, 308);
            this.btnDemo.MaximumSize = new System.Drawing.Size(182, 45);
            this.btnDemo.MinimumSize = new System.Drawing.Size(182, 45);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Padding = new System.Windows.Forms.Padding(8, 0, 30, 1);
            this.btnDemo.RootElement = null;
            this.btnDemo.Size = new System.Drawing.Size(182, 45);
            this.btnDemo.TabIndex = 27;
            this.btnDemo.TabStop = false;
            this.btnDemo.Tag = "";
            this.btnDemo.Text = "Tutorial";
            this.btnDemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDemo.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnDemo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btnDemo, "Video Tutorial link");
            this.btnDemo.UseVisualStyleBackColor = false;
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // pnl_SyncControl
            // 
            this.pnl_SyncControl.BackgroundImage = global::BDU.UI.Properties.Resources.Pic_SyncControl;
            this.pnl_SyncControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnl_SyncControl.Controls.Add(this.pictureBox2);
            this.pnl_SyncControl.Controls.Add(this.panel12);
            this.pnl_SyncControl.Controls.Add(this.panel8);
            this.pnl_SyncControl.Controls.Add(this.panel4);
            this.pnl_SyncControl.Controls.Add(this.lblNewOrderName);
            this.pnl_SyncControl.Controls.Add(this.lbl_RestaurensVal);
            this.pnl_SyncControl.Controls.Add(this.lbl_Experiences_Val);
            this.pnl_SyncControl.Controls.Add(this.lblConcierge_Val);
            this.pnl_SyncControl.Controls.Add(this.btn_Experiences);
            this.pnl_SyncControl.Controls.Add(this.btn_Concierge);
            this.pnl_SyncControl.Controls.Add(this.lblSpa_Val);
            this.pnl_SyncControl.Controls.Add(this.btnSpa);
            this.pnl_SyncControl.Controls.Add(this.btn_Restaurans);
            this.pnl_SyncControl.Location = new System.Drawing.Point(3, 431);
            this.pnl_SyncControl.MaximumSize = new System.Drawing.Size(180, 210);
            this.pnl_SyncControl.MinimumSize = new System.Drawing.Size(180, 210);
            this.pnl_SyncControl.Name = "pnl_SyncControl";
            this.pnl_SyncControl.Size = new System.Drawing.Size(180, 210);
            this.pnl_SyncControl.TabIndex = 15;
            this.pnl_SyncControl.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::BDU.UI.Properties.Resources.Sync_info;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(133, 17);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(18, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 41;
            this.pictureBox2.TabStop = false;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.LightGray;
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Controls.Add(this.panel15);
            this.panel12.Location = new System.Drawing.Point(24, 154);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(139, 2);
            this.panel12.TabIndex = 40;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.LightGray;
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Location = new System.Drawing.Point(0, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(2, 555);
            this.panel13.TabIndex = 20;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.LightGray;
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(2, 555);
            this.panel14.TabIndex = 19;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.LightGray;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(2, 555);
            this.panel15.TabIndex = 19;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightGray;
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Location = new System.Drawing.Point(24, 76);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(139, 2);
            this.panel8.TabIndex = 39;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.LightGray;
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(2, 555);
            this.panel9.TabIndex = 20;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.LightGray;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(2, 555);
            this.panel10.TabIndex = 19;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.LightGray;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(2, 555);
            this.panel11.TabIndex = 19;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGray;
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Location = new System.Drawing.Point(23, 114);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(139, 2);
            this.panel4.TabIndex = 38;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightGray;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(2, 555);
            this.panel5.TabIndex = 20;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGray;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(2, 555);
            this.panel6.TabIndex = 19;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightGray;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(2, 555);
            this.panel7.TabIndex = 19;
            // 
            // lblNewOrderName
            // 
            this.lblNewOrderName.AutoSize = true;
            this.lblNewOrderName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.lblNewOrderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNewOrderName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblNewOrderName.Location = new System.Drawing.Point(24, 14);
            this.lblNewOrderName.Name = "lblNewOrderName";
            this.lblNewOrderName.Size = new System.Drawing.Size(99, 20);
            this.lblNewOrderName.TabIndex = 37;
            this.lblNewOrderName.Text = "New orders";
            this.ttp_Menus.SetToolTip(this.lblNewOrderName, "Last Sync Time");
            // 
            // lbl_RestaurensVal
            // 
            this.lbl_RestaurensVal.AutoSize = true;
            this.lbl_RestaurensVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.lbl_RestaurensVal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_RestaurensVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_RestaurensVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbl_RestaurensVal.LinkColor = System.Drawing.Color.Black;
            this.lbl_RestaurensVal.Location = new System.Drawing.Point(122, 167);
            this.lbl_RestaurensVal.MaximumSize = new System.Drawing.Size(41, 20);
            this.lbl_RestaurensVal.MinimumSize = new System.Drawing.Size(41, 20);
            this.lbl_RestaurensVal.Name = "lbl_RestaurensVal";
            this.lbl_RestaurensVal.Size = new System.Drawing.Size(41, 20);
            this.lbl_RestaurensVal.TabIndex = 7;
            this.lbl_RestaurensVal.TabStop = true;
            this.lbl_RestaurensVal.Text = "0";
            this.lbl_RestaurensVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_RestaurensVal.Click += new System.EventHandler(this.btn_Restaurans_Click);
            // 
            // lbl_Experiences_Val
            // 
            this.lbl_Experiences_Val.AutoSize = true;
            this.lbl_Experiences_Val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.lbl_Experiences_Val.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Experiences_Val.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_Experiences_Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbl_Experiences_Val.LinkColor = System.Drawing.Color.Black;
            this.lbl_Experiences_Val.Location = new System.Drawing.Point(122, 126);
            this.lbl_Experiences_Val.MaximumSize = new System.Drawing.Size(41, 20);
            this.lbl_Experiences_Val.MinimumSize = new System.Drawing.Size(41, 20);
            this.lbl_Experiences_Val.Name = "lbl_Experiences_Val";
            this.lbl_Experiences_Val.Size = new System.Drawing.Size(41, 20);
            this.lbl_Experiences_Val.TabIndex = 6;
            this.lbl_Experiences_Val.TabStop = true;
            this.lbl_Experiences_Val.Text = "0";
            this.lbl_Experiences_Val.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_Experiences_Val.Click += new System.EventHandler(this.btn_Experiences_Click);
            // 
            // lblConcierge_Val
            // 
            this.lblConcierge_Val.AutoSize = true;
            this.lblConcierge_Val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.lblConcierge_Val.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblConcierge_Val.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblConcierge_Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblConcierge_Val.LinkColor = System.Drawing.Color.Black;
            this.lblConcierge_Val.Location = new System.Drawing.Point(122, 86);
            this.lblConcierge_Val.MaximumSize = new System.Drawing.Size(41, 20);
            this.lblConcierge_Val.MinimumSize = new System.Drawing.Size(41, 20);
            this.lblConcierge_Val.Name = "lblConcierge_Val";
            this.lblConcierge_Val.Size = new System.Drawing.Size(41, 20);
            this.lblConcierge_Val.TabIndex = 7;
            this.lblConcierge_Val.TabStop = true;
            this.lblConcierge_Val.Text = "0";
            this.lblConcierge_Val.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblConcierge_Val.Click += new System.EventHandler(this.btn_Concierge_Click);
            // 
            // btn_Experiences
            // 
            this.btn_Experiences.BackColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.BackgroundColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Experiences.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.BorderRadius = 0;
            this.btn_Experiences.BorderSize = 0;
            this.btn_Experiences.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Experiences.FlatAppearance.BorderSize = 0;
            this.btn_Experiences.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Experiences.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Experiences.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Experiences.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Experiences.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Experiences.Location = new System.Drawing.Point(17, 120);
            this.btn_Experiences.MaximumSize = new System.Drawing.Size(145, 30);
            this.btn_Experiences.MinimumSize = new System.Drawing.Size(145, 30);
            this.btn_Experiences.Name = "btn_Experiences";
            this.btn_Experiences.Padding = new System.Windows.Forms.Padding(1, 0, 0, 3);
            this.btn_Experiences.RootElement = null;
            this.btn_Experiences.Size = new System.Drawing.Size(145, 30);
            this.btn_Experiences.TabIndex = 32;
            this.btn_Experiences.TabStop = false;
            this.btn_Experiences.Tag = "ServrMessageBox";
            this.btn_Experiences.Text = "Experiences";
            this.btn_Experiences.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Experiences.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Experiences.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Experiences, "LogOut & Close application");
            this.btn_Experiences.UseVisualStyleBackColor = false;
            this.btn_Experiences.Click += new System.EventHandler(this.btn_Experiences_Click);
            // 
            // btn_Concierge
            // 
            this.btn_Concierge.BackColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.BackgroundColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Concierge.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.BorderRadius = 0;
            this.btn_Concierge.BorderSize = 0;
            this.btn_Concierge.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Concierge.FlatAppearance.BorderSize = 0;
            this.btn_Concierge.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Concierge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Concierge.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Concierge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Concierge.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Concierge.Location = new System.Drawing.Point(17, 81);
            this.btn_Concierge.MaximumSize = new System.Drawing.Size(145, 30);
            this.btn_Concierge.MinimumSize = new System.Drawing.Size(145, 30);
            this.btn_Concierge.Name = "btn_Concierge";
            this.btn_Concierge.Padding = new System.Windows.Forms.Padding(1, 0, 0, 3);
            this.btn_Concierge.RootElement = null;
            this.btn_Concierge.Size = new System.Drawing.Size(145, 30);
            this.btn_Concierge.TabIndex = 31;
            this.btn_Concierge.TabStop = false;
            this.btn_Concierge.Tag = "ServrMessageBox";
            this.btn_Concierge.Text = "Concierge";
            this.btn_Concierge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Concierge.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Concierge.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Concierge, "LogOut & Close application");
            this.btn_Concierge.UseVisualStyleBackColor = false;
            this.btn_Concierge.Click += new System.EventHandler(this.btn_Concierge_Click);
            // 
            // lblSpa_Val
            // 
            this.lblSpa_Val.AutoSize = true;
            this.lblSpa_Val.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.lblSpa_Val.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSpa_Val.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSpa_Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblSpa_Val.LinkColor = System.Drawing.Color.Black;
            this.lblSpa_Val.Location = new System.Drawing.Point(122, 49);
            this.lblSpa_Val.Margin = new System.Windows.Forms.Padding(0);
            this.lblSpa_Val.MaximumSize = new System.Drawing.Size(41, 20);
            this.lblSpa_Val.MinimumSize = new System.Drawing.Size(41, 20);
            this.lblSpa_Val.Name = "lblSpa_Val";
            this.lblSpa_Val.Size = new System.Drawing.Size(41, 20);
            this.lblSpa_Val.TabIndex = 6;
            this.lblSpa_Val.TabStop = true;
            this.lblSpa_Val.Text = "0";
            this.lblSpa_Val.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblSpa_Val.Click += new System.EventHandler(this.btnSpa_Click);
            // 
            // btnSpa
            // 
            this.btnSpa.BackColor = System.Drawing.Color.Transparent;
            this.btnSpa.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnSpa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpa.BorderColor = System.Drawing.Color.Transparent;
            this.btnSpa.BorderRadius = 0;
            this.btnSpa.BorderSize = 0;
            this.btnSpa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSpa.FlatAppearance.BorderSize = 0;
            this.btnSpa.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSpa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSpa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSpa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSpa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSpa.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpa.Location = new System.Drawing.Point(17, 43);
            this.btnSpa.MaximumSize = new System.Drawing.Size(145, 30);
            this.btnSpa.MinimumSize = new System.Drawing.Size(145, 30);
            this.btnSpa.Name = "btnSpa";
            this.btnSpa.Padding = new System.Windows.Forms.Padding(1, 0, 0, 3);
            this.btnSpa.RootElement = null;
            this.btnSpa.Size = new System.Drawing.Size(145, 30);
            this.btnSpa.TabIndex = 30;
            this.btnSpa.TabStop = false;
            this.btnSpa.Tag = "ServrMessageBox";
            this.btnSpa.Text = "Spa";
            this.btnSpa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpa.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnSpa.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btnSpa, "LogOut & Close application");
            this.btnSpa.UseVisualStyleBackColor = false;
            this.btnSpa.Click += new System.EventHandler(this.btnSpa_Click);
            // 
            // btn_Restaurans
            // 
            this.btn_Restaurans.BackColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.BackgroundColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Restaurans.BorderColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.BorderRadius = 0;
            this.btn_Restaurans.BorderSize = 0;
            this.btn_Restaurans.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Restaurans.FlatAppearance.BorderSize = 0;
            this.btn_Restaurans.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Restaurans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Restaurans.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Restaurans.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Restaurans.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Restaurans.Location = new System.Drawing.Point(17, 160);
            this.btn_Restaurans.MaximumSize = new System.Drawing.Size(145, 30);
            this.btn_Restaurans.MinimumSize = new System.Drawing.Size(145, 30);
            this.btn_Restaurans.Name = "btn_Restaurans";
            this.btn_Restaurans.Padding = new System.Windows.Forms.Padding(1, 0, 0, 3);
            this.btn_Restaurans.RootElement = null;
            this.btn_Restaurans.Size = new System.Drawing.Size(145, 30);
            this.btn_Restaurans.TabIndex = 33;
            this.btn_Restaurans.TabStop = false;
            this.btn_Restaurans.Tag = "ServrMessageBox";
            this.btn_Restaurans.Text = "Restaurans";
            this.btn_Restaurans.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Restaurans.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Restaurans.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Restaurans, "LogOut & Close application");
            this.btn_Restaurans.UseVisualStyleBackColor = false;
            this.btn_Restaurans.Click += new System.EventHandler(this.btn_Restaurans_Click);
            // 
            // btnPreference
            // 
            this.btnPreference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreference.BackColor = System.Drawing.Color.White;
            this.btnPreference.BackgroundColor = System.Drawing.Color.White;
            this.btnPreference.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnPreference.BorderRadius = 40;
            this.btnPreference.BorderSize = 0;
            this.btnPreference.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreference.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPreference.FlatAppearance.BorderSize = 0;
            this.btnPreference.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnPreference.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPreference.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btnPreference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPreference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPreference.Image = global::BDU.UI.Properties.Resources.settings;
            this.btnPreference.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreference.Location = new System.Drawing.Point(22, 257);
            this.btnPreference.MaximumSize = new System.Drawing.Size(182, 45);
            this.btnPreference.MinimumSize = new System.Drawing.Size(182, 45);
            this.btnPreference.Name = "btnPreference";
            this.btnPreference.Padding = new System.Windows.Forms.Padding(8, 0, 30, 1);
            this.btnPreference.RootElement = null;
            this.btnPreference.Size = new System.Drawing.Size(182, 45);
            this.btnPreference.TabIndex = 26;
            this.btnPreference.TabStop = false;
            this.btnPreference.Tag = "BDUPreferencesForm";
            this.btnPreference.Text = "Preference";
            this.btnPreference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreference.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnPreference.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btnPreference, "Notification, Visualizer & API Settings");
            this.btnPreference.UseVisualStyleBackColor = false;
            this.btnPreference.Click += new System.EventHandler(this.btnPreference_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(6, 145);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 1);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Mapping
            // 
            this.btn_Mapping.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Mapping.BackColor = System.Drawing.Color.White;
            this.btn_Mapping.BackgroundColor = System.Drawing.Color.White;
            this.btn_Mapping.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btn_Mapping.BorderRadius = 40;
            this.btn_Mapping.BorderSize = 0;
            this.btn_Mapping.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Mapping.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Mapping.FlatAppearance.BorderSize = 0;
            this.btn_Mapping.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Mapping.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Mapping.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_Mapping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Mapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Mapping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Mapping.Image = global::BDU.UI.Properties.Resources.map;
            this.btn_Mapping.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Mapping.Location = new System.Drawing.Point(22, 206);
            this.btn_Mapping.MaximumSize = new System.Drawing.Size(182, 45);
            this.btn_Mapping.MinimumSize = new System.Drawing.Size(182, 45);
            this.btn_Mapping.Name = "btn_Mapping";
            this.btn_Mapping.Padding = new System.Windows.Forms.Padding(8, 0, 30, 1);
            this.btn_Mapping.RootElement = null;
            this.btn_Mapping.Size = new System.Drawing.Size(182, 45);
            this.btn_Mapping.TabIndex = 25;
            this.btn_Mapping.TabStop = false;
            this.btn_Mapping.Tag = "BDUMappingForm";
            this.btn_Mapping.Text = "Mapping";
            this.btn_Mapping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Mapping.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btn_Mapping.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Mapping, "Form & Field mapping configuration");
            this.btn_Mapping.UseVisualStyleBackColor = false;
            this.btn_Mapping.Click += new System.EventHandler(this.btn_Mapping_Click);
            // 
            // Pic_HotelLogo
            // 
            this.Pic_HotelLogo.BackgroundImage = global::BDU.UI.Properties.Resources.logo_Servr_Rounded_BackTransparent;
            this.Pic_HotelLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pic_HotelLogo.Location = new System.Drawing.Point(42, 36);
            this.Pic_HotelLogo.Name = "Pic_HotelLogo";
            this.Pic_HotelLogo.Size = new System.Drawing.Size(105, 105);
            this.Pic_HotelLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_HotelLogo.TabIndex = 9;
            this.Pic_HotelLogo.TabStop = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 29);
            this.toolStripMenuItem1.Tag = "mnu_administration";
            this.toolStripMenuItem1.Text = "Administration";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 24);
            this.toolStripMenuItem2.Tag = "usergroups";
            this.toolStripMenuItem2.Text = "Groups";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem3.Size = new System.Drawing.Size(129, 24);
            this.toolStripMenuItem3.Tag = "users";
            this.toolStripMenuItem3.Text = "&Users";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem4.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(79, 29);
            this.toolStripMenuItem4.Tag = "mnu_settings";
            this.toolStripMenuItem4.Text = "Settings";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem5.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem5.Tag = "cities";
            this.toolStripMenuItem5.Text = "Cities";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem6.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem6.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem6.Tag = "stores";
            this.toolStripMenuItem6.Text = "Stores";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem7.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem7.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem7.Tag = "warehouse";
            this.toolStripMenuItem7.Text = "&Ware Houses";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem8.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem8.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem8.Tag = "currencies";
            this.toolStripMenuItem8.Text = "Currencies";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem9.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(257, 22);
            this.toolStripMenuItem9.Tag = "tradeunit";
            this.toolStripMenuItem9.Text = "Trade &Unit";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem10.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem10.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem10.Tag = "productcategorysettings";
            this.toolStripMenuItem10.Text = "Prodct Category Settings";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem11.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem11.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem11.Tag = "majortypes_brands";
            this.toolStripMenuItem11.Text = "Major Types | Brands";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem12.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem12.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem12.Tag = "products";
            this.toolStripMenuItem12.Text = "Products";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem13.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem13.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem13.Tag = "productdetail";
            this.toolStripMenuItem13.Text = "Product &Details";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem14.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem14.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem14.Tag = "customers";
            this.toolStripMenuItem14.Text = "&Customers";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem15.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem15.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem15.Tag = "accountingheads";
            this.toolStripMenuItem15.Text = "Accounting Heads";
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem16.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem16.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem16.Tag = "lowstocksettings";
            this.toolStripMenuItem16.Text = "Low Stock Settings";
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem17.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem17.Size = new System.Drawing.Size(257, 24);
            this.toolStripMenuItem17.Tag = "aditionalcostheads";
            this.toolStripMenuItem17.Text = "Additional Cost Heads";
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem18.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(96, 29);
            this.toolStripMenuItem18.Tag = "mnu_purchases";
            this.toolStripMenuItem18.Text = "Purchases";
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem19.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem19.Size = new System.Drawing.Size(152, 24);
            this.toolStripMenuItem19.Tag = "purchases";
            this.toolStripMenuItem19.Text = "Pu&rchases";
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem20.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(60, 29);
            this.toolStripMenuItem20.Tag = "mnu_sales";
            this.toolStripMenuItem20.Text = "Sales";
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem21.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem21.Size = new System.Drawing.Size(191, 24);
            this.toolStripMenuItem21.Tag = "sales";
            this.toolStripMenuItem21.Text = "&Sales";
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem22.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem22.Size = new System.Drawing.Size(191, 24);
            this.toolStripMenuItem22.Tag = "salesreturn";
            this.toolStripMenuItem22.Text = "Sale Return ";
            // 
            // toolStripMenuItem23
            // 
            this.toolStripMenuItem23.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem23.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem23.Name = "toolStripMenuItem23";
            this.toolStripMenuItem23.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem23.Size = new System.Drawing.Size(191, 24);
            this.toolStripMenuItem23.Tag = "salescommissionbased";
            this.toolStripMenuItem23.Text = "Sales(% Based)";
            // 
            // toolStripMenuItem24
            // 
            this.toolStripMenuItem24.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem24.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem24.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem24.Name = "toolStripMenuItem24";
            this.toolStripMenuItem24.Size = new System.Drawing.Size(87, 29);
            this.toolStripMenuItem24.Tag = "mnu_inventory";
            this.toolStripMenuItem24.Text = "Inventory";
            // 
            // toolStripMenuItem25
            // 
            this.toolStripMenuItem25.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem25.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem25.Name = "toolStripMenuItem25";
            this.toolStripMenuItem25.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem25.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem25.Tag = "receivingofgoods";
            this.toolStripMenuItem25.Text = "&Recieving of Goods";
            // 
            // toolStripMenuItem26
            // 
            this.toolStripMenuItem26.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem26.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem26.Name = "toolStripMenuItem26";
            this.toolStripMenuItem26.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem26.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem26.Tag = "warehousegoodstransfer";
            this.toolStripMenuItem26.Text = "&Ware House Goods Transfer";
            // 
            // toolStripMenuItem27
            // 
            this.toolStripMenuItem27.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem27.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem27.Name = "toolStripMenuItem27";
            this.toolStripMenuItem27.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem27.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem27.Tag = "goodstransfer";
            this.toolStripMenuItem27.Text = "Goods &Transfer";
            // 
            // toolStripMenuItem28
            // 
            this.toolStripMenuItem28.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem28.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem28.Name = "toolStripMenuItem28";
            this.toolStripMenuItem28.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem28.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem28.Tag = "goodsexpiry";
            this.toolStripMenuItem28.Text = "Goods &Expiry";
            // 
            // toolStripMenuItem29
            // 
            this.toolStripMenuItem29.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem29.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem29.Name = "toolStripMenuItem29";
            this.toolStripMenuItem29.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem29.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem29.Tag = "lowstock";
            this.toolStripMenuItem29.Text = "&Low Stock";
            // 
            // toolStripMenuItem30
            // 
            this.toolStripMenuItem30.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem30.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem30.Name = "toolStripMenuItem30";
            this.toolStripMenuItem30.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem30.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem30.Tag = "deleteinvoices";
            this.toolStripMenuItem30.Text = "Deleted Invoices";
            // 
            // toolStripMenuItem31
            // 
            this.toolStripMenuItem31.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem31.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem31.Name = "toolStripMenuItem31";
            this.toolStripMenuItem31.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem31.Size = new System.Drawing.Size(284, 24);
            this.toolStripMenuItem31.Tag = "displaystockmanagement";
            this.toolStripMenuItem31.Text = "Display Stock Management";
            // 
            // toolStripMenuItem32
            // 
            this.toolStripMenuItem32.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem32.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem32.Name = "toolStripMenuItem32";
            this.toolStripMenuItem32.Size = new System.Drawing.Size(86, 29);
            this.toolStripMenuItem32.Tag = "mnu_account";
            this.toolStripMenuItem32.Text = "Accounts";
            // 
            // toolStripMenuItem33
            // 
            this.toolStripMenuItem33.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem33.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem33.Name = "toolStripMenuItem33";
            this.toolStripMenuItem33.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem33.Size = new System.Drawing.Size(153, 24);
            this.toolStripMenuItem33.Tag = "cashbook";
            this.toolStripMenuItem33.Text = "Cash Book";
            // 
            // toolStripMenuItem34
            // 
            this.toolStripMenuItem34.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem34.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem34.Name = "toolStripMenuItem34";
            this.toolStripMenuItem34.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem34.Size = new System.Drawing.Size(153, 24);
            this.toolStripMenuItem34.Tag = "ledger_search\t";
            this.toolStripMenuItem34.Text = "Ledger";
            // 
            // toolStripMenuItem35
            // 
            this.toolStripMenuItem35.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem35.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem35.Name = "toolStripMenuItem35";
            this.toolStripMenuItem35.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem35.Size = new System.Drawing.Size(153, 24);
            this.toolStripMenuItem35.Tag = "payments";
            this.toolStripMenuItem35.Text = "&Payments";
            // 
            // toolStripMenuItem36
            // 
            this.toolStripMenuItem36.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem36.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem36.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem36.Name = "toolStripMenuItem36";
            this.toolStripMenuItem36.Size = new System.Drawing.Size(77, 29);
            this.toolStripMenuItem36.Tag = "mnu_reports";
            this.toolStripMenuItem36.Text = "Reports";
            // 
            // toolStripMenuItem37
            // 
            this.toolStripMenuItem37.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem37.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem37.Name = "toolStripMenuItem37";
            this.toolStripMenuItem37.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem37.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem37.Tag = "dashboard";
            this.toolStripMenuItem37.Text = "Dashboard";
            // 
            // toolStripMenuItem38
            // 
            this.toolStripMenuItem38.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem38.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem38.Name = "toolStripMenuItem38";
            this.toolStripMenuItem38.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem38.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem38.Tag = "balancesheet_reports";
            this.toolStripMenuItem38.Text = "Balance Sheet";
            // 
            // toolStripMenuItem39
            // 
            this.toolStripMenuItem39.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem39.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem39.Name = "toolStripMenuItem39";
            this.toolStripMenuItem39.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem39.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem39.Tag = "customertrailbalance_reports";
            this.toolStripMenuItem39.Text = "Customer Trail  Balance";
            // 
            // toolStripMenuItem40
            // 
            this.toolStripMenuItem40.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem40.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem40.Name = "toolStripMenuItem40";
            this.toolStripMenuItem40.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem40.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem40.Tag = "customersbalancesheet_reports";
            this.toolStripMenuItem40.Text = "Customers Balance Sheet";
            // 
            // toolStripMenuItem41
            // 
            this.toolStripMenuItem41.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem41.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem41.Name = "toolStripMenuItem41";
            this.toolStripMenuItem41.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem41.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem41.Tag = "dailybusiness_reports";
            this.toolStripMenuItem41.Text = "Daily Business Report";
            // 
            // toolStripMenuItem42
            // 
            this.toolStripMenuItem42.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem42.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem42.Name = "toolStripMenuItem42";
            this.toolStripMenuItem42.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem42.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem42.Tag = "dailybusiness_reports";
            this.toolStripMenuItem42.Text = "Team Productivity Report";
            // 
            // toolStripMenuItem43
            // 
            this.toolStripMenuItem43.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem43.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem43.Name = "toolStripMenuItem43";
            this.toolStripMenuItem43.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem43.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem43.Tag = "purchasercommissionbalancesheet_reports";
            this.toolStripMenuItem43.Text = "Purchaser (Comm) Balance Sheet";
            // 
            // toolStripMenuItem44
            // 
            this.toolStripMenuItem44.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem44.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem44.Name = "toolStripMenuItem44";
            this.toolStripMenuItem44.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem44.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem44.Tag = "purchasercommissionbalancesheet_reports";
            this.toolStripMenuItem44.Text = "Supplier Trail Balance";
            // 
            // toolStripMenuItem45
            // 
            this.toolStripMenuItem45.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem45.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem45.Name = "toolStripMenuItem45";
            this.toolStripMenuItem45.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem45.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem45.Tag = "cashbookdetail_report";
            this.toolStripMenuItem45.Text = "Cash Book";
            // 
            // toolStripMenuItem46
            // 
            this.toolStripMenuItem46.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem46.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem46.Name = "toolStripMenuItem46";
            this.toolStripMenuItem46.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem46.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem46.Tag = "sale(generalproducts)_reports";
            this.toolStripMenuItem46.Text = "Sale(General Products)";
            // 
            // toolStripMenuItem47
            // 
            this.toolStripMenuItem47.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem47.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem47.Name = "toolStripMenuItem47";
            this.toolStripMenuItem47.Size = new System.Drawing.Size(320, 22);
            this.toolStripMenuItem47.Tag = "sale(commissionproduct)_reports)";
            this.toolStripMenuItem47.Text = "Sale(Commission Products)";
            // 
            // toolStripMenuItem48
            // 
            this.toolStripMenuItem48.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem48.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem48.Name = "toolStripMenuItem48";
            this.toolStripMenuItem48.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem48.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem48.Tag = "salesummary_reports";
            this.toolStripMenuItem48.Text = "Sale Summary";
            // 
            // toolStripMenuItem49
            // 
            this.toolStripMenuItem49.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem49.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem49.Name = "toolStripMenuItem49";
            this.toolStripMenuItem49.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem49.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem49.Tag = "saletopanddead_reports";
            this.toolStripMenuItem49.Text = "Sale Rating Report";
            // 
            // toolStripMenuItem50
            // 
            this.toolStripMenuItem50.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem50.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem50.Name = "toolStripMenuItem50";
            this.toolStripMenuItem50.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem50.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem50.Tag = "saletopanddead_reports";
            this.toolStripMenuItem50.Text = "Profit && Loss Report";
            // 
            // toolStripMenuItem51
            // 
            this.toolStripMenuItem51.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem51.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem51.Name = "toolStripMenuItem51";
            this.toolStripMenuItem51.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem51.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem51.Tag = "tradestatistics_report";
            this.toolStripMenuItem51.Text = "Trade Statistics";
            // 
            // toolStripMenuItem52
            // 
            this.toolStripMenuItem52.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem52.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem52.Name = "toolStripMenuItem52";
            this.toolStripMenuItem52.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem52.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem52.Tag = "stockinquery_reports";
            this.toolStripMenuItem52.Text = "Stock Enquiry";
            // 
            // toolStripMenuItem53
            // 
            this.toolStripMenuItem53.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem53.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem53.Name = "toolStripMenuItem53";
            this.toolStripMenuItem53.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem53.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem53.Tag = "stockinqueryShelf_reports";
            this.toolStripMenuItem53.Text = "Stock Enquiry (Shelf)";
            // 
            // toolStripMenuItem54
            // 
            this.toolStripMenuItem54.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem54.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem54.Name = "toolStripMenuItem54";
            this.toolStripMenuItem54.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem54.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem54.Tag = "ledger_reports";
            this.toolStripMenuItem54.Text = "Ledger Summery";
            // 
            // toolStripMenuItem55
            // 
            this.toolStripMenuItem55.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem55.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem55.Name = "toolStripMenuItem55";
            this.toolStripMenuItem55.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem55.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem55.Tag = "auditlogSearch";
            this.toolStripMenuItem55.Text = "Audit Log";
            // 
            // toolStripMenuItem56
            // 
            this.toolStripMenuItem56.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem56.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem56.Name = "toolStripMenuItem56";
            this.toolStripMenuItem56.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem56.Size = new System.Drawing.Size(320, 24);
            this.toolStripMenuItem56.Tag = "expiry_report";
            this.toolStripMenuItem56.Text = "Expiry Summery";
            // 
            // toolStripMenuItem57
            // 
            this.toolStripMenuItem57.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem57.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem57.Name = "toolStripMenuItem57";
            this.toolStripMenuItem57.Size = new System.Drawing.Size(108, 29);
            this.toolStripMenuItem57.Tag = "mnu_preferences";
            this.toolStripMenuItem57.Text = "&Preferences";
            // 
            // toolStripMenuItem58
            // 
            this.toolStripMenuItem58.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem58.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem58.Name = "toolStripMenuItem58";
            this.toolStripMenuItem58.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem58.Size = new System.Drawing.Size(191, 24);
            this.toolStripMenuItem58.Tag = "storesetting";
            this.toolStripMenuItem58.Text = "Store Settings";
            // 
            // toolStripMenuItem59
            // 
            this.toolStripMenuItem59.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem59.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem59.Name = "toolStripMenuItem59";
            this.toolStripMenuItem59.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem59.Size = new System.Drawing.Size(191, 24);
            this.toolStripMenuItem59.Tag = "stationsetting";
            this.toolStripMenuItem59.Text = "Station Settings";
            // 
            // toolStripMenuItem60
            // 
            this.toolStripMenuItem60.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem60.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem60.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem60.Name = "toolStripMenuItem60";
            this.toolStripMenuItem60.Size = new System.Drawing.Size(53, 29);
            this.toolStripMenuItem60.Tag = "mnu_help";
            this.toolStripMenuItem60.Text = "Help";
            // 
            // toolStripMenuItem61
            // 
            this.toolStripMenuItem61.BackColor = System.Drawing.Color.Teal;
            this.toolStripMenuItem61.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem61.Name = "toolStripMenuItem61";
            this.toolStripMenuItem61.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripMenuItem61.Size = new System.Drawing.Size(109, 24);
            this.toolStripMenuItem61.Text = "Help";
            // 
            // btn_MainMenuClose
            // 
            this.btn_MainMenuClose.BackColor = System.Drawing.Color.White;
            this.btn_MainMenuClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_MainMenuClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_MainMenuClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_MainMenuClose.FlatAppearance.BorderSize = 0;
            this.btn_MainMenuClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_MainMenuClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn_MainMenuClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_MainMenuClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MainMenuClose.Font = new System.Drawing.Font("Century Gothic", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_MainMenuClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.btn_MainMenuClose.Location = new System.Drawing.Point(1016, 4);
            this.btn_MainMenuClose.Margin = new System.Windows.Forms.Padding(0);
            this.btn_MainMenuClose.Name = "btn_MainMenuClose";
            this.btn_MainMenuClose.Size = new System.Drawing.Size(41, 45);
            this.btn_MainMenuClose.TabIndex = 20;
            this.btn_MainMenuClose.TabStop = false;
            this.btn_MainMenuClose.Text = "X";
            this.ttp_Menus.SetToolTip(this.btn_MainMenuClose, "DBU Log Out");
            this.btn_MainMenuClose.UseVisualStyleBackColor = false;
            this.btn_MainMenuClose.Click += new System.EventHandler(this.btn_MainMenuClose_Click);
            // 
            // btn_Minimize
            // 
            this.btn_Minimize.BackColor = System.Drawing.Color.White;
            this.btn_Minimize.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Minimize.BackgroundImage")));
            this.btn_Minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Minimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minimize.Font = new System.Drawing.Font("Leelawadee UI", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Minimize.Location = new System.Drawing.Point(975, 17);
            this.btn_Minimize.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Minimize.Name = "btn_Minimize";
            this.btn_Minimize.Size = new System.Drawing.Size(29, 26);
            this.btn_Minimize.TabIndex = 30;
            this.btn_Minimize.TabStop = false;
            this.ttp_Menus.SetToolTip(this.btn_Minimize, "DBU Minimize");
            this.btn_Minimize.UseVisualStyleBackColor = false;
            this.btn_Minimize.Click += new System.EventHandler(this.btn_Minimize_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem62});
            this.menuStrip1.Location = new System.Drawing.Point(187, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(928, 50);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(928, 50);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(928, 50);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem62
            // 
            this.toolStripMenuItem62.Name = "toolStripMenuItem62";
            this.toolStripMenuItem62.Size = new System.Drawing.Size(12, 46);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1115, 697);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Minimize);
            this.Controls.Add(this.btn_MainMenuClose);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1115, 697);
            this.MinimumSize = new System.Drawing.Size(1115, 697);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " IntegrateX";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnl_SyncControl.ResumeLayout(false);
            this.pnl_SyncControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_HotelLogo)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem24;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem25;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem26;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem27;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem28;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem29;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem30;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem31;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem32;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem33;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem34;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem35;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem36;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem37;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem38;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem39;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem40;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem41;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem42;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem43;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem44;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem45;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem46;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem47;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem48;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem49;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem51;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem52;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem53;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem54;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem55;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem56;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem57;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem58;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem59;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem60;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem61;
        private System.Windows.Forms.PictureBox Pic_HotelLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ToolTip ttp_Menus;
        public LinkLabel lblConcierge_Val;
        public LinkLabel lblSpa_Val;
        public LinkLabel lbl_RestaurensVal;
        public LinkLabel lbl_Experiences_Val;
        //private ListBox listBox1;
        //private Label lblLastSyncedDateShow;
        //private PictureBox pictureBox5;
        //private PictureBox pictureBox4;
        //private PictureBox pictureBox3;
        private MenuStrip menuStrip1;
        private Panel pnl_SyncControl;
        private Button btn_MainMenuClose;
       // private Panel pnl_LastSyncTime;
      //  private Label label1;
        private BDU.Extensions.ServrButton btn_BDUSync;
        private BDU.Extensions.ServrButton btn_Mapping;
        private BDU.Extensions.ServrButton btnPreference;
        private BDU.Extensions.ServrButton btnDemo;
        private BDU.Extensions.ServrButton btn_Logout;
        private BDU.Extensions.ServrButton btn_Experiences;
        private BDU.Extensions.ServrButton btn_Concierge;
        private BDU.Extensions.ServrButton btnSpa;
        private BDU.Extensions.ServrButton btn_Restaurans;
        private Button btn_Minimize;
        private ToolStripMenuItem toolStripMenuItem62;
        private PictureBox pictureBox2;
        private Panel panel12;
        private Panel panel13;
        private Panel panel14;
        private Panel panel15;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Label lblNewOrderName;
       // private BDU.Extensions.ServrButton servrButton1;
    }
}