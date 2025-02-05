using BLL.Contracts;
using BLL.DTOs.UserDtos;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public UserController(UserManager<AppUser> usermanger, ITokenService tokenService,SignInManager <AppUser> signInManager) 
        {
            _usermanger = usermanger;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }



        [HttpPost("Login")]

        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _usermanger.Users.FirstOrDefaultAsync(u => u.UserName == userLogin.UserName.ToLower());
            if (user == null)
                return Unauthorized("Invalid UserName!!");
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Username not found or Password isn't correct!");
            return Ok(
            
                new UserTokenDto
                {
                    email = user.Email,
                    username = user.UserName,
                    token = _tokenService.CreateToken(user)
                }

            );
        }





        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] UserRegisterDto userRegisterDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appuser = new AppUser
                {
                    Email = userRegisterDto.Email,
                    UserName = userRegisterDto.UserName
                };
                var createduser=await _usermanger.CreateAsync(appuser,userRegisterDto.Password);
                if (createduser.Succeeded) 
                {
                    var roleresult = await _usermanger.AddToRoleAsync(appuser, "User");
                    if (roleresult.Succeeded)
                    {
                        return Ok(
                            new UserTokenDto
                            {
                                username = appuser.UserName,
                                email = appuser.Email,
                                token = _tokenService.CreateToken(appuser)
                            }
                            );
                    }
                    else 
                    {
                        return StatusCode(500,roleresult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500,createduser.Errors);
                }
            }
            catch (Exception ex) {

                return StatusCode(500, ex);
            }
            return Ok();
        }
    }
}
