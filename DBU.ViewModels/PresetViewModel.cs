using System;
using System.Collections.Generic;

namespace BDU.ViewModels
{
   public class PresetViewModel
    {
        public int id { get; set; } = 1;
        public DateTime time { get; set; } 
        public string hotel_code { get; set; } = "servr";
        public int hotel_id { get; set; } 
        public string version { get; set; } 
        public HotelViewModel jsonData { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public int Status { get; set; } = 1;

    }
}
