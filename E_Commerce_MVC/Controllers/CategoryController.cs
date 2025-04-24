using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly GenericApiService<Category> _apiService;

        public CategoryController(GenericApiService<Category> apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _apiService.GetAllAsync("GetAll");

            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _apiService.GetByIdAsync("Get", id);

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _apiService.GetByIdAsync("Get", id);

            return View(category);
        }

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

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _apiService.GetByIdAsync("Get", id);

            return View(category); // Pass the category model to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int CategoryId)
        {
            await _apiService.DeleteAsync("Delete", CategoryId);

            return RedirectToAction(nameof(Index));
        }
    }
}