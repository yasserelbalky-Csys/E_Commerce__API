using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }


        public string UserId { get; set; }

        [ForeignKey("ProductId")]
        public Products? Product {  get; set; }

        [ForeignKey("UserId")]
        public AppUser? ApplicationUser { get; set; }
    }
}
