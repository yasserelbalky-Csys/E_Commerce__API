using E_Commerce_MVC.Models;
using E_Commerce_MVC.Models.EntitiesViewModel;
using NuGet.Protocol;
using System.Net.Http.Headers;
using System.Text.Json;

namespace E_Commerce_MVC.ApiServices
{
    public class StoreService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StoreService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            httpClient.BaseAddress = new Uri("http://localhost:5097/api/Store/");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            Console.WriteLine($"token is: {token}");

            if (!string.IsNullOrEmpty(token)) {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<StoreViewModel>> GetAllStores()
        {
            try {
                var response = await _httpClient.GetAsync("Get");

                if (response.IsSuccessStatusCode) {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<StoreViewModel>>();

                    return result ?? [];
                } else {
                    throw new Exception($"Error fetching stores: {response.ReasonPhrase}");
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);

                throw new Exception(ex.Message);
            }
        }

        public async Task<StoreViewModel> GetStoreById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<StoreViewModel>();

                return result ?? new StoreViewModel();
            } else {
                throw new Exception($"Error fetching store with ID {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<string> CreateStore(StoreViewModel store)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", store);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"result of creating store: {result}");

                return result;
            } else {
                throw new Exception($"Error creating store: {response.ReasonPhrase}");
            }
        }

        public async Task<ErrorViewModel> UpdateStore(StoreViewModel store)
        {
            var response = await _httpClient.PutAsJsonAsync("PUT", store);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<ErrorViewModel>();

                return result ?? new ErrorViewModel();
            } else {
                throw new Exception($"Error updating store: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteStore(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete?id={id}");

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content is: {responseContent} ");
        }
    }
}