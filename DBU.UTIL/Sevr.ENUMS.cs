
using System.ComponentModel;

namespace BDU.UTIL
{
    public static class Enums
    {
        public enum CacheEnums
        {
            UserBasicInfo = 1,
            UserMenu = 2,
        }
        public enum PMS_VERSIONS
        {
            PMS_Desktop = 1,
            PMS_Web = 2,
        }
        public enum SEVERITY_LEVEL
        {
            INFO =1,
            ERROR = 2,
            CRITICAL = 3,
           FAIL = 4
        }
        public enum FIELD_REQUIREMENT_TYPE
        {
            MANDATORY = 1,
            OPTIONAL = 0,
        }
         public enum AUTOMATION_TRAGET_ACTVITY
        {
            FEED = 1,
            SCAN = 2
        }
        public enum AUTOMATION_MODE_TYPE
        {
            APP = 1,
            PROCESS = 1,
        }
        public enum IX_ENGINE_VERSIONS
        {
            V1 = 1,
            V2 = 2,
        }
        public enum AUTOMATION_MODES
        {
            UIAUTOMATION = 1,
            OCR = 2,
            HYBRID = 3,
        }
        public enum PMS_ACTION_REQUIREMENT_TYPE
        {
            REQUIRED = 1,
            NOT_REQUIRED = 0,
        }
        public enum SYNC_MODE
        {
            TO_CMS = 1,
            TO_PMS = 2,
        }
        public enum OS_VERSIONS
        {
            WINDOWS = 1,
            MAC = 2,
        }
        public enum ENTITY_TYPES
        {
            SYNC = 1,
            STATS = 2,
            ACCESS_MNGT = 3,
        }
        public enum ENTITIES
        {
            [Description("Reservations")]
            RESERVATIONS = 1,
            [Description("Check In")]
            CHECKIN = 2,
            [Description("Billing Details")]
            BILLINGDETAILS = 3,
            [Description("Check Out")]
            CHECKOUT = 4,
            [Description("Spas")]
            SPAS = 5,
            [Description("Conceirge")]
            CONCEIRGE = 6,
            [Description("Experiences")]
            EXPERIENCES = 7,
            [Description("Restaurans")]
            RESTAURANS = 8,
            [Description("Access")]
            ACCESS = 9
        }
        public enum CONTROL_TYPES
        {
            TEXTBOX = 1,
            SELECT = 2,
            LISTBOX = 3,
            DATE = 4,
            HIDDEN = 5,
            DATETIME = 6,
            TOGGLE = 7,
            PAGE = 8,
            SWITCH = 9,
            TEL = 10,
            INCREMENT = 11,
            RADIO = 12,
            CHECKBOX = 13,
            RADIO_GROUP = 14,
            LABEL = 15,
            ACTION = 16,
            IMAGE = 17,
            LINKPARTIALTEXT = 18,
            FRAME = 19,
            FORM = 20,
            TAB = 21,
            ANCHOR = 22,
            BUTTON = 23,
            URL = 24,
            NOCONTROL = 25,
            GRID = 26

        }
        public enum ERROR_CODE
        {
            SUCCESS = 200,
            NO_DATA = 201,
            INVALID_DATA = 401,
            ERROR_DATA = 404,
            WARNING = 202,
            FAILED = 501,
        }
        public enum CONTROl_ACTIONS
        {
            SUBMIT = 1,
            CANCEL = 2,
            CLEAR = 3,
            CLOSE = 4,
            LOAD = 5,
            FETCH = 6,
            INPUT = 7,
            Manual_Input = 8,
            SUBMIT_CAPTURE = 9,
            AUTO_INPUT_JSEXPRESSION = 10,
            GET_INPUT_FROM_JSEXPRESSION = 11,
            CLICK = 12,
            INPUT_CONFIRM = 13,
            CLICK_OK = 14,
            CLICK_WAIT = 16,
            INPUT_OK = 15,
            PROCESS_AND_GET = 17,
            INPUT_OPTIONAL = 18,
            INPUT_WAIT = 19,
            SEARCH_AND_CLICK = 20,
            MANDATORY_CONTROL = 21,
            OCR_EXTRACT_RELATIVE = 22,
            XCLICK_RELATIVE = 23,
            HYBRID_ECHO = 24,
            HYBRID_STORE = 25,
            HYBRID_DESKTOP = 26,
            DOUBLE_CLICK = 27,
            HYBRID_WAIT = 28,
            STORE_VALUE = 29,
            HYBRID_IGNORE_ERROR = 30,
            RIGHT_CLICK = 31,
            SHORTCUT_KEY = 32
        }
        public enum DATA_TYPES
        {
            NO_DATA = 0,
            INT = 1,
            TEXT = 2,
            DATE = 3,
            DATETIME = 4,
            ARRAY_INT = 5,
            ARRAY_TEXT = 6,
            BOOL = 7,
            JSON = 8,
            TIME = 9,
            INT_RANGE = 10,
            TEXT_MULTI = 11,
            Double = 12,
            ByeArray = 13
        }

