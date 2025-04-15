using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.OrderDtos;

namespace BLL.DTOs.OrderDetailsDtos
{
	public class OrderUpdateRequestDto
	{
		public OrderUpdateDto Order { get; set; }
		public ICollection<OrderDetailsUpdateDto> Details { get; set; }
	}
}