using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ImageUploader.Web.Models;

namespace ImageUploader.Web.Controllers
{

    public class ValuationRequestController : ApiController
    {
        public async Task<HttpResponseMessage> Post(ValuationRequest model)
        {
            return await Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }


        ///// <summary>
        ///// http://www.asp.net/web-api/overview/advanced/sending-html-form-data-part-2
        ///// </summary>
        ///// <returns></returns>
        //public async Task<HttpResponseMessage> Post_BareMetal()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    string root = HttpContext.Current.Server.MapPath("~/App_Data");
        //    var provider = new MultipartFormDataStreamProvider(root);

        //    try
        //    {
        //        await Request.Content.ReadAsMultipartAsync(provider)
        //            .ContinueWith<HttpResponseMessage>(t =>
        //            {
        //                if (t.IsFaulted || t.IsCanceled)
        //                {
        //                    Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
        //                }

        //                // This illustrates how to get the file names.
        //                foreach (MultipartFileData file in provider.FileData)
        //                {
        //                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
        //                    Trace.WriteLine("Server file path: " + file.LocalFileName);
        //                }

        //                // Show all the key-value pairs.
        //                foreach (var key in provider.FormData.AllKeys)
        //                {
        //                    foreach (var val in provider.FormData.GetValues(key))
        //                    {
        //                        Trace.WriteLine(string.Format("{0}: {1}", key, val));
        //                    }
        //                }

        //                return Request.CreateResponse(HttpStatusCode.OK);
        //            });

        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (System.Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        //    }
        //}
    }
}
