using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class BrandsController : Controller
    {
        private readonly BrandService _brandService;

        public BrandsController(BrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllBrands();

            if (brands == null) {
                return NotFound();
            }

            return View(brands);
        }

        public async Task<IActionResult> Details(int id)
        {
            var brand = await _brandService.GetBrandById(id);

            if (brand == null) {
                return NotFound();
            }

            return View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandViewModel brand)
        {
            if (ModelState.IsValid) {
                await _brandService.CreateBrand(brand);

                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetBrandById(id);

            if (brand == null) {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandViewModel brand)
        {
            if (ModelState.IsValid) {
                await _brandService.UpdateBrand(brand);

                return RedirectToAction(nameof(Index));
            }

            return View(brand);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandService.GetBrandById(id);

            if (brand == null) {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _brandService.GetBrandById(id);

            if (brand == null) {
                return NotFound();
            }

            await _brandService.DeleteBrand(id);

            return RedirectToAction(nameof(Index));
        }
    }
}