using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SubCategoryController : Controller
    {
        private readonly SubCategoryService _subCategoryService;
        private readonly GenericApiService<Category> _categoryService;

        public SubCategoryController(SubCategoryService subCategoryService, GenericApiService<Category> categoryService)
        {
            _subCategoryService = subCategoryService;
            _categoryService = categoryService;
        }

        // GET: SubCategoryController
        public async Task<ActionResult> Index()
        {
            var subCategories = await _subCategoryService.GetAllSubCategories();

            return View(subCategories);
        }

        // GET: SubCategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);

            if (subCategory == null) {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: SubCategoryController/Create
        public async Task<ActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync("GetAll");

            ViewBag.Categories = categories.Select(c => new SelectListItem {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            });

            return View();
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategory subCategory)
        {
            try {
                if (ModelState.IsValid) {
                    await _subCategoryService.CreateSubCategory(subCategory);

                    return RedirectToAction(nameof(Index));
                }

                var categories = await _categoryService.GetAllAsync("GetAll");

                ViewBag.Categories = categories.Select(c => new SelectListItem {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });

                return View(subCategory);
            } catch (Exception ex) {
                var categories = await _categoryService.GetAllAsync("GetAll");

                ViewBag.Categories = categories.Select(c => new SelectListItem {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
                ModelState.AddModelError(string.Empty, $"Error creating sub-category: {ex.Message}");

                return View(subCategory);
            }
        }

        // GET: SubCategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);

            if (subCategory == null) {
                return NotFound();
            }

            var categories = await _categoryService.GetAllAsync("GetAll");

            ViewBag.Categories = categories.Select(c => new SelectListItem {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            });

            return View(subCategory);
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubCategory model)
        {
            try {
                var subCategory = await _subCategoryService.UpdateSubCategory(model);

                if (subCategory == null) {
                    return NotFound();
                }

                var categories = await _categoryService.GetAllAsync("GetAll");

                ViewBag.Categories = categories.Select(c => new SelectListItem {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });

                return RedirectToAction(nameof(Index));
            } catch {
                var categories = await _categoryService.GetAllAsync("GetAll");

                ViewBag.Categories = categories.Select(c => new SelectListItem {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                });
                ModelState.AddModelError(string.Empty, "Error updating sub-category");

                return View();
            }
        }

        // GET: SubCategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var subCategory = await _subCategoryService.GetSubCategoryById(id);

            return View(subCategory);
        }

        // POST: SubCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try {
                var subCategory = await _subCategoryService.DeleteSubCategory(id);

                if (subCategory == null) {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"Error deleting sub-category: {ex.Message}");

                return RedirectToAction(nameof(Index));
            }
        }
    }
}