﻿namespace GlamourZone.Models
{
    public class AppointmentService
    {
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int ServiceId { get; set; }
        public ServiceViewModel Service { get; set; }
    }
}
