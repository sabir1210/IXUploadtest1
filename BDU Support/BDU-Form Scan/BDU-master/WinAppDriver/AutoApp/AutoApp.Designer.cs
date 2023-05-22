
namespace AutoApp
{
    partial class AutoApp
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
            this.btnDesktop = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbRoot = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnWeb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDesktop
            // 
            this.btnDesktop.Location = new System.Drawing.Point(985, 98);
            this.btnDesktop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDesktop.Name = "btnDesktop";
            this.btnDesktop.Size = new System.Drawing.Size(270, 75);
            this.btnDesktop.TabIndex = 0;
            this.btnDesktop.Text = "Save PMS data for CMS API";
            this.btnDesktop.UseVisualStyleBackColor = true;
            this.btnDesktop.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(13, 218);
            this.tbOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(1296, 1247);
            this.tbOutput.TabIndex = 1;
            // 
            // tbRoot
            // 
            this.tbRoot.Location = new System.Drawing.Point(2, 12);
            this.tbRoot.Name = "tbRoot";
            this.tbRoot.Size = new System.Drawing.Size(985, 23);
            this.tbRoot.TabIndex = 2;
            this.tbRoot.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(1034, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(221, 48);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start Server";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnWeb
            // 
            this.btnWeb.Location = new System.Drawing.Point(702, 98);
            this.btnWeb.Name = "btnWeb";
            this.btnWeb.Size = new System.Drawing.Size(237, 75);
            this.btnWeb.TabIndex = 4;
            this.btnWeb.Text = "Load CMS Api Data";
            this.btnWeb.UseVisualStyleBackColor = true;
            this.btnWeb.Click += new System.EventHandler(this.btnWeb_Click);
            // 
            // AutoApp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1321, 1045);
            this.Controls.Add(this.btnWeb);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.tbRoot);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.btnDesktop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutoApp";
            this.Text = "AutoApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDesktop;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.TextBox tbRoot;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnWeb;
    }
}

