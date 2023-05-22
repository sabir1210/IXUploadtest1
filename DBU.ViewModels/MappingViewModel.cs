using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BDU.UTIL;

namespace BDU.ViewModels
{
    [Serializable]
    public class MappingViewModel
    {
        public Int64 id { get; set; }
        [JsonIgnore]
        public Int64 uid { get; set; }//= (new Random()).Next(100, 10000098);
        [JsonIgnore]
        public DateTime createdAt { get; set; } = UTIL.GlobalApp.CurrentLocalDateTime;
        public int entity_type { get; set; } = 1; // Default Fetch & Post     
        public string pmsformid { get; set; }
        public string roomno { get; set; }
        public int automation_mode { get; set; } = 1;
        public int automation_mode_Type { get; set; } = 1;
        public int automation_engine_version { get; set; } = 1;
        public string xpath { get; set; }
        [JsonIgnore]
        public string entity_name { get; set; }
        [JsonIgnore]
        public string reference { get; set; }
        public int entity_Id { get; set; }
        public int mode { get; set; } = 1; // {1- PMG New, 2- PMS Update, 3- CMS New, 4- CMS Update}
        public int status { get; set; }
        [JsonIgnore]
        public int saves_status { get; set; }
        [JsonIgnore]
        public int undo { get; set; } = 0;
        public virtual List<FormViewModel> forms { get; set; }
        public virtual List<fieldData> data { get; set; }
        public MappingViewModel DCopy()
        {
            return (MappingViewModel)this.DeepCopyByExpressionTree();
        }
        //public MappingViewModel Clone()
        //{
        //    return (MappingViewModel)this.MemberwiseClone();
        //}


    }
    [Serializable]
    public class FormViewModel
    {
        public int id { get; set; }
        [JsonIgnore]
        public int entityid { get; set; } = 1; // Default Fetch & Post     
        public string pmspageid { get; set; }
        public int sort_order { get; set; } = 1;
        public string pmspagetitle { get; set; }
        public string pmspagename { get; set; }
        public string xpath { get; set; }
        [JsonIgnore]
        public string expression { get; set; }
        [JsonIgnore]
        public int mandatory { get; set; } = 1;
        public int Status { get; set; } = 1;
        public List<EntityFieldViewModel> fields { get; set; }



    }
    [Serializable]
    public class MappingDefinitionViewModel
    { 
        public string pmsformid { get; set; }
        public string xpath { get; set; }
        public int entity_Id { get; set; }
        public  string SubmitCaptureFieldXpath { get; set; }
        public string SubmitCaptureFieldExpression { get; set; }
        public string SubmitCaptureFieldId { get; set; }
        public MappingDefinitionViewModel DCopy()
        {
            return (MappingDefinitionViewModel)this.DeepClone();
        }


    }
    [Serializable]
    public class fieldData
    {
        public string fuid { get; set; }
        public string value { get; set; }  // Default Fetch & Post     
       
        public List<fieldData> childData { get; set; }



    }
}
