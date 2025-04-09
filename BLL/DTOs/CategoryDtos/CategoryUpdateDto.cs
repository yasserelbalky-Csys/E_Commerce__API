using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CategoryDTOs {
	public class CategoryUpdateDto {
		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryDescription { get; set; }
		public bool b_deleted { get; set; }
	}
}