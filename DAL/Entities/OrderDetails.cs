using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
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
		public Products Product { get; set; }

		public int Qty { get; set; }

		[Required]
		public decimal ProductPrice { get; set; }

		public decimal TotalValue { get; set; }
		public decimal Discount { get; set; }
	}
}