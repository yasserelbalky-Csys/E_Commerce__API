using E_Commerce_MVC.Models.EntitiesViewModel;

namespace E_Commerce_MVC.Models.UtilitesSupport
{
    public class CartItemViewModel
    {
        public ShopingCartViewModel CartItem { get; set; }
        public ProductViewModel Product { get; set; }
    }
}