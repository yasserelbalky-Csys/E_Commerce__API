using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using BLL.DTOs.OrderDetailsDtos;
using BLL.DTOs.OrderDtos;
using BLL.DTOs.ProductDtos;
using BLL.DTOs.StoreDtos;
using DAL.Contracts;
using DAL.Entities;

namespace BLL.Services {
	class OrderService : IOrderService {
		private readonly IUnitOfWork _uof;
		private ShoppingCartService shoppingcart;

		public OrderService(IUnitOfWork uof) {
			_uof = uof;
			shoppingcart = new ShoppingCartService(_uof);
		}

		public void DeleteOrder(int id) { }

		public OrderListDto GetOrderById(int orderid) {
			var found = _uof.Orders.GetById(orderid);
			if (found != null) {
				return new OrderListDto {
					OrderNo = found.OrderNo,
					UserId = found.UserId,
					UserName = found.User.UserName,
					OrderDate = found.OrderDate,
					OrderShippedDate = found.OrderShippedDate,
					OrderStatus = found.OrderStatus,
					PaymentStatus = found.PaymentStatus,
					Traking = found.Traking,
					PaymentDate = found.PaymentDate,
					PaymentDueDate = found.PaymentDueDate,
					UserCardId = found.UserCardId,
					PhoneNo = found.PhoneNo,
					StreetAddress = found.StreetAddress,
					City = found.City,
					State = found.State,
					PostalCode = found.PostalCode,
					Name = found.Name,
					NetValue = found.NetValue,
					b_deleted = found.b_deleted
				};
			} else {
				return null;
				//throw new KeyNotFoundException($"Order with ID {orderid} not found.");
			}
		}

		public IEnumerable<OrderListDto> GetOrders() {
			return _uof.Orders.GetAll().Select(o => new OrderListDto {
				OrderNo = o.OrderNo,
				UserId = o.UserId,
				UserName = o.User.UserName,
				OrderDate = o.OrderDate,
				OrderShippedDate = o.OrderShippedDate,
				OrderStatus = o.OrderStatus,
				PaymentStatus = o.PaymentStatus,
				Traking = o.Traking,
				PaymentDate = o.PaymentDate,
				PaymentDueDate = o.PaymentDueDate,
				UserCardId = o.UserCardId,
				PhoneNo = o.PhoneNo,
				StreetAddress = o.StreetAddress,
				City = o.City,
				State = o.State,
				PostalCode = o.PostalCode,
				Name = o.Name,
				NetValue = o.NetValue,
				b_deleted = o.b_deleted
			}).Where(order => order.b_deleted == false);
		}

		public void InsertOrder(OrderInsertDto order) {
			var details = shoppingcart.GetUserCart(order.UserId); // shoppingcart.GetUserCart(order.UserId);
			var CartTotalValue = shoppingcart.GetTotalCartPrice(order.UserId);
			_uof.save();
			var orderNumber = (_uof.Orders.GetAll().Select(o => (int?)o.OrderNo).DefaultIfEmpty(0).Max() ?? 0) + 1;

			// var orderNumber = _uof.Orders.GetAll().Max(o => o.OrderNo);
			int serial = 0;
			order.orderDetailss = new List<OrderDetails>();
			foreach (var item in details) {
				serial = serial + 1;
				var orderDetail = new OrderDetails {
					OrderNo = orderNumber,
					ProductId = item.ProductId,
					LineNo = serial,
					Qty = item.Count,
					ProductPrice = item.price,
					TotalValue = item.Count * item.price,
				};
				_uof.OrderDetails.Insert(orderDetail);
				order.orderDetailss.Add(orderDetail);
			}

			_uof.Orders.Insert(new OrderMaster {
				OrderNo = orderNumber,
				UserId = order.UserId,
				OrderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
					DateTime.Now.Minute, DateTime.Now.Second),
				OrderShippedDate = order.OrderShippedDate,
				OrderStatus = order.OrderStatus,
				PaymentStatus = order.PaymentStatus,
				Traking = order.Traking,
				PaymentDate = order.PaymentDate,
				PaymentDueDate = order.PaymentDueDate,
				UserCardId = order.UserCardId,
				PhoneNo = order.PhoneNo,
				StreetAddress = order.StreetAddress,
				City = order.City,
				State = order.State,
				PostalCode = order.PostalCode,
				Name = order.Name,
				NetValue = CartTotalValue,
				b_deleted = false,
			});
			_uof.save();
		}

		public int UpdateOrder(OrderUpdateDto order, ICollection<OrderDetailsUpdateDto> details) {
			var found = GetOrderById(order.OrderNo);
			if (found != null) {
				var ordernumber = found.OrderNo;
				found.OrderNo = ordernumber;
				found.UserId = order.UserId;
				found.OrderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
					DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
				found.OrderShippedDate = order.OrderShippedDate;
				found.OrderStatus = order.OrderStatus;
				found.PaymentStatus = order.PaymentStatus;
				found.Traking = order.Traking;
				found.PaymentDate = order.PaymentDate;
				found.PaymentDueDate = order.PaymentDueDate;
				found.UserCardId = order.UserCardId;
				found.PhoneNo = order.PhoneNo;
				found.StreetAddress = order.StreetAddress;
				found.City = order.City;
				found.State = order.State;
				found.PostalCode = order.PostalCode;
				found.Name = order.Name;

				found.b_deleted = order.b_deleted;

				var detailsdeleted = _uof.OrderDetails.GetAll().Where(d => d.OrderNo == found.OrderNo);

				foreach (var itemm in detailsdeleted) {
					_uof.OrderDetails.Delete(itemm.LineNo);
				}

				decimal net_valuee = 0;
				int serial = 0;
				foreach (var item in details) {
					serial = serial + 1;
					var orderDetail = new OrderDetails {
						OrderNo = ordernumber,
						ProductId = item.ProductId,
						LineNo = serial,
						Qty = item.Qty,
						ProductPrice = item.ProductPrice,
						TotalValue = item.Qty * item.ProductPrice,
					};
					net_valuee += orderDetail.TotalValue;
					_uof.OrderDetails.Insert(orderDetail);
				}

				found.NetValue = net_valuee;
				_uof.save();
				return 1;
			} else {
				return 0;
			}
		}
	}
}