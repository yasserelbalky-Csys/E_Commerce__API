using BLL.DTOs.UserDtos;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanger;


        public UserController(UserManager<AppUser> usermanger) 
        {
            _usermanger = usermanger;
        
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
                        return Ok("User Created");
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
