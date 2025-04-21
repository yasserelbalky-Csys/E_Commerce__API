﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.UserDtos
{
	public class UserLoginDto
	{
		[Required]
		public string? UserName { get; set; }

		[Required]
		public string? Password { get; set; }
	}
}