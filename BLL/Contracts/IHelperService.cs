﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.OrderDetailsDtos;
using BLL.DTOs.OrderDtos;

namespace BLL.Contracts
{
	public interface IHelperService
	{
		public int UpdateOrderStatus(int order_no);
	}
}