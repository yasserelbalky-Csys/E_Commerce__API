using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.UserViewModel
{
	public class UserTokenDTO
	{
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
	}
}