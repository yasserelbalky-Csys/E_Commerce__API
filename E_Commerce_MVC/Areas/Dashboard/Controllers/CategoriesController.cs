using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoriesController : Controller
    {
        private readonly GenericApiService<Category> _apiService;

        public CategoriesController(GenericApiService<Category> apiService)
        {
            _apiService = apiService;
        }

        // GET: CategoryController
        public async Task<ActionResult> Index()
        {
            var categories = await _apiService.GetAllAsync("GetAll");

            return View(categories);
        }

        // GET: CategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var categroy = await _apiService.GetByIdAsync("Get", id);

            return View(categroy);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid) {
                await _apiService.CreateAsync("Post", category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _apiService.GetByIdAsync("Get", id);

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid) {
                await _apiService.UpdateAsync("Put", category);

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: CategoryController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _apiService.GetByIdAsync("Get", id);

            return View(category); // Pass the category model to the view
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CategoryId)
        {
            await _apiService.DeleteAsync("Delete", CategoryId);

            return RedirectToAction(nameof(Index));
        }
    }
}