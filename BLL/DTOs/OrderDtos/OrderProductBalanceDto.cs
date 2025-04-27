using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.OrderDtos
{
    public class OrderProductBalanceDto
    {
        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int StoreId { get; set; }

        public bool b_order_pending { get; set; }

        public bool b_order_done { get; set; }

        public bool b_order_cancel { get; set; }

        public int OrderNo { get; set; }
    }
}