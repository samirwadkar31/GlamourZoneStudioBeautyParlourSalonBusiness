// Models/ServiceViewModel.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamourZone.Models
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }  // Foreign Key to Category
        public string Name { get; set; }  // Massage, HairCut, Makeup, etc.
        public string Description { get; set; }
        public string PhotosPath { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // include price

        // Navigation property for related category
        public CategoryViewModel Categories { get; set; }

        // Navigation property for appointments
        public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
    }
}
