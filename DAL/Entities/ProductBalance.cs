using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class ProductBalance
	{
		public int ProductId { get; set; }
		public int StoreId { get; set; }
		public int Qty { get; set; }
		public bool b_order_done { get; set; }
		public bool b_order_Pending { get; set; }
	}
}