using BLL.Contracts;
using BLL.DTOs.UserDtos;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class AccountManager : IAccountManager
    {
        private readonly IUnitOfWork _uof;
        private readonly ITokenService _tokenService;

        public AccountManager(IUnitOfWork uof, ITokenService tokenService)
        {
            _uof = uof;
            _tokenService = tokenService;
        }

        public async Task<bool> CreateRole(string newrole)
        {
            //throw new NotImplementedException();
            var roleExists = await _uof.RoleManager.RoleExistsAsync(newrole);
            if (roleExists)
            {
                return false;
            }
            var result = await _uof.RoleManager.CreateAsync(new IdentityRole(newrole));
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserListDto>> GetAll()
        {

            var users = _uof.UserManager.Users;
            var userList = new List<UserListDto>();
            foreach (var user in users)
            {
                var roles = await _uof.UserManager.GetRolesAsync(user);
                userList.Add(new UserListDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                });
            }
            return userList;
        }
        public async Task<UserTokenDto> LoginAsync(UserLoginDto user)
        {

            if (string.IsNullOrWhiteSpace(user?.UserName))
                return null;
            var userExist = await _uof.UserManager.Users.FirstOrDefaultAsync(u => u.UserName!.ToLower() == user.UserName.ToLower());
            var result = await _uof.SignInManager.CheckPasswordSignInAsync(userExist, userExist.UserPassword, false);

            if (!result.Succeeded)
                return null;

            var roles = await _uof.UserManager.GetRolesAsync(userExist);
            string role = roles.Any() ? roles[0] : null;
            return new UserTokenDto
            {
                email = userExist.Email,
                FirstName = userExist.FirstName,
                LastName = userExist.LastName,
                role = role,
                username = userExist.UserName,
                token = _tokenService.CreateToken(userExist, roles)
            };
            // throw new NotImplementedException();
        }

        public async Task<bool> RegisterAsync(UserRegisterDto user)
        {

            var allowedRoles = new[] { "Admin", "User" };
            if (!allowedRoles.Contains(user.Role))
            {
                return false;
            }

            var appUser = new AppUser
            {
                Email = user.Email,
                UserName = user.UserName,
                UserPassword = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            var createdUser = await _uof.UserManager.CreateAsync(appUser, user.Password);

            if (!createdUser.Succeeded)
            {
                return false;
            }

            var roleResult = await _uof.UserManager.AddToRoleAsync(appUser, user.Role);

            if (!roleResult.Succeeded)
            {
                return false;
            }

            var roles = await _uof.UserManager.GetRolesAsync(appUser);
            var token = _tokenService.CreateToken(appUser, roles);

            return true;
            //throw new NotImplementedException();
        }

        public async Task<bool> UpdateRole(string username)
        {

            var user = await _uof.UserManager.FindByNameAsync(username);
            if (user == null)
                return false;
            var currentRoles = await _uof.UserManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();
            if (string.IsNullOrEmpty(currentRole))
                return false;
            var removeResult = await _uof.UserManager.RemoveFromRoleAsync(user, currentRole);
            if (!removeResult.Succeeded)
                return false;

            var newRole = currentRole == "Admin" ? "User" : "Admin";
            var addResult = await _uof.UserManager.AddToRoleAsync(user, newRole);

            return addResult.Succeeded;

            //throw new NotImplementedException();
        }
    }
}
