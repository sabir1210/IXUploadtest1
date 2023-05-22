using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinAppDriver.Generation.Client;
using WinAppDriver.Generation.Client.Models;
using WinAppDriver.Generation.Events.Hook.Models;
using WinAppDriver.Generation.PlaybackEvents.Extensions;
using WinAppDriver.Generation.PlaybackEvents.Models;
using WinAppDriver.Generation.UiEvents.Models;
using System.Threading;
using System.IO;
using static BDU.UTIL.BDUUtil;
using AutoApp.States;
using BDU.Services;
using BDU.ViewModels;
using System.Collections.Generic;

namespace AutoApp
{
    /// <summary>
    /// Simple use case of recording / playing a recording using AutomationCOM / WinAppDriver
    /// </summary>
    /// <remarks>
    /// Highly suggest refactoring to WPF & MVVM pattern.
    /// </remarks>
    public partial class StartupForm : System.Windows.Forms.Form
    {
        private GenerationClient _generationClient { get; set; }

        private EventHandler<EventArgs> _generationClientHookProcedureEventHandler { get; set; }

        private EventHandler<UiEventEventArgs> _generationClientUiEventEventHandler { get; set; }

        private RecorderUiState _recorderUiState { get; set; }

        public string rootFolder
        {
            get
            {
                return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\"));
            }
        }

        //private readonly BindingList<UiEvent> _uiEvents = new BindingList<UiEvent>();
        UiEvent lastUIEvent = null;
        private FormHandler _formHandler;
        static System.Threading.Timer TTimer;
        BDU.RobotDesktop.DesktopHandler desktopHandler;

