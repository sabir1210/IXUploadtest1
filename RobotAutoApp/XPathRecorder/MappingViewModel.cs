using System;
using System.Collections.Generic;
using System.Text;

namespace formsuirecorder
{
    class MappingViewModel
    {
        public int ID { get; set; }
        public int CMSFieldId { get; set; }
        public string CMSFieldName { get; set; }
        public string FieldName { get; set; }
        public int FieldID { get; set; }
        public string DataType { get; set; }
        public string XPathID { get; set; }
        public string Formate { get; set; }
        public int Length { get; set; }
        public int Status { get; set; }
        public int SortOrder { get; set; }
        public string DefaultValue { get; set; }
    }
}
