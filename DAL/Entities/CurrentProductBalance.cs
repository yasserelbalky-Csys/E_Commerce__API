using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CurrentProductBalance
    {
        [Key]
        public int id { get; set; }

        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int StoreId { get; set; }
        public int OrderNo { get; set; }

        public bool b_pending { get; set; }

        //هضيف ف ال current item balance الكميه بموجب
        public bool b_cancel { get; set; }
    }
}