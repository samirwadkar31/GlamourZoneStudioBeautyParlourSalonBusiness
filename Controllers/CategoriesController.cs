using Microsoft.AspNetCore.Mvc;
using GlamourZone.Data;
using GlamourZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GlamourZone.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SalonDbContext _context;

        public CategoriesController(SalonDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            ViewData["BodyClass"] = "categories-page";
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
            
        }
    }
}
