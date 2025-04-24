using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace E_Commerce_MVC.Services
{
    public class GenericApiService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Updated method to handle CS8603: Possible null reference return
        public async Task<IEnumerable<T>> GetAllAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();

                return result ?? []; // Return an empty collection if result is null
            }

            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        // Updated method to handle CS8603: Possible null reference return
        public async Task<T> GetByIdAsync(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<T>();

                return result ?? throw new Exception("Error fetching data: Response content is null.");
            }

            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }

        // POST: Create a new entity
        public async Task CreateAsync(string endpoint, T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, entity);

            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error creating entity: {response.ReasonPhrase}");
            }
        }

        // PUT: Update an existing entity
        public async Task UpdateAsync(string endpoint, T entity)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, entity);

            if (!response.IsSuccessStatusCode) {
                throw new Exception($"Error updating entity: {response.ReasonPhrase}");
            }
        }

        // DELETE: Delete an entity by ID
        public async Task DeleteAsync(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"Get/{id}");

            if (response.IsSuccessStatusCode) {
                var response2 = await _httpClient.DeleteAsync($"{endpoint}?id={id}");

                if (!response2.IsSuccessStatusCode) {
                    throw new Exception($"Error deleting entity: {response2.ReasonPhrase}");
                }
            }
        }

        // Getting sub Category 

        public async Task<IEnumerable<T>> GetSubCatAsync(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");
            Console.WriteLine($"Response from getting subCategories is: {response}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();

                return result ?? throw new Exception("Error fetching data: Response content is null.");
            }

            throw new Exception($"Error fetching data: {response.ReasonPhrase}");
        }
    }
}