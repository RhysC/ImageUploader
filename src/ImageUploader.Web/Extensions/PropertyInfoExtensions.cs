using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace ImageUploader.Web.Extensions
{
    public static class HtmlCustom
    {
        public static MvcHtmlString MyLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            return new MvcHtmlString(string.Format("<label class=\"control-label col-md-2\" for=\"{0}\">{1}</label>", propertyName.ToCamelCase(), propertyName.ToSeparatedWords()));
        }
        public static MvcHtmlString MyTextInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("text", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyTelInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("text", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyEmailInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("text", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyCheckboxInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("checkbox", ExpressionHelper.GetExpressionText(expression), new KeyValuePair<string, string>("class", "checkbox"));
        }

        private static MvcHtmlString GetInput(string inputType, string propertyName, params KeyValuePair<string, string>[] overrides)
        {
            var defaults = new Dictionary<string, string>
				{
					{"type", inputType},
					{"id", propertyName.ToCamelCase()},
					{"class", "form-control"},
					{"data-bind", "value: " + propertyName.ToCamelCase()},
		            //{"placeholder=\"{1}\}
		        };
            foreach (var @override in overrides)
            {
                defaults[@override.Key] = @override.Value;
            }
            var htmlAttributes = string.Join(" ", defaults.Select(a => string.Format("{0}=\'{1}\'", a.Key, a.Value)));
            return new MvcHtmlString(string.Format("<input {0}>", htmlAttributes));
        }
    }

    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static bool AttributeExists<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }

        public static string LabelFromType(Type @type)
        {
            var att = GetAttribute<DisplayNameAttribute>(@type);
            return att != null ? att.DisplayName
                : @type.Name.ToSeparatedWords();
        }

        public static string GetLabel(this Object model)
        {
            return LabelFromType(model.GetType());
        }

        public static string GetLabel(this IEnumerable model)
        {
            var elementType = model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }
}