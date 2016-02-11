using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ImageUploader.Web.Extensions
{
    public static class HtmlCustom
    {
        public static MvcHtmlString MyLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            return new MvcHtmlString(string.Format("<label class=\"control-label col-md-2\" for=\"{0}\">{1}</label>", propertyName.ToCamelCase(), propertyName.ToSeparatedWords()));
        }
        public static MvcHtmlString MyDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var dropDownvalues = JsonConvert.SerializeObject(GetDropDownValues(expression).Select(d => new { Key = d.Key, Text = d.Value }));
            return new MvcHtmlString(string.Format(
                 "<select class='form-control' data-bind='options: {0}, optionsText: \"{1}\", optionsValue:\"{2}\", value: {3}, optionsCaption: \"{4}\"'></select>",
                 dropDownvalues,
                 "Text", "Key",
                 propertyName.ToCamelCase(), //NOTE this should NOT be a quoted value
                 "Choose..."));
        }
        public static MvcHtmlString MyTextInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("text", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyTelInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("tel", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyEmailInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return GetInput("email", ExpressionHelper.GetExpressionText(expression));
        }
        public static MvcHtmlString MyCheckboxInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            return GetInput(
                "checkbox",
                propertyName,
                new KeyValuePair<string, string>("data-bind", "checked: " + propertyName.ToCamelCase()),
                new KeyValuePair<string, string>("class", "checkbox"));
        }

        private static MvcHtmlString GetInput(string inputType, string propertyName, params KeyValuePair<string, string>[] overrides)
        {
            var defaults = new Dictionary<string, string>
				{
					{"type", inputType},
					{"id", propertyName.ToCamelCase()},
					{"name", propertyName},//CSharp naming (ie Pascal)
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

        public static Dictionary<string, string> GetDropDownValues<TModel, TValue>(Expression<Func<TModel, TValue>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) throw new InvalidOperationException("Expression member is not a property or field accessor");
            var propertyType = memberExpression.Type;
            if (!propertyType.IsEnum) throw new InvalidOperationException("Expression member not an Enum type");
            var values = propertyType.GetEnumValues();
            return values.Cast<object>()
                         .Where(e => !IsDefaultEnum(e))
                         .ToDictionary(enumValue => enumValue.ToString(), GetEnumDescription);
        }

        private static bool IsDefaultEnum(object enumValue)
        {
            var type = enumValue.GetType();
            return (enumValue.Equals(GetDefault(type))) &&
                    enumValue.ToString().Equals("None", StringComparison.InvariantCultureIgnoreCase);
        }
        public static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private static string GetEnumDescription(object enumValue)
        {
            return enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .OfType<DisplayAttribute>()
                .Select(da => da.Name)
                .SingleOrDefault() ?? enumValue.ToString();
        }
    }
}