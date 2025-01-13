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

        [HttpGet]
        public async Task<IActionResult> Services(int categoryId)
        {
            ViewData["BodyClass"] = "services-page";
            // Fetch the category along with its services
            var category = await _context.Categories
                .Include(c => c.Services) // Ensure services are included
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound(); // Return 404 if the category doesn't exist
            }

            ViewData["CategoryName"] = category.Name; // Pass category name for display
            return View(category.Services); // Pass the services to the view
        }
    }
}
