using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using E_Commerce_MVC.Models.UserViewModel;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace E_Commerce_MVC.ApiServices
{
    public class AccountApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5097/api/User/");
            _httpContextAccessor = httpContextAccessor;

            var token = httpContextAccessor.HttpContext?.Session.GetString("JWTToken");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<List<UserTokenDTO>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("GetAllUsers");

            if (response.IsSuccessStatusCode) {
                var users = await response.Content.ReadFromJsonAsync<List<UserTokenDTO>>();

                if (users != null) {
                    foreach (var user in users) {
                        Console.WriteLine(user.ToJson());
                    }
                }

                return users ?? [];
            } else {
                return new List<UserTokenDTO>();
            }
        }

        public async Task<HttpResponseMessage> Register(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Register", model);

            return response;
        }

        public async Task<UserTokenDTO?> Login(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Login", model);

            if (response.IsSuccessStatusCode) {
                var userToken = await response.Content.ReadFromJsonAsync<UserTokenDTO>();

                return userToken;
            } else {
                return null;
            }
        }

        public List<Claim> DecodeToken(string token)
        {
            if (string.IsNullOrEmpty(token)) {
                return new List<Claim>();
            }

            var handler = new JwtSecurityTokenHandler().ReadJwtToken(token);

            return [.. handler.Claims];
        }

        public List<string> GetRolesFromToken(string token)
        {
            var claims = DecodeToken(token);

            return claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
        }

        public string? GetUserIdFromToken(string token)
        {
            var claims = DecodeToken(token);
            var userIdClaim = claims.FirstOrDefault(c => c.Type == "nameid");
            _httpContextAccessor?.HttpContext?.Session.SetString("UserId", userIdClaim?.Value!);

            return userIdClaim?.Value;
        }
    }
}