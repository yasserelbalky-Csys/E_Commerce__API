using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.BrandDtos
{
    public class BrandInsertDto
    {
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public bool b_deleted { get; set; }
    }
}
