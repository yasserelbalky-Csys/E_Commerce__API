using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ProductDtos
{
	public class ProductUpdateDto
	{
		public int ProductId { get; set; }

		[Required]
		public string? ProductName { get; set; }

		public string? ProductDiscription { get; set; }

		[Range(1, 1000)]
		[Required]
		public decimal ProductPrice { get; set; }

		[Required]
		public int SubcategoryId { get; set; }

		[Required]
		public int BrandId { get; set; }

		public bool b_deleted { get; set; }
	}
}