using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Brands
	{
		[Key]
		public int BrandId { get; set; }

		[Required]
		public string? BrandName { get; set; }

		public string? BrandDescription { get; set; }
		public ICollection<Products>? Products { get; set; }
		public bool b_deleted { get; set; }
	}
}