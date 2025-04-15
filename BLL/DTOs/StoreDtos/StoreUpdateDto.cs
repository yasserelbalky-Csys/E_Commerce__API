using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.StoreDtos
{
	public class StoreUpdateDto
	{
		public int StoreId { get; set; }
		public string? StoreName { get; set; }
		public bool b_deleted { get; set; }
	}
}