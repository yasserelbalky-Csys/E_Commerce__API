using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class ProductBalanceViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display(Name = "Product Quantity")]
        public int Qty { get; set; }

        [Display(Name = "Store ID")]
        public int StoreId { get; set; }

        [Display(Name = "Order No")]
        public int OrderNo { get; set; }

        [Display(Name = "Pending")]
        public bool b_pending { get; set; }

        [Display(Name = "Canceled")]
        public bool b_cancel { get; set; }
    }
}