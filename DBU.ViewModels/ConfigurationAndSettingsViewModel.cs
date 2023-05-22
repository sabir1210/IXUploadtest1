using System;
using System.Text.Json.Serialization;

namespace BDU.ViewModels
{
    [Serializable]
    public class ConfigurationAndSettingsViewModel
    {
        public int id { get; set; }
        public int hotel_id { get; set; }
        public string hotel_code { get; set; }
        [JsonIgnore]
        //public string service_url { get; set; } = "https://bduapi.servrhotels.com/";
        public string service_url { get; set; } = "https://staging.bduapi.servrhotels.com/";
        public int ix_engine_version { get; set; } =1;
        public int automation_mode_type { get; set; } = 1;
        public int automation_mode { get; set; } = 1;
        public string app { get; set; }
        public string station_number { get; set; }
        public int sync_interval { get; set; }
        public string ospwd { get; set; }
        public string osusername { get; set; }
        public string custom1 { get; set; }
        public string description { get; set; }
        public string root_path { get; set; }       
        public int status { get; set; } = 1;
    }
}

