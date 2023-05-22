using System;
using System.Collections.Generic;
using System.Text;

namespace BDU.ViewModels
{
    public class EntityData
    {
        public int hotel_id { get; set; }
        public string time { get; set; } = UTIL.GlobalApp.GetLastSyncTimeWithDifference(UTIL.GlobalApp.SyncTime_CMS).ToString();
        public int mode { get; set; } = 1; // {1- PMG New, 2- PMS Update, 3- CMS New, 4- CMS Update}
        public virtual List<CoreDataEntity> jsonData { get; set; }

    }
    //public class EntityDataFlatModel
    //{
    //    public int hotel_id { get; set; }
    //    public int mode { get; set; } = 1; // {1- PMG New, 2- PMS Update, 3- CMS New, 4- CMS Update}
    //    public int entity_id { get; set; }
    //    public string fuid { get; set; }
    //    public int value { get; set; }

    //}
    public class CoreFieldData
    {
        //public int id { get; set; }
        public string fuid { get; set; }  // field unique id     
        public string value { get; set; }
        //public string desc { get; set; } = "";
        public List<CoreFieldData>  data { get; set; } // its for child heirarchy

    }    
    public class CoreDataEntityRecords
    {
        //public int id { get; set; }
        public string id { get; set; }  // primary which is to identify data of which record
        public string fuid { get; set; }
        public string roomno { get; set; }
        public List<CoreFieldData> data { get; set; }

    }
    public class CoreDataEntity
    {
        public int entity_id { get; set; }
        public List<CoreDataEntityRecords> data { get; set; }
       

    }

}