        public StartupForm()
        {
            InitializeComponent();
            desktopHandler = new BDU.RobotDesktop.DesktopHandler();
            List<MappingViewModel> ls = new List<MappingViewModel>();
            ls.Add(new BDUService().TestFieldMapping());
            desktopHandler.StartDesktopSession(ls);
            var response = desktopHandler.TestRun(ls[0]);
            if (response.status)
            {
                MessageBox.Show(
                    response.message,
                    "Test Run",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    (MessageBoxOptions)0x40000);


            }
            else
            {
                MessageBox.Show(
                   response.message,
                   "Test Run",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1,
                   (MessageBoxOptions)0x40000);

            }
            //StartServer();
            //InitFormMapping();
            //SetupDataGridView();
            //SetupDataBindings();
            //SetupGenerationClient();
            //var timer = new System.Threading.Timer((e) => { GetFocued(_formHandler); }, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10));

            //Thread thread = new Thread(() => GetFocued(_formHandler),);
            // thread.Start();
        }
        private void StartServer()
        {
            try
            {
                String command = Path.Combine(rootFolder, @"Driver\WinAppDriver.exe");//@"D:\Shared\BDU-Core\WinAppDriver\Driver\WinAppDriver.exe";
                if (System.IO.File.Exists(command))
                {
                    Process p = new Process();
                    // Configure the process using the StartInfo properties.
                    p.StartInfo.FileName = command;

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
                }
            }
            catch (Exception ex)
            {
            }
        }
        void GetFocued(FormHandler _formHandler)
        {
            try
            {
                var focused = _formHandler.GetFocued();
                if (focused != null)
                {
                    AppendTextBox(focused.GetAttribute("AutomationId") + " " + focused.Text);
                }
                else
                {
                    AppendTextBox("Nothing found");
                }
            }
            catch (Exception ex)
            {
                AppendTextBox("Nothing found");
            }
            //Thread.Sleep(2000);
        }

        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }
            tbStatus.Text = value + Environment.NewLine + tbStatus.Text;
        }

        #region Robot
        private void InitFormMapping()
        {
            _formHandler = new FormHandler();
        }
        #endregion

        #region Hooks
        private void RefreshRecorderDataGridView_YesThisIsAHack_UseTwoWayBinding(UiEvent curUIEvent)
        {
            RecordedDataGridView.Invoke(new Action(() =>
            {
                var type = _formHandler.GetFieldType(curUIEvent);
                if (type == FIELD_TYPES.SUBMIT)
                {
                    _formHandler.GetFieldData(lastUIEvent);
                    _formHandler.ScanRemainingData();
                    _formHandler.SaveFormData();
                    lastUIEvent = null;
                    RefreshGrid();
                    _formHandler.ResetForm();
                }
                else if (type == FIELD_TYPES.CANCEL)
                {
                    lastUIEvent = null;
                    _formHandler.ResetForm();
                    RefreshGrid();
                }
                else if (type == FIELD_TYPES.DATA)
                {
                    _formHandler.GetFieldData(lastUIEvent);
                    RefreshGrid();
                }
            }));
            lastUIEvent = curUIEvent; // will be scanned on next focus
        }
        private void RefreshGrid()
        {
            var filtered = _formHandler._formData.forms.FirstOrDefault().fields.Where(d => d.value != "" && d.value != null).ToList();
            RecordedDataGridView.DataSource = filtered;
            RecordedDataGridView.Update();
            RecordedDataGridView.Refresh();
            //RecordedDataGridView.DataSource = _uiEvents;
            //if (RecordedDataGridView.Rows.Count > 0)
            //{
            //    RecordedDataGridView.Rows.OfType<DataGridViewRow>().Last().Selected = true;
            //}
            //RecordedDataGridView.CurrentCell = RecordedDataGridView.Rows.OfType<DataGridViewRow>().Last().Cells.OfType<DataGridViewCell>().First();
        }
        private void SetupDataGridView()
        {
            RecordedDataGridView.AutoGenerateColumns = false;
            RecordedDataGridView.ColumnCount = 3;
            RecordedDataGridView.Columns[0].Name = "Name";
            RecordedDataGridView.Columns[0].DataPropertyName = "name";
            RecordedDataGridView.Columns[1].Name = "Type";
            RecordedDataGridView.Columns[1].DataPropertyName = "data_type";
            RecordedDataGridView.Columns[2].Name = "Value";
            RecordedDataGridView.Columns[2].DataPropertyName = "value";
        }

        private void SetupDataBindings()
        {
            RecordedDataGridView.DataBindings.Add("DataSource", _formHandler._formData.forms.FirstOrDefault().fields, String.Empty, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Setups Generation Client
        /// </summary>
        private void SetupGenerationClient()
        {
            if (_generationClient == null)
            {
                _generationClient = new GenerationClient(new GenerationClientSettings
                {
                    ProcessId = Process.GetCurrentProcess().Id,
                    AutomationTransactionTimeout = new TimeSpan(0, 1, 0),
                });

                _generationClientHookProcedureEventHandler = GenerationClientHookProcedure;
                _generationClient.GenerationHookProcedureEventHandler += _generationClientHookProcedureEventHandler;

                _generationClientUiEventEventHandler = GenerationClientUiEvent;
                _generationClient.GenerationUiEventEventHandler += _generationClientUiEventEventHandler;
            }
        }

        /// <summary>
        /// Sets Ui / State when the "pause" key on the keyboard is pressed
        /// </summary>
        private void GenerationClientHookProcedure(object sender, EventArgs e)
        {
            if (_generationClient.IsPaused)
            {
                SetUiForRecorderUiStateIsStopped();
            }
            else
            {
                SetUiForRecorderUiStateIsRecording();
            }
        }

        private void SetUiForRecorderUiStateIsStopped()
        {
            _recorderUiState = RecorderUiState.IsStopped;
            RecordButton.Text = $"Record";
        }

        private void SetUiForRecorderUiStateIsRecording()
        {
            _recorderUiState = RecorderUiState.IsRecording;
            RecordButton.Text = $"Stop";
            btnLoad.Enabled = false;
        }

        private void SetUiForRecorderUiStateIsDefault()
        {
            _recorderUiState = RecorderUiState.IsDefault;
            RecordButton.Enabled = true;
            btnLoad.Enabled = true;
        }

        /// <summary>
        /// Handler for new Events being created from WinAppDriver.Generation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerationClientUiEvent(object sender, UiEventEventArgs e)
        {
            //_uiEvents.Add(e.UiEvent);

            Thread thread = new Thread(() => RefreshRecorderDataGridView_YesThisIsAHack_UseTwoWayBinding(e.UiEvent));
            thread.Start();
        }



        /// <summary>
        /// IsPlaying functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartButton_Click(object sender, EventArgs e)
        {      
            try
            {
                // var formData = desktopHandler.
                // ();
                // var res = desktopHandler.FeedDataToDesktopForm(formData);
                var res = desktopHandler.FeedDataToDesktopForm(new MappingViewModel());
                tbStatus.Text += res.status + Environment.NewLine;
                tbStatus.Text += res.status + Environment.NewLine;
                tbStatus.Text += res.message + Environment.NewLine;
                tbStatus.Text += res.jsonData + Environment.NewLine;
            }
            catch (Exception ex)
            {
            }
            //_formHandler.SetAndSubmit();
            //SetUiForRecorderUiStateIsPlayback();

            //_generationClient.TerminateRecording();
            //_generationClient.InitializePlayback();

            //await ExecuteUiEvents();

            //_generationClient.TerminatePlayback();
            //SetUiForRecorderUiStateIsDefault();
        }

        /// <summary>
        /// IsRecording / IsStopped functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordButton_Click(object sender, EventArgs e)
        {


            //InitFormMapping();
            if (_recorderUiState != RecorderUiState.IsRecording)
            {
                SetUiForRecorderUiStateIsRecording();
                try
                {
                    //desktopHandler.StartCaptering();
                }
                catch (Exception ex)
                {
                }
            }
            else if (_recorderUiState != RecorderUiState.IsStopped)
            {
                SetUiForRecorderUiStateIsStopped();
                SetUiForRecorderUiStateIsDefault();
                desktopHandler.StopCaptering();

                //    _generationClient.TerminateRecording();
                //    lastUIEvent = null;
                //    _formHandler.ResetForm();
                //    RefreshGrid();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            //_uiEvents.Clear();
        }

        private void StartupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var processes = Process.GetProcesses().Where(p => p.ProcessName.Contains("TestApp")).ToList();
            foreach (var p in processes)
            {
                //p.Close();
                p.Kill();
            }
            //_generationClient.Terminate();
            Environment.Exit(0);
            Application.Exit();
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private void btnFocused_Click(object sender, EventArgs e)
        {
            var focused = _formHandler.GetFocued();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _formHandler.GetElementData("tbData1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _formHandler.GetElementData("tbData2");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            _formHandler.GetElementData("tbFirst");

        }
    }
}