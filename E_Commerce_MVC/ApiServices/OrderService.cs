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

        // Create order method through api taking order object and return error message
        public async Task<string> CreateOrder(OrderViewModel order)
        {
            var response = await _httpClient.PostAsJsonAsync("PostMaster", order);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                var errorMessage = await response.Content.ReadAsStringAsync();

                return errorMessage;
            }
        }

        // Update order method through api taking order object and return error message
        public async Task<string> UpdateOrder(OrderViewModel order)
        {
            var response = await _httpClient.PutAsJsonAsync("PutMaster", order);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                var errorMessage = await response.Content.ReadAsStringAsync();

                return errorMessage;
            }
        }

        // Update Order by OrderNO method thourgh the API taking order numb as int and returning error message  

        public async Task<string> UpdateOrder(int id)
        {
            var response = await _httpClient.PutAsJsonAsync($"Update/{id}", id);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                var errorMessage = await response.Content.ReadAsStringAsync();

                return errorMessage;
            }
        }

        // Delete Order method through api taking order object and return error message

        public async Task<string> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete?OrderNo={id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                var errorMessage = await response.Content.ReadAsStringAsync();

                return errorMessage;
            }
        }

        // Get order Details through api taking order number as int and returning list of  orders details
        public async Task<IEnumerable<OrderDetailsViewModel>> GetOrderDetails(int id)
        {
            var response = await _httpClient.GetAsync($"GetDetailsByorderid?masterid={id}");

            if (response.IsSuccessStatusCode) {
                var orderDetails = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDetailsViewModel>>();

                return orderDetails ?? [];
            }

            throw new Exception($"Error Fetching Order Details: {response.ReasonPhrase}");
        }
    }
}