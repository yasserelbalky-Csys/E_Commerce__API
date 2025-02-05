using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.ShoppingCartDtos
{
    public class ShoppingCartInsertDto
    {


        public int ProductId { get; set; }

        public int Count { get; set; }


        public string UserId { get; set; }

    


    }
}
