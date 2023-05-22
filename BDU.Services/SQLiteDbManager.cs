using System;
using System.Collections.Generic;
using System.Text;
using BDU.UTIL;
using System.Linq;
using System.Data.SQLite;
using System.Reflection;
using System.Globalization;
using BDU.ViewModels;
using System.Data;

namespace BDU.Services
{
    public static class SQLiteDbManager
    {
        //Data Retrieval
        public static SQLiteConnection sqliteConn;


        public static SQLiteConnection GetSQLiteConnection()
        {
            if (sqliteConn == null)
            {
                sqliteConn = new SQLiteConnection(BDU.UTIL.BDUConstants.INTEGRATEX_SQLITE_CONNECTION_STRING);
                try
                {
                    sqliteConn.Open();

                    SQLiteCommand sqlite_cmd;
                    try
                    {
                        sqlite_cmd = sqliteConn.CreateCommand();
                        //sqlite_cmd.CommandText = "drop table reservations";
                        //sqlite_cmd.ExecuteNonQuery();
                        //sqlite_cmd.CommandText = "drop table fields";
                        //sqlite_cmd.ExecuteNonQuery();
                        //sqlite_cmd = sqliteConn.CreateCommand();
                        //sqlite_cmd.CommandText = "drop table fields";
                        //sqlite_cmd.ExecuteNonQuery();
                        sqlite_cmd.CommandText = "PRAGMA read_uncommitted = 1;";
                        sqlite_cmd.ExecuteNonQuery();
                        string Createsql = "CREATE TABLE IF NOT EXISTS reservations (id INTEGER PRIMARY KEY AUTOINCREMENT,  reference VARCHAR(20),mode int NOT NULL, arrivaldate datetime,  departuredate datetime, receivetime datetime, firstname VARCHAR(50),lastname VARCHAR(50),paymentamount decimal,transactiondate datetime,  voucherno VARCHAR(50),roomno VARCHAR(20),syncstatus int DEFAULT 1 NOT NULL ,email VARCHAR(50),entity_id SMALLINT,entity_name VARCHAR(50),entity_type SMALLINT,undo SMALLINT DEFAULT 0 NOT NULL,payableamount decimal)";
                        // string Createsql1 = "CREATE TABLE SampleTable1 (Col1 VARCHAR(20), Col2 INT)";

                        sqlite_cmd.CommandText = Createsql;
                        sqlite_cmd.ExecuteNonQuery();

                        string CreateFieldSQL = "CREATE TABLE IF NOT EXISTS fields (id INTEGER PRIMARY KEY AUTOINCREMENT,idref INTEGER, sr INTEGER,  fuid VARCHAR(50),field_desc VARCHAR(100),parent_field_id VARCHAR(100),pms_field_name VARCHAR(50),pms_field_xpath VARCHAR(100),pms_field_expression VARCHAR(500),value VARCHAR(200),automation_mode INTEGER default 0,ocrFactor decimal default 0.3,ocrImage ARCHAR(50), default_value ARCHAR(200),format VARCHAR(100),control_type SMALLINT,mandatory SMALLINT default 1,is_reference SMALLINT default 0,is_unmapped SMALLINT default 0,maxLength SMALLINT default 0,scan SMALLINT default 1,feed SMALLINT default 1,status SMALLINT default 1,data_type INT,action_type INT, form VARCHAR(200), formname VARCHAR(200),custom_tag VARCHAR(200))";

                        sqlite_cmd.CommandText = CreateFieldSQL;
                        sqlite_cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Table already exists");
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return sqliteConn;
            }
            else if (sqliteConn.State != System.Data.ConnectionState.Open)
            {
                sqliteConn.Open();
                return sqliteConn;
            }
            else
                return sqliteConn;
            // return new SQLiteConnection();
        }
        public static bool InsertSQLiteData(List<ViewModels.MappingViewModel> lst)
        {
            Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                SQLiteCommand sqlitecmd;
                try
                {
                    foreach (ViewModels.MappingViewModel model in lst)
                    {
                        //string roomNumber = string.IsNullOrWhiteSpace(model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value) ? model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault().value : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value;
                        //DateTime dtArrival = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value);
                        //DateTime dtDeparture = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value);
                        ////this.lblDepartureDate.Text = objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value;
                        //model.roomno = ("" + roomNumber).Contains("$") ? "" : "" + roomNumber;
                        //string fName = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault().value;
                        //string LName = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault().value;
                        //string email = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault().value;
                        //string paymentAmount = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault() == null ? "" : ("" + model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault().value).Trim();
                        ////string paymentAmount = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault().value;
                        //string voucher = model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault() == null ? "" : model.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;  //objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;
                      

                        List<EntityFieldViewModel> fList = new List<EntityFieldViewModel>();
                        foreach (FormViewModel frm in model.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active)) //(x => x.status == (int)UTIL.Enums.STATUSES.InActive && !string.IsNullOrWhiteSpace(x.custom_tag) && ("" + x.custom_tag).Contains(BDUConstants.CUSTOM_TAG_EBILLS_KEYWORD_ROOT)))
                        {
                            fList.AddRange(frm.fields);
                        }

                        string roomNumber = string.IsNullOrWhiteSpace(fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value) ? fList.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault().value : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value;
                        DateTime dtArrival = fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value);
                        DateTime dtDeparture = fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value);
                        //this.lblDepartureDate.Text = objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value;
                        model.roomno = roomNumber.Contains("$") ? "" : roomNumber;
                        string fName = fList.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault().value;
                        string LName = fList.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault().value;
                        string email = fList.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault().value;
                        string paymentAmount = fList.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault().value;
                        string voucher = fList.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;  //objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;
                        string payableamount = string.Empty;
                        Double payableamountdbl = 0.0;
                        if (model.entity_Id == (int)UTIL.Enums.ENTITIES.BILLINGDETAILS)
                        {
                            //List<EntityFieldViewModel> aFlds = fList.Where(x => BDUConstants.PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES.Contains(x.fuid) && !string.IsNullOrWhiteSpace(x.value) && x.value != "0").ToList();
                            foreach (EntityFieldViewModel pItemfld in fList.Where(x => x.entity_id == model.entity_Id && BDUConstants.PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES.Contains(x.fuid)))
                            {
                                double n = 0;                              
                                if (!string.IsNullOrWhiteSpace(pItemfld.value) && Double.TryParse(pItemfld.value, out n))
                                {
                                    payableamountdbl += Convert.ToDouble(pItemfld.value);
                                }
                            }
                            payableamount = payableamountdbl > 0 ? Math.Round(payableamountdbl, 1).ToString() : "";
                        }
                        Double amount;
                        double.TryParse(paymentAmount, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out amount);

                        sqlitecmd = sqliteConn.CreateCommand();
                        // sqlitecmd.CommandText = "INSERT INTO reservations (reference,mode,arrivaldate,departuredate,receivetime,firstname,lastname,paymentamount,transactiondate,voucherno,roomno,syncstatus,email) VALUES('Ahmad',2,'2014-10-23 15:21:07','2014-10-23 15:21:07','2014-10-23 15:21:07','arshad','ali',120,'2014-10-23 15:21:07','20nm','10vm',12,'znawazch@gmail.com'); ";
                        sqlitecmd.CommandText = "INSERT INTO reservations (reference,mode,arrivaldate,departuredate,receivetime,firstname,lastname,paymentamount,transactiondate,voucherno,roomno,syncstatus,email,entity_id, entity_name,entity_type,undo,payableamount) VALUES(@reference,@mode,@arrivaldate,@departuredate,@receivetime,@firstname,@lastname,@paymentamount,@transactiondate,@voucherno,@roomno,@syncstatus,@email,@entity_id,@entity_name,@entity_type,@undo,@payableamount); ";
                        List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                        SQLiteParameter pEntity = new SQLiteParameter("@entity_id", "" + model.entity_Id);
                        parameter.Add(pEntity);
                        SQLiteParameter pReference = new SQLiteParameter("@reference", "" + model.reference);
                        parameter.Add(pReference);
                        SQLiteParameter pEntityName = new SQLiteParameter("@entity_name", "" + model.entity_name);
                        parameter.Add(pEntityName);
                        SQLiteParameter pMode = new SQLiteParameter("@mode", System.Data.DbType.Int16);
                        pMode.Value = model.mode;
                        parameter.Add(pMode);

                        SQLiteParameter preceivetime = new SQLiteParameter("@receivetime", System.Data.DbType.DateTime);
                        preceivetime.Value = GlobalApp.CurrentLocalDateTime;
                        parameter.Add(preceivetime);
                        SQLiteParameter pArrivaldate = new SQLiteParameter("@arrivaldate", System.Data.DbType.DateTime);
                        pArrivaldate.Value = dtArrival;
                        parameter.Add(pArrivaldate);
                        SQLiteParameter pDeparturedate = new SQLiteParameter("@departuredate", System.Data.DbType.DateTime);
                        pDeparturedate.Value = dtDeparture;
                        parameter.Add(pDeparturedate);
                        SQLiteParameter pfirstname = new SQLiteParameter("@firstname", fName);
                        parameter.Add(pfirstname);
                        SQLiteParameter pLastname = new SQLiteParameter("@lastname", LName);
                        parameter.Add(pLastname);
                        SQLiteParameter ppaymentamount = new SQLiteParameter("@paymentamount", System.Data.DbType.Decimal);
                        // ppaymentamount.Value = string.IsNullOrWhiteSpace(paymentAmount) || paymentAmount == "0" ? 0.0 : Convert.ToDouble(paymentAmount);
                        ppaymentamount.Value = string.IsNullOrWhiteSpace(paymentAmount.Trim()) || paymentAmount == "0" ? 0.0 : Convert.ToDecimal(amount);
                        parameter.Add(ppaymentamount);

                        SQLiteParameter ppayableamount = new SQLiteParameter("@payableamount", System.Data.DbType.Decimal);
                        ppayableamount.Value = string.IsNullOrWhiteSpace(payableamount) || payableamount == "0" ? 0.0 : payableamountdbl;
                        parameter.Add(ppayableamount);

                        SQLiteParameter pTtransactiondate = new SQLiteParameter("@transactiondate", System.Data.DbType.DateTime);
                        pTtransactiondate.Value = GlobalApp.CurrentLocalDateTime;
                        parameter.Add(pTtransactiondate);
                        SQLiteParameter pVoucherno = new SQLiteParameter("@voucherno", "" + voucher);
                        parameter.Add(pVoucherno);
                        SQLiteParameter pRoomno = new SQLiteParameter("@roomno", "" + model.roomno);
                        parameter.Add(pRoomno);
                        SQLiteParameter pSaves_status = new SQLiteParameter("@syncstatus", System.Data.DbType.Int16);
                        pSaves_status.Value = model.status;
                        parameter.Add(pSaves_status);
                        SQLiteParameter pEmail = new SQLiteParameter("@email", "" + email);
                        parameter.Add(pEmail);
                        SQLiteParameter pEntity_id = new SQLiteParameter("@entity_id", System.Data.DbType.Int16);
                        pEntity_id.Value = model.entity_Id;
                        parameter.Add(pEntity_id);
                        SQLiteParameter pentity_type = new SQLiteParameter("@entity_type", System.Data.DbType.Int16);
                        pentity_type.Value = model.entity_type;
                        parameter.Add(pentity_type);
                        SQLiteParameter pUndo = new SQLiteParameter("@undo", System.Data.DbType.Int16);
                        pUndo.Value = model.undo;
                        parameter.Add(pUndo);
                        //SQLiteParameter pEntity_name = new SQLiteParameter("@entity_name", model.entity_name);
                        //parameter.Add(pEntity_name);
                        sqlitecmd.Parameters.AddRange(parameter.ToArray());
                        int insertedCount = sqlitecmd.ExecuteNonQuery();

                        sqlitecmd.CommandText = "select last_insert_rowid()";
                        // The row ID is a 64-bit value - cast the Command result to an Int64.                        //
                        Int64 LastRowID64 = (Int64)sqlitecmd.ExecuteScalar();
                        // Then grab the bottom 32-bits as the unique ID of the row.
                        //
                        int LastRowID = (int)LastRowID64;
                        foreach (ViewModels.FormViewModel frm in model.forms.Where(x => x.Status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                        {
                            foreach (ViewModels.EntityFieldViewModel fld in frm.fields.Where(x => x.status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED && (x.feed == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED || x.scan == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED)))
                            {

                                if (!string.IsNullOrWhiteSpace(fld.value) || ((!string.IsNullOrWhiteSpace(fld.default_value) && fld.mandatory == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED) && !fld.default_value.Contains(BDUConstants.SPECIAL_KEYWORD_PREFIX)))
                                {
                                    sqlitecmd = sqliteConn.CreateCommand();
                                    sqlitecmd.CommandText = "INSERT INTO fields (idref,sr,fuid,field_desc,parent_field_id,pms_field_name,pms_field_xpath,pms_field_expression,value,automation_mode,ocrFactor,ocrImage,default_value,format, control_type,mandatory,is_reference,is_unmapped,maxLength,scan,feed,status,data_type,action_type,  form, formname, custom_tag ) VALUES(@idref,@sr,@fuid,@field_desc,@parent_field_id,@pms_field_name,@pms_field_xpath,@pms_field_expression,@value,@automation_mode,@ocrFactor,@ocrImage,@default_value,@format, @control_type,@mandatory,@is_reference,@is_unmapped,@maxLength,@scan,@feed,@status,@data_type,@action_type,@form, @formname,@custom_tag); ";
                                    parameter = new List<SQLiteParameter>();
                                    SQLiteParameter idref = new SQLiteParameter("@idref", System.Data.DbType.Int64);
                                    idref.Value = LastRowID64;
                                    parameter.Add(idref);
                                    SQLiteParameter sr = new SQLiteParameter("@sr", System.Data.DbType.Int16);
                                    sr.Value = fld.sr;
                                    parameter.Add(sr);
                                    SQLiteParameter fuid = new SQLiteParameter("@fuid", fld.fuid);
                                    parameter.Add(fuid);
                                    SQLiteParameter field_desc = new SQLiteParameter("@field_desc", fld.field_desc);
                                    parameter.Add(field_desc);
                                    SQLiteParameter parent_field_id = new SQLiteParameter("@parent_field_id", fld.parent_field_id);
                                    parameter.Add(parent_field_id);
                                    SQLiteParameter pms_field_name = new SQLiteParameter("@pms_field_name", fld.pms_field_name);
                                    parameter.Add(pms_field_name);
                                    SQLiteParameter pms_field_xpath = new SQLiteParameter("@pms_field_xpath", fld.pms_field_xpath);
                                    parameter.Add(pms_field_xpath);
                                    SQLiteParameter pms_field_expression = new SQLiteParameter("@pms_field_expression", fld.pms_field_expression);
                                    parameter.Add(pms_field_expression);
                                    //@is_unmapped,@maxLength,@scan,@feed,@status,@data_type,@action_type
                                    SQLiteParameter value = new SQLiteParameter("@value", fld.value);
                                    parameter.Add(value);
                                    SQLiteParameter pAutomation_mode = new SQLiteParameter("@automation_mode", System.Data.DbType.SByte);
                                    pAutomation_mode.Value = fld.automation_mode;
                                    parameter.Add(pAutomation_mode);
                                    SQLiteParameter ocrFactor = new SQLiteParameter("@ocrFactor", System.Data.DbType.Decimal);
                                    ocrFactor.Value = fld.ocrFactor;
                                    parameter.Add(ocrFactor);
                                    SQLiteParameter ocrImage = new SQLiteParameter("@ocrImage", fld.ocrImage);
                                    parameter.Add(ocrImage);
                                    SQLiteParameter default_value = new SQLiteParameter("@default_value", fld.default_value);
                                    parameter.Add(default_value);
                                    SQLiteParameter format = new SQLiteParameter("@format", fld.format);
                                    parameter.Add(format);
                                    SQLiteParameter control_type = new SQLiteParameter("@control_type", System.Data.DbType.Int16);
                                    control_type.Value = fld.control_type;
                                    parameter.Add(control_type);
                                    SQLiteParameter is_unmapped = new SQLiteParameter("@is_unmapped", System.Data.DbType.Int16);
                                    is_unmapped.Value = fld.is_unmapped;
                                    parameter.Add(is_unmapped);
                                    SQLiteParameter mandatory = new SQLiteParameter("@mandatory", System.Data.DbType.SByte);
                                    mandatory.Value = fld.mandatory;
                                    parameter.Add(mandatory);
                                    SQLiteParameter is_reference = new SQLiteParameter("@is_reference", System.Data.DbType.Int16);
                                    is_reference.Value = fld.is_reference;
                                    parameter.Add(is_reference);
                                    SQLiteParameter maxLength = new SQLiteParameter("@maxLength", System.Data.DbType.Int16);
                                    maxLength.Value = fld.maxLength;
                                    parameter.Add(maxLength);
                                  
                                    SQLiteParameter pCustomTag = new SQLiteParameter("@custom_tag", ""+fld.custom_tag);
                                    parameter.Add(pCustomTag);
                                    // scan,feed,status,data_type,action_type
                                    SQLiteParameter scan = new SQLiteParameter("@scan", System.Data.DbType.SByte);
                                    scan.Value = fld.scan;
                                    parameter.Add(scan);
                                    SQLiteParameter feed = new SQLiteParameter("@feed", System.Data.DbType.SByte);
                                    feed.Value = fld.feed;
                                    parameter.Add(feed);
                                    SQLiteParameter status = new SQLiteParameter("@status", System.Data.DbType.SByte);
                                    status.Value = fld.status;
                                    parameter.Add(status);
                                    SQLiteParameter data_type = new SQLiteParameter("@data_type", System.Data.DbType.Int16);
                                    data_type.Value = fld.data_type;
                                    parameter.Add(data_type);
                                    SQLiteParameter action_type = new SQLiteParameter("@action_type", System.Data.DbType.Int16);
                                    action_type.Value = fld.action_type;
                                    parameter.Add(action_type);
                                    SQLiteParameter form = new SQLiteParameter("@form", frm.pmspageid);
                                    parameter.Add(form);
                                    SQLiteParameter formname = new SQLiteParameter("@formname", frm.pmspagename);
                                    parameter.Add(formname);
                                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                                    insertedCount = sqlitecmd.ExecuteNonQuery();
                                }// Validation Check
                            } // Forms

                        }// Fields
                    }
                    retStatus = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Reservation data storage in SQlite failed, Error detail {0}", ex.Message));
                // throw new Exception("Reservation data storage in SQlite failed");
            }
            return retStatus;
            // return new SQLiteConnection();
        }
        public static bool InsertSQLiteData(ViewModels.MappingViewModel model, bool force = false)
        {
            Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                SQLiteCommand sqlitecmd;
                try
                {
                    sqlitecmd = sqliteConn.CreateCommand();
                    if (force)
                    {
                        //sqlitecmd.CommandText = "delete from  reservations where id=@id; ";
                        //List<SQLiteParameter> dParam = new List<SQLiteParameter>();
                        //SQLiteParameter pid = new SQLiteParameter("@id", System.Data.DbType.Int64);
                        //pid.Value = model.uid;
                        //dParam.Add(pid);
                        //sqlitecmd.Parameters.AddRange(dParam.ToArray());
                        //int res = sqlitecmd.ExecuteNonQuery();
                        //sqlitecmd.CommandText = "delete from  fields where idref=@idref; ";
                        //dParam = new List<SQLiteParameter>();
                        //SQLiteParameter idref = new SQLiteParameter("@idref", System.Data.DbType.Int64);
                        //idref.Value = model.uid;
                        //dParam.Add(idref);
                        //sqlitecmd.Parameters.AddRange(dParam.ToArray());
                        //int resF = sqlitecmd.ExecuteNonQuery();
                    }
                    List<EntityFieldViewModel> fList = new List<EntityFieldViewModel>();
                    foreach (FormViewModel frm in model.forms.Where(x => x.Status == (int)UTIL.Enums.STATUSES.Active)) //(x => x.status == (int)UTIL.Enums.STATUSES.InActive && !string.IsNullOrWhiteSpace(x.custom_tag) && ("" + x.custom_tag).Contains(BDUConstants.CUSTOM_TAG_EBILLS_KEYWORD_ROOT)))
                    {
                        fList.AddRange(frm.fields);
                    }

                    string roomNumber = string.IsNullOrWhiteSpace(fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value) ? fList.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("rmno")).FirstOrDefault().value : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("room number")).FirstOrDefault().value;
                    DateTime dtArrival = fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(fList.Where(x => x.field_desc.ToLower().Contains("arrival date")).FirstOrDefault().value);
                    DateTime dtDeparture = fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault() == null ? GlobalApp.CurrentDateTime : string.IsNullOrWhiteSpace("" + (fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value)) ? GlobalApp.CurrentDateTime : Convert.ToDateTime(fList.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value);
                    //this.lblDepartureDate.Text = objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("departure date")).FirstOrDefault().value;
                    model.roomno = roomNumber.Contains("$") ? "" : roomNumber;
                    string fName = fList.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("first name")).FirstOrDefault().value;
                    string LName = fList.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("last name")).FirstOrDefault().value;
                    string email = fList.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("email")).FirstOrDefault().value;
                    string paymentAmount = fList.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("total_price")).FirstOrDefault().value;
                    string voucher = fList.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault() == null ? "" : fList.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;  //objMapping.forms.FirstOrDefault().fields.Where(x => x.field_desc.ToLower().Contains("voucher")).FirstOrDefault().value;
                    string payableamount = string.Empty;
                    Double payableamountdbl = 0.0;
                    if (model.entity_Id== (int)UTIL.Enums.ENTITIES.BILLINGDETAILS) {
                        //List<EntityFieldViewModel> aFlds = fList.Where(x => BDUConstants.PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES.Contains(x.fuid) && !string.IsNullOrWhiteSpace(x.value) && x.value != "0").ToList();
                        foreach (EntityFieldViewModel pItemfld in fList.Where(x => x.entity_id== model.entity_Id && BDUConstants.PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES.Contains(x.fuid)))
                        {
                            double n = 0;                           
                            if (!string.IsNullOrWhiteSpace(pItemfld.value) && Double.TryParse(pItemfld.value, out n))
                            {
                                payableamountdbl += Convert.ToDouble(pItemfld.value);                                
                            }
                        }
                        payableamount = payableamountdbl > 0 ? Math.Round(payableamountdbl, 1).ToString() : "";
                    }
                    // sqlitecmd.CommandText = "INSERT INTO reservations (reference,mode,arrivaldate,departuredate,receivetime,firstname,lastname,paymentamount,transactiondate,voucherno,roomno,syncstatus,email) VALUES('Ahmad',2,'2014-10-23 15:21:07','2014-10-23 15:21:07','2014-10-23 15:21:07','arshad','ali',120,'2014-10-23 15:21:07','20nm','10vm',12,'znawazch@gmail.com'); ";
                    sqlitecmd.CommandText = "INSERT INTO reservations (reference,mode,arrivaldate,departuredate,receivetime,firstname,lastname,paymentamount,transactiondate,voucherno,roomno,syncstatus,email,entity_id, entity_name,entity_type,undo,payableamount) VALUES(@reference,@mode,@arrivaldate,@departuredate,@receivetime,@firstname,@lastname,@paymentamount,@transactiondate,@voucherno,@roomno,@syncstatus,@email,@entity_id,@entity_name,@entity_type,@undo,@payableamount); ";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pEntity = new SQLiteParameter("@entity_id", "" + model.entity_Id);
                    parameter.Add(pEntity);
                    SQLiteParameter pReference = new SQLiteParameter("@reference", model.reference);
                    parameter.Add(pReference);
                    SQLiteParameter pEntityName = new SQLiteParameter("@entity_name", model.entity_name);
                    parameter.Add(pEntityName);
                    SQLiteParameter pMode = new SQLiteParameter("@mode", System.Data.DbType.Int16);
                    pMode.Value = model.mode;
                    parameter.Add(pMode);
                    SQLiteParameter preceivetime = new SQLiteParameter("@receivetime", System.Data.DbType.DateTime);
                    preceivetime.Value = GlobalApp.CurrentLocalDateTime;
                    parameter.Add(preceivetime);
                    SQLiteParameter pArrivaldate = new SQLiteParameter("@arrivaldate", System.Data.DbType.DateTime);
                    pArrivaldate.Value = dtArrival;
                    parameter.Add(pArrivaldate);
                    SQLiteParameter pDeparturedate = new SQLiteParameter("@departuredate", System.Data.DbType.DateTime);
                    pDeparturedate.Value = dtDeparture;
                    parameter.Add(pDeparturedate);
                    SQLiteParameter pfirstname = new SQLiteParameter("@firstname", fName);
                    parameter.Add(pfirstname);
                    SQLiteParameter pLastname = new SQLiteParameter("@lastname", LName);
                    parameter.Add(pLastname);
                    SQLiteParameter ppaymentamount = new SQLiteParameter("@paymentamount", System.Data.DbType.Decimal);
                    ppaymentamount.Value = string.IsNullOrWhiteSpace(paymentAmount) || paymentAmount == "0" ? 0.0 : Convert.ToDouble(paymentAmount);
                    parameter.Add(ppaymentamount);

                    SQLiteParameter ppayableamount = new SQLiteParameter("@payableamount", System.Data.DbType.Decimal);
                    ppayableamount.Value = string.IsNullOrWhiteSpace(payableamount) || payableamount == "0" ? 0.0 : payableamountdbl;
                    parameter.Add(ppayableamount);
                    SQLiteParameter pTtransactiondate = new SQLiteParameter("@transactiondate", System.Data.DbType.DateTime);
                    pTtransactiondate.Value = model.createdAt.Year <= 1900 ? GlobalApp.CurrentLocalDateTime : model.createdAt;
                    parameter.Add(pTtransactiondate);
                    SQLiteParameter pVoucherno = new SQLiteParameter("@voucherno", voucher);
                    parameter.Add(pVoucherno);
                    SQLiteParameter pRoomno = new SQLiteParameter("@roomno", model.roomno);
                    parameter.Add(pRoomno);
                    SQLiteParameter pSaves_status = new SQLiteParameter("@syncstatus", System.Data.DbType.Int16);
                    pSaves_status.Value = model.status;
                    parameter.Add(pSaves_status);
                    SQLiteParameter pEmail = new SQLiteParameter("@email", email);
                    parameter.Add(pEmail);
                    SQLiteParameter pEntity_id = new SQLiteParameter("@entity_id", System.Data.DbType.Int16);
                    pEntity_id.Value = model.entity_Id;
                    parameter.Add(pEntity_id);
                    SQLiteParameter pEntity_name = new SQLiteParameter("@entity_name", model.entity_name);
                    parameter.Add(pEntity_name);
                    SQLiteParameter pentity_type = new SQLiteParameter("@entity_type", System.Data.DbType.Int16);
                    pentity_type.Value = model.entity_type;
                    parameter.Add(pentity_type);
                    SQLiteParameter pUndo = new SQLiteParameter("@undo", System.Data.DbType.Int16);
                    pUndo.Value = model.undo;
                    parameter.Add(pUndo);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    int insertedCount = sqlitecmd.ExecuteNonQuery();

                    sqlitecmd.CommandText = "select last_insert_rowid()";
                    // The row ID is a 64-bit value - cast the Command result to an Int64.                        //
                    Int64 LastRowID64 = (Int64)sqlitecmd.ExecuteScalar();
                    // Then grab the bottom 32-bits as the unique ID of the row.
                    //
                    int LastRowID = (int)LastRowID64;
                    foreach (ViewModels.FormViewModel frm in model.forms.Where(x => x.Status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED))
                    {
                        foreach (ViewModels.EntityFieldViewModel fld in frm.fields.Where(x => x.status == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED && (x.feed == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED || x.scan == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED)))
                        {
                            if (!string.IsNullOrWhiteSpace(fld.value) || ((!string.IsNullOrWhiteSpace(fld.default_value) && fld.mandatory == (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED) && !fld.default_value.Contains(BDUConstants.SPECIAL_KEYWORD_PREFIX)))
                            {
                                sqlitecmd = sqliteConn.CreateCommand();
                                sqlitecmd.CommandText = "INSERT INTO fields (idref,sr,fuid,field_desc,parent_field_id,pms_field_name,pms_field_xpath,pms_field_expression,value,automation_mode,ocrFactor,ocrImage,default_value,format, control_type,mandatory,is_reference,is_unmapped,maxLength,scan,feed,status,data_type,action_type,  form, formname,custom_tag ) VALUES(@idref,@sr,@fuid,@field_desc,@parent_field_id,@pms_field_name,@pms_field_xpath,@pms_field_expression,@value,@automation_mode,@ocrFactor,@ocrImage,@default_value,@format, @control_type,@mandatory,@is_reference,@is_unmapped,@maxLength,@scan,@feed,@status,@data_type,@action_type,@form, @formname,@custom_tag); ";
                                parameter = new List<SQLiteParameter>();
                                SQLiteParameter idref = new SQLiteParameter("@idref", System.Data.DbType.Int64);
                                idref.Value = LastRowID64;
                                parameter.Add(idref);
                                SQLiteParameter sr = new SQLiteParameter("@sr", System.Data.DbType.Int16);
                                sr.Value = fld.sr;
                                parameter.Add(sr);
                                SQLiteParameter fuid = new SQLiteParameter("@fuid", fld.fuid);
                                parameter.Add(fuid);
                                SQLiteParameter field_desc = new SQLiteParameter("@field_desc", fld.field_desc);
                                parameter.Add(field_desc);
                                SQLiteParameter parent_field_id = new SQLiteParameter("@parent_field_id", fld.parent_field_id);
                                parameter.Add(parent_field_id);
                                SQLiteParameter pms_field_name = new SQLiteParameter("@pms_field_name", fld.pms_field_name);
                                parameter.Add(pms_field_name);
                                SQLiteParameter pms_field_xpath = new SQLiteParameter("@pms_field_xpath", fld.pms_field_xpath);
                                parameter.Add(pms_field_xpath);
                                SQLiteParameter pms_field_expression = new SQLiteParameter("@pms_field_expression", fld.pms_field_expression);
                                parameter.Add(pms_field_expression);
                                //@is_unmapped,@maxLength,@scan,@feed,@status,@data_type,@action_type
                                SQLiteParameter value = new SQLiteParameter("@value", fld.value);
                                parameter.Add(value);
                                SQLiteParameter pAutomation_mode = new SQLiteParameter("@automation_mode", System.Data.DbType.SByte);
                                pAutomation_mode.Value = fld.automation_mode;
                                parameter.Add(pAutomation_mode);
                                SQLiteParameter ocrFactor = new SQLiteParameter("@ocrFactor", System.Data.DbType.Decimal);
                                ocrFactor.Value = fld.ocrFactor;
                                parameter.Add(ocrFactor);
                                SQLiteParameter ocrImage = new SQLiteParameter("@ocrImage", fld.ocrImage);
                                parameter.Add(ocrImage);
                                SQLiteParameter default_value = new SQLiteParameter("@default_value", fld.default_value);
                                parameter.Add(default_value);
                                SQLiteParameter format = new SQLiteParameter("@format", fld.format);
                                parameter.Add(format);
                                SQLiteParameter control_type = new SQLiteParameter("@control_type", System.Data.DbType.Int16);
                                control_type.Value = fld.control_type;
                                parameter.Add(control_type);
                                SQLiteParameter is_unmapped = new SQLiteParameter("@is_unmapped", System.Data.DbType.Int16);
                                is_unmapped.Value = fld.is_unmapped;
                                parameter.Add(is_unmapped);
                                SQLiteParameter mandatory = new SQLiteParameter("@mandatory", System.Data.DbType.SByte);
                                mandatory.Value = fld.mandatory;
                                parameter.Add(mandatory);
                                SQLiteParameter is_reference = new SQLiteParameter("@is_reference", System.Data.DbType.Int16);
                                is_reference.Value = fld.is_reference;
                                parameter.Add(is_reference);
                                SQLiteParameter maxLength = new SQLiteParameter("@maxLength", System.Data.DbType.Int16);
                                maxLength.Value = fld.maxLength;
                                parameter.Add(maxLength);
                                // scan,feed,status,data_type,action_type
                                SQLiteParameter scan = new SQLiteParameter("@scan", System.Data.DbType.SByte);
                                scan.Value = fld.scan;
                                parameter.Add(scan);
                                SQLiteParameter feed = new SQLiteParameter("@feed", System.Data.DbType.SByte);
                                feed.Value = fld.feed;
                                parameter.Add(feed);
                                SQLiteParameter status = new SQLiteParameter("@status", System.Data.DbType.SByte);
                                status.Value = fld.status;
                                parameter.Add(status);
                                SQLiteParameter data_type = new SQLiteParameter("@data_type", System.Data.DbType.Int16);
                                data_type.Value = fld.data_type;
                                parameter.Add(data_type);
                                SQLiteParameter action_type = new SQLiteParameter("@action_type", System.Data.DbType.Int16);
                                action_type.Value = fld.action_type;
                                parameter.Add(action_type);
                                SQLiteParameter form = new SQLiteParameter("@form", frm.pmspageid);
                                parameter.Add(form);
                                SQLiteParameter formname = new SQLiteParameter("@formname", frm.pmspagename);
                                parameter.Add(formname);
                                SQLiteParameter pCustomTag = new SQLiteParameter("@custom_tag", "" + fld.custom_tag);
                                parameter.Add(pCustomTag);
                                sqlitecmd.Parameters.AddRange(parameter.ToArray());
                                insertedCount = sqlitecmd.ExecuteNonQuery();
                            }// Validation Check
                        } // Forms

                    }// Fields

                    retStatus = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Reservation data persist in database failed");
            }
            return retStatus;
            // return new SQLiteConnection();
        }
        public static List<ViewModels.SQLiteMappingViewModel> loadSQLiteHistoryData(DateTime dtFrom, DateTime dtTo)
        {
            List<ViewModels.SQLiteMappingViewModel> lst = new List<ViewModels.SQLiteMappingViewModel>();
            // Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            dtFrom = dtFrom.Year <= 1900 ? GlobalApp.CurrentLocalDateTime : dtFrom;
            dtTo = dtTo.Year <= 1900 ? GlobalApp.CurrentLocalDateTime.AddDays(-1) : dtTo;
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    sqlitecmd.CommandText = "SELECT * FROM reservations where DATE(transactiondate) >= @dtFrom and DATE(transactiondate) <= @dtTo;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pDateFrom = new SQLiteParameter("@dtFrom", System.Data.DbType.Date);
                    pDateFrom.Value = dtFrom.AddDays(-10);
                    parameter.Add(pDateFrom);
                    SQLiteParameter pDtTo = new SQLiteParameter("@dtTo", System.Data.DbType.Date);
                    pDtTo.Value = dtTo;
                    parameter.Add(pDtTo);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    sqlite_datareader = sqlitecmd.ExecuteReader();

                    while (sqlite_datareader.Read() && lst != null)
                    {
                        ViewModels.SQLiteMappingViewModel model = new ViewModels.SQLiteMappingViewModel();

                        model.id = sqlite_datareader.GetInt64(0);
                        model.reference = "" + sqlite_datareader.GetValue(1);// sqlite_datareader.GetString(1);
                        model.mode = sqlite_datareader.GetInt16(2);
                        model.arrivaldate = sqlite_datareader.GetDateTime(3);
                        model.departuredate = sqlite_datareader.GetDateTime(4);
                        model.receivetime = sqlite_datareader.GetDateTime(5);
                        model.firstname = "" + sqlite_datareader.GetValue(6);
                        model.lastname = "" + sqlite_datareader.GetValue(7);
                        model.paymentamount = Convert.ToDouble(sqlite_datareader.GetValue(8));
                        model.transactiondate = sqlite_datareader.GetDateTime(9);
                        model.voucherno = "" + sqlite_datareader.GetValue(10);
                        model.roomno = "" + sqlite_datareader.GetValue(11);// sqlite_datareader.GetString(11);
                        model.syncstatus = sqlite_datareader.GetInt32(12);
                        model.email = "" + sqlite_datareader.GetValue(13); //sqlite_datareader.GetString(13);
                        model.entity_id = sqlite_datareader.GetInt16(14);
                        model.entity_name = "" + sqlite_datareader.GetValue(15); //sqlite_datareader.GetString(15);
                        model.entity_type = sqlite_datareader.GetInt16(16);
                        model.undo = sqlite_datareader.GetInt16(17);
                        model.payableamount = Convert.ToDouble(sqlite_datareader.GetValue(18));
                        lst.Add(model);
                    }
                    // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lst;
            // return new SQLiteConnection();
        }
        public static List<ViewModels.SQLiteMappingViewModel> loadSQLiteData(DateTime dtFrom, DateTime dtTo, Int16 currentEntity = 0, Int16 syncstatus = 1)
        {
            List<ViewModels.SQLiteMappingViewModel> lst = new List<ViewModels.SQLiteMappingViewModel>();
            // Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            dtFrom = dtFrom.Year <= 1900 ? GlobalApp.CurrentLocalDateTime : dtFrom;
            dtTo = dtTo.Year <= 1900 ? GlobalApp.CurrentLocalDateTime : dtTo;
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    if (currentEntity > 0)
                        sqlitecmd.CommandText = "SELECT * FROM reservations where DATE(transactiondate) between @dtFrom and @dtTo and  entity_id=@entity_id and syncstatus=@syncstatus;";
                    else
                        sqlitecmd.CommandText = "SELECT * FROM reservations where DATE(transactiondate) between @dtFrom and @dtTo and syncstatus=@syncstatus;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pDateFrom = new SQLiteParameter("@dtFrom", System.Data.DbType.Date);
                    pDateFrom.Value = dtFrom.Date;
                    parameter.Add(pDateFrom);
                    SQLiteParameter pDtTo = new SQLiteParameter("@dtTo", System.Data.DbType.Date);
                    pDtTo.Value = dtTo.Date;
                    parameter.Add(pDtTo);
                    SQLiteParameter pStatus = new SQLiteParameter("@syncstatus", System.Data.DbType.Int16);
                    pStatus.Value = syncstatus;
                    parameter.Add(pStatus);
                    if (currentEntity > 0)
                    {
                        SQLiteParameter pEntity_id = new SQLiteParameter("@entity_id", System.Data.DbType.Int16);
                        pEntity_id.Value = currentEntity;
                        parameter.Add(pEntity_id);
                    }
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    sqlite_datareader = sqlitecmd.ExecuteReader();

                    DataTable dataTable= new DataTable();
                    List<string> stringslist = new List<string>();
                    var ddk = sqlite_datareader.GetSchemaTable();

                    var ddf2 = sqlite_datareader.HasRows;
                    while (sqlite_datareader.Read() && lst != null)
                    {
                        ViewModels.SQLiteMappingViewModel model = new ViewModels.SQLiteMappingViewModel();

                        model.id = sqlite_datareader.GetInt64(0);
                        //model.ui = sqlite_datareader.GetInt64(0);
                        model.reference = "" + sqlite_datareader.GetValue(1);// sqlite_datareader.GetString(1);
                        model.mode = sqlite_datareader.GetInt16(2);
                        model.arrivaldate = sqlite_datareader.GetDateTime(3);
                        model.departuredate = sqlite_datareader.GetDateTime(4);
                        model.receivetime = sqlite_datareader.GetDateTime(5);
                        model.firstname = "" + sqlite_datareader.GetValue(6);
                        model.lastname = "" + sqlite_datareader.GetValue(7);
                        model.paymentamount = Convert.ToDouble(sqlite_datareader.GetValue(8));
                        model.transactiondate = sqlite_datareader.GetDateTime(9);
                        model.voucherno = "" + sqlite_datareader.GetValue(10);
                        model.roomno = "" + sqlite_datareader.GetValue(11);// sqlite_datareader.GetString(11);
                        model.syncstatus = sqlite_datareader.GetInt32(12);
                        model.email = "" + sqlite_datareader.GetValue(13); //sqlite_datareader.GetString(13);
                        model.entity_id = sqlite_datareader.GetInt16(14);
                        model.entity_name = "" + sqlite_datareader.GetValue(15); //sqlite_datareader.GetString(15);
                        model.entity_type = sqlite_datareader.GetInt16(16);
                        model.undo = sqlite_datareader.GetInt16(17);
                        model.payableamount = Convert.ToDouble(sqlite_datareader.GetValue(18));// GetInt16(18);
                        lst.Add(model);
                    }
                    // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lst;
            // return new SQLiteConnection();
        }
        public static ViewModels.SQLiteMappingViewModel loadSQLiteDataDetailed(Int64 Uid)
        {
            ViewModels.SQLiteMappingViewModel model = new ViewModels.SQLiteMappingViewModel();
            // Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    sqlitecmd.CommandText = "SELECT * FROM reservations where id=@id";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pUid = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pUid.Value = Uid;
                    parameter.Add(pUid);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    sqlite_datareader = sqlitecmd.ExecuteReader();

                    if (sqlite_datareader.Read() && model != null)
                    {


                        model.id = sqlite_datareader.GetInt64(0);
                        model.reference = "" + sqlite_datareader.GetValue(1);// sqlite_datareader.GetString(1);
                        model.mode = sqlite_datareader.GetInt16(2);
                        model.arrivaldate = sqlite_datareader.GetDateTime(3);
                        model.departuredate = sqlite_datareader.GetDateTime(4);
                        model.receivetime = sqlite_datareader.GetDateTime(5);
                        model.firstname = "" + sqlite_datareader.GetValue(6);
                        model.lastname = "" + sqlite_datareader.GetValue(7);
                        model.paymentamount = Convert.ToDouble(sqlite_datareader.GetValue(8));
                        model.transactiondate = sqlite_datareader.GetDateTime(9);
                        model.voucherno = "" + sqlite_datareader.GetValue(10);
                        model.roomno = "" + sqlite_datareader.GetValue(11);// sqlite_datareader.GetString(11);
                        model.syncstatus = sqlite_datareader.GetInt32(12);
                        model.email = "" + sqlite_datareader.GetValue(13); //sqlite_datareader.GetString(13);
                        model.entity_id = sqlite_datareader.GetInt16(14);
                        model.entity_name = "" + sqlite_datareader.GetValue(15); //sqlite_datareader.GetString(15);
                        model.entity_type = sqlite_datareader.GetInt16(16);
                        model.undo = sqlite_datareader.GetInt16(17);
                        model.payableamount = Convert.ToDouble(sqlite_datareader.GetValue(18));// sqlite_datareader.GetInt16(18);
                        
                    }
                    // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
            // return new SQLiteConnection();
        }
        public static ViewModels.MappingViewModel loadSQLiteFullData(Int64 id, int status = 1)
        {
            ViewModels.MappingViewModel model = new ViewModels.MappingViewModel();
            List<string> lstFrms = new List<string>();
            Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {
                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    sqlitecmd.CommandText = "SELECT  * FROM reservations where id=@id;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pId = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pId.Value = id;
                    parameter.Add(pId);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    sqlite_datareader = sqlitecmd.ExecuteReader();
                    if (sqlite_datareader.Read() && model != null)
                    {
                        model.uid = sqlite_datareader.GetInt64(0);
                        model.reference = "" + sqlite_datareader.GetValue(1);// sqlite_datareader.GetString(1);
                        model.mode = sqlite_datareader.GetInt32(2);
                        model.createdAt = sqlite_datareader.GetDateTime(9);
                        model.roomno = "" + sqlite_datareader.GetValue(11);// sqlite_datareader.GetString(11);
                        model.status = sqlite_datareader.GetInt32(12);
                        model.entity_Id = sqlite_datareader.GetInt32(14);
                        model.id = model.entity_Id;
                        model.entity_name = "" + sqlite_datareader.GetValue(15);// sqlite_datareader.GetString(15);
                        model.undo = sqlite_datareader.GetInt16(17);
                       // model.pay = Convert.ToDouble(sqlite_datareader.GetValue(8)) sqlite_datareader.GetInt16(17);
                        sqlite_datareader.Close();
                        int fromId = 0;
                        // Get field Data
                        if (model.id > 0)
                        {
                            //sqlitecmd = sqliteConn.CreateCommand();
                            sqlitecmd.CommandText = "SELECT sr,fuid,field_desc,parent_field_id,pms_field_name,pms_field_xpath,pms_field_expression,value,automation_mode,ocrFactor,ocrImage,default_value,format, control_type,mandatory,is_reference,is_unmapped,maxLength,scan,feed,data_type,action_type,form, formname,custom_tag FROM fields where idref=@refid";
                            parameter = new List<SQLiteParameter>();
                            SQLiteParameter refid = new SQLiteParameter("@refid", System.Data.DbType.Int64);
                            refid.Value = id;
                            parameter.Add(refid);
                            ////SQLiteParameter pstatus = new SQLiteParameter("@status", System.Data.DbType.Int16);
                            ////pstatus.Value = status;
                            ////parameter.Add(pstatus);
                            sqlitecmd.Parameters.AddRange(parameter.ToArray());
                            SQLiteDataReader sqlitedrdr = sqlitecmd.ExecuteReader();
                            List<ViewModels.EntityFieldViewModel> flds = new List<ViewModels.EntityFieldViewModel>();
                            List<ViewModels.FormViewModel> frmls = new List<ViewModels.FormViewModel>();
                            while (sqlitedrdr.Read() && model != null)
                            {
                                ViewModels.EntityFieldViewModel fldModel = new ViewModels.EntityFieldViewModel();
                                //fldModel.id = id;
                                fldModel.sr = sqlitedrdr.GetInt32(0);
                                fldModel.fuid = "" + sqlitedrdr.GetValue(1);// sqlitedrdr.GetString(1);
                                fldModel.field_desc = "" + sqlitedrdr.GetValue(2);// sqlitedrdr.GetString(2);
                                fldModel.parent_field_id = "" + sqlitedrdr.GetValue(3);// sqlitedrdr.GetString(3);
                                fldModel.pms_field_name = "" + sqlitedrdr.GetValue(4);// sqlitedrdr.GetString(4);
                                fldModel.pms_field_xpath = "" + sqlitedrdr.GetValue(5);// sqlitedrdr.GetString(5);
                                fldModel.pms_field_expression = "" + sqlitedrdr.GetValue(6);// sqlitedrdr.GetString(6);
                                fldModel.value = "" + sqlitedrdr.GetValue(7);// sqlitedrdr.GetString(7);
                                fldModel.automation_mode = sqlitedrdr.GetInt32(8);
                                fldModel.ocrFactor = Convert.ToDouble(sqlitedrdr.GetValue(9));// sqlitedrdr.GetDouble(9);
                                fldModel.ocrImage = "" + sqlitedrdr.GetValue(10);// sqlitedrdr.GetString(10);
                                fldModel.default_value = "" + sqlitedrdr.GetValue(11);// sqlitedrdr.GetString(11);
                                fldModel.format = "" + sqlitedrdr.GetValue(12);// sqlitedrdr.GetString(12);
                                fldModel.control_type = sqlitedrdr.GetInt16(13);
                                fldModel.mandatory = sqlitedrdr.GetInt16(14);
                                fldModel.is_reference = sqlitedrdr.GetInt16(15);
                                fldModel.is_unmapped = sqlitedrdr.GetInt16(16);
                                fldModel.maxLength = sqlitedrdr.GetInt16(17);
                                fldModel.scan = sqlitedrdr.GetInt16(18);
                                fldModel.feed = sqlitedrdr.GetInt16(19);
                                fldModel.data_type = sqlitedrdr.GetInt16(20);
                                fldModel.action_type = sqlitedrdr.GetInt16(21);
                                fldModel.custom_tag = "" + sqlitedrdr.GetValue(24);// Custom tag
                                fldModel.entity_id = model.entity_Id;
                                flds.Add(fldModel);
                                if (frmls.Where(x => x.pmspageid.Contains("" + sqlitedrdr.GetValue(22))).FirstOrDefault() == null)
                                {
                                    //form, formname
                                    ViewModels.FormViewModel frm = new ViewModels.FormViewModel();
                                    frm.id = fromId + 1;
                                    frm.entityid = model.entity_Id;
                                    frm.pmspageid = "" + sqlitedrdr.GetValue(22);// sqlitedrdr.GetString(22);
                                    frm.pmspagename = "" + sqlitedrdr.GetValue(23);// sqlitedrdr.GetString(23);                                   
                                    frm.fields = flds;
                                    frmls.Add(frm);
                                    if (frmls.Count > 1)
                                        flds = new List<ViewModels.EntityFieldViewModel>();
                                }
                            }// while (sqlitedrdr.Read() && model != null)
                            if (frmls != null && frmls.Any())
                                model.forms = frmls;
                            sqlitedrdr.Close();
                        }
                    }// While
                     // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
            // return new SQLiteConnection();
        }
        public static ViewModels.MappingViewModel loadSQLiteFullDataWithAllFields(Int64 id, int pEntityId)
        {
            ViewModels.MappingViewModel model = new ViewModels.MappingViewModel();
            ViewModels.MappingViewModel SModel = API.PRESETS.mappings.Where(x => x.entity_Id == pEntityId).FirstOrDefault();

            List<string> lstFrms = new List<string>();
            Boolean retStatus = false;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {
                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    sqlitecmd.CommandText = "SELECT  * FROM reservations where id=@id;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pId = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pId.Value = id;
                    parameter.Add(pId);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    sqlite_datareader = sqlitecmd.ExecuteReader();
                    if (sqlite_datareader.Read() && model != null)
                    {
                        model.uid = sqlite_datareader.GetInt64(0);
                        model.reference = "" + sqlite_datareader.GetValue(1);// sqlite_datareader.GetString(1);
                        model.mode = sqlite_datareader.GetInt32(2);
                        model.createdAt = sqlite_datareader.GetDateTime(9);
                        model.roomno = "" + sqlite_datareader.GetValue(11);// sqlite_datareader.GetString(11);
                        model.status = sqlite_datareader.GetInt32(12);
                        model.entity_Id = sqlite_datareader.GetInt32(14);
                        model.undo = sqlite_datareader.GetInt16(17);
                        model.id = model.entity_Id;
                        model.entity_name = "" + sqlite_datareader.GetValue(15);// sqlite_datareader.GetString(15);
                        sqlite_datareader.Close();
                        int fromId = 0;
                        // Get field Data
                        if (model.id > 0)
                        {
                            //sqlitecmd = sqliteConn.CreateCommand();
                            sqlitecmd.CommandText = "SELECT sr,fuid,field_desc,parent_field_id,pms_field_name,pms_field_xpath,pms_field_expression,value,automation_mode,ocrFactor,ocrImage,default_value,format, control_type,mandatory,is_reference,is_unmapped,maxLength,scan,feed,data_type,action_type,form, formname,custom_tag FROM fields where idref=@refid";
                            parameter = new List<SQLiteParameter>();
                            SQLiteParameter refid = new SQLiteParameter("@refid", System.Data.DbType.Int64);
                            refid.Value = id;
                            parameter.Add(refid);
                            ////SQLiteParameter pstatus = new SQLiteParameter("@status", System.Data.DbType.Int16);
                            ////pstatus.Value = status;
                            ////parameter.Add(pstatus);
                            sqlitecmd.Parameters.AddRange(parameter.ToArray());
                            SQLiteDataReader sqlitedrdr = sqlitecmd.ExecuteReader();
                            List<ViewModels.EntityFieldViewModel> flds = new List<ViewModels.EntityFieldViewModel>();
                            List<ViewModels.FormViewModel> frmls = new List<ViewModels.FormViewModel>();
                            while (sqlitedrdr.Read() && model != null)
                            {
                                ViewModels.EntityFieldViewModel fldModel = new ViewModels.EntityFieldViewModel();
                                //fldModel.id = id;
                                fldModel.sr = sqlitedrdr.GetInt32(0);
                                fldModel.fuid = "" + sqlitedrdr.GetValue(1);// sqlitedrdr.GetString(1);
                                fldModel.field_desc = "" + sqlitedrdr.GetValue(2);// sqlitedrdr.GetString(2);
                                fldModel.parent_field_id = "" + sqlitedrdr.GetValue(3);// sqlitedrdr.GetString(3);
                                fldModel.pms_field_name = "" + sqlitedrdr.GetValue(4);// sqlitedrdr.GetString(4);
                                fldModel.pms_field_xpath = "" + sqlitedrdr.GetValue(5);// sqlitedrdr.GetString(5);
                                fldModel.pms_field_expression = "" + sqlitedrdr.GetValue(6);// sqlitedrdr.GetString(6);
                                fldModel.value = "" + sqlitedrdr.GetValue(7);// sqlitedrdr.GetString(7);
                                fldModel.automation_mode = sqlitedrdr.GetInt32(8);
                                fldModel.ocrFactor = Convert.ToDouble(sqlitedrdr.GetValue(9));// sqlitedrdr.GetDouble(9);
                                fldModel.ocrImage = "" + sqlitedrdr.GetValue(10);// sqlitedrdr.GetString(10);
                                fldModel.default_value = "" + sqlitedrdr.GetValue(11);// sqlitedrdr.GetString(11);
                                fldModel.format = "" + sqlitedrdr.GetValue(12);// sqlitedrdr.GetString(12);
                                fldModel.control_type = sqlitedrdr.GetInt16(13);
                                fldModel.mandatory = sqlitedrdr.GetInt16(14);
                                fldModel.is_reference = sqlitedrdr.GetInt16(15);
                                fldModel.is_unmapped = sqlitedrdr.GetInt16(16);
                                fldModel.maxLength = sqlitedrdr.GetInt16(17);
                                fldModel.scan = sqlitedrdr.GetInt16(18);
                                fldModel.feed = sqlitedrdr.GetInt16(19);
                                fldModel.data_type = sqlitedrdr.GetInt16(20);
                                fldModel.action_type = sqlitedrdr.GetInt16(21);
                                fldModel.custom_tag = "" + sqlitedrdr.GetValue(24);  
                                fldModel.entity_id = model.entity_Id;
                                flds.Add(fldModel);
                                if (frmls.Where(x => x.pmspageid.Contains("" + sqlitedrdr.GetValue(22))).FirstOrDefault() == null)
                                {
                                    //form, formname
                                    ViewModels.FormViewModel frm = new ViewModels.FormViewModel();
                                    frm.id = fromId + 1;
                                    frm.entityid = model.entity_Id;
                                    frm.pmspageid = "" + sqlitedrdr.GetValue(22);// sqlitedrdr.GetString(22);
                                    frm.pmspagename = "" + sqlitedrdr.GetValue(23);// sqlitedrdr.GetString(23);                                   
                                    frm.fields = flds;
                                    frmls.Add(frm);
                                    if (frmls.Count > 1)
                                        flds = new List<ViewModels.EntityFieldViewModel>();
                                }
                            }// while (sqlitedrdr.Read() && model != null)
                            if (frmls != null && frmls.Any())
                            {
                                foreach (ViewModels.FormViewModel vfrm in SModel.forms.Where(x => x.Status == 1))
                                {
                                    ViewModels.FormViewModel nForm = frmls.Where(x => x.pmspageid.ToLower() == vfrm.pmspageid.ToLower() && x.pmspagename.ToLower() == vfrm.pmspagename.ToLower()).FirstOrDefault();
                                    if (nForm != null)
                                    {
                                        foreach (ViewModels.EntityFieldViewModel fld in vfrm.fields.Where(x => x.status == 1))
                                        {
                                            if (nForm.fields.Where(t => t.fuid == fld.fuid && fld.entity_id == model.entity_Id).FirstOrDefault() == null)
                                            {
                                                
                                                nForm.fields.Add(new ViewModels.EntityFieldViewModel { fuid = fld.fuid, feed = fld.feed, scan = fld.scan, entity_id = fld.entity_id, is_reference = fld.is_reference, is_unmapped = fld.is_unmapped, control_type = fld.control_type, field_desc = fld.field_desc, sr = fld.sr, status = fld.status, pms_field_expression = fld.pms_field_expression, maxLength = fld.maxLength, default_value = fld.default_value, parent_field_id = fld.parent_field_id, action_type = fld.action_type, data_type = fld.data_type, mandatory = fld.mandatory, format = fld.format, schema_field_name = fld.schema_field_name, pms_field_xpath = fld.pms_field_xpath, pms_field_name = fld.pms_field_name, ocrImage = fld.ocrImage, ocrFactor = fld.ocrFactor, automation_mode = fld.automation_mode, custom_tag= fld.custom_tag });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        frmls.Add(nForm.DeepClone());
                                    }
                                }// Form Level
                                model.forms = frmls;
                            }
                            sqlitedrdr.Close();
                        }
                    }// While
                     // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Table already exists");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
            // return new SQLiteConnection();
        }
        public static int purgeData(Int64 id, DateTime dtFrom, DateTime dtTo)
        {
            dtFrom = dtFrom.Year <= 1900 ? GlobalApp.CurrentLocalDateTime : dtFrom;
            dtTo = dtTo.Year <= 1900 ? GlobalApp.CurrentLocalDateTime.AddDays(-1) : dtTo;
            int retStatus = 0;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();

                    // Purge Fields Data
                    sqlitecmd.CommandText = "delete from reservations where (id=@id OR @id=0) and DATE(transactiondate) between @dtFrom and @dtTo;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pId = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pId.Value = id;
                    parameter.Add(pId);
                    SQLiteParameter pdtFrom = new SQLiteParameter("@dtFrom", System.Data.DbType.Date);
                    pdtFrom.Value = dtFrom.Date;
                    parameter.Add(pdtFrom);
                    SQLiteParameter pdtTo = new SQLiteParameter("@dtTo", System.Data.DbType.Date);
                    pdtTo.Value = dtTo.Date;
                    parameter.Add(pdtTo);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    retStatus = sqlitecmd.ExecuteNonQuery();
                    // Fields
                    sqlitecmd.CommandText = "delete from fields where idref not in (select id from reservations);";
                    retStatus = sqlitecmd.ExecuteNonQuery();

                    // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Purge executed successfully");
                }
            }
            catch (Exception ex)
            {
                return retStatus;
            }
            return retStatus;
            // return new SQLiteConnection();
        }
        public static int updateReservationStatus(Int64 id, int status)
        {

            int retStatus = 0;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();

                    // Purge Fields Data
                    sqlitecmd.CommandText = "update reservations set syncstatus=@syncstatus, transactiondate=@transactiondate where id=@id;";
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pId = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pId.Value = id;
                    parameter.Add(pId);
                    SQLiteParameter pSyncstatus = new SQLiteParameter("@syncstatus", System.Data.DbType.Int16);
                    pSyncstatus.Value = status;
                    parameter.Add(pSyncstatus);
                    SQLiteParameter pTransactiondate = new SQLiteParameter("@transactiondate", System.Data.DbType.DateTime);
                    pTransactiondate.Value = GlobalApp.CurrentLocalDateTime;
                    parameter.Add(pTransactiondate);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    retStatus = sqlitecmd.ExecuteNonQuery();
                    // Fields
                    if (status != (int)UTIL.Enums.APPROVAL_STATUS.NEW_ISSUED)
                    {
                        sqlitecmd.CommandText = "delete from fields where idref=@idref";
                        List<SQLiteParameter> fParams = new List<SQLiteParameter>();
                        SQLiteParameter pidref = new SQLiteParameter("@idref", System.Data.DbType.Int64);
                        pidref.Value = id;
                        fParams.Add(pidref);
                        sqlitecmd.Parameters.AddRange(fParams.ToArray());
                        sqlitecmd.ExecuteNonQuery();
                    }

                    // sqliteConn.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Delete failed, try another time or contact vendor.");
                }
            }
            catch (Exception ex)
            {
                return retStatus;
            }
            return retStatus;
            // return new SQLiteConnection();
        }
        public static ViewModels.MappingViewModel updateSQLiteFullData(Int64 id, ViewModels.MappingViewModel model)
        {
            // ViewModels.MappingViewModel model = new ViewModels.MappingViewModel();
            // List<string> lstFrms = new List<string>();
            int retStatus = 0;
            if (sqliteConn == null)
                sqliteConn = GetSQLiteConnection();
            try
            {
                if (sqliteConn.State != System.Data.ConnectionState.Open)
                    sqliteConn.Open();
                try
                {

                    SQLiteDataReader sqlite_datareader;
                    SQLiteCommand sqlitecmd = sqliteConn.CreateCommand();
                    sqlitecmd.CommandText = "update reservations set syncstatus=@syncstatus where id= @id; ";// where  syncstatus = @syncstatus";                   
                    List<SQLiteParameter> parameter = new List<SQLiteParameter>();
                    SQLiteParameter pId = new SQLiteParameter("@id", System.Data.DbType.Int64);
                    pId.Value = model.uid;
                    parameter.Add(pId);
                    SQLiteParameter psyncstatus = new SQLiteParameter("@syncstatus", System.Data.DbType.Int16);
                    psyncstatus.Value = model.saves_status;
                    parameter.Add(psyncstatus);
                    sqlitecmd.Parameters.AddRange(parameter.ToArray());
                    // sqlitecmd.CommandText = CreateFieldSQL;
                    retStatus = sqlitecmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Update failed");
                }
            }
            catch (Exception ex)
            {
                retStatus = 0;
            }
            return model;
            // return new SQLiteConnection();
        }

    }
}
