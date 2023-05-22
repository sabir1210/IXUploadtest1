using System;

namespace BDU.ViewModels
{
   public class ErrorViewModel
    {
        public int hotel_id{ get; set; }
        public Int64 id { get; set; }
        public string reference { get; set; }
        public int entity_id { get; set; }
        public int mode { get; set; } = 0;       
        public int Status { get; set; } = 1;
        public DateTime time { get; set; } = System.DateTime.Now;
    }
}

