namespace CMS.Utilities.Common
{
    using CMS.Utilities.Attribute;
    using System;
    using System.Reflection;
    
    public class GetEnumValue
    {
        public static string CollectPropertyValue<T>(T enumeration)
        {
            string result = string.Empty;

            try
            {
                Type type = enumeration.GetType();
                FieldInfo fieldInfo = enumeration.GetType().GetField(enumeration.ToString());
                EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    result = attributes[0].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
