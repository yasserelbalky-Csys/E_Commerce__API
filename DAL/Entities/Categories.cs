using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }


        public string? CategoryDescription { get; set; }

        public bool b_deleted { get; set; }


        public ICollection<SubCategories>? SubCategories { get; set; }




    }
}
