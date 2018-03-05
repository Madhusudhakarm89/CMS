namespace CMS.Utilities.Attribute
{
    using System;
   
    internal class EnumDescriptionAttribute:Attribute
    {
         private string dataValue;

         public EnumDescriptionAttribute(string value)
        {
            dataValue = value;
        }

        public string Value
        {
            get
            {
                return dataValue;
            }
        }
    }
 
}
