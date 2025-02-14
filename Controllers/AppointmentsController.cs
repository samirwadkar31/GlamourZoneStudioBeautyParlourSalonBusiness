using Microsoft.AspNetCore.Mvc;
using GlamourZone.Data;
using GlamourZone.Models;
using System;
using System.Linq;

namespace GlamourZone.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly SalonDbContext _context;

        public AppointmentsController(SalonDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public IActionResult Index()
        {
            // Fetch today's appointments from the database
            var today = DateTime.Today;
            var appointments = _context.Appointments
                .Where(a => a.AppointmentDate == today)
                .OrderBy(a => a.AppointmentTime)
                .ToList();

            return View(appointments);
        }
 
    }
}
