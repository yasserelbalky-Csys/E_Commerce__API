using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }  

        [Required]
        public string? ProductName { get; set; } 

        public string? ProductDiscription { get; set; }

        [Required]
        public decimal? ProductPrice { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [ForeignKey(nameof(SubcategoryId))]
        public SubCategories? Subcategory { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brands? Brand { get; set; }
    }
}
