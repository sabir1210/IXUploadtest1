﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using BDU.ViewModels;
using System.Windows.Forms;
using BDU.Services;
using System.Linq;
using BDU.UTIL;
using Servr.UI;
using NLog;
using BDU.RobotDesktop;
using BDU.RobotWeb;
using System.IO;
using System.Diagnostics;
using System.Windows.Documents;

namespace BDU.UI
{
    public partial class BDUMappingForm : System.Windows.Forms.Form
    {
        #region "Load & Form Initialization"
        private Logger _log = LogManager.GetCurrentClassLogger();

        List<HotelViewModel> htls = new List<HotelViewModel>();
        List<PMSVersionsBindingViewModel> pmsVersions = new List<PMSVersionsBindingViewModel>();
        HotelViewModel hotelPreset = new HotelViewModel { id = GlobalApp.Hotel_id, Status = (int)UTIL.Enums.STATUSES.Active, email = GlobalApp.email, time = GlobalApp.CurrentDateTime.AddSeconds(GlobalApp.DifferenceinSecs).ToString("yyyy-MM-dd hh:mm:ss"), preferences = null, mappings = null };
        BDUService _service = new BDUService();
        List<MappingBindingViewModel> mFields = new List<MappingBindingViewModel>();

        // GET Main Form Reference
        // Form mdiForm = null;
        public DesktopHandler _desktopHandler = null;
        public WebHandler _WebHandler = null;
        int candidateRowIndex = 0;
        bool isLoadingMode = true;

        public BDUMappingForm()
        {
            InitializeComponent();
        }

