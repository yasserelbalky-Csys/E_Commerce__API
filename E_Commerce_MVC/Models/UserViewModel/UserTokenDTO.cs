﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models.UserViewModel
{
	public class UserTokenDTO
	{
		public string? UserName { get; set; }

		[Required]
		public string? Email { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}