using System.Diagnostics;
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
            Debugger.Break();
            //look at the wonderful model!
            return await Task.FromResult(Request.CreateResponse(HttpStatusCode.OK));
        }
    }
}
