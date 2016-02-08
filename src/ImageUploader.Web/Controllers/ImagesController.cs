using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ImageUploader.Web.Controllers
{
    public class ImagesController : ApiController
    {
        // GET: api/Images
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Images/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Images
        //public void Post([FromBody]string value)
        public async Task<HttpResponseMessage> Post()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider)
                    .ContinueWith<HttpResponseMessage>(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                        }

                        // This illustrates how to get the file names.
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                            Trace.WriteLine("Server file path: " + file.LocalFileName);
                        }
                
                        // Show all the key-value pairs.
                foreach (var key in provider.FormData.AllKeys)
                        {
                            foreach (var val in provider.FormData.GetValues(key))
                            {
                                Trace.WriteLine(string.Format("{0}: {1}", key, val));
                            }
                        }


                        return Request.CreateResponse(HttpStatusCode.OK);
                    });



               
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        // PUT: api/Images/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Images/5
        public void Delete(int id)
        {
        }
    }
}
