using System;
using System.Collections.Generic;

namespace GlamourZone.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime AppointmentTime { get; set; }

        // Navigation property for related services
        public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
    }
}
