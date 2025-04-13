using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTOs.UserDtos
{
	public class UserTokenDto
	{
		public string? username { get; set; }
		public string? email { get; set; }
		public string? token { get; set; }
	}
}
