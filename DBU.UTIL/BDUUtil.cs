using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Reflection;

namespace BDU.UTIL
{
    public static class BDUUtil
    {
        public enum USERROLES
        {
            [Description("Servr Staff")]
            ServrStaff = 1,
            [Description("Hotel Staff")]
            HotelStaff = 2,
        }
        public enum PMS_TYPES
        {
            [Description("PMS Desktop")]
            PMS_DESKTOP = 1,
            [Description("PMS Web")]
            PMS_WEB = 2,
        }
        public enum COMMON_STATUS_ALL
        {
            [Description("All")]
            ALL = 0
        }
        public enum MESSAGE_RESPONSE_TYPES
        {
            OK = 1,
            CONFIRM = 2,
            CANCEL = 3,
        }

        public enum FIELD_TYPES
        {
            UNKNOWN = 0,
            DATA = 1,
            SUBMIT = 2,
            SUBMIT_AND_SCAN = 4,
            CANCEL = 3,
        }

        public enum PARENT_ENUMS
        {
            CONTENT_CATEGORIES = 1,
            STATUS_VIEDO = 2,
            STATUS_NOTIFICATION = 3,
            STATUS_SUBSCRIBER = 4,
            STATUS_USERS = 5,
            USERROLES = 6,
            USER_TYPES = 7,
            PAYMENT_METHODS = 8,
            RESPONSE_STATUS = 9,
            SUBSCRIPTION_PACKAGES = 10,
            GENDER = 11,
            STATUS_TYPE = 12,
            LOGIN_ACTIVITY = 13,
            MESSAGE_RESPONSE_TYPES = 14,
            TRANSACTION_TYPES = 15,
            COMMON_STATUS = 16
        }
        public enum COMMON_STATUS
        {
            [Description("Active")]
            ACTIVE = 1,
            [Description("In-active")]
            IN_ACTIVE = 2

        }

    }
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
    public static class GlobalApp
    {
        // UI Controls Global Settings
        private static DateTime _currentDate = System.DateTime.UtcNow;  // Backing store
        private static DateTime _lastSyncTime_CMS = System.DateTime.Now;  // Backing store
        private static DateTime _lastSyncBackTime_CMS;
        public static DateTime CurrentDateTime
        {
            get => System.DateTime.UtcNow;
            set => _currentDate = value;
        }
        public static DateTime CurrentLocalDateTime
        {
            get => System.DateTime.Now;
            set => _currentDate = value;
        }
        public static Color Btn_FocusColor = Color.White;
        public static Color Btn_InactiveColor = Color.FromArgb(236, 239, 240);
        public static Color TBX_Border_Color = Color.FromArgb(211, 211, 211);
        //public static Color TBX_Border_Color = Color.FromArgb(51, 51, 51);
        public static Color TBX_Fill_Color = Color.White;
        public static Color Servr_Fill_Color = Color.FromArgb(32, 168, 216);
        public static Color Servr_Cancel_Fill_Color = Color.FromArgb(231, 231, 231);
        // BTN Cancel
        public static Color Btn_Cancel_FocusColor = Color.FromArgb(51, 51, 51);
        public static Color Btn_Cancel_InactiveColor = Color.FromArgb(32, 32, 32);      
        //public static Color Btn_LeftFocusColor = Color.Black;

        // public static Color Btn_White = Color.FromArgb(51, 51, 51);
        public static Color Btn_ServrBlackColor = Color.FromArgb(51, 51, 51);
        //public static Color Btn_Back_LogoColor = Color.FromArgb(90, 185, 234);
        public static Color Btn_Back_LogoColor = Color.White;
        public static Color Btn_Border_Color = Color.White;
        public static string PMS_Version = "0";// its now dealing with version id
        public static string SYSTEM_TYPE = "1";

        //Desktop System Desired Capabilities
        public static string APP_ARGUMENTS = "";
        public static string PLATFORM_NAME = "";
        public static string PROPERTY_MACHINE_NAME = "";
        public static string START_IN = "";
        public static string DEVICE_NAME = "0";// its now dealing with version id
        public static string CUSTOM1 = "";
        public static string CUSTOM2 = "";
        public static string PMS_USER_PWD = "";
        public static string PMS_USER_NAME = "";
        public static string IS_PROCESS = "0";
        public static string STATION_NUMBER = "0";
        public static string STATION_ALLOWED_RECEIPTION_ENTITIES = "0";       
        public static string IX_LAST_MESSAGE = "";

