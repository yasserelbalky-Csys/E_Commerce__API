using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class ProductViewModel
    {
        [Display(Name = "Product ID")]  
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]    
        public string? ProductName { get; set; }

        [Display(Name = "Product Description")] 
        public string? ProductDiscription { get; set; }

        [Display(Name = "Product Price")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "Subcategory ID")]
        public int SubcategoryId { get; set; }

        [Display(Name = "Subcategory Name")]
        public string? SubcategoryName { get; set; }

        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        [Display(Name = "Deleted")]
        public bool b_deleted { get; set; }
    }
}