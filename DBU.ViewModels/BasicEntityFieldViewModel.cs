using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static BDU.UTIL.Enums;

namespace BDU.ViewModels
{
   public class BasicEntityFieldViewModel
    {
        public int sr { get; set; } = 0;      
        public Int64 id { get; set; } = 0;
        public int entity_id { get; set; }
        public int entity_type { get; set; }
        [JsonIgnore]
        public string parent_field_id { get; set; }
        public string uuid { get; set; }// = (new Guid()).ToString();      
        public string field_desc { get; set; }
        //public string parent_field_id { get; set; }
        public string pms_field_name { get; set; } 
        public string pms_field_xpath { get; set; } 
        public string pms_field_expression { get; set; }
        public int automation_mode { get; set; } = 0;
        public double ocrFactor { get; set; } = 0.3;
        public string ocrImage { get; set; }
        //public int sort_order { get; set; } = 1;
        public string value { get; set; } 
        public string default_value { get; set; } = "";
        public string format { get; set; } = "";      
        public int control_type { get; set; }
        public int mandatory { get; set; } = 1;
        public int is_reference { get; set; } = 0;
        public int is_unmapped { get; set; } = 0;
        public int scan { get; set; } = 1;
        public int feed { get; set; } = 1;
        public int status { get; set; } = 1;
        public int data_type { get; set; }
        public int action_type { get; set; }
       

    }
}
