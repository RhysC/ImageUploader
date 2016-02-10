using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ImageUploader.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch { }
            return MvcHtmlString.Empty;
        }
    }
}