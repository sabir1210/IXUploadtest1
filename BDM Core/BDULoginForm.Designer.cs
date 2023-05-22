
namespace servr.integratex.ui
{
    partial class BDULoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resour ces should be disposed; otherwise, false.</param>
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDULoginForm));
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.Pic_Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ttp_Menus = new System.Windows.Forms.ToolTip(this.components);
            this.btn_LoginClose = new System.Windows.Forms.Button();
            this.txtPassword = new BDU.Extensions.ServrInputControl();
            this.btnLogin = new BDU.Extensions.ServrButton();
            this.btn_Minimize = new System.Windows.Forms.Button();
            this.lbl_ForgotPassword = new System.Windows.Forms.LinkLabel();
            this.txtUserName = new BDU.Extensions.ServrInputControl();
            this.Pic_Eye = new System.Windows.Forms.PictureBox();
            this.sPanel2 = new BDU.Extensions.SPanel();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Eye)).BeginInit();
            this.sPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.AutoSize = true;
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCopyRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lblCopyRight.Location = new System.Drawing.Point(55, 405);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(282, 16);
            this.lblCopyRight.TabIndex = 5;
            this.lblCopyRight.Text = "Copyrights © 2021 Servr Ltd All rights reserved.";
            // 
            // Pic_Logo
            // 
            this.Pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pic_Logo.Image = ((System.Drawing.Image)(resources.GetObject("Pic_Logo.Image")));
            this.Pic_Logo.Location = new System.Drawing.Point(143, 46);
            this.Pic_Logo.Name = "Pic_Logo";
            this.Pic_Logo.Size = new System.Drawing.Size(100, 100);
            this.Pic_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic_Logo.TabIndex = 6;
            this.Pic_Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.label1.Location = new System.Drawing.Point(140, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "IntegrateX";
            // 
            // btn_LoginClose
            // 
            this.btn_LoginClose.BackColor = System.Drawing.Color.White;
            this.btn_LoginClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_LoginClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LoginClose.FlatAppearance.BorderSize = 0;
            this.btn_LoginClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_LoginClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn_LoginClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_LoginClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoginClose.Font = new System.Drawing.Font("Century Gothic", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_LoginClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.btn_LoginClose.Location = new System.Drawing.Point(837, 16);
            this.btn_LoginClose.Name = "btn_LoginClose";
            this.btn_LoginClose.Size = new System.Drawing.Size(56, 53);
            this.btn_LoginClose.TabIndex = 9;
            this.btn_LoginClose.Text = "X";
            this.ttp_Menus.SetToolTip(this.btn_LoginClose, "IntegrateX Log Out");
            this.btn_LoginClose.UseVisualStyleBackColor = false;
            this.btn_LoginClose.Click += new System.EventHandler(this.btn_LoginClose_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtPassword.BorderSize = 0;
            this.txtPassword.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.txtPassword.Font = new System.Drawing.Font("Roboto Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtPassword.Location = new System.Drawing.Point(66, 255);
            this.txtPassword.MaximumSize = new System.Drawing.Size(257, 44);
            this.txtPassword.MinimumSize = new System.Drawing.Size(257, 44);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PlaceHolderText = "";
            this.txtPassword.Size = new System.Drawing.Size(257, 44);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.TabStop = false;
            this.txtPassword.Tag = "Password";
            this.txtPassword.textboxRadius = 18;
            this.ttp_Menus.SetToolTip(this.txtPassword, " Password");
            this.txtPassword.UseSystemPasswordChar = false;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnLogin.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnLogin.BorderRadius = 40;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(65, 313);
            this.btnLogin.MaximumSize = new System.Drawing.Size(256, 43);
            this.btnLogin.MinimumSize = new System.Drawing.Size(256, 43);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RootElement = null;
            this.btnLogin.Size = new System.Drawing.Size(256, 43);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.TabStop = false;
            this.btnLogin.Text = "&Login";
            this.btnLogin.TextColor = System.Drawing.Color.White;
            this.ttp_Menus.SetToolTip(this.btnLogin, "IntegrateX Login");
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btn_Minimize
            // 
            this.btn_Minimize.BackColor = System.Drawing.Color.White;
            this.btn_Minimize.BackgroundImage = global::servr.integratex.ui.Properties.Resources.Minimize_White;
            this.btn_Minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatAppearance.BorderSize = 0;
            this.btn_Minimize.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btn_Minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minimize.Font = new System.Drawing.Font("Leelawadee UI", 27F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btn_Minimize.Location = new System.Drawing.Point(808, 31);
            this.btn_Minimize.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Minimize.Name = "btn_Minimize";
            this.btn_Minimize.Size = new System.Drawing.Size(30, 22);
            this.btn_Minimize.TabIndex = 8;
            this.ttp_Menus.SetToolTip(this.btn_Minimize, "IntegrateX Minimize");
            this.btn_Minimize.UseVisualStyleBackColor = false;
            this.btn_Minimize.Click += new System.EventHandler(this.btn_Minimize_Click);
            // 
            // lbl_ForgotPassword
            // 
            this.lbl_ForgotPassword.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lbl_ForgotPassword.AutoSize = true;
            this.lbl_ForgotPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_ForgotPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_ForgotPassword.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lbl_ForgotPassword.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lbl_ForgotPassword.Location = new System.Drawing.Point(80, 365);
            this.lbl_ForgotPassword.Name = "lbl_ForgotPassword";
            this.lbl_ForgotPassword.Size = new System.Drawing.Size(131, 18);
            this.lbl_ForgotPassword.TabIndex = 4;
            this.lbl_ForgotPassword.TabStop = true;
            this.lbl_ForgotPassword.Text = "Forgot Password?";
            this.lbl_ForgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbl_ForgotPassword_LinkClicked);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtUserName.BorderSize = 1;
            this.txtUserName.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.txtUserName.Font = new System.Drawing.Font("Roboto Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtUserName.Location = new System.Drawing.Point(66, 195);
            this.txtUserName.MaximumSize = new System.Drawing.Size(257, 44);
            this.txtUserName.MinimumSize = new System.Drawing.Size(257, 44);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.PlaceHolderText = "";
            this.txtUserName.Size = new System.Drawing.Size(257, 44);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.TabStop = false;
            this.txtUserName.Tag = "User Name";
            this.txtUserName.textboxRadius = 18;
            this.txtUserName.UseSystemPasswordChar = false;
            // 
            // Pic_Eye
            // 
            this.Pic_Eye.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Eye.BackgroundImage = global::servr.integratex.ui.Properties.Resources.ShowPassword_Small;
            this.Pic_Eye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pic_Eye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pic_Eye.Image = ((System.Drawing.Image)(resources.GetObject("Pic_Eye.Image")));
            this.Pic_Eye.Location = new System.Drawing.Point(292, 268);
            this.Pic_Eye.Name = "Pic_Eye";
            this.Pic_Eye.Size = new System.Drawing.Size(24, 15);
            this.Pic_Eye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Eye.TabIndex = 14;
            this.Pic_Eye.TabStop = false;
            // 
            // sPanel2
            // 
            this.sPanel2.BackColor = System.Drawing.Color.White;
            this.sPanel2.BorderColor = System.Drawing.Color.White;
            this.sPanel2.Controls.Add(this.lblVersion);
            this.sPanel2.Controls.Add(this.Pic_Eye);
            this.sPanel2.Controls.Add(this.btnLogin);
            this.sPanel2.Controls.Add(this.txtPassword);
            this.sPanel2.Controls.Add(this.txtUserName);
            this.sPanel2.Controls.Add(this.Pic_Logo);
            this.sPanel2.Controls.Add(this.lblCopyRight);
            this.sPanel2.Controls.Add(this.lbl_ForgotPassword);
            this.sPanel2.Controls.Add(this.label1);
            this.sPanel2.Edge = 30;
            this.sPanel2.Location = new System.Drawing.Point(268, 70);
            this.sPanel2.Name = "sPanel2";
            this.sPanel2.Size = new System.Drawing.Size(390, 490);
            this.sPanel2.TabIndex = 6;
            // 
            // lblVersion
            // 
            this.lblVersion.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lblVersion.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblVersion.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.lblVersion.Location = new System.Drawing.Point(212, 365);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(82, 18);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.TabStop = true;
            this.lblVersion.Text = "Version 1.1";
            // 
            // BDULoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(927, 640);
            this.Controls.Add(this.sPanel2);
            this.Controls.Add(this.btn_Minimize);
            this.Controls.Add(this.btn_LoginClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(927, 640);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(927, 640);
            this.Name = "BDULoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servr IntegrateX";
            this.Load += new System.EventHandler(this.BDULoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Eye)).EndInit();
            this.sPanel2.ResumeLayout(false);
            this.sPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblCopyRight;
        private System.Windows.Forms.PictureBox Pic_Logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ttp_Menus;
        private System.Windows.Forms.Button btn_LoginClose;
      //  private System.Windows.Forms.Panel pnlBorder;
       // private System.Windows.Forms.Button btnRegisterHere;
        private System.Windows.Forms.LinkLabel lbl_ForgotPassword;
      //  private System.Windows.Forms.TextBox txtColor_LevelFirst;
        private BDU.Extensions.ServrInputControl txtUserName;
        private BDU.Extensions.ServrInputControl txtPassword;
        private System.Windows.Forms.PictureBox Pic_Eye;
     //   private BDU.Extensions.ServrButton servrButton1;
        private BDU.Extensions.ServrButton btnLogin;
        private System.Windows.Forms.Button btn_Minimize;
     
        private BDU.Extensions.SPanel sPanel2;
        private System.Windows.Forms.LinkLabel lblVersion;
        //private System.Windows.Forms.ComboBox comboBox1;
        //private System.Windows.Forms.ComboBox comboBox2;
        //private System.Windows.Forms.ComboBox comboBox3;
        //private System.Windows.Forms.ComboBox comboBox4;
    }
}

