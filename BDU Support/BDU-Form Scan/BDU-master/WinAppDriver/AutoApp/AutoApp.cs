using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
//using System.Threading;
using OpenQA.Selenium.Appium;
using System.Diagnostics;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Reflection;
using OpenQA.Selenium.Interactions;

namespace AutoApp
{
    public partial class AutoApp : Form
    {
        public AutoApp()
        {
            InitializeComponent();
            StartServer();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
            //var root = tbRoot.Text;
            //tbOutput.Text = "";
            //Thread thread = new Thread(() => GetData());
            //thread.Start();
            //Process();
        }
        private void btnWeb_Click(object sender, EventArgs e)
        {
            SetData();
            //var root = tbRoot.Text;
            //tbOutput.Text = "";
            //Thread thread = new Thread(() => SetData());
            //thread.Start();
            //Process();
        }

        private void GetData()
        {
            AppendTextBox("* Load CMS API data int PMS - Start *" + Environment.NewLine);

            DesktopSession session = new DesktopSession();

            bool bSuccess = false;
            try
            {
                var form = FieldJson();
                var formData = new FormData();
                formData.formRootXPath = form.formRootXPath;
                formData.fieldsData = new List<FieldData>();
                var root = session.FindElementByAbsoluteXPath(form.formRootXPath);
                if (form != null && form.formFields.Count > 0)
                {
                    //var root = session.FindElementByAbsoluteXPath(form.formRootXPath);
                    for (int i = 0; i < form.formFields.Count; i++)
                    {
                        var field = form.formFields[i];
                        try
                        {
                            var data = CheckElement(root, field);
                            formData.fieldsData.Add(new FieldData { field = field.name, value = data, datatype = field.datatype });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }

                        //Paste generated code here
                    }
                }
                //test complete
                if (formData.fieldsData.Count > 0)
                {
                    SaveData(formData);
                }
                bSuccess = true;
            }
            catch (Exception ex)
            { }
            finally
            {
                Assert.AreEqual(bSuccess, true);
            }
            AppendTextBox("* End *" + Environment.NewLine);

        }
        private void SetData()
        {
            AppendTextBox("* Start Save data for CMS API*" + Environment.NewLine);

            DesktopSession session = new DesktopSession();
            bool bSuccess = false;
            try
            {
                var formData = LoadData();
                var root = session.FindElementByAbsoluteXPath(formData.formRootXPath);
                for (int i = 0; i < formData.fieldsData.Count; i++)
                {
                    var data = formData.fieldsData[i];
                    try
                    {
                        AppiumWebElement element = root.FindElementByAccessibilityId(data.field);
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
                Assert.AreEqual(bSuccess, true);
            }
            AppendTextBox("* End *" + Environment.NewLine);

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

        private string GetFieldData(AppiumWebElement element, FormField field)
        {
            PropertyInfo result = typeof(AppiumWebElement).GetProperty(field.expression);
            var data = result.GetValue(element).ToString();
            if (field.datatype == "date" && !string.IsNullOrEmpty(data))
            {
                data = GetDate(data);
            }
            else if (field.datatype == "time" && !string.IsNullOrEmpty(data))
            {
                data = GetTime(data);
            }
            return data.ToString();
        }

        private string GetElementData(AppiumWebElement element, FormField field)
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
                    var children = element.FindElementsByTagName("CheckBox");// (By.XPath("*/*"));
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
                AppendTextBox(output);

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
        private string CheckElement(AppiumWebElement root, FormField field)
        {
            AppiumWebElement element = root.FindElementByAccessibilityId(field.name);
            return GetElementData(element, field);
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
        public List<string> CheckChildren(IReadOnlyCollection<AppiumWebElement> children)
        {
            var selected = new List<string>();
            foreach (var child in children)
            {
                if (child.TagName != "ControlType.ScrollBar" && child.Selected)
                {
                    selected.Add(child.GetAttribute("Name"));
                }
            }
            return selected;
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
                        Actions action = new Actions(child.WrappedDriver);
                        //action.MoveToElement(child).Build().Perform();
                        action.DoubleClick(child).Build().Perform();
                        //action.SendKeys(" ").Build().Perform();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return selected;
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            tbOutput.Text = value + tbOutput.Text;
        }

        private FormMapping FieldJson()
        {
            string json = File.ReadAllText(@"..\..\..\..\AutoApp\Fields.json");
            var form = JsonConvert.DeserializeObject<FormMapping>(json);

            return form;
        }
        private void SaveData(FormData formData)
        {
            var json = JsonConvert.SerializeObject(formData);
            AppendTextBox(json + Environment.NewLine);
            AppendTextBox("Data Saved for CMS API" + Environment.NewLine);
            File.WriteAllText(@"..\..\..\..\AutoApp\Data.json", json);
        }
        private FormData LoadData()
        {
            string json = File.ReadAllText(@"..\..\..\..\AutoApp\Data.json");
            AppendTextBox(json + Environment.NewLine);
            AppendTextBox("Loading API Data " + Environment.NewLine);
            var data = JsonConvert.DeserializeObject<FormData>(json);
            return data;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartServer();
        }
        private void StartServer()
        {
            try
            {
                String command = @"..\..\..\..\Driver\WinAppDriver.exe";
                if (System.IO.File.Exists(@"..\..\..\..\Driver\WinAppDriver.exe"))
                {
                    Process p = new Process();
                    // Configure the process using the StartInfo properties.
                    p.StartInfo.FileName = command;

                    if (btnStart.Text == "Stop Server")
                    {
                        StopExisting();
                        btnStart.Text = "Start Server";
                    }
                    else
                    {
                        StopExisting();
                        var process = new Process();
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.WindowStyle = ProcessWindowStyle.Normal;
                        startInfo.FileName = "cmd.exe";
                        startInfo.WorkingDirectory = @"..\..\..\..\Driver\";
                        startInfo.Arguments = $"/C WinAppDriver 4723 > log.txt";
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.UseShellExecute = true;
                        process.StartInfo = startInfo;
                        process.Start();

                        //ProcessStartInfo psi = new ProcessStartInfo();
                        //psi.FileName = "cmd.exe";
                        //psi.UseShellExecute = false;
                        //psi.RedirectStandardError = true;
                        //psi.RedirectStandardOutput = true;
                        //psi.WorkingDirectory = @"..\..\..\..\Driver\";
                        //psi.Arguments = $"/C WinAppDriver 4723 > log.txt";
                        ////var proc = new Process();
                        //Process proc = Process.Start(psi);
                        ////proc.StartInfo = psi;
                        ////proc.Start();
                        //proc.WaitForExit();
                        //string errorOutput = proc.StandardError.ReadToEnd();
                        //string standardOutput = proc.StandardOutput.ReadToEnd();
                        //if (proc.ExitCode != 0)
                        //    throw new Exception("WinAppDriver exit code: " + proc.ExitCode.ToString() + " " + (!string.IsNullOrEmpty(errorOutput) ? " " + errorOutput : "") + " " + (!string.IsNullOrEmpty(standardOutput) ? " " + standardOutput : ""));
                        ////p.StartInfo.UseShellExecute = false;
                        //p.StartInfo.RedirectStandardOutput = true;
                        //p.StartInfo.StandardOutputEncoding = System.Text.Encoding.Unicode;
                        //p.StartInfo.Arguments = @" > log.txt";
                        //var f = p.Start();
                        btnStart.Text = "Stop Server";
                    }
                }
            }
            catch (Exception ex)
            {
                StopExisting();
            }
        }
        private static void StopExisting()
        {
            Process[] proc = Process.GetProcessesByName("WinAppDriver");
            if (proc.Length > 0)
                proc[0].Kill();

        }
        private static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //Serilog.Log.Information($"{e.Data}");
        }


    }


}
