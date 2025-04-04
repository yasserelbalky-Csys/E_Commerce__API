using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.OrderDtos
{
    public class OrderUpdateDto
    {
        public int OrderNo { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderShippedDate { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? Traking { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }
        public string UserCardId { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal NetValue { get; set; }
        public bool b_deleted { get; set; }

        //public ICollection<OrderDetails> orderDetailss { get; set; }

    }
}
