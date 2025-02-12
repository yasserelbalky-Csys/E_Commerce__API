using BLL.Contracts;
using BLL.DTOs.ShoppingCartDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce__API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        [HttpGet]
        public IActionResult Get() {
            return Ok(_shoppingCartService.GetShoppingCarts());     
        }



        [HttpPost]
        [Authorize]
        public IActionResult Post(ShoppingCartInsertDto cart)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            // Ensure the identity is not null
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
            {
                return Unauthorized(new { message = "User is not authenticated." });
            }

            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // Ensure the claim exists
            if (userIdClaim == null)
            {
                return BadRequest(new { message = "User ID claim is missing from the token." });
            }

            var userId = userIdClaim.Value;

            cart.UserId = userId; // Ensure UserId is assigned before inserting



            bool exists = _shoppingCartService.GetShoppingCarts()
            .Any(cart1 => cart1.UserId == userId && cart1.ProductId == cart.ProductId);

            if (exists)
            {
                return BadRequest(new { message = "Duplicate product found in the cart." });
            }

            _shoppingCartService.InsertShoppingCart(cart);
            return Ok(new { message = "Shopping cart added successfully." });
        }

        [HttpPut]

        public IActionResult Update(ShoppingCartUpdateDto cart)
        {
            try
            {

                var claimidentiy=(ClaimsIdentity)User.Identity;
                if (claimidentiy == null || !claimidentiy.IsAuthenticated)
                {
                    return Unauthorized(new { message = "User is not authenticated." });
                }

                var userIdClaim = claimidentiy.FindFirst(ClaimTypes.NameIdentifier);
                // Ensure the claim exists
                if (userIdClaim == null)
                {
                    return BadRequest(new { message = "User ID claim is missing from the token." });
                }

                var userid = userIdClaim.Value;

                //var cartfromdatabase=_shoppingCartService.GetShoppingCarts().Select(cart1=>
                //cart1.UserId==userid && cart1.ProductId==cart.ProductId);
                //if (cartfromdatabase != null) {
                //    _shoppingCartService.UpdateShoppingCart(cart);
                //    return Ok(new { message = "Shopping cart updated successfully." });

                //}
                //else
                //{

                //    return Ok(new { message = "Not Found"});
                //}
                cart.UserId =userid;
                _shoppingCartService.UpdateShoppingCart(cart);
                return Ok();

                
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the shopping cart.", details = ex.Message });
            }
        }

    }
}
