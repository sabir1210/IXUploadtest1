using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BDU.Services;
using BDU.UTIL;
using BDU.ViewModels;
using NLog;


namespace servr.integratex.ui
{
    public partial class BDUInegationNewForm : Form
    {
        private MappingBindingViewModel editModel = null;
        private Logger _log = LogManager.GetCurrentClassLogger();
        BDUService _service = new BDUService();
        public List<MappingBindingViewModel> m_fields = null;

        public string fuid = null;
        public int EntityId = 0;
        public int entity_type_id; 
        public string entityName = string.Empty;
        public bool isExisting = false;
        private bool isEditMode = false;
        private int serialNo = 0;
        //private int entity_type;

        public BDUInegationNewForm()
        {
            InitializeComponent();
            this.cmbDataType_Source.Name = "-- Select Data Type --";
            this.cmbControlType.Name = "-- Select Control Type --";

            this.tgl_Feed.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
            this.tgl_Scan.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
            this.tgl_ControlStatus.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.isEditMode)
            {
                Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Are you sure, want to discard changes & close?", "Confirmation!");
                if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
                {
                    this.Close();
                }
            }
            else this.Close();
        }
        private void BDUInegationNewForm_Load(object sender, EventArgs e)
        {
            try
            {
                PlaceLowerRight();
                // Set Entity Name as heading
                this.lblMappingForm.Text = entityName;
                this.ControlBox = false;
                if (this.isExisting)
                {
                    editModel = m_fields.Where(x => x.fuid == this.fuid && !string.IsNullOrWhiteSpace(fuid) && x.entity_id == this.EntityId).FirstOrDefault();
                    this.btnAdd.Text = "Save";
                }
                else if (m_fields != null)
                {
                    // serialNo = 0; // m_fields.Where(x => x.entity_id == this.EntityId).Max(t => t.fieldsr)+1;
                    this.isExisting = false;
                    this.btnAdd.Text = "Add";
                }

                // frm.fieldId = Convert.ToInt32(model.fieldId);
                //******************************* Fill Data Types***************************//            
                this.cmbDataType_Source.ValueMember = "id";
                this.cmbDataType_Source.DisplayMember = "name";
                cmbDataType_Source.DataSource = GlobalApp.dataTypes();



                //******************************* Fill Control Type***************************//
                this.cmbControlAction.ValueMember = "id";
                this.cmbControlAction.DisplayMember = "name";
                cmbControlAction.DataSource = GlobalApp.controlAction();
                //******************************* Fill Control Type***************************//
                this.cmbAutomationMode.ValueMember = "id";
                this.cmbAutomationMode.DisplayMember = "name";
                cmbAutomationMode.DataSource = GlobalApp.AUTOMATION_MODES();
                //******************************* Fill Parent Controls***************************//
                List<ParentsViewModel> parents = new List<ParentsViewModel>();

                if (m_fields != null)
                {
                    var rs = (from x in m_fields
                              where x.entity_id == this.EntityId && (x.control_type == (int)Enums.CONTROL_TYPES.FORM || x.control_type == (int)Enums.CONTROL_TYPES.FRAME)
                              && (x.parent_field_id != this.fuid || string.IsNullOrWhiteSpace(this.fuid))
                              select new ParentsViewModel
                              {
                                  id = x.fuid,                          // reference = r.mode==1 ? r.xpath:(r.id *2).ToString(),
                                  name = string.IsNullOrWhiteSpace(x.field_desc) ? x.pms_field_xpath : x.field_desc
                              }).ToList();
                    parents = rs;
                }//if (m_fields != null)

                parents.Add(new ParentsViewModel { id = "", name = "-- SELECT --" });
                this.cmbParentControl.DisplayMember = "name";
                this.cmbParentControl.ValueMember = "id";
                cmbParentControl.DataSource = parents.Distinct().OrderBy(x => x.id).ToList();
                //******************************* Fill Control Type***************************//
                this.cmbControlType.ValueMember = "id";
                this.cmbControlType.DisplayMember = "name";
                cmbControlType.DataSource = GlobalApp.controlTypes();
                //if (editModel != null)
                //{
                //    if (!string.IsNullOrWhiteSpace(editModel.fuid))
                //        this.FillEditData();
                //    // if (Convert.ToInt16(cmbControlType.SelectedValue) != (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX)
                //    // this.txtMaxLength.Enabled = false;
                //}
                if (editModel != null)
                {
                    if (!string.IsNullOrWhiteSpace(editModel.fuid))
                        this.FillEditData();
                    //if (Convert.ToInt16(cmbDataType_Source.SelectedValue) != (int)UTIL.Enums.DATA_TYPES.TEXT)
                    //    this.txtMaxLength.Enabled = false;
                }
            }catch(Exception ex){ }
        }
        private void FillEditData()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(editModel.fuid))
                {

                    this.txtfield_desc.Text = "" + editModel.field_desc;
                    //this.cmbParentControl.SelectedValue = ""+editModel.parent_field_id;
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(cmbParentControl.SelectedValue)))
                    {
                        this.cmbParentControl.SelectedValue = "";
                    }
                    else
                    {
                        this.cmbParentControl.SelectedValue = "" + editModel.parent_field_id;
                    }
                    this.txtfield_desc.Enabled = false;
                    this.txtDefaultValue.Text = "" + editModel.default_value;
                    this.txtpms_field_expression.Text = "" + editModel.pms_field_expression;
                    this.txtpms_field_xpath.Text = "" + editModel.pms_field_xpath;
                    this.txtpms_field_name.Text = "" + editModel.pms_field_name;
                    this.txtfieldformat.Text = "" + editModel.fieldformat;

                    this.txtTag.Text = "" + editModel.custom_tag;

                    if (string.IsNullOrWhiteSpace(editModel.pmspageid))
                        this.txtFormId.Text = "form1";
                    else
                        this.txtFormId.Text = "" + editModel.pmspageid;
                    this.cmbDataType_Source.SelectedValue = (int)editModel.data_type;
                    this.cmbControlType.SelectedValue = (int)editModel.control_type;
                    this.cmbControlAction.SelectedValue = (int)editModel.control_action;
                    this.cmbAutomationMode.SelectedValue = (int)editModel.automation_mode;
                    if ((int)editModel.automation_mode == (int)Enums.AUTOMATION_MODES.OCR && System.IO.File.Exists(string.Format(GlobalApp.UIVInstalPath + "\\{0}\\{1}", BDUConstants.Hybrid_Automation_MediaFiles, "" + editModel.ocrImage)))
                    {
                        this.txtaccuracy.Text = (Convert.ToDouble(editModel.ocrFactor)> 0.0? Math.Round(editModel.ocrFactor,1):0.6).ToString();
                        Pic_OCR.Image = Bitmap.FromFile(string.Format(GlobalApp.UIVInstalPath + "\\{0}\\{1}", BDUConstants.Hybrid_Automation_MediaFiles, editModel.ocrImage));
                    }
                    //Pic_OCR.Image  = Bitmap.FromFile(editModel.ocrImage);
                    //FileStream fs = new System.IO.FileStream(@"Images\a.bmp", FileMode.Open, FileAccess.Read);
                    //pictureBox1.Image = Image.FromStream(fs);
                    //Pic_OCR.Image = Image.FromStream(editModel.fuid);
                    //this.cmbAutomationMode.SelectedValue = (int)editModel.contr;
                    this.txtMaxLength.Text = Convert.ToString(editModel.maxLength);
                    // editModel.maxLength = Convert.ToInt32(this.txtMaxLength.Text);
                    if (Convert.ToBoolean(editModel.fieldmanadatory))
                        this.rdbYes_Mandatory.Checked = true;
                    else
                        this.rdbNo_Mandatory.Checked = true;
                    //Feed

                    // Scan
                    if (Convert.ToBoolean(editModel.feed))
                        this.tgl_Feed.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
                    else
                        this.tgl_Feed.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;
                    // Control Enable / Disable
                    if (Convert.ToBoolean(editModel.fieldstatus))
                        this.tgl_ControlStatus.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
                    else
                        this.tgl_ControlStatus.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;
                    // Scan
                    if (Convert.ToBoolean(editModel.scan))
                        this.tgl_Scan.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
                    else
                        this.tgl_Scan.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;


                    if (Convert.ToBoolean(editModel.is_reference))
                        this.rdbIsReference_Yes.Checked = true;
                    else
                        this.rdbIsReference_No.Checked = true;

                }
            }
            catch (Exception ex) {
                _log.Error(ex);
                servr.integratex.ui.ServrMessageBox.ShowBox(string.Format("Unable to load field mapping, error -{0}", ex.Message), "Error!");
            }
        }
        private void ResetFormData()
        {
            this.lblMappingForm.Text = "";
            txtfield_desc.Enabled = true;
            this.txtFormId.Text = "";
            //this.txtIdentifier.Text = "";
            this.txtpms_field_name.Text = "";
            this.txtpms_field_xpath.Text = "";
            this.txtfield_desc.Text = "";
            this.txtDefaultValue.Text = "";
            this.txtMaxLength.Text = "";
            this.txtfieldformat.Text = "";
            this.txtpms_field_expression.Text = "";
            cmbParentControl.SelectedIndex = 0;
            cmbControlType.SelectedIndex = 0;
            cmbDataType_Source.SelectedIndex = 0;
            cmbControlAction.SelectedIndex = 0;
            cmbAutomationMode.SelectedIndex = 0;
            rdbIsReference_No.Checked = true;
            rdbIsReference_Yes.Checked = false;
            rdbYes_Mandatory.Checked = true;
            rdbNo_Mandatory.Checked = false;
            this.tgl_Feed.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;
            this.tgl_Scan.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.OFF;
            this.tgl_ControlStatus.ToggleState = BDU.Extensions.ToggleButton.ToggleButtonState.ON;
            this.isExisting = false;
            Pic_OCR.Refresh();
            this.txtaccuracy.Text = "0.6";
            Pic_OCR.Image = global::servr.integratex.ui.Properties.Resources.No_Image;
            //Pic_OCR.Image= project.Properties.Resources.imgfromresource;
            //Pic_OCR.Image = Image.FromFile(@"\Resources\NoImage.jpg");
            // Pic_OCR.BackgroundImage=Image.;
            //this.Pic_OCR.Image = null;
        }
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            //Screen rightmost = Screen.AllScreens[0];
            //foreach (Screen screen in Screen.AllScreens)
            //{
            //    if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
            //        rightmost = screen;
            //}
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) - 427),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) - 206)
            };
            //this.Left = rightmost.WorkingArea.Right - this.Width;
            //this.Top = rightmost.WorkingArea.Bottom - this.Height - 12;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (performValidation())
                {
                    if (!isExisting)
                    {
                        MappingBindingViewModel newModel = new MappingBindingViewModel();
                        newModel.entity_id = EntityId;
                        newModel.fuid = (Guid.NewGuid()).ToString();
                        newModel.entity_status = (int)Enums.STATUSES.Active;
                        newModel.entity_type = entity_type_id;//(int)UTIL.Enums.ENTITY_TYPES.SYNC;
                        newModel.fieldstatus = (int)Enums.STATUSES.Active;
                        newModel.is_unmapped = 1;
                        if (m_fields != null)
                        {
                            serialNo = m_fields.Where(x => x.entity_id == this.EntityId ).Max(x=>x.fieldsr) + 1;
                            newModel.fieldsr = serialNo;
                        }
                        else
                            newModel.fieldsr = 1;

                        newModel.field_desc = this.txtfield_desc.Text;
                        newModel.pmspageid = this.txtFormId.Text;
                        //newModel.pmspagename = this.txtIdentifier.Text;
                        newModel.pms_field_name = this.txtpms_field_name.Text;
                        newModel.pms_field_xpath = this.txtpms_field_xpath.Text;
                        newModel.pms_field_expression = this.txtpms_field_expression.Text;
                        newModel.data_type = Convert.ToInt32(this.cmbDataType_Source.SelectedValue);
                        newModel.control_type = Convert.ToInt32(this.cmbControlType.SelectedValue);
                        newModel.custom_tag = this.txtTag.Text;
                        newModel.control_action = Convert.ToInt32(this.cmbControlAction.SelectedValue);
                        newModel.automation_mode = Convert.ToInt32(this.cmbAutomationMode.SelectedValue);
                        newModel.maxLength = Convert.ToInt32(string.IsNullOrWhiteSpace(this.txtMaxLength.Text) ? "0" : this.txtMaxLength.Text);
                        //editModel.maxLength = Convert.ToInt32(string.IsNullOrWhiteSpace(this.txtMaxLength.Text) ? "0" : this.txtMaxLength.Text);
                        newModel.fieldformat = this.txtfieldformat.Text;
                        newModel.default_value = this.txtDefaultValue.Text;
                    
                        if (rdbYes_Mandatory.Checked)
                            newModel.fieldmanadatory = 1;
                        else
                            newModel.fieldmanadatory = 0;

                        if (rdbIsReference_Yes.Checked)
                            newModel.is_reference = 1;
                        else
                            newModel.is_reference = 0;
                        //editModel.fieldsr = serialNo;

                        // editModel.fuid = new Guid(); no need to set here
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(cmbParentControl.SelectedValue)))
                        {
                            newModel.parent_field_id = ""+cmbParentControl.SelectedValue;
                            //newModel.parent_field_id = "";
                        }
                        else
                        {
                            newModel.parent_field_id = "";
                        }

                        //Control Enable / Disable
                        if (this.tgl_ControlStatus.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            newModel.fieldstatus = 1;
                        else
                            newModel.fieldstatus = 0;

                        //Feed
                        if (this.tgl_Feed.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            newModel.feed = 1;
                        else
                            newModel.feed = 0;
                        // Scan                   
                        if (this.tgl_Scan.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            newModel.scan = 1;
                        else
                            newModel.scan = 0;

                        if (!isExisting)
                        {
                            if (Pic_OCR.Image != null && newModel.automation_mode == (int)Enums.AUTOMATION_MODES.OCR)// && Pic_OCR.Image.GetPropertyItem("Name").ToString() != "NoImage.jpg")
                            {
                                newModel.ocrFactor = Convert.ToDouble(txtaccuracy.Text) > 0.0 ? Math.Round(Convert.ToDouble(txtaccuracy.Text), 1) : 0.6;
                                string fileName = Guid.NewGuid().ToString();
                                string filePath = string.Format(GlobalApp.UIVInstalPath + "\\{0}\\{1}", BDUConstants.Hybrid_Automation_MediaFiles, fileName + ".png");
                                using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate))
                                {
                                    Bitmap bImage = new Bitmap(Pic_OCR.Image);                                   
                                    using (MemoryStream mstream = new MemoryStream())
                                    {
                                        bImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                        stream.Write(mstream.ToArray());
                                    }
                                    stream.Flush();
                                    stream.Close();
                                    stream.Dispose();
                                }
                                newModel.ocrImage = fileName + ".png";
                            }
                             m_fields.Add(newModel);
                        }
                    }
                    else {

                        editModel.field_desc = this.txtfield_desc.Text;
                        editModel.pmspageid = this.txtFormId.Text;
                        //editModel.pmspagename = this.txtIdentifier.Text;
                        editModel.pms_field_name = this.txtpms_field_name.Text;
                        editModel.pms_field_xpath = this.txtpms_field_xpath.Text;
                        editModel.pms_field_expression = this.txtpms_field_expression.Text;
                        editModel.data_type = Convert.ToInt32(this.cmbDataType_Source.SelectedValue);
                        editModel.control_type = Convert.ToInt32(this.cmbControlType.SelectedValue);
                        editModel.custom_tag = this.txtTag.Text;
                        editModel.automation_mode = Convert.ToInt32(this.cmbAutomationMode.SelectedValue);
                        editModel.control_action = Convert.ToInt32(this.cmbControlAction.SelectedValue);
                        editModel.maxLength = Convert.ToInt32(string.IsNullOrWhiteSpace(this.txtMaxLength.Text) ? "0" : this.txtMaxLength.Text);
                        //editModel.maxLength = Convert.ToInt32(string.IsNullOrWhiteSpace(this.txtMaxLength.Text) ? "0" : this.txtMaxLength.Text);
                        editModel.fieldformat = this.txtfieldformat.Text;
                        editModel.default_value = this.txtDefaultValue.Text;
                        if (rdbYes_Mandatory.Checked)
                            editModel.fieldmanadatory = 1;
                        else
                            editModel.fieldmanadatory = 0;

                        if (rdbIsReference_Yes.Checked)
                            editModel.is_reference = 1;
                        else
                            editModel.is_reference = 0;

                        //Feed
                        if (this.tgl_Feed.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            editModel.feed = 1;
                        else
                            editModel.feed = 0;
                        // Scan                   
                        if (this.tgl_Scan.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            editModel.scan = 1;
                        else
                            editModel.scan = 0;

                        //Control Enable / Disable
                        if (this.tgl_ControlStatus.ToggleState == BDU.Extensions.ToggleButton.ToggleButtonState.ON)
                            editModel.fieldstatus = 1;
                        else
                            editModel.fieldstatus = 0;
                        //editModel.fieldsr = serialNo;

                        // editModel.fuid = new Guid(); no need to set here
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(cmbParentControl.SelectedValue)))
                        {
                            editModel.parent_field_id = "" + cmbParentControl.SelectedValue;
                        }
                        else
                        {
                            editModel.parent_field_id = "" ;
                        }
                        if (Pic_OCR.Image != null && editModel.automation_mode == (int)Enums.AUTOMATION_MODES.OCR)// && Pic_OCR.Image.GetPropertyItem("Name").ToString() != "NoImage.jpg")
                        {
                            editModel.ocrFactor = Convert.ToDouble(txtaccuracy.Text) > 0.0 ? Math.Round(Convert.ToDouble(txtaccuracy.Text), 1) : 0.6;
                           // newModel.ocrFactor
                            string fileName = Guid.NewGuid().ToString();
                            string filePath = string.Format(GlobalApp.UIVInstalPath + "\\{0}\\{1}", BDUConstants.Hybrid_Automation_MediaFiles, fileName + ".png");
                           
                            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate))
                            {
                                Bitmap bImage = new Bitmap(Pic_OCR.Image);
                               // var mstreamArr ;
                                using (MemoryStream mstream = new MemoryStream())
                                {
                                    bImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                    stream.Write(mstream.ToArray());
                                }                               
                                stream.Flush();
                                stream.Close();
                                stream.Dispose();

                            }
                           // Pic_OCR.Image.Save(filePath, ImageFormat.Png);
                            editModel.ocrImage = fileName + ".png";
                        }
                        // m_fields.Remove(editModel);
                        m_fields.Add(editModel);
                    }
 
                    servr.integratex.ui.ServrMessageBox.ShowBox("Field mapping has been added successfully.", "Success");
                    Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Would you like to add another field?", "Confirmation!");
                    if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
                    {
                        ResetFormData();
                        this.isEditMode = false;
                       
                    }
                    else
                    {
                        //this.isEditMode = false;
                        this.Close();
                    }

                    if (this.isEditMode) {
                        this.btnAdd.Text = "Save";
                    }
                    else
                        this.btnAdd.Text = "Add";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                servr.integratex.ui.ServrMessageBox.Error("This additional mapping has failed, for hybrid field image is required, try again with fix or contact support@servrhotels.com for assistance.", "Failed");
            }
        }
        private bool performValidation()
        {
            double factor;
             if (string.IsNullOrWhiteSpace(txtpms_field_expression.Text) && string.IsNullOrWhiteSpace(txtpms_field_name.Text) && string.IsNullOrWhiteSpace(txtpms_field_xpath.Text) && (Convert.ToInt32(cmbAutomationMode.SelectedValue) <=0) || Pic_OCR.Image == null)
            {
                if (string.IsNullOrWhiteSpace(txtpms_field_expression.Text) && string.IsNullOrWhiteSpace(txtpms_field_name.Text) && string.IsNullOrWhiteSpace(txtpms_field_xpath.Text))
                {
                    
                    ServrMessageBox.Error(lblFieldName.Text + " is required");
                    this.txtpms_field_name.Focus();
                    return false;
                }
                else if ((Convert.ToInt32(cmbAutomationMode.SelectedValue) == (int)Enums.AUTOMATION_MODES.OCR) || Pic_OCR.Image == null)
                {
                    ServrMessageBox.Error(lblOCRImage.Text + " is required");
                    this.cmbAutomationMode.Focus();
                    return false;
                }
            }
            else if ((txtpms_field_name.Text.Length < 2 || string.IsNullOrWhiteSpace(txtpms_field_name.Text)) && (txtpms_field_expression.Text.Length < 4 || string.IsNullOrWhiteSpace(txtpms_field_expression.Text)) && (txtpms_field_xpath.Text.Length < 4 || string.IsNullOrWhiteSpace(txtpms_field_xpath.Text)) && (Convert.ToInt32(cmbAutomationMode.SelectedValue) != (int)Enums.AUTOMATION_MODES.OCR) || (Pic_OCR.Image == null))
                {
                ServrMessageBox.Error(this.lblPMSUniqueID.Text + " is required");
                this.txtpms_field_xpath.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbAutomationMode.SelectedValue) == (int)Enums.AUTOMATION_MODES.OCR  && Double.TryParse(("" + txtaccuracy.Text), out factor) && ((""+txtaccuracy.Text).Length >=4 || (""+txtaccuracy.Text).Length < 3 || string.IsNullOrWhiteSpace(txtaccuracy.Text) || Convert.ToDouble(txtaccuracy.Text) > 1.0 || Convert.ToDouble(txtaccuracy.Text) <= 0.0) )
            {
                ServrMessageBox.Error("Accuracy factor must be betweeb 0.1 ~ 1.0");
                this.txtaccuracy.Focus();
                return false;
            }
            else if (txtfield_desc.Text.Length < 2 || txtfield_desc.Text == "")
            {
                ServrMessageBox.Error(lblfield_desc.Text + " is required");
                this.txtfield_desc.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbControlType.SelectedValue) == (int)Enums.CONTROL_TYPES.TEXTBOX && ((int)Enums.ENTITY_TYPES.ACCESS_MNGT ==this.entity_type_id ) && txtDefaultValue.Text == "")
            {
                if (Convert.ToInt32(cmbControlAction.SelectedValue) != (int)Enums.CONTROl_ACTIONS.Manual_Input)
                {
                    ServrMessageBox.Error(lblDefaultValue.Text + " is required");
                    return false;
                }
                else
                {

                }
                    
            }
            //else if (Convert.ToInt32(cmbAutomationMode.SelectedValue) == (int)UTIL.Enums.AUTOMATION_MODES.OCR)
            //{
            //    this.btn_BrowseOCR.Enabled = true;
            //    return false;
            //}
            else if (Convert.ToInt32(cmbControlType.SelectedValue) == (int)Enums.CONTROl_ACTIONS.INPUT && txtDefaultValue.Text == "")
            //else if (cmbControlType.SelectedIndex == 1 && entity_type_id == 2 && txtDefaultValue.Text == "")
            {                
                //ServrMessageBox.Error(lblDefaultValue.Text + " is must be required");
                return false;
            }
            else if (cmbDataType_Source.SelectedIndex == -1 || cmbDataType_Source.SelectedItem == null)
            {
                ServrMessageBox.Error("Please select the data type from the list, under Datatype", "Acknowledgement");
                this.cmbDataType_Source.Focus();
                return false;
            }
            else if (cmbControlType.SelectedIndex == -1 || cmbControlType.SelectedItem == null)
            {
                ServrMessageBox.Error("Please select the control type from the list, under Control type", "Acknowledgement");
                this.cmbControlType.Focus();
                return false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(txtMaxLength.Text, "[^0-9.]"))
            {
                ServrMessageBox.Error(string.Format(" Please only enter in {0} numbers within this field e.g: 0-9 ", lblMaxLength.Text ));
                this.txtMaxLength.Focus();
                return false;
            }
            return true;
        }
        private void cmbDataType_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDataType_Source.SelectedItem != null)
            {
                int selectDType = Convert.ToInt32(this.cmbDataType_Source.SelectedValue);
                if ((selectDType == ((int)Enums.DATA_TYPES.TEXT)) && (selectDType == ((int)Enums.DATA_TYPES.TEXT_MULTI)))
                {
                    this.txtMaxLength.Enabled = true;
                }
                else if ((selectDType != ((int)Enums.DATA_TYPES.TEXT)) && (selectDType != ((int)Enums.DATA_TYPES.TEXT_MULTI)))
                {
                    this.txtMaxLength.Text ="0";
                    this.txtMaxLength.Enabled = false;
                }
            }
        }
        private void txtFieldName_TextChanged(object sender, EventArgs e)
        {
            this.isEditMode = true;
        }

        private void btn_MainMenuClose_Click(object sender, EventArgs e)
        {
            Enums.MESSAGERESPONSETYPES res = servr.integratex.ui.ServrMessageBox.Confirm("Are you sure you would like to log out?", "Confirmation!");

            if (res == Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                Application.Exit();
            }
        }
        private void cmbDataType_Source_DrawItem(object sender, DrawItemEventArgs e)
        {
            // By using Sender, one method could handle multiple ComboBoxes
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();

                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment.  Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);

                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;
                    try
                    {
                        if (cbx.Items[e.Index].ToString().Contains("System.Data.DataRowView"))
                        {
                            DataRowView dv = (DataRowView)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(dv.Row.ItemArray[1]), cbx.Font, brush, e.Bounds, sf);
                        }
                        else if (cbx.Items[e.Index].ToString().Contains("EnumViewModel"))
                        {
                            EnumViewModel item = (EnumViewModel)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(item.name.ToString()), cbx.Font, brush, e.Bounds, sf);
                        }
                        else if (cbx.Items[e.Index].ToString().Contains("ParentsViewModel"))
                        {
                            ParentsViewModel item = (ParentsViewModel)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(item.name.ToString()), cbx.Font, brush, e.Bounds, sf);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        private void cmbControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbControlType.SelectedItem != null)
            {
                int selectedControl = Convert.ToInt32(this.cmbControlType.SelectedValue);
                if ((selectedControl == ((int)Enums.CONTROL_TYPES.TEXTBOX)))
                {
                    // this.txtMaxLength.Enabled = true;
                }
                else
                {
                    // this.txtMaxLength.Enabled = false;
                }
            }
        }
        private void cmbControlType_DrawItem(object sender, DrawItemEventArgs e)
        {
            // By using Sender, one method could handle multiple ComboBoxes
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();

                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment.  Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    //sf.LineAlignment = StringAlignment.Center;
                    //sf.Alignment = StringAlignment.Center;

                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);

                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;
                    try
                    {
                        if (cbx.Items[e.Index].ToString().Contains("System.Data.DataRowView"))
                        {
                            DataRowView dv = (DataRowView)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(dv.Row.ItemArray[1]), cbx.Font, brush, e.Bounds, sf);
                        }
                        else if (cbx.Items[e.Index].ToString().Contains("EnumViewModel"))
                        {
                            EnumViewModel item = (EnumViewModel)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(item.name.ToString()), cbx.Font, brush, e.Bounds, sf);
                        }
                        else if (cbx.Items[e.Index].ToString().Contains("ParentsViewModel"))
                        {
                            ParentsViewModel item = (ParentsViewModel)cbx.Items[e.Index];
                            e.Graphics.DrawString(Convert.ToString(item.name.ToString()), cbx.Font, brush, e.Bounds, sf);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void Pic_OCR_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.PNG); | *.PNG;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Pic_OCR.Load(ofd.FileName);

                }
                catch (Exception ex)
                {
                    ServrMessageBox.Error("Security error.\n\nError message: {ex.Message}\n\n Details:\n\n{ex.StackTrace}");
                    //BlazorMessageBox.ShowBox("Security error.\n\nError message: {ex.Message}\n\n Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void cmbAutomationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbAutomationMode.SelectedValue) == (int)Enums.AUTOMATION_MODES.OCR)
            {
                this.Pic_OCR.Enabled = true;
                this.txtaccuracy.Text = "0.6";
                this.txtaccuracy.Enabled = true;
                
            }
            else
            {
                this.Pic_OCR.Enabled = false;
                this.txtaccuracy.Text = "0.6";
                this.txtaccuracy.Enabled = false;
            }

        }
       
    }
}

