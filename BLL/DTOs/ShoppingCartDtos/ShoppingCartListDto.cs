using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ShoppingCartDtos
{
	public class ShoppingCartListDto
	{
		public int ShoppingCartId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int Count { get; set; }
		public decimal price { get; set; }
		public string UserId { get; set; }
	}
}