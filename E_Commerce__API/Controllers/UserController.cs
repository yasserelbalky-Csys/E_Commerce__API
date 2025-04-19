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
        private readonly UserManager<AppUser> _usermanger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccountManager _accountManager;

        public UserController(UserManager<AppUser> usermanger, ITokenService tokenService,
            SignInManager <AppUser> signInManager, RoleManager<IdentityRole> roleManager
            ,IAccountManager accountManager) 
        {
            _usermanger = usermanger;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager= roleManager;
            _accountManager= accountManager; 
        }



        [HttpPost]

        public async Task<IActionResult> Login(UserLoginDto user)
        {

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			
            var res = await _accountManager.LoginAsync(user);
            if (res !=null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Login Failed");
            }

               
        }
        
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
        public async Task<IActionResult> Register( UserRegisterDto userRegisterDto)
        {



            var result = await _accountManager.RegisterAsync(userRegisterDto);
            if (!result)
                return BadRequest("Registration failed");
            return Ok("Registration succeeded");
        }

    }
}
