using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using BDU.Services;
using BDU.UTIL;
using Microsoft.Win32;
using NLog;
namespace servr.integratex.ui
{
   
    static class Program
    {
        //Logging library, NLOG
        private static Logger log = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                GlobalApp.IX_LAST_MESSAGE = "IntegrateX is ready";
#if (RELEASE)
                //try
                //{
                //    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                //    WindowsPrincipal principal = new WindowsPrincipal(identity);

                //    var groupNames = from id in identity.Groups
                //                     select id.Translate(typeof(NTAccount)).Value.ToLower();

                //    // MessageBox.Show("Role -" + principal.IsInRole(WindowsBuiltInRole.Administrator));

                //    if (!(groupNames.ToList().Contains("admin") || principal.IsInRole(WindowsBuiltInRole.Administrator)))
                //    {

                //        WindowNotifications.windowNotification(message: "IntegrateX must need to run as administrator role!!!", notifyType: 3);

                //        ProcessStartInfo startInfo = new ProcessStartInfo();
                //        startInfo.UseShellExecute = true;
                //        startInfo.WorkingDirectory = Environment.CurrentDirectory;
                //        startInfo.FileName = Application.ExecutablePath;
                //        startInfo.Verb = "runas";
                //        Process.Start(startInfo);
                //        Application.Exit();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    ServrMessageBox.warning("IntegrateX, must need to run as administrator, closing IntegrateX!!!");
                //    Environment.Exit(Environment.ExitCode);
                //}


                try
                {
                   
                    BDU.UTIL.GlobalApp.PROPERTY_MACHINE_NAME = Environment.MachineName;                   
                   

                    string localAppData =
              Environment.GetFolderPath(
              Environment.SpecialFolder.LocalApplicationData);
                    string userFilePath
                      = Path.Combine(localAppData, "servrbkp");
                    if (!Directory.Exists(userFilePath))
                        Directory.CreateDirectory(userFilePath);
                    //if it's not already there, 
                    //copy the file from the deployment location to the folder
                    string sourceFilePath = Path.Combine(
                      System.Windows.Forms.Application.StartupPath, string.Format("{0}.dll.config", "Servr IntegrateX"));
                    string destFilePath = Path.Combine(userFilePath, string.Format("{0}.dll.config", "Servr IntegrateX"));
                    if (!File.Exists(destFilePath))
                        File.Copy(sourceFilePath, destFilePath);
                    else if (File.Exists(destFilePath) && (new FileInfo(sourceFilePath)).LastWriteTimeUtc != (new FileInfo(destFilePath)).LastWriteTimeUtc)
                        File.Copy(destFilePath, sourceFilePath, true);

                    Application.ApplicationExit += new EventHandler(OnApplicationExit);
                }
                catch (Exception ex) { log.Error(ex); }
#endif

                BDU.UTIL.GlobalApp.API_URI = Convert.ToString(""+System.Configuration.ConfigurationManager.AppSettings["apiUrl"]).Trim();
               // BDU.UTIL.GlobalApp.SPECIAL_EMAIL_ACCOUNT = Convert.ToString("" + System.Configuration.ConfigurationManager.AppSettings["apiUrl"]).Trim();
               // BDU.UTIL.GlobalApp.SPECIAL_EMAIL_ACCOUNT_PWD = Convert.ToString("" + System.Configuration.ConfigurationManager.AppSettings["apiUrl"]).Trim();
                //GlobalDiagnosticsContext.Set("hotelname", "Hanging Gardens of Bali");
                //GlobalDiagnosticsContext.Set("subjectcritical", "IntegrateX Critical Error");
                //GlobalDiagnosticsContext.Set("subjecttechnical", "IntegrateX Error");
                //GlobalDiagnosticsContext.Set("smtpuser", "no-reply@servrhotels.com");
                //GlobalDiagnosticsContext.Set("smtppassword", "Servrhotels@123");
                //GlobalDiagnosticsContext.Set("smtpserver", "smtp.servrhotels.com");
                //GlobalDiagnosticsContext.Set("smtpport", "587");
                //GlobalDiagnosticsContext.Set("enablessl", "false");
                //GlobalDiagnosticsContext.Set("mailfrom", "no-reply@servrhotels.com");
                //GlobalDiagnosticsContext.Set("mailtorevenueteam", "znawazch@gmail.com");
                //GlobalDiagnosticsContext.Set("mailtotechnicalteam", "znawazch@gmail.com");
                Application.Run(new BDULoginForm());
               // Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex) {
                log.Error(ex);
            }
        }
        private static void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the
            Environment.Exit(Environment.ExitCode);
        }

       
    }
}
