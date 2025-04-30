using E_Commerce_MVC.Models.EntitiesViewModel;
using E_Commerce_MVC.Models.UtilitesSupport;
using System.Net.Http.Headers;
using System.Text.Json;

namespace E_Commerce_MVC.ApiServices
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/Product/");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var response = await _httpClient.GetAsync("Get");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<List<ProductViewModel>>();

                return result ?? [];
            }

            return [];
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<ProductViewModel>();

                return result ?? new ProductViewModel();
            }

            return new ProductViewModel();
        }

        public async Task<object> CreateProduct(ProductViewModel product)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", product);
            Console.WriteLine($"Respnse of Creating Product is: \n{response}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result ?? "Successded to Create a Product";
            }

            return new { success = false, message = "Failed to create product." };
        }

        public async Task<object> UpdateProduct(ProductViewModel product)
        {
            var response = await _httpClient.PutAsJsonAsync("Put", product);

            if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 0) {
                var result = await response.Content.ReadFromJsonAsync<object>();
                Console.WriteLine($"Result: {result}");

                return result ?? new { success = true, message = "Product updated successfully." };
            } else if (response.IsSuccessStatusCode) {
                return new { success = true, message = "Product updated successfully, but no content returned." };
            }

            throw new Exception($"Error updating product: {response.ReasonPhrase}");
        }

        public async Task<object> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync($"DeleteById?id={id}");

            if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 0) {
                var result = await response.Content.ReadFromJsonAsync<object>();

                return result ?? new { success = true, message = "Product deleted successfully." };
            }

            var errorResponse = await response.Content.ReadAsStringAsync();

            return new { success = false, message = errorResponse };
        }
    }
}