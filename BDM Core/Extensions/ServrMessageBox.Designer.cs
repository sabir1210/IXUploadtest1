namespace servr.integratex.ui
{
    partial class ServrMessageBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.TextBox();
            this.Pic_MessageError = new System.Windows.Forms.PictureBox();
            this.btnOK = new BDU.Extensions.ServrButton();
            this.btnConfirm = new BDU.Extensions.ServrButton();
            this.btnCancel = new BDU.Extensions.ServrButton();
            this.pnl_BackMessagebox = new BDU.Extensions.SPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_MessageError)).BeginInit();
            this.pnl_BackMessagebox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(12, 103);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MaximumSize = new System.Drawing.Size(355, 62);
            this.panel1.MinimumSize = new System.Drawing.Size(355, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 62);
            this.panel1.TabIndex = 6;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.BackColor = System.Drawing.Color.White;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblMessage.Font = new System.Drawing.Font("Roboto Light", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMessage.Location = new System.Drawing.Point(2, 1);
            this.lblMessage.MaximumSize = new System.Drawing.Size(351, 60);
            this.lblMessage.MinimumSize = new System.Drawing.Size(351, 60);
            this.lblMessage.Multiline = true;
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(351, 60);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Pic_MessageError
            // 
            this.Pic_MessageError.BackgroundImage = global::servr.integratex.ui.Properties.Resources.MessageBox_Question;
            this.Pic_MessageError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pic_MessageError.Location = new System.Drawing.Point(154, 13);
            this.Pic_MessageError.Name = "Pic_MessageError";
            this.Pic_MessageError.Size = new System.Drawing.Size(70, 70);
            this.Pic_MessageError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pic_MessageError.TabIndex = 11;
            this.Pic_MessageError.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnOK.BorderRadius = 36;
            this.btnOK.BorderSize = 0;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(127, 187);
            this.btnOK.MaximumSize = new System.Drawing.Size(122, 38);
            this.btnOK.MinimumSize = new System.Drawing.Size(122, 38);
            this.btnOK.Name = "btnOK";
            this.btnOK.RootElement = null;
            this.btnOK.Size = new System.Drawing.Size(122, 38);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Ok";
            this.btnOK.TextColor = System.Drawing.Color.White;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnConfirm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnConfirm.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(168)))), ((int)(((byte)(216)))));
            this.btnConfirm.BorderRadius = 36;
            this.btnConfirm.BorderSize = 0;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(236, 34);
            this.btnConfirm.MaximumSize = new System.Drawing.Size(122, 36);
            this.btnConfirm.MinimumSize = new System.Drawing.Size(122, 38);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.RootElement = null;
            this.btnConfirm.Size = new System.Drawing.Size(122, 38);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Con&firm";
            this.btnConfirm.TextColor = System.Drawing.Color.White;
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.btnCancel.BorderRadius = 36;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.Location = new System.Drawing.Point(15, 30);
            this.btnCancel.MaximumSize = new System.Drawing.Size(122, 36);
            this.btnCancel.MinimumSize = new System.Drawing.Size(122, 38);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RootElement = null;
            this.btnCancel.Size = new System.Drawing.Size(122, 38);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnl_BackMessagebox
            // 
            this.pnl_BackMessagebox.BackColor = System.Drawing.Color.White;
            this.pnl_BackMessagebox.BorderColor = System.Drawing.Color.White;
            this.pnl_BackMessagebox.Controls.Add(this.btnOK);
            this.pnl_BackMessagebox.Controls.Add(this.Pic_MessageError);
            this.pnl_BackMessagebox.Controls.Add(this.btnConfirm);
            this.pnl_BackMessagebox.Controls.Add(this.panel1);
            this.pnl_BackMessagebox.Controls.Add(this.btnCancel);
            this.pnl_BackMessagebox.Edge = 98;
            this.pnl_BackMessagebox.Location = new System.Drawing.Point(3, 3);
            this.pnl_BackMessagebox.MaximumSize = new System.Drawing.Size(380, 242);
            this.pnl_BackMessagebox.MinimumSize = new System.Drawing.Size(380, 242);
            this.pnl_BackMessagebox.Name = "pnl_BackMessagebox";
            this.pnl_BackMessagebox.Size = new System.Drawing.Size(380, 242);
            this.pnl_BackMessagebox.TabIndex = 6;
            // 
            // ServrMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(388, 250);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_BackMessagebox);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(388, 250);
            this.MinimumSize = new System.Drawing.Size(388, 250);
            this.Name = "ServrMessageBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servr Message Box";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Messagebox_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pic_MessageError)).EndInit();
            this.pnl_BackMessagebox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
       // private System.Windows.Forms.Label lblMessageXX;
        private System.Windows.Forms.PictureBox Pic_MessageError;
        private System.Windows.Forms.TextBox lblMessage;
        private BDU.Extensions.ServrButton btnOK;
        private BDU.Extensions.ServrButton btnConfirm;
        private BDU.Extensions.ServrButton btnCancel;
        //private BDU.Extensions.SPanel sPanel1;
        private BDU.Extensions.SPanel pnl_BackMessagebox;
    }
}