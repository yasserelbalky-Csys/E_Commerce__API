using E_Commerce_MVC.Models.EntitiesViewModel;
using System.Net.Http.Headers;

namespace E_Commerce_MVC.ApiServices
{
    public class BrandService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/Brand/");

            //var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
            //Console.WriteLine($"Token is: {token}");
            //if (!string.IsNullOrEmpty(token)) {
            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //}
        }

        public async Task<IEnumerable<BrandViewModel>> GetAllBrands()
        {
            var response = await _httpClient.GetAsync("Get");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<BrandViewModel>>();

                return result ?? [];
            } else {
                throw new Exception("Error Getting All Brands");
            }
        }

        public async Task<BrandViewModel> GetBrandById(int id)
        {
            var response = await _httpClient.GetAsync($"GetById/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<BrandViewModel>();

                return result ?? new BrandViewModel();
            } else {
                throw new Exception("Error fetching Brand By ID");
            }
        }

        public async Task<object> CreateBrand(BrandViewModel brand)
        {
            var response = await _httpClient.PostAsJsonAsync("Post", brand);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadFromJsonAsync<BrandViewModel>();

                return result ?? new BrandViewModel();
            } else {
                throw new Exception("Error Creating Brand");
            }
        }

        public async Task<object> UpdateBrand(BrandViewModel brand)
        {
            var response = await _httpClient.PutAsJsonAsync("Put", brand);

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                throw new Exception("Error Updating Brand");
            }
        }

        public async Task<object> DeleteBrand(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete/{id}");

            if (response.IsSuccessStatusCode) {
                var result = await response.Content.ReadAsStringAsync();

                return result;
            } else {
                throw new Exception("Error Deleting Brand");
            }
        }
    }
}