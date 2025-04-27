using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class ShopingCartViewModel
    {
        [Display(Name = "ID")]
        public int ShoppingCartId { get; set; }

        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Count")]
        public int Count { get; set; } = 1;

        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}