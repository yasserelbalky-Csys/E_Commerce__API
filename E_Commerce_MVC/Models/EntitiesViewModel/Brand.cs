using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class Brand
	{
		[Key]
		public int BrandId { get; set; }

		[Required]
		public string? BrandName { get; set; }

		public string? BrandDescription { get; set; }
		public ICollection<Products>? Products { get; set; }
		public bool b_deleted { get; set; }
	}
}
