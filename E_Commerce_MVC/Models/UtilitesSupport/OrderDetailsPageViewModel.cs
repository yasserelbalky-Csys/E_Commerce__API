using E_Commerce_MVC.Models.EntitiesViewModel;

namespace E_Commerce_MVC.Models.UtilitesSupport
{
    public class OrderDetailsPageViewModel
    {
        public OrderViewModel Order { get; set; }
        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}