        private async void BDUMappingForm_Load(object sender, EventArgs e)
        {
            try
            {
                //grvMappingData.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                //mdiForm = this.MdiParent;
                grvMappingData.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                grvMappingData.EnableHeadersVisualStyles = false;

                Cursor.Current = Cursors.WaitCursor;
                MappingViewModel mvm = new MappingViewModel();
                List<MappingViewModel> lss = new List<MappingViewModel>();
                //  RESET 
                cmbpmsVersion.DataSource = null;
                this.ControlBox = false;

                // Preset_PresetMappingControl();
                //********************** Fill dropdown PMS Tpye *****************************//

                this.cmbPMSType.ValueMember = "id";
                this.cmbPMSType.DisplayMember = "name";
                cmbPMSType.DataSource = GlobalApp.PMSType();
                //********************** Fill dropdown  *****************************//
                this.cmbMappingEntity.ValueMember = "id";
                this.cmbMappingEntity.DisplayMember = "name";
                cmbMappingEntity.DataSource = GlobalApp.MappingType();
                //********************** Fill dropdown status *****************************//                
                this.cmbStatus.ValueMember = "id";
                this.cmbStatus.DisplayMember = "name";
                this.cmbStatus.DataSource = GlobalApp.Status();
                //********************** Fill dropdown  *****************************//
                // mvm = new List<MappingViewModel>();
                //********************** Update Mode   *****************************//
                if (GlobalApp.Authentication_Done)
                {
                    if (API.HotelList == null || API.HotelList.Count <= 0)
                        API.HotelList = await _service.getHotelslist(GlobalApp.Hotel_id, (int)UTIL.Enums.STATUSES.Active);
                    if (API.HotelList != null)
                    {
                        List<EnumViewModel> hrs = (from h in API.HotelList
                                                   where h.Status == (int)UTIL.Enums.STATUSES.Active
                                                   select new EnumViewModel
                                                   {
                                                       id = h.id,
                                                       name = h.name + " (" + h.code + ")"
                                                   }).ToList();

                        cmbMappingHotel.ValueMember = "id";
                        cmbMappingHotel.DisplayMember = "name";
                        cmbMappingHotel.DataSource = hrs.OrderBy(x => x.name).ToList();
                        // if (GlobalApp.Hotel_id > 0)
                        if (GlobalApp.Hotel_id > 0 && UTIL.GlobalApp.login_role == Enums.USERROLES.PMS_Staff)
                        {
                            cmbMappingHotel.SelectedValue = GlobalApp.Hotel_id;
                            cmbMappingHotel.Enabled = false;
                            this.cmbpmsVersion.Enabled = false;
                        }
                        else if (UTIL.GlobalApp.login_role == Enums.USERROLES.PMS_Staff && GlobalApp.isNew)
                        {
                            cmbMappingHotel.SelectedValue = GlobalApp.Hotel_id;
                            cmbMappingHotel.Enabled = false;
                            this.cmbpmsVersion.Enabled = true;

                        }
                        else if (UTIL.GlobalApp.login_role == Enums.USERROLES.Servr_Staff)
                        {
                            cmbMappingHotel.SelectedValue = GlobalApp.Hotel_id;
                            cmbMappingHotel.Enabled = true;

                        }

                    }
                    //********************** PMS Versions   *****************************//
                    if (API.PMSVersionsList == null || API.PMSVersionsList.Count <= 0)
                        API.PMSVersionsList = await _service.getPMSVersions();
                    // pmsVersions = await _service.getPMSVersions();
                    if (API.PMSVersionsList != null)
                    {
                        // int id = 1;
                        List<EnumViewModel> vrs = (from v in API.PMSVersionsList
                                                   select new EnumViewModel
                                                   {
                                                       id = v.id,
                                                       name = "" + v.version
                                                   }).ToList();
                        vrs.Add(new EnumViewModel { id = 0, name = "-- Select Version--" });
                        cmbpmsVersion.ValueMember = "id";
                        cmbpmsVersion.DisplayMember = "name";
                        cmbpmsVersion.DataSource = vrs.OrderBy(x => x.id).ToList();
                        //cmbpmsVersion.SelectedIndex = 1;
                        cmbpmsVersion.SelectedValue = Convert.ToInt32(string.IsNullOrWhiteSpace(GlobalApp.PMS_Version) ? "0" : GlobalApp.PMS_Version);
                        this.cmbPMSType.SelectedValue = Convert.ToInt32(GlobalApp.SYSTEM_TYPE);
                    }
                }

                hotelPreset.mappings = null;
                hotelPreset.mappings = API.PRESETS.mappings;
                hotelPreset.preferences = API.PRESETS.preferences;
                // Load Preset Mapping

                if (hotelPreset.mappings != null)
                {
                    mFields.Clear();
                    // this.txtPMSVersion.Text = (string.IsNullOrWhiteSpace(API.PRESETS.version) ? "" : "" + API.PRESETS.version);
                    // var qry = hotelPreset.mappings.Where(x => x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS && x.status == (int)UTIL.Enums.STATUSES.Active).ToList();
                    var qry = hotelPreset.mappings.Where(x => x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && (x.entity_Id > 0 || x.id > 0)).ToList();
                    foreach (MappingViewModel enty in qry)
                    {
                        if (enty.status == (int)UTIL.Enums.STATUSES.Active)
                        {
                            foreach (FormViewModel f in enty.forms)
                            {
                                foreach (EntityFieldViewModel fds in f.fields)
                                {
                                    if (mFields.Where(x => x.fuid == fds.fuid).FirstOrDefault() == null)
                                        mFields.Add(new MappingBindingViewModel { entity_id = Convert.ToInt32(enty.id), pms_field_xpath = fds.pms_field_xpath, feed = fds.feed, scan = fds.scan, fieldsr = fds.sr, data_type = fds.data_type, maxLength = Convert.ToInt32(fds.maxLength), control_type = fds.control_type, control_action = fds.action_type, mode = enty.mode, entity_status = enty.status, entity_type = enty.entity_type, entity_name = enty.entity_name, table_name = "" + fds.table_name, formid = f.id, pmspagename = f.pmspagename, pmspageid = f.pmspageid, fuid = fds.fuid, pms_field_name = fds.pms_field_name, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, field_desc = fds.field_desc, default_value = fds.default_value, parent_field_id = fds.parent_field_id, fieldformat = fds.format, schema_field_name = fds.schema_field_name, pms_field_expression = fds.pms_field_expression, fieldmanadatory = fds.mandatory, fieldstatus = fds.status });
                                }
                            }

                        }//if (mv.status == (int)UTIL.Enums.STATUSES.Active) { 
                    }
                    //mFields = (from enty in qry
                    //           from frms in enty.forms
                    //           from fds in frms.fields
                    //           where fds.status == (int)UTIL.Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(fds.fuid) && enty.status==(int)UTIL.Enums.STATUSES.Active && frms.id >0
                    //           select new MappingBindingViewModel { entity_id = Convert.ToInt32(enty.id), pms_field_xpath = fds.pms_field_xpath,  fieldsr = fds.sr, mode = enty.mode, entity_status = enty.status, entity_type = enty.entity_type, entity_name = enty.entity_name, table_name = "" + fds.table_name, formid = frms.id, pmspagename = frms.pmspagename, pmspageid = frms.pmspageid,  fuid = fds.fuid,  pms_field_name = fds.pms_field_name, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, field_desc = fds.field_desc, default_value = fds.default_value, fieldformat = fds.format, schema_field_name = fds.schema_field_name, pms_field_expression = fds.pms_field_expression, fieldmanadatory = fds.mandatory, fieldstatus = fds.status }).Distinct().ToList();

                    this.fillDataGrid();
                }
                // Keep Status disable bc mistakenly setting it can harm
                this.cmbStatus.SelectedValue = 1;
                this.cmbStatus.Enabled = false;

            }
            catch (Exception ex)
            {
                Servr.UI.ServrMessageBox.Error(ex.StackTrace, "Error");
                _log.Error(ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                isLoadingMode = false;
            }
        }
        #endregion
        #region "Validation & Custom Functions"

        private void fillDataGrid()
        {
            this.grvMappingData.DataSource = null;
            this.grvMappingData.AutoGenerateColumns = false;
            // int Serial = 1;
            if (mFields != null && mFields.Count > 0)
            {
                var gSrc = (from flds in mFields
                            where flds.fieldstatus == (int)UTIL.Enums.STATUSES.Active && flds.entity_type != (int)Enums.ENTITY_TYPES.STATS && flds.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? flds.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue)) && !string.IsNullOrWhiteSpace(flds.fuid)// > 0
                            select new { flds.pms_field_name, mandatory = (Convert.ToInt32(flds.fieldmanadatory) > 0 ? "Yes" : "No"), flds.fieldsr, flds.pms_field_xpath, flds.default_value, flds.fuid, parentId = flds.parent_field_id, flds.entity_id, flds.is_unmapped, flds.field_desc }).ToList();// new MappingBindingViewModel{ sr = Serial++, entity_id = flds.entity_id, mode =flds.mode, entity_type= flds.entity_type, entity_name= flds.entity_name, formid = flds.formid, pmspagename= flds.pmspagename, pmspageid= flds.pmspageid , fieldId= flds.fieldId, fieldsr = flds.sr, pms_field_name= flds.pms_field_name, field_desc= flds.field_desc , default_value= flds.default_value, fieldformat = flds.fieldformat, schema_field_name= flds.schema_field_name,fieldname= flds.fieldname, fieldmanadatory = flds.fieldmanadatory, fieldstatus = flds.fieldstatus }).ToList();

                grvMappingData.DataSource = gSrc.Distinct().OrderBy(x => x.fieldsr).ToList();
                grvMappingData.Enabled = true;
                // ******  ENTITY TYPE *************************************//

                MappingViewModel l_mcm = hotelPreset.mappings.Where(x => x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_Id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue)).FirstOrDefault();
                if (l_mcm != null)
                    this.cmbStatus.SelectedValue = Convert.ToInt32(l_mcm.status);
            }
            //*************** Almas Comment this testtesting code end *******//
            // DataGridViewColumnCollection cols = this.grvMappingData.Columns; 
        }
        private bool performValidation_GetPresets()
        {
            if (cmbpmsVersion.SelectedIndex == -1 || cmbpmsVersion.SelectedItem == null)
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbpmsVersion.Tag), "Validation");
                this.cmbpmsVersion.Focus();
                return false;
            }
            else if (cmbMappingHotel.SelectedIndex == -1 || cmbMappingHotel.SelectedItem == null)
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbMappingHotel.Tag), "Validation");
                this.cmbMappingHotel.Focus();
                return false;
            }

            return true;
        }
        private bool performValidation_PresetMapping()
        {
            if (cmbPMSType.SelectedIndex == -1 || cmbPMSType.SelectedItem == null)
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbPMSType.Tag), "Validation");
                this.cmbPMSType.Focus();
                return false;
            }
            else if (cmbMappingEntity.SelectedIndex == -1 || cmbMappingEntity.SelectedItem == null)
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbMappingEntity.Tag), "Validation");
                this.cmbMappingEntity.Focus();
                return false;
            }

            //else if ((txtPMSVersion.Text.Length < 1 || txtPMSVersion.Text == "") && chb_PMSVersion.Checked == true)
            else if (string.IsNullOrWhiteSpace(txtPMSVersion.Text) && chb_PMSVersion.Checked)
            {
                ServrMessageBox.Error(string.Format("{0} is required", txtPMSVersion.Tag), "Validation");
                this.txtPMSVersion.Focus();
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtPMSVersion.Text) && cmbpmsVersion.SelectedIndex > 0)
            {
                ServrMessageBox.Error(string.Format("{0} is required", txtPMSVersion.Tag), "Validation");
                this.txtPMSVersion.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbpmsVersion.SelectedValue) <= 0 && string.IsNullOrWhiteSpace(txtPMSVersion.Text))
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbpmsVersion.Tag), "Validation");
                this.cmbpmsVersion.Focus();
                return false;
            }
            else if (cmbStatus.SelectedIndex == -1 || cmbStatus.SelectedItem == null)
            {
                ServrMessageBox.Error(string.Format("{0} is required", cmbStatus.Tag), "Validation");
                this.cmbStatus.Focus();
                return false;
            }
            else if (txtPMSVersion.Text != "" && txtPMSVersion.Text != null && chb_PMSVersion.Checked == true && Convert.ToInt32(cmbpmsVersion.SelectedValue) <= 0)
            {
                var listVersions = cmbpmsVersion.Items.Cast<EnumViewModel>().ToList();
                //for (int i = 0; i < listVersions.Count; i++)
                //{
                //    if (listVersions[i].name == txtPMSVersion.Text)
                //    {
                //        ServrMessageBox.Error(string.Format("{0} already exist", txtPMSVersion.Text), "Validation");
                //        this.txtPMSVersion.Focus();
                //        return false;
                //    }
                //} 

                //*** OR ****////
                bool exist = listVersions.Any(x => x.name == txtPMSVersion.Text);
                if (exist)
                {
                    ServrMessageBox.Error(string.Format("{0} already exist", txtPMSVersion.Text), "Validation");
                    this.txtPMSVersion.Focus();
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region "Form Events"
        private async void btnGetPresets_Click(object sender, EventArgs e)
        {
            if (performValidation_GetPresets())
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    if (ServrMessageBox.Confirm("Are you sure, you want to update presets?", "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                    {
                        PMSVersionViewModel version = await _service.getPMSVersion(Convert.ToInt32(this.cmbpmsVersion.SelectedValue));//, string.Empty, Convert.ToInt32(this.cmbPMSType.SelectedValue), string.Empty);
                        if (version != null)
                        {
                            //hotelPreset= versions.FirstOrDefault().jsonData;
                            hotelPreset.mappings = null;
                            API.ClonedPRESETS = null;
                            hotelPreset.id = version.id;
                            hotelPreset.version = version.id.ToString();
                            hotelPreset.mappings = version.jsonData;
                            API.PRESETS.mappings = version.jsonData;
                            API.PRESETS.preferences = await _service.loadDefaultPreferences();
                            hotelPreset.preferences = API.PRESETS.preferences;
                            API.PRESETS = hotelPreset;
                            // Making cloning of it for Reset operations
                            API.ClonedPRESETS = hotelPreset;

                            //****************************** Set values on controls *********************************//
                            GlobalApp.Hotel_Code = this.cmbMappingHotel.Text;
                            GlobalApp.Hotel_id = Convert.ToInt32(this.cmbMappingHotel.SelectedValue);
                            GlobalApp.isNew = false;
                            //*************************************Fill datasource with update

                            if (hotelPreset.mappings != null)
                            {
                                mFields.Clear();
                                var qry = hotelPreset.mappings.Where(x => x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && (x.entity_Id > 0 || x.id > 0)).ToList();
                                foreach (MappingViewModel enty in qry)
                                {
                                    if (enty.status == (int)UTIL.Enums.STATUSES.Active)
                                    {
                                        foreach (FormViewModel f in enty.forms)
                                        {
                                            foreach (EntityFieldViewModel fds in f.fields)
                                            {
                                                if (mFields.Where(x => x.fuid == fds.fuid).FirstOrDefault() == null)
                                                    mFields.Add(new MappingBindingViewModel { entity_id = Convert.ToInt32(enty.id), pms_field_xpath = fds.pms_field_xpath, fieldsr = fds.sr, data_type = fds.data_type, maxLength = Convert.ToInt32(fds.maxLength), control_type = fds.control_type, control_action = fds.action_type, mode = enty.mode, entity_status = enty.status, entity_type = enty.entity_type, entity_name = enty.entity_name, table_name = "" + fds.table_name, formid = f.id, pmspagename = f.pmspagename, pmspageid = f.pmspageid, fuid = fds.fuid, pms_field_name = fds.pms_field_name, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, field_desc = fds.field_desc, default_value = fds.default_value, parent_field_id = fds.parent_field_id, fieldformat = fds.format, schema_field_name = fds.schema_field_name, pms_field_expression = fds.pms_field_expression, fieldmanadatory = fds.mandatory, fieldstatus = fds.status });
                                            }
                                        }

                                    }//if (mv.status == (int)UTIL.Enums.STATUSES.Active) { 
                                }

                                this.fillDataGrid();
                                ServrMessageBox.ShowBox(string.Format("Mapping of '{0}' loaded successfully, Need to run 'save & complete'.", this.txtPMSVersion.textbox.Text), "Success");
                            }
                        }//Version  
                        else
                        {
                            ServrMessageBox.Error("PMS version  retrieval failed, contact administrator.", "Error");
                        }////Version   Else
                    }
                }
                catch (Exception ex)
                {
                    _log.Error(ex);
                    ServrMessageBox.Error(string.Format("Failed,error {0} ", ex.Message), "Error");
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void btn_PresetMapping_Click(object sender, EventArgs e)
        {
            try
            {
                // var rootFolder = Directory.GetDirectoryRoot("BDM Core");
                //string  pathRoot = Path.GetPathRoot(@"\Drivers\");
                string pathRoot = Path.GetFullPath(@"Drivers");
                // var rootFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                String command = Path.Combine(pathRoot, @"WinAppDriverUiRecorder.exe");//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                                                                                       //String command = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["UIAUTOmationAppPath"] );//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                                                                                       //  String command = @"D:\Shared\BDUV3.0\RobotAutoApp\Driver\WinAppDriver.exe";
                if (System.IO.File.Exists(command))
                {
                    Process.Start(new ProcessStartInfo(command) { CreateNoWindow = true });
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void grvMappingData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == grvMappingData.Columns["colAdvance"].Index)
            {
                DataGridViewRow dRow = grvMappingData.CurrentRow;
                if (Convert.ToInt16(dRow.Cells["colAdvanceMapping"].Value) == 0)
                {
                    try
                    {
                        BDUInegationNewForm frm = new BDUInegationNewForm();
                        MappingBindingViewModel model = mFields.Where(x => x.fuid == "" + dRow.Cells["Id"].Value && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue))).FirstOrDefault();
                        frm.fuid = model.fuid;
                        frm.EntityId = Convert.ToInt32(this.cmbMappingEntity.SelectedValue);
                        frm.entity_type_id = frm.EntityId == (int)UTIL.Enums.ENTITIES.ACCESS ? (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT : (int)UTIL.Enums.ENTITY_TYPES.SYNC;
                        frm.entityName = "" + this.cmbMappingEntity.Text;
                        frm.isExisting = true;
                        frm.m_fields = mFields;
                        frm.ShowDialog();
                        this.fillDataGrid();
                    }
                    catch (Exception ex)
                    {
                        Servr.UI.ServrMessageBox.Error(ex.StackTrace, "Error");
                    }
                }
            }// Advance Mapping
            if (e.ColumnIndex == grvMappingData.Columns["colDelete"].Index)
            {
                DataGridViewRow cRow = grvMappingData.CurrentRow;
                if (Convert.ToInt16(cRow.Cells["col_unmapped"].Value) == 1)
                {
                    if (Servr.UI.ServrMessageBox.Confirm(string.Format("Are you sure, want to delete {0} ?", Convert.ToString(cRow.Cells["colCMSFieldName"].Value)), "Confirm") == Enums.MESSAGERESPONSETYPES.CONFIRM)
                    {
                        try
                        {
                            var dEntity = mFields.Where(x => x.fuid == "" + cRow.Cells["Id"].Value && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue)) && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS).ToList();
                            if (dEntity != null)
                            {
                                MappingBindingViewModel model = dEntity.FirstOrDefault();
                                //********************* Mark it deleted ************************************//
                                model.fieldstatus = (int)UTIL.Enums.STATUSES.Deleted;
                                mFields.Remove(model);
                                //********************* Reflect in grid ************************************//
                                this.fillDataGrid();

                            }
                        }
                        catch (Exception ex)
                        {
                            Servr.UI.ServrMessageBox.Error(ex.StackTrace, "Error");
                        }
                    }
                }// Delete only unmapped fields
            }// Advance Mappinga
        }
        private async void btn_UpDown_Click(object sender, EventArgs e)
        {

            if (this.grvMappingData.SelectedRows.Count > 0)
            {
                int CurrentRowIndex = this.grvMappingData.SelectedRows[0].Index;

                if (CurrentRowIndex > 0 && !this.grvMappingData.IsCurrentRowDirty)
                {
                    DataGridViewRow selectedR = this.grvMappingData.SelectedRows[0];
                    DataGridViewRow nextR = this.grvMappingData.Rows[CurrentRowIndex - 1];
                    candidateRowIndex = CurrentRowIndex - 1;

                    var selectedField = mFields.Where(x => x.fuid == "" + selectedR.Cells["Id"].Value && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue))).ToList();
                    if (selectedField != null)
                    {
                        MappingBindingViewModel model = selectedField.FirstOrDefault();
                        if (Convert.ToString(selectedR.Cells["Id"].Value) == model.fuid)
                        {
                            model.fieldsr = model.fieldsr - 1;
                            //   model.sr = model.sr + 1;
                        }
                    }//if (selectedField !=null)
                    var NextField = mFields.Where(x => x.fuid == "" + nextR.Cells["Id"].Value && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue))).ToList();
                    if (NextField != null)
                    {
                        MappingBindingViewModel model = NextField.FirstOrDefault();
                        if (Convert.ToString(nextR.Cells["Id"].Value) == model.fuid)
                        {
                            model.fieldsr = model.fieldsr + 1;
                            //   model.sr = model.sr - 1;
                        }
                    }//(NextField != null)

                    this.fillDataGrid();

                    grvMappingData.Rows[candidateRowIndex].Selected = true;
                }//if (nRow !=null) {                 

            }
            else
            {
                Servr.UI.ServrMessageBox.ShowBox("Row must be selected", "Warning..!");
            }


        }
        private async void btn_DownRow_Click(object sender, EventArgs e)
        {
            if (this.grvMappingData.SelectedRows.Count > 0 && this.grvMappingData.SelectedRows[0].Index != this.grvMappingData.Rows.Count - 1)
            {
                int CurrentRowIndex = this.grvMappingData.SelectedRows[0].Index;

                if (!this.grvMappingData.IsCurrentRowDirty)
                {

                    // DataGridViewRow selectedR = this.grvMappingData.Rows[CurrentRowIndex];
                    DataGridViewRow selectedR = this.grvMappingData.SelectedRows[0];
                    DataGridViewRow nextR = this.grvMappingData.Rows[CurrentRowIndex + 1];
                    candidateRowIndex = CurrentRowIndex + 1;
                    //  Servr.UI.ServrMessageBox.ShowBox(string.Format("current row {0}", CurrentRowIndex.ToString()), "Warning..!");

                    // var NextField = mFields.Where(x => x.fuid == ""+ nextR.Cells["Id"].Value && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue))).ToList();
                    MappingBindingViewModel modelN = mFields.Where(x => x.fuid == "" + nextR.Cells["Id"].Value && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS).FirstOrDefault();
                    if (modelN != null)
                    {
                        if ("" + nextR.Cells["Id"].Value == modelN.fuid)
                        {
                            modelN.fieldsr = modelN.fieldsr - 1;
                        }
                    }//(NextField != null)
                     //var selectedField = mFields.Where(x => x.fuid == ""+selectedR.Cells["Id"].Value && x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) == 0 ? x.entity_id : Convert.ToInt32(this.cmbMappingEntity.SelectedValue))).ToList();
                    MappingBindingViewModel modelSelected = mFields.Where(x => x.fuid == "" + selectedR.Cells["Id"].Value && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS).FirstOrDefault();
                    if (modelSelected != null)
                    {
                        if ("" + selectedR.Cells["Id"].Value == modelSelected.fuid)
                        {
                            modelSelected.fieldsr = modelSelected.fieldsr + 1;
                            //   model.sr = model.sr + 1;
                        }
                    }//if (selectedField !=null)


                    this.fillDataGrid();
                    // this.grvMappingData.ClearSelection();
                    //Servr.UI.ServrMessageBox.ShowBox(string.Format("selected row {0}", (CurrentRowIndex + 4).ToString()), "Warning..!");
                    this.grvMappingData.Rows[candidateRowIndex].Selected = true;
                }//if (nRow !=null) {                 

            }
            else
            {
                Servr.UI.ServrMessageBox.ShowBox("Row must be selected", "Warning..!");
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UTIL.Enums.MESSAGERESPONSETYPES res = Servr.UI.ServrMessageBox.Confirm("Are you sure, want to discard changes?", "Confirmation!");

            if (res == UTIL.Enums.MESSAGERESPONSETYPES.CONFIRM)
            {
                Refresh();
                int selectR = 0;
                if (this.grvMappingData.SelectedRows[0] != null)
                {
                    selectR = this.grvMappingData.SelectedRows[0].Index;
                }

                hotelPreset = API.ClonedPRESETS == null ? API.PRESETS : API.ClonedPRESETS;
                //****************************Restore the Previous Presets***************************//
                if (hotelPreset != null)
                {

                    this.cmbPMSType.SelectedValue = Convert.ToInt32(API.PRESETS.system_type);
                    // this.txtPMSVersion.Text = Convert.ToString(API.PRESETS.version);
                    this.cmbpmsVersion.SelectedValue = Convert.ToInt32(API.PRESETS.version);
                    // hotelPreset = API.PRESETS;
                    mFields.Clear();

                    var qry = hotelPreset.mappings.Where(x => x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.status == (int)UTIL.Enums.STATUSES.Active).ToList();
                    //mFields = (from enty in qry
                    //           from frms in enty.forms
                    //           from fds in frms.fields
                    //           where fds.status == (int)UTIL.Enums.STATUSES.Active && !string.IsNullOrWhiteSpace(fds.fuid)
                    //           select new MappingBindingViewModel { entity_id = Convert.ToInt32(enty.id), fuid = fds.fuid, mode = enty.mode, entity_status = enty.status, entity_type = enty.entity_type, entity_name = enty.entity_name, table_name = "" + fds.table_name, formid = frms.id, pmspagename = frms.pmspagename, pmspageid = frms.pmspageid, fieldId = fds.id, fieldsr = fds.sr, pms_field_name = fds.pms_field_name, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, field_desc = fds.field_desc, default_value = fds.default_value, pms_field_xpath = fds.pms_field_xpath, fieldformat = fds.format, schema_field_name = fds.schema_field_name, pms_field_expression = fds.pms_field_expression, fieldmanadatory = fds.mandatory, fieldstatus = fds.status }).ToList();
                    foreach (MappingViewModel enty in qry)
                    {
                        if (enty.status == (int)UTIL.Enums.STATUSES.Active)
                        {
                            foreach (FormViewModel f in enty.forms)
                            {
                                foreach (EntityFieldViewModel fds in f.fields)
                                {
                                    if (mFields.Where(x => x.fuid == fds.fuid).FirstOrDefault() == null)
                                        mFields.Add(new MappingBindingViewModel { entity_id = Convert.ToInt32(enty.id), pms_field_xpath = fds.pms_field_xpath, fieldsr = fds.sr, data_type = fds.data_type, maxLength = Convert.ToInt32(fds.maxLength), control_type = fds.control_type, control_action = fds.action_type, mode = enty.mode, entity_status = enty.status, entity_type = enty.entity_type, entity_name = enty.entity_name, table_name = "" + fds.table_name, formid = f.id, pmspagename = f.pmspagename, pmspageid = f.pmspageid, fuid = fds.fuid, pms_field_name = fds.pms_field_name, is_unmapped = fds.is_unmapped, is_reference = fds.is_reference, field_desc = fds.field_desc, default_value = fds.default_value, parent_field_id = fds.parent_field_id, fieldformat = fds.format, schema_field_name = fds.schema_field_name, pms_field_expression = fds.pms_field_expression, fieldmanadatory = fds.mandatory, fieldstatus = fds.status });
                                }
                            }

                        }//if (mv.status == (int)UTIL.Enums.STATUSES.Active) { 
                    }

                    //****************************Fill Grid to Reflect Changes ***************************//
                    this.fillDataGrid();
                    //****************************Row Selection Persistance ***************************//
                    if (selectR > 0) this.grvMappingData.Rows[selectR].Selected = true;

                }
            }
        }

        private void grvMappingData_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    this.btn_UpDown_Click(null, null);
                    break;
                case Keys.Down:
                    this.btn_DownRow_Click(null, null);
                    break;
            }
        }

        #endregion

        private void btnTestRun_Click(object sender, EventArgs e)
        {
            if (this.performValidation_PresetMapping())
            {
                ResponseViewModel res = null;
                // Prepare  Entity for Test Run
                foreach (DataGridViewRow row in grvMappingData.Rows)
                {
                    string fuid = Convert.ToString(row.Cells["Id"].Value);
                    Int32 enty = Convert.ToInt32(row.Cells["entity"].Value);
                    if (!string.IsNullOrWhiteSpace(fuid))
                    {
                        MappingBindingViewModel savedata = mFields.Where(x => x.fuid == fuid && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == enty).FirstOrDefault();
                        if (savedata != null)
                        {
                            savedata.pms_field_name = Convert.ToString(row.Cells["colPmsFieldName"].Value);
                            //savedata.pmspageid = Convert.ToString(row.Cells["colIdentifier"].Value);
                            // savedata.fieldname = Convert.ToString(row.Cells["colIdentifier"].Value);
                            savedata.fieldmanadatory = Convert.ToString(row.Cells["colmandatory"].Value) == "Yes" ? 1 : 0;
                            //  savedata.expression = Convert.ToString(row.Cells["colIdentifier"].Value);
                            savedata.default_value = Convert.ToString(row.Cells["colDefaultValue"].Value);
                            savedata.entity_status = Convert.ToInt32(this.cmbStatus.SelectedValue);
                            savedata.entity_name = Convert.ToString(this.cmbMappingEntity.Text);
                        }
                    }
                }// foreach (DataGridViewRow row in grvMappingData.Rows)
                // Mapping Entity
                List<MappingViewModel> TestRunEntity = new List<MappingViewModel>();
                var entities = (from ents in mFields
                                where ents.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && ents.entity_id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue)
                                orderby ents.entity_id
                                select new { entity_id = ents.entity_id, ents.entity_status, entity_name = ents.entity_name, entity_type = ents.entity_type }
                                      ).Distinct();

                //******************************  Entity Type UTIL.Enums.ENTITIES.CHECKIN *******************************//
                foreach (var ets in entities)
                {

                    var frms = (from mps in mFields
                                where mps.fieldstatus == (int)UTIL.Enums.STATUSES.Active && mps.entity_id == ets.entity_id
                                orderby mps.fieldsr
                                select new { pmspageid = string.IsNullOrWhiteSpace(mps.pmspageid) ? "form1" : mps.pmspageid }
                                          ).Distinct();
                    int form = 1;
                    List<FormViewModel> formsls = new List<FormViewModel>();
                    foreach (var f in frms)
                    {

                        FormViewModel frm = new FormViewModel { id = form++, pmspageid = string.IsNullOrWhiteSpace(f.pmspageid) ? "form1" : f.pmspageid, pmspagename = string.IsNullOrWhiteSpace(f.pmspageid) ? "form1" : f.pmspageid };

                        var fields = (from flds in mFields
                                      where flds.fieldstatus == (int)UTIL.Enums.STATUSES.Active && ((string.IsNullOrWhiteSpace(flds.pmspageid) ? "form1" : flds.pmspageid) == f.pmspageid) && flds.entity_id == ets.entity_id
                                      orderby flds.pmspageid
                                      select flds).Distinct();
                        List<EntityFieldViewModel> fieldsls = new List<EntityFieldViewModel>();
                        //  
                        foreach (MappingBindingViewModel fds in fields)
                        {
                            if (fieldsls.Where(x => x.fuid == fds.fuid).FirstOrDefault() == null)
                                fieldsls.Add(new EntityFieldViewModel { fuid = fds.fuid, entity_id = ets.entity_id, feed = fds.feed, scan = fds.scan, is_reference = fds.is_reference, is_unmapped = fds.is_unmapped, control_type = fds.control_type, field_desc = fds.field_desc, sr = fds.fieldsr, status = fds.fieldstatus, pms_field_expression = fds.pms_field_expression, maxLength = fds.maxLength, default_value = fds.default_value, parent_field_id = fds.parent_field_id, action_type = fds.control_action, data_type = fds.data_type, mandatory = fds.fieldmanadatory, format = fds.fieldformat, schema_field_name = fds.schema_field_name, pms_field_xpath = fds.pms_field_xpath, pms_field_name = fds.pms_field_name });
                        }
                        frm.fields = fieldsls;
                        formsls.Add(frm);
                    }// foreach (DataGridViewRow row in grvMappingData.Rows)
                    if (formsls != null && formsls.Count() > 0)
                        TestRunEntity.Add(new MappingViewModel { id = ets.entity_id, entity_Id = ets.entity_id, entity_type = ets.entity_type, mode = 1, status = ets.entity_status, entity_name = ets.entity_name, forms = formsls, data = null });
                    ////******************************  Entity Type UTIL.Enums.ENTITIES.Billing Details *******************************//

                }
                // End Entty Resr Run
                if ((_desktopHandler != null || _WebHandler != null) && TestRunEntity != null && TestRunEntity.Count() > 0)
                {
                    if (_desktopHandler != null && GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                    {
                        //List<EntityFieldViewModel> flds = (from f in mFields
                        //                                   where f.entity_status == (int)UTIL.Enums.STATUSES.Active && f.entity_id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue) && f.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS
                        //                                   select new EntityFieldViewModel { parent_field_id = string.Empty, id = f.fieldId, pms_field_name = f.pms_field_name, field_desc = f.field_desc, mandatory = f.fieldmanadatory, pms_field_expression = f.pms_field_expression, pms_field_xpath = f.pms_field_xpath, status = f.fieldstatus, control_type = f.control_type, data_type = f.data_type, is_unmapped = f.is_unmapped, is_reference = f.is_reference, sr = f.fieldsr }).Distinct().ToList();

                        //  _desktopHandler._formData = TestRunEntity[0];// hotelPreset.mappings.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue)).Distinct().FirstOrDefault();
                        Cursor.Current = Cursors.WaitCursor;
                        res = _desktopHandler.TestRun(TestRunEntity[0].DCopy());
                        Cursor.Current = Cursors.Default;
                        if (res.status && res.status_code == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                        {
                            ServrMessageBox.ShowBox("Test run success", "Success");
                            this.btnSave_Complete.Enabled = true;
                        }
                        else if (!res.status)
                        {
                            ServrMessageBox.Error(res.message, "Failed");
                            this.btnSave_Complete.Enabled = false;
                            // this.btnSave_Complete.Enabled = false;
                        }
                        else if (res.status && res.status_code == ((int)UTIL.Enums.ERROR_CODE.WARNING).ToString())
                        {
                            ServrMessageBox.ShowBox(res.message, "Warning");
                            this.btnSave_Complete.Enabled = true;
                        }
                    }
                    else if (_WebHandler != null && GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString())
                    {
                        if (TestRunEntity != null && TestRunEntity.Count() > 0)
                        {
                            List<MappingViewModel> mappings = TestRunEntity;// hotelPreset.mappings.Where(x => x.status == (int)UTIL.Enums.STATUSES.Active && x.id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue)).ToList();
                            if (mappings != null && mappings.Count > 0)
                            {
                                Cursor.Current = Cursors.WaitCursor;
                                res = _WebHandler.TestRun(mappings, true);
                                Cursor.Current = Cursors.Default;
                            }
                            if (res.status && res.status_code == ((int)UTIL.Enums.ERROR_CODE.SUCCESS).ToString())
                            {
                                ServrMessageBox.ShowBox("Test run success", "Success");
                                this.btnSave_Complete.Enabled = true;
                            }
                            else if (!res.status)
                            {
                                ServrMessageBox.Error(res.message, "Failed");
                                this.btnSave_Complete.Enabled = false;
                                // this.btnSave_Complete.Enabled = false;
                            }
                            else if (res.status && res.status_code == ((int)UTIL.Enums.ERROR_CODE.WARNING).ToString())
                            {
                                ServrMessageBox.ShowBox(res.message, "Warning");
                                this.btnSave_Complete.Enabled = true;
                            }
                        }
                    }//else if (_WebHandler != null && GlobalApp.SYSTEM_TYPE == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString())
                }
                else
                {
                    ServrMessageBox.Error("PMS is not started", "Failed");
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // if (this.performValidation_PresetMapping())
                // {
                // Its save locally
                foreach (DataGridViewRow row in grvMappingData.Rows)
                {
                    string fuid = Convert.ToString(row.Cells["Id"].Value);
                    Int32 enty = Convert.ToInt32(row.Cells["entity"].Value);
                    if (string.IsNullOrWhiteSpace(fuid))
                    {
                        MappingBindingViewModel savedata = mFields.Where(x => x.fuid == fuid && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == enty).FirstOrDefault();
                        if (savedata != null)
                        {
                            savedata.pms_field_name = Convert.ToString(row.Cells["colPmsFieldName"].Value);
                            //savedata.pmspageid = Convert.ToString(row.Cells["colIdentifier"].Value);
                            // savedata.fieldname = Convert.ToString(row.Cells["colIdentifier"].Value);
                            //  savedata. = Convert.ToString(row.Cells["colpms_field_xpath"].Value);
                            savedata.fieldmanadatory = Convert.ToString(row.Cells["colmandatory"].Value) == "Yes" ? 1 : 0;
                            //savedata.pms_field_xpath = Convert.ToString(row.Cells["colpms_field_xpath"].Value);
                            //  savedata.expression = Convert.ToString(row.Cells["colIdentifier"].Value);
                            savedata.default_value = Convert.ToString(row.Cells["colDefaultValue"].Value);
                            savedata.entity_status = Convert.ToInt32(this.cmbStatus.SelectedValue);
                            savedata.entity_name = Convert.ToString(this.cmbMappingEntity.Text);
                        }
                    }
                }// foreach (DataGridViewRow row in grvMappingData.Rows)
                ServrMessageBox.ShowBox("Presets saved locally", "Success");
                // }//if (this.performValidation_PresetMapping())
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ServrMessageBox.Error(string.Format("Failed, error {0} ", ex.Message), "Failed");
            }
        }
        private void grvMappingData_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            //Offsets to adjust the position of the merged Header.
            int heightOffset = 29;
            int widthOffset = -2;
            int xOffset = 0;
            int yOffset = -29;

            //Index of Header column from where the merging will start.
            int columnIndex = 8;

            //Number of Header columns to be merged.
            int columnCount = 3;
            try
            {
                //Get the position of the Header Cell.
                Rectangle headerCellRectangle = grvMappingData.GetCellDisplayRectangle(columnIndex, 0, true);

                //X coordinate  of the merged Header Column.
                int xCord = headerCellRectangle.Location.X + xOffset;

                //Y coordinate  of the merged Header Column.
                int yCord = headerCellRectangle.Location.Y - headerCellRectangle.Height + yOffset;

                //Calculate Width of merged Header Column by adding the widths of all Columns to be merged.
                int mergedHeaderWidth = grvMappingData.Columns[columnIndex].Width + grvMappingData.Columns[columnIndex + columnCount - 1].Width + widthOffset;

                //Generate the merged Header Column Rectangle.
                Rectangle mergedHeaderRect = new Rectangle(xCord, yCord, mergedHeaderWidth, headerCellRectangle.Height + heightOffset);

                //Draw the merged Header Column Rectangle.
                e.Graphics.FillRectangle(new SolidBrush(Color.White), mergedHeaderRect);

                //Draw the merged Header Column Text.
                e.Graphics.DrawString("", grvMappingData.ColumnHeadersDefaultCellStyle.Font, Brushes.Black, xCord + 6, yCord + 12);
            }
            catch (Exception ex)
            {

            }
        }

        private void AllControl_DrawItem(object sender, DrawItemEventArgs e)
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
        private async void btnSave_Complete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.performValidation_PresetMapping())
                {
                    // step1: Its save locally
                    foreach (DataGridViewRow row in grvMappingData.Rows)
                    {
                        string fuid = Convert.ToString(row.Cells["Id"].Value);
                        Int32 enty = Convert.ToInt32(row.Cells["entity"].Value);
                        if (!string.IsNullOrWhiteSpace(fuid))
                        {
                            MappingBindingViewModel savedata = mFields.Where(x => x.fuid == fuid && x.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS && x.entity_id == enty).FirstOrDefault();
                            if (savedata != null)
                            {
                                savedata.pms_field_name = Convert.ToString(row.Cells["colPmsFieldName"].Value);
                                //savedata.pmspageid = Convert.ToString(row.Cells["colIdentifier"].Value);
                                // savedata.fieldname = Convert.ToString(row.Cells["colIdentifier"].Value);
                                // savedata.pms_field_xpath = Convert.ToString(row.Cells["colpms_field_xpath"].Value);
                                savedata.fieldmanadatory = Convert.ToString(row.Cells["colmandatory"].Value) == "Yes" ? 1 : 0;
                                //  savedata.expression = Convert.ToString(row.Cells["colIdentifier"].Value);
                                savedata.default_value = Convert.ToString(row.Cells["colDefaultValue"].Value);
                                savedata.entity_status = Convert.ToInt32(this.cmbStatus.SelectedValue);
                                savedata.entity_name = Convert.ToString(this.cmbMappingEntity.Text);
                            }
                        }
                    }// foreach (DataGridViewRow row in grvMappingData.Rows)
                    List<MappingViewModel> EntityMappings = new List<MappingViewModel>();
                    var entities = (from ents in mFields
                                    where ents.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS
                                    orderby ents.entity_id
                                    select new { entity_id = ents.entity_id, ents.entity_status, entity_name = ents.entity_name, entity_type = ents.entity_type }
                                          ).Distinct();

                    //******************************  Entity Type UTIL.Enums.ENTITIES.CHECKIN *******************************//
                    foreach (var ets in entities)
                    {

                        var frms = (from mps in mFields
                                    where mps.fieldstatus == (int)UTIL.Enums.STATUSES.Active && mps.entity_id == ets.entity_id
                                    orderby mps.fieldsr
                                    select new { pmspageid = string.IsNullOrWhiteSpace(mps.pmspageid) ? "form1" : mps.pmspageid }
                                              ).Distinct();
                        int form = 1;
                        List<FormViewModel> formsls = new List<FormViewModel>();
                        foreach (var f in frms)
                        {

                            FormViewModel frm = new FormViewModel { id = form++, pmspageid = string.IsNullOrWhiteSpace(f.pmspageid) ? "form1" : f.pmspageid, pmspagename = string.IsNullOrWhiteSpace(f.pmspageid) ? "form1" : f.pmspageid };

                            var fields = (from flds in mFields
                                          where flds.fieldstatus == (int)UTIL.Enums.STATUSES.Active && ((string.IsNullOrWhiteSpace(flds.pmspageid) ? "form1" : flds.pmspageid) == f.pmspageid) && flds.entity_id == ets.entity_id
                                          orderby flds.pmspageid
                                          select flds).Distinct();
                            List<EntityFieldViewModel> fieldsls = new List<EntityFieldViewModel>();
                            //  
                            foreach (MappingBindingViewModel fds in fields)
                            {
                                if (fieldsls.Where(x => x.fuid == fds.fuid).FirstOrDefault() == null)
                                    fieldsls.Add(new EntityFieldViewModel { fuid = fds.fuid, feed = fds.feed, scan = fds.scan, entity_id = ets.entity_id, is_reference = fds.is_reference, is_unmapped = fds.is_unmapped, control_type = fds.control_type, field_desc = fds.field_desc, sr = fds.fieldsr, status = fds.fieldstatus, pms_field_expression = fds.pms_field_expression, maxLength = fds.maxLength, default_value = fds.default_value, parent_field_id = fds.parent_field_id, action_type = fds.control_action, data_type = fds.data_type, mandatory = fds.fieldmanadatory, format = fds.fieldformat, schema_field_name = fds.schema_field_name, pms_field_xpath = fds.pms_field_xpath, pms_field_name = fds.pms_field_name });
                            }
                            frm.fields = fieldsls;
                            formsls.Add(frm);
                        }// foreach (DataGridViewRow row in grvMappingData.Rows)
                        if (EntityMappings.Where(x => x.entity_Id == ets.entity_id).FirstOrDefault() == null)
                            EntityMappings.Add(new MappingViewModel { id = ets.entity_id, entity_Id = ets.entity_id, entity_type = ets.entity_type, mode = 1, status = 1, entity_name = ets.entity_name, forms = formsls, data = null });
                        // EntityMappings.Add(new MappingViewModel { id = ets.entity_id, entity_Id = ets.entity_id, entity_type = ets.entity_type, mode = 1, status = ets.entity_status, entity_name = ets.entity_name, forms = formsls, data = null });
                        ////******************************  Entity Type UTIL.Enums.ENTITIES.Billing Details *******************************//

                    }
                    List<MappingViewModel> oldMappings = hotelPreset.mappings.Where(x => x.entity_type == (int)UTIL.Enums.ENTITY_TYPES.STATS).ToList();
                    if (oldMappings != null)
                    {
                        foreach (var m in oldMappings)
                        {
                            if (m.entity_Id <= 0)
                                m.entity_Id = Convert.ToInt32(m.id);
                        }

                        EntityMappings.AddRange(oldMappings);
                        hotelPreset.mappings = null;
                        hotelPreset.mappings = EntityMappings;
                        hotelPreset.id = Convert.ToInt32(this.cmbpmsVersion.SelectedValue);
                        hotelPreset.system_type = Convert.ToString(this.cmbPMSType.SelectedValue);
                        hotelPreset.version = Convert.ToInt32(this.cmbpmsVersion.SelectedValue).ToString();// this.txtPMSVersion.Text;
                    }
                    // ++++++ Step2: run test ++++++++++++++++
                    if (this.TestRunHandler(hotelPreset.system_type))
                    {
                        // ---------------------------------------
                        string pmsVersion = Convert.ToInt32(this.cmbpmsVersion.SelectedValue).ToString();
                        //******************************Step3: Set saved globally save as well *********************************//
                        if (hotelPreset != null && hotelPreset.mappings != null)
                        {
                            if (this.chb_PMSVersion.Checked)
                            {
                                ResponseViewModel resPMSVersion = await _service.savePMSVersion(hotelPreset.mappings, Convert.ToInt32(cmbpmsVersion.SelectedValue), Convert.ToInt32(cmbStatus.SelectedValue), this.txtPMSVersion.Text);
                                if (resPMSVersion.status)
                                    pmsVersion = resPMSVersion.message;

                            }


                            ResponseViewModel res = await _service.saveHotelPresets(Convert.ToInt32(this.cmbMappingHotel.SelectedValue), this.cmbMappingHotel.Text, hotelPreset, pmsVersion);

                            if (res.status)
                            {
                                API.PRESETS = hotelPreset;
                                API.ClonedPRESETS = hotelPreset;
                                GlobalApp.isNew = false;
                                ServrMessageBox.ShowBox("Presets saved successfully", "Success");
                                Application.Restart();

                            }
                            else
                                ServrMessageBox.Error("Preset save failed, try later.", "Failed");
                        }
                    }
                }
                //else
                //    ServrMessageBox.Error("Preset not available for save, try later.", "Failed");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ServrMessageBox.Error(string.Format("Failed, error {0} ", ex.Message), "Failed");
            }
        }

        private bool TestRunHandler(string sType)
        {
            return true;
            ResponseViewModel resViewModel = null;
            bool testRunResult = false;
            string system_types = GlobalApp.SYSTEM_TYPE;
            if (!string.IsNullOrWhiteSpace(sType))
                system_types = sType;
            if (_desktopHandler != null || _WebHandler != null)
            {
                if (_desktopHandler != null && system_types == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Desktop).ToString())
                {
                    _desktopHandler.TestRun(hotelPreset.mappings[0]);
                    testRunResult = true;
                }
                else if (_WebHandler != null && system_types == ((int)UTIL.Enums.PMS_VERSIONS.PMS_Web).ToString())
                {

                    //List<EntityFieldViewModel> flds = (from f in mFields
                    //                                   where f.entity_status == (int)UTIL.Enums.STATUSES.Active && f.entity_id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue) && f.entity_type != (int)UTIL.Enums.ENTITY_TYPES.STATS
                    //                                   select new EntityFieldViewModel { parent_field_id = f.parent_field_id, fuid = f.fuid, pms_field_name = f.pms_field_name, field_desc = f.field_desc, mandatory = f.fieldmanadatory, pms_field_expression = f.pms_field_expression, pms_field_xpath = f.pms_field_xpath, status = f.fieldstatus, control_type = f.control_type, data_type = f.data_type, is_unmapped = f.is_unmapped, is_reference = f.is_reference, sr = f.fieldsr }).ToList();

                    if (hotelPreset.mappings != null && hotelPreset.mappings.Count > 0)
                        resViewModel = _WebHandler.TestRun(hotelPreset.mappings);
                    if (resViewModel.status)
                    {
                        ServrMessageBox.ShowBox("Test run success", "Success");
                        this.btnSave_Complete.Enabled = true;
                        testRunResult = true;
                    }
                    else
                    {
                        ServrMessageBox.Error(resViewModel.message, "Failed");
                        testRunResult = false;
                        this.btnSave_Complete.Enabled = false;
                    }

                }
            }
            else
            {
                ServrMessageBox.Error("PMS is not started", "Failed");
                testRunResult = false;
            }
            return testRunResult;
        }
        private void btnAddField_Click(object sender, EventArgs e)
        {
            BDUInegationNewForm frm = new BDUInegationNewForm();
            if (mFields != null)
            {
                ///frm.serialNo = mFields.Where(y => y.entity_id == Convert.ToInt32(this.cmbMappingEntity.SelectedValue)).Max(x => x.fieldsr) + 1;
                frm.m_fields = mFields;
                frm.EntityId = Convert.ToInt32(this.cmbMappingEntity.SelectedValue);
                frm.entity_type_id = frm.EntityId == (int)UTIL.Enums.ENTITIES.ACCESS ? (int)UTIL.Enums.ENTITY_TYPES.ACCESS_MNGT : (int)UTIL.Enums.ENTITY_TYPES.SYNC;
                frm.entityName = "" + this.cmbMappingEntity.Text;
            }

            frm.ShowDialog();
            this.fillDataGrid();
        }

        private void cmbpmsVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbpmsVersion.SelectedIndex > 0)
            {

                this.btnGetPresets.Enabled = true;
                this.txtPMSVersion.Text = this.cmbpmsVersion.Text;
            }
            else
            {
                this.btnGetPresets.Enabled = false;
                if (string.IsNullOrWhiteSpace(this.txtPMSVersion.Text))
                    this.txtPMSVersion.Text = string.Empty;
                this.txtPMSVersion.Text = "";
            }
        }

        private void grvMappingData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grvMappingData.Columns[e.ColumnIndex].Name.Equals("colDelete") && grvMappingData.SelectedRows.Count > 0)
            {

                if (grvMappingData.SelectedRows[0].Index == e.RowIndex)
                {
                    e.Value = BDU.UI.Properties.Resources.delete_whihte;
                }
                else
                {
                    e.Value = BDU.UI.Properties.Resources.delete_gray;
                }
            }
            if (grvMappingData.Columns[e.ColumnIndex].Name.Equals("colAdvance") && grvMappingData.SelectedRows.Count > 0)
            {

                if (grvMappingData.SelectedRows[0].Index == e.RowIndex)
                {
                    e.Value = BDU.UI.Properties.Resources.edit_white;
                }
                else
                {
                    e.Value = BDU.UI.Properties.Resources.edit_gray;
                }
            }
        }

        private void cmbMappingEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(this.cmbMappingEntity.SelectedValue) > -1)
            {
                this.fillDataGrid();
            }
        }

        private void cmbMappingHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbMappingEntity.SelectedItem != null && !this.isLoadingMode)
                this.fillDataGrid();
        }
    }
}
