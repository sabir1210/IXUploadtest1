using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDU.RobotDesktop
{
    public class MappingActionCommands
    {
        public  UTIL.Enums.AUTOMATION_MODES AUTOMATION_MODE { get; set; } = UTIL.Enums.AUTOMATION_MODES.UIAUTOMATION;
        public List<ActionMappingViewModel> actions = new List<ActionMappingViewModel>();
        public List<ActionMappingViewModel> sanActions = new List<ActionMappingViewModel>();
        public List<ActionMappingViewModel> LoadAutomationScanCommands(UTIL.Enums.AUTOMATION_MODES mode)
        {
            switch (mode)
            {
                case UTIL.Enums.AUTOMATION_MODES.HYBRID:
                    if (!this.sanActions.Any())
                    {                        
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_IGNORE_ERROR, command = "store", target = "true", value = "!errorignore" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT, command = "XClick", target = "", justCommand = 1, value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "", justCommand = 1, value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", target = "", justCommand=1, value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", target = "", justCommand = 1, value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "XClick", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO, command = "echo", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_STORE, command = "store", target = "medium", value = "!replayspeed" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.DOUBLE_CLICK, command = "XClick", justCommand = 1, target = "", value = "#doubleclick" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_WAIT, command = "pause", target = "1000", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.OCR_EXTRACT_RELATIVE, command = "OCRExtractRelative",  target = "",  value = "" }); // For Scan Only OCR
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.XCLICK_RELATIVE, command = "XClickRelative", target = "", justCommand = 1, value = "" }); // For Scan Only OCR                                                                                                                                                                                                             
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_DESKTOP, command = "XDesktopAutomation", target = "true", value = "" });
                    }

                    break;
                case UTIL.Enums.AUTOMATION_MODES.OCR:
                    if (!this.sanActions.Any())
                    {
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", justCommand = 1, target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "SubmitAndCapture", target = "", value = "" });
                    }

                    break;
                case UTIL.Enums.AUTOMATION_MODES.UIAUTOMATION:
                    if (!this.sanActions.Any())
                    {
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", justCommand = 1, target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", justCommand = 1, target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "", value = "" });
                        sanActions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "SubmitAndCapture", target = "", value = "" });
                    }

                    break;
            }
            return sanActions;

        }// Function Close
        public List<ActionMappingViewModel> LoadAutomationFeedCommands(UTIL.Enums.AUTOMATION_MODES mode) {
            switch (mode) {
                case UTIL.Enums.AUTOMATION_MODES.HYBRID:
                    if (!this.actions.Any())
                    {
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "", justCommand = 1, value = "" });                        
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_IGNORE_ERROR, command = "store", target = "true", value = "!errorignore" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT, command = "XClick", target = "", justCommand = 1, value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", target = "", justCommand = 1, value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", target = "", justCommand = 1, value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "XClick", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO, command = "echo", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_STORE, command = "store", target = "medium", value = "!replayspeed" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.DOUBLE_CLICK, command = "XClick", target = "", justCommand = 1, value = "#doubleclick" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_WAIT, command = "pause", target = "1000", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.XCLICK_RELATIVE, command = "XClickRelative", justCommand = 1, target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_DESKTOP, command = "XDesktopAutomation", target = "true", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.OCR_EXTRACT_RELATIVE, command = "XClickRelative", target = "", justCommand = 1, value = "" }); // For Scan Only OCR
                    }
                 
                    break;
                case UTIL.Enums.AUTOMATION_MODES.OCR:
                    if (!this.actions.Any())
                    {
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", justCommand = 1, target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", justCommand = 1, target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "SubmitAndCapture", target = "" });
                    }

                    break;
                case UTIL.Enums.AUTOMATION_MODES.UIAUTOMATION:
                    if (!this.actions.Any())
                    {
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.CLICK, command = "XClick", target = "", value="" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.FETCH, command = "open", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.LOAD, command = "XRun", target = "" , value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT, command = "XType", justCommand = 1, target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.INPUT, command = "XType", justCommand = 1, target = "" , value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SEARCH_AND_CLICK, command = "ExtractAndWait", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT, command = "ExtractAndWait", target = "", value = "" });
                        actions.Add(new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.SUBMIT_CAPTURE, command = "SubmitAndCapture", target = "", value = "" });
                    }

                    break;
            }
            return actions;
        
        }// Function Close
        public ActionMappingViewModel findAutomationCommand(UTIL.Enums.AUTOMATION_MODES mode,UTIL.Enums.CONTROl_ACTIONS action)
        {
            ActionMappingViewModel cmd = null; 
            if (actions != null && actions.Count <= 0)
                this.LoadAutomationFeedCommands(mode);
             cmd = this.actions.Where(x => x.Automation_mode == mode && x.IX_Action == action).FirstOrDefault();
            if (cmd == null)
                cmd = new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO, command = "echo", target = "", value = "" };
            return cmd;
        }
        public List<ActionMappingViewModel> loadDefaultCommands(UTIL.Enums.AUTOMATION_MODES mode)
        {
            List<ActionMappingViewModel> cmdls = new List<ActionMappingViewModel>();
            if (actions != null && actions.Count <= 0)
                this.LoadAutomationFeedCommands(mode);
            cmdls = this.actions.Where(x => x.Automation_mode == mode && (x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_IGNORE_ERROR || x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_DESKTOP || x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_STORE)).ToList();

            return cmdls;
        }

        public ActionMappingViewModel findAutomationCommandScan(UTIL.Enums.AUTOMATION_MODES mode, UTIL.Enums.CONTROl_ACTIONS action)
        {
            ActionMappingViewModel cmd = null;
            if (sanActions != null && sanActions.Count <= 0)
                this.LoadAutomationScanCommands(mode);
            cmd = this.sanActions.Where(x => x.Automation_mode == mode && x.IX_Action == action).FirstOrDefault();
            if (cmd == null)
                cmd = new ActionMappingViewModel { Automation_mode = mode, IX_Action = UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO, command = "echo", target = "", value = "" };
            return cmd;
        }
        public List<ActionMappingViewModel> loadDefaultCommandsScan(UTIL.Enums.AUTOMATION_MODES mode)
        {
            List<ActionMappingViewModel> cmdls = new List<ActionMappingViewModel>();
            if (sanActions != null && sanActions.Count <= 0)
                this.LoadAutomationScanCommands(mode);
            cmdls = this.sanActions.Where(x => x.Automation_mode == mode && (x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_IGNORE_ERROR || x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_DESKTOP || x.IX_Action == UTIL.Enums.CONTROl_ACTIONS.HYBRID_STORE)).ToList();

            return cmdls;
        }
        
    }// Class

    
}


