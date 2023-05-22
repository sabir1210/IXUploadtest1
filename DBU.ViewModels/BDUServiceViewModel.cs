using System;
using System.Collections.Generic;

namespace BDU.ViewModels
{
   public class BDUServiceViewModel
    {
        public int hotel_id { get; set; }
        public int entity_id { get; set; }
        public string date_from { get; set; }
    }
    public class login_Filters
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class Fetch_CMS_Data_Filters
    {
        public int hotel_id { get; set; }
        //public int entity_id { get; set; }
        public string time { get; set; }
        //public string time { get; set }
    }
    public class Save_CMS_Data_Filters
    {
        public int hotel_id { get; set; }
        public DateTime time { get; set; }
        public int created_by { get; set; }
        public DateTime created_at { get; set; }
        public List<CoreDataEntity> jsonData { get; set; }

    }
    public class Hotel_Filters
    {
        public string version { get; set; }
        public int hotel_id { get; set; }
        public int status { get; set; }

    }
    public class API_Response
    {
        public string error_messgage { get; set; }
        public string error_code { get; set; }
        public object data { get; set; }

    }
    public class pms_data_filters
    {
        public int id { get; set; } = 0;       

    }

    public class pms_lov_filters
    {
        public string table { get; set; } = "bdu_status_mappings";
        public string select { get; set; } = "hotel_id,status,target_status";       

    }
}
