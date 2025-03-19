using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.BrandDtos
{
    public class BrandListAllProductsDto
    {
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }


        public string? ProductDescription { get; set; }

        public bool b_deleted { get; set; }


    }
}
