using System;

namespace BDU.ViewModels
{
   public class ResponseViewModel
    {
        public bool status { get; set; } = false;
        public string access_token { get; set; }
        public string status_code { get; set; } = "200";
        public string message { get; set; }
        public int recordsCount { get; set; }
        public object jsonData { get; set; }
    }
    public class ServiceResponseViewModel
    {
        public int status { get; set; } = 0;
        public string access_token { get; set; }
        public string status_code { get; set; } = "200";
        public string message { get; set; }
        public string version { get; set; }
        public string id { get; set; }
        public object jsonData { get; set; }
    }
    public class ServiceConfigurationResViewModel
    {           
        public string status_code { get; set; } = "200";
        public string message { get; set; }
        public string utctime { get; set; }     
        public object jsonData { get; set; }
    }
    //public class LoginResponseViewModel
    //{
    //    public string access_token { get; set; } 
    //    public int hotel_id { get; set; } = 0;
    //    public string message { get; set; }
    //    public string status_code { get; set; }
    //    public object data { get; set; }
    //}
}
