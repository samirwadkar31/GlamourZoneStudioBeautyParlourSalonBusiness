using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlamourZone.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUsController
        public ActionResult ContactUs()
        {

            ViewData["BodyClass"] = "contactus-page";

            return View();
        }

    }
}
