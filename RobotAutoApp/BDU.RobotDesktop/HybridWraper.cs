using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BDU.UTIL;
using BDU.ViewModels;
using System.Management.Automation;
using System.Threading;
using NLog;

namespace BDU.RobotDesktop
{
    public class HybridWraper
    {
        string responseJson = string.Empty;
        private Logger _log = LogManager.GetCurrentClassLogger();
        MappingActionCommands mCommands = new MappingActionCommands();
        public string HybridAutomationFeedCommands(List<EntityFieldViewModel> cmdFieldls, string automationName, bool forceGenerate = false)
        {
            string jsonFile = Path.Combine(GlobalApp.UIVInstalPath + "\\" + UTIL.BDUConstants.Hybrid_Automation_Content_Folder, automationName + ".json");
            bool fileExists = File.Exists(jsonFile);
            try
            {
                if (cmdFieldls != null && cmdFieldls.Any() && (fileExists && forceGenerate || !fileExists))
                {
                    List<UICommands> uicommnads = new List<UICommands>();
                    HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = automationName, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    if (fileExists && forceGenerate)
                        File.Delete(jsonFile);
                    if (hMapping != null)
                    {
                        List<ActionMappingViewModel> hybridDefaultCommands = mCommands.loadDefaultCommands(Enums.AUTOMATION_MODES.HYBRID);
                        /********* HYBRID Default Commands START*****************/
                        if (hybridDefaultCommands != null && hybridDefaultCommands.Count > 0 && uicommnads != null)
                        {
                            foreach (ActionMappingViewModel dcmd in hybridDefaultCommands)
                            {
                                uicommnads.Add(new UICommands { Command = "" + dcmd.command, Target = dcmd.target, Value = dcmd.value, Description = "" });
                            }
                        }

                        List<ActionMappingViewModel> als = mCommands.LoadAutomationFeedCommands(UTIL.Enums.AUTOMATION_MODES.HYBRID);
                        foreach (EntityFieldViewModel flds in cmdFieldls.OrderBy(x => x.sr))
                        {


                            /**********  Is Reference Field Handling Start**********/
                            if (flds.is_reference == 1)// This is for Refence
                            {
                                UICommands fcmd = new UICommands();
                                fcmd.Command = "OCRExtractRelative";
                                if ((flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.OCR || flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID) && !string.IsNullOrWhiteSpace(flds.ocrImage))
                                {
                                    fcmd.Target = "" + flds.ocrImage + (flds.ocrFactor > 0.0 ? "@" + Math.Round(flds.ocrFactor, 1).ToString() : string.Empty);
                                    fcmd.Value = flds.fuid.Replace("-", "_");
                                }
                                else
                                {
                                    fcmd.Target = string.Empty;// string.IsNullOrWhiteSpace("" + flds.pms_field_xpath) ? "" + flds.pms_field_name : "" + flds.pms_field_xpath;
                                    fcmd.Value = flds.fuid.Replace("-", "_");
                                }
                                fcmd.Description = "";
                                // Add Command
                                uicommnads.Add(fcmd);
                                /******** ECHO**********/

                                UICommands ecmd = new UICommands();
                                ecmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO).command.ToString();
                                ecmd.Target = string.Format("##{0}:${1}##{2}", flds.fuid, "{" + "" + flds.fuid.Replace("-", "_") + "}", flds.field_desc);
                                ecmd.Value = string.Empty;
                                ecmd.Description = "";
                                uicommnads.Add(ecmd);
                            }
                            else
                            {
                                /**********  Is Reference Field Handling END**********/
                                UICommands fcmd = new UICommands();
                                ActionMappingViewModel sourceCommand = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, (UTIL.Enums.CONTROl_ACTIONS)flds.action_type);
                                string fieldValue = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + flds.value;
                                fcmd.Command = sourceCommand.command.ToString();
                                if ((flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.OCR || flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID) && !string.IsNullOrWhiteSpace(flds.ocrImage))
                                {
                                    fcmd.Target = "" + flds.ocrImage + (flds.ocrFactor > 0.0 ? "@" + Math.Round(flds.ocrFactor, 1).ToString() : string.Empty);
                                    fcmd.Value = Convert.ToBoolean(sourceCommand.justCommand) && !string.IsNullOrWhiteSpace(sourceCommand.value) ? "" + sourceCommand.value : string.Empty;
                                }
                                else
                                {
                                    fcmd.Target = string.IsNullOrWhiteSpace("" + flds.pms_field_xpath) ? "" + flds.pms_field_name : "" + flds.pms_field_xpath;
                                    fcmd.Value = Convert.ToBoolean(sourceCommand.justCommand) ? (string.IsNullOrWhiteSpace(sourceCommand.value) ? string.Empty : sourceCommand.value) : this.GetGXValueForInput(flds, fieldValue);
                                }
                                fcmd.Description = "" + flds.pms_field_expression;
                                uicommnads.Add(fcmd);

                                if (flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT || flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT)
                                {
                                    UICommands fcmdw = new UICommands();
                                    // ActionMappingViewModel sourceCommandw = mCommands.findAutomationCommandScan(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.HYBRID_WAIT);
                                    ActionMappingViewModel sourceCommandw = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.HYBRID_WAIT);
                                    fcmdw.Command = sourceCommandw.command.ToString();
                                    fcmdw.Target = "" + sourceCommandw.target;
                                    fcmdw.Value = "";
                                    // fcmd.Value = flds.fuid;// string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? flds.default_value : flds.value;
                                    fcmdw.Description = "";
                                    uicommnads.Add(fcmdw);
                                }
                                /************** For Output ****************/
                                if (flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.GRID || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATETIME || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATE || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.RADIO || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.SELECT || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.CHECKBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEL)
                                {
                                    //  Clear Control before new Input
                                    if (!string.IsNullOrWhiteSpace(fieldValue))// flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.GRID || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATETIME || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATE || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.RADIO || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.SELECT || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.CHECKBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEL)
                                    {
                                        UICommands emtypCmd = new UICommands();
                                        emtypCmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.INPUT).command.ToString();
                                        emtypCmd.Target = "${KEY_CTRL+KEY_U}";
                                        emtypCmd.Value = "";
                                        emtypCmd.Description = "";
                                        uicommnads.Add(emtypCmd);
                                    }


                                    UICommands scmd = new UICommands();
                                    scmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.INPUT).command.ToString();
                                    scmd.Target = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + this.GetGXValueForInput(flds, flds.value);
                                    scmd.Value = "";
                                    scmd.Description = "";
                                    uicommnads.Add(scmd);
                                }
                            }// Else of Refence

                        }
                    }
                    if (uicommnads != null && uicommnads.Count > 0)
                        hMapping.Commands = uicommnads;
                    else
                        hMapping.Commands = null;

                    using (FileStream stream = File.Open(jsonFile, FileMode.OpenOrCreate))
                    {
                        Task.Run(async () => await writeMacro(hMapping, stream)).Wait();  //this.writeMacro(hMapping, stream);                    
                    }

                    // return JsonSerializer.Serialize(hMapping);
                }
                else if (File.Exists(jsonFile) && !forceGenerate)
                    return jsonFile;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
            return jsonFile;
        }
        public string HybridAutomationScanCommands(List<EntityFieldViewModel> cmdFieldls, string automationName, bool storeAsOutput = true, bool forceGenerate = false)
        {
            string jsonFile = Path.Combine(GlobalApp.UIVInstalPath + "\\" + UTIL.BDUConstants.Hybrid_Automation_Content_Folder, automationName + ".json");
            bool fileExists = File.Exists(jsonFile);
            try
            {
                if (cmdFieldls != null && cmdFieldls.Any() && (fileExists && forceGenerate || !fileExists))
                {
                    List<UICommands> uicommnads = new List<UICommands>();
                    HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = automationName, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    if (fileExists && forceGenerate)
                        File.Delete(jsonFile);
                    if (hMapping != null && cmdFieldls != null && cmdFieldls.Any())
                    {
                        /********** Default Commands************/
                        List<ActionMappingViewModel> hybridDefaultCommands = mCommands.loadDefaultCommandsScan(Enums.AUTOMATION_MODES.HYBRID);
                        /********* HYBRID Default Commands START*****************/
                        if (hybridDefaultCommands != null && hybridDefaultCommands.Count > 0 && uicommnads != null)
                        {
                            foreach (ActionMappingViewModel dcmd in hybridDefaultCommands)
                            {
                                uicommnads.Add(new UICommands { Command = "" + dcmd.command, Target = dcmd.target, Value = dcmd.value });
                            }
                        }
                        /*********Default Commands END***********/

                        List<ActionMappingViewModel> als = mCommands.LoadAutomationScanCommands(UTIL.Enums.AUTOMATION_MODES.HYBRID);
                        foreach (EntityFieldViewModel flds in cmdFieldls.Where(x => x.action_type > 0).OrderBy(x => x.sr))
                        {
                            UICommands fcmd = new UICommands();
                            ActionMappingViewModel sourceCommand = mCommands.findAutomationCommandScan(UTIL.Enums.AUTOMATION_MODES.HYBRID, (UTIL.Enums.CONTROl_ACTIONS)flds.action_type);
                            fcmd.Command = sourceCommand.command.ToString();
                            if ((flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.OCR || flds.automation_mode == (int)UTIL.Enums.AUTOMATION_MODES.HYBRID) && !string.IsNullOrWhiteSpace(flds.ocrImage))
                                fcmd.Target = "" + flds.ocrImage + (flds.ocrFactor > 0.0 ? "@" + Math.Round(flds.ocrFactor, 1).ToString() : string.Empty);
                            else
                                fcmd.Target = "" + flds.pms_field_xpath;

                            fcmd.Value = sourceCommand.justCommand == 1 ? (string.IsNullOrWhiteSpace(sourceCommand.value) ? string.Empty : sourceCommand.value) : "" + flds.fuid.Replace("-", "_");
                            // fcmd.Value = flds.fuid;// string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? flds.default_value : flds.value;
                            fcmd.Description = "" + flds.pms_field_expression;
                            uicommnads.Add(fcmd);
                            if (flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.INPUT_WAIT || flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.CLICK_WAIT)
                            {
                                UICommands fcmdw = new UICommands();
                                ActionMappingViewModel sourceCommandw = mCommands.findAutomationCommandScan(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.HYBRID_WAIT);
                                fcmdw.Command = "" + sourceCommandw.command;
                                fcmdw.Target = "" + sourceCommandw.target;
                                fcmdw.Value = "";
                                // fcmd.Value = flds.fuid;// string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? flds.default_value : flds.value;
                                fcmdw.Description = "";
                                uicommnads.Add(fcmdw);
                            }
                            if (storeAsOutput)
                            {

                                if (flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.GRID || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATETIME || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.DATE || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEXTBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.RADIO || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.SELECT || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.CHECKBOX || flds.control_type == (int)UTIL.Enums.CONTROL_TYPES.TEL)
                                {
                                    if (flds.action_type == (int)UTIL.Enums.CONTROl_ACTIONS.XCLICK_RELATIVE)
                                    {
                                        UICommands xcmd = new UICommands();
                                        xcmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.INPUT).command.ToString();
                                        xcmd.Target = string.IsNullOrWhiteSpace(flds.value) && flds.mandatory == 1 ? "" + flds.default_value : "" + flds.value; ;
                                        xcmd.Value = string.Empty;
                                        xcmd.Description = "";
                                        uicommnads.Add(xcmd);
                                    }
                                    else
                                    {
                                        UICommands ecmd = new UICommands();
                                        ecmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, UTIL.Enums.CONTROl_ACTIONS.HYBRID_ECHO).command.ToString();
                                        ecmd.Target = string.Format("##{0}:${1}##{2}", flds.fuid, "{" + "" + flds.fuid.Replace("-", "_") + "}", flds.field_desc);
                                        ecmd.Value = string.Empty;
                                        ecmd.Description = "";
                                        uicommnads.Add(ecmd);
                                    }
                                }
                            }
                        }
                    }
                    if (uicommnads != null && uicommnads.Count > 0)
                        hMapping.Commands = uicommnads;
                    else
                        hMapping.Commands = null;

                    using (FileStream stream = File.Open(jsonFile, FileMode.OpenOrCreate))
                    {
                        Task.Run(async () => await writeMacro(hMapping, stream)).Wait();  //this.writeMacro(hMapping, stream);
                                                                                          //  var res= task.Result;
                                                                                          // using FileStream createStream = File.Open(jsonFile, FileMode.OpenOrCreate);
                                                                                          // JsonSerializer.SerializeAsync(stream, hMapping);

                        // await createStream.DisposeAsync();
                        //JsonSerializer.Serialize(createStream, hMapping);
                        // stream.DisposeAsync();

                    }
                }
                else if (File.Exists(jsonFile) && !forceGenerate)
                    return jsonFile;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
            return jsonFile;
        }
        public async Task<HybridMappingViewModel> writeMacro(HybridMappingViewModel mapping, FileStream stream)
        {
            await JsonSerializer.SerializeAsync(stream, mapping);
            Thread.Sleep(2000);
            await stream.DisposeAsync();
            // }
            _log.Info("IntegrateX sync started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            return mapping;
        }
        public string HybridAutomationCommands(EntityFieldViewModel field, bool forceGenerate = false)
        {
            //string jsonFile= Path.Combine(GlobalApp.UIVInstalPath, field.fuid, ".json");
            string jsonFile = Path.Combine(GlobalApp.UIVInstalPath + "\\" + UTIL.BDUConstants.Hybrid_Automation_Content_Folder, field.fuid, ".json");
            bool fileExists = File.Exists(jsonFile);
            try
            {
                if (fileExists && forceGenerate || !fileExists)
                {
                    if (fileExists && forceGenerate)
                        File.Delete(jsonFile);
                    // Start For new Automation Script
                    List<UICommands> uicommnads = new List<UICommands>();
                    List<ActionMappingViewModel> hybridDefaultCommands = new List<ActionMappingViewModel>();
                    HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = field.fuid, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    // Load Default fields
                    hybridDefaultCommands = mCommands.loadDefaultCommands(Enums.AUTOMATION_MODES.HYBRID);
                    /********* HYBRID Default Commands START*****************/
                    if (hybridDefaultCommands != null && hybridDefaultCommands.Count > 0 && uicommnads != null)
                    {
                        foreach (ActionMappingViewModel dcmd in hybridDefaultCommands)
                        {
                            uicommnads.Add(new UICommands { Command = "" + dcmd.command, Target = dcmd.target, Value = dcmd.value });
                        }
                    }
                    /********* HYBRID Default Commands END*****************/
                    // Write Field Level Macro
                    if (field != null)
                    {
                        List<ActionMappingViewModel> als = mCommands.LoadAutomationFeedCommands(UTIL.Enums.AUTOMATION_MODES.HYBRID);
                        UICommands fcmd = new UICommands();
                        fcmd.Command = mCommands.findAutomationCommand(UTIL.Enums.AUTOMATION_MODES.HYBRID, (UTIL.Enums.CONTROl_ACTIONS)field.action_type).command.ToString();
                        fcmd.Target = "" + field.pms_field_xpath;
                        fcmd.Value = string.IsNullOrWhiteSpace(field.value) && field.mandatory == 1 ? field.default_value : field.value;
                        fcmd.Description = "" + field.pms_field_expression;
                        uicommnads.Add(fcmd);
                    }
                    if (uicommnads != null && uicommnads.Count > 0)
                        hMapping.Commands = uicommnads;
                    else
                        hMapping.Commands = null;
                    // Macro is ready, now writ ein file
                    string macro = JsonSerializer.Serialize(hMapping);
                    File.WriteAllText(jsonFile, macro);
                }
                else if (File.Exists(jsonFile) && !forceGenerate)
                    return jsonFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return jsonFile;
        }
        public string GetControlValues(EntityFieldViewModel field, bool updateField = true)
        {
            string result = string.Empty;
            try
            {
                if (field != null && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && field.scan == 1 && !string.IsNullOrWhiteSpace(responseJson))
                {
                    // Start For new Automation Script
                    List<string> lines = new List<string>();

                    var linesls = responseJson.Split("\n").Where(x => x.Trim().Contains("[echo]") && x.Trim().Contains(field.fuid)).Select(item => item.Trim()).ToList();
                    // HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = field.fuid, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    // Write Field Level Macro
                    if (linesls != null && linesls.Count > 0)
                    {
                        string strfValue = linesls.Where(x => x.Trim().Contains(field.fuid)).ToList().FirstOrDefault();
                        // Complete string
                        if (strfValue.Contains(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                        {
                            if (strfValue.Contains(field.fuid) && strfValue.Split(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS).Length > 0)
                            {
                                int startIndex = strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) + 1;
                                int lastIndex = strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS);
                                //int strfValue.Length - (startIndex + lastIndex)
                                string elementNode = strfValue.Substring(startIndex, strfValue.Length - (startIndex + lastIndex));
                                string[] elementValues = elementNode.Split(":");
                                if (elementValues != null && elementValues.Length > 1)
                                    result = elementValues[1];
                            }
                            else if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(field.default_value))
                            {
                                result = field.default_value;
                            }
                            else
                            {
                                switch (field.data_type)
                                {
                                    case (int)UTIL.Enums.DATA_TYPES.BOOL:
                                        result = "false";
                                        break;
                                    case (int)UTIL.Enums.DATA_TYPES.DATE:
                                        result = UTIL.GlobalApp.CurrentDateTime.ToString(field.format);
                                        break;
                                    case (int)UTIL.Enums.DATA_TYPES.DATETIME:
                                        result = UTIL.GlobalApp.CurrentDateTime.ToString(field.format);
                                        break;
                                    case (int)UTIL.Enums.DATA_TYPES.INT:
                                        result = "0";
                                        break;
                                    case (int)UTIL.Enums.DATA_TYPES.Double:
                                        result = "0.0";
                                        break;
                                }
                                // result = string.Empty;
                            }
                        }// 
                        else if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(field.default_value))
                            result = field.default_value;
                    }

                }// 
                 // File.WriteAllText(jsonFile, macro);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (updateField)
                field.value = result;
            return result;
        }
        public List<EntityFieldViewModel> GetControlValues(List<EntityFieldViewModel> pFields, string pResponseJson, bool pFillWithValues = false)
        {
            List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
            string result = string.Empty;
            try
            {
                foreach (EntityFieldViewModel field in pFields)
                {
                    result = string.Empty;
                    if (field != null && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && !string.IsNullOrWhiteSpace(pResponseJson))
                    {
                        // Start For new Automation Script
                        List<string> lines = new List<string>();

                        var linesls = pResponseJson.Split("\n").Where(x => x.Trim().Contains("[echo]") && x.Trim().Contains(field.fuid)).Select(item => item.Trim()).ToList();
                        // HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = field.fuid, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                        // Write Field Level Macro
                        if (linesls != null && linesls.Count > 0)
                        {
                            string strfValue = linesls.Where(x => x.Trim().Contains(field.fuid)).ToList().FirstOrDefault();
                            // Complete string
                            if (strfValue.Contains(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                            {
                                if (strfValue.Contains(field.fuid) && strfValue.Split(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS).Length > 0)
                                {
                                    int startIndex = strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) + 2;
                                    int lastIndex = strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS);
                                    //int strfValue.Length - (startIndex + lastIndex)
                                    //strfValue.Substring(8 + 1, ((50 - 1) - 8))
                                    string elementNode = strfValue.Substring(startIndex, (lastIndex - startIndex));
                                    string[] elementValues = elementNode.Split(":");
                                    if (elementValues != null && elementValues.Length > 1)
                                        result = elementValues[1];
                                }// Indepentdent If
                                if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(result) && ("" + result).ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                                {
                                    result = UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD;
                                    errorFields.Add(field);
                                }
                                else if (field.mandatory == 1 && string.IsNullOrWhiteSpace(result) && !string.IsNullOrWhiteSpace(field.default_value))
                                {
                                    result = field.default_value;
                                }
                                else if (string.IsNullOrWhiteSpace(result))
                                {
                                    switch (field.data_type)
                                    {
                                        case (int)UTIL.Enums.DATA_TYPES.BOOL:
                                            result = "false";
                                            break;
                                        case (int)UTIL.Enums.DATA_TYPES.DATE:
                                            result = UTIL.GlobalApp.CurrentDateTime.ToString(field.format);
                                            break;
                                        case (int)UTIL.Enums.DATA_TYPES.DATETIME:
                                            result = UTIL.GlobalApp.CurrentDateTime.ToString(field.format);
                                            break;
                                        case (int)UTIL.Enums.DATA_TYPES.INT:
                                            result = "0";
                                            break;
                                        case (int)UTIL.Enums.DATA_TYPES.Double:
                                            result = "0.0";
                                            break;
                                    }// Switch Case
                                    // result = string.Empty;
                                }// Nested If
                                if (pFillWithValues && !result.ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                                    field.value = result.Trim();

                            }// 
                             //else if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(field.default_value))
                             //  result = field.default_value;
                        }

                    }// 

                    // Process Field Value
                }// Collection Loop
                 // File.WriteAllText(jsonFile, macro);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return errorFields;
        }
        private string GetGXValueForInput(EntityFieldViewModel field, string gxValue)
        {
            string returnStr = gxValue;
            switch (field.data_type)
            {

                case (int)UTIL.Enums.DATA_TYPES.DATE://
                    if (!string.IsNullOrWhiteSpace(gxValue))
                    {
                        try
                        {
                            System.DateTime dt = System.DateTime.Now;//;//UTIL.GlobalApp.CurrentDateTime;
                            try
                            {

                                dt = Convert.ToDateTime(gxValue);//, UTIL.GlobalApp.date_format, System.Globalization.CultureInfo.CurrentCulture);
                                if (dt.Year > 1900)
                                    returnStr = dt.ToString(field.format);
                            }
                            catch (Exception)
                            {
                                returnStr = dt.ToString(field.format);
                            }

                        }
                        catch (Exception) { }
                    }
                    break;
                case (int)UTIL.Enums.DATA_TYPES.DATETIME://
                    if (!string.IsNullOrWhiteSpace(gxValue))
                    {
                        try
                        {
                            System.DateTime dt = System.DateTime.Now;
                            try
                            {

                                dt = Convert.ToDateTime(gxValue);// DateTime.ParseExact(gxValue, UTIL.GlobalApp.date_time_format, System.Globalization.CultureInfo.CurrentCulture);
                                if (dt.Year > 1900)
                                    returnStr = dt.ToString(field.format);
                            }
                            catch (Exception)
                            {
                                returnStr = dt.ToString(field.format);
                            }

                        }
                        catch (Exception) { }
                    }
                    break;
            }// Switch Case
            return returnStr;
        }

        public List<EntityFieldViewModel> ProcessShellBasedHybridResponse(List<EntityFieldViewModel> pFields, string automationItem, bool pFillWithValues = false)
        {
            ResponseViewModel response = new ResponseViewModel();
            List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
            string result = string.Empty;
            int entity_id = 0;

            // EntityFieldViewModel refField = pFields.Where(x => x.is_reference == 1).FirstOrDefault();
            // Boolean automationItemExecution = false;
            string pResponseJson = string.Empty;
            string logFile = string.Format("{0}.{1}", Guid.NewGuid().ToString(), "txt"); //"ReservationScan.txt";
                                                                                         //  string LogfileWithPath = string.Format(@"{0}\{1}.{2}", UTIL.GlobalApp.APPLICATION_DRIVERS_PATH, logFile, "txt");
            string LogfileWithPath = string.Format(@"{0}{1}", UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_LOG_PATH, logFile);
            // string LogfileWithPath = @"C:\Users\zahid.nawaz.BTDOMAIN\Downloads\ReservationScan.txt";
            try
            {
                _log.Info("Hybrid data scan from form started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                /***********EXECUETE Hybrid Command Start*************/
                Int16 executionStatus = 0;
                string error = string.Empty;
                using (var ps = PowerShell.Create())
                {
                    int waitInterval = BDU.UTIL.GlobalApp.IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS > 0 ? BDU.UTIL.GlobalApp.IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS * 1000 : 120000;
                    entity_id = pFields.FirstOrDefault() == null ? 1 : pFields.FirstOrDefault().entity_id;
                    ps.AddScript(System.IO.File.ReadAllText(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_SHELL_WRAPER));
                    ps.AddArgument(automationItem);
                    // ps.AddArgument(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_LOG_PATH);
                    ps.AddArgument(logFile);

                    ps.AddArgument(UTIL.GlobalApp.APPLICATION_DRIVERS_UIVISIONCLI);// @"D:\Shared\shell\ui.vision.html");
                    //var results;
                    var psAsyncResult = ps.BeginInvoke();
                    if (psAsyncResult.AsyncWaitHandle.WaitOne(waitInterval))
                    {
                        // Execution finished
                        var results = ps.EndInvoke(psAsyncResult);
                        var outputCollection = results;

                        //string error = string.Empty;
                        // if (outputCollection != null && outputCollection.Any() && System.IO.File.Exists("" + outputCollection[3]))//.Contains("Status=OK"))
                        if (outputCollection != null && outputCollection.Any() && ("" + outputCollection[5]).Contains("Status=OK"))
                        {
                            executionStatus = Convert.ToInt16(string.IsNullOrWhiteSpace("" + outputCollection[1]) ? "-1" : "" + outputCollection[1]);
                            LogfileWithPath = Convert.ToString(outputCollection[3]);
                            error = string.IsNullOrWhiteSpace(Convert.ToString(outputCollection[5])) ? "Hybrid system don't have enough rights! " : Convert.ToString(outputCollection[5]);
                        }
                    }
                    else
                    {
                        // Execution terminated by timeout
                        throw new Exception("Hybrid PMS response timeout!!");
                    }
                    //  DateTime executionTime = System.DateTime.Now;

                    // Zahid Test Injector
                    // LogfileWithPath = @"D:\Shared\Almas\reservation.txt";
                    /************ Process Result*************/
                    if (1 > -1 && System.IO.File.Exists(LogfileWithPath))
                    {
                        string macroOutput = System.IO.File.ReadAllText(LogfileWithPath);
                        if (!string.IsNullOrWhiteSpace(macroOutput) && macroOutput.Contains("Status=OK"))
                        {
                            response.status = true;
                            pResponseJson = macroOutput;
                        }
                        else if (1 != 0)
                        {
                            response.status = false;
                            response.jsonData = errorFields;
                            response.message = string.Format("{0} Hybrid Test failed", automationItem);
                            errorFields.Add(new EntityFieldViewModel { entity_id = entity_id, field_desc = "Hybrid test run failed, check mapping or contact servr support team.", sr = 1, id = 1, parent_field_id = string.Empty, control_type = 1, mandatory = 1, fuid = Guid.NewGuid().ToString() });
                        }
                    }
                    else
                    {
                        response.status = false;
                        response.jsonData = errorFields;
                        response.message = string.Format("{0} Hybrid Test failed", automationItem);
                        errorFields.Add(new EntityFieldViewModel { entity_id = entity_id, field_desc = LogfileWithPath.Contains("terminated") ? "Hybrid test run failed, check mapping or contact servr support team." : error, sr = 1, id = 1, parent_field_id = string.Empty, control_type = 1, mandatory = 1, fuid = Guid.NewGuid().ToString() });
                    }

                }
                /***********EXECUETE Hybrid Command End*************/
                if (response.status)
                {
                    foreach (EntityFieldViewModel field in pFields)
                    {
                        result = string.Empty;
                        if (field != null && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && !string.IsNullOrWhiteSpace(pResponseJson))
                        {
                            // Start For new Automation Script
                            List<string> lines = new List<string>();

                            var linesls = pResponseJson.Split("\n").Where(x => x.Trim().Contains("[echo]")).Select(item => item.Trim()).ToList();
                            // HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = field.fuid, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                            // Write Field Level Macro
                            if (linesls != null && linesls.Count > 0)
                            {
                                string strfValue = linesls.Where(x => x.Trim().Contains(field.fuid)).ToList().FirstOrDefault();
                                // Complete string
                                if (!string.IsNullOrWhiteSpace(strfValue) && strfValue.Contains(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                                {
                                    if (strfValue.Contains(field.fuid) && strfValue.Split(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS).Length > 0)
                                    {

                                        //int strfValue.Length - (startIndex + lastIndex)
                                        //strfValue.Substring(8 + 1, ((50 - 1) - 8))
                                        if (strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) != strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                                        {
                                            int startIndex = strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) + 2;
                                            int lastIndex = strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS);
                                            string elementNode = strfValue.Substring(startIndex, (lastIndex - startIndex));
                                            string[] elementValues = elementNode.Split(":");
                                            if (elementValues != null && elementValues.Length > 1)
                                                result = elementValues[1];
                                        }
                                        else
                                        {
                                            string[] elementValues = strfValue.Split(":");
                                            if (elementValues != null && elementValues.Length > 1)
                                                result = elementValues[1];
                                        }

                                        // Date Formats
                                        switch (field.data_type)
                                        {
                                            case (int)UTIL.Enums.DATA_TYPES.DATE://
                                                if (!string.IsNullOrWhiteSpace(result))
                                                {
                                                    try
                                                    {
                                                        System.DateTime dt = System.DateTime.Now;
                                                        try
                                                        {

                                                            dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                                                            if (dt.Year > 1900)
                                                                result = dt.ToString(UTIL.GlobalApp.date_format);
                                                        }
                                                        catch (Exception)
                                                        {
                                                            dt = DateTime.ParseExact(result, string.IsNullOrWhiteSpace(field.format) ? UTIL.GlobalApp.date_time_format : field.format, System.Globalization.CultureInfo.CurrentCulture);
                                                            if (dt.Year > 1900)
                                                                result = dt.ToString(UTIL.GlobalApp.date_format);
                                                        }
                                                        //try
                                                        //{
                                                        //    dt = DateTime.Parse(result);
                                                        //    if (dt.Year > 1900)
                                                        //        result = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                        //    // result = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                        //}
                                                        //catch (Exception ex)
                                                        //{
                                                        //    dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                                                        //    if (dt.Year > 1900)
                                                        //        result = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                        //}

                                                    }
                                                    catch (Exception) { }
                                                }
                                                break;
                                            case (int)UTIL.Enums.DATA_TYPES.DATETIME:// "text": // Input                                              
                                                if (!string.IsNullOrWhiteSpace(result))
                                                {
                                                    try
                                                    {
                                                        System.DateTime dt = System.DateTime.Now;
                                                        try
                                                        {
                                                            dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                                                            if (dt.Year > 1900)
                                                                field.value = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                        }
                                                        catch (Exception)
                                                        {
                                                            dt = DateTime.ParseExact(result, string.IsNullOrWhiteSpace(field.format) ? UTIL.GlobalApp.date_time_format : field.format, System.Globalization.CultureInfo.CurrentCulture);
                                                            if (dt.Year > 1900)
                                                                result = dt.ToString(UTIL.GlobalApp.date_time_format);
                                                        }
                                                    }
                                                    catch (Exception) { }
                                                }
                                                break;
                                        }

                                        //   END Date Formats

                                    }// Indepentdent If
                                    if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(result) && ("" + result).ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                                    {
                                        result = UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD;
                                        errorFields.Add(field);
                                    }
                                    else if (field.mandatory == 1 && string.IsNullOrWhiteSpace(result) && !string.IsNullOrWhiteSpace(field.default_value))
                                    {
                                        result = field.default_value;
                                    }
                                    else if (string.IsNullOrWhiteSpace(result))
                                    {
                                        switch (field.data_type)
                                        {
                                            case (int)UTIL.Enums.DATA_TYPES.BOOL:
                                                result = "false";
                                                break;
                                            case (int)UTIL.Enums.DATA_TYPES.DATE:
                                                result = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_time_format);
                                                break;
                                            case (int)UTIL.Enums.DATA_TYPES.DATETIME:
                                                result = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_time_format);
                                                break;
                                            case (int)UTIL.Enums.DATA_TYPES.INT:
                                                result = "0";
                                                break;
                                            case (int)UTIL.Enums.DATA_TYPES.Double:
                                                result = "0.0";
                                                break;
                                        }// Switch 

                                    }// Nested If
                                    if (pFillWithValues && !result.ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                                        field.value = result.Trim();

                                }//
                                else if (field.mandatory == 1 && string.IsNullOrWhiteSpace(result) && !string.IsNullOrWhiteSpace(field.default_value))
                                {
                                    result = field.default_value;
                                }


                            }
                            else if (field.mandatory == 1)
                                errorFields.Add(field);

                        }// 

                        // Process Field Value
                    }// Collection Loop
                    /********* Build Response ***************/


                }//First step else part
                _log.Info("Hybrid data scan from form completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
            return errorFields;
        }

        public List<EntityFieldViewModel> ProcessShellBasedHybridFeedResponse(List<EntityFieldViewModel> pFields, string automationItem, bool pFillWithValues = false)
        {
            ResponseViewModel response = new ResponseViewModel();
            List<EntityFieldViewModel> errorFields = new List<EntityFieldViewModel>();
            string result = string.Empty;
            int entity_id = 0;

            // EntityFieldViewModel refField = pFields.Where(x => x.is_reference == 1).FirstOrDefault();
            // Boolean automationItemExecution = false;
            string pResponseJson = string.Empty;
            string logFile = string.Format("{0}.{1}", Guid.NewGuid().ToString(), "txt"); //"ReservationScan.txt";
                                                                                         //  string LogfileWithPath = string.Format(@"{0}\{1}.{2}", UTIL.GlobalApp.APPLICATION_DRIVERS_PATH, logFile, "txt");
            string LogfileWithPath = string.Format(@"{0}{1}", UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_LOG_PATH, logFile);
            // string LogfileWithPath = @"C:\Users\zahid.nawaz.BTDOMAIN\Downloads\ReservationScan.txt";
            try
            {
                _log.Info("Hybrid data scan from form started, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
                /***********EXECUETE Hybrid Command Start*************/
                Int16 executionStatus = 0;
                string error = string.Empty;
                using (var ps = PowerShell.Create())
                {
                    int waitInterval = BDU.UTIL.GlobalApp.IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS > 0 ? BDU.UTIL.GlobalApp.IX_HYBRID_EXECUTION_TIME_INTERVALI_SECS * 1000 : 120000;
                    entity_id = pFields.FirstOrDefault() == null ? 1 : pFields.FirstOrDefault().entity_id;
                    ps.AddScript(System.IO.File.ReadAllText(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_SHELL_WRAPER));
                    ps.AddArgument(automationItem);
                    // ps.AddArgument(UTIL.GlobalApp.APPLICATION_DRIVERS_BLAZOR_LOG_PATH);
                    ps.AddArgument(logFile);

                    ps.AddArgument(UTIL.GlobalApp.APPLICATION_DRIVERS_UIVISIONCLI);// @"D:\Shared\shell\ui.vision.html");
                    //var results;
                    var psAsyncResult = ps.BeginInvoke();
                    if (psAsyncResult.AsyncWaitHandle.WaitOne(waitInterval))
                    {
                        // Execution finished
                        var results = ps.EndInvoke(psAsyncResult);
                        var outputCollection = results;

                        //string error = string.Empty;
                        if (outputCollection != null && outputCollection.Any() && !("" + outputCollection[1]).Contains("erminated"))
                        {
                            executionStatus = Convert.ToInt16(string.IsNullOrWhiteSpace("" + outputCollection[1]) ? "-1" : "" + outputCollection[1]);
                            LogfileWithPath = Convert.ToString(outputCollection[3]);
                            error = string.IsNullOrWhiteSpace(Convert.ToString(outputCollection[5])) ? "Hybrid system don't have enough rights! " : Convert.ToString(outputCollection[5]);
                        }
                    }
                    else
                    {
                        // Execution terminated by timeout
                        throw new Exception("Hybrid PMS response timeout!!");
                    }
                    //  DateTime executionTime = System.DateTime.Now;

                    // Zahid Test Injector
                    // LogfileWithPath = @"D:\Shared\Almas\reservation.txt";
                    /************ Process Result*************/
                    if (1 > -1 && System.IO.File.Exists(LogfileWithPath))
                    {
                        string macroOutput = System.IO.File.ReadAllText(LogfileWithPath);
                        if (!string.IsNullOrWhiteSpace(macroOutput) && macroOutput.Contains("Status=OK"))
                        {
                            response.status = true;
                            pResponseJson = macroOutput;
                        }
                        else if (1 != 0)
                        {
                            response.status = false;
                            response.jsonData = errorFields;
                            response.message = string.Format("{0} Hybrid Test failed", automationItem);
                            errorFields.Add(new EntityFieldViewModel { entity_id = entity_id, field_desc = "Hybrid test run failed, check mapping or contact servr support team.", sr = 1, id = 1, parent_field_id = string.Empty, control_type = 1, mandatory = 1, fuid = Guid.NewGuid().ToString() });
                        }
                    }
                    else
                    {
                        response.status = false;
                        response.jsonData = errorFields;
                        response.message = string.Format("{0} Hybrid Test failed", automationItem);
                        errorFields.Add(new EntityFieldViewModel { entity_id = entity_id, field_desc = LogfileWithPath.Contains("terminated") ? "Hybrid test run failed, check mapping or contact servr support team." : error, sr = 1, id = 1, parent_field_id = string.Empty, control_type = 1, mandatory = 1, fuid = Guid.NewGuid().ToString() });
                    }

                }
                /***********EXECUETE Hybrid Command End*************/
                if (!response.status)
                {
                    response.status = false;
                    response.jsonData = errorFields;
                    response.message = string.Format("{0} Hybrid Test failed", automationItem);
                    errorFields.Add(new EntityFieldViewModel { entity_id = entity_id, field_desc = LogfileWithPath.Contains("terminated") ? "Hybrid test run failed, check mapping and run test again or contact support@servrhotels.com." : error, sr = 1, id = 1, parent_field_id = string.Empty, control_type = 1, mandatory = 1, fuid = Guid.NewGuid().ToString() });
                    //foreach (EntityFieldViewModel field in pFields)
                    //{
                    //    result = string.Empty;
                    //    if (field != null && field.control_type != (int)UTIL.Enums.CONTROL_TYPES.NOCONTROL && !string.IsNullOrWhiteSpace(pResponseJson))
                    //    {
                    //        // Start For new Automation Script
                    //        List<string> lines = new List<string>();

                    //        var linesls = pResponseJson.Split("\n").Where(x => x.Trim().Contains("[echo]")).Select(item => item.Trim()).ToList();
                    //        // HybridMappingViewModel hMapping = new HybridMappingViewModel { Name = field.fuid, CreationDate = System.DateTime.Now.ToString("yyyy-MM-dd") };
                    //        // Write Field Level Macro
                    //        if (linesls != null && linesls.Count > 0)
                    //        {
                    //            string strfValue = linesls.Where(x => x.Trim().Contains(field.fuid)).ToList().FirstOrDefault();
                    //            // Complete string
                    //            if (!string.IsNullOrWhiteSpace(strfValue) && strfValue.Contains(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                    //            {
                    //                if (strfValue.Contains(field.fuid) && strfValue.Split(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS).Length > 0)
                    //                {

                    //                    //int strfValue.Length - (startIndex + lastIndex)
                    //                    //strfValue.Substring(8 + 1, ((50 - 1) - 8))
                    //                    if (strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) != strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS))
                    //                    {
                    //                        int startIndex = strfValue.IndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS) + 2;
                    //                        int lastIndex = strfValue.LastIndexOf(UTIL.BDUConstants.SPECIAL_HYBRID_CONSTANTS);
                    //                        string elementNode = strfValue.Substring(startIndex, (lastIndex - startIndex));
                    //                        string[] elementValues = elementNode.Split(":");
                    //                        if (elementValues != null && elementValues.Length > 1)
                    //                            result = elementValues[1];
                    //                    }
                    //                    else
                    //                    {
                    //                        string[] elementValues = strfValue.Split(":");
                    //                        if (elementValues != null && elementValues.Length > 1)
                    //                            result = elementValues[1];
                    //                    }

                    //                    // Date Formats
                    //                    switch (field.data_type)
                    //                    {
                    //                        case (int)UTIL.Enums.DATA_TYPES.DATE://
                    //                            if (!string.IsNullOrWhiteSpace(result))
                    //                            {
                    //                                try
                    //                                {
                    //                                    System.DateTime dt = System.DateTime.Now;
                    //                                    try
                    //                                    {

                    //                                        dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                    //                                        if (dt.Year > 1900)
                    //                                            result = dt.ToString(UTIL.GlobalApp.date_format);
                    //                                    }
                    //                                    catch (Exception)
                    //                                    {
                    //                                        dt = DateTime.ParseExact(result, string.IsNullOrWhiteSpace(field.format) ? UTIL.GlobalApp.date_time_format : field.format, System.Globalization.CultureInfo.CurrentCulture);
                    //                                        if (dt.Year > 1900)
                    //                                            result = dt.ToString(UTIL.GlobalApp.date_format);
                    //                                    }
                    //                                    //try
                    //                                    //{
                    //                                    //    dt = DateTime.Parse(result);
                    //                                    //    if (dt.Year > 1900)
                    //                                    //        result = dt.ToString(UTIL.GlobalApp.date_time_format);
                    //                                    //    // result = dt.ToString(UTIL.GlobalApp.date_time_format);
                    //                                    //}
                    //                                    //catch (Exception ex)
                    //                                    //{
                    //                                    //    dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                    //                                    //    if (dt.Year > 1900)
                    //                                    //        result = dt.ToString(UTIL.GlobalApp.date_time_format);
                    //                                    //}

                    //                                }
                    //                                catch (Exception) { }
                    //                            }
                    //                            break;
                    //                        case (int)UTIL.Enums.DATA_TYPES.DATETIME:// "text": // Input                                              
                    //                            if (!string.IsNullOrWhiteSpace(result))
                    //                            {
                    //                                try
                    //                                {
                    //                                    System.DateTime dt = System.DateTime.Now;
                    //                                    try
                    //                                    {
                    //                                        dt = DateTime.ParseExact(result, field.format, System.Globalization.CultureInfo.CurrentCulture);
                    //                                        if (dt.Year > 1900)
                    //                                            field.value = dt.ToString(UTIL.GlobalApp.date_time_format);
                    //                                    }
                    //                                    catch (Exception)
                    //                                    {
                    //                                        dt = DateTime.ParseExact(result, string.IsNullOrWhiteSpace(field.format) ? UTIL.GlobalApp.date_time_format : field.format, System.Globalization.CultureInfo.CurrentCulture);
                    //                                        if (dt.Year > 1900)
                    //                                            result = dt.ToString(UTIL.GlobalApp.date_time_format);
                    //                                    }
                    //                                }
                    //                                catch (Exception) { }
                    //                            }
                    //                            break;
                    //                    }

                    //                    //   END Date Formats

                    //                }// Indepentdent If
                    //                if (field.mandatory == 1 && !string.IsNullOrWhiteSpace(result)) //&& ("" + result).ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                    //                {
                    //                    result = UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD;
                    //                    errorFields.Add(field);
                    //                }
                    //                else if (field.mandatory == 1 && string.IsNullOrWhiteSpace(result) && !string.IsNullOrWhiteSpace(field.default_value))
                    //                {
                    //                    result = field.default_value;
                    //                }
                    //                else if (string.IsNullOrWhiteSpace(result))
                    //                {
                    //                    switch (field.data_type)
                    //                    {
                    //                        case (int)UTIL.Enums.DATA_TYPES.BOOL:
                    //                            result = "false";
                    //                            break;
                    //                        case (int)UTIL.Enums.DATA_TYPES.DATE:
                    //                            result = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_time_format);
                    //                            break;
                    //                        case (int)UTIL.Enums.DATA_TYPES.DATETIME:
                    //                            result = UTIL.GlobalApp.CurrentDateTime.ToString(UTIL.GlobalApp.date_time_format);
                    //                            break;
                    //                        case (int)UTIL.Enums.DATA_TYPES.INT:
                    //                            result = "0";
                    //                            break;
                    //                        case (int)UTIL.Enums.DATA_TYPES.Double:
                    //                            result = "0.0";
                    //                            break;
                    //                    }// Switch 

                    //                }// Nested If
                    //                if (pFillWithValues && !result.ToUpper().Contains(UTIL.BDUConstants.SPECIAL_HYBRID_NO_FIELD))
                    //                    field.value = result.Trim();

                    //            }//
                    //            else if (field.mandatory == 1 && string.IsNullOrWhiteSpace(result) && !string.IsNullOrWhiteSpace(field.default_value))
                    //            {
                    //                result = field.default_value;
                    //            }


                    //        }
                    //        else if (field.mandatory == 1)
                    //            errorFields.Add(field);

                    //    }// 

                    //    // Process Field Value
                    //}// Collection Loop
                    ///********* Build Response ***************/


                }//First step else part
                _log.Info("Hybrid data scan from form completed, at " + GlobalApp.CurrentLocalDateTime.ToString(BDUConstants.INTEGRATEX_LOG_FORMATTED));
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw ex;
            }
            return errorFields;
        }

    }
}