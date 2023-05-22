using System;
using System.Text.Json.Serialization;

namespace BDU.ViewModels
{
    [Serializable]
    public class EmailConfigurationsViewModel
    {
        [JsonIgnore]
        public int id { get; set; }
        public string smtpport { get; set; } = "587";
        public string smtpserver { get; set; }       
        public string subjecttechnical { get; set; }
        public string subjectcritical { get; set; }
        public string mailtorevenueteam { get; set; }
        public string mailtotechnicalteam { get; set; }               
        public string mailfrom { get; set; }
        public string smtpuser { get; set; }
        public string smtppassword { get; set; }
        [JsonIgnore]
        public DateTime utctime { get; set; }
        public string enablessl { get; set; }       
    }
}

