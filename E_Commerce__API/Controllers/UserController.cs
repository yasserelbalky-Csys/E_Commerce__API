using BLL.Contracts;
using BLL.DTOs.UserDtos;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public UserController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _accountManager.LoginAsync(user);

            if (res != null) {
                return Ok(res);
            } else {
                return BadRequest("Login Failed");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostRole(string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole))
                return BadRequest("Role name is required");

            var result = await _accountManager.CreateRole(newRole);

            if (!result)
                return BadRequest("Role already exists or creation failed");

            return Ok("Role created successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _accountManager.RegisterAsync(userRegisterDto);

            if (!result)
                return BadRequest("Registration failed");

            return Ok("Registration succeeded");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _accountManager.GetAll();

            if (result == null) {
                return Ok("No Users");
            } else {
                return Ok(result);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(string username)
        {
            var result = await _accountManager.UpdateRole(username);

            if (result == false)
                return Ok("UserName Not Found");
            else
                return Ok("User Role Updated Successfully");
        }
    }
}