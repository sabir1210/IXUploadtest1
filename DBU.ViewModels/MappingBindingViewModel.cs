using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static BDU.UTIL.Enums;

namespace BDU.ViewModels
{
   public class MappingBindingViewModel
    {
        public int sr { get; set; } = 0;
        public Int64 fieldId { get; set; } = 0;
        public string fuid { get; set; } //= (new Guid()).ToString();
        public string p_fuid { get; set; } 
        public int entity_id { get; set; }
        public int entity_type { get; set; }
        public int mode { get; set; } 
        public string entity_name { get; set; } 
        public string pmspagename { get; set; }
        public string pmspageid { get; set; } = "form1";
        public int formid { get; set; }
        public int fieldsr { get; set; }
        public string pms_field_name { get; set; }
        public string pms_field_xpath { get; set; }
        public string parent_field_id { get; set; }
        public int automation_mode { get; set; } = 0;
        public double ocrFactor { get; set; } = 0.3;
        public string ocrImage { get; set; }
        public string table_name { get; set; }
        public string schema_field_name { get; set; }
        public string field_desc { get; set; }
        public int data_type { get; set; } = 1;
        public int control_type { get; set; } = 1;
        public int control_action { get; set; } = 1;
        public int is_unmapped { get; set; } = 1;
        public int is_reference { get; set; } = 0;
        public int undo { get; set; } = 0;
        public int scan { get; set; } = 1;
        public int feed { get; set; } = 1;
        public int maxLength { get; set; }
        public string default_value { get; set; }
        public string fieldformat { get; set; }
        public string value { get; set; }       
        public int fieldmanadatory { get; set; } 
        public int fieldstatus { get; set; }
        [JsonIgnore]
        public string custom_desc { get; set; }
        public string custom_tag { get; set; } = "";
        public int entity_status { get; set; }
        public string sqlExpression { get; set; } = "";
        public string pms_field_expression { get; set; } = "";        
    }
}
