using BLL.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IAccountManager
    {
        public Task<bool> RegisterAsync(UserRegisterDto user);
        public Task<UserTokenDto> LoginAsync(UserLoginDto user);
        public Task<bool> CreateRole(string newrole);

        public Task<bool> UpdateRole(string username);

        public Task<IEnumerable<UserListDto>> GetAll();

    }
}
