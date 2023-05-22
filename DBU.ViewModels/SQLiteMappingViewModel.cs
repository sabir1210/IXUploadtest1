using System;
using System.Collections.Generic;
using static BDU.UTIL.Enums;

namespace BDU.ViewModels
{
   public class SQLiteMappingViewModel
    {
        public Int64 id { get; set; }
        public string reference { get; set; }
        public int mode { get; set; }
        public DateTime arrivaldate { get; set; }
        public DateTime departuredate { get; set; }
        public DateTime receivetime { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string voucherno { get; set; }
        public Double paymentamount { get; set; }
        public Double payableamount { get; set; }
        public DateTime transactiondate { get; set; }
        public string roomno { get; set; }
        public int syncstatus { get; set; }
        public string email { get; set; }
        public int sr { get; set; } = 0;       
        public string formname { get; set; } //= (new Guid()).ToString();
        public int formidid { get; set; }
        public int entity_id { get; set; }
        public int undo { get; set; } 
        public int entity_type { get; set; }       
        public string entity_name { get; set; }       
        public int formid { get; set; }
            
    }
}
