using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class SubCategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubCategoryService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/SubCategory/");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
        {
            var response = await _httpClient.GetAsync("Get");
            Console.WriteLine($"response is {response}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<SubCategory>>();

                return result ?? [];
            }

            throw new Exception($"Error fetching SubCategories Data: {response.ReasonPhrase}");
        }

        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"Get/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<SubCategory>();

                return result ?? throw new Exception($"Error in fetching Data: Response is null");
            }

            throw new Exception($"Error fetching Data: {response.ReasonPhrase}");
        }

        public async Task<object> CreateSubCategory(SubCategory subCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", subCategory);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }

            throw new Exception($"Error Creating Sub-Category: {response.ReasonPhrase}");
        }

        public async Task<object> UpdateSubCategory(SubCategory subCategory)
        {
            var response = await _httpClient.PutAsJsonAsync("Put", subCategory);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"result is: {result}");

                return result;
            }

            throw new Exception($"Error Updating Sub-Category: {response.ReasonPhrase}");
        }

        public async Task<object> DeleteSubCategory(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete?id={id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"result is: {result}");

                return result;
            }

            throw new Exception($"Error Deleting Sub-Category: {response.ReasonPhrase}");
        }
    }
}