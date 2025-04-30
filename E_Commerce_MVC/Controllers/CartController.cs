using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ShoppingCartService _shoppingCartService;
        private readonly ProductService _productService;
        private readonly HttpClient _httpClient;

        public CartController(ShoppingCartService shoppingCartService, IHttpContextAccessor httpContextAccessor,
            HttpClient httpClient, ProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            // get the current userid ? 
            var token = httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var userID = httpContextAccessor.HttpContext?.Session.ToJson();
            _productService = productService;
        }

        public async Task<IActionResult> CartIndex(int ProductId)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            var carts = await _shoppingCartService.GetByUserId(userId);
            var products = await _productService.GetAllProducts();

            ViewBag.Product = products;

            // Join products with their corresponding cart items
            var cartItemsWithProducts = carts.Select(cart => {
                var product = products.FirstOrDefault(p => p.ProductId == cart.ProductId);

                return new CartItemViewModel {
                    CartItem = cart, Product = product ?? new ProductViewModel() // or handle it however you need
                };
            }).ToList();

            // Update the session with the current cart item count
            _httpContextAccessor.HttpContext?.Session.SetInt32("CartItemCount", carts.Count);

            // Get the total price of the cart
            var totalPrice = await GetCartTotalPrice();
            ViewBag.SubTotal = totalPrice;
            var shippingCost = 25m;
            ViewBag.ShippingCost = shippingCost;
            ViewBag.TotalPrice = totalPrice + shippingCost;

            return View(cartItemsWithProducts);
        }

        public async Task<IActionResult> AddToCart(int ProductId)
        {
            var exists = await _shoppingCartService.CheckProductInCart(ProductId);
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId)) {
                return Json(new { success = false, message = "User not logged in." });
            }

            // Check if the product already exists in the cart
            var CartItems = await _shoppingCartService.GetByUserId(userId);

            // Get product details
            var product = await _productService.GetProductById(ProductId);

            var existingCartItem = CartItems.FirstOrDefault(c => c.ProductId == ProductId && c.UserId == userId);

            if (existingCartItem != null) {
                return Json(new { success = false, message = "This product is already in your cart." });
            }

            // Add the product to the cart
            var cart = new ShopingCartViewModel {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UserId = userId,
                Count = 1 // Default count is 1 when adding a new product
            };

            var result = await _shoppingCartService.AddToCart(cart);

            if (!result) {
                return Json(new { success = false, message = "Item is out of Stock!!!" });
            }

            // Get the updated cart item count
            var carts = await _shoppingCartService.GetByUserId(userId);
            _httpContextAccessor.HttpContext?.Session.SetInt32("CartItemCount", carts.Count);

            return Json(new { success = true, exists });
        }

        public async Task<List<ShopingCartViewModel>> GetCartItemsByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"GetByUserId/{userId}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<List<ShopingCartViewModel>>();

                return result ?? new List<ShopingCartViewModel>();
            }

            return new List<ShopingCartViewModel>();
        }

        public async Task<IActionResult> DeleteCart(int ProductId)
        {
            var result = await _shoppingCartService.DeleteCartProduct(ProductId);

            if (!result) {
                ModelState.AddModelError(string.Empty, "Failed to delete item from cart.");

                return View(nameof(CartIndex));
            }

            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            // Get the updated cart item count
            var carts = await _shoppingCartService.GetByUserId(userId);
            _httpContextAccessor.HttpContext?.Session.SetInt32("CartItemCount", carts.Count);

            return RedirectToAction(nameof(CartIndex));
        }

        public async Task<decimal> GetCartTotalPrice()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            var totalPrice = await _shoppingCartService.GetTotalCartPrice();

            return totalPrice;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromBody] ShopingCartViewModel model)
        {
            if (model == null || model.Count < 1) {
                return BadRequest(new { message = "Invalid cart data." });
            }

            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (userId != null)
                model.UserId = userId;

            var result = await _shoppingCartService.UpdateCart(model);

            if (!result) {
                return StatusCode(500, new { message = "Item is out of Stock" });
            }

            return Ok(new { message = "Cart updated successfully." });
        }
    }
}