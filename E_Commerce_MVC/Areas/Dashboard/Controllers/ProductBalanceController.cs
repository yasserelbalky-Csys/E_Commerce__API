using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProductBalanceController : Controller
    {
        private readonly CurrentProductBalanceService _balanceService;

        public ProductBalanceController(CurrentProductBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // Get all product balances (this is a List<ProductBalanceViewModel>)
            var balances = await _balanceService.GetAllProductBalance();
            ViewBag.TotalCount = balances.Count;

            // Use the PaginatedList's CreateAsync method to paginate the list
            var paginatedBalances = PaginatedList<ProductBalanceViewModel>.CreateFromList(balances, page, pageSize);

            return View(paginatedBalances);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _balanceService.GetProductBalanceById(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductBalanceViewModel dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var message = await _balanceService.InsertProductBalance(dto);
            TempData["Message"] = message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _balanceService.GetProductBalanceById(id);

            if (item == null)
                return NotFound();

            var dto = new ProductBalanceViewModel {
                Id = item.Id,
                ProductId = item.ProductId,
                Qty = item.Qty,
                OrderNo = item.OrderNo,
                StoreId = item.StoreId,
                b_pending = item.b_pending,
                b_cancel = item.b_cancel
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductBalanceViewModel dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var message = await _balanceService.UpdateProductBalance(dto);
            TempData["Message"] = message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _balanceService.GetProductBalanceById(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var message = await _balanceService.DeleteProductBalance(id);
            TempData["Message"] = message;

            return RedirectToAction("Index");
        }
    }
}