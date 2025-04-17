using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class SubCategory
	{
		[Key]
		public int SubCategoryId { get; set; }

		[Required]
		public string? SubCategoryName { get; set; }

		public string? SubCategoryDescription { get; set; }

		[Required]
		public int CategoryId { get; set; } // Explicit Foreign Key

		public bool b_deleted { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } // Navigation Property

		public ICollection<Product>? Products { get; set; }
	}
}