        public static int AUTOMATION_MODE_CONFIG = 1;
        public static int AUTOMATION_MODE_TYPE_CONFIG = 1;
        public static int IX_ENGINE_VERSION_CONFIG = 1;
        public static int IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS= 60;
        public static string TEST_RESERVATION_NO = "";
        public static int IX_SQLITE_DATA_PURGE_DAYS_INTERVAL_DAYS = 15;
        //**************************** Public Get & Sets***************************//
        // public static string JWT_Token = "";
        public static string JWT_Token { get; set; }
        public static string API_URI { get; set; }
        public static DateTime SyncTime_PMS { get; set; }

        public static DateTime SyncTime_CMS
        {
            get => System.DateTime.UtcNow;
            set => _lastSyncTime_CMS = value;
        }
        public static DateTime SyncBackTime_CMS
        {
            get => _lastSyncBackTime_CMS.Year <= 1900 ? System.DateTime.UtcNow : _lastSyncBackTime_CMS;
            set => _lastSyncBackTime_CMS = value;
        }
        // public static DateTime SyncTime_CMS { get; set; } = System.DateTime.Now;//System.DateTime.UtcNow;
        public static double DifferenceinSecs { get; set; }
        public static bool isNew { get; set; } = true;

        public static int User_id { get; set; }
        public static int Hotel_id { get; set; }
        public static int PMS_Version_No { get; set; } = 0;
        public static string UserName = string.Empty;
        public static string User_Pwd { get; set; }
        public static string Hotel_Code = string.Empty;
        public static int AIService_Interval_Secs = 60;
        public static string Hotel_Name = string.Empty;
        public static string UIVInstalPath = string.Empty;
        public static UTIL.Enums.USERROLES login_role { get; set; } = Enums.USERROLES.PMS_Staff;
        public static UTIL.Enums.ROBOT_UI_STATUS currentIntegratorXStatus { get; set; } = UTIL.Enums.ROBOT_UI_STATUS.DEFAULT;
        public static string APPLICATION_DRIVERS_PATH { get; set; } = Path.Combine(Path.GetFullPath(Environment.CurrentDirectory), "Drivers\\");
        public static string APPLICATION_DRIVERS_UIVISIONCLI = APPLICATION_DRIVERS_PATH + UTIL.BDUConstants.Hybrid_Automation_UIVision_CLI.Replace(@"\\", @"\");
        public static string APPLICATION_DRIVERS_BLAZOR_NODE_WRAPER = APPLICATION_DRIVERS_PATH + UTIL.BDUConstants.Hybrid_Automation_Node_CustomCLi.Replace(@"\\", @"\");
        public static string APPLICATION_DRIVERS_BLAZOR_SHELL_WRAPER = APPLICATION_DRIVERS_PATH + UTIL.BDUConstants.Hybrid_Automation_Shell_CustomCLi.Replace(@"\\", @"\");
        public static string APPLICATION_DRIVERS_BLAZOR_LOG_PATH = APPLICATION_DRIVERS_PATH + @"log\";

        public static string APPLICATION_DRIVERS_CHROME_DRIVER = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"; //APPLICATION_DRIVERS_PATH + BDUConstants.Hybrid_Automation_Chrome_Driver.Replace(@"\\", @"\");
        public static string email { get; set; }
        public static string SPECIAL_EMAIL_ACCOUNT { get; set; } = "no-reply@servrhotels.com";
        public static string SPECIAL_EMAIL_ACCOUNT_PWD { get; set; } = "Servrhotels@123";
        public static string PMS_Application_Path_WithName { get; set; } = @"D:\Shared\BDUV3.0\RobotAutoApp\TestApp\bin\Debug\net5.0-windows\TestApp.exe";
        //public static string PMS_Application_Path_WithName { get; set; } = @"D:\Blazor Projects\BDU V2\RobotAutoApp\TestApp\bin\Debug\net5.0-windows\TestApp.exe";
        public static string PMS_WEB_Application_URL { get; set; } = @"http://192.168.18.4:8666/";
        public static string PMS_DESKTOP_PROCESS_NAME { get; set; } = "";
        public static bool Authentication_Done { get; set; } = false;
        public static Object LogEmailSettings { get; set; } = new object();
        public static List<object> errorReservations { get; set; }
        public static string date_time_format { get; set; } = "yyyy-MM-dd HH:mm:ss";
        public static string Hotel_id_GUID { get; set; } = "4abea140-ce8a-11eb-b5e9-ecb1d75edbb3";
        public static string RESERVATION_NOT_FOUND_IN_PMS = "Unfortunately, reservation {0} not found in PMS, please try again, or contact support@servrhotels.com for assistance.";
        // public static List<Ema> PMS_Version_No { get; set; } = 0;
        // Statuses
        public static string Hotel_id_GUID_Reservation_Status { get; set; } = "4abeacd1-ce8a-192eb-ttu9-ecb1d75edbb3";//"4abeacd1-ce8a-11eb-ttu3-ecb1d75edbb3";
        public static string Hotel_id_GUID_CheckOut_Status { get; set; } = "4abeacd1-ce8a-11eb-b5e9-ecb1d75edbb3";
        public static string Hotel_id_GUID_CheckIn_Status { get; set; } = "4abeacd1-ce8a-11eb-ttu9-ecb1d75edbb3";
        public static string Hotel_id_GUID_Billing_Status { get; set; } = "4abeacd1-ce8a-11eb-ttu3-ecb1d75edbb3";// "4abeacd1-ce8a-11eb-b5e9-ecb1d75edbb3";


        public static string date_format { get; set; } = "yyyy-MM-dd";
//        public static PopupNotifier windowNotification()
//        // Notification
//        PopupNotifier pNotify = new PopupNotifier();
//        pNotify.ShowGrip = true;
//          //  pNotify.TitleColor = Color.Black;
//            pNotify.ContentPadding = new Padding(5);
//        pNotify.TitleFont = new Font("Times New Roman", 16.0f);
//        pNotify.Image = winpopups.Properties.Resources.Eye;
//            pNotify.ShowCloseButton = true;
//            pNotify.TitleText = "IntegrateX";
//            // pNotify.Size = new Size(400, 300);
//            pNotify.ContentText = "IntegrateX is happy to run all use cases with higher role!!!";
//            //pNotify.ContentColor = Color.Black;
//            pNotify.Popup();
//}
        public static DataTable dataTypes()
        {
            DataTable dt = new DataTable("DataTypes");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- NO DATA --");
            dt.Rows.Add(1, "INT");
            dt.Rows.Add(2, "TEXT");
            dt.Rows.Add(3, "DATE");
            dt.Rows.Add(4, "DATETIME");
            dt.Rows.Add(5, "ARRAY_INT");
            dt.Rows.Add(6, "ARRAY_TEXT");
            dt.Rows.Add(7, "BOOL");
            dt.Rows.Add(8, "JSON");
            dt.Rows.Add(9, "TIME");
            dt.Rows.Add(10, "INT_RANGE");
            dt.Rows.Add(11, "TEXT_MULTI");
            dt.Rows.Add(12, "Double");
            dt.Rows.Add(13, "ByeArray");

            return dt;
        }
       
        public static DataTable controlTypes()
        {
            DataTable dt = new DataTable("ControlTypes");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT --");
            dt.Rows.Add(1, "Textbox");
            dt.Rows.Add(2, "Select");
            dt.Rows.Add(3, "Listbox");
            dt.Rows.Add(4, "Date");
            dt.Rows.Add(5, "Hidden");
            dt.Rows.Add(6, "DateTime");
            dt.Rows.Add(7, "Toggle");
            dt.Rows.Add(8, "Page");
            dt.Rows.Add(9, "Switch");
            dt.Rows.Add(10, "Tel");
            dt.Rows.Add(11, "Increment");
            dt.Rows.Add(12, "Radio");
            dt.Rows.Add(13, "Checkbox");
            dt.Rows.Add(14, "Radio Group");
            dt.Rows.Add(15, "Label");
            dt.Rows.Add(16, "Action");
            dt.Rows.Add(17, "Image");
            dt.Rows.Add(18, "LinkPartialText");
            dt.Rows.Add(19, "Frame");
            dt.Rows.Add(20, "Form");
            dt.Rows.Add(21, "Tab");
            dt.Rows.Add(22, "Anchor");
            dt.Rows.Add(23, " Button");
            dt.Rows.Add(24, " Url");
            dt.Rows.Add(25, " No Control");
            dt.Rows.Add(26, " Grid");
            return dt;
        }
        public static DataTable controlAction()
        {
            DataTable dt = new DataTable("ControlAction");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT --");
            dt.Rows.Add(1, "Submit");
            dt.Rows.Add(2, "Cancel");
            dt.Rows.Add(3, "Clear");
            dt.Rows.Add(4, "Close");
            dt.Rows.Add(5, "Load");
            dt.Rows.Add(6, "Fetch");
            dt.Rows.Add(7, "Input");
            dt.Rows.Add(8, "Manual Input");
            dt.Rows.Add(13, "Input & Confirm");
            dt.Rows.Add(9, "Submit & Scan");
            dt.Rows.Add(10, "Set Auto JS Input");
            dt.Rows.Add(11, "Get Auto JS Expression");
            dt.Rows.Add(12, "Click");
            dt.Rows.Add(14, "Click With OK");
            dt.Rows.Add(16, "Click & Wait");
            dt.Rows.Add(15, "Input With OK");
            dt.Rows.Add(17, "Process & Get");
            dt.Rows.Add(18, "Input Optional");
            dt.Rows.Add(19, "Input & Wait");
            dt.Rows.Add(20, "Search & Click");
            dt.Rows.Add(21, "Mandatory Control");
            dt.Rows.Add(22, "OCR Extract Relative");
            dt.Rows.Add(23, "XClick Relative");
            dt.Rows.Add(24, "Print"); //HYBRID_ECHO = 24,
           // dt.Rows.Add(25, "Store"); //HYBRID_STORE = 25,
                                      //dt.Rows.Add(26, "Desktop");// HYBRID_DESKTOP = 26,
            dt.Rows.Add(27, "Double Click"); //DOUBLE_CLICK = 27,
            dt.Rows.Add(28, "Hybrid Wait"); //DOUBLE_CLICK = 28,
            dt.Rows.Add(29, "Store Value"); //DOUBLE_CLICK = 29,
            dt.Rows.Add(31, "Right Click"); //DOUBLE_CLICK = 29,
            dt.Rows.Add(32, "Short key"); //DOUBLE_CLICK = 29,
                                            // dt.Rows.Add(30, "XClickRelative"); //DOUBLE_CLICK = 30,

            // DOUBLE_CLICK = 27,
            //  DOUBLE_CLICK = 27,
            //  HYBRID_WAIT = 28
            return dt;
        }
        public static int Get_Entity_Type(int entity_id)
        {
            switch (entity_id)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return 1;
                case 5:
                case 6:
                case 7:
                case 8:
                    return 2;
                case 9:
                    return 3;
            }
            return 0;
        }
        public static string GET_RESPCECTIVE_STATUS(BDU.UTIL.Enums.SYNC_MODE ChangeFor, string status, int pms_version_id)
        {

            string targetStatus = "active";
            if (ChangeFor == Enums.SYNC_MODE.TO_CMS)
            {
                switch (("" + status).ToLower())
                {
                    case "save":
                    case "confirmed":
                        targetStatus = "active";
                        break;
                    case "checked-in":
                        targetStatus = "active";
                        break;
                    case "checked-out":
                        targetStatus = "check_out";
                        break;
                    default:
                        targetStatus = "pending";
                        break;
                }//switch (status.ToLower())
            }
           

            return targetStatus;
        }
        public static DataTable PMSType()
        {
            DataTable dt = new DataTable("PMSType");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT PMS TYPE --");
            dt.Rows.Add(1, "Desktop");
            dt.Rows.Add(2, "Web");
            return dt;
        }
        public static DataTable IX_ENGINE_VERSIONS()
        {
            DataTable dt = new DataTable("IXEngineVersions");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT ENGINE TYPE --");
            dt.Rows.Add(1, "V1");
            dt.Rows.Add(2, "V2");
            return dt;
        }
        public static DataTable AUTOMATION_MODE_TYPE()
        {
            DataTable dt = new DataTable("AUTOMATION_MODE_TYPE");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT AUTOMATION MODE --");
            dt.Rows.Add(1, "APP");
            dt.Rows.Add(2, "PROCESS");
            return dt;
        }
        public static DataTable AUTOMATION_MODES()
        {
            DataTable dt = new DataTable("AUTOMATION_MODES");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            dt.Rows.Add(0, "-- SELECT ENGINE TYPE --");
            dt.Rows.Add(1, "UIAUTOMATION");
            dt.Rows.Add(2, "OCR");
            dt.Rows.Add(3, "HYBRID");
            return dt;
        }
        public static DataTable MappingType()
        {
            DataTable dt = new DataTable("MappingType");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            //dt.Rows.Add(0, "-- SELECT MAPPING TYPE --");
            dt.Rows.Add(9, "PMS Access");
            dt.Rows.Add(1, "Reservations");
            dt.Rows.Add(2, "Check In");
            dt.Rows.Add(3, "Billing Details");
            dt.Rows.Add(4, "Check Out");

            return dt;
        }
       
        public static DataTable Status()
        {
            DataTable dt = new DataTable("Status");
            dt.Columns.Add("id", typeof(Int32));
            dt.Columns.Add("name", typeof(string));
            //dt.Rows.Add(0, "-- SELECT STATUS --");
            dt.Rows.Add(1, "Active");
            dt.Rows.Add(0, "InActive");
            return dt;
        }
        // public static DateTime LAST_SYNC_WITH_DIFFERENCE = SyncTime_PMS.AddSeconds(DifferenceinSecs);
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                try
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start("xdg-open", url);
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        Process.Start("open", url);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public static byte[] CompressProfileImage(MemoryStream ms)
        {
            if (ms == null || ms.Length <= 10) return null;
            Image actual = Image.FromStream(ms);
            // using (MemoryStream ms = new MemoryStream())
            //  System.Drawing.Size size = GetThumbnailSize(actual);           
            using (Image thumbnail = actual.GetThumbnailImage(100, 120, null, new IntPtr()))
            {

                var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                var quality = (long)100; //Image Quality 
                var ratio = new EncoderParameter(qualityEncoder, quality);
                var codecParams = new EncoderParameters(1);
                codecParams.Param[0] = ratio;
                //Rightnow I am saving JPEG only you can choose other formats as well
                var codecInfo = GetEncoder(ImageFormat.Jpeg);


                //   thumbImage.Save(thumbnailPath, codecInfo, codecParams);

                thumbnail.Save(ms, codecInfo, codecParams);
                return ms.ToArray();
            }
        }
        public static string MakeThumbnail(byte[] myImage)
        {
            if (myImage == null || myImage.Length <= 10) return "";
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(myImage)).GetThumbnailImage(100, 100, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string StringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        public static DateTime GetLastSyncTimeWithDifference(DateTime lastSync)
        {
            if (lastSync.Year <= 1900)
            {
                //return SyncTime_PMS.AddSeconds(DifferenceinSecs);
                return System.DateTime.UtcNow.AddSeconds(DifferenceinSecs);
            }
            else
                return System.DateTime.UtcNow.AddSeconds(DifferenceinSecs);
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static List<int> ReceivingEntities()
        {

            var allowedIds = new List<int>() { 1, 2, 3, 4 };
            try
            {
                if (string.IsNullOrWhiteSpace(UTIL.GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES) || UTIL.GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES.Contains("0"))
                {
                    return allowedIds;
                }
                else if (!string.IsNullOrWhiteSpace(UTIL.GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES) && UTIL.GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES.Length >= 1)
                {
                    allowedIds = UTIL.GlobalApp.STATION_ALLOWED_RECEIPTION_ENTITIES.Split(',').Select(int.Parse).ToList();
                    return allowedIds;
                }
            }
            catch (Exception ex) { };
            return allowedIds;


        }
    }
}

