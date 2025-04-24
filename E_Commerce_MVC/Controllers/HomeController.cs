using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace E_Commerce_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;
    private readonly GenericApiService<Category> _categoryService;
    private readonly BrandService _brandService;
    private readonly StoreService _storeService;
    private readonly SubCategoryService _subCategoryService;

    public HomeController(ILogger<HomeController> logger, ProductService productService,
        GenericApiService<Category> categoryService, BrandService brandService, StoreService storeService,
        SubCategoryService subCategoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _storeService = storeService;
        _subCategoryService = subCategoryService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var user = User.Identity;

        var jsonSerialized = JsonConvert.SerializeObject(user,
            new JsonSerializerSettings
                { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, Formatting = Formatting.Indented });

        var products = await _productService.GetAllProducts();
        var categories = await _categoryService.GetAllAsync("GetAll");
        var brands = await _brandService.GetAllBrands();
        var stores = await _storeService.GetAllStores();
        var subCategories = await _subCategoryService.GetAllSubCategories();

        var homeViewModel = new HomeViewModel {
            Products = products,
            Categories = (List<Category>)categories,
            Brands = (List<BrandViewModel>)brands,
            Stores = (List<StoreViewModel>)stores,
            SubCategories = (List<SubCategory>)subCategories
        };

        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}