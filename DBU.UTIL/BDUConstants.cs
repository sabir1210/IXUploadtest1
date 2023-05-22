using System;

namespace BDU.UTIL
{
    public static class BDUConstants
    {
        public static string UPDATED_NOTIFIED_SUCCESS = "<b> {0} </b> is updated & notified via email {1} successfully, at {2}";
        public static string UPDATED_SUCCESS = "<b> {0} </b> is updated successfully, at {1}";
        public static string PWD_UPDATED_SUCCESS = "<b> Password of {0} </b> is updated successfully, at {1}";
        public static string INSERTED_SUCCESS = "<b> {0} </b> is added successfully, at {1}";
        public static string INSERTED_FAILED = "<b> {0} </b> insert is failed,can try again or contact administrator, Error {1}";
        public static string UPDATE_FAILED = "<b> {0} </b> update is failed,can try again or contact administrator, Error - {1}";
        public static string LOGIN_FAILED = "<b> {0} </b>, log In attempt failed,please confirm, username & password is correct, try again or contact DeenScreen administrator, Error - {1}";
        public static string DELETE_FAILED = "Delete of <b> {0} </b> is failed, reference data can be reason, try again or contact administrator, Error {1}";
        public static string DELETE_SUCCESS = "Delete of <b> {0} </b> is completed successfully, at {1}";
        public static string CANCEL_MESSAGE = "Are you sure, you want cancel? ";
        public static string RESET_MESSAGE = "Sure, you want to reload page, all changes will be discarded..? ";
        public static string UPLOAD_INPROGRESS_WAIT_MESSAGE = "Upload is not complete, please first complete upload..!";
        public static string DELETE_CONFIRMATION_MESSAGE = "Are you sure, you want to delete ? ";
        public static string VIDEO_STATUS_ACTIVATE_MESSAGE = "Sure, you want to get back video {0}? ";
        public static string RESERVATION_NOT_FOUND_IN_PMS = "Unfortunately, reservation {0} not found in PMS, please try again, or contact support @servrhotels.com for assistance.";
        public static string API_UNATHENTICATED = "unathenticated";
        public static string API_UNAUTHORIZED = "unauthorized";
        public static string API_SPECIAL_AUTHENTICATION_USER = "agussuarya@gmail.com";
        public static string LOG_SOURCE_GUESTX = "integratex";
        public static string API_SPECIAL_AUTHENTICATION_PWD = "xJNwrJV=GX978";
        public static string INTEGRATEX_LOG_FORMATTED = "dddd, dd MMMM yyyy hh:mm tt";
        public static string INTEGRATEX_SQLITE_CONNECTION_STRING = "Data Source=integratexdb.db; Version = 3; New = True; Compress = True;";
        //public static string FEED_NOT_REQUIRED = "feed=0";
        //public static string SCAN_NOT_REQUIRED = "scan=0";
        //public static string FEED_REQUIRED = "feed=1";// OR NO VALUE
        //public static string SCAN_REQUIRED = "scan=1";// OR NO VALUE

        public static string SPECIAL_KEYWORD_PREFIX = "$";// OR NO VALUE
        public static string SPECIAL_HYBRID_CONSTANTS = "##";// ##
        public static string SPECIAL_HYBRID_NO_FIELD = "LNF";// ##
        public static string HYBRID_EXPRESSION_DATA = "DATA";// ##

        public static string REFERENCE = "Booking Ref";
        public static string RES_REFERENCE = "ResNo";

        // Services 
        public static String[] abc = new string[3] { "Sun", "Mon", "Tue"};// TOTAL_PRICE
        public static string[] PAYMENT_SERVICES_LIST_ALL_ADDITIONAL_SERVICES = new string[6]{ "4abeaa9c-ce8a-4053eb-b5e9-ecb1d75edbb3", "4abeaa9c-ce8a-4073eb-b5e9-ecb1d75edbb3", "4abeaa9c-ce8a-4083eb-b5e9-ecb1d75edbb3", "a5a2628d-bd46-4c8d-9052-c2f903347c3c", "3b7f52b2-aebe-4521-ac97-ce4310a14955", "4abeaa9c-ce8a-4103eb-b5e9-ecb1d75edbb3" };
        public static string PAYMENT_SERVICES_BILL_PAYMENT =  "4abeaa9c-ce8a-11eb-b5e9-ecb1d75edbb3" ;
        public static string PAYMENT_SERVICES_BILL_PAYMENT_DESC = "b35556f2-4b09-41571-b3b3-8dafc5d0a06a";
        public static string ADDITIONAL_SERVICES_AMOUNT = "4abeaa9c-ce8a-4103eb-b5e9-ecb1d75edbb3";
        //public static string[] PAYMENT_SERVICES_LIST_CHILDS = new string[5] { "3b7f52b2 - aebe - 4521 - ac97 - ce4310a14955", “Element 2”, “Element 3”, “Element 4”, “Element 5” };


