using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WinAppDriver.Generation.UiEvents.Models;
using OpenQA.Selenium.Appium.Windows;
using System.Windows.Forms;
using OpenQA.Selenium.Interactions;
using BDU.ViewModels;
using BDU.UTIL;
using static BDU.UTIL.BDUUtil;

namespace AutoApp
{
    class FormHandler
    {
        private MappingViewModel _formMapping = new MappingViewModel();
        public MappingViewModel _formData { get; set; }
        DesktopSession session { get; set; }
        AppiumWebElement root { get; set; }
        //List<AppiumWebElement> elementDict { get; set; }
        Dictionary<string, AppiumWebElement> elementDict { get; set; }
        public string rootFolder
        {
            get
            {
                return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));
            }
        }

        public ICollection<EntityFieldViewModel> FieldsData
        {
            get
            {
                return _formData.forms.FirstOrDefault().fields;
            }
        }
        public FormHandler()
        {
            session = new DesktopSession();
            elementDict = new Dictionary<string, AppiumWebElement>();
            ResetForm();
            CacheElements();
        }
        public void ResetForm()
        {
            _formMapping = FieldJson();
            _formData = _formMapping.DCopy();
            if (_formMapping != null)
            {
                //_formData.xpath = _formMapping.xpath;
                //_formData.submit_action = _formMapping.submit_action;
                //_formData.cancel_action = _formMapping.cancel_actionc;
                root = session.FindElementByAbsoluteXPath(_formMapping.xpath);
                foreach (var field in _formData.forms.FirstOrDefault().fields)
                {
                    field.value = field.default_value;
                }
            }
        }
        private void CacheElements()
        {
            foreach (var field in _formMapping.forms.FirstOrDefault().fields)
            {
                GetCachedElement(root, field.pms_field_name);
            }
        }
        private MappingViewModel FieldJson()
        {
            var fieldsJson = Path.Combine(rootFolder, @"Fields.json");
            string json = File.ReadAllText(fieldsJson);
            var form = JsonConvert.DeserializeObject<MappingViewModel>(json);
            return form;
        }

        #region Set Data To Form

        public bool SetAndSubmit()
        {
            var formData = LoadData();
            var status = false;
            var root = session.FindElementByAbsoluteXPath(formData.xpath);
            if (root != null)
            {
                if (SetData(formData, root))
                {
                    status = true;
                }
            }
            //if (!string.IsNullOrEmpty(formData.submit_action))
            //{
            //    var submitElement = root.FindElementByAccessibilityId(formData.submit_action);
            //    if (submitElement != null)
            //    {
            //        submitElement.Click();
            //    }
            //}
            return status;
        }

        public bool SetData(MappingViewModel formData, AppiumWebElement root)
        {
            Console.WriteLine("* Start *");

            bool bSuccess = false;
            try
            {

                //for (int i = 0; i < formData.fields.Count; i++)
                foreach (var data in formData.forms.FirstOrDefault().fields)
                {
                    try
                    {
                        //AppiumWebElement element = root.FindElementByAccessibilityId(data.fieldname);
                        AppiumWebElement element = GetCachedElement(root, data.pms_field_name);
                        SetElement(element, data.value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //Paste generated code here
                }
                //test complete
                bSuccess = true;
            }
            finally
            {
                //Assert.AreEqual(bSuccess, true);
            }
            Console.WriteLine("* End *");
            return bSuccess;
        }

        private AppiumWebElement GetCachedElement(AppiumWebElement root, string fieldName)
        {
            AppiumWebElement element = null;
            elementDict.TryGetValue(fieldName, out element);
            if (element == null)
            {
                try
                {
                    element = root.FindElementByAccessibilityId(fieldName);
                    elementDict.Add(fieldName, element);
                }
                catch (Exception ex) { }
            }
            return element;
        }
        private MappingViewModel LoadData()
        {
            var fieldsJson = Path.Combine(rootFolder, @"Data.json");
            string json = File.ReadAllText(fieldsJson);
            Console.WriteLine(json);
            Console.Write("Loading Data");
            var data = JsonConvert.DeserializeObject<MappingViewModel>(json);
            return data;
        }

        private string SetElement(AppiumWebElement element, string value)
        {
            string data = "";
            string output = "";
            if (element != null)
            {
                try
                {
                    element.Clear();

                }
                catch (Exception)
                {
                }
            }
            if (element == null)
            {
            }
            else if (element.TagName == "ControlType.Text")
            {
                //element.Clear();
                //element.SendKeys(value);
                Paste(element, value);

            }
            else if (element.TagName == "ControlType.Edit")
            {
                //element.Clear();
                Paste(element, value);
                //element.SendKeys(value);
                //element.SetImmediateValue(value);

            }
            else if (element.TagName == "ControlType.CheckBox")
            {
                element.Clear();
                String checkBoxToggleState = element.GetAttribute("Toggle.ToggleState");
                if (checkBoxToggleState.Equals("0"))
                {
                    element.Click();
                }
            }
            else if (element.TagName == "ControlType.List")
            {
                var values = value.Split(",");
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                var checkBoxes = element.FindElements(By.XPath("*/*"));
                var selCheckBoxes = checkBoxes.Where(c => values.Contains(c.GetAttribute("Name"))).ToArray();
                if (checkBoxes != null && checkBoxes.Count() > 0)
                {

                    SetChildren(selCheckBoxes, values);
                }
                else
                {
                    //var children = element.FindElements(By.XPath("*/*"));
                    //if (children != null && checkBoxes.Count() > 0)
                    //{
                    //    SetChildren(children, value.Split(","));
                    //}
                    //else
                    element.SendKeys(value);
                }
            }
            else if (element.TagName == "ControlType.Spinner")
            {
                var numeric = element.FindElementByTagName("Edit");
                numeric.Clear();
                Paste(numeric, value);
            }
            else if (element.TagName == "ControlType.ComboBox")
            {

                var children = element.FindElements(By.XPath("*/*"));
                if (children.Count() == 2 && children[0].TagName == "ControlType.Spinner" && children[1].TagName == "ControlType.Edit")
                {
                    children[1].Clear();
                    children[1].SendKeys(value);
                }
                //else if(children.Count() > 0)
                //{
                //    SetChildren(children, value.Split(","));
                //}
                else
                {
                    element.Clear();
                    //Paste(element, value);
                    element.SendKeys(value);
                }
            }
            else if (element.TagName == "ControlType.RadioButton")
            {
                var children = element.FindElements(By.XPath("*/*"));
                if (children != null && children.Count() > 0)
                {
                    SetChildren(children, value.Split(","));
                }
                else
                {
                    // LeftClick on RadioButton "Male" at (34,24)
                    String checkBoxToggleState = element.GetAttribute("Toggle.ToggleState");
                    if (checkBoxToggleState != null && value == "True")
                    {
                        element.Click();
                        element.SendKeys(" ");
                    }
                }
            }
            else if (element.TagName == "ControlType.Pane")
            {
                element.Clear();
                //Paste(element, value);
                element.SendKeys(value);
            }
            else if (element.TagName == "ControlType.Document")
            {
                element.Click();
                Paste(element, value);
                //element.SendKeys(value);
            }
            else if (element.TagName == "ControlType.Button")
            {
                element.Click();
                //element.SendKeys(value);
            }
            if (output != string.Empty)
            {
                //AppendTextBox(output);

            }
            else
            {
                //var children = element.FindElements(By.XPath("*/*"));
                //if (children != null && children.Count() > 0)
                //{
                //    foreach (var child in children)
                //    {
                //        CheckElement(child);
                //    }
                //}
            }
            return data;
        }

        private void Paste(AppiumWebElement element, string value)
        {
            System.Windows.Forms.Clipboard.SetText(value);
            element.SendKeys(OpenQA.Selenium.Keys.Control + 'v');
        }
        public List<string> SetChildren(IReadOnlyCollection<AppiumWebElement> children, string[] value)
        {
            var selected = new List<string>();
            foreach (var child in children)
            {
                try
                {
                    if (!child.Displayed)
                    {
                        Actions action = new Actions(child.WrappedDriver);
                        action.MoveToElement(child).Build().Perform();
                    }
                    var t = child.Text;
                    var n = child.GetAttribute("Name");
                    if (value.Contains(t) || value.Contains(n))
                    {
                        //child.Click();
                        child.Click();
                        child.SendKeys(" ");
                        //Actions action = new Actions(child.WrappedDriver);
                        //action.DoubleClick(child);
                        //action.Build().Perform();
                        //action.Perform();
                        //action.SendKeys(" ").Build().Perform();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return selected;
        }
        #endregion

        #region Get Data From Form 
        public void SaveFormData()
        {
            var dataJson = Path.Combine(rootFolder, @"Data.json");

            var json = JsonConvert.SerializeObject(_formData);
            Console.Write(json);
            Console.Write("Data Saved");
            File.WriteAllText(dataJson, json);
        }
        public void ScanRemainingData()
        {
            //var missingField1 = ( from ff in _formMapping.formFields
            //                     join fd in formData.fieldsData on ff.name equals fd.fieldname into f
            //                                 from fd in f.DefaultIfEmpty()
            //                                 where fd.stringvalue == null
            //                                 select ff);

            var missingField = (from ff in _formMapping.forms.FirstOrDefault().fields
                                join fd in _formData.forms.FirstOrDefault().fields on ff.pms_field_name equals fd.pms_field_name
                                where string.IsNullOrEmpty(fd.value)
                                select ff).ToList();
            for (int i = 0; i < missingField.Count; i++)
            {
                var field = missingField[i];
                try
                {
                    var controlData = CheckElement(root, field);
                    var data = _formData.forms.FirstOrDefault().fields.FirstOrDefault(d => d.pms_field_name == field.pms_field_xpath);
                    if (data != null)
                    {
                        data.value = controlData;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                //Paste generated code here
            }
        }
        public bool GetUIFieldData(UiEvent uiEvent)
        {
            if (uiEvent.UiElement.Value != null)
            {
                var fieldName = uiEvent.UiElement.AutomationId;
                var fieldPart = uiEvent.UiElement.AutomationId.Split(".");
                if (fieldPart.Length > 1)
                    fieldName = fieldPart[1];
                var fieldData = _formData.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                var formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                if (formfield != null && fieldData != null)
                {
                    fieldData.value = uiEvent.UiElement.Value;
                    return true;
                }
            }
            return false;
        }

        public FIELD_TYPES GetFieldData(UiEvent uiEvent)
        {
            var type = FIELD_TYPES.UNKNOWN;
            if (uiEvent != null)
            {
                if (uiEvent.UiElement.Value != null)
                {
                    try
                    {
                        var fieldName = string.IsNullOrEmpty(uiEvent.UiElement.AutomationId) ? uiEvent.ParentElement.AutomationId : uiEvent.UiElement.AutomationId;
                        var formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                        if (formfield == null)
                        {
                            fieldName = uiEvent.UiElement.ParentAutomationId;
                            formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                        }
                        var fieldPart = fieldName.Split(".");
                        if (fieldPart.Length > 1)
                            fieldName = fieldPart[1];
                        //if (fieldName == _formMapping.submit_action)
                        //{
                        //    type = FIELD_TYPES.SUBMIT;
                        //}
                        //else if (fieldName == _formMapping.submit_action)
                        //{
                        //    type = FIELD_TYPES.CANCEL;
                        //}
                        else
                        {
                            var fieldData = _formData.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                            if (formfield != null && fieldData != null)
                            {
                                try
                                {
                                    var data = CheckElement(root, formfield);
                                    fieldData.value = data;
                                    type = FIELD_TYPES.DATA;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    { }
                }
            }
            return type;
        }

        public FIELD_TYPES GetFieldType(UiEvent uiEvent)
        {
            var type = FIELD_TYPES.UNKNOWN;
            if (uiEvent != null)
            {
                if (uiEvent.UiElement.Value != null)
                {
                    try
                    {
                        var fieldName = string.IsNullOrEmpty(uiEvent.UiElement.AutomationId) ? uiEvent.ParentElement.AutomationId : uiEvent.UiElement.AutomationId;
                        
                        if (fieldName == _formMapping.entity_name)
                        {
                            type = FIELD_TYPES.SUBMIT;
                        }
                        else if (fieldName == _formMapping.entity_name)
                        {
                            type = FIELD_TYPES.CANCEL;
                        }
                        else
                        {
                            var formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                            if (formfield == null)
                            {
                                fieldName = uiEvent.UiElement.ParentAutomationId;
                                formfield = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
                            }
                            var fieldPart = fieldName.Split(".");
                            if (fieldPart.Length > 1)
                                fieldName = fieldPart[1];
                            if (formfield != null)
                            {
                                    type = FIELD_TYPES.DATA;
                            }
                        }

                    }
                    catch (Exception ex)
                    { }
                }
            }
            return type;
        }
        private string CheckElement(AppiumWebElement root, EntityFieldViewModel field)
        {
            AppiumWebElement element = GetCachedElement(root, field.pms_field_name);
            //AppiumWebElement element = root.FindElementByAccessibilityId(field.name);
            return GetElementData(element, field);
        }
        public void GetElementData(string fieldName)
        {
            AppiumWebElement element = GetCachedElement(root, fieldName);
            var field = _formMapping.forms.FirstOrDefault().fields.FirstOrDefault(f => f.pms_field_name == fieldName);
            MessageBox.Show(GetElementData(element, field));
        }
        private string GetElementData(AppiumWebElement element, EntityFieldViewModel field)
        {
            string data = "";
            string output = "";
            if (element == null)
            {
            }
            else
            {
                var t = element.TagName;
                if (element.TagName == "ControlType.List")
                {
                    var children = element.FindElements(By.XPath("*/*"));
                    //var children1 = element.FindElementsByTagName("CheckBox");// (By.XPath("*/*"));
                    if (children.Count > 0)
                    {
                        var vels = children.Where(c => c.Selected).Select(c => c.GetAttribute("Name")).ToArray();
                        data = string.Join(", ", vels);
                    }
                    else
                    {
                        data = GetFieldData(element, field);
                    }
                }
                else if (element.TagName == "ControlType.Spinner")
                {
                    var child = element.FindElementByTagName("Edit");
                    if (child != null)
                    {
                        data = GetFieldData(child, field);
                    }
                }
                else
                {
                    data = GetFieldData(element, field);
                }
                output = element.TagName + " ( " + element.GetAttribute("AutomationId") + " ) - " + data + Environment.NewLine;
            }
            if (output != string.Empty)
            {
                Console.WriteLine(output);

            }
            else
            {
                //if (element != null)
                //{
                //    var children = element.FindElements(By.XPath("*/*"));
                //    if (children != null && children.Count() > 0)
                //    {
                //        foreach (var child in children)
                //        {
                //            CheckElement(child);
                //        }
                //    }
                //}
            }
            return data;
        }
        private string GetFieldData(AppiumWebElement element, EntityFieldViewModel field)
        {
            PropertyInfo result = typeof(AppiumWebElement).GetProperty(field.pms_field_expression);
            var data = result.GetValue(element).ToString();
            //if (field.data_type == "date" && !string.IsNullOrEmpty(data))
            //{
            //    data = GetDate(data);
            //}
            //else if (field.data_type == "time" && !string.IsNullOrEmpty(data))
            //{
            //    data = GetTime(data);
            //}
            return data.ToString();
        }
        private string GetDate(string data)
        {
            var dt = DateTime.Now;
            DateTime.TryParse(data, out dt);
            var dateStr = dt.ToString("MM/dd/yyyy/");
            return dateStr;
        }

        private string GetTime(string data)
        {
            var dt = DateTime.Now;
            DateTime.TryParse(data, out dt);
            var dateStr = dt.ToString("hh:mm:ss:tt:");
            return dateStr;
        }
        #endregion




        public AppiumWebElement GetFocued()
        {
            try
            {
                ////var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var originalTab = session.desktopSession.SwitchTo();
                //var originalTab1 = session.desktopSession.SwitchTo().Frame("tabPage1");
                //var originalTab2 = session.desktopSession.SwitchTo().Window(session.desktopSession.WindowHandles.First());
                //var e2 = session.DesktopSessionElement.SwitchTo().ActiveElement();
            }
            catch (Exception ex)
            { }
            try
            {
                //var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var currentElement = root.WrappedDriver.SwitchTo().ActiveElement();
            }
            catch (Exception ex)
            { }
            try
            {
                //var e1 = session.desktopSession.SwitchTo().DefaultContent();
                //var e = root.FindElementsByXPath("//*[@focused='true']").FirstOrDefault();
                //return e;
            }
            catch (Exception ex)
            { }
            return null;
        }


    }
}
