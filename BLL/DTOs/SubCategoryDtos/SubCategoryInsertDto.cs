using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.SubCategoryDtos
{
    public class SubCategoryInsertDto
    {
        public string? SubCategoryName { get; set; }


        public string? SubCategoryDescription { get; set; }


        public int MainCategoryId { get; set; }

     
    }
}
