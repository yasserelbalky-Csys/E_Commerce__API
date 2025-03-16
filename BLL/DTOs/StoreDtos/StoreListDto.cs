using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.StoreDtos
{
    public class StoreListDto
    {
        
        public int StoreId { get; set; }
        public string? StoreName { get; set; }
    }
}
