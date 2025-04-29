using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductService _productService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(OrderService orderService, ProductService productService,
            ShoppingCartService shoppingCartService, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _productService = productService;
            _shoppingCartService = shoppingCartService;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            var orders = await _orderService.GetAllOrders();
            var ordersList = orders.Where(o => o.UserId == userId).ToList();

            ViewBag.OrdersList = ordersList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel order)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId)) {
                return RedirectToAction("Login", "Account");
            }

            order.UserId = userId;
            var result = await _orderService.CreateOrder(order);

            if (result == "Order Placed Successfuly") {
                // Clear the cart after successful order creation
                await _shoppingCartService.ClearCart();

                return RedirectToAction("Index", "Home");
            } else {
                // Handle error case
                ModelState.AddModelError("", result);

                return View(order);
            }
        }
    }
}