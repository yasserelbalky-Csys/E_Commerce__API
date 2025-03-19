using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.SubCategoryDtos
{
    public class SubCategoryUpdateDto
    {

        public int SubCategoryId { get; set; }

        public string? SubCategoryName { get; set; }


        public string? SubCategoryDescription { get; set; }

        public bool b_deleted { get; set; }
        public int CategoryId { get; set; }

       
    }
}
