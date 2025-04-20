using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Commerce_MVC.Models.UserViewModel;

namespace E_Commerce_MVC.Models.EntitiesViewModel
{
	public class ShoppingCart
	{
		[Key]
		public int ShoppingCartId { get; set; }

		public int ProductId { get; set; }
		public int Count { get; set; }
		public string UserId { get; set; }

		[ForeignKey("ProductId")]
		public Product? Product { get; set; }

		[ForeignKey("UserId")]
		public AppUser? ApplicationUser { get; set; }
	}
}