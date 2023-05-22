namespace WindowsFormsTestApplication
{
    #region using

    using System;
    using System.Windows.Forms;
    using NLog;
   

    #endregion

    internal static class Program
    {
        #region Methods
       // private Logger _log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
             Logger _log = LogManager.GetCurrentClassLogger();
        _log.Error("Error Found, Can not create ibject");
            _log.Info("Error Found");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PMSForm());
        }

        #endregion
    }
}
