using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Controllers;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OrderService _orderService;
        private readonly HttpClient _httpClient;

        public OrderController(ILogger<OrdersController> logger, IHttpContextAccessor httpContextAccessor,
            OrderService orderService, HttpClient httpClient)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
            _httpClient = httpClient;
        }

        [Authorize]
        // GET: OrdersController
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");
            var orders = await _orderService.GetAllOrders();

            var orderList = orders.Where(o => o.UserId == userId).ToList();

            var paginatedOrders = PaginatedList<OrderViewModel>.CreateFromList(orders, page, pageSize);

            return View(paginatedOrders);
        }

        // GET: OrdersController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var ordersDetails = await _orderService.GetOrderDetails(id);

            // convert DTOs to View Models (if necessary)   
            var viewModel = new OrderDetailsPageViewModel {
                Order = order,
                OrderDetails = ordersDetails.Select(od => new OrderDetailsViewModel {
                    LineNo = od.LineNo,
                    ProductId = od.ProductId,
                    ProductName = od.ProductName,
                    ProductDescription = od.ProductDescription,
                    Qty = od.Qty,
                    ProductPrice = od.ProductPrice,
                    TotalValue = od.TotalValue
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: OrdersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try {
                return RedirectToAction(nameof(Index));
            } catch {
                return View();
            }
        }

        // GET: OrdersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: OrdersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try {
                var response = await _orderService.UpdateOrder(id);

                return RedirectToAction(nameof(Index));
            } catch {
                return View("Edit");
            }
        }

        // GET: OrdersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: OrdersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                var response = await _orderService.DeleteOrder(id);

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View(nameof(Index));
            }
        }

        public async Task<ActionResult> ConfirmOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);

            if (order == null)
                return NotFound();

            await _orderService.UpdateOrder(id);

            return RedirectToAction(nameof(Index));
        }
    }
}