using Microsoft.AspNetCore.Mvc;
using GlamourZone.Data;
using GlamourZone.Models;
using GlamourZone.Extensions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

public class CartController : Controller
{
    private readonly SalonDbContext _context;

    public CartController(SalonDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.Get<List<ServiceViewModel>>("Cart") ?? new List<ServiceViewModel>();
        return View(cart);
    }

    [HttpPost]
    public IActionResult Add(int serviceId)
    {
        var cart = HttpContext.Session.Get<List<ServiceViewModel>>("Cart") ?? new List<ServiceViewModel>();
        var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);

        if (service != null)
        {
            cart.Add(new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                CategoryId = service.CategoryId // Ensure CategoryId is added
            });
            HttpContext.Session.Set("Cart", cart);
        }

        return Json(new { success = true });
    }

    public IActionResult Remove(int serviceId)
    {
        var cart = HttpContext.Session.Get<List<ServiceViewModel>>("Cart");
        var service = cart?.FirstOrDefault(s => s.Id == serviceId);

        if (service != null)
        {
            cart.Remove(service);
            HttpContext.Session.Set("Cart", cart);
        }

        return RedirectToAction("Index");
    }

    public IActionResult ProceedToBook()
    {
        var cart = HttpContext.Session.Get<List<ServiceViewModel>>("Cart");
        var selectedServiceIds = cart.Select(s => s.Id).ToList();

        TempData["SelectedServiceIds"] = string.Join(",", selectedServiceIds);

        // Assuming all services in the cart are from the same category for simplicity
        var category = cart.FirstOrDefault()?.CategoryId ?? 0;
        TempData["CategoryId"] = category;

        return RedirectToAction("Index", "Booking");
    }
}
