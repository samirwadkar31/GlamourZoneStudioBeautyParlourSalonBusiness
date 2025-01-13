using System;
using System.ComponentModel.DataAnnotations;

namespace GlamourZone.Models
{
    public class AppointmentViewModel
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        [Phone]
        public string ClientPhone { get; set; }

        [Required]
        public string Services { get; set; } // Comma-separated list of service IDs or names

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        public bool IsBooked { get; set; } = false; // Indicates if the slot is booked
    }
}
