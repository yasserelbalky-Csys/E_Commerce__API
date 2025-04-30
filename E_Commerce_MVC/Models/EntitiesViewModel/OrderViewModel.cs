using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
    public class OrderViewModel
    {
        [Display(Name = "Order Number")]
        public int OrderNo { get; set; }

        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Shipped Date")]
        public DateTime OrderShippedDate { get; set; } = DateTime.Now;

        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }

        [Display(Name = "Payment Status")]
        public string? PaymentStatus { get; set; } = "Not confirmed";

        [Display(Name = "Tracking")]
        public string? Traking { get; set; } = "Not Confirmed";

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Display(Name = "Payment Due Date")]
        public DateOnly PaymentDueDate { get; set; }

        [Display(Name = "User Card ID")]
        public string UserCardId { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Net Value")]
        public decimal NetValue { get; set; }

        [Display(Name = "Deleted")]
        public bool b_deleted { get; set; }

        [Display(Name = "Confirmed")]
        public bool b_confirmed { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Canceled")]
        public bool b_cancel { get; set; }
    }
}