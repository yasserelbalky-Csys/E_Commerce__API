using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class BrandViewModel
    {
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        [Display(Name = "Brand Name")]
        public string? BrandName { get; set; }

        [Display(Name = "Brand Description")]
        public string? BrandDescription { get; set; }

        [Display(Name = "Deleted")]
        public bool b_deleted { get; set; }
    }
}