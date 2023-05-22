using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BDU.ViewModels
{
    [Serializable]
    public class HybridMappingViewModel
    {
            public string CreationDate { get; set; } = System.DateTime.Now.ToString("yyyy-MM-dd");
            public string Name { get; set; }
            public virtual List<UICommands> Commands { get; set; }
        
    }
    [Serializable]
    public class UICommands
    {
        public string Command { get; set; }
        public string Target { get; set; }
        [JsonIgnore]
        public string factor { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

    }
}

