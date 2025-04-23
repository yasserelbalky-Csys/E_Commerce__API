using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ProductDtos
{
    public class ProductListDto
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDiscription { get; set; }

        public decimal? ProductPrice { get; set; }

        public int SubcategoryId { get; set; }

        public string? SubcategoryName { get; set; }

        public int BrandId { get; set; }

        public bool b_deleted { get; set; }
    }
}