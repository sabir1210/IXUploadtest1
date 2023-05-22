namespace servr.integratex.ui
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pnl_BackMessagebox = new BDU.Extensions.SPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new BDU.Extensions.ServrButton();
            this.Pic_Eye = new System.Windows.Forms.PictureBox();
            this.btn_LoginClose = new System.Windows.Forms.Button();
            this.btnLogin = new BDU.Extensions.ServrButton();
            this.txtPassword = new BDU.Extensions.ServrInputControl();
            this.txtUserName = new BDU.Extensions.ServrInputControl();
            this.pnl_BackMessagebox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Eye)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_BackMessagebox
            // 
            this.pnl_BackMessagebox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_BackMessagebox.AutoScroll = true;
            this.pnl_BackMessagebox.AutoSize = true;
            this.pnl_BackMessagebox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl_BackMessagebox.BackColor = System.Drawing.Color.White;
            this.pnl_BackMessagebox.BorderColor = System.Drawing.Color.White;
            this.pnl_BackMessagebox.Controls.Add(this.label1);
            this.pnl_BackMessagebox.Controls.Add(this.btnCancel);
            this.pnl_BackMessagebox.Controls.Add(this.Pic_Eye);
            this.pnl_BackMessagebox.Controls.Add(this.btn_LoginClose);
            this.pnl_BackMessagebox.Controls.Add(this.btnLogin);
            this.pnl_BackMessagebox.Controls.Add(this.txtPassword);
            this.pnl_BackMessagebox.Controls.Add(this.txtUserName);
            this.pnl_BackMessagebox.Edge = 98;
            this.pnl_BackMessagebox.Location = new System.Drawing.Point(6, 4);
            this.pnl_BackMessagebox.MaximumSize = new System.Drawing.Size(425, 330);
            this.pnl_BackMessagebox.MinimumSize = new System.Drawing.Size(425, 330);
            this.pnl_BackMessagebox.Name = "pnl_BackMessagebox";
            this.pnl_BackMessagebox.Size = new System.Drawing.Size(425, 330);
            this.pnl_BackMessagebox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(185)))), ((int)(((byte)(234)))));
            this.label1.Location = new System.Drawing.Point(179, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 31);
            this.label1.TabIndex = 25;
            this.label1.Text = "Login";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BorderRadius = 40;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.Location = new System.Drawing.Point(65, 254);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RootElement = null;
            this.btnCancel.Size = new System.Drawing.Size(290, 42);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Pic_Eye
            // 
            this.Pic_Eye.BackColor = System.Drawing.Color.Transparent;
            this.Pic_Eye.BackgroundImage = global::servr.integratex.ui.Properties.Resources.ShowPassword_Small;
            this.Pic_Eye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pic_Eye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Pic_Eye.Image = ((System.Drawing.Image)(resources.GetObject("Pic_Eye.Image")));
            this.Pic_Eye.Location = new System.Drawing.Point(319, 159);
            this.Pic_Eye.Name = "Pic_Eye";
            this.Pic_Eye.Size = new System.Drawing.Size(24, 15);
            this.Pic_Eye.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_Eye.TabIndex = 23;
            this.Pic_Eye.TabStop = false;
            this.Pic_Eye.Click += new System.EventHandler(this.Pic_Eye_Click);
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
            this.btn_LoginClose.Location = new System.Drawing.Point(365, 4);
            this.btn_LoginClose.Name = "btn_LoginClose";
            this.btn_LoginClose.Size = new System.Drawing.Size(56, 53);
            this.btn_LoginClose.TabIndex = 22;
            this.btn_LoginClose.Text = "X";
            this.btn_LoginClose.UseVisualStyleBackColor = false;
            this.btn_LoginClose.Click += new System.EventHandler(this.btn_LoginClose_Click);
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
            this.btnLogin.Location = new System.Drawing.Point(65, 199);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RootElement = null;
            this.btnLogin.Size = new System.Drawing.Size(290, 42);
            this.btnLogin.TabIndex = 20;
            this.btnLogin.TabStop = false;
            this.btnLogin.Text = "&Login";
            this.btnLogin.TextColor = System.Drawing.Color.White;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Transparent;
            this.txtPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtPassword.BorderSize = 0;
            this.txtPassword.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.txtPassword.Font = new System.Drawing.Font("Roboto Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtPassword.Location = new System.Drawing.Point(65, 144);
            this.txtPassword.MaximumSize = new System.Drawing.Size(290, 44);
            this.txtPassword.MinimumSize = new System.Drawing.Size(290, 44);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.PlaceHolderText = "";
            this.txtPassword.Size = new System.Drawing.Size(290, 44);
            this.txtPassword.TabIndex = 19;
            this.txtPassword.TabStop = false;
            this.txtPassword.Tag = "Password";
            this.txtPassword.textboxRadius = 18;
            this.txtPassword.UseSystemPasswordChar = false;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.Transparent;
            this.txtUserName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(228)))));
            this.txtUserName.BorderSize = 1;
            this.txtUserName.Br = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.txtUserName.Font = new System.Drawing.Font("Roboto Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.txtUserName.Location = new System.Drawing.Point(65, 86);
            this.txtUserName.MaximumSize = new System.Drawing.Size(290, 44);
            this.txtUserName.MinimumSize = new System.Drawing.Size(290, 44);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.PlaceHolderText = "";
            this.txtUserName.Size = new System.Drawing.Size(290, 44);
            this.txtUserName.TabIndex = 18;
            this.txtUserName.TabStop = false;
            this.txtUserName.Tag = "User Name";
            this.txtUserName.textboxRadius = 18;
            this.txtUserName.UseSystemPasswordChar = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(440, 342);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_BackMessagebox);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(440, 342);
            this.MinimumSize = new System.Drawing.Size(440, 342);
            this.Name = "Login";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Messagebox_Load);
            this.pnl_BackMessagebox.ResumeLayout(false);
            this.pnl_BackMessagebox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_Eye)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        //private BDU.Extensions.SPanel sPanel1;
        private BDU.Extensions.SPanel pnl_BackMessagebox;
        private BDU.Extensions.ServrButton btnLogin;
        private BDU.Extensions.ServrInputControl txtPassword;
        private BDU.Extensions.ServrInputControl txtUserName;
        private System.Windows.Forms.Button btn_LoginClose;
        private System.Windows.Forms.PictureBox Pic_Eye;
        private BDU.Extensions.ServrButton btnCancel;
        private System.Windows.Forms.Label label1;
    }
}