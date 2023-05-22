using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BDU.UTIL;

namespace servr.integratex.ui
{
    public partial class ServrMessageBox : Form
    {
        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse 
        //);

        static ServrMessageBox messageBox;      
        static Enums.MESSAGERESPONSETYPES BUTTON_RESPONSE;
        static int messageType =1;  
      //  static float fontSize =12;
        static int button;//  1 - OK, 2- Confrimation, 3- Error
        //int disposeFormTimer;
       
        public ServrMessageBox()
        {
            InitializeComponent();
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 100, 100));
            //pnl_Border.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pnl_Border.Width, pnl_Border.Height, 100, 100));
        }

        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES ShowBox(string txtMessage)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
                messageBox.Pic_MessageError.Image = Properties.Resources.complete;
                messageBox.lblMessage.Text = txtMessage;
                //  messageBox.lblMessage.Font = new Font("Times New Roman", 12.0f, FontStyle.Regular);
                messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
                // messageBox.lblMessage.BackColor= Color.FromArgb(23, 63, 95);
                messageType = 1;
                messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }

        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES ShowBox(string txtMessage, string txtTitle)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
                messageBox.Pic_MessageError.Image = Properties.Resources.complete;
                // messageBox.lblBlazorTitle.Text = txtTitle;
                messageBox.lblMessage.Text = txtMessage;
                //  messageBox.lblMessage.Font = new Font("Times New Roman", 12.0f, FontStyle.Regular);
                messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
                // messageBox.lblMessage.BackColor = Color.FromArgb(23, 63, 95);
                messageType = 1;
                messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES Confirm(string txtMessage, string txtTitle)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
                messageBox.Pic_MessageError.Image = Properties.Resources.info;
                // messageBox.lblBlazorTitle.Text = txtTitle;
                messageBox.lblMessage.Text = txtMessage;
                messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
                // Font font = new Font("Times New Roman",10.0f,FontStyle.Bold);            
                // Set Font property and then add a new Label.            
                // messageBox.lblMessage.Font = font;
                messageType = 2;
                messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES Delete(string txtMessage, string txtTitle)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
                messageBox.Pic_MessageError.Image = Properties.Resources.warning;
            //messageBox.lblBlazorTitle.Text = txtTitle;
            messageBox.lblMessage.Text = txtMessage;
            messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;         
            messageType = 2;
            messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES Error(string txtMessage, string txtTitle, float fontSize=11)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
            // messageBox.lblBlazorTitle.Text = txtTitle;
            messageBox.Pic_MessageError.Image = Properties.Resources.error;
            //this.Pic_MessageError.BackgroundImage = global::servr.integratex.ui.Properties.Resources.MessageBox_Question;
            messageBox.lblMessage.Text = txtMessage;
            messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.FontFamily, fontSize);
            //messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.Bold, FontBold);
            messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
            messageType = 3;
            messageBox.ShowDialog();
        }
            return BUTTON_RESPONSE;
        }
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES Error(string txtMessage, float fontSize = 11)
        
        
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null)
            {
                messageBox.Pic_MessageError.Image = Properties.Resources.error;
            // messageBox.lblBlazorTitle.Text = txtTitle;
            messageBox.lblMessage.Text = txtMessage;
            messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.FontFamily, fontSize);
            //messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.Bold, FontBold);
            messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
            messageType = 3;
            messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }
        public static BDU.UTIL.Enums.MESSAGERESPONSETYPES warning(string txtMessage, float fontSize = 11)
        {
            messageBox = new ServrMessageBox();
            if (messageBox != null) { 
            messageBox.Pic_MessageError.Image = Properties.Resources.warning;
            // messageBox.lblBlazorTitle.Text = txtTitle;
            messageBox.lblMessage.Text = txtMessage;
            messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.FontFamily, fontSize);
            //messageBox.lblMessage.Font = new Font(messageBox.lblMessage.Font.Bold, FontBold);
            messageBox.lblMessage.ForeColor = GlobalApp.Btn_ServrBlackColor;
            messageType = 3;
            messageBox.ShowDialog();
            }
            return BUTTON_RESPONSE;
        }
        private void Messagebox_Load(object sender, EventArgs e)
        {
           // panel1.Location = new Point(7, 108);
            btnOK.Location = new Point(127, 185);
            btnCancel.Location = new Point(62, 185);
            btnConfirm.Location = new Point(192, 185);
            //string soundFile = "BlazorNotification.wav";
           
            if (messageType == 1) {
                this.btnConfirm.Visible = false;
                this.btnCancel.Visible = false;
                this.btnOK.Visible = true;
                //soundFile = "BlazorNotification.wav";
            }
            else if(messageType == 2)
            {
                panel1.Location = new Point(7, 112);
                this.btnOK.Visible = false;
                // this.btnCancel.Visible = false;
              //  this.btnCancel.Focus= false
                this.btnConfirm.Visible = true;
                //soundFile = "BlazorConfirmation.wav";
            }
            else if (messageType == 3)
            {
              
                this.btnOK.Visible = true;
                this.btnConfirm.Visible = false;
                this.btnCancel.Visible = false;
                //soundFile = "BlazorError.wav";
            }
            PlaceLowerRight();
        }
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                BUTTON_RESPONSE = BDU.UTIL.Enums.MESSAGERESPONSETYPES.OK;
                messageBox.Close();
                messageBox.Dispose();
            }
            catch (Exception ex) { }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                BUTTON_RESPONSE = Enums.MESSAGERESPONSETYPES.CANCEL;
                messageBox.Close();
                messageBox.Dispose();
            }
            catch (Exception ex) { }
        }       

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BUTTON_RESPONSE = Enums.MESSAGERESPONSETYPES.CONFIRM;
            messageBox.Close();
            messageBox.Dispose();
        }
            catch (Exception ex) { }
}
        private void PlaceLowerRight()
        {
            try
            {

                int borderRadius = 103;
                RectangleF Rect = new RectangleF(0, 0, this.Width - 3, this.Height - 3);
                GraphicsPath GraphPath = GraphicsExtension.GetRoundPath(Rect, borderRadius);
                this.Region = new Region(GraphPath);
            }
            catch (Exception ex) { }

        }
        //internal static int Error()
        //{
        //    throw new NotImplementedException();
        //}
    }
}