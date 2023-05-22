using System;
using System.Drawing;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
namespace BDU.UTIL
{
    public static class WindowNotifications
    {

     
        public static void windowNotification(string message ="IntegrateX is ready", string title = "Servr IntegrateX",  Image icon= null, Int16 notifyType=1) {

            try
            {
                PopupNotifier pNotify = new PopupNotifier();
                pNotify.ShowGrip = false;
                pNotify.ShowCloseButton = true;
                //pNotify.AnimationDuration = 6000;
                pNotify.AnimationDuration = 1200;
                // pNotify.AnimationInterval = 15;
                pNotify.AnimationInterval = 3;
                pNotify.Delay = 5500;
                pNotify.HeaderHeight = 1;
                pNotify.Scroll = true;               
                pNotify.ButtonBorderColor = SystemColors.WindowFrame;
                pNotify.ContentHoverColor = SystemColors.HotTrack;
                pNotify.BorderColor = SystemColors.WindowFrame;
                // pNotify.HeaderColor = SystemColors.ControlDark;
                pNotify.TitleFont = new Font("Microsoft Sans Serif", 12.0f,FontStyle.Bold);
                pNotify.TitleColor = Color.FromArgb(90, 185, 234);
                pNotify.TitlePadding = new Padding(5,8,5,5);
                pNotify.ContentFont = new Font("Microsoft Sans Serif", 12.0f);               
                pNotify.ContentPadding = new Padding(6);
                pNotify.ImagePadding = new Padding(5, 6, 5, 5);
              
                pNotify.HeaderColor = Color.FromArgb(90, 185, 234);
                //ToolStripMenuItem DisableWordMenu = new ToolStripMenuItem("Disable");
                //DisableWordMenu.Click += delegate {
                //    if (MenuClick != null)
                //        MenuClick(this, null);
                //};
                pNotify.ShowOptionsButton = false;
               // pNotify.OptionsMenu = new ContextMenuStrip();
               // pNotify.OptionsMenu.Items.Add(DisableWordMenu);
                pNotify.ImageSize = new Size(28, 28);
                switch (notifyType)
                {
                    case 2: // warning
                        pNotify.TitleText = title;
                        //pNotify.OptionsMenu = new ToolStripMenuItem("Disable");
                        pNotify.Image = icon;
                        pNotify.ContentText = message;
                        pNotify.Popup();
                        break;
                    case 3:// Error                     

                        pNotify.TitleText = title;
                        pNotify.Image = icon;
                        pNotify.ContentText = message;
                        //pNotify.ContentColor = Color.Black;
                        pNotify.Popup();
                        break;
                    default:// Info
                        pNotify.TitleText = title;
                        pNotify.Image = icon;
                        pNotify.ContentText = message;
                        //pNotify.ContentColor = Color.Black;
                        pNotify.Popup();
                        break;
                }
            }
            catch (Exception ex) { }
        }

    }
  
}

