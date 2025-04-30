using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class CurrentProductBalanceService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentProductBalanceService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/CurrentProductBalance/");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (token != null) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<ProductBalanceViewModel>> GetAllProductBalance()
        {
            var response = await _httpClient.GetAsync("GetAll");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ProductBalanceViewModel>>() ?? [];
        }

        public async Task<ProductBalanceViewModel> GetProductBalanceById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById/{id}");

            if (!response.IsSuccessStatusCode) {
                return new ProductBalanceViewModel();
            }

            return await response.Content.ReadFromJsonAsync<ProductBalanceViewModel>() ?? new ProductBalanceViewModel();
        }

        public async Task<string> InsertProductBalance(ProductBalanceViewModel dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", dto);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateProductBalance(ProductBalanceViewModel dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Update", dto);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteProductBalance(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete/{id}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}