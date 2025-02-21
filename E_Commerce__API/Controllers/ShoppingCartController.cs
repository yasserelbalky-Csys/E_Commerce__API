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
        private readonly ISessionManager _sessionManager; // Injected session manager

        public ShoppingCartController(IShoppingCartService shoppingCartService, ISessionManager sessionManager)
        {
            _shoppingCartService = shoppingCartService;
            _sessionManager = sessionManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get() {


            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //if (claimsIdentity != null && claimsIdentity.IsAuthenticated)
            //{
            //    // Authenticated user: Fetch from database
            //    return Ok(_shoppingCartService.GetShoppingCarts());
            //}
            //else
            //{
            // Unauthenticated user: Fetch from session

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




            var cart = _sessionManager.Get<List<ShoppingCartInsertDto>>("Cart") ?? new List<ShoppingCartInsertDto>();
            if (cart.Count == 0)
            {
                return Ok(_shoppingCartService.GetShoppingCarts());
               
            }
            else
            {
                return Ok(cart);
            }
            //}


            //return Ok(_shoppingCartService.GetShoppingCarts());     
        }



        //[HttpPost]
        //[Authorize]
        //public IActionResult Post(ShoppingCartInsertDto cart)
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;

        //    // Ensure the identity is not null
        //    if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
        //    {
        //        return Unauthorized(new { message = "User is not authenticated." });
        //    }

        //    var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //    // Ensure the claim exists
        //    if (userIdClaim == null)
        //    {
        //        return BadRequest(new { message = "User ID claim is missing from the token." });
        //    }

        //    var userId = userIdClaim.Value;

        //    cart.UserId = userId; // Ensure UserId is assigned before inserting



        //    bool exists = _shoppingCartService.GetShoppingCarts()
        //    .Any(cart1 => cart1.UserId == userId && cart1.ProductId == cart.ProductId);

        //    if (exists)
        //    {
        //        return BadRequest(new { message = "Duplicate product found in the cart." });
        //    }

        //    _shoppingCartService.InsertShoppingCart(cart);
        //    return Ok(new { message = "Shopping cart added successfully." });
        //}

        //[HttpPut]

        //public IActionResult Update(ShoppingCartUpdateDto cart)
        //{
        //    try
        //    {

        //        var claimidentiy=(ClaimsIdentity)User.Identity;
        //        if (claimidentiy == null || !claimidentiy.IsAuthenticated)
        //        {
        //            return Unauthorized(new { message = "User is not authenticated." });
        //        }

        //        var userIdClaim = claimidentiy.FindFirst(ClaimTypes.NameIdentifier);
        //        // Ensure the claim exists
        //        if (userIdClaim == null)
        //        {
        //            return BadRequest(new { message = "User ID claim is missing from the token." });
        //        }

        //        var userid = userIdClaim.Value;

        //        //var cartfromdatabase=_shoppingCartService.GetShoppingCarts().Select(cart1=>
        //        //cart1.UserId==userid && cart1.ProductId==cart.ProductId);
        //        //if (cartfromdatabase != null) {
        //        //    _shoppingCartService.UpdateShoppingCart(cart);
        //        //    return Ok(new { message = "Shopping cart updated successfully." });

        //        //}
        //        //else
        //        //{

        //        //    return Ok(new { message = "Not Found"});
        //        //}
        //        cart.UserId =userid;
        //        _shoppingCartService.UpdateShoppingCart(cart);
        //        return Ok();


        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the shopping cart.", details = ex.Message });
        //    }
        //}

        ////private ActionResult<string> GetUserIdFromClaims()
        ////{
        ////    var claimsIdentity = User.Identity as ClaimsIdentity;

        ////    if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
        ////    {
        ////        return Unauthorized("User is not authenticated.");
        ////    }

        ////    var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        ////    if (userIdClaim == null)
        ////    {
        ////        return BadRequest("User ID claim is missing from the token.");
        ////    }

        ////    return userIdClaim.Value;
        ////}
        
        
        [Authorize]
        [HttpPost]
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
            // Guest user: Use session for cart storage
            var sessionCart = _sessionManager.Get<List<ShoppingCartInsertDto>>("Cart") ?? new List<ShoppingCartInsertDto>();
            if (sessionCart.Any(c => c.ProductId == cart.ProductId))
            {
                return BadRequest(new { message = "Duplicate product found in the cart." });
            }

            _shoppingCartService.InsertShoppingCart(cart);
            sessionCart.Add(cart);
            _sessionManager.Set("Cart", sessionCart);
            return Ok(new { message = "Item added to cart (Session)." });
          
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(ShoppingCartUpdateDto cart)
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
            cart.UserId = userId;
            _shoppingCartService.UpdateShoppingCart(cart);

            var sessionCart = _sessionManager.Get<List<ShoppingCartInsertDto>>("Cart") ?? new List<ShoppingCartInsertDto>();

            var existingItem = sessionCart.FirstOrDefault(c => c.ProductId == cart.ProductId);

            if (existingItem != null)
            {
                existingItem.Count = cart.Count;
                _sessionManager.Set("Cart", sessionCart);
                return Ok(new { message = "Shopping cart updated successfully in database and session." });
              
            }
            else
            {
                //existingItem.Count = cart.Count;
                //existingItem.ProductId = cart.ProductId;
                // existingItem.UserId = cart.UserId;

                sessionCart.Add(new ShoppingCartInsertDto
                {
                    ProductId = cart.ProductId,
                    UserId = cart.UserId,
                    Count = cart.Count
                });

                _sessionManager.Set("Cart", sessionCart);


               
                return Ok(new { message = "Shopping cart updated successfully in database and insert in session first time." });
            }

            
            
          /*  else
            {
                // Guest user: Update session-based cart
                var sessionCart = _sessionManager.Get<List<ShoppingCartInsertDto>>("Cart") ?? new List<ShoppingCartInsertDto>();

                var existingItem = sessionCart.FirstOrDefault(c => c.ProductId == cart.ProductId);
                if (existingItem != null)
                {
                    existingItem.Count = cart.Count;
                    _sessionManager.Set("Cart", sessionCart);
                    return Ok(new { message = "Cart updated in session." });
                }

                return NotFound(new { message = "Product not found in the cart." });
            }*/
        }




        [Authorize]
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
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



            


            var cartItems = _shoppingCartService.GetShoppingCarts();
            var item = cartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            //item.UserId = userId;

            if (item == null)
            {
                return NotFound(new { message = "Product not found in the cart." });
            }

            _shoppingCartService.DeleteShoppingCart(productId);

            var sessionCart = _sessionManager.Get<List<ShoppingCartInsertDto>>("Cart") ?? new List<ShoppingCartInsertDto>();
            sessionCart.RemoveAll(c => c.ProductId == productId);
            _sessionManager.Set("Cart", sessionCart);

            return Ok(new { message = "Product removed from cart and database successfully." });
        }

    }
}
