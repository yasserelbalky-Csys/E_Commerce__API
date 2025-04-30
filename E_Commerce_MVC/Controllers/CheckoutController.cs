using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;

namespace E_Commerce_MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ProductApiService _productApiService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckoutController(OrderService orderService, ProductApiService productApiService,
            ShoppingCartService shoppingCartService, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _productApiService = productApiService;
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
        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel order)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var cartItems = await _shoppingCartService.GetByUserId(userId); // Get your current cart

            if (cartItems == null || !cartItems.Any()) {
                ModelState.AddModelError("", "Your cart is empty.");

                return View(order);
            }

            // Calculate total
            decimal totalAmount = cartItems.Sum(item => item.Count * item.Price);
            order.NetValue = totalAmount;
            order.UserId = userId;

            // Create Stripe session
            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = cartItems.Select(item => new SessionLineItemOptions {
                    PriceData = new SessionLineItemPriceDataOptions {
                        Currency = "usd",
                        UnitAmount = (long)(item.Price * 100), // Convert to cents
                        ProductData = new SessionLineItemPriceDataProductDataOptions { Name = item.ProductName }
                    },
                    Quantity = item.Count
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + Url.Action("Success", "Checkout"),
                CancelUrl = domain + Url.Action("Cancel", "Checkout")
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            TempData["OrderData"] = System.Text.Json.JsonSerializer.Serialize(order); // store order temporarily

            return Redirect(session.Url);
        }

        public async Task<IActionResult> Success()
        {
            var orderJson = TempData["OrderData"] as string;

            if (string.IsNullOrEmpty(orderJson)) return RedirectToAction("Index", "Home");

            var order = System.Text.Json.JsonSerializer.Deserialize<OrderViewModel>(orderJson);

            if (order != null) {
                order.PaymentStatus = "Paid";
                order.OrderStatus = "Pending"; // Set order status to pending
            }

            // Place order after successful payment
            var result = await _orderService.CreateOrder(order);

            if (result == "Order Placed Successfuly") {
                await _shoppingCartService.ClearCart();
                ViewBag.Message = "Your payment was successful, and your order has been placed!";

                return View("Success");
            }

            ViewBag.Message = "Payment succeeded, but order creation failed.";

            return View("Cancel");
        }

        public IActionResult Cancel()
        {
            ViewBag.Message = "Payment was canceled.";

            return View();
        }
    }
}