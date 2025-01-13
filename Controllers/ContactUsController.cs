using Microsoft.AspNetCore.Mvc;

namespace GlamourZone.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
