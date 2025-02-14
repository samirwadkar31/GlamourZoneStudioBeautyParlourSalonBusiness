using GlamourZone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlamourZone.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public List<ServiceViewModel> Services { get; set; } = new List<ServiceViewModel>();
        public List<int> SelectedServiceIds { get; set; } = new List<int>();
        public List<int> SelectedCategoryIds { get; set; } = new List<int>(); // Keep this
        public int SelectedCategoryId { get; set; } // Added this
    }
}
