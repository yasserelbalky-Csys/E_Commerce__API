using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ShoppingCartDtos
{
    public class ShoppingCartUpdateDto
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
        public string UserId { get; set; }
    }
}
