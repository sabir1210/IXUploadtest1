namespace AutoApp
{
    partial class StartupForm
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
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFocused = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.RecordedDataGridView = new System.Windows.Forms.DataGridView();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecordedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnFocused);
            this.panel1.Controls.Add(this.ClearButton);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.RecordButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1484, 123);
            this.panel1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(752, 32);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 58);
            this.button5.TabIndex = 8;
            this.button5.Text = "3";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(765, 32);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(8, 8);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(783, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(8, 8);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(672, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 58);
            this.button2.TabIndex = 5;
            this.button2.Text = "2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(593, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 58);
            this.button1.TabIndex = 4;
            this.button1.Text = "1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFocused
            // 
            this.btnFocused.Location = new System.Drawing.Point(953, 20);
            this.btnFocused.Name = "btnFocused";
            this.btnFocused.Size = new System.Drawing.Size(234, 82);
            this.btnFocused.TabIndex = 3;
            this.btnFocused.Text = "Get Focused";
            this.btnFocused.UseVisualStyleBackColor = true;
            this.btnFocused.Visible = false;
            this.btnFocused.Click += new System.EventHandler(this.btnFocused_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(1212, 20);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(246, 82);
            this.ClearButton.TabIndex = 2;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Visible = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(285, 20);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(246, 82);
            this.btnLoad.TabIndex = 1;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(26, 20);
            this.RecordButton.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(246, 82);
            this.RecordButton.TabIndex = 0;
            this.RecordButton.Text = "Scan";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // RecordedDataGridView
            // 
            this.RecordedDataGridView.AllowUserToAddRows = false;
            this.RecordedDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RecordedDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.RecordedDataGridView.BackgroundColor = System.Drawing.Color.Silver;
            this.RecordedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RecordedDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.RecordedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecordedDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.RecordedDataGridView.GridColor = System.Drawing.Color.Silver;
            this.RecordedDataGridView.Location = new System.Drawing.Point(0, 123);
            this.RecordedDataGridView.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.RecordedDataGridView.Name = "RecordedDataGridView";
            this.RecordedDataGridView.ReadOnly = true;
            this.RecordedDataGridView.RowHeadersWidth = 51;
            this.RecordedDataGridView.RowTemplate.Height = 24;
            this.RecordedDataGridView.Size = new System.Drawing.Size(1484, 217);
            this.RecordedDataGridView.TabIndex = 1;
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(12, 351);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStatus.Size = new System.Drawing.Size(1442, 1049);
            this.tbStatus.TabIndex = 2;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1501, 1045);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.RecordedDataGridView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.MaximumSize = new System.Drawing.Size(1500, 1500);
            this.MinimumSize = new System.Drawing.Size(1500, 1038);
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A Startup Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartupForm_FormClosing);
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RecordedDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.DataGridView RecordedDataGridView;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button btnFocused;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

