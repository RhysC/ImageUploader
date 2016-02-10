using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageUploader.Web.Extensions
{
    public static class DefaultScaffoldingExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static PropertyInfo[] VisibleProperties(this IEnumerable model)
        {
            var elementType = model.GetType().GetElementType() ?? model.GetType().GetGenericArguments()[0];
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).ToArray();
        }

        public static PropertyInfo[] VisibleProperties(this Object model)
        {
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName()).ToArray();
        }

        public static RouteValueDictionary GetIdValue(this object model)
        {
            return new RouteValueDictionary { { model.IdentifierPropertyName(), model.GetId() } };
        }

        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model, new object[0]);
        }

        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.PropertyType.AttributeExists<System.ComponentModel.DataAnnotations.KeyAttribute>())
                        .Name;
            }
            if (type.GetProperties().Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
            return meta.GetDisplayName();
        }

        public static string GetLabelAsSeparatedWords(this PropertyInfo propertyInfo)
        {
            var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
            return meta.GetDisplayName();
        }


        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }
        public static string AsExternalLink(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !value.ToLowerInvariant().StartsWith("http://"))
                return string.Format("http://{0}", value);
            return value;
        }

        public static string ToCamelCase(this string value)
        {
            // If there are 0 or 1 characters, just return the string.
            if (value == null || value.Length < 2)
                return value;

            // Split the string into words.
            value = value.Trim().Replace(" ", "_");
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }
    }
}