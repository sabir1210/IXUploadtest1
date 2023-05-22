using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BDU.ViewModels
{
   public class  HotelViewModel
    {
        public int id { get; set; } = 1;
        //public int membership_id { get; set; } = 1;
        public string time { get; set; } = "2021-05-25T09:08:17Z";
        public string hotel_code { get; set; } = "servr";
        public string code { get; set; }
        [JsonIgnore]
        public string email { get; set; } 
        public string xpath { get; set; }
        public string pms_application_path_withname { get; set; } = @"..\..\..\..\RobotAutoApp\TestApp\bin\Debug\netcoreapp3.1\TestApp.exe";
        public string system_type { get; set; } = "1";// 1- Desktop,  2- web
        public string login_user { get; set; } = "almas@blazortech.com";
        [JsonIgnore]
        public string user_pwd { get; set; } = "12345678";
        [JsonIgnore]
        public string login_user_name { get; set; } = "Almas Arshad";
        [JsonIgnore]
        public string hotel_name { get; set; }       
        public string version { get; set; }
        [JsonIgnore]
        public int pms_version_id { get; set; } = 0;
        public string name { get; set; }     
        [JsonIgnore]
        public string Access_Token { get; set; } = "@$@$@$@@@@@#@#@";
        public int Status { get; set; } = 1;       
        public int isHotelUser { get; set; } = 1;
        //  "hotel_id": 30,
        public virtual List<MappingViewModel> mappings { get; set; }
        public virtual List<PreferenceViewModel> preferences { get; set; }

    }
}
