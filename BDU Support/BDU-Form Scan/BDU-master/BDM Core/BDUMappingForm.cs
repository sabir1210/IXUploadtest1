using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bdu
{
    public partial class BDUMappingForm : Form
    {
        public BDUMappingForm()
        {
            InitializeComponent();
            btnSave.MouseEnter += new EventHandler(btnSave_MouseEnter);
            btnSave.MouseLeave += new EventHandler(btnSave_MouseLeave);

            btnCancel.MouseEnter += new EventHandler(btnCancel_MouseEnter);
            btnCancel.MouseLeave += new EventHandler(btnancel_MouseLeave);

            btnTestRun.MouseEnter += new EventHandler(btnTestRun_MouseEnter);
            btnTestRun.MouseLeave += new EventHandler(btnTestRun_MouseLeave);
            
            btnXPath.MouseEnter += new EventHandler(btnXPath_MouseEnter);
            btnXPath.MouseLeave += new EventHandler(btnXPath_MouseLeave);

            btnGetHotelMapping.MouseEnter += new EventHandler(btnGetHotelMapping_MouseEnter);
            btnGetHotelMapping.MouseLeave += new EventHandler(btnGetHotelMapping_MouseLeave);

            btnApply.MouseEnter += new EventHandler(btnApply_MouseEnter);
            btnApply.MouseLeave += new EventHandler(btnApply_MouseLeave);

            btnUpLow.MouseEnter += new EventHandler(btnUpLow_MouseEnter);
            btnUpLow.MouseLeave += new EventHandler(btnUpLow_MouseLeave);

        }

        void btnSave_MouseEnter(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnCancelHvr));
        }
        void btnancel_MouseLeave(object sender, EventArgs e)
        {
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnCancel)); ;
        } 
        void btnTestRun_MouseEnter(object sender, EventArgs e)
        {
            this.btnTestRun.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnTestRun_MouseLeave(object sender, EventArgs e)
        {
            this.btnTestRun.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnXPath_MouseEnter(object sender, EventArgs e)
        {
            this.btnXPath.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnXPath_MouseLeave(object sender, EventArgs e)
        {
            this.btnXPath.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnGetHotelMapping_MouseEnter(object sender, EventArgs e)
        {
            this.btnGetHotelMapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnBigDefaultHvr));
        }
        void btnGetHotelMapping_MouseLeave(object sender, EventArgs e)
        {
            this.btnGetHotelMapping.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnBigDefault));
        }
        void btnApply_MouseEnter(object sender, EventArgs e)
        {
            this.btnApply.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefaultHvr));
        }
        void btnApply_MouseLeave(object sender, EventArgs e)
        {
            this.btnApply.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.pic_btnDefault));
        }
        void btnUpLow_MouseEnter(object sender, EventArgs e)
        {
            this.btnUpLow.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnUpLowHvr1));
        }
        void btnUpLow_MouseLeave(object sender, EventArgs e)
        {
            this.btnUpLow.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Pic_btnUpLow));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }  
        private void BDUMappingForm_Load(object sender, EventArgs e)
        {
            MappingViewModel mvm = new MappingViewModel();
            List<MappingViewModel> lss = new List<MappingViewModel>();
            mvm.CMSFieldName = "First Name";
            mvm.CMSFieldId = 1;
            mvm.DefaultValue = "";
            mvm.FieldID = 1;
            mvm.FieldName = "First Name";
            mvm.Formate = "";
            mvm.DataType = "Text";
            mvm.Length = 12;
            mvm.Status = 1;
            mvm.XPathID = "(//input[@type='text'])[4]";
            lss.Add(mvm);

            foreach (MappingViewModel ent in lss)
            {
                ListViewItem listitem = new ListViewItem(ent.CMSFieldName.ToString());
                listitem.SubItems.Add(ent.CMSFieldName);
                listitem.SubItems.Add(ent.FieldName);
                listitem.SubItems.Add(ent.DataType);
                listitem.SubItems.Add(ent.Length.ToString());
                listitem.SubItems.Add(ent.XPathID);
                listitem.SubItems.Add(ent.DefaultValue);
                listitem.SubItems.Add("Edit > Del> Advance");
                //  InterfaceMapping_Grid..Bounds.Size = new Size(50, 50);
                InterfaceMapping_Grid.Items.Add(listitem);

                //InterfaceMapping_Grid.Items.Add(ent.CMSFieldName);
                //InterfaceMapping_Grid.Items[1].SubItems.Add(ent.FieldName);
                //InterfaceMapping_Grid.Items[2].SubItems.Add(ent.Formate);
                //InterfaceMapping_Grid.Items.Add(ent.DataType);
                //InterfaceMapping_Grid.Items.Add(ent.Length.ToString());
                //InterfaceMapping_Grid.Items.Add(ent.DefaultValue);
                //InterfaceMapping_Grid.Items.Add(ent.XPathID);
                //InterfaceMapping_Grid.Items.Add("Sync > Edit > Del> Advance");

            }
        }
        private void btnXPath_Click(object sender, EventArgs e)
        {
            BDUInegationNewForm frm = new BDUInegationNewForm();
            frm.ShowDialog();
        }

        private void grp_Presets_Enter(object sender, EventArgs e)
        {

        }
    }
}
