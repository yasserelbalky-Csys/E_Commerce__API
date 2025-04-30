using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class ProductBalanceService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductBalanceService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<bool> InsertProductBalance(ProductBalanceViewModel productBalance)
        {
            var response = await _httpClient.PostAsJsonAsync("api/CurrentProductBalance/Post", productBalance);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response of Creating Product Balance: \n{result}");

                return true;
            } else {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Error: \n{result}");

                return false;
            }
        }
    }
}