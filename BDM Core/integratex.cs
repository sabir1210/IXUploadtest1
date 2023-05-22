using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace servr.integratex.ui
{
    [RunInstaller(true)]
    public partial class integratex : System.Configuration.Install.Installer
    {
        public integratex()
        {
            InitializeComponent();
        }
        public override void Install(IDictionary savedState)
        {
            try
            {
                base.Install(savedState);
               // ServrMessageBox.Confirm("Test Comments", "Confirmation");
                var regVersion = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\PolicyManager\default\ApplicationManagement\AppModelUnlock", true);
                if (regVersion is null)
                {
                    // Key doesn't exist; create it.
                    regVersion = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\PolicyManager\default\ApplicationManagement\AppModelUnlock");
                }

               
                if (regVersion is object)
                {
                    //intDeveloperMode = Convert.ToInt16(regVersion.GetValue("AppModelUnlock", 0));
                    regVersion.SetValue("RegValueNameRedirect", "AllowDevelopmentWithoutDevLicense");
                    regVersion.Close();
                }
            }
            catch (Exception ex) { }// Try Catch Block
        }
    }
}
