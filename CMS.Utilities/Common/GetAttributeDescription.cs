using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CMS.Utilities.Common
{
    public class GetAttributeDescription
    {
        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            Type type = typeof(TModel);
            string propertyName = null;
            string[] properties = null;
            IEnumerable<string> propertyList;
            //unless it's a root property the expression NodeType will always be Convert
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    propertyList = (ue != null ? ue.Operand : null).ToString().Split(".".ToCharArray()).Skip(1); //don't use the root property
                    break;
                default:
                    propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1);
                    break;
            }

            //the propert name is what we're after
            propertyName = propertyList.Last();
            //list of properties - the last property name
            properties = propertyList.Take(propertyList.Count() - 1).ToArray(); //grab all the parent properties

            foreach (string property in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(property);
                type = propertyInfo.PropertyType;
            }

            DisplayNameAttribute attr;
            attr = (DisplayNameAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            // http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.DisplayName : String.Empty;
        }
    }
}
