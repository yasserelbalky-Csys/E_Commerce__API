using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities {

namespace DAL.Entities
{

    public class Brands
    {
        //Test Brand Comment
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }

        //test product testttt  zzzzz
        public ICollection<Products>? Products { get; set; }




    }
}
