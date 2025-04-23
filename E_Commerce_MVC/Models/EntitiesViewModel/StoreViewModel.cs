using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class StoreViewModel
    {
        [Display(Name = "Store ID")]
        public int StoreId { get; set; }

        [Display(Name = "Store Name")]
        public string? StoreName { get; set; }

        [Display(Name = "Store Description")]
        public bool b_deleted { get; set; }
    }
}