        public enum MESSAGERESPONSETYPES
        {
            OK = 1,
            CONFIRM = 2,
            CANCEL = 3,
        }
        public enum USERROLES
        {
            PMS_Staff = 1,
            Servr_Staff = 2
        }


        public enum LANGUAGES
        {
            ENGLISH = 1,
            URDU = 2,
        }
        public enum SYNC_MESSAGE_TYPES
        {
            COMPLETE = 1,
            INFO = 2,
            ERROR = 3,
            WAIT = 4,
            PMS_SCANNING = 5,
            PUSHING_TO_CMS = 6,
            PUSHING_TO_PMS = 7
        }
        public enum STATUSES
        {
            [Description("Active")]
            Active = 1,
            [Description("Processed")]
            InActive = 0,
            [Description("Deleted")]
            Deleted = 2,
        }
        public enum WebInputTypes
        {
            button = 1,
            checkbox = 2,
            date = 3,
            email = 4,
            file = 5,
            hidden = 6,
            number = 7,
            password = 8,
            radio = 9,
            range = 10,
            search = 11,
            submit = 12,
            tel = 13,
            text = 14,
            time = 15,
            url = 16

        }
        public enum RESPONSE_STATUS
        {
            Pass = 1,
            Fail = 2
        }
        public enum RESERVATION_STATUS
        {
            [Description("ACTIVE")]
            ACTIVE = 1,
            [Description("INACTIVE")]
            INACTIVE = 2,
            [Description("DELETED")]
            DELETED = 3,
            [Description("PROCESSED / SYNCHED")]
            PROCESSED = 6
        }

        public enum ROBOT_UI_STATUS
        {
            FEEDING_DATA = 2,
            ERROR_STATUS = 3,
            SCANNING = 4,
            STOPPED = 5,
            LOADING = 6,
            DEFAULT = 7,
            READY = 8,
            SYNCHRONIZING_WITH_PMS = 9,
            SYNCHRONIZING_WITH_GUESTX = 10,
        }
        public enum PMS_LOGGIN_STATUS
        {
            LOGGED_IN = 1,
            LOGGED_OUT = 2
        }
        public enum WebBrowser
        {
            Chrome = 1,
            IE = 2,
            InternetExplorer = 3,
            Firefox = 4,
            Edge = 5,
            Bravo = 6,
            Safari = 7

        }

        public enum APPROVAL_STATUS
        {

            NEW_ISSUED = 1,
            APPROVED = 2,
            REJECTED = 3,
            DEFFERED = 4,
            CANCELLED = 5,
        }

        public enum LOGIN_ACTIVITY
        {
            LOGGED_IN = 1,
            LOGGED_OUD = 2

        }


    }
}
