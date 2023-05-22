using System;
using System.Collections.Generic;
using System.Text;

namespace AutoApp
{
    public class FormMapping
    {
        public string formRootXPath;
        //public string name;
        ///public string type;
        public List<FormField> formFields;
    }
    public class FormField
    {
        public string desktopXPath;
        //public string name;
        ///public string type;
        public string name;
        public string datatype;
        public string expression;
    }

    public class FormData
    {
        public string formRootXPath;
        //public string name;
        ///public string type;
        public List<FieldData> fieldsData;
    }
    public class FieldData
    {
        public string field;
        //public string name;
        ///public string type;
        public string datatype;
        public string value;
    }
}
