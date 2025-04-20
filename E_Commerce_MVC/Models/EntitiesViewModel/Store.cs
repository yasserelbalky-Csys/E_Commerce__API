using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class Store
	{
		[Key]
		public int StoreId { get; set; }

		[Required]
		public string? StoreName { get; set; }

		public bool b_deleted { get; set; }
	}
}
