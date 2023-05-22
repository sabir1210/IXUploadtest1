using System;

namespace DBU.ViewModels
{
   public class  HotelViewModel
    {
        public int ID { get; set; } = 1;
        public string hotel_code { get; set; } = "servr";
        public string login_user { get; set; } = "almas@blazortech.com";
        public string user_pwd { get; set; } = "12345678";
        public string login_user_name { get; set; } = "Almas Arshad";
        public int user_detail_id { get; set; } = 1290;
        public string hotel_name { get; set; } = "Servr Hotel";
        public int Status { get; set; } = 1;
      
    }
}
