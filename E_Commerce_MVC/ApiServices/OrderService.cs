using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/Order/");
            _httpContextAccessor = httpContextAccessor;

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync("GetAll");

            if (response.IsSuccessStatusCode) {
                var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderViewModel>>();

                return orders ?? [];
            }

            throw new Exception($"Error Fetching Order: {response.ReasonPhrase}");
        }

        public async Task<OrderViewModel> GetOrderById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById/{id}");

            if (response.IsSuccessStatusCode) {
                var order = await response.Content.ReadFromJsonAsync<OrderViewModel>();

                return order ?? new OrderViewModel();
            }

            throw new Exception($"Error Fetching Order: {response.ReasonPhrase}");
        }
    }
}