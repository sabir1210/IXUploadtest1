using System;
using System.Collections.Generic;

namespace BDU.ViewModels
{
   public class PMSVersionViewModel
    {
        public int id { get; set; } = 1;
        //public int membership_id { get; set; } = 1;
        public string version { get; set; }
        public int Status { get; set; } = 1;      
        public  List<MappingViewModel> jsonData { get; set; }
      

    }
    public class PMSPaymentAndTaxes    { 
        public string paymenthead { get; set; }
        public string value { get; set; }
        public Int16 istax { get; set; } = 0;
        public Int16 ispayable { get; set; } = 0;

    }
    public class PMSVersionServiceViewModel
    {
        public int id { get; set; } = 1;
        //public int membership_id { get; set; } = 1;
        public string version { get; set; }
        public int Status { get; set; } = 1;
        public string jsonData { get; set; }


    }
}
