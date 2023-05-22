using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDU.RobotDesktop
{
   public class ActionMappingViewModel
    {
        public UTIL.Enums.AUTOMATION_MODES Automation_mode { get; set; } = UTIL.Enums.AUTOMATION_MODES.UIAUTOMATION;
        public UTIL.Enums.CONTROl_ACTIONS IX_Action { get; set; } = UTIL.Enums.CONTROl_ACTIONS.CLICK;
        public string command { get; set; } 
        public string target { get; set; }
        public string value { get; set; }
        public Int16 justCommand { get; set; } = 0;

    }
}
