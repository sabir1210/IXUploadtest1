using System;
using System.Collections.Generic;

namespace BDU.ViewModels
{
   public class CMSDataViewModel
    {
        public int id { get; set; } = 0;
        public int entity_id { get; set; } 
        public int fieldid { get; set; }
        public string value { get; set; }
        public virtual List<CMSDataViewModel> childData { get; set; }
        
    }
    public class CMSDataEntityViewModel
    {
        public int entity_id { get; set; } = 0;
        public virtual List<CMSDataViewModel> data { get; set; }        

    }
}

