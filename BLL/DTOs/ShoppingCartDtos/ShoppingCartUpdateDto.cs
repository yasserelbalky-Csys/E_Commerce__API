using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTOs.ShoppingCartDtos
{
	public class ShoppingCartUpdateDto
	{
		// public int ShoppingCartId { get; set; }
		public int ProductId { get; set; }

		[Range(1, 100)]
		public int Count { get; set; }

		public string UserId { get; set; }
	}
}