        public static string RES_REFERENCE_WITH_SPACE = "Res No";
        public static string GUEST_NAME = "Guest Name";
        public static string NEW_GUEST_NAME = "First Name";
        public static string GUEST_NAME_FIELD = "Guest Name Field";
        public static string ROOM_NO = "RmNo";// OR NO VALUE
        public static string NEW_ROOM_NO = "Room Number";// OR NO VALUE
        public static string BILL_DESCRIPTION_FIELD_DESC = "Bill Description";// OR NO VALUEBill Description
        public static string BILL_AMOUNT_FIELD_DESC = "total_price";// OR NO VALUE
        public static string BILL_DESC_FIELD = "Amount";
        // Bill Amount

        public static string REFERENCE_KEYWORD = "$reference";
        public static string CUSTOM_TAG_EBILLS_KEYWORD_ROOT = "#ebill";
        public static string CUSTOM_TAG_EBILLS_KEYWORD_SECOND_LEVEL = "#ebill#expense";
        public static string CUSTOM_TAG_ETAX_KEYWORD_ROOT = "#etax";
        public static string CUSTOM_TAG_ETAX_KEYWORD_SECOND_LEVEL = "#etax#eitems";
        public static string CUSTOM_TAG_PBILL_KEYWORD_ROOT = "#pbill";
        public static string CUSTOM_TAG_PBILL_KEYWORD_SECOND_LEVEL = "#pbill#pitems";
        public static string GUEST_NAME_KEYWORD = "$gname";
        public static string BILL_AMOUNT_KEYWORD = "$billamount";
        public static string BILL_DESC_KEYWORD = "$billdescription";
        public static string ENTITY_DEFAULT_CHECKIN_SERVR_STATUS = "active";
        public static string ENTITY_DEFAULT_CHECKOUT_SERVR_STATUS = "checked_out";
        public static string ROOM_NO_KEYWORD = "$rmno";// OR NO VALUE
        public static string UNDU_FIELD_NAME = "Undo";
       // public static string ROOM_NO_KEYWORD = "$rmno";// OR NO VALUE
        public static string SPECIAL_KEYWORD_SCAN = "scan";// OR NO VALUE
        public static string SPECIAL_KEYWORD_FEED = "feed";// OR NO VALUE
        public static string EXPRESSION_KEYWRODS_DELIMETER = ";";// OR NO VALUE
        public static string EXPRESSION_VALUE_DELIMETER = "=";// OR NO VALUE
        public static string EXPRESSION_VALUE_NOT_EQUAL_DELIMETER = "!=";
        public static string SCAN_EXPRESSION_VALUE_DELIMETER = "$=";// OR NO VALUE
        public static string SCAN_EXPRESSION_VALUE_NOT_EQUAL_DELIMETER = "$!=";

        public static string SCAN_EXPRESSION_AND_OPERATOR_DELIIMETER = "&&";

        public static string SCAN_EXPRESSION_OR_OPERATOR_DELIIMETER = "||";

        // Hybrid
        public static string Hybrid_Automation_Content_Folder = "macros";
        public static string Hybrid_Automation_MediaFiles = "images";
        public static string Hybrid_Automation_Screenshots = "screenshots";

        public static string Hybrid_Automation_UIVision_CLI = "ui.vision.html";
        public static string Hybrid_Automation_Node_CustomCLi = "blazorclix.js";
        public static string Hybrid_Automation_Shell_CustomCLi = "clix.ps1";
        public static string Hybrid_Automation_Chrome_Driver = "chrome.exe";
        public static int AUTO_SYNC_PROCESS_WAIT_INTERVAL = 800;
        public static int AUTOMATION_FIND_REF_CONTROL_WAIT_SECS = 180;
        public static int AUTOMATION_COMMON_DELAY_WEB_MSECS = 800;
        public static int AUTOMATION_FIND_CONTROL_WAIT_SECS = 30;

    }
}
