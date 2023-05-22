
namespace WindowsFormsTestApplication
{
    partial class MainForm
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
            this.btnPMS = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnTabbed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPMS
            // 
            this.btnPMS.Location = new System.Drawing.Point(28, 57);
            this.btnPMS.Name = "btnPMS";
            this.btnPMS.Size = new System.Drawing.Size(108, 90);
            this.btnPMS.TabIndex = 0;
            this.btnPMS.Text = "PMS Single";
            this.btnPMS.UseVisualStyleBackColor = true;
            this.btnPMS.Click += new System.EventHandler(this.btnPMS_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(291, 57);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(111, 90);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test Form";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnTabbed
            // 
            this.btnTabbed.Location = new System.Drawing.Point(163, 57);
            this.btnTabbed.Name = "btnTabbed";
            this.btnTabbed.Size = new System.Drawing.Size(105, 90);
            this.btnTabbed.TabIndex = 2;
            this.btnTabbed.Text = "PMS Tabbed";
            this.btnTabbed.UseVisualStyleBackColor = true;
            this.btnTabbed.Click += new System.EventHandler(this.btnTabbed_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 264);
            this.Controls.Add(this.btnTabbed);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnPMS);
            this.Name = "MainForm";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPMS;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnTabbed;
    }
}