﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.UserDtos
{
    public class UserRegisterDto
    {
        [Required]
        public string? UserName { get; set; }


        [Required]
        public string? Email { get; set; }


        [Required]
        public string? Password { get; set; }


        public string Role { get; set; }






    }
}
