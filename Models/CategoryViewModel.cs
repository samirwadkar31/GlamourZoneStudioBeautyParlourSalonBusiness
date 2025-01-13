using System.ComponentModel.DataAnnotations.Schema;

namespace GlamourZone.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }  // e.g., Full Body Massage, Simple Makeup, etc.
        public string Description { get; set; }
        public string PhotosPath { get; set; }


    }

}