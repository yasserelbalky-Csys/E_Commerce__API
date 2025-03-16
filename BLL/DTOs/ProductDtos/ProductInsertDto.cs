using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ProductDtos
{
    public class ProductInsertDto
    {
        public string? ProductName { get; set; }

        public string? ProductDiscription { get; set; }
        [Range(1, 1000)]
        [Required]
        public decimal ProductPrice { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }
    }
}
