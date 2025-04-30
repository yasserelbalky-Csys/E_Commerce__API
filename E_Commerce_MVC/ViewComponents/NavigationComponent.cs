using E_Commerce_MVC.ApiServices;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.ViewComponents
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationComponent : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NavigationComponent(ShoppingCartService shoppingCartService, HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartService = shoppingCartService;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetString("UserId");

            var carts = await _shoppingCartService.GetByUserId(userId);

            var model = new NavigationModel { Carts = carts, };

            /*// Simulate some asynchronous operation
            await Task.Delay(100); // Simulating async work*/

            return View(model);
        }
    }
}