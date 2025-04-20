using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }

		[Required]
		public string? ProductName { get; set; }

		public string? ProductDiscription { get; set; }

		[Required]
		public decimal ProductPrice { get; set; }

		[Required]
		public int SubcategoryId { get; set; }

		[Required]
		public int BrandId { get; set; }

		public bool b_deleted { get; set; }

		[ForeignKey(nameof(SubcategoryId))]
		public SubCategory Subcategory { get; set; }

		[ForeignKey(nameof(BrandId))]
		public Brand? Brand { get; set; }
	}
}