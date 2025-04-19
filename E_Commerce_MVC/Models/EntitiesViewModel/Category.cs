using System.ComponentModel;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class Category
	{
		public int? CategoryId { get; set; }
		[DisplayName("Category Name")]
		public string? CategoryName { get; set; }
		[DisplayName("Category Description")]
		public string? CategoryDescription { get; set; }
		[DisplayName("HARD DELETE")]

		public bool b_deleted { get; set; }
	}
}