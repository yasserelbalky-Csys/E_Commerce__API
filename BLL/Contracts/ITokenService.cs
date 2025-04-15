using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.UserDtos;
using DAL.Entities;

namespace BLL.Contracts
{
	public interface ITokenService
	{
		public string CreateToken(AppUser user, IList<string> roles);
	}
}