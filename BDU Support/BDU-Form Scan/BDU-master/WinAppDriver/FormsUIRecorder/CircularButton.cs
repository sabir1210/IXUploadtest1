using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace bdu
{
    class CircularButton:Button 
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region= new System.Drawing.Region(grPath);
            base.OnPaint(pevent);
        }
    }
}
