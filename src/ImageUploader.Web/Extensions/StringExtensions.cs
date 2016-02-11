using System.Text.RegularExpressions;

namespace ImageUploader.Web.Extensions
{
    public static class StringExtensions
    {
        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
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