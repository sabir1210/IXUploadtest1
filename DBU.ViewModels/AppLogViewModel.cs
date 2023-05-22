using System;
using System.Text.Json.Serialization;

namespace BDU.ViewModels
{
    [Serializable]
    public class AppLogViewModel
    {
        [JsonIgnore]
        public int id { get; set; }
        public int hotel_id { get; set; } =0;
        public string log_source { get; set; }       
        public string action_user_by { get; set; }
        public string severity { get; set; }
        public string log_detail { get; set; }
        public string log_source_system { get; set; }               
       
        public DateTime created_at
        {
            get => System.DateTime.UtcNow ;           
        }
    }
}

