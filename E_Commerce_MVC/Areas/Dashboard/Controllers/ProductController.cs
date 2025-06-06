﻿using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models.EntitiesViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ProductController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly SubCategoryService _subCategoryService;
        private readonly BrandService _brandService;
        private readonly ProductBalanceService _productBalanceService;

        public ProductController(ProductApiService productApiService, SubCategoryService subCategoryService,
            BrandService brandService, ProductBalanceService productBalanceService)
        {
            _productApiService = productApiService;
            _subCategoryService = subCategoryService;
            _brandService = brandService;
            _productBalanceService = productBalanceService;
        }

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var products = await _productApiService.GetAllProducts();

            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var product = await _productApiService.GetProductById(id);

            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductController/Create
        public async Task<ActionResult> Create()
        {
            var subCategories = await _subCategoryService.GetAllSubCategories();

            ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
            });

            var brands = await _brandService.GetAllBrands();

            ViewBag.Brands = brands.Select(b => new SelectListItem {
                Value = b.BrandId.ToString(), Text = b.BrandName
            });

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel product, IFormFile Image)
        {
            try {
                if (ModelState.IsValid) {
                    if (Image == null) {
                        ModelState.AddModelError(nameof(product.Img_Url), "Image is required.");

                        return View(product);
                    }

                    var imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products"))) {
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/images/Products"));
                    }

                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Products", imageName);

                    await using (var stream = new FileStream(savePath, FileMode.Create)) {
                        await Image.CopyToAsync(stream);
                    }

                    product.Img_Url = $"/images/Products/{imageName}";

                    await _productApiService.CreateProduct(product);

                    return RedirectToAction(nameof(Index));
                }

                var subCategories = await _subCategoryService.GetAllSubCategories();

                ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                    Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
                });

                var brands = await _brandService.GetAllBrands();

                ViewBag.Brands = brands.Select(b => new SelectListItem {
                    Value = b.BrandId.ToString(), Text = b.BrandName
                });

                return View(product);
            } catch (Exception ex) {
                var subCategories = await _subCategoryService.GetAllSubCategories();

                ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                    Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
                });

                var brands = await _brandService.GetAllBrands();

                ViewBag.Brands = brands.Select(b => new SelectListItem {
                    Value = b.BrandId.ToString(), Text = b.BrandName
                });

                ModelState.AddModelError(string.Empty, $"Cannot Create due to: {ex.Message}");

                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var product = await _productApiService.GetProductById(id);

            var subCategories = await _subCategoryService.GetAllSubCategories();

            ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
            });

            var brands = await _brandService.GetAllBrands();

            ViewBag.Brands = brands.Select(b => new SelectListItem {
                Value = b.BrandId.ToString(), Text = b.BrandName
            });

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel product, IFormFile Image)
        {
            try {
                if (ModelState.IsValid) {
                    if (Image != null) {
                        var imageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

                        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(),
                                "wwwroot/images/Products"))) {
                            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(),
                                "wwwroot/images/Products"));
                        }

                        var savePath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot/images/Products",
                            imageName);

                        await using (var stream = new FileStream(savePath, FileMode.Create)) {
                            await Image.CopyToAsync(stream);
                        }

                        product.Img_Url = $"/images/Products/{imageName}";
                    }

                    await _productApiService.UpdateProduct(product);

                    return RedirectToAction(nameof(Index));
                }

                var subCategories = await _subCategoryService.GetAllSubCategories();

                ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                    Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
                });

                var brands = await _brandService.GetAllBrands();

                ViewBag.Brands = brands.Select(b => new SelectListItem {
                    Value = b.BrandId.ToString(), Text = b.BrandName
                });

                return View(product);
            } catch (Exception ex) {
                var subCategories = await _subCategoryService.GetAllSubCategories();
                var brands = await _brandService.GetAllBrands();

                ViewBag.SubCategories = subCategories.Select(s => new SelectListItem {
                    Value = s.SubCategoryId.ToString(), Text = s.SubCategoryName
                });

                ViewBag.Brands = brands.Select(b => new SelectListItem {
                    Value = b.BrandId.ToString(), Text = b.BrandName
                });
                ModelState.AddModelError(string.Empty, $"Cannot Update due to: {ex.Message}");

                return View("Edit", product);
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var product = await _productApiService.GetProductById(id);

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int ProductId)
        {
            try {
                await _productApiService.DeleteProduct(ProductId);

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"Cannot Delete due to: {ex.Message}");

                return View();
            }
        }

        // GET: ProductController/AddProductBalance
        public async Task<ActionResult> AddProductBalance()
        {
            var products = await _productApiService.GetAllProducts();

            ViewBag.Products = products.Select(p =>
                new SelectListItem { Value = p.ProductId.ToString(), Text = p.ProductId.ToString() });

            return View();
        }

        [HttpGet]
        public IActionResult AddProductBalance(int productId)
        {
            var model = new ProductBalanceViewModel {
                ProductId = productId // Pre-fill the product ID
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProductBalance(ProductBalanceViewModel model)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, "Invalid data provided.");

                return View("Index");
            }

            try {
                await _productBalanceService.InsertProductBalance(model);

                return RedirectToAction(nameof(Index));
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"Error adding product balance: {ex.Message}");

                return RedirectToAction(nameof(Index));
            }
        }
    }
}