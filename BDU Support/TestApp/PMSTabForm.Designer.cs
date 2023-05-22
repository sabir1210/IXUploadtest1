﻿
namespace WindowsFormsTestApplication
{
    partial class PMSTabForm
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
            this.lblbooking_ref = new System.Windows.Forms.Label();
            this.lblHotel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.reference = new System.Windows.Forms.TextBox();
            this.card_number = new System.Windows.Forms.TextBox();
            this.card_address = new System.Windows.Forms.TextBox();
            this.phone_number = new System.Windows.Forms.TextBox();
            this.room_number = new System.Windows.Forms.TextBox();
            this.email = new System.Windows.Forms.TextBox();
            this.status = new System.Windows.Forms.TextBox();
            this.arrival_date = new System.Windows.Forms.DateTimePicker();
            this.departure_date = new System.Windows.Forms.DateTimePicker();
            this.hotel_id = new System.Windows.Forms.ComboBox();
            this.created_At = new System.Windows.Forms.DateTimePicker();
            this.updated_At = new System.Windows.Forms.DateTimePicker();
            this.tabData = new System.Windows.Forms.TabControl();
            this.pageInfo = new System.Windows.Forms.TabPage();
            this.pageCard = new System.Windows.Forms.TabPage();
            this.card_expiry_date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cardholder_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.tabData.SuspendLayout();
            this.pageInfo.SuspendLayout();
            this.pageCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblbooking_ref
            // 
            this.lblbooking_ref.AutoSize = true;
            this.lblbooking_ref.Location = new System.Drawing.Point(8, 17);
            this.lblbooking_ref.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblbooking_ref.Name = "lblbooking_ref";
            this.lblbooking_ref.Size = new System.Drawing.Size(67, 15);
            this.lblbooking_ref.TabIndex = 0;
            this.lblbooking_ref.Text = "Reference *";
            // 
            // lblHotel
            // 
            this.lblHotel.AutoSize = true;
            this.lblHotel.Location = new System.Drawing.Point(8, 44);
            this.lblHotel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblHotel.Name = "lblHotel";
            this.lblHotel.Size = new System.Drawing.Size(44, 15);
            this.lblHotel.TabIndex = 0;
            this.lblHotel.Text = "Hotel *";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Arrival Date *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Departure Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Card Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 193);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "Email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 140);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Phone Number *";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 167);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Room Number *";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(0, 12);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Card Number";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 218);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "CheckIn Status *";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 249);
            this.label11.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 15);
            this.label11.TabIndex = 3;
            this.label11.Text = "Created At";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 276);
            this.label12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "Updated At";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(195, 445);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(1);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(236, 25);
            this.btnSubmit.TabIndex = 20;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(38, 445);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 25);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Reset";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // reference
            // 
            this.reference.Location = new System.Drawing.Point(138, 15);
            this.reference.Margin = new System.Windows.Forms.Padding(1);
            this.reference.Name = "reference";
            this.reference.Size = new System.Drawing.Size(236, 23);
            this.reference.TabIndex = 1;
            // 
            // card_number
            // 
            this.card_number.Location = new System.Drawing.Point(130, 10);
            this.card_number.Margin = new System.Windows.Forms.Padding(1);
            this.card_number.Name = "card_number";
            this.card_number.Size = new System.Drawing.Size(236, 23);
            this.card_number.TabIndex = 5;
            // 
            // card_address
            // 
            this.card_address.Location = new System.Drawing.Point(130, 127);
            this.card_address.Margin = new System.Windows.Forms.Padding(1);
            this.card_address.Name = "card_address";
            this.card_address.Size = new System.Drawing.Size(236, 23);
            this.card_address.TabIndex = 8;
            // 
            // phone_number
            // 
            this.phone_number.Location = new System.Drawing.Point(138, 138);
            this.phone_number.Margin = new System.Windows.Forms.Padding(1);
            this.phone_number.Name = "phone_number";
            this.phone_number.Size = new System.Drawing.Size(236, 23);
            this.phone_number.TabIndex = 9;
            // 
            // room_number
            // 
            this.room_number.Location = new System.Drawing.Point(138, 165);
            this.room_number.Margin = new System.Windows.Forms.Padding(1);
            this.room_number.Name = "room_number";
            this.room_number.Size = new System.Drawing.Size(236, 23);
            this.room_number.TabIndex = 10;
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(138, 193);
            this.email.Margin = new System.Windows.Forms.Padding(1);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(236, 23);
            this.email.TabIndex = 11;
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(138, 218);
            this.status.Margin = new System.Windows.Forms.Padding(1);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(236, 23);
            this.status.TabIndex = 12;
            // 
            // arrival_date
            // 
            this.arrival_date.Location = new System.Drawing.Point(138, 76);
            this.arrival_date.Margin = new System.Windows.Forms.Padding(1);
            this.arrival_date.Name = "arrival_date";
            this.arrival_date.Size = new System.Drawing.Size(236, 23);
            this.arrival_date.TabIndex = 3;
            // 
            // departure_date
            // 
            this.departure_date.Location = new System.Drawing.Point(138, 102);
            this.departure_date.Margin = new System.Windows.Forms.Padding(1);
            this.departure_date.Name = "departure_date";
            this.departure_date.Size = new System.Drawing.Size(236, 23);
            this.departure_date.TabIndex = 4;
            // 
            // hotel_id
            // 
            this.hotel_id.FormattingEnabled = true;
            this.hotel_id.Location = new System.Drawing.Point(138, 44);
            this.hotel_id.Margin = new System.Windows.Forms.Padding(1);
            this.hotel_id.Name = "hotel_id";
            this.hotel_id.Size = new System.Drawing.Size(236, 23);
            this.hotel_id.TabIndex = 2;
            // 
            // created_At
            // 
            this.created_At.Location = new System.Drawing.Point(138, 247);
            this.created_At.Margin = new System.Windows.Forms.Padding(1);
            this.created_At.Name = "created_At";
            this.created_At.Size = new System.Drawing.Size(236, 23);
            this.created_At.TabIndex = 13;
            // 
            // updated_At
            // 
            this.updated_At.Location = new System.Drawing.Point(138, 276);
            this.updated_At.Margin = new System.Windows.Forms.Padding(1);
            this.updated_At.Name = "updated_At";
            this.updated_At.Size = new System.Drawing.Size(236, 23);
            this.updated_At.TabIndex = 14;
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.pageInfo);
            this.tabData.Controls.Add(this.pageCard);
            this.tabData.Location = new System.Drawing.Point(38, 38);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(412, 389);
            this.tabData.TabIndex = 22;
            // 
            // pageInfo
            // 
            this.pageInfo.Controls.Add(this.reference);
            this.pageInfo.Controls.Add(this.updated_At);
            this.pageInfo.Controls.Add(this.lblbooking_ref);
            this.pageInfo.Controls.Add(this.created_At);
            this.pageInfo.Controls.Add(this.lblHotel);
            this.pageInfo.Controls.Add(this.status);
            this.pageInfo.Controls.Add(this.hotel_id);
            this.pageInfo.Controls.Add(this.email);
            this.pageInfo.Controls.Add(this.label1);
            this.pageInfo.Controls.Add(this.room_number);
            this.pageInfo.Controls.Add(this.departure_date);
            this.pageInfo.Controls.Add(this.phone_number);
            this.pageInfo.Controls.Add(this.label2);
            this.pageInfo.Controls.Add(this.arrival_date);
            this.pageInfo.Controls.Add(this.label6);
            this.pageInfo.Controls.Add(this.label11);
            this.pageInfo.Controls.Add(this.label10);
            this.pageInfo.Controls.Add(this.label12);
            this.pageInfo.Controls.Add(this.label7);
            this.pageInfo.Controls.Add(this.label8);
            this.pageInfo.Location = new System.Drawing.Point(4, 24);
            this.pageInfo.Name = "pageInfo";
            this.pageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pageInfo.Size = new System.Drawing.Size(404, 361);
            this.pageInfo.TabIndex = 0;
            this.pageInfo.Text = "Info";
            this.pageInfo.UseVisualStyleBackColor = true;
            // 
            // pageCard
            // 
            this.pageCard.Controls.Add(this.card_expiry_date);
            this.pageCard.Controls.Add(this.label3);
            this.pageCard.Controls.Add(this.cardholder_name);
            this.pageCard.Controls.Add(this.label4);
            this.pageCard.Controls.Add(this.label9);
            this.pageCard.Controls.Add(this.card_number);
            this.pageCard.Controls.Add(this.card_address);
            this.pageCard.Controls.Add(this.label5);
            this.pageCard.Location = new System.Drawing.Point(4, 24);
            this.pageCard.Name = "pageCard";
            this.pageCard.Padding = new System.Windows.Forms.Padding(3);
            this.pageCard.Size = new System.Drawing.Size(404, 361);
            this.pageCard.TabIndex = 1;
            this.pageCard.Text = "Credit Card";
            this.pageCard.UseVisualStyleBackColor = true;
            // 
            // card_expiry_date
            // 
            this.card_expiry_date.Location = new System.Drawing.Point(130, 88);
            this.card_expiry_date.Margin = new System.Windows.Forms.Padding(1);
            this.card_expiry_date.Name = "card_expiry_date";
            this.card_expiry_date.Size = new System.Drawing.Size(236, 23);
            this.card_expiry_date.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Card Expiry";
            // 
            // cardholder_name
            // 
            this.cardholder_name.Location = new System.Drawing.Point(130, 51);
            this.cardholder_name.Margin = new System.Windows.Forms.Padding(1);
            this.cardholder_name.Name = "cardholder_name";
            this.cardholder_name.Size = new System.Drawing.Size(236, 23);
            this.cardholder_name.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 54);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Card Holder Name *";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog4";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // PMSTabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(484, 480);
            this.Controls.Add(this.tabData);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximumSize = new System.Drawing.Size(500, 519);
            this.MinimumSize = new System.Drawing.Size(500, 519);
            this.Name = "PMSTabForm";
            this.Text = "PMS Form";
            this.tabData.ResumeLayout(false);
            this.pageInfo.ResumeLayout(false);
            this.pageInfo.PerformLayout();
            this.pageCard.ResumeLayout(false);
            this.pageCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblbooking_ref;
        private System.Windows.Forms.Label lblHotel;
        private System.Windows.Forms.ComboBox comboBox1hotel_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox reference;
        private System.Windows.Forms.TextBox card_number;
        private System.Windows.Forms.TextBox card_address;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox room_number;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.DateTimePicker arrival_date;
        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.ComboBox hotel_id;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker updated_At;
        private System.Windows.Forms.DateTimePicker departure_date;
        private System.Windows.Forms.TextBox phone_number;
        private System.Windows.Forms.DateTimePicker dateTimePicreated_Atcker1;
        private System.Windows.Forms.DateTimePicker created_At;
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage pageInfo;
        private System.Windows.Forms.TabPage pageCard;
        private System.Windows.Forms.DateTimePicker card_expiry_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cardholder_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}