using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "Line Number")]
        public int LineNo { get; set; }

        [Display(Name = "Product Id")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        [Display(Name = "Product Price")]
        [Required]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Total Value")]
        public decimal TotalValue { get; set; }
    }
}