using System;
using System.Collections.Generic;
using System.Text;
using BDU.UTIL;
using System.Linq;
using System.Reflection;

namespace BDU.Services
{
    public static class API
    {
        //Data Retrieval
       
        public static string POST_LOGIN = "bdu_login";       
        public static string GET_PMS_ENTITY_FIELD_MAPPINGS = "get_bdu_entities_fields";
        public static string GET_LOG_CONFIGURATIONS_SETTINGS = "get_log_configurations";
        public static string POST_GET_HOTEL_PRESETS = "get_hotel_bdu_presets";
        public static string POST_CMS_DATA = "get_bdu_cms_data";
        public static string GET_USER_DETAILS = "getuserdetail";
        public static string POST_GET_HOTELS_LIST = "get_hotel_list";
        public static string POST_GET_PMS_VERSIONS = "get_pms_versions";
        public static string POST_GET_LOV_DATA = "get_data";
        public static string GET_HOTELS_LIST = "get_hotel_list";
        //Data Post 
        public static string POST_SAVE_PRESETS = "bdu_login";
        public static string POST_SAVE_CMS_DATA = "save_bdu_cms_data";
        public static string POST_SAVE_PMS_PRESETS = "save_hotel_bdu_presets";
        public static string POST_SAVE_ENTITY_FIELD = "save_bdu_entities_fields";
        public static string POST_SAVE_PMS_VERSION = "save_pms_versions";
        public static string POST_SAVE_SETTINGS = "save_data";
        public static string POST_REVERSE_CHECKIN_CHECKOUT = "reverse-booking";
        
        public static string POST_SAVE_APP_LOG = "save_logs";
        public static string POST_LOGOUT = "logout";
      
        // ********************** Default &  Loaded Presets*******************//
        public static ViewModels.HotelViewModel PRESETS { get; set; }
        public static ViewModels.HotelViewModel ClonedPRESETS { get; set; }
        public static ViewModels.ConfigurationAndSettingsViewModel IXSettings { get; set; }
        // ********************** Data Scanned from PMS and Retrieved from CMS for Sync*******************//
        public static List<ViewModels.MappingViewModel> AIData { get; set; }
        public static List<ViewModels.ErrorViewModel> ErrorReferences { get; set; }
        public static List<ViewModels.LOVViewModel> LOVData { get; set; }
        public static List<ViewModels.HotelViewModel> HotelList { get; set; }
        public static List<ViewModels.PMSVersionsBindingViewModel> PMSVersionsList { get; set; }       
        public static string GET_RESPCECTIVE_STATUS(BDU.UTIL.Enums.SYNC_MODE ChangeFor, string status, int pms_version_id = 41, int entityid = (int)UTIL.Enums.ENTITIES.RESERVATIONS)
        {

            string targetStatus = "pending";
           
             if (LOVData != null && LOVData.Count > 0)
            {
                if (ChangeFor == Enums.SYNC_MODE.TO_CMS)
                {
                    if (entityid == (int)UTIL.Enums.ENTITIES.CHECKIN || entityid == (int)UTIL.Enums.ENTITIES.CHECKOUT)
                    {
                        targetStatus = entityid == (int)UTIL.Enums.ENTITIES.CHECKIN ? UTIL.BDUConstants.ENTITY_DEFAULT_CHECKIN_SERVR_STATUS : UTIL.BDUConstants.ENTITY_DEFAULT_CHECKOUT_SERVR_STATUS;
                    }
                    else { 
                    ViewModels.LOVViewModel lov = LOVData.Where(x => x.pms_version_id == pms_version_id && x.status.ToLower() == Convert.ToString(status).ToLower()).FirstOrDefault();
                    if (lov != null)
                        targetStatus = lov.target_status;
                    }
                }
                else {
                    ViewModels.LOVViewModel lov= LOVData.Where(x => x.pms_version_id == pms_version_id && x.target_status.ToLower() == Convert.ToString(status).ToLower()).FirstOrDefault();
                    if (lov != null)
                        targetStatus = lov.status;
                }
            }
            else { 
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
            }
            return targetStatus;
        }
    }
}
