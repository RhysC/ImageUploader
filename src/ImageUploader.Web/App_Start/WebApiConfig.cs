using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MultipartDataMediaFormatter;

namespace ImageUploader.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //https://github.com/iLexDev/ASP.NET-WebApi-MultipartDataMediaFormatter
            GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter());
            config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           // config.MessageHandlers.Add(new MyHandler());
        }

        class TraceExceptionLogger : ExceptionLogger
        {
            public override void Log(ExceptionLoggerContext context)
            {
                Trace.TraceError(context.ExceptionContext.Exception.ToString());
                base.Log(context);
            }

            public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
            {
                Trace.TraceError(context.ExceptionContext.Exception.ToString());
                return base.LogAsync(context, cancellationToken);
            }

            
        }

        public class MyHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(
                                                     HttpRequestMessage request,
                                                        CancellationToken token)
            {
                HttpMessageContent requestContent = new HttpMessageContent(request);
                string requestMessage = requestContent.ReadAsStringAsync().Result;

                return await base.SendAsync(request, token);
            }
        }
    }
}
