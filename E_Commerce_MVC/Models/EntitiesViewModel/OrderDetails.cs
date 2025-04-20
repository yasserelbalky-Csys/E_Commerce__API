using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class OrderDetails
	{
		[Required]
		public int OrderNo { get; set; }

		[ForeignKey(nameof(OrderNo))]
		public OrderMaster Order { get; set; }

		public int LineNo { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

		public int Qty { get; set; }

		[Required]
		public decimal ProductPrice { get; set; }

		public decimal TotalValue { get; set; }
		public decimal Discount { get; set; }
	}
}