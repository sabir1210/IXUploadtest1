using System;

namespace BDU.ViewModels
{
   public class VisualIndicatorsViewModel
    {
        public int id { get; set; } = 1;
        public int level { get; set; } = 1;
        public string color { get; set; } = "";
        public int threshold { get; set; }= 40;       
        public int Status { get; set; } = 1;

       
    }
}
