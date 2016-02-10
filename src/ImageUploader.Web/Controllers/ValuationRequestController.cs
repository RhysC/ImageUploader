using System.Web.Mvc;
using ImageUploader.Web.Models;

namespace ImageUploader.Web.Controllers
{
    public class ValuationRequestController : Controller
    {
        [HttpPost]
        public ActionResult Create(ValuationRequest model)
        {
            return View();
        }
    }
}