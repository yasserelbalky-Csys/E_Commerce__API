using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using E_Commerce_MVC.Models.UtilitesSupport.Helpers;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

public class PaymentController : Controller
{
    private readonly OrderService _orderService;
    private readonly ShoppingCartService _shoppingCartService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaymentController(OrderService orderService, ShoppingCartService shoppingCartService,
        IHttpContextAccessor httpContextAccessor)
    {
        _orderService = orderService;
        _shoppingCartService = shoppingCartService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = HttpContext.Session.GetObject<List<ShopingCartViewModel>>("Cart");

        var viewModel = new StripePaymentViewModel {
            PublishableKey = Environment.GetEnvironmentVariable("STRIPE_PUBLISHABLE_KEY")!,
            Amount = cart.Sum(c => c.Count * c.Price),
            CartItems = cart
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] OrderViewModel model)
    {
        var domain = $"{Request.Scheme}://{Request.Host}";

        var options = new SessionCreateOptions {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions> {
                new SessionLineItemOptions {
                    PriceData = new SessionLineItemPriceDataOptions {
                        Currency = "usd",
                        UnitAmount = (long)(model.NetValue * 100), // Stripe expects cents
                        ProductData =
                            new SessionLineItemPriceDataProductDataOptions { Name = "Order #" + model.OrderNo },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment",
            SuccessUrl = domain + "/Checkout/Success",
            CancelUrl = domain + "/Checkout/Cancel",
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return Json(new { sessionId = session.Id });
    }

    public async Task<IActionResult> Success()
    {
        var orderJson = TempData["OrderData"] as string;

        if (string.IsNullOrEmpty(orderJson)) return RedirectToAction("Index", "Home");

        var order = System.Text.Json.JsonSerializer.Deserialize<OrderViewModel>(orderJson);

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