using System.Windows.Forms;

namespace bdu
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
            this.btnDemo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Pic_HotelLogo = new System.Windows.Forms.PictureBox();
            this.btn_Clone = new System.Windows.Forms.Button();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.btn_BDUSync = new System.Windows.Forms.Button();
            this.btn_Mapping = new System.Windows.Forms.Button();
            this.btnPreference = new System.Windows.Forms.Button();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ttp_Menus = new System.Windows.Forms.ToolTip(this.components);
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_HotelLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContent.Controls.Add(this.btnDemo);
            this.pnlContent.Controls.Add(this.pictureBox1);
            this.pnlContent.Controls.Add(this.Pic_HotelLogo);
            this.pnlContent.Controls.Add(this.btn_Clone);
            this.pnlContent.Controls.Add(this.btn_Logout);
            this.pnlContent.Controls.Add(this.btn_BDUSync);
            this.pnlContent.Controls.Add(this.btn_Mapping);
            this.pnlContent.Controls.Add(this.btnPreference);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(171, 629);
            this.pnlContent.TabIndex = 6;
            // 
            // btnDemo
            // 
            this.btnDemo.AutoSize = true;
            this.btnDemo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDemo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDemo.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnDemo.FlatAppearance.BorderSize = 0;
            this.btnDemo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnDemo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDemo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDemo.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDemo.Image = ((System.Drawing.Image)(resources.GetObject("btnDemo.Image")));
            this.btnDemo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDemo.Location = new System.Drawing.Point(11, 352);
            this.btnDemo.Margin = new System.Windows.Forms.Padding(2);
            this.btnDemo.MaximumSize = new System.Drawing.Size(150, 35);
            this.btnDemo.MinimumSize = new System.Drawing.Size(150, 35);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Padding = new System.Windows.Forms.Padding(2, 0, 6, 0);
            this.btnDemo.Size = new System.Drawing.Size(150, 35);
            this.btnDemo.TabIndex = 4;
            this.btnDemo.Tag = "1";
            this.btnDemo.Text = "Demo";
            this.btnDemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDemo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btnDemo, "Video Tutorial link");
            this.btnDemo.UseVisualStyleBackColor = false;
            this.btnDemo.Click += new System.EventHandler(this.btnTutorial_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(8, 162);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 2);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Pic_HotelLogo
            // 
            this.Pic_HotelLogo.Image = ((System.Drawing.Image)(resources.GetObject("Pic_HotelLogo.Image")));
            this.Pic_HotelLogo.Location = new System.Drawing.Point(32, 28);
            this.Pic_HotelLogo.Name = "Pic_HotelLogo";
            this.Pic_HotelLogo.Size = new System.Drawing.Size(105, 105);
            this.Pic_HotelLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_HotelLogo.TabIndex = 9;
            this.Pic_HotelLogo.TabStop = false;
            // 
            // btn_Clone
            // 
            this.btn_Clone.AutoSize = true;
            this.btn_Clone.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Clone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Clone.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Clone.FlatAppearance.BorderSize = 0;
            this.btn_Clone.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Clone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Clone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Clone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clone.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Clone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Clone.Image = ((System.Drawing.Image)(resources.GetObject("btn_Clone.Image")));
            this.btn_Clone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Clone.Location = new System.Drawing.Point(10, 259);
            this.btn_Clone.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Clone.MaximumSize = new System.Drawing.Size(150, 35);
            this.btn_Clone.MinimumSize = new System.Drawing.Size(150, 35);
            this.btn_Clone.Name = "btn_Clone";
            this.btn_Clone.Padding = new System.Windows.Forms.Padding(2, 0, 4, 0);
            this.btn_Clone.Size = new System.Drawing.Size(150, 35);
            this.btn_Clone.TabIndex = 3;
            this.btn_Clone.Tag = "1";
            this.btn_Clone.Text = "Clone";
            this.btn_Clone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Clone.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Clone, "Make copy from another");
            this.btn_Clone.UseVisualStyleBackColor = false;
            this.btn_Clone.Click += new System.EventHandler(this.btn_Clone_Click);
            // 
            // btn_Logout
            // 
            this.btn_Logout.AutoSize = true;
            this.btn_Logout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Logout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Logout.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Logout.FlatAppearance.BorderSize = 0;
            this.btn_Logout.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Logout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Logout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Logout.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Logout.Image = ((System.Drawing.Image)(resources.GetObject("btn_Logout.Image")));
            this.btn_Logout.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Logout.Location = new System.Drawing.Point(10, 400);
            this.btn_Logout.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Logout.MaximumSize = new System.Drawing.Size(150, 35);
            this.btn_Logout.MinimumSize = new System.Drawing.Size(150, 35);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Padding = new System.Windows.Forms.Padding(2, 0, 6, 0);
            this.btn_Logout.Size = new System.Drawing.Size(150, 35);
            this.btn_Logout.TabIndex = 5;
            this.btn_Logout.Tag = "1";
            this.btn_Logout.Text = "Log Out";
            this.btn_Logout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Logout.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Logout, "LogOut & Close application");
            this.btn_Logout.UseVisualStyleBackColor = false;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // btn_BDUSync
            // 
            this.btn_BDUSync.AutoSize = true;
            this.btn_BDUSync.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_BDUSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_BDUSync.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_BDUSync.FlatAppearance.BorderSize = 0;
            this.btn_BDUSync.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_BDUSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_BDUSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_BDUSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BDUSync.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_BDUSync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_BDUSync.Image = ((System.Drawing.Image)(resources.GetObject("btn_BDUSync.Image")));
            this.btn_BDUSync.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BDUSync.Location = new System.Drawing.Point(10, 169);
            this.btn_BDUSync.Margin = new System.Windows.Forms.Padding(2);
            this.btn_BDUSync.MaximumSize = new System.Drawing.Size(150, 35);
            this.btn_BDUSync.MinimumSize = new System.Drawing.Size(150, 35);
            this.btn_BDUSync.Name = "btn_BDUSync";
            this.btn_BDUSync.Padding = new System.Windows.Forms.Padding(2, 0, 5, 0);
            this.btn_BDUSync.Size = new System.Drawing.Size(150, 35);
            this.btn_BDUSync.TabIndex = 1;
            this.btn_BDUSync.Tag = "1";
            this.btn_BDUSync.Text = "Sync";
            this.btn_BDUSync.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_BDUSync.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_BDUSync, "Push & Pull Data");
            this.btn_BDUSync.UseVisualStyleBackColor = false;
            this.btn_BDUSync.Click += new System.EventHandler(this.btn_BDUSync_Click);
            // 
            // btn_Mapping
            // 
            this.btn_Mapping.AutoSize = true;
            this.btn_Mapping.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Mapping.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Mapping.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Mapping.FlatAppearance.BorderSize = 0;
            this.btn_Mapping.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Mapping.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Mapping.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Mapping.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Mapping.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_Mapping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Mapping.Image = ((System.Drawing.Image)(resources.GetObject("btn_Mapping.Image")));
            this.btn_Mapping.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Mapping.Location = new System.Drawing.Point(10, 215);
            this.btn_Mapping.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Mapping.MaximumSize = new System.Drawing.Size(150, 35);
            this.btn_Mapping.MinimumSize = new System.Drawing.Size(150, 35);
            this.btn_Mapping.Name = "btn_Mapping";
            this.btn_Mapping.Padding = new System.Windows.Forms.Padding(2, 0, 4, 0);
            this.btn_Mapping.Size = new System.Drawing.Size(150, 35);
            this.btn_Mapping.TabIndex = 2;
            this.btn_Mapping.Tag = "1";
            this.btn_Mapping.Text = "Mapping";
            this.btn_Mapping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Mapping.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btn_Mapping, "Form & Field mapping configuration");
            this.btn_Mapping.UseVisualStyleBackColor = false;
            this.btn_Mapping.Click += new System.EventHandler(this.btn_Mapping_Click);
            // 
            // btnPreference
            // 
            this.btnPreference.AutoSize = true;
            this.btnPreference.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPreference.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPreference.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnPreference.FlatAppearance.BorderSize = 0;
            this.btnPreference.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPreference.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPreference.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPreference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreference.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPreference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPreference.Image = ((System.Drawing.Image)(resources.GetObject("btnPreference.Image")));
            this.btnPreference.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPreference.Location = new System.Drawing.Point(10, 305);
            this.btnPreference.Margin = new System.Windows.Forms.Padding(2);
            this.btnPreference.MaximumSize = new System.Drawing.Size(150, 35);
            this.btnPreference.MinimumSize = new System.Drawing.Size(150, 35);
            this.btnPreference.Name = "btnPreference";
            this.btnPreference.Padding = new System.Windows.Forms.Padding(2, 0, 6, 0);
            this.btnPreference.Size = new System.Drawing.Size(150, 35);
            this.btnPreference.TabIndex = 4;
            this.btnPreference.Tag = "1";
            this.btnPreference.Text = "Preference";
            this.btnPreference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPreference.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ttp_Menus.SetToolTip(this.btnPreference, "Notification, Visualizer & API Settings");
            this.btnPreference.UseVisualStyleBackColor = false;
            this.btnPreference.Click += new System.EventHandler(this.btnPreference_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Location = new System.Drawing.Point(171, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(917, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1088, 629);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlContent);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Location = new System.Drawing.Point(120, 100);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1104, 668);
            this.MinimumSize = new System.Drawing.Size(1104, 668);
            this.Name = "frmMain";
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.Text = "BDU - Main";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_HotelLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnPreference;
        private System.Windows.Forms.Button btn_Mapping;
        private System.Windows.Forms.Button btn_BDUSync;
        private System.Windows.Forms.Button btn_Logout;
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btn_Clone;
        private System.Windows.Forms.PictureBox Pic_HotelLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDemo;
        private ToolTip ttp_Menus;
    }
}