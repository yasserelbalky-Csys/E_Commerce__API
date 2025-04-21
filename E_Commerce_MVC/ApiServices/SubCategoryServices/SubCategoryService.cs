using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices.SubCategoryServices
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
        }

        public async Task TestMethod()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:44312/api/SubCategory/GetAllSubCategories");
            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            if (token != null) {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                // Process the result as needed
                Console.WriteLine(result);
            } else {
                // Handle the error response
                Console.WriteLine($"Error: {response.Content}");
            }

        }
    }
}