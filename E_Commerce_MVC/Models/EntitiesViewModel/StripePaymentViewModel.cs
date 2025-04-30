namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class StripePaymentViewModel
    {
        public string PublishableKey { get; set; }
        public decimal Amount { get; set; }
        public List<ShopingCartViewModel> CartItems { get; set; }
    }

}
