using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using BLL.DTOs.OrderDtos;
using DAL.Contracts;

namespace BLL.Services
{
	class HelperService : IHelperService
	{
		private readonly IUnitOfWork _uof;

		public HelperService(IUnitOfWork uof)
		{
			_uof = uof;
		}

		public int UpdateOrderStatus(int order_no)
		{
			var found = _uof.Orders.GetById(order_no);
			if (found != null) {
				found.OrderStatus = "Confirmed";
				_uof.save();
				return 1;
			} else {
				return 0;
			}
		}
	}
}