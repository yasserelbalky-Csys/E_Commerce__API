using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class SubCategories
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }


        public string? SubCategoryDescription { get; set; }


        [Required]
        public int CategoryId { get; set; } // Explicit Foreign Key

        [ForeignKey(nameof(CategoryId))]
        public Categories Category { get; set; } // Navigation Property

        public ICollection<Products>? Products { get; set; }

    }
}
