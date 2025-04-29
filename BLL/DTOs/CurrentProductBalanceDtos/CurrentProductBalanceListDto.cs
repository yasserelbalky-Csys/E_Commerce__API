using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CurrentProductBalanceDtos
{
    public class CurrentProductBalanceListDto
    {
        public int id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int StoreId { get; set; }
        public int OrderNo { get; set; }

        public bool b_pending { get; set; }

        public bool b_cancel { get; set; }
    }
}