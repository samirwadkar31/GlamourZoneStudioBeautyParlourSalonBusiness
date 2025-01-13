using Microsoft.AspNetCore.Mvc;

namespace GlamourZone.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
