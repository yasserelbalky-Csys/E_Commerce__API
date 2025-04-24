using BLL.Contracts;
using BLL.DTOs.OrderDetailsDtos;
using BLL.DTOs.OrderDtos;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.Data.SqlClient;

namespace BLL.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uof;
        private ShoppingCartService shoppingcart;

        public OrderService(IUnitOfWork uof)
        {
            _uof = uof;
            shoppingcart = new ShoppingCartService(_uof);
        }

        public void DeleteOrder(int id) { }

        public OrderListDto GetOrderById(int orderid)
        {
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
                    b_deleted = found.b_deleted,
                    b_confirmed = found.b_confirmed,
                    b_cancel = found.b_cancel
                };
            } else {
                return null;
                //throw new KeyNotFoundException($"Order with ID {orderid} not found.");
            }
        }

        public IEnumerable<OrderListDto> GetOrders()
        {
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
                b_deleted = o.b_deleted,
                b_confirmed = o.b_confirmed,
                b_cancel = o.b_cancel
            }).Where(order => order.b_deleted == false);
        }

        public bool InsertOrder(OrderInsertDto order)
        {
            var details = shoppingcart.GetUserCart(order.UserId); // shoppingcart.GetUserCart(order.UserId);
            var CartTotalValue = shoppingcart.GetTotalCartPrice(order.UserId);
            _uof.save();
            var orderNumber = (_uof.Orders.GetAll().Select(o => (int?)o.OrderNo).DefaultIfEmpty(0).Max() ?? 0) + 1;

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

            bool res = false;

            string connStr =
                "server=.;database=E_Commerce_Rewad;Trusted_Connection=SSPI;TrustServerCertificate=True;MultipleActiveResultSets=True";

            foreach (var item in _uof.OrderDetails.GetAll()) {
                string query =
                    $"SELECT SUM(qty) FROM CurrentProductBalance WHERE product_id = '{item.ProductId}' AND order_no = '{item.OrderNo}'";

                using (var conn = new SqlConnection(connStr)) {
                    conn.Open();

                    using (var cmd = new SqlCommand(query, conn)) {
                        var result = cmd.ExecuteScalar();
                        var qty = result != null ? Convert.ToInt32(result) : 0;

                        if (qty < item.Qty) {
                            res = false;
                        } else {
                            res = true;
                        }
                        // Use qty here
                    }
                }
            }

            if (CartTotalValue >= 2000) {
                CartTotalValue = CartTotalValue - (CartTotalValue * 0.1m);
            }

            _uof.Orders.Insert(new OrderMaster {
                OrderNo = orderNumber,
                UserId = order.UserId,
                OrderDate = new DateTime(DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.Hour,
                    DateTime.Now.Minute,
                    DateTime.Now.Second),
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
                b_confirmed = false,
                b_cancel = false
            });

            if (res == false) {
                return false;
            } else {
                _uof.save();

                return true;
            }
        }

        public int UpdateOrder(OrderUpdateDto order, ICollection<OrderDetailsUpdateDto> details)
        {
            var foundEntity = _uof.Orders.GetById(order.OrderNo);

            if (foundEntity != null) {
                foundEntity.UserId = order.UserId;
                foundEntity.OrderDate = DateTime.Now;
                foundEntity.OrderShippedDate = order.OrderShippedDate;
                foundEntity.OrderStatus = order.OrderStatus;
                foundEntity.PaymentStatus = order.PaymentStatus;
                foundEntity.Traking = order.Traking;
                foundEntity.PaymentDate = order.PaymentDate;
                foundEntity.PaymentDueDate = order.PaymentDueDate;
                foundEntity.UserCardId = order.UserCardId;
                foundEntity.PhoneNo = order.PhoneNo;
                foundEntity.StreetAddress = order.StreetAddress;
                foundEntity.City = order.City;
                foundEntity.State = order.State;
                foundEntity.PostalCode = order.PostalCode;
                foundEntity.Name = order.Name;
                foundEntity.b_deleted = order.b_deleted;

                var existingDetails = _uof.OrderDetails.GetAll().Where(d => d.OrderNo == foundEntity.OrderNo).ToList();

                foreach (var item in existingDetails) {
                    _uof.OrderDetails.Delete(item.LineNo);
                }

                decimal netValue = 0;
                int serial = 0;

                foreach (var item in details) {
                    serial++;

                    var detail = new OrderDetails {
                        OrderNo = foundEntity.OrderNo,
                        ProductId = item.ProductId,
                        LineNo = serial,
                        Qty = item.Qty,
                        ProductPrice = item.ProductPrice,
                        TotalValue = item.Qty * item.ProductPrice
                    };
                    netValue += detail.TotalValue;
                    _uof.OrderDetails.Insert(detail);
                }

                foundEntity.Discount = netValue * 0.1m;

                if (netValue >= 2000) {
                    netValue = netValue - (netValue * 0.1m);
                }

                foundEntity.NetValue = netValue;

                _uof.Orders.Update(foundEntity);
                _uof.save();

                return 1;
            }

            return 0;
        }
    }
}