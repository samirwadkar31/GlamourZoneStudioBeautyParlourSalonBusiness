using GlamourZone.Data;
using GlamourZone.Models;
using GlamourZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamourZone.Controllers
{
    public class BookingController : Controller
    {
        private readonly SalonDbContext _context;

        public BookingController(SalonDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new BookingViewModel
            {
                Categories = _context.Categories.ToList(),
                Services = new List<ServiceViewModel>()
            };

            if (TempData["SelectedServiceIds"] != null)
            {
                var selectedServiceIds = TempData["SelectedServiceIds"].ToString()
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();
                viewModel.SelectedServiceIds = selectedServiceIds;

                viewModel.Services = _context.Services
                    .Where(s => selectedServiceIds.Contains(s.Id))
                    .ToList();
            }

            if (TempData["SelectedCategoryId"] != null)
            {
                var categoryId = int.Parse(TempData["SelectedCategoryId"].ToString());
                viewModel.SelectedCategoryId = categoryId;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var appointment = new Appointment
                        {
                            ClientName = model.ClientName,
                            MobileNumber = model.MobileNumber,
                            AppointmentTime = model.BookingDate
                        };

                        _context.Appointments.Add(appointment);
                        _context.SaveChanges();

                        if (model.SelectedServiceIds != null && model.SelectedServiceIds.Any())
                        {
                            foreach (var serviceId in model.SelectedServiceIds)
                            {
                                var appointmentService = new AppointmentService
                                {
                                    AppointmentId = appointment.Id,
                                    ServiceId = serviceId
                                };

                                _context.AppointmentServices.Add(appointmentService);
                            }

                            _context.SaveChanges();
                        }

                        transaction.Commit();
                        TempData["SuccessMessage"] = "Your appointment has been booked successfully!";
                        return RedirectToAction("Success");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("", "An error occurred while booking the appointment.");
                    }
                }
            }

            model.Categories = _context.Categories.ToList();
            return View("Index", model);
        }

        public IActionResult Success()
        {
            ViewBag.Message = TempData["SuccessMessage"];
            return View();
        }

        public IActionResult ViewAppointments(DateTime? date)
        {
            var selectedDate = date ?? DateTime.Today;

            var appointments = _context.Appointments
                .Where(a => a.AppointmentTime.Date == selectedDate.Date)
                .Include(a => a.AppointmentServices)
                .ThenInclude(asv => asv.Service)
                .ToList();

            return View(appointments);
        }

        [HttpGet]
        public JsonResult LoadServices(int categoryId)
        {
            var services = _context.Services
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new { id = s.Id, name = s.Name })
                .ToList();

            return Json(services);
        }

        [HttpGet]
        public async Task<IActionResult> FilterServicesByCategory(int categoryId)
        {
            var services = await _context.Services
                .Where(s => s.CategoryId == categoryId)
                .Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    price = s.Price
                })
                .ToListAsync();

            return Json(services);
        }

        public IActionResult BookNow(int serviceId)
        {
            var service = _context.Services.Include(s => s.Categories).FirstOrDefault(s => s.Id == serviceId);
            if (service == null)
            {
                return NotFound();
            }

            var viewModel = new BookingViewModel
            {
                Categories = _context.Categories.ToList(),
                Services = new List<ServiceViewModel> { new ServiceViewModel { Id = service.Id, Name = service.Name } },
                SelectedServiceIds = new List<int> { service.Id },
                SelectedCategoryId = service.CategoryId // Prepopulate the category
            };

            return View("Index", viewModel);
        }
    }
}
