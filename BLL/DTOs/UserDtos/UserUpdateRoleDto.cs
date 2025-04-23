using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.UserDtos
{
    public class UserUpdateRoleDto
    {

        public string UserName { get; set; }// Username to identify the user
        public string NewRole { get; set; }   // "User" or "Admin"




    }
}
