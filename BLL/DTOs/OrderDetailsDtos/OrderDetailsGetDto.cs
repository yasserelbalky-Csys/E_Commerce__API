using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.OrderDetailsDtos
{
    public class OrderDetailsGetDto
    {
        public int LineNo { get; set; }

        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public int Qty { get; set; }

        [Required]
        public decimal ProductPrice { get; set; }

        public decimal TotalValue { get; set; }
    }
}