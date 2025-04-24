using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Stores
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        public string? StoreName { get; set; }

        public bool b_deleted { get; set; }
    }
}