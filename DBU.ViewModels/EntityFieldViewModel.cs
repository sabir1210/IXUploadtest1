using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static BDU.UTIL.Enums;

namespace BDU.ViewModels
{
    [Serializable]
    public class EntityFieldViewModel
    {
        public int sr { get; set; } = 0;
        [JsonIgnore]
        public int id { get; set; } = 0;
   
        [JsonIgnore]
        public string table_name { get; set; }
        public int entity_id { get; set; }
        public string fuid { get; set; }// = (new Guid()).ToString();
        [JsonIgnore]
        public string schema_field_name { get; set; } = "";
        public string field_desc { get; set; }
        public string parent_field_id { get; set; }
        public string pms_field_name { get; set; } 
        public string pms_field_xpath { get; set; } 
        public string pms_field_expression { get; set; } 
        //public int sort_order { get; set; } = 1;
        public string value { get; set; }
        public int automation_mode { get; set; } = 0;
        public double ocrFactor { get; set; } = 0.3;
        public string ocrImage { get; set; }
        //[JsonIgnore]
        //public int save_status { get; set; } = 1;
        public string default_value { get; set; } = "";
        public string format { get; set; } = "";
        [JsonIgnore]
        public string sqlExpression { get; set; } = "";
        //public string expression { get; set; } = "";
        public int control_type { get; set; }
        public int mandatory { get; set; } = 1;
        public int is_reference { get; set; } = 0;
        public int is_unmapped { get; set; } = 0;
        public int maxLength { get; set; }
        public int scan { get; set; } = 1;
        public int feed { get; set; } = 1;
        public int status { get; set; } = 1;
        [JsonIgnore]
        public string custom_desc { get; set; }     
        public string custom_tag { get; set; }
        public int data_type { get; set; }
        public int action_type { get; set; }
        public virtual List<EntityFieldViewModel> Fields { get; set; }

    }
}
