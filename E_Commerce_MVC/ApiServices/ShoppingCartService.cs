using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class ShoppingCartService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/ShoppingCart/");

            var token = httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<ShopingCartViewModel>> GetAllCarts()
        {
            var response = await _httpClient.GetAsync("GetAllCarts");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<List<ShopingCartViewModel>>();

                return result ?? new List<ShopingCartViewModel>();
            }

            return new List<ShopingCartViewModel>();
        }

        public async Task<ShopingCartViewModel> GetByUserId(string? userId)
        {
            var response = await _httpClient.GetAsync($"GetByUserId/{userId}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<ShopingCartViewModel>();

                return result ?? new ShopingCartViewModel();
            }

            return new ShopingCartViewModel();
        }

        public async Task<bool> AddToCart(ShopingCartViewModel cart)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", cart);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                return true;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCart(ShopingCartViewModel cart)
        {
            var response = await _httpClient.PutAsJsonAsync("Update", cart);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                return true;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCartProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                return true;
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClearCart()
        {
            var response = await _httpClient.DeleteAsync($"clearUserCart");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                return true;
            }

            return response.IsSuccessStatusCode;
        }
    }
}